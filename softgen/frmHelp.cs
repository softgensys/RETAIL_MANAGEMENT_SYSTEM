using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace softgen
{
    public partial class frmHelp : Form
    {
        public frmHelp()
        {
            InitializeComponent();
            
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            Help.PrepareGridHelp();

        }

        private void cboFields_SelectionChangeCommitted(object sender, EventArgs e)
        {



        }

        private void cboFields_MouseClick(object sender, MouseEventArgs e)
        {


        }

        private void cboFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string selectedItemValue = cboDataType.SelectedItem.ToString().Trim();

            if (cboFields.Items.Count > 0)
            {
                cboFieldsId.SelectedIndex = cboFields.SelectedIndex;
                //string copyComboBoxItemValue = Help.data_type_index_copy.Items[cboDataType.SelectedIndex].ToString().Trim();
                //Help.send_combo_box_value = copyComboBoxItemValue.ToString();
            }
            cboDataType.SelectedIndex = cboFields.SelectedIndex;
            cboRel.Items.Clear();

            if (cboDataType.Text.Trim() == cboDataType.Text.Trim())
            {
                cboRel.Items.Add("=");
                cboRel.Items.Add("LIKE");
                // cboRel.SelectedIndex = 1; // Optional
                Help.selectedIndex = cboDataType.SelectedIndex;
            }
            else
            {
                cboRel.Items.Add("=");
                cboRel.Items.Add("<");
                cboRel.Items.Add("<=");
                cboRel.Items.Add(">");
                cboRel.Items.Add(">=");
                // cboRel.SelectedIndex = 0; // Optional
            }

            cboRel.SelectedIndex = 1;
            txtValue.Text = string.Empty;
            if (cboFields.SelectedItem != "")
            {
                txtValue.Focus();

            }

        }

        private void grdHelp_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if a valid cell was double-clicked
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Transfer data to another form
                if (DeTools.gobjActiveForm.Name == "frmT_Invoice")
                {
                    Help.TransferDataInv();
                }

                else
                {

                    Help.TransferData();
                }

                this.Hide();

            }
        }

        private void grdHelp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmHelp_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Prevent the form from closing
            e.Cancel = true;

            // Hide the form instead
            this.Hide();
        }
    }
}
