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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace softgen
{
    public partial class frmR_invoice_wise_sale_rpt : Form, Interface_for_Common_methods.ISearchableForm
    {
        General general = new General();
        private string mstrEntBy, mstrEntOn, mstrAuthBy, mstrAuthOn, chkItemid;
        public bool mblnSearch, mblnDataEntered;
        public static string Paymchecked_yn = "N";
        public static string Gstdetchecked_yn = "N";
        public static string ItemGstdetchecked_yn = "N";
        public static string InvGstdetchecked_yn = "N";
        public static string FromDt = "", Todt = "";
        public static string FromDt_mysql = "", Todt_mysql = "";
        // Get the selected item from the ComboBox
        public static string selectedPaymItem = "";
        DbConnector dbConnector;

        public frmR_invoice_wise_sale_rpt()
        {
            InitializeComponent();
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
            //GetFormattedDate();
            r_Invoicewise_sale_frm r_Invoicewise_Sale_Frm = new r_Invoicewise_sale_frm();
            r_Invoicewise_Sale_Frm.GenerateAndDisplayReport();

            r_Invoicewise_Sale_Frm.ShowDialog();
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

        private void cboPaymod_DropDown(object sender, EventArgs e)
        {
            //general.FillCombo(cboPaymod, "pay_mode_id", "m_paymode", false);
            DbConnector dbConnector = new DbConnector();
            dbConnector.connection = new OdbcConnection(dbConnector.connectionString);
            dbConnector.connection.Open();

            string paymode = "Select pay_mode_id from m_pay_mode;";

            using (OdbcCommand cmdtopaym = new OdbcCommand(paymode, dbConnector.connection))
            {
                string oldValue = cboPaymod.Text.Trim();
                cboPaymod.Items.Clear();
                using (OdbcDataReader reader = cmdtopaym.ExecuteReader())
                {
                    if (reader.HasRows)
                    {


                        while (reader.Read())
                        {
                            cboPaymod.Items.Add(reader["pay_mode_id"].ToString().Trim());

                        }
                    }
                    cboPaymod.Text = oldValue;
                    cboPaymod.MaxDropDownItems = 5;
                }
            }
        }

        private void MyForm_Activated(object sender, EventArgs e)
        {
            DeTools.ClearStatusBarHelp();
            DeTools.ActiveFileMenu(this);
            DeTools.CreatedBy(mstrEntBy, mstrEntOn);
            DeTools.PostedBy(mstrAuthBy, mstrAuthOn);



        }

        private void frmR_invoice_wise_sale_rpt_Load(object sender, EventArgs e)
        {
            DeTools.DisplayForm(this, 217, 487);
            //this.Location = new Point(280, 0);
        }

        private void chkPaymAll_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkPaymAll.Checked)
            {
                Paymchecked_yn = "Y";
            }
            else
            {
                Paymchecked_yn = "N";
            }
        }

        public string GetFormattedFDate()
        {
            // Get the date value from the DateTimePicker
            DateTime selectedFDate = dtpFromDate.Value;

            // Format the date in MySQL date format (yyyy-MM-dd)
            string formattedFDate = selectedFDate.ToString("yyyy-MM-dd");

            return formattedFDate;

        }



        public string GetFormattedTDate()
        {
            // Get the date value from the DateTimePicker
            DateTime selectedTDate = dtpToDate.Value;

            // Format the date in MySQL date format (yyyy-MM-dd)
            string formattedTDate = selectedTDate.ToString("yyyy-MM-dd");

            return formattedTDate;

        }

        private void cboPaymod_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedPaymItem = cboPaymod.SelectedItem.ToString().Trim();
        }

        private void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            FromDt = dtpFromDate.Value.ToString("dd/MM/yyyy").Trim();
            FromDt_mysql = dtpFromDate.Value.ToString("yyyy-MM-dd").Trim();
        }

        private void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
            Todt = dtpToDate.Value.ToString("dd/MM/yyyy").Trim();
            Todt_mysql = dtpToDate.Value.ToString("yyyy-MM-dd").Trim();

        }

        private void frmR_invoice_wise_sale_rpt_FormClosed(object sender, FormClosedEventArgs e)
        {
            DeTools.DestroyToolbar(this);
            MainForm.Instance.mnuSinvoiceWiseSaleReportMenu.Enabled = true;
        }

        private void cboPaymod_SelectedValueChanged(object sender, EventArgs e)
        {
            selectedPaymItem = cboPaymod.SelectedItem.ToString().Trim();
        }

        private void chkGstdet_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGstdet.Checked)
            {
                Gstdetchecked_yn = "Y";
            }
            else
            {
                Gstdetchecked_yn = "N";
            }
        }

        private void chkItemGstDet_CheckedChanged(object sender, EventArgs e)
        {
            if (chkItemGstDet.Checked)
            {
                ItemGstdetchecked_yn = "Y";
            }
            else
            {
                ItemGstdetchecked_yn = "N";
            }
        }

        private void chkInvGstDet_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInvGstDet.Checked)
            {
                InvGstdetchecked_yn = "Y";
            }
            else
            {
                InvGstdetchecked_yn = "N";
            }
        }
    }
}
