using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace softgen
{
    public partial class frmT_Invoice : Form, Interface_for_Common_methods.ISearchableForm
    {
#pragma warning disable CS0169 // The field 'frmT_Invoice.chkItemid' is never used
        private string mstrEntBy, mstrEntOn, mstrAuthBy, mstrAuthOn, chkItemid;
#pragma warning restore CS0169 // The field 'frmT_Invoice.chkItemid' is never used
        public bool mblnSearch, mblnDataEntered;
        public string strMode;
        public int roundoffval = 2;
        private int chkItemsn;
        private bool cellValueChangedInProgress = false;
        private int flaggotonextcell = 0;
        private decimal mrpValue;
        private bool isValidationPerformed = false;
        private object previousValue;
        public DataGridViewCell cell;
        public DataGridViewComboBoxCell custIDCell1;
        private List<string> selectedValues = new List<string>();

        public frmT_Invoice()
        {
            InitializeComponent();

            this.Activated += MyForm_Activated;
            // dbgPayDet.EditingControlShowing += dbgPayDet_EditingControlShowing;
        }

        private void MyForm_Activated(object sender, EventArgs e)
        {
            DeTools.ClearStatusBarHelp();
            DeTools.ActiveFileMenu(this);
            DeTools.CreatedBy(mstrEntBy, mstrEntOn);
            DeTools.PostedBy(mstrAuthBy, mstrAuthOn);



        }



        public void SetSearchVar(bool StartVal)
        {
            // Implementation of SetSearchVar
            // You can define the behavior of SetSearchVar here

            mblnSearch = StartVal;
        }

        public bool GetDEStatus()
        {
            return mblnDataEntered == true ? true : false;
        }

        public void ClearForm()
        {
            int i, j;
            strMode = DeTools.GetMode(this);
            //mblnDataEntered = false;
            //mstrEntBy = null;
            //mstrEntOn = null;
            //mstrAuthBy = null;
            //mstrAuthOn = null;
            //ClearItemGrid();

            if (strMode == DeTools.ADDMODE)
            {

                txtInvNo.Enabled = false;
                cboCust.Focus();

            }
            else
            {
                txtInvNo.Enabled = true;
                txtInvNo.Focus();
            }


        }


        // Add this method to your class
        public void UpdateRotGAmt()
        {
            // Calculate the sum of values in column 5 (assuming column index is 5)
            decimal sumColumn5 = 0; //SP
            foreach (DataGridViewRow row in dbgItemDet.Rows)
            {
                if (row.Cells[5].Value != null)
                {
                    sumColumn5 += Convert.ToDecimal(row.Cells[5].Value);
                }
            }

            decimal sumColumn4 = 0; //mrp
            foreach (DataGridViewRow row in dbgItemDet.Rows)
            {
                if (row.Cells[4].Value != null)
                {
                    sumColumn4 += Convert.ToDecimal(row.Cells[4].Value);
                }
            }

            decimal sumColumn7 = 0; //disc amt
            foreach (DataGridViewRow row in dbgItemDet.Rows)
            {
                if (row.Cells[7].Value != null)
                {
                    sumColumn7 += Convert.ToDecimal(row.Cells[7].Value);
                }
            }


            // Calculate the sum of values in column 3 (assuming column index is 3)
            decimal sumColumn3 = 0; //qty
            foreach (DataGridViewRow row in dbgItemDet.Rows)
            {
                if (row.Cells[3].Value != null)
                {
                    sumColumn3 += Convert.ToDecimal(row.Cells[3].Value);
                }
            }

            // Calculate the product of the sums
            decimal product = sumColumn3 * sumColumn5;

            // Round the product to 2 decimal places
            decimal roundedProduct = Math.Round(product, roundoffval);

            // Assuming that rotGAmt is your label
            rotGAmt.Text = product.ToString("0.00");

            decimal rounddiff = 0;



            rotTotQty.Text = sumColumn3.ToString();
            rotTotmrp.Text = (sumColumn4 * sumColumn3).ToString();
            rotNOI.Text = (dbgItemDet.RowCount - 1).ToString();
            txtDiscAmt.Text = "0.00";
            rotTotdisc.Text = (Math.Round(sumColumn7 + decimal.Parse(txtDiscAmt.Text), roundoffval)).ToString();


            // decimal discamttot = decimal.Parse(rotTotdisc.Text);
            if (decimal.TryParse(rotTotdisc.Text, out decimal discamttot))
            {
                // Use discamttot here
                // Round up to the nearest integer
                rotNetAmt.Text = (decimal.Parse(rotGAmt.Text) - discamttot).ToString("0.00");
            }

            rotPayAmt.Text = Math.Round(decimal.Parse(rotNetAmt.Text), 0).ToString("0.0");
            rounddiff = Math.Abs(decimal.Parse(rotNetAmt.Text) - decimal.Parse(rotPayAmt.Text));

            if (decimal.Parse(rotNetAmt.Text) > decimal.Parse(rotPayAmt.Text))
            {

                if (rounddiff > 0.50m)
                {
                    rotRO.Text = "(+)" + (rounddiff).ToString();
                }
                else if (rounddiff <= 0.50m)
                {
                    rotRO.Text = "(-)" + (rounddiff).ToString();
                }
            }
            else if (decimal.Parse(rotNetAmt.Text) < decimal.Parse(rotPayAmt.Text))
            {
                if (rounddiff > 0.50m)
                {
                    rotRO.Text = "(-)" + (rounddiff).ToString();
                }
                else if (rounddiff <= 0.50m)
                {
                    rotRO.Text = "(+)" + (rounddiff).ToString();
                }
            }
            else
            {
                rotRO.Text = "(+/-)" + "0.00";
            }

            for (int i = 0; i < dbgItemDet.Rows.Count; i++)
            {
                // Assuming column indexes are 4, 6, and 8
                decimal column3Value = Convert.ToDecimal(dbgItemDet.Rows[i].Cells[3].Value ?? 0); // qty
                decimal column5Value = Convert.ToDecimal(dbgItemDet.Rows[i].Cells[5].Value ?? 0); // SP
                decimal column7Value = Convert.ToDecimal(dbgItemDet.Rows[i].Cells[7].Value ?? 0); // DISC AMT

                // Calculate the value for the 10th column
                decimal calculatedValue = Math.Round((column3Value * column5Value) - column7Value, roundoffval);

                // Update the 10th column value
                dbgItemDet.Rows[i].Cells[10].Value = calculatedValue.ToString();

            }
        }

        public void disccal()
        {
            for (int i = 0; i < dbgItemDet.Rows.Count; i++)
            {
                // Assuming column indexes are 4, 6, and 8
                decimal column3Value = Convert.ToDecimal(dbgItemDet.Rows[i].Cells[3].Value ?? 0); // qty
                decimal column5Value = Convert.ToDecimal(dbgItemDet.Rows[i].Cells[5].Value ?? 0); // SP
                decimal column7Value = Convert.ToDecimal(dbgItemDet.Rows[i].Cells[7].Value ?? 0); // DISC AMT
                decimal column6Value = Convert.ToDecimal(dbgItemDet.Rows[i].Cells[6].Value ?? 0); // DISC %
                //decimal column6Value = Convert.ToDecimal(dbgItemDet.Rows[i].Cells[6].Value ?? 0); // DISC %
                // Check if the value in the 10th column is not null or empty
                if (!string.IsNullOrEmpty(dbgItemDet.Rows[i].Cells[10].Value?.ToString()) && column7Value > 0)
                {
                    decimal calcdiscper = Math.Round((column7Value / column5Value) * 100, roundoffval);
                    dbgItemDet.Rows[i].Cells[6].Value = calcdiscper;
                }
                // Check if the value in the 10th column is null or empty
                if (column5Value > 0 && column6Value > 0)
                {
                    // Check if the denominator (column5Value * column3Value * column6Value) is not zero
                    if (column5Value * column3Value != 0)
                    {
                        decimal rawCalcdiscamt = (column5Value * column3Value * column6Value) / 100;

                        if (rawCalcdiscamt >= Decimal.MinValue && rawCalcdiscamt <= Decimal.MaxValue)
                        {
                            decimal calcdiscamt = Math.Round(rawCalcdiscamt, 2);
                            dbgItemDet.Rows[i].Cells[7].Value = calcdiscamt; // Assuming you want to assign the result to the 7th column
                        }
                        else
                        {
                            // Handle the case where the raw calculated value is too large or too small for a decimal
                            // For example, you might set a default value or show an error message
                            dbgItemDet.Rows[i].Cells[7].Value = 0.0M; // Default value or appropriate handling
                        }
                    }
                    else
                    {
                        // Handle the case where the denominator is zero (to avoid division by zero)
                        dbgItemDet.Rows[i].Cells[6].Value = 0.0M; // Default value or appropriate handling
                    }
                }

            }
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

        public void SaveForm()
        {

        }

        public void SearchForm()
        {

        }

        public void UnsavedData()
        {

        }

        public void check_temp_login_sytemname()
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void T_Invoice_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click_2(object sender, EventArgs e)
        {

        }

        private void rotGrpDesc_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void txtInvNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmT_Invoice_Load(object sender, EventArgs e)
        {
            DeTools.DisplayForm(this, 601, 937);
            this.Location = new Point(280, 0);
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(dbgItemDet, "Enter Item Details Here");
            toolTip.SetToolTip(dbgPayDet, "Enter Payment Details Here");

            DeTools.CheckTemporaryTableExists("t_invoice_hdr");
            DeTools.CheckTemporaryTableExists("t_invoice_det");
            DeTools.CheckTemporaryTableExists("t_invoice_pay_det");

            Help.controlToHelpTopicMapping.Add(txtInvNo, "1012"); /////For Help ContextId///IMP...
            txtDiscPer.Text = "0.00";
            dbgPayDet.DataError += dbgPayDet_DataError;
            PopulateDataGridViewWithComboBox();
            //dbgPayDet.CellValidating += dbgPayDet_CellValidating;

            dbgPayDet.CellValueChanged += dbgPayDet_CellValueChanged;
            UpdateDataGridViewStatus();
        }

        private void dbgItemDet_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl editingControl)
            {
                // Assuming the columns you want to restrict are at indices 1, 2, and 3
                int columnIndex = dbgItemDet.CurrentCell.ColumnIndex;
                if (columnIndex > 3)
                {
                    editingControl.KeyPress -= NumericKeyPressHandlergrthree;
                    editingControl.KeyPress += NumericKeyPressHandlergrthree;


                }
                else if (columnIndex == 3)
                {
                    editingControl.KeyPress -= NumericKeyPressHandlerforthree;
                    editingControl.KeyPress += NumericKeyPressHandlerforthree;


                }
            }
        }

        private void dbgItemDet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //int columnIndex = 1; // Adjust this based on your requirement

            //if (e.RowIndex >= 0 && e.ColumnIndex == columnIndex)
            //{
            //    DataGridView dgv = (DataGridView)sender;
            //    string helpTopic = "9001"; // Adjust this based on your requirement

            //    // Store the help topic for the specific cell
            //    Tuple<DataGridView, int, int> key = Tuple.Create(dgv, e.RowIndex, e.ColumnIndex);
            //    Help.dgvCellToHelpTopicMapping[key] = helpTopic;
            //}
        }

        private void dbgItemDet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void rotTotQty_Click(object sender, EventArgs e)
        {

        }

        private void lblTotDisc_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void lblTotMrp_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {
        }

        private void dbgItemDet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!cellValueChangedInProgress)
            {
                cellValueChangedInProgress = true;

                // Your event handling code here
                UpdateRotGAmt();
                disccal();

                cellValueChangedInProgress = false;
                UpdateDataGridViewStatus();
            }

        }

        private void dbgItemDet_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //----code for serial number-----------

            // Get the current row index
            int rowIndex = e.RowIndex;

            // Get the serial number (row index + 1)
            string serialNumber = (rowIndex + 1).ToString();

            // Get the bounds of the cell in the 0th column
            Rectangle cellBounds = dbgItemDet.GetCellDisplayRectangle(0, rowIndex, false);

            // Set the position for drawing the serial number
            float x = cellBounds.Location.X + cellBounds.Width / 2 - (TextRenderer.MeasureText(serialNumber, dbgItemDet.Font).Width / 2);
            float y = cellBounds.Location.Y + cellBounds.Height / 2 - (TextRenderer.MeasureText(serialNumber, dbgItemDet.Font).Height / 2);

            // Draw the serial number on the DataGridView
            e.Graphics.DrawString(serialNumber, dbgItemDet.Font, SystemBrushes.ControlText, x, y);
        }

        private void dbgItemDet_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Delete key is pressed
            if (e.KeyCode == Keys.Delete)
            {
                // Check if there is a selected row
                if (dbgItemDet.SelectedRows.Count > 0)
                {
                    // Get the selected row
                    DataGridViewRow selectedRow = dbgItemDet.SelectedRows[0];

                    // Get the index of the selected row
                    int rowIndex = selectedRow.Index;

                    // Remove the selected row
                    dbgItemDet.Rows.Remove(selectedRow);

                    UpdateRotGAmt();
                    disccal();
                    // Update serial numbers of the remaining rows
                    // UpdateSerialNumbers();
                }
            }
        }

        // Add this method to your class
        public void UpdateSerialNumbers()
        {
            // Update the serial numbers based on the current rows
            for (int i = 0; i < dbgItemDet.Rows.Count; i++)
            {
                // Assuming that the serial number is displayed in the 0th column
                dbgItemDet.Rows[i].Cells[0].Value = (i + 1).ToString();
            }
        }

        private void lblRO_Click(object sender, EventArgs e)
        {

        }

        private void rotRO_Click(object sender, EventArgs e)
        {
        }

        private void rotNOI_Click(object sender, EventArgs e)
        {

        }

        private void lblNetAmt_Click(object sender, EventArgs e)
        {

        }

        private void rotNetAmt_Click(object sender, EventArgs e)
        {
        }

        private void dbgItemDet_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int columnIndex = 1; // 0 corresponds to the first column

            if (e.RowIndex >= 0 && e.ColumnIndex == columnIndex)
            {
                DataGridView dgv = (DataGridView)sender;
                string helpTopic = "9001"; // Adjust this based on your requirement

                // Store the help topic for the specific cell
                Tuple<DataGridView, int, int> key = Tuple.Create(dgv, e.RowIndex, e.ColumnIndex);
                Help.dgvCellToHelpTopicMapping[key] = helpTopic;
            }
        }

        private void NumericKeyPressHandlergrthree(object sender, KeyPressEventArgs e)
        {
            // Allow numeric characters, backspace, and the decimal point
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; // Suppress the key press
            }
            //If the character is a decimal point
            if (sender is System.Windows.Forms.TextBox textBox)
            {
                if (textBox.Text.Contains('.'))
                {
                    int dotIndex = textBox.Text.IndexOf('.');


                    if (dotIndex != -1 && textBox.Text.Length - dotIndex > 2)
                    {
                        // Remove extra digits beyond 3 decimal places
                        textBox.Text = textBox.Text.Substring(0, dotIndex + 2);
                        //textBox.SelectionStart = textBox.Text.Length; // Move cursor to the end
                    }
                }
            }

        }
        private void NumericKeyPressHandlerforthree(object sender, KeyPressEventArgs e)
        {
            // Allow numeric characters, backspace, and the decimal point
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; // Suppress the key press
            }
            // If the character is a decimal point
            if (sender is System.Windows.Forms.TextBox textBox)
            {
                int dotIndex = textBox.Text.IndexOf('.');
                if (dotIndex != -1 && textBox.Text.Length - dotIndex > 3)
                {
                    // Remove extra digits beyond 3 decimal places
                    textBox.Text = textBox.Text.Substring(0, dotIndex + 3);
                    textBox.SelectionStart = textBox.Text.Length; // Move cursor to the end
                }
            }

        }

        private void dbgItemDet_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the edited cell is in a column where formatting is desired
            if (e.ColumnIndex >= 3)
            {
                // Get the entered value
                object rawValue = dbgItemDet.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                mrpValue = Convert.ToDecimal(dbgItemDet.Rows[e.RowIndex].Cells[4].Value);
                // Format the value as a decimal with two decimal places
                if (rawValue != null && decimal.TryParse(rawValue.ToString(), out decimal enteredValue))
                {
                    // Ensure Column 5 (SP) is not greater than Column 4 (MRP)
                    if (e.ColumnIndex == 5 && flaggotonextcell == 1)
                    {
                        // Reset the value to MRP
                        dbgItemDet.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = mrpValue;
                    }

                    dbgItemDet.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = enteredValue.ToString("0.00");
                }
            }
        }

        private void dbgItemDet_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            // Check if leaving the last column
            if (e.ColumnIndex == dbgItemDet.Columns.Count - 1)
            {
                // Check if leaving the last row
                if (e.RowIndex < dbgItemDet.Rows.Count - 1)
                {
                    // Move the selection to the next row's 1st column after a small delay
                    this.BeginInvoke(new Action(() =>
                    {
                        dbgItemDet.CurrentCell = dbgItemDet[0, e.RowIndex + 1];
                    }));
                }
            }
        }

        public void frmT_Invoice_FormClosed(object sender, FormClosedEventArgs e)
        {
            DeTools.DestroyToolbar(this);
            MainForm.Instance.TInvGenmenu.Enabled = true;
        }

        private void dbgItemDet_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                // Check if SP is greater than MRP
                decimal enteredValue = Convert.ToDecimal(e.FormattedValue);
                mrpValue = Convert.ToDecimal(dbgItemDet.Rows[e.RowIndex].Cells[4].Value);
                // Store the previous value before making any changes

                if (enteredValue > mrpValue)
                {

                    // Display a message or take appropriate action
                    MessageBox.Show("SP cannot be greater than MRP");
                    flaggotonextcell = 1;
                    // Cancel the cell validation to prevent leaving the current cell                    

                    e.Cancel = true;
                    // Update other calculations as needed
                    UpdateRotGAmt();
                    disccal();
                }
            }
        }

        private void dbgItemDet_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            //           previousValue= dbgItemDet.Rows[e.RowIndex].Cells[5].Value;
        }

        private void PopulateDataGridViewWithComboBox()
        {
            try
            {
                string sql;
                DbConnector dbConnector = new DbConnector();

                sql = "SELECT distinct pay_mode_id FROM m_pay_mode WHERE status LIKE 'A'";

                using (OdbcDataReader reader = dbConnector.CreateResultset(sql))
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add("Paymod", typeof(string));

                    // Add a new row to the DataTable
                    DataRow row = dataTable.NewRow();

                    // Populate the ComboBox column in the new row
                    while (reader.Read())
                    {
                        row["Paymod"] = reader["pay_mode_id"].ToString();
                    }

                    // Add the row to the DataTable
                    dataTable.Rows.Add(row);

                    // Set AllowUserToAddRows property to false
                    //dbgPayDet.AllowUserToAddRows = false;

                    // Set the DataTable as the DataSource for the DataGridView
                    dbgPayDet.DataSource = dataTable;

                    // Set the combo box data source
                    DataGridViewComboBoxColumn comboColumn = (DataGridViewComboBoxColumn)dbgPayDet.Columns["Paymod"];
                    comboColumn.DataSource = GetAvailableOptions();
                    comboColumn.DisplayMember = "Paymod";
                    comboColumn.ValueMember = "Paymod";

                    // Update the status of the DataGridView
                    UpdateDataGridViewStatus();
                }
            }
            catch (Exception ex)
            {
                // Handle the exception or display an error message
                Console.WriteLine(ex.Message);
            }
        }

        private DataTable GetAvailableOptions()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Paymod", typeof(string));

            // Retrieve all available options from the database
            // and add them to the DataTable
            string sql;
            DbConnector dbConnector = new DbConnector();

            sql = "SELECT distinct pay_mode_id FROM m_pay_mode WHERE status LIKE 'A'";

            using (OdbcDataReader reader = dbConnector.CreateResultset(sql))
            {
                while (reader.Read())
                {
                    DataRow row = dataTable.NewRow();
                    row["Paymod"] = reader["pay_mode_id"].ToString();
                    dataTable.Rows.Add(row);
                }
            }

            return dataTable;
        }

        //private void AddAndSelectItemInDataGridViewComboBox(string selectedItem, int rowIndex)
        //{
        //    // Assuming "CustId" is the name of the 7th column ComboBox
        //    DataGridViewComboBoxColumn comboColumn = (DataGridViewComboBoxColumn)dbgPayDet.Columns["CustId"];

        //    // Check if selectedItem is not null before adding it to the DataGridViewComboBoxColumn items
        //    if (!string.IsNullOrEmpty(selectedItem))
        //    {
        //        // Add the selected item to the DataGridViewComboBoxColumn
        //        comboColumn.Items.Add(selectedItem);
        //    }

        //    // Find the DataGridViewRow
        //    DataGridViewRow targetRow = dbgPayDet.Rows[rowIndex];

        //    // Find the DataGridViewComboBoxCell in the 7th column of the target row
        //    DataGridViewComboBoxCell comboCell = (DataGridViewComboBoxCell)targetRow.Cells["CustId"];

        //    // Set the selected item
        //    comboCell.Value = selectedItem;
        //}


        private void AddAndSelectItemInDataGridViewComboBox(string itemToAdd, int rowIndex)
        {
            // Assuming "CustId" is the name of the 7th column ComboBox
            DataGridViewComboBoxColumn comboColumn = (DataGridViewComboBoxColumn)dbgPayDet.Columns["CustId"];

            // Check if itemToAdd is not null and not already in the items
            if (!string.IsNullOrEmpty(itemToAdd) && !comboColumn.Items.Contains(itemToAdd))
            {
                // Add the item to the DataGridViewComboBoxColumn
                comboColumn.Items.Add(itemToAdd);
            }

            // Find the DataGridViewRow
            DataGridViewRow targetRow = dbgPayDet.Rows[rowIndex];

            // Find the DataGridViewComboBoxCell in the "CustId" column of the target row
            DataGridViewComboBoxCell comboCell = (DataGridViewComboBoxCell)targetRow.Cells["CustId"];

            // Set the selected item
            comboCell.Value = itemToAdd;
        }


        private void dbgPayDet_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

        }

        private void dbgPayDet_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //// Check if the error is related to a ComboBox cell
            //if (e.Exception is ArgumentException && dbgPayDet.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn)
            //{
            //    // Display a custom error message
            //    MessageBox.Show("Invalid value selected in ComboBox. Please select a valid value.");
            //}
            //else
            //{
            //    // Handle other data errors if needed
            //    // You might want to log or handle other types of errors here
            //    MessageBox.Show($"DataError: {e.Exception.Message}");
            //}

            //// Optionally, you can set e.ThrowException to false to suppress the default error dialog
            //e.ThrowException = false;
        }

        private void dbgPayDet_RowLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dbgPayDet_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //----code for serial number-----------

            // Get the current row index
            int rowIndex = e.RowIndex;

            // Get the serial number (row index + 1)
            string serialNumber = (rowIndex + 1).ToString();

            // Get the bounds of the cell in the 0th column
            Rectangle cellBounds = dbgPayDet.GetCellDisplayRectangle(0, rowIndex, false);

            // Set the position for drawing the serial number
            float x = cellBounds.Location.X + cellBounds.Width / 2 - (TextRenderer.MeasureText(serialNumber, dbgItemDet.Font).Width / 2);
            float y = cellBounds.Location.Y + cellBounds.Height / 2 - (TextRenderer.MeasureText(serialNumber, dbgItemDet.Font).Height / 2);

            // Draw the serial number on the DataGridView
            e.Graphics.DrawString(serialNumber, dbgPayDet.Font, SystemBrushes.ControlText, x, y);
        }

        private void dbgPayDet_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // Check if validation has already been performed for this cell
            if (e.ColumnIndex == dbgPayDet.Columns["Paymod"].Index && e.RowIndex >= 0 && !isValidationPerformed)
            {
              
                // Set the flag to true to prevent duplicate messages
                isValidationPerformed = true;

                // Check for other conditions or validations as needed
            }


        }

        //-Lock the dbgPayDet
        private void UpdateDataGridViewStatus()
        {
            // Get the value from rotPayAmt label, assuming it contains a numeric value
            decimal rotPayAmtValue = 0;

            if (decimal.TryParse(rotPayAmt.Text, out rotPayAmtValue))
            {
                // Enable or disable the DataGridView based on the condition
                dbgPayDet.Enabled = rotPayAmtValue > 0;
            }

        }



        //private void dbgPayDet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.ColumnIndex == dbgPayDet.Columns["Paymod"].Index && e.RowIndex >= 0)
        //    {
        //        DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)dbgPayDet[e.ColumnIndex, e.RowIndex];
        //        string selectedValue = cell.Value?.ToString();

        //        if (!string.IsNullOrEmpty(selectedValue))
        //        {
        //            selectedValues.Add(selectedValue);

        //            // Get the selected value in the ComboBox
        //            DataGridViewComboBoxCell comboCell = (DataGridViewComboBoxCell)dbgPayDet.Rows[e.RowIndex].Cells["Paymod"];
        //            string selectedValue1 = comboCell.Value?.ToString();

        //            // Check if the selected value is 'CR'
        //            //if (!string.IsNullOrEmpty(selectedValue1) && selectedValue1 == "CR")
        //            //{
        //            // Get the corresponding value from cboInvCust
        //            string selectedItem = cboCust.SelectedItem?.ToString();

        //            // Add and select the item in the 7th column ComboBox
        //            AddAndSelectItemInDataGridViewComboBox(selectedItem, e.RowIndex);
        //            //}
        //        }

        //    }
        //}

        private void dbgPayDet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dbgPayDet.Columns["Paymod"].Index && e.RowIndex >= 0)
            {
                DataGridViewComboBoxCell comboCell = (DataGridViewComboBoxCell)dbgPayDet.Rows[e.RowIndex].Cells["CustId"];
                string enteredText = comboCell.EditedFormattedValue?.ToString();

                // Check if the value in rotInvCust is "Value does not exist"
                if (rotInvCust.Text == "Value does not exist")
                {
                    // Add the entered text from cboCust to the "CustId" column ComboBox in dbgPayDet
                    AddAndSelectItemInDataGridViewComboBox(cboCust.Text.Trim(), e.RowIndex);
                }
                else
                {
                    // Get the selected item from cboCust
                    string selectedItem = cboCust.SelectedItem?.ToString();

                    // Add and select the item in the "CustId" column ComboBox in dbgPayDet
                    AddAndSelectItemInDataGridViewComboBox(selectedItem, e.RowIndex);
                }

                if (e.ColumnIndex == 1 && e.RowIndex >= 0)
                {
                    DataGridViewComboBoxCell comboCell1 = (DataGridViewComboBoxCell)dbgPayDet.Rows[e.RowIndex].Cells["Paymod"];
                    string selectedValue = comboCell1.Value?.ToString();

                    decimal sum = 0;
                    decimal paycalc = 0;
                    string newValue = rotPayAmt.Text.Trim();
                    // Assuming "Column2" is the name of the 2nd column
                    DataGridViewTextBoxColumn column2 = (DataGridViewTextBoxColumn)dbgPayDet.Columns["PayAmt"];

                    // Update the value in the 2nd column
                    column2.DataGridView.Rows[e.RowIndex].Cells[column2.Index].Value = newValue;

                    foreach (DataGridViewRow row in dbgPayDet.Rows)
                    {
                        if (row.Cells[column2.Index].Value != null && decimal.TryParse(row.Cells[column2.Index].Value.ToString(), out decimal value))
                        {
                            sum += Math.Round(value, 0);
                        }

                        if (sum < decimal.Parse(newValue))
                        {
                            column2.DataGridView.Rows[e.RowIndex].Cells[column2.Index].Value = sum;
                        }
                    }


                    // Update the sum of column2 values and check if it matches rotPayAmt
                    decimal remainingAmount = CalculateRemainingAmount();

                    if (remainingAmount != 0)
                    {
                        // Display the remaining amount in the next row's column2
                        UpdateColumn2ValueInNextRow(remainingAmount, e.RowIndex);
                    }
                    else
                    {
                        // Display a message indicating that the amount is matched
                        MessageBox.Show("Amount Matched", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }
        }

        //private void UpdateColumn2Value(DataGridViewTextBoxColumn column2, string newValue, int rowIndex)
        //{
        //    decimal sum = 0;
        //    decimal paycalc = 0;
        //    // Update the value in the 2nd column
        //    column2.DataGridView.Rows[rowIndex].Cells[column2.Index].Value = newValue;

        //    foreach (DataGridViewRow row in dbgPayDet.Rows)
        //    {
        //        if (row.Cells[column2.Index].Value != null && decimal.TryParse(row.Cells[column2.Index].Value.ToString(), out decimal value))
        //        {
        //            sum += Math.Round(value,0) - Math.Round(sum,0);
        //        }

        //        if (sum < decimal.Parse(newValue))
        //        {
        //            column2.DataGridView.Rows[rowIndex].Cells[column2.Index].Value = sum;
        //        }
        //    }


        //}
        // Calculate the sum of values in the specified column (column2)
        private decimal CalculateColumn2Sum()
        {
            decimal sum = 0;

            // Assuming "Column2" is the name of the 2nd column
            DataGridViewTextBoxColumn column2 = (DataGridViewTextBoxColumn)dbgPayDet.Columns["PayAmt"];

            if (column2 != null)
            {
                foreach (DataGridViewRow row in dbgPayDet.Rows)
                {
                    // Check if the column exists in the row
                    if (row.Cells.Count > column2.Index && row.Cells[column2.Index].Value != null &&
                        decimal.TryParse(row.Cells[column2.Index].Value.ToString(), out decimal value))
                    {
                        sum += value;
                    }
                }
            }

            return sum;
        }

        // Calculate the remaining amount based on the difference between rotPayAmt and the sum of column2 values
        private decimal CalculateRemainingAmount()
        {
            decimal totalAmount = Convert.ToDecimal(rotPayAmt.Text);
            decimal currentSum = CalculateColumn2Sum();

            return totalAmount - currentSum;
        }

        // Update the column2 value in the next row with the remaining amount
        private void UpdateColumn2ValueInNextRow(decimal remainingAmount, int rowIndex)
        {
            // Assuming "Column2" is the name of the 2nd column
            DataGridViewTextBoxColumn column2 = (DataGridViewTextBoxColumn)dbgPayDet.Columns["PayAmt"];

            // Find the next DataGridViewRow
            DataGridViewRow nextRow = dbgPayDet.Rows[rowIndex + 1];

            // Find the DataGridViewTextBoxCell in the 2nd column of the next row
            DataGridViewTextBoxCell textBoxCell = (DataGridViewTextBoxCell)nextRow.Cells["PayAmt"];

            // Set the remaining amount as the value in the TextBoxCell
            textBoxCell.Value = remainingAmount.ToString();
        }

       


        public string GetDescCust(string strTable, string strId_Field, string strDesc_Field, string strType, Object vntId_Value)
        {
            DbConnector dbConnector = new DbConnector();
            string result = string.Empty;

            if (vntId_Value == null || string.IsNullOrWhiteSpace(vntId_Value.ToString()))
            {
                return string.Empty;
            }
            else
            {
                string sql = "SELECT " + strDesc_Field + " from " + strTable + " where " + strId_Field + "=";
                if (strType == "N")
                {
                    sql += vntId_Value;
                }
                else if (strType == "C")
                {
                    sql += "'" + vntId_Value + "'";
                }
                else if (strType == "D")
                {
                    sql += "'" + ((DateTime)vntId_Value).ToString("yyyy-MM-dd") + "'";
                }

                using (OdbcDataReader reader = dbConnector.CreateResultset(sql))
                {
                    if (reader != null)
                    {
                        if (reader.Read())
                        {
                            result = reader[0].ToString().Trim();
                        }
                        //else
                        //{
                        //    result = "Value does not Exists in the Master.";
                        //    Messages.InfoMsg("Value does not Exists in the Master!");
                        //}
                        reader.Close();
                    }
                }
            }

            return result;

        }

        //public void FillCustCombo()
        //{
        //    try
        //    {
        //        string sql;
        //        DbConnector dbConnector = new DbConnector();

        //        sql = "SELECT DISTINCT cust_id FROM m_customer";


        //        using (OdbcDataReader reader = dbConnector.CreateResultset(sql))
        //        {
        //            string oldValue = cboCust.Text.ToString().Trim();
        //            cboCust.Items.Clear();

        //            while (reader.Read())
        //            {
        //                cboCust.Items.Add(reader["cust_id"].ToString().Trim());

        //            }


        //            cboCust.MaxDropDownItems = 5;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle the exception or display an error message
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        public void FillCustCombo()
        {
            try
            {
                string sql;
                DbConnector dbConnector = new DbConnector();

                sql = "SELECT DISTINCT cust_id FROM m_customer";

                using (OdbcDataReader reader = dbConnector.CreateResultset(sql))
                {
                    string searchText = cboCust.Text.ToLower().Trim();
                    bool isTextExists = false;

                    // Check if the text actually changed
                    if (cboCust.Tag == null || !searchText.Equals(cboCust.Tag.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        cboCust.Items.Clear();

                        while (reader.Read())
                        {
                            string custId = reader["cust_id"].ToString().Trim();

                            // Check if the current customer ID contains the entered text
                            if (custId.ToLower().Contains(searchText))
                            {
                                cboCust.Items.Add(custId);
                                isTextExists = true;
                            }
                        }

                        cboCust.MaxDropDownItems = 5;

                        // Update the label based on whether the entered text exists
                        rotInvCust.Text = isTextExists ? GetDescription(searchText) : "Value does not exist";

                        // Set the cursor position to the end of the text
                        cboCust.Select(cboCust.Text.Length, 0);

                        // Store the current text in the Tag property
                        cboCust.Tag = searchText;

                        // Enable/disable dbgPayDet based on cboCust
                        //dbgPayDet.Enabled = isTextExists && !string.IsNullOrEmpty(cboCust.SelectedItem?.ToString().Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle the exception or display an error message
                Console.WriteLine(ex.Message);
            }
        }

        private void cboCust_DropDown(object sender, EventArgs e)
        {
            FillCustCombo();
        }

        private string GetDescription(string custId)
        {
            // You can implement the logic to retrieve the description based on custId
            General general = new General();
            return GetDescCust("m_customer", "cust_id", "cust_name", "C", custId);
        }

        private void cboCust_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCust.SelectedItem != null)
            {
                General general = new General();
                string desc = GetDescCust("m_customer", "cust_id", "cust_name", "C", cboCust.SelectedItem.ToString().Trim());
                rotInvCust.Text = desc;
                //dbgPayDet.Enabled = cboCust != null && !string.IsNullOrEmpty(cboCust.SelectedItem?.ToString().Trim());
            }

            // Get the selected item from cboCust
            string selectedItem = cboCust.SelectedItem?.ToString();

            // Check if there is any selected item in the Paymod column ComboBox in dbgPayDet
            foreach (DataGridViewRow row in dbgPayDet.Rows)
            {
                DataGridViewComboBoxCell paymodCell = (DataGridViewComboBoxCell)row.Cells["Paymod"];
                if (paymodCell.Value != null)
                {
                    // Update the corresponding 7th column ComboBox in the same row
                    DataGridViewComboBoxCell custIDCell = (DataGridViewComboBoxCell)row.Cells["CustId"];

                    // Clear any existing item in the 7th column ComboBox
                    custIDCell.Value = null;
                    if (!string.IsNullOrEmpty(selectedItem) & selectedItem != null)
                    {
                        // Add the selected item from cboCust to the 7th column ComboBox
                        custIDCell.Items.Add(selectedItem);
                        
                    }

                    // Automatically select the added item in the 7th column ComboBox
                    custIDCell.Value = selectedItem;
                }
            }
        }

        private void dbgPayDet_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            // Check if the selected value already exists in any other row
            if (e.ColumnIndex == dbgPayDet.Columns["Paymod"].Index && e.RowIndex >= 0)
            {
                string newValue = dbgPayDet[e.ColumnIndex, e.RowIndex].Value?.ToString();

                //// Check if validation has already been performed for this cell
                //if (isValidationPerformed)
                //{
                //    isValidationPerformed = false; // Reset the flag
                //    return;
                //}

                // Check for duplicate selections in the Paymod column
                foreach (DataGridViewRow row in dbgPayDet.Rows)
                {
                    if (row.Index != e.RowIndex)
                    {
                        DataGridViewComboBoxCell comboCell = (DataGridViewComboBoxCell)row.Cells["Paymod"];
                        if (comboCell.Value != null && comboCell.Value.ToString() == newValue)
                        {
                            // Cancel the edit
                            dbgPayDet.CancelEdit();

                            MessageBox.Show("This value is already selected in another row. Please select a different value.", "Duplicate Value", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            // Clear the selected item in the ComboBox
                            DataGridViewComboBoxCell currentCell = (DataGridViewComboBoxCell)dbgPayDet[e.ColumnIndex, e.RowIndex];
                            currentCell.Value = null;

                            break; // No need to continue checking if a duplicate is found
                        }
                    }
                }

                if (newValue == "CR")
                {
                    // Check if a customer is selected in cboCust
                    if (cboCust.SelectedIndex == -1)
                    {
                        MessageBox.Show("Please select a customer ID.", "Missing Customer ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboCust.Focus();
                        cboCust.SelectAll();

                        DataGridViewComboBoxCell custIDCell1 = (DataGridViewComboBoxCell)dbgPayDet.Rows[e.RowIndex].Cells[7];
                      //  dbgPayDet.Enabled = custIDCell1 != null && !string.IsNullOrEmpty(custIDCell1.Value?.ToString().Trim());
                    }
                }

                // Reset the validation flag
                isValidationPerformed = false;
                // Check for other conditions or validations as needed
            }
        }

        private void UpdateComboBoxItems(DataGridViewComboBoxColumn comboColumn, string newText)
        {
            // Check if the new text is already present in the ComboBox items
            if (!string.IsNullOrEmpty(newText) && !comboColumn.Items.Contains(newText))
            {
                // Remove the old items and add the new text
                comboColumn.Items.Clear();
                comboColumn.Items.Add(newText);

                // Update the displayed values in the DataGridView
                foreach (DataGridViewRow row in dbgPayDet.Rows)
                {
                    DataGridViewComboBoxCell comboCell = (DataGridViewComboBoxCell)row.Cells["CustId"];
                    comboCell.DataSource = comboColumn.Items;
                }
            }
        }

        private void cboCust_TextChanged(object sender, EventArgs e)
        {
            FillCustCombo();

            // Assuming "CustId" is the name of the 7th column ComboBox
            DataGridViewComboBoxColumn comboColumn = (DataGridViewComboBoxColumn)dbgPayDet.Columns["CustId"];

            // Update the items in the DataGridViewComboBoxColumn based on cboCust's text
            UpdateComboBoxItems(comboColumn, cboCust.Text.Trim());

        }








    }  ///////end
}
