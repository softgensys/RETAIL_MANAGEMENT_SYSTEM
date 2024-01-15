using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Google.Protobuf.Reflection.FieldOptions.Types;
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
        private decimal remainingAmount;
        private decimal currentAmount = 0;
        private bool paymatchflag = false;
        public string custName;
        public DbConnector dbConnector;
        public string custPhoneNo;
        public string custEmail;
        public string custAdd1;
        public string custAdd2;
        public string CustIDFromDatabase;
        decimal totalAmount;
        public string refund_amt;
        public string get_rowise_bar_code;
        public string get_rowise_netrt;
        //public string refund_amt;
        public bool saveflag = true;
        public string gen_invoice_no;
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
            try
            {

                dbConnector = new DbConnector();
                // dbConnector.connectionString= new OdbcConnection();
                dbConnector.connection = new OdbcConnection(dbConnector.connectionString);

                saveflag = true;
                //mblnSearch = true;

                bool blnItem_H, blnItem_D;
                int J;

                dbgItemDet.Update();
                //transaction = dbConnector.connection.BeginTransaction();

                DeTools.gstrSQL = "SELECT a.*, b.*, c.* FROM t_invoice_det a JOIN t_invoice_hdr b " +
                    "ON a.invoice_no = b.invoice_no JOIN t_invoice_pay_det c ON a.invoice_no = c.invoice_no " +
                    "WHERE a.invoice_no = '" + txtInvNo.Text.Trim() + "' AND b.invoice_no = '" + txtInvNo.Text.Trim() + "' + AND c.invoice_no = '" + txtInvNo.Text.Trim() + "'LIMIT 1; ";
                OdbcCommand cmd = new OdbcCommand(DeTools.gstrSQL, dbConnector.connection);
                dbConnector.connection.Open();

                OdbcDataReader reader = cmd.ExecuteReader();

                if (mblnSearch == false)
                {
                    //if (!CheckMandatoryFields())
                    //{
                    //    saveflag = false;
                    //}

                    //else
                    //{

                    blnItem_H = true;
                    string pnlusername = MainForm.Instance.pnlUserName.Text.Trim();
                    string machine_name = DeTools.fOSMachineName.GetMachineName();

                    // Check if the record with the specified Group_id exists
                    if (DeTools.GetMode(this) != DeTools.ADDMODE)
                    {
                        if (reader.HasRows)
                        {

                            if (DeTools.CheckTemporaryTableExists("t_invoice_hdr") != null)
                            {
                                if (DeTools.CheckTemporaryTableExists("t_invoice_det") != null)
                                {
                                    if (DeTools.CheckTemporaryTableExists("t_invoice_pay_det") != null)
                                    {

                                        // The record exists, so update it
                                        reader.Close();
                                        Cursor.Current = Cursors.WaitCursor;
                                        //---------------For Updation so We are entering in temp table while modifying
                                        string gstrSQL1 = "INSERT INTO temp_t_invoice_hdr (invoice_no, invoice_dt, branch_id, bill_time, cust_id," +
                                                            " sm_id, custname, custaddress, gross_amt, xmode, sr_no, sr_inv_no, sr_amt, disc_per, disc_amt, oth_amt," +
                                                            " net_amt_after_disc, round_off, net_amt, cash_id, notes, status, ent_on, ent_by, auth_on, auth_by, sale_type," +
                                                            " machine_id, o_amt, INV_TIME, machine_id_m, veh_no, po_no, open_yn, comp_name, mod_date, mod_by) " +
                                                            "Select invoice_no, invoice_dt, branch_id, bill_time, cust_id," +
                                                            " sm_id, custname, custaddress, gross_amt, xmode, sr_no, sr_inv_no, sr_amt, disc_per, disc_amt, oth_amt," +
                                                            " net_amt_after_disc, round_off, net_amt, cash_id, notes, status, ent_on, ent_by, auth_on, auth_by, sale_type," +
                                                            " machine_id, o_amt, INV_TIME, machine_id_m, veh_no, po_no, 'Y' AS open_yn, '" + DeTools.fOSMachineName.GetMachineName() + "' AS comp_name, mod_date, mod_by from t_invoice_hdr where invoice_no='" + txtInvNo.Text.Trim() + "';";

                                        using (OdbcCommand insertintotemphdr1 = new OdbcCommand(gstrSQL1, dbConnector.connection))
                                        {
                                            insertintotemphdr1.ExecuteNonQuery();
                                        }


                                        //--------Fetch to check if data inserted to temp table then delete from main table to send updated records
                                        string gstrSQL2 = "Select * from temp_t_invoice_hdr where invoice_no='" + txtInvNo.Text.Trim() + "' and open_yn='Y'";
                                        OdbcCommand selectintemp1 = new OdbcCommand(DeTools.gstrSQL, dbConnector.connection);

                                        OdbcDataReader selectread = selectintemp1.ExecuteReader();

                                        if (selectread.HasRows)
                                        {
                                            string delSQL = "Delete FROM t_invoice_hdr WHERE invoice_no = '" + txtInvNo.Text.Trim() + "'; ";

                                            using (OdbcCommand delfrmhdr1 = new OdbcCommand(delSQL, dbConnector.connection))
                                            {
                                                delfrmhdr1.ExecuteNonQuery();
                                            }

                                        }

                                        //--------Check data if its entered in temp table or not
                                        string gstrSql3 = "Select * from temp_t_invoice_hdr where invoice_no = '" + txtInvNo.Text.Trim() + "";

                                        OdbcCommand fetchtempdata1 = new OdbcCommand(gstrSql3);

                                        OdbcDataReader fetchread = fetchtempdata1.ExecuteReader();

                                        if (fetchread.HasRows)
                                        {
                                            string org_amt = fetchread["net_amt_after_disc"].ToString().Trim();
                                            string updatehdr = "UPDATE temp_t_invoice_hdr SET branch_id=?," +
                                                               " cust_id=?, sm_id=?, custname=?, custaddress=?, gross_amt=?, xmode='M'," +
                                                               " disc_per=?, disc_amt=?, oth_amt=?, net_amt_after_disc=?, round_off=?, net_amt=?, cash_id=?," +
                                                               " notes=?, status='V', sale_type=?, machine_id=?, o_amt=?, INV_TIME=?," +
                                                               " machine_id_m=?, veh_no=?, po_no=?, mod_date=?, mod_by=?, open_yn=?, comp_name=? WHERE invoice_no='" + txtInvNo.Text.Trim() + "' ;";

                                            cmd.CommandText = updatehdr;
                                            cmd.Parameters.Add(new OdbcParameter("branch_id", DeTools.strBranch));

                                            //-------------------------Cust combo----------------------------//
                                            //general.FillCombo(cboCust, "cust_id", "m_customer", false);
                                            if (reader["cust_id"] != DBNull.Value)
                                            {
                                                CustIDFromDatabase = reader["cust_id"].ToString().Trim();

                                                if (CustIDFromDatabase != "")
                                                {
                                                    // Find the item in the ComboBox's items collection
                                                    object selectedItem = cboCust.Items.Cast<object>().FirstOrDefault(item => item.ToString() == CustIDFromDatabase);

                                                    // Set the selected item if found
                                                    if (selectedItem != null)
                                                    {
                                                        cboCust.SelectedItem = selectedItem;
                                                        DataRow customerData = GetCustomerData("m_customer", "cust_id", "C", cboCust.SelectedItem.ToString().Trim());

                                                        if (customerData != null)
                                                        {
                                                            custName = customerData["cust_name"].ToString().Trim();
                                                            custPhoneNo = customerData["phone_1"].ToString().Trim();
                                                            custAdd1 = customerData["address1"].ToString().Trim();
                                                            custAdd2 = customerData["address2"].ToString().Trim();
                                                            custEmail = customerData["email"].ToString().Trim();
                                                        }


                                                        rotInvCust.Text = custName;
                                                        txtCustName.Text = custName;
                                                        txtAddress.Text = custAdd1 + custAdd2;
                                                    }



                                                }
                                            } // -end cust combo

                                            cmd.Parameters.Add(new OdbcParameter("cust_id", CustIDFromDatabase));
                                            cmd.Parameters.Add(new OdbcParameter("sm_id", ""));
                                            cmd.Parameters.Add(new OdbcParameter("custname", custName));
                                            cmd.Parameters.Add(new OdbcParameter("custaddress", custAdd1 + custAdd2));
                                            cmd.Parameters.Add(new OdbcParameter("gross_amt", rotGAmt.Text.Trim()));
                                            cmd.Parameters.Add(new OdbcParameter("disc_per", txtDiscPer.Text.Trim()));
                                            cmd.Parameters.Add(new OdbcParameter("disc_amt", txtDiscAmt.Text.Trim()));
                                            cmd.Parameters.Add(new OdbcParameter("oth_amt", ""));
                                            cmd.Parameters.Add(new OdbcParameter("net_amt_after_disc", rotPayAmt.Text.Trim()));
                                            cmd.Parameters.Add(new OdbcParameter("round_off", rotRO.Text.Trim()));
                                            cmd.Parameters.Add(new OdbcParameter("net_amt", rotNetAmt.Text.Trim()));
                                            cmd.Parameters.Add(new OdbcParameter("cash_id", pnlusername));
                                            cmd.Parameters.Add(new OdbcParameter("notes", ""));
                                            cmd.Parameters.Add(new OdbcParameter("sale_type", ""));
                                            cmd.Parameters.Add(new OdbcParameter("machine_id", machine_name));
                                            cmd.Parameters.Add(new OdbcParameter("o_amt", org_amt));
                                            cmd.Parameters.Add(new OdbcParameter("INV_TIME", rotBillTime.Text.Trim()));
                                            cmd.Parameters.Add(new OdbcParameter("machine_id_m", machine_name));
                                            cmd.Parameters.Add(new OdbcParameter("veh_no", ""));
                                            cmd.Parameters.Add(new OdbcParameter("po_no", ""));
                                            cmd.Parameters.Add(new OdbcParameter("mod_date", OdbcType.DateTime)).Value = DateTime.Now;
                                            cmd.Parameters.Add(new OdbcParameter("mod_by", DeTools.gstrloginId));
                                            cmd.Parameters.Add(new OdbcParameter("open_yn", "Y"));
                                            cmd.Parameters.Add(new OdbcParameter("comp_name", machine_name));

                                            cmd.ExecuteNonQuery();

                                            Cursor.Current = Cursors.Default;
                                            //reader.Close();
                                            fetchread.Close();

                                            //---Inserting updated temp hdr data to main table

                                            string InsertTempTohdr = "Insert into t_invoice_hdr(invoice_no, invoice_dt, branch_id, bill_time, cust_id, sm_id, custname, custaddress, gross_amt, xmode," +
                                                                     "disc_per, disc_amt, oth_amt, net_amt_after_disc, round_off, net_amt, cash_id," +
                                                                     "notes, status, ent_on, ent_by, auth_on, auth_by, sale_type, machine_id, o_amt, INV_TIME," +
                                                                     "machine_id_m, veh_no, po_no, mod_date, mod_by) " +
                                                                     "Select invoice_no, invoice_dt, branch_id, bill_time, cust_id, sm_id, custname, custaddress, gross_amt, xmode," +
                                                                     "disc_per, disc_amt, oth_amt, net_amt_after_disc, round_off, net_amt, cash_id," +
                                                                     "notes, status, ent_on, ent_by, auth_on, auth_by, sale_type, machine_id, o_amt, INV_TIME," +
                                                                     "machine_id_m, veh_no, po_no, mod_date, mod_by from temp_t_invoice_hdr where invoice_no =? and open_yn='Y' order by mod_date desc limit 1;";

                                            using (OdbcCommand insertCmd = new OdbcCommand(InsertTempTohdr, dbConnector.connection))
                                            {
                                                insertCmd.Parameters.Add(new OdbcParameter("invoice_no", txtInvNo.Text.ToString().Trim()));
                                                insertCmd.ExecuteNonQuery();

                                            }

                                            string querupdN1 = "update temp_t_invoice_hdr set open_yn='N' where invoice_no=? order by mod_date desc limit 1";

                                            using (OdbcCommand querupdNCmd = new OdbcCommand(querupdN1, dbConnector.connection))
                                            {
                                                querupdNCmd.Parameters.Add(new OdbcParameter("invoice_no", txtInvNo.Text.ToString().Trim()));
                                                querupdNCmd.ExecuteNonQuery();

                                            }

                                            string querdel1 = "delete from temp_t_invoice_hdr where invoice_no=? order by mod_date desc limit 1";

                                            using (OdbcCommand querdelCmd = new OdbcCommand(querdel1, dbConnector.connection))
                                            {
                                                querdelCmd.Parameters.Add(new OdbcParameter("invoice_no", txtInvNo.Text.ToString().Trim()));
                                                querdelCmd.ExecuteNonQuery();

                                            }


                                        }

                                    }//===================hdr updation complete=========================================

                                    //---------------For Updation so We are entering in temp det table while modifying
                                    string gstrSQLdet = "INSERT INTO temp_t_invoice_det (invoice_no, invoice_dt, branch_id, item_id, bar_code, item_sl_no, qty, mrp, sale_price," +
                                                        " disc_per, disc_amt, sale_tax_per, sale_tax_amt, net_amt, free_item_id, free_item_qty, free_amt, pur_rate, cess_perc," +
                                                        " cess_amt, excis_perc, excis_amt, group_id, mod_date, mod_by) " +
                                                        "Select invoice_no, invoice_dt, branch_id, item_id, bar_code, item_sl_no, qty, mrp, sale_price," +
                                                        " disc_per, disc_amt, sale_tax_per, sale_tax_amt, net_amt, free_item_id, free_item_qty, free_amt, pur_rate, cess_perc," +
                                                        " cess_amt, excis_perc, excis_amt, group_id, mod_date, mod_by, 'Y' AS open_yn, '" + DeTools.fOSMachineName.GetMachineName() + "' AS comp_name, mod_date, mod_by from t_invoice_det where invoice_no='" + txtInvNo.Text.Trim() + "';";

                                    using (OdbcCommand insertintotempdet1 = new OdbcCommand(gstrSQLdet, dbConnector.connection))
                                    {
                                        insertintotempdet1.ExecuteNonQuery();
                                    }
                                    //--------Fetch to check if data inserted to temp table then delete from main table to send updated records
                                    string gstrSQLdet2 = "Select * from temp_t_invoice_det where invoice_no='" + txtInvNo.Text.Trim() + "' and open_yn='Y'";
                                    OdbcCommand selectintempdet1 = new OdbcCommand(gstrSQLdet2, dbConnector.connection);

                                    OdbcDataReader selectdetread = selectintempdet1.ExecuteReader();

                                    if (selectdetread.HasRows)
                                    {
                                        string delSQLdet = "Delete FROM t_invoice_det WHERE invoice_no = '" + txtInvNo.Text.Trim() + "'; ";

                                        using (OdbcCommand delfrmdet1 = new OdbcCommand(delSQLdet, dbConnector.connection))
                                        {
                                            delfrmdet1.ExecuteNonQuery();
                                        }

                                    }
                                    selectdetread.Close();

                                    //--------Check data if its entered in temp table or not
                                    string gstrSqldet3 = "Select * from temp_t_invoice_det where invoice_no = '" + txtInvNo.Text.Trim() + " and open_yn= 'Y'";

                                    OdbcCommand fetchtempdatadet1 = new OdbcCommand(gstrSqldet3);

                                    OdbcDataReader fetchreaddet = fetchtempdatadet1.ExecuteReader();

                                    if (fetchreaddet.HasRows)
                                    {
                                        //string org_amt = fetchreaddet["net_amt_after_disc"].ToString().Trim();
                                        string updatedet = "UPDATE temp_t_invoice_det SET branch_id=@branch_id, item_id=@item_id, bar_code=@bar_code," +
                                                           " item_sl_no=@item_sl_no, qty=@qty, mrp=@mrp, sale_price=@sale_price, disc_per=@disc_per, disc_amt=@disc_amt, sale_tax_per=@sale_tax_per, sale_tax_amt=@sale_tax_amt," +
                                                           " net_amt=@net_amt, free_item_id=@free_item_id, free_item_qty=@free_item_qty, free_amt=@free_amt, pur_rate=@pur_rate, cess_perc=@cess_perc, cess_amt=@cess_amt, excis_perc=@excis_perc," +
                                                           " excis_amt=@excis_amt, group_id=@group_id, mod_date=@mod_date, mod_by=@mod_by,open_yn=@open_yn, comp_name=@comp_name WHERE invoice_no='" + txtInvNo.Text.Trim() + "' ;";

                                        cmd.CommandText = updatedet;
                                        //cmd.Parameters.Add(new OdbcParameter("branch_id", DeTools.strBranch));


                                        foreach (DataGridViewRow row in dbgItemDet.Rows)
                                        {
                                            // Set parameters for the current row

                                            //------get net rate----------------//
                                            get_rowise_bar_code = row.Cells[1].Value.ToString();
                                            string get_netrate_from_item_mas = "SELECT a.net_rate, b.group_id FROM m_item_det a LEFT JOIN m_item_hdr b" +
                                                                               " ON a.item_id = b.item_id WHERE (a.plu = '" + get_rowise_bar_code.Trim() + "' OR a.bar_code = '" + get_rowise_bar_code.Trim() + "')" +
                                                                               " AND a.item_id = b.item_id;";


                                            OdbcCommand fetchnetrt = new OdbcCommand(get_netrate_from_item_mas, dbConnector.connection);

                                            OdbcDataReader fetchreadnetrt = fetchnetrt.ExecuteReader();


                                            cmd.Parameters["@branch_id"].Value = DeTools.strBranch;
                                            cmd.Parameters["@item_id"].Value = string.IsNullOrEmpty(row.Cells["Itemid"].Value.ToString()) ? "" : row.Cells["Itemid"].Value.ToString();
                                            cmd.Parameters["@bar_code"].Value = string.IsNullOrEmpty(row.Cells[1].Value.ToString()) ? "0" : row.Cells[1].Value.ToString();
                                            cmd.Parameters["@item_sl_no"].Value = row.Cells[0].Value.ToString();
                                            cmd.Parameters["@qty"].Value = string.IsNullOrEmpty(row.Cells[3].Value.ToString()) ? "0.00" : row.Cells[3].Value.ToString();
                                            cmd.Parameters["@mrp"].Value = string.IsNullOrEmpty(row.Cells[4].Value.ToString()) ? "0.00" : row.Cells[4].Value.ToString();
                                            cmd.Parameters["@sale_price"].Value = string.IsNullOrEmpty(row.Cells[5].Value.ToString()) ? "0.00" : row.Cells[5].Value.ToString();
                                            cmd.Parameters["@disc_per"].Value = string.IsNullOrEmpty(row.Cells[6].Value.ToString()) ? "0.00" : row.Cells[6].Value.ToString();
                                            cmd.Parameters["@disc_amt"].Value = string.IsNullOrEmpty(row.Cells[7].Value.ToString()) ? "0.00" : row.Cells[7].Value.ToString();
                                            cmd.Parameters["@sale_tax_per"].Value = string.IsNullOrEmpty(row.Cells[8].Value.ToString()) ? "0.00" : row.Cells[8].Value.ToString();
                                            cmd.Parameters["@sale_tax_amt"].Value = string.IsNullOrEmpty(row.Cells["GstAmt"].Value.ToString()) ? "0.00" : row.Cells["GstAmt"].Value.ToString();
                                            cmd.Parameters["@net_amt"].Value = row.Cells[10].Value.ToString();
                                            cmd.Parameters["@free_item_id"].Value = "";
                                            cmd.Parameters["@free_item_qty"].Value = "";
                                            cmd.Parameters["@free_amt"].Value = "";
                                            cmd.Parameters["@pur_rate"].Value = string.IsNullOrEmpty(fetchreadnetrt["net_rate"].ToString().Trim()) ? "" : fetchreadnetrt["net_rate"].ToString().Trim();
                                            cmd.Parameters["@cess_perc"].Value = string.IsNullOrEmpty(row.Cells[9].Value.ToString().Trim()) ? "" : row.Cells[9].Value.ToString().Trim();
                                            cmd.Parameters["@cess_amt"].Value = string.IsNullOrEmpty(row.Cells["CessAmt"].Value.ToString().Trim()) ? "" : row.Cells["CessAmt"].Value.ToString().Trim();
                                            cmd.Parameters["@excis_perc"].Value = "";
                                            cmd.Parameters["@excis_amt"].Value = "";
                                            cmd.Parameters["@group_id"].Value = string.IsNullOrEmpty(fetchreadnetrt["group_id"].ToString().Trim()) ? "" : fetchreadnetrt["group_id"].ToString().Trim(); ;
                                            cmd.Parameters.Add(new OdbcParameter("@mod_date", OdbcType.DateTime)).Value = DateTime.Now;
                                            cmd.Parameters["@mod_by"].Value = DeTools.gstrloginId;
                                            cmd.Parameters["@open_yn"].Value = "Y";
                                            cmd.Parameters["@comp_name"].Value = machine_name;


                                            cmd.ExecuteNonQuery();

                                            fetchreadnetrt.Close();
                                        }

                                        Cursor.Current = Cursors.Default;
                                        //reader.Close();


                                        //---Inserting updated temp hdr data to main table

                                        string InsertTempTodet = "Insert into t_invoice_det(invoice_no, invoice_dt, branch_id, item_id, bar_code," +
                                                                 " item_sl_no, qty, mrp, sale_price, disc_per, disc_amt, sale_tax_per, sale_tax_amt," +
                                                                 " net_amt, free_item_id, free_item_qty, free_amt, pur_rate, cess_perc, cess_amt, excis_perc=@excis_perc," +
                                                                 " excis_amt, group_id, mod_date, mod_by) " +
                                                                 "Select invoice_no, invoice_dt, branch_id, item_id, bar_code," +
                                                                 " item_sl_no, qty, mrp, sale_price, disc_per, disc_amt, sale_tax_per, sale_tax_amt," +
                                                                 " net_amt, free_item_id, free_item_qty, free_amt, pur_rate, cess_perc, cess_amt, excis_perc=@excis_perc," +
                                                                 " excis_amt, group_id, mod_date, mod_by from temp_t_invoice_det where invoice_no =? and open_yn='Y';";

                                        using (OdbcCommand insertCmddet = new OdbcCommand(InsertTempTodet, dbConnector.connection))
                                        {
                                            insertCmddet.Parameters.Add(new OdbcParameter("invoice_no", txtInvNo.Text.ToString().Trim()));
                                            insertCmddet.ExecuteNonQuery();

                                        }


                                        string querupdN1 = "update temp_t_invoice_det set open_yn='N' where invoice_no=?";

                                        using (OdbcCommand querupdNCmd = new OdbcCommand(querupdN1, dbConnector.connection))
                                        {
                                            querupdNCmd.Parameters.Add(new OdbcParameter("invoice_no", txtInvNo.Text.ToString().Trim()));
                                            querupdNCmd.ExecuteNonQuery();

                                        }

                                        string querdel1 = "delete from temp_t_invoice_det where invoice_no=?";

                                        using (OdbcCommand querdelCmd = new OdbcCommand(querdel1, dbConnector.connection))
                                        {
                                            querdelCmd.Parameters.Add(new OdbcParameter("invoice_no", txtInvNo.Text.ToString().Trim()));
                                            querdelCmd.ExecuteNonQuery();

                                        }


                                    }
                                    fetchreaddet.Close();
                                    Cursor.Current = Cursors.WaitCursor;
                                    Messages.SavingMsg();
                                } //======================end for update in invoice det============

                                //---------------For Updation so We are entering in temp pay det table while modifying
                                string gstrSQLpaydet = "INSERT INTO temp_t_invoice_pay_det (invoice_no, pay_mode_id, branch_id, pay_amt, cash_t_amt, ref_amt, cc_code, cc_no, coup_id, cust_id," +
                                                    "bank_name, cheque_no, cheque_dt, gv_no, gv_amt, mod_date, mod_by) " +
                                                    "Select invoice_no, pay_mode_id, branch_id, pay_amt, cash_t_amt, ref_amt, cc_code, cc_no, coup_id, cust_id," +
                                                    "bank_name, cheque_no, cheque_dt, gv_no, gv_amt, mod_date, mod_by, 'Y' AS open_yn, '" + DeTools.fOSMachineName.GetMachineName() + "' AS comp_name, mod_date, mod_by from t_invoice_pay_det where invoice_no='" + txtInvNo.Text.Trim() + "';";

                                using (OdbcCommand insertintotemppaydet1 = new OdbcCommand(gstrSQLpaydet, dbConnector.connection))
                                {
                                    insertintotemppaydet1.ExecuteNonQuery();
                                }
                                //--------Fetch to check if data inserted to temp table then delete from main table to send updated records
                                string gstrSQLpaydet2 = "Select * from temp_t_invoice_pay_det where invoice_no='" + txtInvNo.Text.Trim() + "' and open_yn='Y'";
                                OdbcCommand selectintemppaydet1 = new OdbcCommand(gstrSQLpaydet2, dbConnector.connection);

                                OdbcDataReader selectpaydetread = selectintemppaydet1.ExecuteReader();

                                if (selectpaydetread.HasRows)
                                {
                                    string delSQLpaydet = "Delete FROM t_invoice_pay_det WHERE invoice_no = '" + txtInvNo.Text.Trim() + "'; ";

                                    using (OdbcCommand delfrmpaydet1 = new OdbcCommand(delSQLpaydet, dbConnector.connection))
                                    {
                                        delfrmpaydet1.ExecuteNonQuery();
                                    }

                                }
                                selectpaydetread.Close();

                                //--------Check data if its entered in temp table or not
                                string gstrSqlpaydet3 = "Select * from temp_t_invoice_pay_det where invoice_no = '" + txtInvNo.Text.Trim() + " and open_yn= 'Y'";

                                OdbcCommand fetchtempdatapaydet1 = new OdbcCommand(gstrSqlpaydet3);

                                OdbcDataReader fetchreadpaydet = fetchtempdatapaydet1.ExecuteReader();

                                if (fetchreadpaydet.HasRows)
                                {
                                    //string org_amt = fetchreaddet["net_amt_after_disc"].ToString().Trim();
                                    string updatepaydet = "UPDATE temp_t_invoice_pay_det SET invoice_no=@invoice_no, pay_mode_id=@pay_mode_id, branch_id=@branch_id, pay_amt=@pay_amt, cash_t_amt=@cash_t_amt, ref_amt=@ref_amt," +
                                                       " cc_code=@cc_code, cc_no=@cc_no, coup_id=@coup_id, cust_id=@cust_id," +
                                                       "bank_name=@bank_name, cheque_no=@cheque_no, cheque_dt=@cheque_dt, gv_no=@gv_no, gv_amt=@gv_amt, mod_date=@mod_date, mod_by=@mod_by, mod_date =@mod_date, mod_by=@mod_by," +
                                                       "open_yn=@open_yn, comp_name=@comp_name WHERE invoice_no='" + txtInvNo.Text.Trim() + "' ;";

                                    cmd.CommandText = updatepaydet;
                                    //cmd.Parameters.Add(new OdbcParameter("branch_id", DeTools.strBranch));


                                    foreach (DataGridViewRow row in dbgPayDet.Rows)
                                    {

                                        // Assuming the column at index 1 is your ComboBox column
                                        DataGridViewComboBoxCell comboBoxCell = row.Cells[1] as DataGridViewComboBoxCell;

                                        // Check if the ComboBox cell is not null
                                        if (comboBoxCell != null)
                                        {
                                            // Get the selected value from the ComboBox
                                            string selectedValue = comboBoxCell.Value?.ToString();

                                            // Set the parameter value
                                            cmd.Parameters["@pay_mode_id"].Value = selectedValue;
                                        }
                                        else if (comboBoxCell == null)
                                        {
                                            MessageBox.Show("Pls Select Payment Mode First!", "Select Payment Mode!!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                            break;
                                        }

                                        cmd.Parameters["@branch_id"].Value = DeTools.strBranch;

                                        string payamt = row.Cells[2].Value.ToString();
                                        if (!string.IsNullOrEmpty(payamt))
                                        {
                                            cmd.Parameters["@pay_amt"].Value = row.Cells[2].Value.ToString();
                                        }
                                        else if (string.IsNullOrEmpty(payamt))
                                        {
                                            MessageBox.Show("Pls Select Payment Mode First!", "Select Payment Mode!!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                            break;
                                        }

                                        cmd.Parameters["@cash_t_amt"].Value = row.Cells[3].Value.ToString().Trim();

                                        if (comboBoxCell != null)
                                        {
                                            // Get the selected value from the ComboBox
                                            string selectedValue = comboBoxCell.Value?.ToString();

                                            // Set the parameter value
                                            if (selectedValue == "CASH" && !string.IsNullOrEmpty(row.Cells[3].Value.ToString().Trim()) || row.Cells[3].Value.ToString().Trim() != "0")
                                            {
                                                cmd.Parameters["@ref_amt"].Value = Math.Round(decimal.Parse(row.Cells[3].Value.ToString().Trim()) - decimal.Parse(row.Cells[2].Value.ToString().Trim()), 0);

                                            }
                                            else
                                            {

                                                MessageBox.Show("Pls Enter Cash Tend First!", "Enter Cash Tend!!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                                break;
                                            }
                                        }
                                        else if (comboBoxCell == null)
                                        {
                                            MessageBox.Show("Pls Select Payment Mode First!", "Select Payment Mode!!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                            break;
                                        }

                                        if (comboBoxCell != null)
                                        {
                                            // Get the selected value from the ComboBox
                                            string selectedValue = comboBoxCell.Value?.ToString();

                                            // Set the parameter value
                                            if (selectedValue == "CC")
                                            {
                                                DataGridViewComboBoxCell comboBoxCellcc = row.Cells[4] as DataGridViewComboBoxCell;
                                                string selectedvalcc = comboBoxCellcc.Value?.ToString();

                                                if (!string.IsNullOrEmpty(selectedvalcc))
                                                {
                                                    cmd.Parameters["@cc_code"].Value = selectedvalcc;

                                                }
                                                else
                                                {
                                                    MessageBox.Show("Pls Select CC Code First!", "Select CC Code!!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                                    break;
                                                }

                                            }

                                        }
                                        else if (comboBoxCell == null)
                                        {
                                            MessageBox.Show("Pls Select Payment Mode First!", "Select Payment Mode!!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                            break;
                                        }

                                        cmd.Parameters["@cc_no"].Value = string.IsNullOrEmpty(row.Cells[5].Value.ToString()) ? "0" : row.Cells[5].Value.ToString();

                                        DataGridViewComboBoxCell comboBoxCellcoup = row.Cells[6] as DataGridViewComboBoxCell;
                                        string selectedvalcoup = comboBoxCellcoup.Value?.ToString();

                                        if (!string.IsNullOrEmpty(selectedvalcoup))
                                        {
                                            cmd.Parameters["@coup_id"].Value = selectedvalcoup;

                                        }
                                        else
                                        {
                                            cmd.Parameters["@coup_id"].Value = "";
                                        }

                                        if (comboBoxCell != null)
                                        {
                                            // Get the selected value from the ComboBox
                                            string selectedValue = comboBoxCell.Value?.ToString();

                                            // Set the parameter value
                                            if (selectedValue == "CR")
                                            {
                                                DataGridViewComboBoxCell comboBoxCellcust = row.Cells[7] as DataGridViewComboBoxCell;
                                                string selectedvalcust = comboBoxCellcust.Value?.ToString();

                                                if (!string.IsNullOrEmpty(selectedvalcust))
                                                {
                                                    cmd.Parameters["@cust_id"].Value = selectedvalcust;

                                                }
                                                else if (!string.IsNullOrEmpty(row.Cells[7].Value.ToString().Trim()))
                                                {
                                                    cmd.Parameters["@cust_id"].Value = row.Cells[7].Value.ToString().Trim();
                                                }
                                                else if (string.IsNullOrEmpty(row.Cells[7].Value.ToString().Trim()) || string.IsNullOrEmpty(selectedvalcust))
                                                {
                                                    MessageBox.Show("Pls Select Customer Id First!", "Select Customer Id!!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                                    break;
                                                }

                                            }

                                        }
                                        else if (comboBoxCell == null)
                                        {
                                            MessageBox.Show("Pls Select Payment Mode First!", "Select Payment Mode!!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                            break;
                                        }

                                        cmd.Parameters["@bank_name"].Value = string.IsNullOrEmpty(row.Cells[8].Value.ToString()) ? "" : row.Cells[8].Value.ToString();
                                        cmd.Parameters["@cheque_no"].Value = string.IsNullOrEmpty(row.Cells[9].Value.ToString()) ? "" : row.Cells[9].Value.ToString();
                                        cmd.Parameters["@cheque_dt"].Value = string.IsNullOrEmpty(row.Cells[10].Value.ToString()) ? "" : row.Cells[10].Value.ToString();
                                        cmd.Parameters["@gv_no"].Value = string.IsNullOrEmpty(row.Cells[11].Value.ToString()) ? "" : row.Cells[11].Value.ToString();
                                        cmd.Parameters["@gv_amt"].Value = string.IsNullOrEmpty(row.Cells[12].Value.ToString()) ? "" : row.Cells[12].Value.ToString();

                                        cmd.Parameters.Add(new OdbcParameter("@mod_date", OdbcType.DateTime)).Value = DateTime.Now;
                                        cmd.Parameters["@mod_by"].Value = DeTools.gstrloginId;
                                        cmd.Parameters["@open_yn"].Value = "Y";
                                        cmd.Parameters["@comp_name"].Value = machine_name;


                                        cmd.ExecuteNonQuery();


                                    }

                                    Cursor.Current = Cursors.Default;
                                    //reader.Close();


                                    //---Inserting updated temp hdr data to main table

                                    string InsertTempTopaydet = "Insert into t_invoice_pay_det(  invoice_no, pay_mode_id, branch_id, pay_amt, cash_t_amt, ref_amt, cc_code, cc_no, coup_id, cust_id," +
                                                                "bank_name, cheque_no, cheque_dt, gv_no, gv_amt, mod_date, mod_by) " +
                                                             "Select invoice_no, pay_mode_id, branch_id, pay_amt, cash_t_amt, ref_amt, cc_code, cc_no, coup_id, cust_id," +
                                                                "bank_name, cheque_no, cheque_dt, gv_no, gv_amt, mod_date, mod_by from temp_t_invoice_pay_det where invoice_no =? and open_yn='Y';";

                                    using (OdbcCommand insertCmdpaydet = new OdbcCommand(InsertTempTopaydet, dbConnector.connection))
                                    {
                                        insertCmdpaydet.Parameters.Add(new OdbcParameter("invoice_no", txtInvNo.Text.ToString().Trim()));
                                        insertCmdpaydet.ExecuteNonQuery();

                                    }


                                    string querupdNpay1 = "update temp_t_invoice_det set open_yn='N' where invoice_no=?";

                                    using (OdbcCommand querupdNpayCmd = new OdbcCommand(querupdNpay1, dbConnector.connection))
                                    {
                                        querupdNpayCmd.Parameters.Add(new OdbcParameter("invoice_no", txtInvNo.Text.ToString().Trim()));
                                        querupdNpayCmd.ExecuteNonQuery();

                                    }

                                    string querdelpay1 = "delete from temp_t_invoice_det where invoice_no=?";

                                    using (OdbcCommand querdelpayCmd = new OdbcCommand(querdelpay1, dbConnector.connection))
                                    {
                                        querdelpayCmd.Parameters.Add(new OdbcParameter("invoice_no", txtInvNo.Text.ToString().Trim()));
                                        querdelpayCmd.ExecuteNonQuery();

                                    }


                                }
                                fetchreadpaydet.Close();
                                Messages.SavedMsg();

                            }
                        }
                        reader.Close();
                    }


                }  //-------End of If//===========******End of Update Now Add*******==============

                else if (mblnSearch == true)
                {
                    if (strMode != string.Empty && saveflag == true)
                    {
                        gen_invoice_no = General.GenMDocno("INV").ToString().Trim();
                        if (gen_invoice_no.Length == 0)
                        {
                            gen_invoice_no = "";
                            string gstrMsg = "Document series for Item Serial Generation. exhausted or not available. Item cannot be saved.";
                            Messages.ErrorMsg(gstrMsg);
                            saveflag = false;
                        }

                    }

                    string pnlusername = MainForm.Instance.pnlUserName.Text.Trim();
                    string machine_name = DeTools.fOSMachineName.GetMachineName();

                    if (DeTools.CheckTemporaryTableExists("t_invoice_hdr") != null)
                    {
                        if (DeTools.CheckTemporaryTableExists("t_invoice_det") != null)
                        {
                            if (DeTools.CheckTemporaryTableExists("t_invoice_pay_det") != null)
                            {
                                Cursor.Current = Cursors.WaitCursor;

                                string inserthdr = "INSERT INTO temp_t_invoice_hdr (invoice_no, invoice_dt, branch_id, bill_time, cust_id, sm_id, custname, custaddress, gross_amt, xmode," +
                                                   "disc_per, disc_amt, oth_amt, net_amt_after_disc, round_off, net_amt, cash_id," +
                                                   "notes, status, ent_on, ent_by, auth_on, auth_by, sale_type, machine_id, o_amt, INV_TIME," +
                                                   " veh_no, po_no, open_yn, comp_name) VALUES" +
                                                   " (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                                OdbcCommand cmddhdr = new OdbcCommand(inserthdr, dbConnector.connection);
                                // cmd.Transaction = transaction;



                                cmddhdr.CommandText = inserthdr;

                                string chk_cls_day = "select cls_dt from r_closing where doc_type_id='INV' order by cls_dt desc limit 1;";
                                using (OdbcDataReader chk_cls_day_read = dbConnector.CreateResultset(chk_cls_day))
                                {
                                    string closedt;

                                    if (chk_cls_day_read.HasRows)
                                    {
                                        if (!string.IsNullOrEmpty(chk_cls_day_read["cls_dt"].ToString().Trim()))
                                        {
                                            closedt = chk_cls_day_read["cls_dt"].ToString().Trim();

                                        }
                                       
                                    }
                                }

                                cmddhdr.Parameters.Add(new OdbcParameter("invoice_no", gen_invoice_no.Trim()));
                                cmddhdr.Parameters.Add(new OdbcParameter("invoice_dt", dtpInvDate));
                                cmddhdr.Parameters.Add(new OdbcParameter("branch_id", DeTools.strBranch.Trim()));
                                cmddhdr.Parameters.Add(new OdbcParameter("bill_time", rotBillTime));
                                //-------------------------Cust combo----------------------------//
                                //general.FillCombo(cboCust, "cust_id", "m_customer", false);

                                

                                if (!string.IsNullOrEmpty(cboCust.SelectedItem.ToString().Trim()))
                                {
                                    CustIDFromDatabase = cboCust.SelectedItem.ToString().Trim();

                                    if (CustIDFromDatabase != "")
                                    {
                                        
                                            DataRow customerData = GetCustomerData("m_customer", "cust_id", "C", CustIDFromDatabase);

                                            if (customerData != null)
                                            {
                                                custName = customerData["cust_name"].ToString().Trim();
                                                custPhoneNo = customerData["phone_1"].ToString().Trim();
                                                custAdd1 = customerData["address1"].ToString().Trim();
                                                custAdd2 = customerData["address2"].ToString().Trim();
                                                custEmail = customerData["email"].ToString().Trim();
                                            }                                            

                                    }
                                

                                }

                                cmddhdr.Parameters.Add(new OdbcParameter("cust_id", CustIDFromDatabase));
                                cmddhdr.Parameters.Add(new OdbcParameter("sm_id", ""));
                                cmddhdr.Parameters.Add(new OdbcParameter("custname", custName));
                                cmddhdr.Parameters.Add(new OdbcParameter("custaddress", custAdd1 + custAdd2));
                                cmddhdr.Parameters.Add(new OdbcParameter("gross_amt", rotGAmt.Text.Trim()));
                                cmddhdr.Parameters.Add(new OdbcParameter("disc_per", txtDiscPer.Text.Trim()));
                                cmddhdr.Parameters.Add(new OdbcParameter("disc_amt", txtDiscAmt.Text.Trim()));
                                cmddhdr.Parameters.Add(new OdbcParameter("oth_amt", ""));
                                cmddhdr.Parameters.Add(new OdbcParameter("net_amt_after_disc", rotPayAmt.Text.Trim()));
                                cmddhdr.Parameters.Add(new OdbcParameter("round_off", rotRO.Text.Trim()));
                                cmddhdr.Parameters.Add(new OdbcParameter("net_amt", rotNetAmt.Text.Trim()));
                                cmddhdr.Parameters.Add(new OdbcParameter("cash_id", pnlusername));
                                cmddhdr.Parameters.Add(new OdbcParameter("notes", ""));
                                cmddhdr.Parameters.Add(new OdbcParameter("status", "V"));
                                cmddhdr.Parameters.Add(new OdbcParameter("ent_on", OdbcType.DateTime)).Value = DateTime.Now;
                                cmddhdr.Parameters.Add(new OdbcParameter("ent_by", DeTools.gstrloginId));
                                cmddhdr.Parameters.Add(new OdbcParameter("auth_on", ""));
                                cmddhdr.Parameters.Add(new OdbcParameter("auth_by", ""));
                                cmddhdr.Parameters.Add(new OdbcParameter("sale_type", ""));
                                cmddhdr.Parameters.Add(new OdbcParameter("machine_id", machine_name));
                                cmddhdr.Parameters.Add(new OdbcParameter("o_amt", rotPayAmt.Text.Trim()));
                                cmddhdr.Parameters.Add(new OdbcParameter("INV_TIME", rotBillTime.Text.Trim()));                                
                                cmddhdr.Parameters.Add(new OdbcParameter("veh_no", ""));
                                cmddhdr.Parameters.Add(new OdbcParameter("po_no", ""));
                                cmddhdr.Parameters.Add(new OdbcParameter("open_yn", "Y"));
                                cmddhdr.Parameters.Add(new OdbcParameter("comp_name", machine_name));

                                cmd.ExecuteNonQuery();

                                Cursor.Current = Cursors.Default;

                            }
                        }
                    }
                }//===========================End of ADD================================

            }//-------try over---------------
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }
        }

        public void SearchForm()
        {

        }

        public void UnsavedData()
        {
            DbConnector dbConnector = new DbConnector();


            try
            {
                dbConnector.OpenConnection();
                string compname = DeTools.fOSMachineName.GetMachineName();
                string user = MainForm.Instance.pnlUserName.Text.Trim();
                string matchbranchinv = "SELECT b.invoice_no FROM temp_t_invoice_hdr b WHERE b.open_yn='Y' and b.ent_by='" + user.Trim() + "' and b.comp_name='" + compname.Trim() + "' and b.branch_id='" + DeTools.strBranch.Trim() + "' order by ent_on desc;";
                OdbcParameter[] parameters_match_branchinv = new OdbcParameter[0];
                using (OdbcDataReader readerinv = dbConnector.ExecuteReader(matchbranchinv, parameters_match_branchinv))
                {
                    if (readerinv.Read())
                    {


                        if (DeTools.CheckTemporaryTableExists("t_invoice_hdr") != null)
                        {
                            if (DeTools.CheckTemporaryTableExists("t_invoice_det") != null)
                            {
                                if (DeTools.CheckTemporaryTableExists("t_invoice_pay_det") != null)
                                {


                                    saveflag = true;
                                    string querypaydet = "SELECT a.* FROM temp_t_invoice_pay_det a, temp_t_invoice_hdr b WHERE a.open_yn='Y' AND b.ent_by=? AND a.comp_name=? AND a.invoice_no=? ORDER BY ent_on DESC;";

                                    OdbcParameter[] parameterspaydet = new OdbcParameter[3];
                                    parameterspaydet[0] = new OdbcParameter("ent_by", user.Trim());
                                    parameterspaydet[1] = new OdbcParameter("comp_name", compname.Trim());
                                    parameterspaydet[2] = new OdbcParameter("invoice_no", readerinv["invoice_no"]);



                                    using (OdbcDataReader reader = dbConnector.ExecuteReader(querypaydet, parameterspaydet))
                                    {
                                        if (reader.HasRows)
                                        {
                                            Messages.UnsavedMsg(null);
                                            //txtInvNo.Text = reader["invoice_no"].ToString().Trim();
                                            while (reader.Read())
                                            {
                                                // Check if the row already exists based on a condition (e.g., invoice_no)
                                                DataGridViewRow existingRow = null;

                                                foreach (DataGridViewRow row in dbgPayDet.Rows)
                                                {
                                                    // Update values in the existing row
                                                    string payModeValue = reader["pay_mode_id"].ToString().Trim();
                                                    // Assuming the column index is 1
                                                    DataGridViewComboBoxCell comboBoxCell = existingRow.Cells[1] as DataGridViewComboBoxCell;

                                                    if (comboBoxCell != null && comboBoxCell.Items.Contains(payModeValue))
                                                    {
                                                        comboBoxCell.Value = payModeValue;
                                                    }
                                                    existingRow.Cells[1].Value = reader["pay_mode_id"].ToString().Trim();
                                                    existingRow.Cells[2].Value = reader["pay_amt"].ToString().Trim();
                                                    existingRow.Cells[3].Value = reader["cash_t_amt"].ToString().Trim();

                                                    string ccodeval = reader["cc_code"].ToString().Trim();
                                                    DataGridViewComboBoxCell comboBoxCell4 = existingRow.Cells[4] as DataGridViewComboBoxCell;

                                                    if (comboBoxCell4 != null && comboBoxCell4.Items.Contains(ccodeval))
                                                    {
                                                        comboBoxCell4.Value = ccodeval;
                                                    }
                                                    existingRow.Cells[5].Value = reader["cc_no"].ToString().Trim();

                                                    string coupval = reader["coup_id"].ToString().Trim();
                                                    DataGridViewComboBoxCell comboBoxCell6 = existingRow.Cells[6] as DataGridViewComboBoxCell;

                                                    if (comboBoxCell6 != null && comboBoxCell6.Items.Contains(coupval))
                                                    {
                                                        comboBoxCell6.Value = coupval;
                                                    }

                                                    //existingRow.Cells[7].Value = reader["cust_id"].ToString().Trim();
                                                    existingRow.Cells[8].Value = reader["bank_name"].ToString().Trim();
                                                    existingRow.Cells[9].Value = reader["cheque_no"].ToString().Trim();
                                                    existingRow.Cells[10].Value = reader["cheque_dt"].ToString().Trim();
                                                    existingRow.Cells[11].Value = reader["gv_no"].ToString().Trim();
                                                }

                                            }
                                            // Populate other text fields similarly
                                            //txtItemDesc.Text = reader["item_desc"].ToString().Trim();
                                            reader.Close();

                                        }

                                    }
                                }


                                string querydet = "SELECT a.* FROM temp_t_invoice_det a, temp_t_invoice_hdr b WHERE a.open_yn='Y' AND b.ent_by=? AND a.comp_name=? AND a.invoice_no=? ORDER BY ent_on DESC;";

                                OdbcParameter[] parametersdet = new OdbcParameter[3];
                                parametersdet[0] = new OdbcParameter("ent_by", user.Trim());
                                parametersdet[1] = new OdbcParameter("comp_name", compname.Trim());
                                parametersdet[2] = new OdbcParameter("invoice_no", readerinv["invoice_no"]);


                                using (OdbcDataReader reader = dbConnector.ExecuteReader(querydet, parametersdet))
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            // Check if the row already exists based on a condition (e.g., invoice_no)
                                            DataGridViewRow existingRow = null;

                                            foreach (DataGridViewRow row in dbgItemDet.Rows)
                                            {

                                                existingRow.Cells[1].Value = reader["bar_code"].ToString().Trim();
                                                existingRow.Cells[3].Value = reader["qty"].ToString().Trim();
                                                existingRow.Cells[4].Value = reader["mrp"].ToString().Trim();
                                                existingRow.Cells[5].Value = reader["sale_price"].ToString().Trim();
                                                existingRow.Cells[6].Value = reader["disc_per"].ToString().Trim();
                                                existingRow.Cells[7].Value = reader["disc_amt"].ToString().Trim();
                                                existingRow.Cells[8].Value = reader["sale_tax_per"].ToString().Trim();
                                                existingRow.Cells[9].Value = reader["cess_perc"].ToString().Trim();
                                                existingRow.Cells[10].Value = reader["net_amt"].ToString().Trim();
                                                existingRow.Cells[11].Value = reader["sale_tax_amt"].ToString().Trim();
                                                existingRow.Cells[12].Value = reader["cess_amt"].ToString().Trim();
                                                existingRow.Cells[13].Value = reader["item_id"].ToString().Trim();
                                            }

                                            reader.Close();

                                        }

                                    }
                                }

                                string queryinvhdr = "SELECT a.* FROM temp_t_invoice_hdr a WHERE a.open_yn='Y' AND a.ent_by=? AND a.comp_name=? AND a.invoice_no=? ORDER BY a.ent_on DESC limit 1;";

                                OdbcParameter[] parametershdr = new OdbcParameter[3];
                                parametershdr[0] = new OdbcParameter("ent_by", user.Trim());
                                parametershdr[1] = new OdbcParameter("comp_name", compname.Trim());
                                parametershdr[2] = new OdbcParameter("invoice_no", readerinv["invoice_no"]);




                                using (OdbcDataReader reader = dbConnector.ExecuteReader(queryinvhdr, parametershdr))
                                {
                                    if (reader.Read())
                                    {
                                        if (reader["invoice_dt"] != DBNull.Value)
                                        {
                                            DateTime invDate = Convert.ToDateTime(reader["invoice_dt"]);

                                            // Set the format you desire (dd/MM/yyyy)
                                            dtpInvDate.Format = DateTimePickerFormat.Custom;
                                            dtpInvDate.CustomFormat = "dd/MM/yyyy";

                                            // Set the DateTimePicker value
                                            dtpInvDate.Value = invDate;
                                        }
                                        General general = new General();
                                        //-------------------------Cust combo----------------------------//
                                        //general.FillCombo(cboCust, "cust_id", "m_customer", false);
                                        if (reader["cust_id"] != DBNull.Value)
                                        {
                                            string typIDFromDatabase = reader["cust_id"].ToString().Trim();

                                            if (typIDFromDatabase != "")
                                            {
                                                // Find the item in the ComboBox's items collection
                                                object selectedItem = cboCust.Items.Cast<object>().FirstOrDefault(item => item.ToString() == typIDFromDatabase);

                                                // Set the selected item if found
                                                if (selectedItem != null)
                                                {
                                                    cboCust.SelectedItem = selectedItem;
                                                    DataRow customerData = GetCustomerData("m_customer", "cust_id", "C", cboCust.SelectedItem.ToString().Trim());

                                                    if (customerData != null)
                                                    {
                                                        custName = customerData["cust_name"].ToString().Trim();
                                                        custPhoneNo = customerData["phone_1"].ToString().Trim();
                                                        custAdd1 = customerData["address1"].ToString().Trim();
                                                        custAdd2 = customerData["address2"].ToString().Trim();
                                                        custEmail = customerData["email"].ToString().Trim();
                                                    }


                                                    rotInvCust.Text = custName;
                                                    txtCustName.Text = custName;
                                                    txtAddress.Text = custAdd1 + custAdd2;
                                                }



                                            }
                                        }
                                        rotBillTime.Text = reader["bill_time"].ToString().Trim();
                                        rotGAmt.Text = reader["gross_amt"].ToString().Trim();
                                        txtDiscPer.Text = reader["disc_per"].ToString().Trim();
                                        txtDiscAmt.Text = reader["disc_amt"].ToString().Trim();
                                        rotPayAmt.Text = reader["net_amt_after_disc"].ToString().Trim();
                                        rotNetAmt.Text = reader["net_amt"].ToString().Trim();
                                        rotRO.Text = reader["round_off"].ToString().Trim();

                                        reader.Close();

                                    }

                                }


                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the database operation
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                dbConnector.CloseConnection();
            }

        }

        public void check_temp_login_sytemname()
        {
            string getent_by = "";
            string getcomp_name = "";
            DbConnector dbConnector = new DbConnector();

            dbConnector.OpenConnection();

            bool check = false;
            string id = "invoice_no";
            string tblname = "t_invoice_hdr";

            DeTools.gstrSQL = "SELECT " + id + ",ent_by,comp_name FROM temp_" + tblname + " where open_yn='Y' order by ent_on desc limit 1;";

            using (OdbcDataReader reader = dbConnector.CreateResultset(DeTools.gstrSQL))
            {
                if (reader.HasRows)
                {
                    getent_by = reader["ent_by"].ToString();
                    getcomp_name = reader["comp_name"].ToString();


                }
                string pnlusername = MainForm.Instance.pnlUserName.Text.Trim();
                string machine_name = DeTools.fOSMachineName.GetMachineName();

                if (getent_by.ToLower().Trim() == pnlusername.ToLower().Trim() && getcomp_name.Trim() == machine_name.Trim())
                {

                    UnsavedData();
                }
            }
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
            // dbgPayDet.CellValueChanged -= dbgPayDet_CellValueChanged;
            UpdateDataGridViewStatus();
            FillCustCombo();


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

            // Check if F3 key is pressed
            if (e.KeyCode == Keys.F3)
            {
                // Check if the current cell is in the desired column
                //if (dbgItemDet.CurrentCell != null && dbgItemDet.CurrentCell.ColumnIndex == YourDesiredColumnIndex)

                UpdateDbgPayDet();

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
                    //string Payamtcol = dbgPayDet.Columns['PayAmt'].;
                    //----code for restricting to add payment mode again after amount matched
                    if (currentAmount == decimal.Parse(rotPayAmt.Text))
                    {
                        paymatchflag = true;
                    }
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

        //--for F3 default select CASH MODE
        private void UpdateDbgPayDet()
        {
            // Assuming 'Paymod' column index is 0 and 'PayAmt' column index is 1
            int paymodColumnIndex = 1;
            int payamtColumnIndex = 2;

            // Add a new row to dbgPayDet
            // int rowIndex = dbgPayDet.Rows.Add();

            // Set the default value in 'Paymod' column to "Cash"
            dbgPayDet.Rows[0].Cells[paymodColumnIndex].Value = "Cash";

            // Set the value in 'PayAmt' column to rotPayAmt.Text
            dbgPayDet.Rows[0].Cells[payamtColumnIndex].Value = rotPayAmt.Text;
        }

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

                //-------------------------------------------------



                //if (e.ColumnIndex == 1 && e.RowIndex >= 0)
                //{
                //    // Get the selected value in the ComboBox
                //    DataGridViewComboBoxCell comboCell1 = (DataGridViewComboBoxCell)dbgPayDet.Rows[e.RowIndex].Cells["Paymod"];
                //    string selectedValue = comboCell.Value?.ToString();

                //    // Assuming "Column2" is the name of the 2nd column
                //    DataGridViewTextBoxColumn column2 = (DataGridViewTextBoxColumn)dbgPayDet.Columns["Column2"];

                //    // Update Column2 dynamically
                //    UpdateColumn2Value((DataGridViewTextBoxColumn)dbgPayDet.Columns["Column2"], selectedValue, e.RowIndex);

                //    // Update the sum of column2 values and check if it matches rotPayAmt
                //    decimal remainingAmount = CalculateRemainingAmount();

                //    if (remainingAmount != 0)
                //    {
                //        // Display the remaining amount in the next row's column2
                //        UpdateColumn2ValueInNextRow(remainingAmount, e.RowIndex);
                //    }
                //    else
                //    {
                //        // Display a message indicating that the amount is matched
                //        MessageBox.Show("Amount Matched", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    }

                //}

            }

            //if (e.RowIndex >= 0 && e.ColumnIndex == 1) // Check if the edited cell is in the 'Paymod' column
            //{
            //    // Get the total amount from 'rotPayAmt.Text' for the first row only
            //    if (e.RowIndex == 0)
            //    {
            //        totalAmount = decimal.Parse(rotPayAmt.Text);
            //        // Directly use the value from 'rotPayAmt.Text' based on the selected category
            //        string selectedCategory = dbgPayDet.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            //        dbgPayDet.Rows[e.RowIndex].Cells[2].Value = totalAmount;

            //        // Check if the total 'PayAmt' matches 'rotPayAmt.Text'
            //        if (IsTotalAmountMatch())
            //        {
            //            MessageBox.Show("Total amount matched!");
            //        }
            //    }

            //    // Update the remaining amount in the next row's 'PayAmt' column
            //    int nextRowIndex = e.RowIndex + 1;
            //    if (nextRowIndex < dbgPayDet.Rows.Count)
            //    {
            //        // Ensure the edited 'PayAmt' does not exceed the remaining amount
            //        decimal editedAmount = decimal.Parse(dbgPayDet.Rows[e.RowIndex].Cells[2].Value?.ToString() ?? "0");
            //        decimal remainingAmount = totalAmount - editedAmount;

            //        // Update the remaining amount in the next row's 'PayAmt' column regardless of payment mode selection
            //        dbgPayDet.Rows[nextRowIndex].Cells[2].Value = remainingAmount;
            //    }
            //}

            // Check if the changed cell is in the 'PayAmt' column



            if (e.ColumnIndex == 1 && e.RowIndex > 0)
            {
                // Check if the 'Paymod' column in the current row is not null
                object paymodValue = dbgPayDet.Rows[e.RowIndex].Cells["Paymod"].Value;

                string payamtValue = dbgPayDet.Rows[e.RowIndex].Cells["PayAmt"].Value != null
                ? dbgPayDet.Rows[e.RowIndex].Cells["PayAmt"].Value.ToString() : "0";

                if (paymodValue != null && paymodValue != DBNull.Value && payamtValue != null && decimal.Parse(payamtValue) > 0)
                {
                    dbgPayDet.Rows[e.RowIndex].Cells["PayAmt"].Value = 0.0;
                }
                // Update the remaining amount for the next row
                currentAmount = 0; // Reset the currentAmount before the loop
                foreach (DataGridViewRow row in dbgPayDet.Rows)
                {
                    currentAmount += Convert.ToDecimal(row.Cells["PayAmt"].Value);
                }

                remainingAmount = Decimal.Parse(rotPayAmt.Text) - currentAmount;

                if (paymodValue != null && paymodValue != DBNull.Value)
                {
                    // Set the remaining amount in the 'PayAmt' column of the next row
                    dbgPayDet.Rows[e.RowIndex].Cells["PayAmt"].Value = remainingAmount;
                }

            }
            //if (currentAmount == Decimal.Parse(rotPayAmt.Text))
            //{
            //    MessageBox.Show("Settlement Matched!!", "Settlement Matched", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    if (paymatchflag)
            //    {
            //        if (dbgPayDet.Rows[e.RowIndex].Cells["Paymod"].Value != null)
            //        {
            //            MessageBox.Show("Settlement Matched!! You Can't Add More.", "Settlement Already Matched!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //        }
            //    }

            //}



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

        //public DataRow GetCustomerData(string strTable, string strId_Field, string strType, object vntId_Value)
        //{
        //    DbConnector dbConnector = new DbConnector();
        //    DataRow result = null;

        //    if (vntId_Value == null || string.IsNullOrWhiteSpace(vntId_Value.ToString()))
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        string sql = "SELECT distinct * FROM " + strTable + " WHERE " + strId_Field + "=";
        //        if (strType == "N")
        //        {
        //            sql += vntId_Value;
        //        }
        //        else if (strType == "C")
        //        {
        //            sql += "'" + vntId_Value + "'";
        //        }
        //        else if (strType == "D")
        //        {
        //            sql += "'" + ((DateTime)vntId_Value).ToString("yyyy-MM-dd") + "'";
        //        }

        //        using (OdbcDataReader reader = dbConnector.CreateResultset(sql))
        //        {
        //            if (reader != null)
        //            {
        //                if (reader.Read())
        //                {
        //                    DataTable schemaTable = reader.GetSchemaTable();
        //                    result = new DataTable().NewRow();
        //                    foreach (DataRow schemaRow in schemaTable.Rows)
        //                    {
        //                        string columnName = schemaRow["ColumnName"].ToString();
        //                        result[columnName] = reader[columnName];
        //                    }
        //                }
        //                reader.Close();
        //            }
        //        }
        //    }

        //    return result;
        //}

        public DataRow GetCustomerData(string strTable, string strId_Field, string strType, object vntId_Value)
        {
            DbConnector dbConnector = new DbConnector();
            DataRow result = null;

            if (vntId_Value == null || string.IsNullOrWhiteSpace(vntId_Value.ToString()))
            {
                return null;
            }
            else
            {
                string sql = "SELECT * FROM " + strTable + " WHERE " + strId_Field + "=";
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
                            DataTable schemaTable = reader.GetSchemaTable();

                            // Check if 'cust_id' column is present in the result set
                            if (schemaTable.Columns.Contains("cust_id"))
                            {
                                result = new DataTable().NewRow();
                                foreach (DataRow schemaRow in schemaTable.Rows)
                                {
                                    string columnName = schemaRow["ColumnName"].ToString();

                                    // Check if the result DataRow has the column before assigning the value
                                    if (result.Table.Columns.Contains(columnName))
                                    {
                                        result[columnName] = reader[columnName];
                                    }
                                }
                            }
                            else
                            {
                                // Handle the case when 'cust_id' column is not present
                                // For example, log a message or throw an exception
                            }
                        }
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
            return GetDescCust("m_customer", "cust_id", "*", "C", custId);
        }

        private void cboCust_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cboCust.SelectedItem != null)
            //{
            //    General general = new General();
            //    string desc = GetDescCust("m_customer", "cust_id", "cust_name", "C", cboCust.SelectedItem.ToString().Trim());
            //    rotInvCust.Text = desc;
            //    //dbgPayDet.Enabled = cboCust != null && !string.IsNullOrEmpty(cboCust.SelectedItem?.ToString().Trim());
            //}

            //// Get the selected item from cboCust
            //string selectedItem = cboCust.SelectedItem?.ToString();

            //// Check if there is any selected item in the Paymod column ComboBox in dbgPayDet
            //foreach (DataGridViewRow row in dbgPayDet.Rows)
            //{
            //    DataGridViewComboBoxCell paymodCell = (DataGridViewComboBoxCell)row.Cells["Paymod"];
            //    if (paymodCell.Value != null)
            //    {
            //        // Update the corresponding 7th column ComboBox in the same row
            //        DataGridViewComboBoxCell custIDCell = (DataGridViewComboBoxCell)row.Cells["CustId"];

            //        // Clear any existing item in the 7th column ComboBox
            //        custIDCell.Value = null;
            //        if (!string.IsNullOrEmpty(selectedItem) & selectedItem != null)
            //        {
            //            // Add the selected item from cboCust to the 7th column ComboBox
            //            custIDCell.Items.Add(selectedItem);

            //        }

            //        // Automatically select the added item in the 7th column ComboBox
            //        custIDCell.Value = selectedItem;
            //    }
            //}


            if (cboCust.SelectedItem != null)
            {
                General general = new General();
                //string desc = GetDescCust("m_customer", "cust_id", "cust_name", "C", cboCust.SelectedItem.ToString().Trim());
                DataRow customerData = GetCustomerData("m_customer", "cust_id", "C", cboCust.SelectedItem.ToString().Trim());

                if (customerData != null)
                {
                    custName = customerData["cust_name"].ToString().Trim();
                    custPhoneNo = customerData["phone_1"].ToString().Trim();
                    custAdd1 = customerData["address1"].ToString().Trim();
                    custAdd2 = customerData["address2"].ToString().Trim();
                    custEmail = customerData["email"].ToString().Trim();
                }


                rotInvCust.Text = custName;
                txtCustName.Text = custName;
                txtAddress.Text = custAdd1 + custAdd2;
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

                    // Clear the DataSource property
                    custIDCell.DataSource = null;

                    // Clear any existing item in the 7th column ComboBox
                    custIDCell.Items.Clear();

                    if (!string.IsNullOrEmpty(selectedItem) && selectedItem != null)
                    {
                        // Add the selected item from cboCust to the 7th column ComboBox
                        custIDCell.Items.Add(selectedItem);
                    }

                    // Automatically select the added item in the 7th column ComboBox
                    custIDCell.Value = selectedItem;
                }
            }
        }

        private void InitializeTotalAmount()
        {
            // Parse the total amount from rotPayAmt.Text when initializing the form or setting up your data
            totalAmount = decimal.Parse(rotPayAmt.Text);
        }


        //private void dbgPayDet_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{

        //    // Check if the selected value already exists in any other row
        //    if (e.ColumnIndex == dbgPayDet.Columns["Paymod"].Index && e.RowIndex >= 0)
        //    {
        //        string newValue = dbgPayDet[e.ColumnIndex, e.RowIndex].Value?.ToString();

        //        //// Check if validation has already been performed for this cell
        //        //if (isValidationPerformed)
        //        //{
        //        //    isValidationPerformed = false; // Reset the flag
        //        //    return;
        //        //}

        //        // Check for duplicate selections in the Paymod column
        //        foreach (DataGridViewRow row in dbgPayDet.Rows)
        //        {
        //            if (row.Index != e.RowIndex)
        //            {
        //                DataGridViewComboBoxCell comboCell = (DataGridViewComboBoxCell)row.Cells["Paymod"];
        //                if (comboCell.Value != null && comboCell.Value.ToString() == newValue)
        //                {
        //                    // Cancel the edit
        //                    dbgPayDet.CancelEdit();

        //                    MessageBox.Show("This value is already selected in another row. Please select a different value.", "Duplicate Value", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        //                    // Clear the selected item in the ComboBox
        //                    DataGridViewComboBoxCell currentCell = (DataGridViewComboBoxCell)dbgPayDet[e.ColumnIndex, e.RowIndex];
        //                    currentCell.Value = null;

        //                    break; // No need to continue checking if a duplicate is found
        //                }
        //            }
        //        }

        //        if (newValue == "CR")
        //        {
        //            // Check if a customer is selected in cboCust
        //            if (cboCust.SelectedIndex == -1)
        //            {
        //                MessageBox.Show("Please select a customer ID.", "Missing Customer ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                cboCust.Focus();
        //                cboCust.SelectAll();

        //                DataGridViewComboBoxCell custIDCell1 = (DataGridViewComboBoxCell)dbgPayDet.Rows[e.RowIndex].Cells[7];
        //              //  dbgPayDet.Enabled = custIDCell1 != null && !string.IsNullOrEmpty(custIDCell1.Value?.ToString().Trim());
        //            }
        //        }

        //        // Reset the validation flag
        //        isValidationPerformed = false;
        //        // Check for other conditions or validations as needed
        //    }

        //    if (e.ColumnIndex == 1) // Check if the edited cell is in the 'Paymod' column
        //    {
        //        // Get the total amount from 'rotPayAmt.Text' for the first row only
        //        if (e.RowIndex == 0)
        //        {
        //            totalAmount = decimal.Parse(rotPayAmt.Text);
        //            //Directly use the value from 'rotPayAmt.Text' based on the selected category
        //            string selectedCategory = dbgPayDet.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
        //            dbgPayDet.Rows[e.RowIndex].Cells[2].Value = totalAmount;
        //        }


        //        // Check if the total 'PayAmt' matches 'rotPayAmt.Text'
        //        if (IsTotalAmountMatch())
        //        {
        //            MessageBox.Show("Total amount matched!");
        //        }
        //    }
        //    else if (e.ColumnIndex == 2) // Check if the edited cell is in the 'PayAmt' column
        //    {
        //        // Get the edited 'PayAmt' value
        //        object editedValue = dbgPayDet.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

        //        if (editedValue != null)
        //        {
        //            // Ensure the edited 'PayAmt' does not exceed the remaining amount
        //            decimal editedAmount = decimal.Parse(editedValue.ToString());
        //            decimal remainingAmount = totalAmount - editedAmount;

        //            // Update the remaining amount in the next row's 'PayAmt' column
        //            int nextRowIndex = e.RowIndex + 1;
        //            if (nextRowIndex < dbgPayDet.Rows.Count)
        //            {
        //                dbgPayDet.Rows[nextRowIndex].Cells[2].Value = remainingAmount;
        //            }
        //        }
        //    }
        //}


        //private bool IsTotalAmountMatch()
        //{
        //    decimal totalAmount = decimal.Parse(rotPayAmt.Text);

        //    // Calculate the sum of 'PayAmt' in column 2
        //    decimal sumPayAmt = 0;
        //    foreach (DataGridViewRow row in dbgPayDet.Rows)
        //    {
        //        if (row.Index == 0 && row.Cells[2].Value != null)
        //        {
        //            // Display totalAmount only in the first row
        //            row.Cells[2].Value = totalAmount;
        //        }

        //        if (row.Index >= 0 && row.Cells[2].Value != null)
        //        {
        //            // Calculate the sum of 'PayAmt' in subsequent rows
        //            sumPayAmt += decimal.Parse(row.Cells[2].Value.ToString());
        //        }
        //    }

        //    // Check if the sum matches the total amount
        //    return sumPayAmt == totalAmount;
        //}

        private void dbgPayDet_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the selected value already exists in any other row
            if (e.ColumnIndex == dbgPayDet.Columns["Paymod"].Index && e.RowIndex >= 0)
            {
                string newValue = dbgPayDet[e.ColumnIndex, e.RowIndex].Value?.ToString();

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
                if (currentAmount == Decimal.Parse(rotPayAmt.Text))
                {
                    MessageBox.Show("Settlement Matched!!", "Settlement Matched", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (paymatchflag)
                    {
                        if (dbgPayDet.Rows[e.RowIndex].Cells["Paymod"].Value != null)
                        {
                            MessageBox.Show("Settlement Matched!! You Can't Add More.", "Settlement Already Matched!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            // Remove the current row
                            dbgPayDet.Rows.RemoveAt(e.RowIndex);
                        }
                    }

                }
            }
            //if (e.RowIndex >= 0 && e.ColumnIndex == 1) // Check if the edited cell is in the 'Paymod' column
            //{
            //    // Get the total amount from 'rotPayAmt.Text' for the first row only
            //    if (e.RowIndex == 0)
            //    {
            //        totalAmount = decimal.Parse(rotPayAmt.Text);
            //        // Directly use the value from 'rotPayAmt.Text' based on the selected category
            //        string selectedCategory = dbgPayDet.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            //        dbgPayDet.Rows[e.RowIndex].Cells[2].Value = totalAmount;

            //        // Check if the total 'PayAmt' matches 'rotPayAmt.Text'
            //        if (IsTotalAmountMatch())
            //        {
            //            MessageBox.Show("Total amount matched!");
            //        }
            //    }

            //    // Update the remaining amount in the next row's 'PayAmt' column
            //    int nextRowIndex = e.RowIndex + 1;
            //    if (nextRowIndex < dbgPayDet.Rows.Count)
            //    {
            //        // Ensure the edited 'PayAmt' does not exceed the remaining amount
            //        decimal editedAmount = decimal.Parse(dbgPayDet.Rows[e.RowIndex].Cells[2].Value?.ToString() ?? "0");
            //        decimal remainingAmount = totalAmount - editedAmount;

            //        // Update the remaining amount in the next row's 'PayAmt' column regardless of payment mode selection
            //        dbgPayDet.Rows[nextRowIndex].Cells[2].Value = remainingAmount;
            //    }
            //}

            // Check if the edited cell is in the 'Paymod' column
            if (e.ColumnIndex == dbgPayDet.Columns["Paymod"].Index && e.RowIndex == 0)
            {
                // Set the 'PayAmt' in the first row directly from rotPayAmt.Text
                dbgPayDet.Rows[0].Cells["PayAmt"].Value = rotPayAmt.Text;
            }
        }

        private bool IsTotalAmountMatch()
        {
            decimal totalAmount = decimal.Parse(rotPayAmt.Text);

            // Calculate the sum of 'PayAmt' in column 2
            decimal sumPayAmt = 0;
            foreach (DataGridViewRow row in dbgPayDet.Rows)
            {
                if (row.Index == 0 && row.Cells[2].Value != null)
                {
                    // Display totalAmount only in the first row
                    row.Cells[2].Value = totalAmount;
                }

                if (row.Index >= 0 && row.Cells[2].Value != null)
                {
                    // Calculate the sum of 'PayAmt' in subsequent rows
                    sumPayAmt += decimal.Parse(row.Cells[2].Value.ToString());
                }
            }

            // Check if the sum matches the total amount
            return sumPayAmt == totalAmount;
        }

        private void UpdateRemainingAmount(int rowIndex, decimal remainingAmount)
        {
            // Check if the row index is within the bounds of the DataGridView
            if (rowIndex >= 0 && rowIndex < dbgPayDet.Rows.Count)
            {
                // Only update the 'PayAmt' value if it's in the first row
                if (rowIndex == 0)
                {
                    dbgPayDet.Rows[rowIndex].Cells[2].Value = remainingAmount;
                }
                else
                {
                    dbgPayDet.Rows[rowIndex].Cells[2].Value = DBNull.Value; // or set it to null or 0 as needed
                }
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
