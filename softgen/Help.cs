using Org.BouncyCastle.Crmf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace softgen
{
    public static class Help
    {
        public static string s_Mode = "";
        public static int i_Help_id;
        public static int i_NoOfRecords;
        public static Form o_form;
        public static Control o_control;
        public static DataGridViewCell o_cell;
        public static Dictionary<Control, string> controlToHelpTopicMapping = new Dictionary<Control, string>();
        public static Dictionary<Tuple<DataGridView, int, int>, string> dgvCellToHelpTopicMapping = new Dictionary<Tuple<DataGridView, int, int>, string>();
        // Declare a HashSet to store unique field names with aliases
        private static HashSet<string> fieldNamesWithAliases = new HashSet<string>();
        public static ComboBox combo;
        public static decimal qtysp;
        public static frmHelp frmHelp = new frmHelp();
        public static DbConnector dbConnector;
        public static int selectedIndex = frmHelp.cboDataType.SelectedIndex;
        public static ComboBox data_type_index_copy = new ComboBox();
        public static string send_combo_box_value;
        public static General general = new General();
        public static string getcbogrpval = "";


        /////////////////////////////////---start-----///////////////////////////////////

        public static void ClickHelp(Form form, Control control)
        {
            o_form = form;
            o_control = control;

            i_NoOfRecords = 0;

            s_Mode = DeTools.GetMode(form);



            if (controlToHelpTopicMapping.TryGetValue(control, out var helpTopic))
            {
                i_Help_id = Int32.Parse(helpTopic);

                switch (i_Help_id)
                {
                    case 0:
                        // Currently Help is not available
                        frmHelp.pnlText.Text = "Currently Help is not available.";
                        frmHelp.pnlText.Visible = true;
                        break;

                    case var _ when i_Help_id < 3000:
                        // In add mode, don't display the grid help.
                        if (s_Mode == DeTools.ADDMODE)
                        {
                            PrepareGridHelp();
                        }
                        else
                        {
                            PrepareGridHelp();
                        }
                        break;

                    case var _ when i_Help_id > 9000:
                        // These helps are for a substitute for combo help.
                        PrepareGridHelp();
                        break;

                    case var _ when i_Help_id > 3000:
                        PrepareTextHelp();
                        break;
                }
            }

            frmHelp.pnlToptxt.Text = DeTools.RestoreCaption(form) + " (" + i_NoOfRecords.ToString().Trim() + ")";
            frmHelp.pnlToptxt.Tag = DeTools.RestoreCaption(form);
            FillFields(frmHelp.cboFields, frmHelp.cboFieldsId);
            FillFields(frmHelp.cboOrder, frmHelp.cboOrderId);
            frmHelp.Show();
        }



        public static void DisplayHelp(Form form, int keyCode, Control control)
        {
            try
            {

                var helpTopic = "";
                if (keyCode == (int)Keys.F1)
                {
                    o_form = form;
                    o_control = control;

                    i_NoOfRecords = 0;

                    s_Mode = DeTools.GetMode(form);
                    //i_Help_id = control.HelpContextID;

                    if (controlToHelpTopicMapping.TryGetValue(control, out helpTopic) && !string.IsNullOrEmpty(helpTopic))
                    {
                        i_Help_id = Int32.Parse(helpTopic);


                        switch (i_Help_id)
                        {
                            case 0:
                                // Currently Help is not available
                                frmHelp.pnlText.Text = "Currently Help is not available.";
                                frmHelp.pnlText.Visible = true;
                                break;

                            case var _ when i_Help_id < 3000:
                                // In add mode, don't display the grid help.
                                if (s_Mode == DeTools.ADDMODE)
                                {
                                    PrepareGridHelp();
                                }
                                else
                                {
                                    PrepareGridHelp();
                                }
                                break;

                            case var _ when i_Help_id > 9000:
                                // These helps are for a substitute for combo help.
                                PrepareGridHelp();
                                break;

                            case var _ when i_Help_id > 3000:
                                PrepareTextHelp();
                                break;
                        }
                    }

                    else
                    {
                        Tuple<DataGridView, int, int> key = Tuple.Create(DeTools.dgv, DeTools.dgvCell.RowIndex, DeTools.dgvCell.ColumnIndex);
                        dgvCellToHelpTopicMapping.TryGetValue(key, out helpTopic);
                        i_Help_id = Int32.Parse(helpTopic);


                        switch (i_Help_id)
                        {
                            case 0:
                                // Currently Help is not available
                                frmHelp.pnlText.Text = "Currently Help is not available.";
                                frmHelp.pnlText.Visible = true;
                                break;

                            case var _ when i_Help_id < 3000:
                                // In add mode, don't display the grid help.
                                if (s_Mode == DeTools.ADDMODE)
                                {
                                    PrepareGridHelp();
                                }
                                else
                                {
                                    PrepareGridHelp();
                                }
                                break;

                            case var _ when i_Help_id > 9000:
                                // These helps are for a substitute for combo help.
                                PrepareGridHelp();
                                break;

                            case var _ when i_Help_id > 3000:
                                PrepareTextHelp();
                                break;
                        }
                    }


                    frmHelp.pnlToptxt.Text = DeTools.RestoreCaption(form) + " (" + i_NoOfRecords.ToString().Trim() + ")";
                    frmHelp.pnlToptxt.Tag = DeTools.RestoreCaption(form);
                    FillFields(frmHelp.cboFields, frmHelp.cboFieldsId);
                    FillFields(frmHelp.cboOrder, frmHelp.cboOrderId);
                    frmHelp.Show();

                    switch (i_Help_id)
                    {
                        case 9001:
                            frmHelp.cboDataType.Items.Add(1);
                            frmHelp.cboFields.Items.Add("Item Description");
                            break;
                        case 9002:
                            frmHelp.cboDataType.Items.Add(1);
                            frmHelp.cboFields.Items.Add("Customer Name");
                            break;
                        case 9005:
                            frmHelp.cboDataType.Items.Add(1);
                            frmHelp.cboFields.Items.Add("Supplier Name");
                            break;
                        case 9010:
                            frmHelp.cboDataType.Items.Add(1);
                            frmHelp.cboFields.Items.Add("Item Description");
                            break;
                        case 9011:
                            frmHelp.cboDataType.Items.Add(1);
                            frmHelp.cboFields.Items.Add("Item Description");
                            break;
                        case 1005:
                            frmHelp.cboDataType.Items.Add(1);
                            frmHelp.cboFields.Items.Add("Customer Name");
                            break;
                        case 1006:
                            frmHelp.cboDataType.Items.Add(1);
                            frmHelp.cboFields.Items.Add("Item Name");
                            break;
                        case 1013:
                            frmHelp.cboDataType.Items.Add(1);
                            frmHelp.cboFields.Items.Add("Supplier Name");
                            break;
                        case 9014:
                            frmHelp.cboDataType.Items.Add(1);
                            frmHelp.cboFields.Items.Add("Description");
                            break;
                        case 9018:
                            frmHelp.cboDataType.Items.Add(1);
                            frmHelp.cboFields.Items.Add("Description");
                            break;
                        case 9006:
                            frmHelp.cboDataType.Items.Add(1);
                            frmHelp.cboFields.Items.Add("Description");
                            break;
                    }
                }
            }
            catch (Exception)
            {

                
            }

        }

        public static void FieldHelp(string strReqdFor, Button objControl)
        {
            //--todo-           Messages.HelpMsg(objControl.Tooltiptext);

            if (strReqdFor == "S" || strReqdFor == "A")
            {
                // Display shadow only if required for saving or authorization.
                objControl.FlatStyle = FlatStyle.Flat;
                objControl.FlatAppearance.BorderSize = 1;
                objControl.FlatAppearance.BorderColor = Color.DarkGray;
            }
        }


        public static void FillFields(ComboBox cboField, ComboBox cboFieldId)
        {
            int i_Cols = 0;
            string strFieldName, strQuery = null;
            string[] strField;

            string strFormType = o_form.Name.Trim().Substring(3, 1).ToUpper();

            dbConnector = new DbConnector();
            dbConnector.OpenConnection();

            DeTools.gstrSQL = "Select Query, No_of_columns, Caption1, Caption2, Caption3, Caption4, Caption5, " +
               "Caption6, Caption7, Caption8, Caption9, Caption10 " +
               "from Help where Help_id = " + i_Help_id;

            using (OdbcDataReader reader = dbConnector.CreateResultset(DeTools.gstrSQL))
            {
                if (reader.HasRows)
                {
                    cboField.Items.Clear();
                    cboFieldId.Items.Clear();

                    i_Cols = (int)reader["No_of_columns"];
                    strQuery = reader["Query"].ToString();

                    for (int J = 0; J < i_Cols; J++)
                    {
                        strFieldName = "Caption" + (J + 1).ToString().Trim();
                        cboField.Items.Add(reader[strFieldName].ToString().Trim());
                    }
                    cboField.SelectedIndex = -1;
                }
                else
                {
                    cboField.Items.Clear();
                    cboFieldId.Items.Clear();
                    cboField.SelectedIndex = 1;
                }
            }
            frmHelp.cboDataType.Items.Clear();
            frmHelp.cboDataType.SelectedIndex = 1;

            using (OdbcDataReader rs_Query = dbConnector.CreateResultset(strQuery + " limit 200"))
            {
                for (int J = 0; J < i_Cols; J++)
                {
                    frmHelp.cboDataType.Items.Add(rs_Query.GetFieldType(J).Name);
                    //---copying data------
                    data_type_index_copy.Items.Add(rs_Query.GetFieldType(J).Name);
                }
            }

            strQuery = strQuery.Trim();
            int lintLen = strQuery.Length;
            strQuery = strQuery.Substring(6).Trim();
            lintLen = strQuery.Length;
            if (strQuery.IndexOf("DISTINCT", StringComparison.OrdinalIgnoreCase) != -1)
            {
                strQuery = strQuery.Substring(8).Trim();
            }

            strField = strQuery.Split(',');

            for (int I = 0; I < i_Cols; I++)
            {
                strFieldName = strField[I].Trim();

                // Check if the field name contains a dot (.)
                if (strFieldName.Contains("."))
                {
                    // Split the field name based on the dot (.)
                    string[] parts = strFieldName.Split('.');

                    // If the split results in two parts, consider the second part as the actual field name
                    if (parts.Length == 2)
                    {
                        strFieldName = parts[0] + "." + parts[1].Trim();
                    }
                }
                fieldNamesWithAliases.Add(strFieldName);

                if (strFieldName.Contains("="))
                {
                    string[] strCase = strFieldName.Split('=');
                    strFieldName = strCase[0].Trim();
                }

                if (I != i_Cols - 1)
                {
                    cboFieldId.Items.Add(strFieldName);
                }
                if (I == i_Cols - 1)
                {
                    strField = strFieldName.Split(' ');
                    cboFieldId.Items.Add(strField[0].Trim());
                }
            }
        }


        public static void PrepareGridHelp()
        {
            int i, j, i_Rows = 0, Count;
            string strFieldName, strFormType, strOrderBy;
            string strFields;
            string strCondition;
            object strValue = null;
            string strheading = "";
            string strMode = new string(' ', 1);
            ComboBox cmb_for_query_cols = new ComboBox();



            strFormType = o_form.Name.Trim().Substring(3, 1).ToUpper();
            frmHelp.pnlText.Visible = false;

            dbConnector = new DbConnector();
            dbConnector.OpenConnection();

            // Select the query from the Help table
            DeTools.gstrSQL = "Select Query , A_Mode_Cond, M_Mode_Cond, D_Mode_Cond, I_Mode_Cond, " +
                "P_Mode_Cond, Order_by, No_of_columns, Caption1, Caption2, " +
                "Caption3, Caption4, Caption5, Caption6, Caption7, " +
                "Caption8, Caption9, Caption10 " +
                "from Help where Help_id = " + "'" + i_Help_id + "';";
            //rs_Query = CreateResultset(gstrSQL);
            using (OdbcDataReader rs_Query = dbConnector.CreateResultset(DeTools.gstrSQL))
            {
                if (!rs_Query.HasRows)
                {
                    frmHelp.pnlText.Visible = true;
                    frmHelp.pnlText.Text = "Record not available.";
                    return;
                }

                DeTools.gstrSQL = (rs_Query["Query"] + "  ");
                if (string.IsNullOrEmpty(DeTools.gstrSQL))
                {
                    frmHelp.pnlText.Text = "Query not present in Help table.";
                    frmHelp.pnlInstructiontxt.Text = "Press Escape to exit from help.";
                    frmHelp.pnlText.Visible = true;
                    return;
                }

                switch (s_Mode)
                {
                    case DeTools.ADDMODE:
                        //-------this is for getting data of the particular group id for sub group . I have modified in the database added additon ?
                        if (getcbogrpval != string.Empty)
                        {
                            string originalQuery = rs_Query["A_Mode_Cond"].ToString();
                            string modifiedQuery = originalQuery.Replace("?", "'" + getcbogrpval + "'");
                            DeTools.gstrSQL += (modifiedQuery + " ").Trim();

                        }

                        else
                        {
                            DeTools.gstrSQL += (rs_Query["A_Mode_Cond"] + " ").Trim();
                        }
                        break;

                    case DeTools.MODIFYMODE:
                        if (getcbogrpval != string.Empty)
                        {  //-------this is for getting data of the particular group id  . I have modified in the database added additon ?
                            string originalQuery = rs_Query["M_Mode_Cond"].ToString();
                            string modifiedQuery = originalQuery.Replace("?", "'" + getcbogrpval + "'");
                            DeTools.gstrSQL += (modifiedQuery + " ").Trim();

                        }
                        else
                        {
                            DeTools.gstrSQL += (rs_Query["M_Mode_Cond"] + " ");

                        }
                        break;

                    case DeTools.DELETEMODE:
                        DeTools.gstrSQL += (" " + (rs_Query["D_Mode_Cond"] + " ").Trim());
                        break;

                    case DeTools.INQUIREMODE:
                        DeTools.gstrSQL += (" " + (rs_Query["I_Mode_Cond"] + " ").Trim());
                        break;

                    case DeTools.POSTMODE:
                        DeTools.gstrSQL += (" " + (rs_Query["P_Mode_Cond"] + " ").Trim());
                        break;

                    default:
                        DeTools.gstrSQL += string.Empty;
                        break;

                }


                //-----------------------
                //string innerquery_for_cols = DeTools.gstrSQL.ToString().Trim()+ " limit 1";
                //using (OdbcDataReader innerQueryReader = dbConnector.CreateResultset(innerquery_for_cols))
                //{
                //    int i_Cols = innerQueryReader.FieldCount;

                //    for (int J = 0; J < i_Cols; J++)
                //    {
                //        string columnName = innerQueryReader.GetName(J);
                //        cmb_for_query_cols.Items.Add(columnName);
                //    }
                //}
                foreach (string columnName in fieldNamesWithAliases)
                {

                    int firstSpaceIndex = columnName.IndexOf(' ');

                    string fieldName = (firstSpaceIndex != -1) ? columnName.Substring(0, firstSpaceIndex) : columnName;



                    cmb_for_query_cols.Items.Add(fieldName);
                }

                if (frmHelp.cboFields.SelectedItem == null)
                {
                    strFields = string.Empty;  // Assign the selected item to strFields
                }
                else
                {
                    string selectedField = frmHelp.cboFields.SelectedItem.ToString().Trim();
                    string col_name = cmb_for_query_cols.Items[selectedIndex].ToString().Trim();
                    strFields = col_name;  // Assign the selected item to strField
                }

                if (frmHelp.cboRel.SelectedItem == null)
                {
                    strCondition = string.Empty;
                }
                else
                {
                    string selectedField = frmHelp.cboRel.SelectedItem.ToString().Trim();
                    // Assign the selected item
                    strCondition = selectedField;
                }

                if (string.IsNullOrEmpty(frmHelp.txtValue.Text.Trim()))
                {
                    strValue = string.Empty;
                }
                else
                {
                    if (selectedIndex >= 0)
                    {
                        string selectedItemValue = frmHelp.cboDataType.SelectedItem.ToString().Trim();
                        string copyComboBoxItemValue = data_type_index_copy.Items[selectedIndex].ToString().Trim();
                        send_combo_box_value = copyComboBoxItemValue.ToString();


                        if (frmHelp.cboDataType.SelectedItem.ToString().Trim() == copyComboBoxItemValue)
                        {
                            if (frmHelp.cboRel.SelectedItem.ToString().Trim() == "LIKE")
                            {
                                strValue = $"'%{frmHelp.txtValue.Text.Trim()}%'";
                            }
                            else if (frmHelp.cboRel.SelectedItem.ToString().Trim() == "=")
                            {
                                strValue = $"'{frmHelp.txtValue.Text.Trim()}'";
                            }
                        }
                        else if (frmHelp.cboDataType.SelectedItem.ToString().Trim() == copyComboBoxItemValue)
                        {
                            strValue = double.Parse(frmHelp.txtValue.Text.Trim());
                        }
                        else if (frmHelp.cboDataType.SelectedItem.ToString().Trim() == copyComboBoxItemValue)
                        {
                            if (!DateTime.TryParse(frmHelp.txtValue.Text.Trim(), out DateTime dateValue))
                            {
                                strValue = $"'{frmHelp.txtValue.Text.Trim().Substring(0, 4)}'";
                            }
                            else
                            {
                                strValue = $"'{frmHelp.txtValue.Text.Trim()}'";
                            }
                        }
                    }
                }

                if (frmHelp.cboOrder.SelectedItem == null)
                {
                    strOrderBy = (rs_Query["order_by"] + " ").Trim();
                }
                else
                {
                    strOrderBy = "ORDER BY " + frmHelp.cboOrderId.SelectedItem;
                }

                if (string.IsNullOrEmpty(strFields))
                {
                    strCondition = string.Empty;
                    strValue = string.Empty;
                }
                else if (string.IsNullOrEmpty(strCondition))
                {
                    strFields = string.Empty;
                    strValue = string.Empty;
                }
                else if (string.IsNullOrEmpty(strValue.ToString()))
                {
                    strFields = string.Empty;
                    strCondition = string.Empty;
                }

                if (!string.IsNullOrEmpty(frmHelp.txtValue.Text.Trim()) && frmHelp.cboDataType.SelectedItem == "11")
                {
                    if (!DateTime.TryParse(frmHelp.txtValue.Text.Trim(), out DateTime dateValue))
                    {
                        strCondition = "=";
                        strValue = "'01-JAN-1900'   ";
                    }
                }

                if (s_Mode == DeTools.INQUIREMODE)
                {
                    if (string.IsNullOrEmpty(rs_Query["I_Mode_Cond"].ToString()))
                    {
                        if (!string.IsNullOrEmpty(strFields))
                        {
                            strFields = " WHERE " + strFields;
                        }
                        DeTools.gstrSQL = DeTools.gstrSQL + strFields + " " + strCondition + " " + strValue + " " + strOrderBy;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(strFields))
                        {
                            strFields = " WHERE " + strFields;
                        }
                        DeTools.gstrSQL = DeTools.gstrSQL;
                    }
                }
                if (s_Mode == DeTools.MODIFYMODE)
                {
                    if (!string.IsNullOrEmpty(strFields))
                    {
                        strFields = " AND " + strFields;
                    }
                    DeTools.gstrSQL = DeTools.gstrSQL + strFields + " " + strCondition + " " + strValue + " " + strOrderBy;
                }

                if (s_Mode == DeTools.ADDMODE)
                {
                    if (!string.IsNullOrEmpty(strFields))
                    {
                        strFields = " AND " + strFields;
                    }
                    DeTools.gstrSQL = DeTools.gstrSQL + strFields + " " + strCondition + " " + strValue + " " + strOrderBy;
                }

                if (s_Mode == DeTools.POSTMODE)
                {
                    if (!string.IsNullOrEmpty(strFields))
                    {
                        strFields = " AND " + strFields;
                    }
                    DeTools.gstrSQL = DeTools.gstrSQL + strFields + " " + strCondition + " " + strValue + " " + strOrderBy;
                }

                if (s_Mode == DeTools.DELETEMODE)
                {
                    if (!string.IsNullOrEmpty(strFields))
                    {
                        strFields = " AND " + strFields;
                    }
                    DeTools.gstrSQL = DeTools.gstrSQL + strFields + " " + strCondition + " " + strValue + " " + strOrderBy;
                }

                if (string.IsNullOrEmpty(s_Mode))
                {
                    if (!string.IsNullOrEmpty(strFields))
                    {
                        strFields = " WHERE " + strFields;
                    }
                    DeTools.gstrSQL = DeTools.gstrSQL + strFields + " " + strCondition + " " + strValue + " " + strOrderBy;
                }

                // Adjust the SQL query to limit the number of records
                DeTools.gstrSQL = DeTools.gstrSQL + " Limit 200";

                // Execute the query and retrieve results
                using (OdbcDataReader rs_Result = dbConnector.CreateResultset(DeTools.gstrSQL))
                {
                    List<List<object>> data = new List<List<object>>();

                    if (rs_Result.HasRows)
                    {
                        while (rs_Result.Read())
                        {
                            List<object> row = new List<object>();
                            for (int jj = 0; jj < rs_Result.FieldCount; jj++)
                            {
                                object columnValue = rs_Result.GetValue(jj);
                                row.Add(columnValue);
                            }
                            data.Add(row);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows in the result set.");
                    }

                    if (!rs_Result.HasRows)
                    {
                        // Handle the case when there are no rows
                        if (s_Mode == DeTools.MODIFYMODE || s_Mode == DeTools.DELETEMODE || s_Mode == DeTools.POSTMODE)
                        {
                            if (i_Help_id == 1101)
                            {
                                frmHelp.pnlText.Text = "No authorized record available in Employee Master.";
                            }
                            else
                            {
                                frmHelp.pnlText.Text = "No unauthorized record available.";
                            }
                        }
                        else
                        {
                            frmHelp.pnlText.Text = "No record available.";
                        }

                        for (Count = 0; Count < frmHelp.pnlToptxt.Text.Length; Count++)
                        {
                            if (frmHelp.pnlToptxt.Text[Count] == '(')
                            {
                                strheading = frmHelp.pnlToptxt.Text.Substring(0, Count);
                                break;
                            }
                        }

                        frmHelp.pnlToptxt.Text = strheading + " (0)";
                        frmHelp.pnlInstructiontxt.Text = "Press Escape to exit from help.";
                        frmHelp.pnlText.Visible = true;
                    }
                    else
                    {
                        // Clear any existing columns and rows in the DataGridView
                        frmHelp.grdHelp.Columns.Clear();
                        frmHelp.grdHelp.Rows.Clear();

                        // Add column headers based on the Help table columns
                        for (j = 0; j < rs_Result.FieldCount; j++)
                        {
                            string columnName = rs_Result.GetName(j);
                            frmHelp.grdHelp.Columns.Add(columnName, columnName);

                            if (columnName == "cess_perc1" || columnName == "disc_per1" || columnName == "tax_per1")
                            {
                                frmHelp.grdHelp.Columns[columnName].Visible = false;

                            }

                        }

                        // Initialize the DataGridView's properties
                        frmHelp.grdHelp.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                        frmHelp.grdHelp.ColumnHeadersHeight = 450;
                        frmHelp.grdHelp.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;


                        //for 2nd column width
                        int columnIndex = 1; // Set to 1 for the 2nd column
                        int newWidth = 250; // Set the desired width

                        if (columnIndex >= 0 && columnIndex < frmHelp.grdHelp.Columns.Count)
                        {
                            frmHelp.grdHelp.Columns[columnIndex].Width = newWidth;
                        }

                        //for 1st column width
                        int columnIndex1 = 0; // Set to 1 for the 2nd column
                        int newWidth1 = 250; // Set the desired width

                        if (columnIndex1 >= 0 && columnIndex1 < frmHelp.grdHelp.Columns.Count)
                        {
                            frmHelp.grdHelp.Columns[columnIndex1].Width = newWidth1;
                        }


                        // Iterate through the columns and make them read-only
                        foreach (DataGridViewColumn column in frmHelp.grdHelp.Columns)
                        {
                            column.ReadOnly = true;
                        }



                        frmHelp.pgbStatus.Minimum = 0;
                        frmHelp.pgbStatus.Maximum = data.Count - 1; // Use the count of rows in the data list                       
                        frmHelp.Show();

                        if (data.Count > 0)
                        {
                            frmHelp.pnlToptxt.Text = strheading + " (0)";

                        }

                        // Loop through the rows and populate the DataGridView from the data list
                        for (i = 0; i < data.Count; i++)
                        {
                            frmHelp.pgbStatus.Value = i;
                            if (i % 50 == 0)
                            {
                                frmHelp.pnlToptxt.Text = strheading + " (" + i + ")";
                            }

                            frmHelp.grdHelp.Rows.Add();
                            //for (j = 0; j < rs_Result.FieldCount; j++)
                            //{
                            //    object cellValue = data[i - 1][j]; // Retrieve data from the List<List<object>>
                            //    frmHelp.grdHelp.Rows[i - 1].Cells[j].Value = cellValue;
                            //}   old

                            for (j = 0; j < rs_Result.FieldCount; j++)
                            {
                                if (i < data.Count && j < data[i].Count)
                                {
                                    object cellValue = data[i][j]; // Retrieve data from the List<List<object>>
                                    frmHelp.grdHelp.Rows[i].Cells[j].Value = cellValue;

                                }
                            }
                        }

                        frmHelp.pnlToptxt.Invalidate();
                        frmHelp.pnlToptxt.Text = strheading + " (" + data.Count + ")";
                        frmHelp.pnlToptxt.Update();
                        frmHelp.grdHelp.CurrentCell = frmHelp.grdHelp[0, 0];
                        frmHelp.grdHelp.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                        frmHelp.grdHelp.Visible = true;


                        // Enable filter menus based on the number of rows
                        if (frmHelp.grdHelp.RowCount > 101)
                        {
                            //frmHelp.mnuLast_100.Enabled = true;
                            //frmHelp.mnuLast_50.Enabled = true;
                            //frmHelp.mnuLast_15.Enabled = true;
                        }
                        else if (frmHelp.grdHelp.RowCount > 51)
                        {
                            //frmHelp.mnuLast_50.Enabled = true;
                            //frmHelp.mnuLast_15.Enabled = true;
                        }
                        else if (frmHelp.grdHelp.RowCount > 16)
                        {
                            //  frmHelp.mnuLast_15.Enabled = true;
                        }

                        frmHelp.pnlInstructiontxt.Text = "Double click or press Enter to select.";
                    }
                }
            }
        }
        //---------------newest old
        //public static void PrepareGridHelp()
        //{
        //    int i, j, i_Rows, i_Cols, Count;
        //    string strFieldName, strFormType, strOrderBy;
        //    string strFields;
        //    string strCondition;
        //    object strValue = null;
        //    string strheading="";
        //    string strMode = new string(' ', 1);


        //    strFormType = o_form.Name.Trim().Substring(3, 1).ToUpper();
        //    frmHelp.pnlText.Visible = false;

        //    dbConnector = new DbConnector();
        //    dbConnector.OpenConnection();

        //    // Select the query from the Help table
        //    DeTools.gstrSQL = "Select Query , A_Mode_Cond, M_Mode_Cond, D_Mode_Cond, I_Mode_Cond, " +
        //        "P_Mode_Cond, Order_by, No_of_columns, Caption1, Caption2, " +
        //        "Caption3, Caption4, Caption5, Caption6, Caption7, " +
        //        "Caption8, Caption9, Caption10 " +
        //        "from Help where Help_id = " + "'"+i_Help_id+"';";
        //    //rs_Query = CreateResultset(gstrSQL);
        //    using (OdbcDataReader rs_Query = dbConnector.CreateResultset(DeTools.gstrSQL))
        //    {
        //        if (!rs_Query.HasRows)
        //        {
        //            frmHelp.pnlText.Visible = true;
        //            frmHelp.pnlText.Text = "Record not available.";
        //            return;
        //        }

        //        DeTools.gstrSQL = (rs_Query["Query"] + "  ");
        //        if (string.IsNullOrEmpty(DeTools.gstrSQL))
        //        {
        //            frmHelp.pnlText.Text = "Query not present in Help table.";
        //            frmHelp.pnlInstructiontxt.Text = "Press Escape to exit from help.";
        //            frmHelp.pnlText.Visible = true;
        //            return;
        //        }

        //        switch (s_Mode)
        //        {
        //            case DeTools.ADDMODE:
        //                DeTools.gstrSQL += (rs_Query["A_Mode_Cond"] + " ").Trim();
        //                break;

        //            case DeTools.MODIFYMODE:
        //                DeTools.gstrSQL += (rs_Query["M_Mode_Cond"] + " ");
        //                break;

        //            case DeTools.DELETEMODE:
        //                DeTools.gstrSQL += (" " + (rs_Query["D_Mode_Cond"] + " ").Trim());
        //                break;

        //            case DeTools.INQUIREMODE:
        //                DeTools.gstrSQL += (" " + (rs_Query["I_Mode_Cond"] + " ").Trim());
        //                break;

        //            case DeTools.POSTMODE:
        //                DeTools.gstrSQL += (" " + (rs_Query["P_Mode_Cond"] + " ").Trim());
        //                break;

        //            default:
        //                DeTools.gstrSQL += string.Empty;
        //                break;
        //        }

        //        if (frmHelp.cboFields.SelectedItem == null)
        //        {
        //            strFields = string.Empty;  // Assign the selected item to strFields
        //        }
        //        else
        //        {
        //            string selectedField = frmHelp.cboFields.SelectedItem.ToString().Trim();
        //            strFields = selectedField;  // Assign the selected item to strField
        //        }

        //        if (frmHelp.cboRel.SelectedItem == null)
        //        {
        //            strCondition = string.Empty;
        //        }
        //        else
        //        {
        //            string selectedField = frmHelp.cboRel.SelectedItem.ToString().Trim();
        //            // Assign the selected item
        //            strCondition = selectedField;
        //        }

        //        if (string.IsNullOrEmpty(frmHelp.txtValue.Text.Trim()))
        //        {
        //            strValue = string.Empty;
        //        }
        //        else
        //        {
        //            if (frmHelp.cboDataType.SelectedItem.ToString().Trim() == "1")
        //            {
        //                if (frmHelp.cboRel.SelectedItem.ToString().Trim() == "LIKE")
        //                {
        //                    strValue = $"'%{frmHelp.txtValue.Text.Trim()}%'";
        //                }
        //                else if (frmHelp.cboRel.SelectedItem.ToString().Trim() == "=")
        //                {
        //                    strValue = $"'{frmHelp.txtValue.Text.Trim()}'";
        //                }
        //            }
        //            else if (frmHelp.cboDataType.SelectedItem.ToString().Trim() == "2")
        //            {
        //                strValue = double.Parse(frmHelp.txtValue.Text.Trim());
        //            }
        //            else if (frmHelp.cboDataType.SelectedItem.ToString().Trim() == "11")
        //            {
        //                if (!DateTime.TryParse(frmHelp.txtValue.Text.Trim(), out DateTime dateValue))
        //                {
        //                    strValue = $"'{frmHelp.txtValue.Text.Trim().Substring(0, 4)}'";
        //                }
        //                else
        //                {
        //                    strValue = $"'{frmHelp.txtValue.Text.Trim()}'";
        //                }
        //            }
        //        }

        //        if (frmHelp.cboOrder.SelectedItem == null)
        //        {
        //            strOrderBy = (rs_Query["order_by"] + " ").Trim();
        //        }
        //        else
        //        {
        //            strOrderBy = "ORDER BY " + frmHelp.cboOrderId.SelectedItem;
        //        }

        //        if (string.IsNullOrEmpty(strFields))
        //        {
        //            strCondition = string.Empty;
        //            strValue = string.Empty;
        //        }
        //        else if (string.IsNullOrEmpty(strCondition))
        //        {
        //            strFields = string.Empty;
        //            strValue = string.Empty;
        //        }
        //        else if (string.IsNullOrEmpty(strValue.ToString()))
        //        {
        //            strFields = string.Empty;
        //            strCondition = string.Empty;
        //        }

        //        if (!string.IsNullOrEmpty(frmHelp.txtValue.Text.Trim()) && frmHelp.cboDataType.SelectedItem == "11")
        //        {
        //            if (!DateTime.TryParse(frmHelp.txtValue.Text.Trim(), out DateTime dateValue))
        //            {
        //                strCondition = "=";
        //                strValue = "'01-JAN-1900'   ";
        //            }
        //        }

        //        if (s_Mode == DeTools.INQUIREMODE)
        //        {
        //            if (string.IsNullOrEmpty(rs_Query["I_Mode_Cond"].ToString()))
        //            {
        //                if (!string.IsNullOrEmpty(strFields))
        //                {
        //                    strFields = " WHERE " + strFields;
        //                }
        //                DeTools.gstrSQL = DeTools.gstrSQL + strFields + " " + strCondition + " " + strValue + " " + strOrderBy;
        //            }
        //            else
        //            {
        //                if (!string.IsNullOrEmpty(strFields))
        //                {
        //                    strFields = " WHERE " + strFields;
        //                }
        //                DeTools.gstrSQL = DeTools.gstrSQL;
        //            }
        //        }
        //        if (s_Mode == DeTools.MODIFYMODE)
        //        {
        //            if (!string.IsNullOrEmpty(strFields))
        //            {
        //                strFields = " AND " + strFields;
        //            }
        //            DeTools.gstrSQL = DeTools.gstrSQL + strFields + " " + strCondition + " " + strValue + " " + strOrderBy;
        //        }

        //        if (s_Mode == DeTools.ADDMODE)
        //        {
        //            if (!string.IsNullOrEmpty(strFields))
        //            {
        //                strFields = " AND " + strFields;
        //            }
        //            DeTools.gstrSQL = DeTools.gstrSQL + strFields + " " + strCondition + " " + strValue + " " + strOrderBy;
        //        }

        //        if (s_Mode == DeTools.POSTMODE)
        //        {
        //            if (!string.IsNullOrEmpty(strFields))
        //            {
        //                strFields = " AND " + strFields;
        //            }
        //            DeTools.gstrSQL = DeTools.gstrSQL + strFields + " " + strCondition + " " + strValue + " " + strOrderBy;
        //        }

        //        if (s_Mode == DeTools.DELETEMODE)
        //        {
        //            if (!string.IsNullOrEmpty(strFields))
        //            {
        //                strFields = " AND " + strFields;
        //            }
        //            DeTools.gstrSQL = DeTools.gstrSQL + strFields + " " + strCondition + " " + strValue + " " + strOrderBy;
        //        }

        //        if (string.IsNullOrEmpty(s_Mode))
        //        {
        //            if (!string.IsNullOrEmpty(strFields))
        //            {
        //                strFields = " WHERE " + strFields;
        //            }
        //            DeTools.gstrSQL = DeTools.gstrSQL + strFields + " " + strCondition + " " + strValue + " " + strOrderBy;
        //        }

        //        // Adjust the SQL query to limit the number of records
        //        DeTools.gstrSQL = DeTools.gstrSQL+" Limit 2000";

        //        // Execute the query and retrieve results
        //        using (OdbcDataReader rs_Result = dbConnector.CreateResultset(DeTools.gstrSQL))
        //        {
        //            List<List<object>> data = new List<List<object>>();

        //            if (rs_Result.HasRows)
        //            {
        //                int ii = 0;
        //                while (rs_Result.Read())
        //                {
        //                    Console.WriteLine($"Row {ii} data:");
        //                    for (int jj = 0; jj < rs_Result.FieldCount; jj++)
        //                    {
        //                        string columnName = rs_Result.GetName(jj);
        //                        object columnValue = rs_Result.GetValue(jj);
        //                        Console.WriteLine($"Column {columnName}: {columnValue}");
        //                    }
        //                    ii++;
        //                }
        //            }
        //            else
        //            {
        //                Console.WriteLine("No rows in the result set.");
        //            }

        //            if (!rs_Result.HasRows)
        //            {

        //                if (s_Mode == DeTools.MODIFYMODE || s_Mode == DeTools.DELETEMODE || s_Mode == DeTools.POSTMODE)
        //                {
        //                    if (i_Help_id == 1101)
        //                    {
        //                        frmHelp.pnlText.Text = "No authorized record available in Employee Master.";
        //                    }
        //                    else
        //                    {
        //                        frmHelp.pnlText.Text = "No unauthorized record available.";
        //                    }
        //                }
        //                else
        //                {
        //                    frmHelp.pnlText.Text = "No record available.";
        //                }

        //                for (Count = 0; Count < frmHelp.pnlToptxt.Text.Length; Count++)
        //                {
        //                    if (frmHelp.pnlToptxt.Text[Count] == '(')
        //                    {
        //                        strheading = frmHelp.pnlToptxt.Text.Substring(0, Count);
        //                        break;
        //                    }
        //                }

        //                frmHelp.pnlToptxt.Text = strheading + " (0)";
        //                frmHelp.pnlInstructiontxt.Text = "Press Escape to exit from help.";
        //                frmHelp.pnlText.Visible = true;
        //            }
        //            else
        //            {
        //                // Get the number of rows (subtract one for column headings)
        //                i_Rows = 1;
        //                while (rs_Result.Read())
        //                {
        //                    i_Rows++;
        //                }
        //                rs_Result.Close();

        //                i_NoOfRecords = i_Rows - 1;

        //                // Get the number of columns from Help table
        //                i_Cols = rs_Query.GetInt32("No_of_columns");

        //                frmHelp.grdHelp.Columns.Clear(); // Clear any existing columns
        //                for (int c = 0; c < i_Cols; c++)
        //                {
        //                    DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
        //                    frmHelp.grdHelp.Columns.Add(column);
        //                }

        //                for (Count = 0; Count < frmHelp.pnlToptxt.Text.Length; Count++)
        //                {
        //                    if (frmHelp.pnlToptxt.Text[Count] == '(')
        //                    {
        //                        strheading = frmHelp.pnlToptxt.Text.Substring(0, Count);
        //                        break;
        //                    }
        //                }



        //                frmHelp.grdHelp.Rows.Clear(); // Clear any existing rows
        //                for (int r = 0; r < i_Rows; r++)
        //                {
        //                    frmHelp.grdHelp.Rows.Add(); // Add a new row
        //                }



        //                //// Prepare the header row
        //                //frmHelp.grdHelp.Row = 0;
        //                //frmHelp.grdHelp.RowHeight[0] = 450;
        //                //for (j = 0; j < i_Cols; j++)
        //                //{
        //                //    frmHelp.grdHelp.Col = j;
        //                //    strFieldName = "Caption" + (j + 1);
        //                //    frmHelp.grdHelp.Text = rs_Query[strFieldName].ToString();
        //                //    frmHelp.grdHelp.ColWidth[j] = 1200;
        //                //    frmHelp.grdHelp.ColWidth[j] = rs_Result.GetFieldValue(j).ToString().Length * 90;
        //                //}

        //                // Prepare the header row
        //                for (j = 0; j < i_Cols; j++)
        //                {
        //                    // Create a new DataGridViewTextBoxColumn
        //                    DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
        //                    column.HeaderText = "Caption" + (j + 1);
        //                    column.Width = 120; // Set your desired width here

        //                    // Set the column as read-only
        //                    column.ReadOnly = true;

        //                    // Add the DataGridViewTextBoxColumn to the DataGridView
        //                   // frmHelp.grdHelp.Columns.Add(column);
        //                }

        //                // After adding columns, you can set the header row's height
        //                frmHelp.grdHelp.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        //                frmHelp.grdHelp.ColumnHeadersHeight = 450;
        //                frmHelp.grdHelp.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;


        //                frmHelp.pgbStatus.Minimum = 0;
        //                frmHelp.pgbStatus.Maximum = i_Rows - 1;
        //                frmHelp.Show();

        //                frmHelp.pnlToptxt.Text = strheading + " (0)";

        //                for (i = 1; i < i_Rows; i++)
        //                {
        //                    frmHelp.pgbStatus.Value = i;
        //                    if (i % 50 == 0)
        //                    {
        //                        frmHelp.pnlToptxt.Text = strheading + " (" + i + ")";
        //                    }
        //                    // Add a new row to the DataGridView and populate its cells
        //                    frmHelp.grdHelp.Rows.Add();
        //                    for (j = 0; j < i_Cols; j++)
        //                    {
        //                        // frmHelp.grdHelp.Rows[i - 1].Cells[j].Value = rs_Result.GetFieldValue(j).ToString();
        //                        frmHelp.grdHelp.Rows[i - 1].Cells[j].Value = rs_Result.GetFieldValue<string>(j);
        //                    }
        //                }

        //                frmHelp.pnlToptxt.Text = strheading + " (" + i_NoOfRecords + ")";
        //                //frmHelp.grdHelp.Col = 0;
        //                //frmHelp.grdHelp.Row = 1;
        //                frmHelp.grdHelp.CurrentCell = frmHelp.grdHelp[0, 1];
        //                frmHelp.grdHelp.Visible = true;

        //                // Enable filter menus based on the number of rows
        //                if (frmHelp.grdHelp.RowCount > 101)
        //                {
        //                    //frmHelp.mnuLast_100.Enabled = true;
        //                    //frmHelp.mnuLast_50.Enabled = true;
        //                    //frmHelp.mnuLast_15.Enabled = true;
        //                }
        //                else if (frmHelp.grdHelp.RowCount > 51)
        //                {
        //                    //frmHelp.mnuLast_50.Enabled = true;
        //                    //frmHelp.mnuLast_15.Enabled = true;
        //                }
        //                else if (frmHelp.grdHelp.RowCount > 16)
        //                {
        //                  //  frmHelp.mnuLast_15.Enabled = true;
        //                }

        //                frmHelp.pnlInstructiontxt.Text = "Double click or press Enter to select.";
        //            }
        //        }
        //    }
        //}

        public static void PrepareTextHelp()
        {
            dbConnector = new DbConnector();

            dbConnector.OpenConnection();

            ///Select the help string from the Help table
            DeTools.gstrSQL = "Select Text_help, Text_help_caption from Help ";
            DeTools.gstrSQL = DeTools.gstrSQL + "where Help_id = " + "'" + i_Help_id + "';";
            using (OdbcDataReader reader = dbConnector.CreateResultset(DeTools.gstrSQL))
            {
                if (!reader.HasRows)
                {
                    frmHelp.pnlText.Text = "No entry available in the table.";
                }
                else
                {
                    string textHelp = reader["Text_help"].ToString();
                    frmHelp.pnlText.Text = string.IsNullOrEmpty(textHelp) ? "No entry available in the table." : textHelp;
                }
            }
            frmHelp.pnlInstructiontxt.Text = "Press Escape to exit from help.";
            frmHelp.pnlText.Visible = true;

        }

        public static void TransferData()
        {
            string comboval = "";
            frmM_Item frmM_Item = new frmM_Item();
            frmM_Sub_Group frmM_Subgrp = new frmM_Sub_Group();

            if (s_Mode == DeTools.ADDMODE && i_Help_id <= 9000)
            {
                if (o_form.Name != "frmM_Gift_Item")
                {
                    MessageBox.Show("You cannot add the existing record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Transfer data from help form to main form.
            if (i_Help_id < 2000)
            {
                // Single key fields
                if (frmHelp.grdHelp.CurrentRow != null)
                {
                    DataGridViewRow selectedRow = frmHelp.grdHelp.CurrentRow;
                    if (selectedRow.Cells.Count > 0)
                    {
                        o_control.Text = selectedRow.Cells[0].Value.ToString();
                        if (DeTools.gobjActiveForm is Interface_for_Common_methods.ISearchableForm searchableForm)
                        {
                            searchableForm.SearchForm();
                        }

                    }
                }
            }

            //=======for group combobox------------------//
            else if (i_Help_id == 9007)
            {
                // Multiple key fields
                //TransferMultipleKeyData();


                DataGridViewRow selectedRow = frmHelp.grdHelp.CurrentRow;
                if (selectedRow.Cells.Count > 0)
                {
                    if (o_form.Name == "frmM_Sub_Group")
                    {

                        // Assuming that cmbExample is your target ComboBox in frmT_Invoice
                        ComboBox targetComboBox = (ComboBox)o_form.Controls.Find("cboGrpId", true).FirstOrDefault();

                        // Transfer data to the ComboBox
                        if (targetComboBox != null)
                        {
                            // Assuming the data you want to transfer is in the first cell of the selected row
                            targetComboBox.SelectedItem = selectedRow.Cells[0].Value;

                            // If the ComboBox allows free text entry, you can use the following instead
                            // targetComboBox.Text = selectedRow.Cells[0].Value.ToString();
                        }

                    }

                    else if (o_form.Name == "frmM_Sub_Subgroup")
                    {

                        // Assuming that cmbExample is your target ComboBox in frmT_Invoice
                        ComboBox targetComboBox = (ComboBox)o_form.Controls.Find("cboGrpId", true).FirstOrDefault();

                        // Transfer data to the ComboBox
                        if (targetComboBox != null)
                        {
                            // Assuming the data you want to transfer is in the first cell of the selected row
                            targetComboBox.SelectedItem = selectedRow.Cells[0].Value;

                            // If the ComboBox allows free text entry, you can use the following instead
                            // targetComboBox.Text = selectedRow.Cells[0].Value.ToString();
                        }

                    }

                    else if (o_form.Name == "frmM_Item")
                    {

                        // Assuming that cmbExample is your target ComboBox in frmT_Invoice
                        ComboBox targetComboBox = (ComboBox)o_form.Controls.Find("cboGrpId", true).FirstOrDefault();

                        // Transfer data to the ComboBox
                        if (targetComboBox != null)
                        {
                            // Assuming the data you want to transfer is in the first cell of the selected row
                            targetComboBox.SelectedItem = selectedRow.Cells[0].Value;

                            // If the ComboBox allows free text entry, you can use the following instead
                            // targetComboBox.Text = selectedRow.Cells[0].Value.ToString();
                        }

                    }

                }


            }

            //=======for Sub group combobox------------------//
            else if (i_Help_id == 9008)
            {
                // Multiple key fields
                //TransferMultipleKeyData();


                DataGridViewRow selectedRow = frmHelp.grdHelp.CurrentRow;
                if (selectedRow.Cells.Count > 0)
                {
                    if (o_form.Name == "frmM_Sub_Subgroup")
                    {

                        // Assuming that cmbExample is your target ComboBox in frmT_Invoice
                        ComboBox targetComboBox = (ComboBox)o_form.Controls.Find("cboSubGrpId", true).FirstOrDefault();

                        // Transfer data to the ComboBox
                        if (targetComboBox != null)
                        {
                            // Assuming the data you want to transfer is in the first cell of the selected row
                            targetComboBox.SelectedItem = selectedRow.Cells[0].Value;

                            // If the ComboBox allows free text entry, you can use the following instead
                            // targetComboBox.Text = selectedRow.Cells[0].Value.ToString();
                        }

                    }

                    if (o_form.Name == "frmM_Item")
                    {

                        // Assuming that cmbExample is your target ComboBox in frmT_Invoice
                        ComboBox targetComboBox = (ComboBox)o_form.Controls.Find("cboSGroup", true).FirstOrDefault();

                        // Transfer data to the ComboBox
                        if (targetComboBox != null)
                        {
                            // Assuming the data you want to transfer is in the first cell of the selected row
                            targetComboBox.SelectedItem = selectedRow.Cells[0].Value;

                            // If the ComboBox allows free text entry, you can use the following instead
                            // targetComboBox.Text = selectedRow.Cells[0].Value.ToString();
                        }

                    }

                }


            }

            else if (!o_form.Name.StartsWith("frmR"))
            {
                if (o_form is Interface_for_Common_methods.ISearchableForm searchableForm)
                {
                    searchableForm.SearchForm();
                }
            }
        }

        //---for invoice---------------//
        public static void TransferDataInv(Form form)
        {
            s_Mode = DeTools.GetMode(form);
            // Transfer data from help form to main form.
            if (i_Help_id == 9001 && DeTools.gobjActiveForm.Name == "frmT_Invoice")
            {
                // Single key fields
                if (frmHelp.grdHelp.CurrentRow != null)
                {
                    // Assuming that targetGrid is your DataGridView
                    DataGridView targetGrid = (DataGridView)DeTools.gobjActiveForm.Controls.Find("dbgItemDet", true).FirstOrDefault();

                    // Get the current row index
                    int rowIndex = targetGrid.Rows.Add();
                    
                    // Assuming that grdHelp is your source DataGridView in frmHelp
                    DataGridViewRow selectedRow = frmHelp.grdHelp.CurrentRow;

                    if (selectedRow != null)
                    {
                        // Assuming you have a list of column indexes to transfer data
                        List<int> columnIndexesToTransfer = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 }; // Add the column indexes you want to transfer

                        foreach (int columnIndex in columnIndexesToTransfer)
                        {
                            // Check if the column index is within the bounds
                            if (columnIndex >= 0 && columnIndex < selectedRow.Cells.Count)
                            {
                                // Assuming that targetGrid has enough columns
                                targetGrid.Rows[rowIndex].Cells[1].Value = selectedRow.Cells[1].Value; //barcode
                                targetGrid.Rows[rowIndex].Cells[2].Value = selectedRow.Cells[0].Value; //itemnm
                                targetGrid.Rows[rowIndex].Cells[3].Value = "1";
                                targetGrid.Rows[rowIndex].Cells[4].Value = selectedRow.Cells[2].Value; //mrp
                                targetGrid.Rows[rowIndex].Cells[5].Value = selectedRow.Cells[3].Value; //unitprice
                                targetGrid.Rows[rowIndex].Cells[6].Value = selectedRow.Cells[6].Value; //disc%
                                targetGrid.Rows[rowIndex].Cells[7].Value = "0.00"; //discamt
                                targetGrid.Rows[rowIndex].Cells[8].Value = selectedRow.Cells[5].Value; //gst%
                                targetGrid.Rows[rowIndex].Cells[9].Value = selectedRow.Cells[7].Value; //cess%
                                targetGrid.Rows[rowIndex].Cells[10].Value = ((decimal.Parse(selectedRow.Cells[3].Value.ToString()) * 1) - decimal.Parse(targetGrid.Rows[rowIndex].Cells[7].Value.ToString())).ToString();

                                //targetGrid.Rows[rowIndex].Cells[10].Value = selectedRow.Cells[5].Value;
                                targetGrid.Rows[rowIndex].Cells[13].Value = selectedRow.Cells[4].Value;



                            }
                            else
                            {
                                // Handle the case where the column index is out of bounds
                                // You can log a message or take appropriate action
                            }
                        }

                        //targetGrid.CurrentCell = targetGrid.Rows[rowIndex].Cells[1];

                        //// Begin the edit to activate the cell for editing
                        //targetGrid.BeginEdit(true);

                    }
                }
            }


            else if (i_Help_id == 1012 && DeTools.gobjActiveForm.Name == "frmT_Invoice" && s_Mode== DeTools.MODIFYMODE)
            {
                // Assuming that grdHelp is your source DataGridView in frmHelp
                DataGridViewRow selectedRow = frmHelp.grdHelp.CurrentRow;

                if (selectedRow != null)
                {
                    // Assuming you have a list of column indexes to transfer data
                    List<int> columnIndexesToTransfer = new List<int> { 0}; // Add the column indexes you want to transfer

                    foreach (int columnIndex in columnIndexesToTransfer)
                    {
                        // Check if the column index is within the bounds
                        if (columnIndex >= 0 && columnIndex < selectedRow.Cells.Count)
                        {
                            frmT_Invoice.GetInvNoFromHelp = selectedRow.Cells[0].Value.ToString().Trim();
                            frmT_Invoice frminv = new frmT_Invoice();
                            frminv.txtInvNo.Text= selectedRow.Cells[0].Value.ToString().Trim();
                            frminv.SearchForm();
                        }
                    }
                }
            }
        }

         public static void TransferDataSr(Form form)
         {
            s_Mode = DeTools.GetMode(form);
            // Transfer data from help form to main form.
            if (i_Help_id == 9001 && DeTools.gobjActiveForm.Name == "frmT_Sale_Return")
            {
                // Single key fields
                if (frmHelp.grdHelp.CurrentRow != null)
                {
                    // Assuming that targetGrid is your DataGridView
                    DataGridView targetGrid = (DataGridView)DeTools.gobjActiveForm.Controls.Find("dbgItemDetRet", true).FirstOrDefault();

                    // Get the current row index
                    int rowIndex = targetGrid.Rows.Add();
                    
                    // Assuming that grdHelp is your source DataGridView in frmHelp
                    DataGridViewRow selectedRow = frmHelp.grdHelp.CurrentRow;

                    if (selectedRow != null)
                    {
                        // Assuming you have a list of column indexes to transfer data
                        List<int> columnIndexesToTransfer = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 }; // Add the column indexes you want to transfer

                        foreach (int columnIndex in columnIndexesToTransfer)
                        {
                            // Check if the column index is within the bounds
                            if (columnIndex >= 0 && columnIndex < selectedRow.Cells.Count)
                            {
                                // Assuming that targetGrid has enough columns
                                targetGrid.Rows[rowIndex].Cells[1].Value = selectedRow.Cells[1].Value; //barcode
                                targetGrid.Rows[rowIndex].Cells[2].Value = selectedRow.Cells[0].Value; //itemnm
                                targetGrid.Rows[rowIndex].Cells[3].Value = "1";
                                targetGrid.Rows[rowIndex].Cells[4].Value = selectedRow.Cells[2].Value; //mrp
                                targetGrid.Rows[rowIndex].Cells[5].Value = selectedRow.Cells[3].Value; //unitprice
                                targetGrid.Rows[rowIndex].Cells[6].Value = selectedRow.Cells[6].Value; //disc%
                                targetGrid.Rows[rowIndex].Cells[7].Value = "0.00"; //discamt
                                targetGrid.Rows[rowIndex].Cells[8].Value = selectedRow.Cells[5].Value; //gst%
                                targetGrid.Rows[rowIndex].Cells[9].Value = selectedRow.Cells[7].Value; //cess%
                                targetGrid.Rows[rowIndex].Cells[10].Value = ((decimal.Parse(selectedRow.Cells[3].Value.ToString()) * 1) - decimal.Parse(targetGrid.Rows[rowIndex].Cells[7].Value.ToString())).ToString();

                                //targetGrid.Rows[rowIndex].Cells[10].Value = selectedRow.Cells[5].Value;
                                targetGrid.Rows[rowIndex].Cells[13].Value = selectedRow.Cells[4].Value;



                            }
                            else
                            {
                                // Handle the case where the column index is out of bounds
                                // You can log a message or take appropriate action
                            }
                        }

                        //targetGrid.CurrentCell = targetGrid.Rows[rowIndex].Cells[1];

                        //// Begin the edit to activate the cell for editing
                        //targetGrid.BeginEdit(true);

                    }
                }
            }


            else if (i_Help_id == 1012 && DeTools.gobjActiveForm.Name == "frmT_Invoice" && s_Mode== DeTools.MODIFYMODE)
            {
                // Assuming that grdHelp is your source DataGridView in frmHelp
                DataGridViewRow selectedRow = frmHelp.grdHelp.CurrentRow;

                if (selectedRow != null)
                {
                    // Assuming you have a list of column indexes to transfer data
                    List<int> columnIndexesToTransfer = new List<int> { 0}; // Add the column indexes you want to transfer

                    foreach (int columnIndex in columnIndexesToTransfer)
                    {
                        // Check if the column index is within the bounds
                        if (columnIndex >= 0 && columnIndex < selectedRow.Cells.Count)
                        {
                            frmT_Invoice.GetInvNoFromHelp = selectedRow.Cells[0].Value.ToString().Trim();
                            frmT_Invoice frminv = new frmT_Invoice();
                            frminv.txtInvNo.Text= selectedRow.Cells[0].Value.ToString().Trim();
                            frminv.SearchForm();
                        }
                    }
                }
            }
         }






    }///////////////////////END////////


}