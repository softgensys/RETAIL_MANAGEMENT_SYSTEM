using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace softgen
{
    public partial class frmU_GrantScreenPermission : Form, Interface_for_Common_methods.ISearchableForm
    {
        public DbConnector dbConnector;
        private string mstrEntBy, mstrEntOn, mstrAuthBy, mstrAuthOn, chkItemid;
        public bool mblnSearch, mblnDataEntered;
        private bool isUpdatingCells = false;


        public frmU_GrantScreenPermission()
        {
            InitializeComponent();
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CurrentCellDirtyStateChanged += dataGridView1_CurrentCellDirtyStateChanged;
            //cboUser.KeyDown += new KeyEventHandler(cboUser_KeyDown);
        }


        private void LoadUserPermissions()
        {
            try
            {
                // Clear existing rows
                dataGridView1.Rows.Clear();

                // Fetch data from s_menu and s_logopt
                DbConnector dbConnector = new DbConnector();
                dbConnector.connection = new OdbcConnection(dbConnector.connectionString);
                dbConnector.connection.Open();

                string query = @"SELECT a.Prog_Name, a.Prog_id, b.Can_Add, b.Can_Modify, b.Can_Delete, b.Can_Inquire, b.Can_Post, b.Can_Print 
                         FROM s_menu a 
                         LEFT JOIN s_logopt b ON a.Prog_id = b.Prog_id
                         WHERE b.Login_id = ?";
                using (var cmd = new OdbcCommand(query, dbConnector.connection))
                {
                    cmd.Parameters.AddWithValue("@Login_id", cboUser.SelectedItem);

                    using (var reader = cmd.ExecuteReader())
                    {
                        //*****CREATING A DICTIONARY TYPE FOR CATEGORIZING THE DATA*********
                        //***HERE, KEY:CATEGORY and VALUE:'LIST' HAVING <('TUPLES'--> BECOZ HAVING FIXED TYPES OF DATA(FOR EVERY CATEGORY))> ***
                        var categorizedData = new Dictionary<string, List<(string ProgName, bool CanAdd, bool CanModify, bool CanDelete, bool CanInquire, bool CanPost, bool CanPrint)>>()
                {
                    { "MASTER", new List<(string, bool, bool, bool, bool, bool, bool)>() },
                    { "TRANSACTION", new List<(string, bool, bool, bool, bool, bool, bool)>() },
                    { "REPORTS", new List<(string, bool, bool, bool, bool, bool, bool)>() },
                    { "UTILITIES", new List<(string, bool, bool, bool, bool, bool, bool)>() },
                    { "OTHER", new List<(string, bool, bool, bool, bool, bool, bool)>() }
                };

                        while (reader.Read())
                        {
                            string progName = reader["Prog_Name"].ToString();
                            string progId = reader["Prog_id"].ToString();
                            bool canAdd = reader["Can_Add"].ToString() == "1";
                            bool canModify = reader["Can_Modify"].ToString() == "1";
                            bool canDelete = reader["Can_Delete"].ToString() == "1";
                            bool canInquire = reader["Can_Inquire"].ToString() == "1";
                            bool canPost = reader["Can_Post"].ToString() == "1";
                            bool canPrint = reader["Can_Print"].ToString() == "1";

                            string category = progId.Length > 3 ? progId[3] switch
                            {
                                'M' => "MASTER",
                                'T' => "TRANSACTION",
                                'R' => "REPORTS",
                                'U' => "UTILITIES",
                                _ => "OTHER"
                            } : "OTHER";

                            //***INSERTING CATEGORY WISE VALUES
                            categorizedData[category].Add((progName, canAdd, canModify, canDelete, canInquire, canPost, canPrint));
                        }

                        // Populate DataGridView with categorized data
                        foreach (var category in categorizedData.Keys)
                        {
                            if (categorizedData[category].Count > 0)
                            {
                                // Add category header row
                                int rowIndex = dataGridView1.Rows.Add($"****{category}****");
                                var categoryRow = dataGridView1.Rows[rowIndex];
                                categoryRow.DefaultCellStyle.BackColor = Color.LightGreen;
                                categoryRow.DefaultCellStyle.Font = new Font(dataGridView1.Font, FontStyle.Bold);
                                categoryRow.ReadOnly = true;

                                // Set cells in category header row to be checkable/unchecked except for REPORTS category
                                for (int i = 1; i < dataGridView1.Columns.Count; i++)
                                {
                                    if (category == "REPORTS" && i != 7) // Only ASSIGN (2nd column) and CanPrint (last column) should be editable for REPORTS
                                    {
                                        dataGridView1.Rows[rowIndex].Cells[i].ReadOnly = true;
                                        dataGridView1.Rows[rowIndex].Cells[i].Style.BackColor = Color.LightGray;
                                    }
                                    else if (category == "UTILITIES" && i != 3) // Only CanModify (4th column) editable for REPORTS
                                    {
                                        dataGridView1.Rows[rowIndex].Cells[i].ReadOnly = true;
                                        dataGridView1.Rows[rowIndex].Cells[i].Style.BackColor = Color.LightGray;
                                    }

                                    else
                                    {
                                        dataGridView1.Rows[rowIndex].Cells[i].ReadOnly = false;
                                    }
                                }

                                // Add rows for each program under the category
                                foreach (var prog in categorizedData[category])
                                {
                                    bool assign = prog.CanAdd || prog.CanModify || prog.CanDelete || prog.CanInquire || prog.CanPost || prog.CanPrint;
                                    int newRowIndex = dataGridView1.Rows.Add(prog.ProgName, assign, prog.CanAdd, prog.CanModify, prog.CanDelete, prog.CanInquire, prog.CanPost, prog.CanPrint);

                                    // If category is REPORTS, disable all checkboxes except ASSIGN and CanPrint
                                    if (category == "REPORTS")
                                    {
                                        for (int i = 0; i < dataGridView1.Columns.Count; i++)
                                        {
                                            if (i != 7) // Only ASSIGN (2nd column) and CanPrint (last column)
                                            {
                                                dataGridView1.Rows[newRowIndex].Cells[i].ReadOnly = true;
                                                dataGridView1.Rows[newRowIndex].Cells[i].Style.BackColor = Color.LightGray; // Optional: change the background color to indicate it's disabled
                                            }
                                        }
                                    }

                                    // If category is REPORTS, disable all checkboxes except ASSIGN and CanPrint
                                    else if (category == "UTILITIES")
                                    {
                                        for (int i = 0; i < dataGridView1.Columns.Count; i++)
                                        {
                                            if (i != 3) // Only CanModify (4th column)
                                            {
                                                dataGridView1.Rows[newRowIndex].Cells[i].ReadOnly = true;
                                                dataGridView1.Rows[newRowIndex].Cells[i].Style.BackColor = Color.LightGray; // Optional: change the background color to indicate it's disabled
                                            }
                                        }
                                    }


                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void SetSearchVar(bool StartVal)
        {
            // Implementation of SetSearchVar
            // You can define the behavior of SetSearchVar here

            // mblnSearch = StartVal;
        }

        public bool GetDEStatus()
        {
            return mblnDataEntered == true ? true : false;
        }

        public void SaveForm()
        {
            try
            {
                // Fetch data from DataGridView
                var permissions = new List<(string FormName, int CanAdd, int CanModify, int CanDelete, int CanInquire, int CanPost, int CanPrint)>();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["FormName"].Value != null && !row.Cells["FormName"].Value.ToString().StartsWith("****"))
                    {
                        string formName = row.Cells["FormName"].Value.ToString();
                        int canAdd = Convert.ToBoolean(row.Cells["Add"].Value) ? 1 : 0;
                        int canModify = Convert.ToBoolean(row.Cells["Modify"].Value) ? 1 : 0;
                        int canDelete = Convert.ToBoolean(row.Cells["Delete"].Value) ? 1 : 0;
                        int canInquire = Convert.ToBoolean(row.Cells["Enquire"].Value) ? 1 : 0;
                        int canPost = Convert.ToBoolean(row.Cells["Post"].Value) ? 1 : 0;
                        int canPrint = Convert.ToBoolean(row.Cells["Print"].Value) ? 1 : 0;

                        permissions.Add((formName, canAdd, canModify, canDelete, canInquire, canPost, canPrint));
                    }
                }

                // Update database
                    DbConnector dbConnector = new DbConnector();
                    dbConnector.connection = new OdbcConnection(dbConnector.connectionString);
                    dbConnector.connection.Open();

                    foreach (var permission in permissions)
                    {
                        string query = @"UPDATE s_logopt a
                                 JOIN s_menu b ON a.Prog_id = b.Prog_id
                                 SET a.Can_Add = ?, a.Can_Modify = ?, a.Can_Delete = ?, a.Can_Inquire = ?, a.Can_Post = ?, a.Can_Print = ?
                                 WHERE a.Login_id = ? AND b.Prog_Name = ?";

                        using (var cmd = new OdbcCommand(query, dbConnector.connection))
                        {
                            cmd.Parameters.AddWithValue("@Can_Add", permission.CanAdd);
                            cmd.Parameters.AddWithValue("@Can_Modify", permission.CanModify);
                            cmd.Parameters.AddWithValue("@Can_Delete", permission.CanDelete);
                            cmd.Parameters.AddWithValue("@Can_Inquire", permission.CanInquire);
                            cmd.Parameters.AddWithValue("@Can_Post", permission.CanPost);
                            cmd.Parameters.AddWithValue("@Can_Print", permission.CanPrint);
                            cmd.Parameters.AddWithValue("@Logid", cboUser.SelectedItem); // Assuming cboUser.SelectedItem holds the Logid
                            cmd.Parameters.AddWithValue("@Prog_Name", permission.FormName);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    dbConnector.connection.Close();            

                MessageBox.Show("Permissions updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void SearchForm()
        {

        }
        public void UnsavedData()
        {

        }

        public void ClearForm()
        {
            int i, j;


        }

        public void check_temp_login_sytemname()
        {

        }

        public void PrintForm()
        {

        }

        public void ResetControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                // Check if the control is a TextBox and its ID starts with "txt"
                if (control is System.Windows.Forms.TextBox && control.Name != null && control.Name.StartsWith("txt"))
                {
                    System.Windows.Forms.TextBox textBox = (System.Windows.Forms.TextBox)control;

                    // Reset the value
                    textBox.Text = "";

                    // Enable the TextBox
                    textBox.Enabled = true;
                }

                // Recursively call the method for nested controls
                if (control.Controls.Count > 0)
                {
                    ResetControls(control.Controls);
                }
            }


        }

        private void cboUser_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void frmU_GrantScreenPermission_Activated(object sender, EventArgs e)
        {
            DeTools.ClearStatusBarHelp();
            DeTools.ActiveFileMenu(this);
            DeTools.CreatedBy(mstrEntBy, mstrEntOn);
            DeTools.PostedBy(mstrAuthBy, mstrAuthOn);
        }

        private void frmU_GrantScreenPermission_Load(object sender, EventArgs e)
        {
            DeTools.DisplayForm(this, 586, 933);
            FillUserComboBox();
        }

        private void FillUserComboBox()
        {
            try
            {
                // Clear existing items
                cboUser.Items.Clear();

                // Create a new DbConnector and open the connection
                DbConnector dbConnector = new DbConnector();
                dbConnector.connection = new OdbcConnection(dbConnector.connectionString);
                dbConnector.connection.Open();

                // Define the query to get Login_id
                string query = "SELECT Login_id FROM s_login;";

                using (var cmd = new OdbcCommand(query, dbConnector.connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        // Add each Login_id to the ComboBox
                        while (reader.Read())
                        {
                            cboUser.Items.Add(reader["Login_id"].ToString());
                        }
                    }
                }

                dbConnector.connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while filling the ComboBox: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboUser.SelectedItem != null)
            {
                LoadUserPermissions();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (isUpdatingCells)
            {
                return;
            }

            if (e.RowIndex >= 0)
            {
                string formName = dataGridView1.Rows[e.RowIndex].Cells["FormName"].Value.ToString();
                bool isCategoryHeader = formName.StartsWith("****") && formName.EndsWith("****");

                // If the changed cell is in the "Assign" column
                if (e.ColumnIndex == dataGridView1.Columns["Assign"].Index)
                {
                    bool isChecked = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells["Assign"].Value);

                    if (isCategoryHeader)
                    {
                        // Apply to all rows under the same category
                        ApplyToAllRowsUnderCategory(e.RowIndex, isChecked, "Assign");
                    }
                    else
                    {
                        string category = GetCategoryFromFormName(formName);

                        // Handle the specific behavior for "REPORTS" category
                        if (category == "REPORTS")
                        {
                            isUpdatingCells = true;
                            dataGridView1.Rows[e.RowIndex].Cells["CanPrint"].Value = isChecked;
                            isUpdatingCells = false;
                        }
                        // Handle the specific behavior for "UTILITIES" category
                        else if (category == "UTILITIES")
                        {
                            isUpdatingCells = true;
                            dataGridView1.Rows[e.RowIndex].Cells["CanModify"].Value = isChecked;
                            isUpdatingCells = false;
                        }
                        else
                        {
                            // Update all checkbox columns in the same row based on the Assign column
                            isUpdatingCells = true;
                            for (int i = 2; i < dataGridView1.Columns.Count; i++) // Starting from index 2 to skip FormName and Assign columns
                            {
                                if (dataGridView1.Columns[i].GetType() == typeof(DataGridViewCheckBoxColumn))
                                {
                                    dataGridView1.Rows[e.RowIndex].Cells[i].Value = isChecked;
                                }
                            }
                            isUpdatingCells = false;
                        }
                    }
                }
                // If the changed cell is in any other checkbox column
                else if (dataGridView1.Columns[e.ColumnIndex].GetType() == typeof(DataGridViewCheckBoxColumn))
                {
                    bool isChecked = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

                    if (isCategoryHeader)
                    {
                        // Apply to all rows under the same category
                        ApplyToAllRowsUnderCategory(e.RowIndex, isChecked, dataGridView1.Columns[e.ColumnIndex].Name);
                    }
                    else
                    {
                        // Automatically check/uncheck the "Assign" column based on other checkbox columns
                        isUpdatingCells = true;
                        dataGridView1.Rows[e.RowIndex].Cells["Assign"].Value = IsAnyColumnChecked(e.RowIndex);
                        isUpdatingCells = false;
                    }
                }
            }
        }

        // Helper method to apply a value to all rows under the same category
        private void ApplyToAllRowsUnderCategory(int headerRowIndex, bool value, string columnName)
        {
            string category = GetCategoryFromFormName(dataGridView1.Rows[headerRowIndex].Cells["FormName"].Value?.ToString() ?? string.Empty);

            isUpdatingCells = true;
            for (int i = headerRowIndex + 1; i < dataGridView1.Rows.Count; i++)
            {
                var formNameCell = dataGridView1.Rows[i].Cells["FormName"].Value;
                if (formNameCell == null || formNameCell.ToString().StartsWith("****"))
                {
                    break; // Stop if we reach the next category header or the cell is null
                }

                if (category == "REPORTS" && columnName != "Assign" && columnName != "CanPrint")
                {
                    continue; // Skip columns other than Assign and CanPrint for REPORTS category
                }
                else if (category == "UTILITIES" && columnName != "Assign" && columnName != "CanModify")
                {
                    continue; // Skip columns other than Assign and CanModify for UTILITIES category
                }

                dataGridView1.Rows[i].Cells[columnName].Value = value;
                if (columnName == "Assign")
                {
                    // Update all checkbox columns in the same row based on the Assign column
                    for (int j = 2; j < dataGridView1.Columns.Count; j++) // Starting from index 2 to skip FormName and Assign columns
                    {
                        if (dataGridView1.Columns[j].GetType() == typeof(DataGridViewCheckBoxColumn))
                        {
                            if (category == "REPORTS" && j != dataGridView1.Columns["CanPrint"].Index)
                            {
                                continue;
                            }
                            if (category == "UTILITIES" && j != dataGridView1.Columns["CanModify"].Index)
                            {
                                continue;
                            }
                            dataGridView1.Rows[i].Cells[j].Value = value;
                        }
                    }
                }
            }
            isUpdatingCells = false;
        }

        // Helper method to get category from the form name
        private string GetCategoryFromFormName(string formName)
        {
            if (formName.Contains("MASTER"))
                return "MASTER";
            if (formName.Contains("TRANSACTION"))
                return "TRANSACTION";
            if (formName.Contains("REPORTS"))
                return "REPORTS";
            if (formName.Contains("UTILITIES"))
                return "UTILITIES";

            return "OTHER";
        }

        // Helper method to check if any checkbox column is checked
        private bool IsAnyColumnChecked(int rowIndex)
        {
            for (int i = 2; i < dataGridView1.Columns.Count; i++) // Starting from index 2 to skip FormName and Assign columns
            {
                if (dataGridView1.Columns[i].GetType() == typeof(DataGridViewCheckBoxColumn) && Convert.ToBoolean(dataGridView1.Rows[rowIndex].Cells[i].Value))
                {
                    return true;
                }
            }
            return false;
        }



        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].GetType() == typeof(DataGridViewCheckBoxColumn))
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void frmU_GrantScreenPermission_FormClosed(object sender, FormClosedEventArgs e)
        {
            DeTools.DestroyToolbar(this);
            MainForm.Instance.UPermissions.Enabled = true;

        }
    }
}
