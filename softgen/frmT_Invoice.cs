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
    public partial class frmT_Invoice : Form, Interface_for_Common_methods.ISearchableForm
    {
        private string mstrEntBy, mstrEntOn, mstrAuthBy, mstrAuthOn, chkItemid;
        public bool mblnSearch, mblnDataEntered;
        public string strMode;
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
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(dbgItemDet, "Enter Item Details Here");
            toolTip.SetToolTip(dbgPayDet, "Enter Payment Details Here");

            DeTools.CheckTemporaryTableExists("t_invoice_hdr");
            DeTools.CheckTemporaryTableExists("t_invoice_det");
            DeTools.CheckTemporaryTableExists("t_invoice_pay_det");

            Help.controlToHelpTopicMapping.Add(txtInvNo, "1012"); /////For Help ContextId///IMP...

        }

        private void dbgItemDet_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

        }

        private void dbgItemDet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int columnIndex = 1; // Adjust this based on your requirement

            if (e.RowIndex >= 0 && e.ColumnIndex == columnIndex)
            {
                DataGridView dgv = (DataGridView)sender;
                string helpTopic = "9001"; // Adjust this based on your requirement

                // Store the help topic for the specific cell
                Tuple<DataGridView, int, int> key = Tuple.Create(dgv, e.RowIndex, e.ColumnIndex);
                Help.dgvCellToHelpTopicMapping[key] = helpTopic;
            }
        }
    }
}
