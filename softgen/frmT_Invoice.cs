using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace softgen
{
    public partial class frmT_Invoice : Form, Interface_for_Common_methods.ISearchableForm
    {
        private string mstrEntBy, mstrEntOn, mstrAuthBy, mstrAuthOn, chkItemid;
        public bool mblnSearch, mblnDataEntered;
        public string strMode;
        public int roundoffval = 3;
        private int chkItemsn;
        public DataGridViewCell cell;

        public frmT_Invoice()
        {
            InitializeComponent();

            this.Activated += MyForm_Activated;
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
            txtDiscPer.Text = "0.00";

            decimal discamttot = decimal.Parse(rotTotdisc.Text);
            // Round up to the nearest integer
            rotNetAmt.Text = (decimal.Parse(rotGAmt.Text) - discamttot).ToString("0.00");

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

                // Check if the value in the 10th column is null or empty
                if (!string.IsNullOrEmpty(dbgItemDet.Rows[i].Cells[10].Value?.ToString()) && column7Value > 0)
                {
                    decimal calcdiscper = (column7Value / column5Value) * 100;
                    dbgItemDet.Rows[i].Cells[6].Value = calcdiscper + "%";
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
            UpdateRotGAmt();
            disccal();
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
            // If the character is a decimal point
            if (sender is System.Windows.Forms.TextBox textBox)
            {
                int dotIndex = textBox.Text.IndexOf('.');
                if (dotIndex != -1 && textBox.Text.Length - dotIndex > 2)
                {
                    // Remove extra digits beyond 3 decimal places
                    textBox.Text = textBox.Text.Substring(0, dotIndex + 2);
                    textBox.SelectionStart = textBox.Text.Length; // Move cursor to the end
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
    }  ///////end
}
