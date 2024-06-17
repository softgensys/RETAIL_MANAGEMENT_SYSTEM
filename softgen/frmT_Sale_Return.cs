using System.Data;
using System.Data.Odbc;

namespace softgen
{
    public partial class frmT_Sale_Return : Form, Interface_for_Common_methods.ISearchableForm
    {
        private string mstrEntBy, mstrEntOn, mstrAuthBy, mstrAuthOn, chkItemid;
        public bool mblnSearch, mblnDataEntered;
        public bool blnItem_H, blnItem_D;
        public string checkboxState = "N"; // Default value when checkbox is not checked
        public string selectedDate = "";
        public string selectedinvoice_No = "";
        public Decimal SRNetAmt = 0.00m;
        public DbConnector dbConnector;
        private bool cellValueChangedInProgress = false;
        private int flaggotonextcell = 0;
        private decimal mrpValue;
        private bool isValidationPerformed = false;
        private object previousValue;
        private decimal remainingAmount;
        private decimal currentAmount = 0;
        private bool paymatchflag = false;
        public string custName;
        //public DbConnector dbConnector;
        public string custPhoneNo;
        public string custEmail;
        public string custAdd1;
        public string custAdd2;
        // Count in multiple item table pop up rows manually
        int rowCount = 0;
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
        public decimal overalldiscamt = 0;
        public string closingdayok = "N";
        public string ccodetxt = "";
        public string paymodtxt = "";
        public string couptxt = "";
        public string selectedCustomer = "";
        public string strMode;
        //*** for getting invoice no from help for Modify 
        public static string GetInvNoFromHelp = "";
        public int roundoffval = 1;


        public frmT_Sale_Return()
        {
            InitializeComponent();
            this.Activated += MyForm_Activated;

            if (checkboxState == "N")
            {
                btnDown.Enabled = false;

                btnDownAll.Enabled = false;
                btnUp.Enabled = false;
                btnUpAll.Enabled = false;
            }
            else
            {
                btnDown.Enabled = true;
                btnDownAll.Enabled = true;
                btnUp.Enabled = true;
                btnUpAll.Enabled = true;
            }
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

        public void UpdateInSaveForm()
        {
            DeTools.gstrSQL = "SELECT a.*, b.*, c.* FROM t_sr_det a " +
                 "JOIN t_sr_hdr b ON a.sr_no = b.sr_no " +
                 "JOIN t_sr_pay_det c ON a.sr_no = c.sr_no " +
                 "WHERE a.sr_no = '" + txtSRNo.Text.Trim() + "' " +
                 "AND b.sr_no = '" + txtSRNo.Text.Trim() + "' " +
                 "AND c.sr_no = '" + txtSRNo.Text.Trim() + "' LIMIT 1;";
            OdbcCommand cmd = new OdbcCommand(DeTools.gstrSQL, dbConnector.connection);
            dbConnector.connection.Open();

            OdbcDataReader reader = cmd.ExecuteReader();
            blnItem_H = true;
            string pnlusername = MainForm.Instance.pnlUserName.Text.Trim();
            string machine_name = DeTools.fOSMachineName.GetMachineName();

            // Check if the record with the specified Group_id exists
            if (DeTools.GetMode(this) != DeTools.ADDMODE)
            {
                if (reader.HasRows)
                {
                    // The record exists, so update it
                    reader.Close();
                    Cursor.Current = Cursors.WaitCursor;
                    machine_name = DeTools.fOSMachineName.GetMachineName();
                    string BillTime = DateTime.Now.ToString("HH:mm:ss");

                    string gstrSQL1_hdr = "UPDATE t_sr_hdr SET branch_id = ?,invoice_no = ?,doc_type = ?,gross_amt = ? ,disc_per = ? ,disc_amt = ? ," +
                        "net_amt = ? ,notes = ? ,status = ? ,mod_on = ? ,mod_by = ?,o_amt = ? ," +
                        "machine_id_m = ? ,ret_time = ? WHERE sr_no = ? AND branch_id = ?;";

                    cmd.CommandText = gstrSQL1_hdr;
                    cmd.Parameters.Add(new OdbcParameter("branch_id", string.IsNullOrEmpty(DeTools.strBranch) ? "" : DeTools.strBranch));
                    cmd.Parameters.Add(new OdbcParameter("invoice_no", string.IsNullOrEmpty(cboInvNo.SelectedItem.ToString().Trim()) ? "" : cboInvNo.SelectedItem.ToString().Trim()));
                    cmd.Parameters.Add(new OdbcParameter("doc_type", "M"));
                    cmd.Parameters.Add(new OdbcParameter("gross_amt", string.IsNullOrEmpty(rotGrossAmt.ToString().Trim()) ? "" : rotGrossAmt.ToString().Trim()));
                    cmd.Parameters.Add(new OdbcParameter("disc_per", string.IsNullOrEmpty(rotDisc.ToString().Trim()) ? "" : rotDisc.ToString().Trim()));
                    cmd.Parameters.Add(new OdbcParameter("disc_amt", string.IsNullOrEmpty(rotDiscAmt.ToString().Trim()) ? "" : rotDiscAmt.ToString().Trim()));
                    cmd.Parameters.Add(new OdbcParameter("net_amt", string.IsNullOrEmpty(rotNetAmt.ToString().Trim()) ? "" : rotNetAmt.ToString().Trim()));
                    cmd.Parameters.Add(new OdbcParameter("notes", string.IsNullOrEmpty(txtRemarks.ToString().Trim()) ? "" : txtRemarks.ToString().Trim()));
                    cmd.Parameters.Add(new OdbcParameter("status", "V"));
                    cmd.Parameters.Add(new OdbcParameter("mod_date", OdbcType.DateTime)).Value = DateTime.Now;
                    cmd.Parameters.Add(new OdbcParameter("mod_by", DeTools.gstrloginId));

                    cmd.Parameters.Add(new OdbcParameter("o_amt", string.IsNullOrEmpty(rotNetAmt.ToString().Trim()) ? "" : rotNetAmt.ToString().Trim()));
                    cmd.Parameters.Add(new OdbcParameter("machine_id_m", string.IsNullOrEmpty(machine_name.Trim()) ? "" : machine_name.ToString().Trim()));
                    cmd.Parameters.Add(new OdbcParameter("ret_time", string.IsNullOrEmpty(BillTime.Trim()) ? "" : BillTime.Trim()));
                    cmd.Parameters.Add(new OdbcParameter("sr_no", string.IsNullOrEmpty(txtSRNo.ToString().Trim()) ? "" : txtSRNo.ToString().Trim()));
                    cmd.Parameters.Add(new OdbcParameter("branch_id", string.IsNullOrEmpty(DeTools.strBranch) ? "" : DeTools.strBranch));

                    string gstrSQL1_det = "UPDATE t_sr_det SET branch_id = ? ,item_id = ? ,bar_code = ? ,item_sl_no = ? ,qty = ? ,mrp = ? ," +
                        "sale_price = ? ,disc_per = ? ,disc_amt = ? ,sale_tax_per = ? ,sale_tax_amt = ? ,net_amt = ? ,pur_rate = ? ," +
                        "cess_perc = ? ,cess_amt = ? ,excis_perc = ? ,excis_amt = ? WHERE sr_no=? AND branch_id = ?;";

                }
            }
        }

        public string dayclosing()
        {
            DbConnector dbConnector = new DbConnector();
            try
            {
                dbConnector.OpenConnection();

                string chk_cls_day = "select IFNULL(max(sr_dt),0) as max_sr_dt from t_sr_hdr;";
                using (OdbcDataReader chk_cls_day_read = dbConnector.CreateResultset(chk_cls_day))
                {
                    if (chk_cls_day_read.HasRows && chk_cls_day_read.Read())
                    {
                        // Check if the max_invoice_dt column is DBNull
                        if (!chk_cls_day_read.IsDBNull(chk_cls_day_read.GetOrdinal("max_sr_dt")))
                        {
                            // Retrieve the max_invoice_dt value
                            string maxSRDateStr = chk_cls_day_read.GetString(chk_cls_day_read.GetOrdinal("max_sr_dt"));
                            string closedt;
                            // Check if max_invoice_dt is empty or equal to the current date
                            if (!string.IsNullOrEmpty(maxSRDateStr) || DateTime.TryParse(maxSRDateStr, out DateTime maxSRDate) && maxSRDate.Date == DateTime.Today)
                            {
                                closedt = chk_cls_day_read["max_sr_dt"].ToString().Trim();
                                closingdayok = "Y";

                            }
                            else
                            {
                                MessageBox.Show("Sale Return Not Possible In Back Date!", "Sale Return Not Possible", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                closingdayok = "N";
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("Sale Return Not Possible In Back Date!", "Sale Return Not Possible", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        closingdayok = "N";
                    }

                }
                dbConnector.connection.Close();
                return closingdayok;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void AddInSaveForm()
        {

            OdbcTransaction transaction = null;
            try
            {


                DbConnector dbConnector = new DbConnector();
                Cursor.Current = Cursors.WaitCursor;
                dayclosing();
                string pnlusername = MainForm.Instance.pnlUserName.Text.Trim();
                string machine_name = DeTools.fOSMachineName.GetMachineName();

                if (closingdayok == "Y")
                {
                    dbConnector.OpenConnection();

                    if (dbConnector.connection != null)
                    {
                        transaction = dbConnector.connection.BeginTransaction();

                        if (strMode != string.Empty && saveflag == true)
                        {
                            gen_invoice_no = General.GenMDocno("SR").ToString().Trim();
                            if (gen_invoice_no.Length == 0)
                            {
                                gen_invoice_no = "";
                                string gstrMsg = "Document series for Sale Return Generation. exhausted or not available. Sale Return cannot be saved.";
                                Messages.ErrorMsg(gstrMsg);
                                saveflag = false;
                            }

                        }

                        string inserthdrnew = "INSERT INTO t_sr_hdr(sr_no,sr_dt,branch_id,invoice_no," +
                            "doc_type,gross_amt,disc_per,disc_amt,net_amt,notes,status,ent_on,ent_by," +
                            "o_amt,machine_id,ret_time)VALUES" +
                            "(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?);";

                        using (OdbcCommand cmddhdr = new OdbcCommand(inserthdrnew, dbConnector.connection))
                        {
                            cmddhdr.Transaction = transaction;

                            string BillTime = DateTime.Now.ToString("HH:mm:ss");

                            cmddhdr.Parameters.Add(new OdbcParameter("sr_no", gen_invoice_no.Trim()));
                            cmddhdr.Parameters.Add(new OdbcParameter("sr_dt", string.IsNullOrEmpty(dtpSRDate.Value.ToString("yyyy-MM-dd")) ? "" : dtpSRDate.Value.ToString("yyyy-MM-dd").Trim()));
                            cmddhdr.Parameters.Add(new OdbcParameter("branch_id", string.IsNullOrEmpty(DeTools.strBranch) ? "" : DeTools.strBranch));
                            if (checkboxState == "Y")
                            {
                                if (cboInvNo.SelectedItem != null && !string.IsNullOrEmpty(cboInvNo.SelectedItem.ToString().Trim()))
                                {
                                    cmddhdr.Parameters.Add(new OdbcParameter("invoice_no", string.IsNullOrEmpty(cboInvNo.SelectedItem.ToString().Trim())));
                                }
                            }
                            else
                            {
                                cmddhdr.Parameters.Add(new OdbcParameter("invoice_no", ""));
                            }
                            cmddhdr.Parameters.Add(new OdbcParameter("doc_type", "S"));
                            cmddhdr.Parameters.Add(new OdbcParameter("gross_amt", string.IsNullOrEmpty(rotGrossAmt.Text.ToString().Trim()) ? "" : rotGrossAmt.Text.ToString().Trim()));
                            cmddhdr.Parameters.Add(new OdbcParameter("disc_per", string.IsNullOrEmpty(rotDisc.Text.ToString().Trim()) ? "" : rotDisc.Text.ToString().Trim()));
                            cmddhdr.Parameters.Add(new OdbcParameter("disc_amt", string.IsNullOrEmpty(rotDiscAmt.Text.ToString().Trim()) ? "" : rotDiscAmt.Text.ToString().Trim()));
                            cmddhdr.Parameters.Add(new OdbcParameter("net_amt", string.IsNullOrEmpty(rotNetAmt.Text.ToString().Trim()) ? "" : rotNetAmt.Text.ToString().Trim()));
                            cmddhdr.Parameters.Add(new OdbcParameter("notes", string.IsNullOrEmpty(txtRemarks.Text.ToString().Trim()) ? "" : txtRemarks.Text.ToString().Trim()));
                            cmddhdr.Parameters.Add(new OdbcParameter("status", "V"));
                            cmddhdr.Parameters.Add(new OdbcParameter("ent_on", OdbcType.DateTime)).Value = DateTime.Now;
                            cmddhdr.Parameters.Add(new OdbcParameter("ent_by", pnlusername));
                            cmddhdr.Parameters.Add(new OdbcParameter("o_amt", string.IsNullOrEmpty(rotNetAmt.Text.ToString().Trim()) ? "" : rotNetAmt.Text.ToString().Trim()));
                            cmddhdr.Parameters.Add(new OdbcParameter("machine_id", string.IsNullOrEmpty(machine_name) ? "" : machine_name));
                            cmddhdr.Parameters.Add(new OdbcParameter("ret_time", string.IsNullOrEmpty(BillTime) ? "" : BillTime));
                            cmddhdr.ExecuteNonQuery();


                        }

                        // Insert details into t_sr_det, but only up to row-1
                        string insertdet = "INSERT INTO t_sr_det(sr_no,branch_id,item_id,bar_code,item_sl_no,qty,mrp," +
                            "sale_price,disc_per,disc_amt,sale_tax_per,sale_tax_amt,net_amt,pur_rate,cess_perc,cess_amt)" +
                            "VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?);";

                        for (int i = 0; i < dbgItemDetRet.Rows.Count - 1; i++)
                        {
                            DataGridViewRow row = dbgItemDetRet.Rows[i];

                            string itemID = row.Cells["Itembar2"].Value?.ToString().Trim();

                            // Fetch the pur_rate from m_item_det
                            string purRateQuery = "SELECT net_rate FROM m_item_det WHERE item_id = ? or plu= ? or bar_code = ?";
                            string purRate = "0.00"; // Default value

                            using (OdbcCommand cmdGetPurRate = new OdbcCommand(purRateQuery, dbConnector.connection))
                            {
                                cmdGetPurRate.Transaction = transaction;
                                cmdGetPurRate.Parameters.Add(new OdbcParameter("item_id", itemID));
                                cmdGetPurRate.Parameters.Add(new OdbcParameter("plu", itemID));
                                cmdGetPurRate.Parameters.Add(new OdbcParameter("bar_code", itemID));

                                using (OdbcDataReader reader = cmdGetPurRate.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        purRate = reader["net_rate"].ToString().Trim();
                                    }
                                }
                            }


                            using (OdbcCommand cmdddet = new OdbcCommand(insertdet, dbConnector.connection))
                            {
                                cmdddet.Transaction = transaction;

                                cmdddet.Parameters.Add(new OdbcParameter("sr_no", gen_invoice_no.Trim()));
                                cmdddet.Parameters.Add(new OdbcParameter("branch_id", DeTools.strBranch.Trim()));
                                cmdddet.Parameters.Add(new OdbcParameter("item_id", row.Cells["Itemid2"].Value?.ToString().Trim()));
                                cmdddet.Parameters.Add(new OdbcParameter("bar_code", row.Cells["Itembar2"].Value?.ToString().Trim()));
                                cmdddet.Parameters.Add(new OdbcParameter("item_sl_no", row.Cells["Srno2"].Value?.ToString().Trim()));
                                cmdddet.Parameters.Add(new OdbcParameter("qty", row.Cells["Qty2"].Value?.ToString().Trim()));
                                cmdddet.Parameters.Add(new OdbcParameter("mrp", row.Cells["Mrp2"].Value?.ToString().Trim()));
                                cmdddet.Parameters.Add(new OdbcParameter("sale_price", row.Cells["Unitprice2"].Value?.ToString().Trim()));
                                cmdddet.Parameters.Add(new OdbcParameter("disc_per", row.Cells["Disc2"].Value?.ToString().Trim()));
                                cmdddet.Parameters.Add(new OdbcParameter("disc_amt", row.Cells["Discamt2"].Value?.ToString().Trim()));
                                cmdddet.Parameters.Add(new OdbcParameter("sale_tax_per", row.Cells["Gst2"].Value?.ToString().Trim()));
                                cmdddet.Parameters.Add(new OdbcParameter("sale_tax_amt", row.Cells["Gstamt2"].Value?.ToString().Trim() ?? "0.00"));
                                cmdddet.Parameters.Add(new OdbcParameter("net_amt", row.Cells["Amt2"].Value?.ToString().Trim() ?? "0.00"));
                                cmdddet.Parameters.Add(new OdbcParameter("pur_rate", purRate));
                                cmdddet.Parameters.Add(new OdbcParameter("cess_perc", row.Cells["Cess2"].Value?.ToString().Trim() ?? "0.00"));
                                cmdddet.Parameters.Add(new OdbcParameter("cess_amt", row.Cells["Cessamt2"].Value?.ToString().Trim() ?? "0.00"));

                                cmdddet.ExecuteNonQuery();
                            }

                        }
                        // Insert details into t_sr_det, but only up to row-1
                        string insertpaydet = "INSERT INTO t_sr_pay_det(sr_no,pay_mode_id,branch_id,pay_amt,cash_t_amt,ref_amt,cc_code,cc_no," +
                            "coup_id,cust_id,bank_name,cheque_no,cheque_dt)VALUES (?,?,?,?,?,?,?,?,?,?,?,?,?)";

                        for (int ii = 0; ii < dbgPayDet.Rows.Count - 1; ii++)
                        {
                            DataGridViewRow row1 = dbgPayDet.Rows[ii];

                            using (OdbcCommand cmdpaydet = new OdbcCommand(insertpaydet, dbConnector.connection))
                            {

                                cmdpaydet.Transaction = transaction;

                                cmdpaydet.Parameters.Add(new OdbcParameter("sr_no", gen_invoice_no.Trim()));
                                // Assuming the second column is the ComboBox column
                                //DataGridViewComboBoxCell comboBoxCell = row.Cells[1] as DataGridViewComboBoxCell;



                                if (paymodtxt != "")
                                {
                                    cmdpaydet.Parameters.Add(new OdbcParameter("pay_mode_id", paymodtxt.Trim()));


                                    cmdpaydet.Parameters.Add(new OdbcParameter("branch_id", DeTools.strBranch.Trim()));
                                    cmdpaydet.Parameters.Add(new OdbcParameter("pay_Amt", row1.Cells[2].Value?.ToString() ?? "0.00"));
                                    cmdpaydet.Parameters.Add(new OdbcParameter("cash_t_amt", row1.Cells[3].Value?.ToString() ?? "0.00"));
                                    cmdpaydet.Parameters.Add(new OdbcParameter("ref_amt", row1.Cells[4].Value?.ToString() ?? "0.00"));

                                    // Assuming paymode is a control that contains the payment mode value
                                    if (paymodtxt == "CC")
                                    {

                                        cmdpaydet.Parameters.Add(new OdbcParameter("cc_code", ccodetxt.Trim()));

                                        cmdpaydet.Parameters.Add(new OdbcParameter("cc_no", row1.Cells[6].Value?.ToString() ?? ""));
                                    }
                                    else
                                    {
                                        cmdpaydet.Parameters.Add(new OdbcParameter("cc_code", ""));
                                        cmdpaydet.Parameters.Add(new OdbcParameter("cc_no", ""));
                                    }

                                    if (paymodtxt == "COUP")
                                    {

                                        cmdpaydet.Parameters.Add(new OdbcParameter("coup_id", couptxt.Trim()));

                                    }
                                    else
                                    {
                                        cmdpaydet.Parameters.Add(new OdbcParameter("coup_id", ""));
                                    }

                                    if (cboCust.Text != "")
                                    {

                                        cmdpaydet.Parameters.Add(new OdbcParameter("cust_id", row1.Cells[8].Value.ToString()));

                                    }

                                    else
                                    {
                                        cmdpaydet.Parameters.Add(new OdbcParameter("cust_id", ""));
                                    }

                                    if (row1.Cells[9].Value != null)
                                    {
                                        cmdpaydet.Parameters.Add(new OdbcParameter("bank_name", row1.Cells[9].Value.ToString()));
                                    }
                                    else
                                    {
                                        cmdpaydet.Parameters.Add(new OdbcParameter("bank_name", ""));
                                    }
                                    cmdpaydet.Parameters.Add(new OdbcParameter("cheque_no", (row1.Cells[10].Value != null) ? row1.Cells[10].Value.ToString() : ""));
                                    cmdpaydet.Parameters.Add(new OdbcParameter("cheque_dt", (row1.Cells[11].Value != null) ? row1.Cells[11].Value.ToString() : ""));

                                    // Execute the command
                                    cmdpaydet.ExecuteNonQuery();
                                }
                                else
                                {
                                    // Handle the case where selectedValue is null
                                    MessageBox.Show("Selected value is null!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }
                        }

                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dbConnector.connection != null && dbConnector.connection.State == ConnectionState.Open)
                {
                    dbConnector.connection.Close();
                }
                Cursor.Current = Cursors.Default;

                MessageBox.Show("Sale Retrun Saved Successfully!", "Sale Return Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


                int J;

                dbgItemDetRet.Update();
                //transaction = dbConnector.connection.BeginTransaction();

                if (mblnSearch == false)
                {
                    //if (!CheckMandatoryFields())
                    //{
                    //    saveflag = false;
                    //}

                    //else

                    UpdateInSaveForm();



                }  //-------End of If//===========******End of Update Now Add*******==============

                else if (mblnSearch == true)
                {

                    int upperlimit = 999999999;



                    dayclosing();
                    if (closingdayok == "Y")
                    {
                        AddInSaveForm();

                    }
                }
            }
            catch (Exception)
            {

                throw;
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

        }

        public void PrintForm()
        {
            SaveForm();
            //string brandnm = DeTools.strBrand.Trim();
            // Instantiate Form2 with ReportViewer
            Sale_Return_Inv_Form sale_Return_Inv_Form = new Sale_Return_Inv_Form();

            // Show Form2
            sale_Return_Inv_Form.ShowDialog();
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
                if (control is System.Windows.Forms.DateTimePicker && control.Name != null && control.Name.StartsWith("dtp"))
                {
                    System.Windows.Forms.DateTimePicker dateTimePicker = (System.Windows.Forms.DateTimePicker)control;

                    // Reset the value
                    //dateTimePicker.Text = "";

                    // Enable the TextBox
                    dateTimePicker.Enabled = true;
                }

                // Recursively call the method for nested controls
                if (control.Controls.Count > 0)
                {
                    ResetControls(control.Controls);
                }
            }


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
                    //UpdateDataGridViewStatus();
                    //string Payamtcol = dbgPayDet.Columns['PayAmt'].;
                    //----code for restricting to add payment mode again after amount matched
                    if (currentAmount == decimal.Parse(rotNetAmt.Text))
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

        public void FillInvCombo(ComboBox combo, string strGdate)
        {
            try
            {
                dbConnector = new DbConnector();
                DeTools.gstrSQL = "SELECT invoice_no FROM t_invoice_hdr WHERE invoice_dt = " + "'" + strGdate + "'";

                using (OdbcDataReader reader = dbConnector.CreateResultset(DeTools.gstrSQL))
                {
                    combo.Items.Clear();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            combo.Items.Add(reader["invoice_no"].ToString());
                        }
                    }

                    reader.Close();
                }

            }

            catch (Exception ex)
            {

                // Handle the exception or display an error message
                Console.WriteLine(ex.Message);
            }

        }

        public void check_temp_login_sytemname()
        {
            string getent_by = "";
            string getcomp_name = "";
            DbConnector dbConnector = new DbConnector();

            dbConnector.OpenConnection();

            bool check = false;

        }

        private void lblInvNo_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkThrRec.Checked)
            {
                checkboxState = "Y";

                btnDown.Enabled = true;
                btnDownAll.Enabled = true;
                btnUp.Enabled = true;
                btnUpAll.Enabled = true;
            }
            else
            {
                checkboxState = "N";

                btnDown.Enabled = false;
                btnDownAll.Enabled = false;
                btnUp.Enabled = false;
                btnUpAll.Enabled = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (cboInvNo.SelectedItem != null)
                {
                    selectedinvoice_No = cboInvNo.SelectedItem.ToString().Trim();
                    string bar_code = "";
                    int sr_no = 1;
                    dbConnector = new DbConnector();
                    DeTools.gstrSQL = "SELECT * FROM t_invoice_det WHERE invoice_no = '" + selectedinvoice_No + "'";

                    using (OdbcDataReader reader = dbConnector.CreateResultset(DeTools.gstrSQL))
                    {
                        // Clear the existing rows in the DataGridView
                        dbgItemDet.Rows.Clear();

                        // Iterate through the result set
                        while (reader.Read())
                        {

                            bar_code = reader["bar_code"].ToString().Trim();
                            // Add each row to the DataGridView
                            int rowIndex = dbgItemDet.Rows.Add();
                            dbgItemDet.Rows[rowIndex].Cells["srno"].Value = sr_no;
                            dbgItemDet.Rows[rowIndex].Cells["BarItemCode"].Value = bar_code;

                            dbConnector = new DbConnector();
                            DeTools.gstrSQL = "SELECT item_desc FROM m_item_det WHERE item_id = '" + bar_code + "' or  plu = '" + bar_code + "' or bar_code = '" + bar_code + "' ;";

                            using (OdbcDataReader reader_itemdesc = dbConnector.CreateResultset(DeTools.gstrSQL))
                            {
                                // Clear the existing rows in the DataGridView
                                //dbgItemDet.Rows.Clear();

                                // Iterate through the result set
                                while (reader_itemdesc.Read())
                                {
                                    dbgItemDet.Rows[rowIndex].Cells["Itemname"].Value = reader_itemdesc["item_desc"];
                                }
                            }

                            dbgItemDet.Rows[rowIndex].Cells["Qty"].Value = reader["qty"];
                            dbgItemDet.Rows[rowIndex].Cells["Mrp"].Value = reader["mrp"];
                            dbgItemDet.Rows[rowIndex].Cells["UnitPrice"].Value = reader["sale_price"];
                            dbgItemDet.Rows[rowIndex].Cells["Disc"].Value = reader["disc_per"];
                            dbgItemDet.Rows[rowIndex].Cells["DiscAmt"].Value = reader["disc_amt"];
                            dbgItemDet.Rows[rowIndex].Cells["Gst"].Value = reader["sale_tax_per"];
                            dbgItemDet.Rows[rowIndex].Cells["Cess"].Value = reader["cess_perc"];
                            dbgItemDet.Rows[rowIndex].Cells["Amount"].Value = reader["net_amt"];
                            dbgItemDet.Rows[rowIndex].Cells["Itemid"].Value = reader["item_id"];

                            // Add more columns as needed
                            sr_no++;
                        }
                    }

                    DeTools.gstrSQL = "SELECT * FROM t_invoice_hdr WHERE invoice_no = '" + selectedinvoice_No + "'";

                    using (OdbcDataReader readerhdr = dbConnector.CreateResultset(DeTools.gstrSQL))
                    {
                        // Iterate through the result set
                        while (readerhdr.Read())
                        {
                            rotInvAmt.Text = readerhdr["net_amt_after_disc"].ToString();
                            rotGrossAmt.Text = readerhdr["gross_amt"].ToString();
                            rotDisc.Text = readerhdr["disc_per"].ToString();
                            rotDiscAmt.Text = readerhdr["disc_amt"].ToString();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }


        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void T_Sale_Return_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblInvDate_Click(object sender, EventArgs e)
        {

        }

        private void frmT_Sale_Return_Load(object sender, EventArgs e)
        {
            DeTools.DisplayForm(this, 593, 831);
            this.Location = new Point(280, 0);
            System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();
            toolTip.SetToolTip(dbgItemDet, "Enter Item Details Here");
            toolTip.SetToolTip(dbgPayDet, "Enter Payment Details Here");


            Help.controlToHelpTopicMapping.Add(txtSRNo, "1027"); /////For Help ContextId///IMP...
            //txtDiscPer.Text = "0.00";
            //dbgPayDet.DataError += dbgPayDet_DataError;
            dtpInvDate.Value = DateTime.Today;
            PopulateDataGridViewWithComboBox();
            dtpSRDate.Value = DateTime.Today;
            FillCustCombo();
        }

        private void frmT_Sale_Return_FormClosed(object sender, FormClosedEventArgs e)
        {
            DeTools.DestroyToolbar(this);
            MainForm.Instance.mnuTBSRGenmenu.Enabled = true;
        }

        private void dtpInvDate_ValueChanged(object sender, EventArgs e)
        {
            if (chkThrRec.Checked)
            {
                selectedDate = dtpInvDate.Value.ToString("yyyy-MM-dd");
                FillInvCombo(cboInvNo, selectedDate);
            }
        }

        private void dbgItemDet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Clear existing rows in dbgItemDetRet
            //dbgItemDetRet.Rows.Clear();

            // Loop through all rows in dbgItemDet
            foreach (DataGridViewRow row in dbgItemDet.Rows)
            {
                // Skip the last row if it is a new row placeholder
                if (row.IsNewRow) continue;

                // Create a new row for dbgItemDetRet
                int rowIndex = dbgItemDetRet.Rows.Add();

                // Copy each cell value from dbgItemDet to dbgItemDetRet
                for (int colIndex = 0; colIndex < row.Cells.Count; colIndex++)
                {
                    dbgItemDetRet.Rows[rowIndex].Cells[colIndex].Value = row.Cells[colIndex].Value;

                }
                //// Add the value of the Amt2 column to totalAmt2
                //if (decimal.TryParse(dbgItemDetRet.Rows[rowIndex].Cells["Amt2"].Value?.ToString(), out decimal amt2Value))
                //{
                //    SRNetAmt += amt2Value;
                //}


            }

            // Reassign serial numbers in dbgItemDet
            ReassignSerialNumbers(dbgItemDet, "srno");

            // Reassign serial numbers in dbgItemDetRet
            ReassignSerialNumbers(dbgItemDetRet, "srno2");

            // Calculate the sum of Amt2 in dbgItemDetRet
            //totalAmt2 = CalculateTotalAmt2(dbgItemDetRet, "Amt2");

            // Display the totalAmt2 in the rotNetAmt TextBox
            //rotNetAmt.Text = totalAmt2.ToString("N2");

            rotNetAmt.Text = rotInvAmt.Text;
            dbgItemDet.Rows.Clear();
        }

        private void btnUpAll_Click(object sender, EventArgs e)
        {
            // Clear existing rows in dbgItemDetRet
            //dbgItemDet.Rows.Clear();
            // Variable to hold the sum of Amt2
            decimal totalAmt2 = 0;

            // Loop through all rows in dbgItemDet
            foreach (DataGridViewRow row in dbgItemDetRet.Rows)
            {
                // Skip the last row if it is a new row placeholder
                if (row.IsNewRow) continue;

                // Create a new row for dbgItemDetRet
                int rowIndex = dbgItemDet.Rows.Add();

                //// Copy each cell value from dbgItemDet to dbgItemDetRet
                //for (int colIndex = 0; colIndex < row.Cells.Count; colIndex++)
                //{
                //    dbgItemDet.Rows[rowIndex].Cells[colIndex].Value = row.Cells[colIndex].Value;

                //}



            }

            // Reassign serial numbers in dbgItemDet
            ReassignSerialNumbers(dbgItemDet, "srno");

            // Reassign serial numbers in dbgItemDetRet
            ReassignSerialNumbers(dbgItemDetRet, "srno2");

            // Calculate the sum of Amt2 in dbgItemDetRet
            totalAmt2 = CalculateTotalAmt2(dbgItemDetRet, "Amt2");

            // Display the totalAmt2 in the rotNetAmt TextBox N2 will do convert like 50-> 50.00
            // and also 1240-->1,240.00
            rotNetAmt.Text = totalAmt2.ToString("N2");

            dbgItemDetRet.Rows.Clear();
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            try
            {
                // List to hold the rows to be removed
                List<DataGridViewRow> rowsToRemove = new List<DataGridViewRow>();

                // Variable to hold the sum of Amt2
                decimal totalAmt2 = 0;

                // Loop through the selected rows in dbgItemDet
                foreach (DataGridViewRow selectedRow in dbgItemDet.SelectedRows)
                {
                    // Create a new row for dbgItemDetRet
                    int rowIndex = dbgItemDetRet.Rows.Add();

                    // Copy each cell value from dbgItemDet to dbgItemDetRet
                    foreach (DataGridViewCell cell in selectedRow.Cells)
                    {
                        dbgItemDetRet.Rows[rowIndex].Cells[cell.ColumnIndex].Value = cell.Value;
                    }

                    // Add the row to the list of rows to be removed
                    rowsToRemove.Add(selectedRow);
                }

                // Remove the selected rows from dbgItemDet
                foreach (var row in rowsToRemove)
                {
                    dbgItemDet.Rows.Remove(row);
                }

                // Reassign serial numbers in dbgItemDet
                ReassignSerialNumbers(dbgItemDet, "srno");

                // Reassign serial numbers in dbgItemDetRet
                ReassignSerialNumbers(dbgItemDetRet, "srno2");

                // Calculate the sum of Amt2 in dbgItemDetRet
                totalAmt2 = CalculateTotalAmt2(dbgItemDetRet, "Amt2");

                // Display the totalAmt2 in the rotNetAmt TextBox
                rotNetAmt.Text = totalAmt2.ToString("N2");

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ReassignSerialNumbers(DataGridView dgv, string serialColumnName)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                dgv.Rows[i].Cells[serialColumnName].Value = i + 1;
            }
        }

        // Method to calculate the total Amt2
        private decimal CalculateTotalAmt2(DataGridView dgv, string amt2ColumnName)
        {
            decimal total = 0;
            decimal discperval = rotDisc.Text.Trim().Length == 0 ? 0 : Convert.ToDecimal(rotDisc.Text.Trim());
            decimal discval = 0;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (decimal.TryParse(row.Cells[amt2ColumnName].Value?.ToString(), out decimal amt2Value))
                {

                    total += amt2Value;

                }
            }
            discval = (total * discperval) / 100;
            total = total - discval;
            return total;
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            try
            {
                // List to hold the rows to be removed
                // List to hold the rows to be removed
                List<DataGridViewRow> rowsToRemove = new List<DataGridViewRow>();

                // Variable to hold the sum of Amt2
                decimal totalAmt2 = 0;

                // Loop through the selected rows in dbgItemDetRet
                foreach (DataGridViewRow selectedRow in dbgItemDetRet.SelectedRows)
                {
                    // Create a new row for dbgItemDet
                    int rowIndex = dbgItemDet.Rows.Add();

                    // Copy each cell value from dbgItemDetRet to dbgItemDet
                    foreach (DataGridViewCell cell in selectedRow.Cells)
                    {
                        dbgItemDet.Rows[rowIndex].Cells[cell.ColumnIndex].Value = cell.Value;
                    }

                    // Add the row to the list of rows to be removed
                    rowsToRemove.Add(selectedRow);
                }

                // Remove the selected rows from dbgItemDetRet
                foreach (var row in rowsToRemove)
                {
                    dbgItemDetRet.Rows.Remove(row);
                }

                // Reassign serial numbers in dbgItemDet
                ReassignSerialNumbers(dbgItemDet, "srno");

                // Reassign serial numbers in dbgItemDetRet
                ReassignSerialNumbers(dbgItemDetRet, "srno2");

                // Calculate the sum of Amt2 in dbgItemDetRet
                totalAmt2 = CalculateTotalAmt2(dbgItemDetRet, "Amt2");

                // Display the totalAmt2 in the rotNetAmt TextBox N2 will do convert like 50-> 50.00
                // and also 1240-->1,240.00
                rotNetAmt.Text = totalAmt2.ToString("N2");

            }
            catch (Exception)
            {

                throw;
            }

        }

        //---for invoice---------------//
        // Transfer data to the main form's DataGridView
        private void TransferDataToMainForm(DataGridViewRow selectedRow)
        {
            // Assuming that targetGrid is your DataGridView in the main form
            DataGridView targetGrid = (DataGridView)DeTools.gobjActiveForm.Controls.Find("dbgItemDetRet", true).FirstOrDefault();

            // Get the current row index
            //int rowIndex = targetGrid.Rows.Add();
            int rowIndex = targetGrid.CurrentRow.Index;
            // Assuming you have a list of column indexes to transfer data
            List<int> columnIndexesToTransfer = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 }; // Add the column indexes you want to transfer

            foreach (int columnIndex in columnIndexesToTransfer)
            {
                // Check if the column index is within the bounds
                if (columnIndex >= 0 && columnIndex < selectedRow.Cells.Count)
                {
                    // Assuming that targetGrid has enough columns
                    targetGrid.Rows[rowIndex].Cells[2].Value = selectedRow.Cells[1].Value;
                    targetGrid.Rows[rowIndex].Cells[3].Value = "1"; // Quantity set to 1
                    targetGrid.Rows[rowIndex].Cells[4].Value = selectedRow.Cells[2].Value;
                    targetGrid.Rows[rowIndex].Cells[5].Value = selectedRow.Cells[3].Value;

                    // Calculate spqty
                    string mrpString = selectedRow.Cells[2].Value.ToString();
                    string qtyString = "1";
                    decimal spqtyDecimal = decimal.Parse(selectedRow.Cells[3].Value.ToString());
                    string spqtyString = (spqtyDecimal * decimal.Parse(qtyString)).ToString();

                    // Get barcode and mrp
                    string barcode = selectedRow.Cells[0].Value.ToString();
                    string mrp = mrpString;

                    // Count rows in m_item_det for the given barcode and mrp
                    string countQuery = "SELECT COUNT(*) FROM m_item_det WHERE bar_code = ? AND mrp = ? AND active_yn='Y'";
                    int rowCountnew = 0;

                    using (OdbcCommand countCmd1 = new OdbcCommand(countQuery, dbConnector.connection))
                    {
                        countCmd1.Parameters.AddWithValue("?", barcode);
                        countCmd1.Parameters.AddWithValue("?", mrp);

                        try
                        {
                            dbConnector.connection.Open();
                            rowCountnew = Convert.ToInt32(countCmd1.ExecuteScalar());

                            if (rowCountnew == 1)
                            {
                                // Retrieve data from m_item_det
                                string query = "SELECT item_id FROM m_item_det WHERE bar_code = ? AND mrp = ? AND active_yn='Y'";
                                using (OdbcCommand dataCmd = new OdbcCommand(query, dbConnector.connection))
                                {
                                    dataCmd.Parameters.AddWithValue("?", barcode);
                                    dataCmd.Parameters.AddWithValue("?", mrp);

                                    using (OdbcDataReader reader = dataCmd.ExecuteReader())
                                    {
                                        if (reader.Read())
                                        {
                                            string itemidval = reader["item_id"].ToString().Trim();

                                            // Retrieve discount percentage, cess percentage, and sale tax paid from m_item_hdr
                                            string discperQuery = "SELECT disc_per, cess_perc, sale_tax_paid FROM m_item_hdr WHERE item_id = ? AND active_yn='Y'";
                                            using (OdbcCommand discperCmd = new OdbcCommand(discperQuery, dbConnector.connection))
                                            {
                                                discperCmd.Parameters.AddWithValue("?", itemidval);

                                                using (OdbcDataReader discreader = discperCmd.ExecuteReader())
                                                {
                                                    if (discreader.Read())
                                                    {
                                                        string disc_perString = discreader["disc_per"].ToString();
                                                        string discamtString = ((decimal.Parse(spqtyString) * decimal.Parse(disc_perString)) / 100).ToString();

                                                        // Assign string values to the DataGridView cells
                                                        targetGrid.Rows[rowIndex].Cells[6].Value = disc_perString;
                                                        targetGrid.Rows[rowIndex].Cells[7].Value = discamtString;

                                                        // For the cess_perc, assuming it's also a decimal value
                                                        string cess_percString = discreader["cess_perc"].ToString();
                                                        targetGrid.Rows[rowIndex].Cells[8].Value = cess_percString;

                                                        // Retrieve tax_per from m_tax_type
                                                        string gstperQuery = "SELECT tax_per FROM m_tax_type WHERE tax_type_id = ?";
                                                        using (OdbcCommand gstperCmd = new OdbcCommand(gstperQuery, dbConnector.connection))
                                                        {
                                                            gstperCmd.Parameters.AddWithValue("?", discreader["sale_tax_paid"]);

                                                            using (OdbcDataReader gstreader = gstperCmd.ExecuteReader())
                                                            {
                                                                if (gstreader.Read())
                                                                {
                                                                    string tax_perString = gstreader["tax_per"].ToString();
                                                                    targetGrid.Rows[rowIndex].Cells[9].Value = tax_perString;
                                                                }
                                                            }
                                                        }

                                                        // Calculate the result and convert it to string
                                                        string resultString = (decimal.Parse(spqtyString) - decimal.Parse(discamtString)).ToString();

                                                        // Assign the string value to the cell
                                                        targetGrid.Rows[rowIndex].Cells[10].Value = resultString;
                                                        UpdateRotGAmt();
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
                            Console.WriteLine("Error: " + ex.Message);
                        }
                    }
                }

                else
                {
                    // Handle the case where the column index is out of bounds
                    // You can log a message or take appropriate action
                }
            }
        }

        public void UpdateRotGAmt()
        {
            decimal totalAmount = 0;
            decimal totalQty = 0;
            decimal totalMrp = 0;
            decimal totalDiscountAmt = 0;
            decimal amount = 0;

            foreach (DataGridViewRow row in dbgItemDetRet.Rows)
            {
                // Get the values from the current row
                decimal qty = Convert.ToDecimal(row.Cells[3].Value ?? 0); // Qty
                decimal mrp = Convert.ToDecimal(row.Cells[4].Value ?? 0); // MRP
                decimal salePrice = Convert.ToDecimal(row.Cells[5].Value ?? 0); // Sale Price
                decimal discountAmt = Convert.ToDecimal(row.Cells[7].Value ?? 0); // Discount Amount

                // Calculate the amount for the current row
                decimal rowAmount = qty * salePrice;
                decimal rowMRPAmount = qty * mrp;

                // Update the total amount
                totalAmount += rowAmount;

                // Update other totals
                totalQty += qty;
                totalMrp += rowMRPAmount;
                totalDiscountAmt += discountAmt;
            }

            // Update the UI elements with the totals
            //rotGAmt.Text = totalAmount.ToString("0.00");
            //rotTotQty.Text = totalQty.ToString();
            //rotTotmrp.Text = totalMrp.ToString("0.00");

            //rotNOI.Text = (dbgItemDet.Rows.Count - 1).ToString();
            //txtDiscAmt.Text = totalDiscountAmt.ToString();

            // Calculate the net amount
            decimal netAmount = totalAmount - totalDiscountAmt - overalldiscamt;

            //rotNetAmt.Text = netAmount.ToString("0.00");

            // Calculate the rounded-off amount
            decimal roundedAmount = Math.Round(netAmount, roundoffval);
            rotNetAmt.Text = roundedAmount.ToString();

            //rotTotdisc.Text = (totalMrp - roundedAmount).ToString();
            //// Calculate the round-off difference
            //decimal roundOffDifference = netAmount - roundedAmount;
            //rotRO.Text = roundOffDifference.ToString("0.00");


        }


        private void DisplayDataTable(DataTable originalTable, params string[] columnsToShow)
        {
            // Create a new DataTable with specified columns
            DataTable displayTable = new DataTable();
            foreach (string column in columnsToShow)
            {
                displayTable.Columns.Add(column, originalTable.Columns[column].DataType);
            }

            // Copy rows from original DataTable to display DataTable
            foreach (DataRow row in originalTable.Rows)
            {
                DataRow newRow = displayTable.NewRow();
                foreach (string column in columnsToShow)
                {
                    newRow[column] = row[column];
                }
                displayTable.Rows.Add(newRow);
            }

            // Create the popup window
            Form dataTableForm = new Form();

            // Create the DataGridView
            DataGridView dataGridView = new DataGridView();
            dataGridView.DataSource = displayTable;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; // Auto-size columns
            dataGridView.CellDoubleClick += (sender, e) =>
            {
                if (e.RowIndex >= 0 && e.RowIndex < dataGridView.Rows.Count)
                {
                    // Transfer data from the double-clicked row to the main form's DataGridView
                    TransferDataToMainForm(dataGridView.Rows[e.RowIndex]);
                    dataTableForm.Close(); // Close the popup window after transferring data
                }
            };

            // Allow the user to press Enter key to select a row
            dataGridView.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter && dataGridView.CurrentRow != null)
                {
                    // Transfer data from the selected row to the main form's DataGridView
                    TransferDataToMainForm(dataGridView.CurrentRow);
                    dataTableForm.Close(); // Close the popup window after transferring data
                }
            };


            // Set the size of the popup window to exactly fit the DataGridView
            dataTableForm.ClientSize = new System.Drawing.Size(dataGridView.Width + 140, dataGridView.Height + 20);

            // Set the DataGridView to fill the entire area of the form
            dataGridView.Dock = DockStyle.Fill;

            // Add the DataGridView to the popup window
            dataTableForm.Controls.Add(dataGridView);

            // Show the popup window
            dataTableForm.ShowDialog();
        }

        private void SearchItemByID(string itemID, out bool itemFound, out string itemnm, out string itemid, out string mrp, out string sale_price, out string disc_per, out string sale_tax_paid, out string cess_perc, out DataTable dataTable)
        {
            dbConnector = new DbConnector();
            dbConnector.connection = new OdbcConnection(dbConnector.connectionString);
            itemFound = false;
            itemid = null;
            itemnm = null;
            mrp = null;
            sale_price = null;
            disc_per = null;
            sale_tax_paid = null;
            cess_perc = null;
            dataTable = new DataTable();

            string countQuery = "SELECT COUNT(*) FROM m_item_hdr WHERE item_id = '" + itemID + "' AND active_yn='Y'";
            string dataQuery = "SELECT * FROM m_item_hdr WHERE item_id = '" + itemID + "' AND active_yn='Y'";


            using (OdbcCommand countCmd = new OdbcCommand(countQuery, dbConnector.connection))
            {
                try
                {
                    dbConnector.connection.Open();
                    rowCount = Convert.ToInt32(countCmd.ExecuteScalar());
                    if (rowCount > 1)
                    {
                        // If multiple rows found, load data into DataTable
                        using (OdbcDataAdapter adapter = new OdbcDataAdapter(dataQuery, dbConnector.connection))
                        {
                            adapter.Fill(dataTable);
                        }
                        itemFound = true;
                    }
                    else if (rowCount == 1)
                    {
                        // Single row found, retrieve data
                        using (OdbcCommand dataCmd = new OdbcCommand(dataQuery, dbConnector.connection))
                        using (OdbcDataReader reader = dataCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                itemid = reader.GetString(reader.GetOrdinal("item_id"));
                                itemnm = reader.GetString(reader.GetOrdinal("item_desc"));
                                mrp = reader.GetString(reader.GetOrdinal("mrp"));
                                sale_price = reader.GetString(reader.GetOrdinal("sale_price"));
                                disc_per = reader.GetString(reader.GetOrdinal("disc_per"));
                                sale_tax_paid = reader.GetString(reader.GetOrdinal("sale_tax_paid"));
                                cess_perc = reader.GetString(reader.GetOrdinal("cess_perc"));

                                // You can retrieve other fields similarly
                                Console.WriteLine($"ItemId: {itemid}");
                                itemFound = true; // Set the flag to true
                            }
                        }
                    }
                    else
                    {
                        // No items found
                        Console.WriteLine("No items found.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    dbConnector.connection.Close();
                }
            }
        }

        private void SearchItemByPLU(string PLU, out bool itempluFound, out string itemnm, out string plu, out string mrp, out string sp, out string item_id, out string disc_per, out string gst_per, out string cess_per)
        {
            dbConnector = new DbConnector();
            dbConnector.connection = new OdbcConnection(dbConnector.connectionString);
            itempluFound = false;
            plu = null;
            itemnm = null;
            item_id = null;
            mrp = null;
            sp = null;
            disc_per = null;
            gst_per = null;
            cess_per = null;

            //    dataTable1 = new DataTable();

            string countQuery1 = "SELECT COUNT(*) FROM m_item_det WHERE plu = '" + PLU + "' AND active_yn='Y'";
            string dataQuery1 = "SELECT * FROM m_item_det WHERE plu = '" + PLU + "' AND active_yn='Y'";

            using (OdbcCommand countCmd = new OdbcCommand(countQuery1, dbConnector.connection))
            {
                try
                {
                    rowCount = 0;
                    dbConnector.connection.Open();
                    rowCount = Convert.ToInt32(countCmd.ExecuteScalar());
                    if (rowCount == 1)
                    {
                        // Single row found, retrieve data
                        using (OdbcCommand dataCmd = new OdbcCommand(dataQuery1, dbConnector.connection))
                        using (OdbcDataReader reader = dataCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                plu = reader.GetString(reader.GetOrdinal("plu"));
                                itemnm = reader.GetString(reader.GetOrdinal("item_desc"));
                                item_id = reader.GetString(reader.GetOrdinal("item_id"));
                                mrp = reader.GetString(reader.GetOrdinal("mrp"));
                                sp = reader.GetString(reader.GetOrdinal("sale_price"));
                                //disc_per= reader.GetString(reader.GetOrdinal("sale_price"));

                                // Retrieve discount percentage, cess percentage, and sale tax paid from m_item_hdr
                                string discperQuery = "SELECT disc_per, cess_perc, sale_tax_paid FROM m_item_hdr WHERE item_id = ? AND active_yn='Y'";
                                using (OdbcCommand discperCmd = new OdbcCommand(discperQuery, dbConnector.connection))
                                {
                                    discperCmd.Parameters.AddWithValue("?", item_id);

                                    using (OdbcDataReader discreader = discperCmd.ExecuteReader())
                                    {
                                        if (discreader.Read())
                                        {
                                            disc_per = discreader["disc_per"].ToString();


                                            // For the cess_perc, assuming it's also a decimal value
                                            cess_per = discreader["cess_perc"].ToString();


                                            // Retrieve tax_per from m_tax_type
                                            string gstperQuery = "SELECT tax_per FROM m_tax_type WHERE tax_type_id = ?";
                                            using (OdbcCommand gstperCmd = new OdbcCommand(gstperQuery, dbConnector.connection))
                                            {
                                                gstperCmd.Parameters.AddWithValue("?", discreader["sale_tax_paid"]);

                                                using (OdbcDataReader gstreader = gstperCmd.ExecuteReader())
                                                {
                                                    if (gstreader.Read())
                                                    {
                                                        gst_per = gstreader["tax_per"].ToString();
                                                    }
                                                }
                                            }

                                        }
                                    }
                                }


                                // You can retrieve other fields similarly
                                Console.WriteLine($"ItemPLU: {plu}");
                                itempluFound = true; // Set the flag to true
                            }
                        }
                    }
                    else
                    {
                        // No items found
                        Console.WriteLine("No items found.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    dbConnector.connection.Close();
                }
            }
        }



        private void SearchItemByBarcode(string barcode, out bool barFound, out string itemnm, out string bar, out DataTable dataTable)
        {
            dbConnector = new DbConnector();
            // dbConnector.connectionString= new OdbcConnection();
            dbConnector.connection = new OdbcConnection(dbConnector.connectionString);
            bar = null;
            itemnm = null;
            barFound = false;
            dataTable = new DataTable();
            string countQuery = "SELECT COUNT(*) FROM m_item_det WHERE bar_code = '" + barcode + "' AND active_yn='Y'";
            string query = "SELECT * FROM m_item_det WHERE bar_code = '" + barcode + "' AND active_yn='Y'";

            using (OdbcCommand countCmd = new OdbcCommand(countQuery, dbConnector.connection))
            {


                try
                {
                    rowCount = 0;
                    dbConnector.connection.Open();
                    rowCount = Convert.ToInt32(countCmd.ExecuteScalar());
                    if (rowCount > 1)
                    {
                        // If multiple rows found, load data into DataTable
                        using (OdbcDataAdapter adapter = new OdbcDataAdapter(query, dbConnector.connection))
                        {
                            adapter.Fill(dataTable);
                        }
                        barFound = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    dbConnector.connection.Close();
                }
            }
        }

        private void SearchItemByBarcodeSingle(string barcode, out bool barFound, out string itemnm, out string bar, out string mrp, out string sp, out string item_id, out string disc_per, out string gst_per, out string cess_per)
        {
            dbConnector = new DbConnector();
            // dbConnector.connectionString= new OdbcConnection();
            dbConnector.connection = new OdbcConnection(dbConnector.connectionString);
            bar = null;
            itemnm = null;
            barFound = false;
            item_id = null;
            mrp = null;
            sp = null;
            disc_per = null;
            gst_per = null;
            cess_per = null;


            //string countQuery = "SELECT COUNT(*) FROM m_item_det WHERE bar_code = '" + barcode + "' AND active_yn='Y'";
            string query = "SELECT * FROM m_item_det WHERE bar_code = '" + barcode + "' AND active_yn='Y'";

            try
            {
                rowCount = 0;
                dbConnector.connection.Open();

                // Single row found, retrieve data
                using (OdbcCommand dataCmd = new OdbcCommand(query, dbConnector.connection))
                using (OdbcDataReader reader = dataCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        bar = reader.GetString(reader.GetOrdinal("bar_code"));
                        itemnm = reader.GetString(reader.GetOrdinal("item_desc"));
                        item_id = reader.GetString(reader.GetOrdinal("item_id"));
                        mrp = reader.GetString(reader.GetOrdinal("mrp"));
                        sp = reader.GetString(reader.GetOrdinal("sale_price"));
                        //disc_per= reader.GetString(reader.GetOrdinal("sale_price"));

                        // Retrieve discount percentage, cess percentage, and sale tax paid from m_item_hdr
                        string discperQuery = "SELECT disc_per, cess_perc, sale_tax_paid FROM m_item_hdr WHERE item_id = ? AND active_yn='Y'";
                        using (OdbcCommand discperCmd = new OdbcCommand(discperQuery, dbConnector.connection))
                        {
                            discperCmd.Parameters.AddWithValue("?", item_id);

                            using (OdbcDataReader discreader = discperCmd.ExecuteReader())
                            {
                                if (discreader.Read())
                                {
                                    disc_per = discreader["disc_per"].ToString();


                                    // For the cess_perc, assuming it's also a decimal value
                                    cess_per = discreader["cess_perc"].ToString();


                                    // Retrieve tax_per from m_tax_type
                                    string gstperQuery = "SELECT tax_per FROM m_tax_type WHERE tax_type_id = ?";
                                    using (OdbcCommand gstperCmd = new OdbcCommand(gstperQuery, dbConnector.connection))
                                    {
                                        gstperCmd.Parameters.AddWithValue("?", discreader["sale_tax_paid"]);

                                        using (OdbcDataReader gstreader = gstperCmd.ExecuteReader())
                                        {
                                            if (gstreader.Read())
                                            {
                                                gst_per = gstreader["tax_per"].ToString();
                                            }
                                        }
                                    }

                                }
                            }
                        }



                        // You can retrieve other fields similarly
                        Console.WriteLine($"ItemId: {bar}");
                        barFound = true; // Set the flag to true
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                dbConnector.connection.Close();
            }

        }


        private void dbgItemDetRet_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                // Check if the edited cell is in a column where formatting is desired
                if (e.ColumnIndex >= 3)
                {
                    // Get the entered value
                    object rawValue = dbgItemDetRet.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    decimal spValue = 0;
                    mrpValue = Convert.ToDecimal(dbgItemDetRet.Rows[e.RowIndex].Cells[4].Value);
                    spValue = Convert.ToDecimal(dbgItemDetRet.Rows[e.RowIndex].Cells[5].Value);
                    // Format the value as a decimal with two decimal places
                    if (rawValue != null && decimal.TryParse(rawValue.ToString(), out decimal enteredValue))
                    {
                        // Ensure Column 5 (SP) is not greater than Column 4 (MRP)
                        if (e.ColumnIndex == 5 && flaggotonextcell == 1)
                        {
                            // Reset the value to MRP
                            dbgItemDetRet.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = mrpValue;
                        }

                        dbgItemDetRet.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = enteredValue.ToString("0.00");
                        dbgItemDetRet.Rows[e.RowIndex].Cells[10].Value = (enteredValue * spValue).ToString("0.00");

                    }
                }

                // Check if the edited cell is in the second column (index 1)
                if (e.ColumnIndex == 1 && e.RowIndex >= 0)
                {
                    if (dbgItemDetRet.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() != null && dbgItemDetRet.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() != "")
                    {


                        string userInput = dbgItemDetRet.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();

                        bool itemFound;
                        bool barFound;
                        DataTable dataTable;
                        string bar_code = null;
                        string bar = null;
                        string itemnm = null;
                        string itemid = null;
                        string plu;
                        bool itempluFound;
                        string mrp = null;
                        string sale_price = null;
                        string disc_per = null;
                        string sale_tax_paid = null;
                        string cess_perc = null;
                        SearchItemByID(userInput, out itemFound, out itemnm, out itemid, out mrp, out sale_price, out disc_per, out sale_tax_paid, out cess_perc, out dataTable);

                        // If item not found by ID, search by barcode
                        if (!itemFound)
                        {
                            SearchItemByBarcode(userInput, out barFound, out itemnm, out bar, out dataTable);


                            if (!barFound)
                            {
                                SearchItemByBarcodeSingle(userInput, out barFound, out itemnm, out bar, out mrp, out sale_price, out itemid, out disc_per, out sale_tax_paid, out cess_perc);

                                dbgItemDetRet.Rows[e.RowIndex].Cells[1].Value = bar; // Barcode
                                dbgItemDetRet.Rows[e.RowIndex].Cells[2].Value = itemnm; // Item Name
                                dbgItemDetRet.Rows[e.RowIndex].Cells[3].Value = "1"; // Default Quantity
                                dbgItemDetRet.Rows[e.RowIndex].Cells[4].Value = mrp;
                                dbgItemDetRet.Rows[e.RowIndex].Cells[5].Value = sale_price;
                                dbgItemDetRet.Rows[e.RowIndex].Cells[6].Value = disc_per;
                                dbgItemDetRet.Rows[e.RowIndex].Cells[7].Value = "0.00"; //discamt                        
                                decimal salePrice = Convert.ToDecimal(dbgItemDetRet.Rows[e.RowIndex].Cells[5].Value ?? "0"); // Sale price
                                decimal discountPercent = Convert.ToDecimal(disc_per ?? "0"); // Discount percent
                                decimal discountAmount = salePrice * discountPercent / 100; // Calculate discount amount

                                dbgItemDetRet.Rows[e.RowIndex].Cells[7].Value = discountAmount.ToString("0.00"); // Set discount amount

                                decimal salePrice1 = Convert.ToDecimal(dbgItemDetRet.Rows[e.RowIndex].Cells[5].Value ?? "0"); // Sale price
                                decimal discPerValue;
                                if (Decimal.TryParse(disc_per, out discPerValue) && discPerValue > 0.00M)
                                {
                                    decimal discountAmount1 = Convert.ToDecimal(dbgItemDetRet.Rows[e.RowIndex].Cells[7].Value ?? "0"); // Discount amount
                                    decimal discountPercent1 = (discountAmount1 / salePrice1) * 100; // Calculate discount percent
                                    dbgItemDetRet.Rows[e.RowIndex].Cells[6].Value = discountPercent1.ToString("0.00"); // Set discount percent


                                }


                                dbgItemDetRet.Rows[e.RowIndex].Cells[8].Value = sale_tax_paid;
                                dbgItemDetRet.Rows[e.RowIndex].Cells[9].Value = cess_perc;
                                dbgItemDetRet.Rows[e.RowIndex].Cells[10].Value = "0"; //amount
                                                                                      //dbgItemDetRet.Rows[e.RowIndex].Cells[11].Value = "0"; //gstamount
                                dbgItemDetRet.Rows[e.RowIndex].Cells[12].Value = "0"; //cessamount
                                dbgItemDetRet.Rows[e.RowIndex].Cells[13].Value = itemid;



                                if (!barFound)
                                {
                                    SearchItemByPLU(userInput, out itempluFound, out itemnm, out plu, out mrp, out sale_price, out itemid, out disc_per, out sale_tax_paid, out cess_perc);
                                    //if (!itempluFound)
                                    //{
                                    //    //MessageBox.Show("This Item Does Not Exists!", "Item Not Exists", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    //}
                                    if (itempluFound)
                                    {

                                        dbgItemDetRet.Rows[e.RowIndex].Cells[1].Value = plu; // Barcode
                                        dbgItemDetRet.Rows[e.RowIndex].Cells[2].Value = itemnm; // Item Name
                                        dbgItemDetRet.Rows[e.RowIndex].Cells[3].Value = "1"; // Default Quantity
                                        dbgItemDetRet.Rows[e.RowIndex].Cells[4].Value = mrp;
                                        dbgItemDetRet.Rows[e.RowIndex].Cells[5].Value = sale_price;
                                        dbgItemDetRet.Rows[e.RowIndex].Cells[6].Value = disc_per;
                                        dbgItemDetRet.Rows[e.RowIndex].Cells[7].Value = "0.00"; //discamt                        
                                        decimal salePricee = Convert.ToDecimal(dbgItemDetRet.Rows[e.RowIndex].Cells[5].Value ?? "0"); // Sale price
                                        decimal discountPercentt = Convert.ToDecimal(disc_per ?? "0"); // Discount percent
                                        decimal discountAmountt = salePricee * discountPercentt / 100; // Calculate discount amount

                                        dbgItemDetRet.Rows[e.RowIndex].Cells[7].Value = discountAmount.ToString("0.00"); // Set discount amount

                                        decimal salePrice11 = Convert.ToDecimal(dbgItemDetRet.Rows[e.RowIndex].Cells[5].Value ?? "0"); // Sale price
                                        decimal discPerValuee;
                                        if (Decimal.TryParse(disc_per, out discPerValuee) && discPerValuee > 0.00M)
                                        {
                                            decimal discountAmount1 = Convert.ToDecimal(dbgItemDetRet.Rows[e.RowIndex].Cells[7].Value ?? "0"); // Discount amount
                                            decimal discountPercent1 = (discountAmount1 / salePrice11) * 100; // Calculate discount percent
                                            dbgItemDetRet.Rows[e.RowIndex].Cells[6].Value = discountPercent1.ToString("0.00"); // Set discount percent


                                        }


                                        dbgItemDetRet.Rows[e.RowIndex].Cells[8].Value = sale_tax_paid;
                                        dbgItemDetRet.Rows[e.RowIndex].Cells[9].Value = cess_perc;
                                        dbgItemDetRet.Rows[e.RowIndex].Cells[10].Value = "0"; //amount
                                                                                              //dbgItemDetRet.Rows[e.RowIndex].Cells[11].Value = "0"; //gstamount
                                        dbgItemDetRet.Rows[e.RowIndex].Cells[12].Value = "0"; //cessamount
                                        dbgItemDetRet.Rows[e.RowIndex].Cells[13].Value = itemid;
                                    }
                                }


                            }
                            //else
                            //{
                            //    // Populate columns with retrieved data
                            //    if (rowCount > 1)
                            //    {
                            //        DisplayDataTable(dataTable, "bar_code", "item_desc", "mrp", "sale_price");

                            //    }
                            //    else
                            //    {
                            //        dbgItemDetRet.Rows[e.RowIndex].Cells[1].Value = bar; // Barcode
                            //        dbgItemDetRet.Rows[e.RowIndex].Cells[2].Value = itemnm; // Item Name
                            //        dbgItemDetRet.Rows[e.RowIndex].Cells[3].Value = "1"; // Default Quantity

                            //    }
                            //}
                        }

                        else
                        {
                            if (rowCount > 1)
                            {
                                DisplayDataTable(dataTable, "item_id", "item_desc", "mrp", "sale_price");
                            }
                            else
                            {

                                // Populate columns with retrieved data
                                dbgItemDetRet.Rows[e.RowIndex].Cells[1].Value = itemid; // Itemid
                                dbgItemDetRet.Rows[e.RowIndex].Cells[2].Value = itemnm; // Item Name
                                dbgItemDetRet.Rows[e.RowIndex].Cells[3].Value = "1"; // Default Quantity
                                dbgItemDetRet.Rows[e.RowIndex].Cells[4].Value = mrp;
                                dbgItemDetRet.Rows[e.RowIndex].Cells[5].Value = sale_price;
                                dbgItemDetRet.Rows[e.RowIndex].Cells[6].Value = disc_per;
                                dbgItemDetRet.Rows[e.RowIndex].Cells[7].Value = "0.00"; //discamt                        
                                decimal salePrice = Convert.ToDecimal(dbgItemDetRet.Rows[e.RowIndex].Cells[5].Value ?? "0"); // Sale price
                                decimal discountPercent = Convert.ToDecimal(disc_per ?? "0"); // Discount percent
                                decimal discountAmount = salePrice * discountPercent / 100; // Calculate discount amount

                                dbgItemDetRet.Rows[e.RowIndex].Cells[7].Value = discountAmount.ToString("0.00"); // Set discount amount

                                decimal salePrice1 = Convert.ToDecimal(dbgItemDetRet.Rows[e.RowIndex].Cells[5].Value ?? "0"); // Sale price
                                decimal discPerValue;
                                if (Decimal.TryParse(disc_per, out discPerValue) && discPerValue > 0.00M)
                                {
                                    decimal discountAmount1 = Convert.ToDecimal(dbgItemDetRet.Rows[e.RowIndex].Cells[7].Value ?? "0"); // Discount amount
                                    decimal discountPercent1 = (discountAmount1 / salePrice1) * 100; // Calculate discount percent
                                    dbgItemDetRet.Rows[e.RowIndex].Cells[6].Value = discountPercent1.ToString("0.00"); // Set discount percent
                                }


                                dbgItemDetRet.Rows[e.RowIndex].Cells[8].Value = sale_tax_paid;
                                dbgItemDetRet.Rows[e.RowIndex].Cells[9].Value = cess_perc;
                                dbgItemDetRet.Rows[e.RowIndex].Cells[10].Value = "0"; //amount
                                                                                      //dbgItemDetRet.Rows[e.RowIndex].Cells[11].Value = "0"; //gstamount
                                dbgItemDetRet.Rows[e.RowIndex].Cells[12].Value = "0"; //cessamount
                                dbgItemDetRet.Rows[e.RowIndex].Cells[13].Value = itemid;

                            }
                        }
                    }
                }
            }
            catch (NullReferenceException)
            {


            }
        }

        private void dbgItemDetRet_CellEnter(object sender, DataGridViewCellEventArgs e)
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

        private void dbgItemDetRet_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {


                if (e.ColumnIndex == 5)
                {
                    // Check if SP is greater than MRP
                    decimal enteredValue = Convert.ToDecimal(e.FormattedValue);
                    mrpValue = Convert.ToDecimal(dbgItemDetRet.Rows[e.RowIndex].Cells[4].Value);
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
                        //   disccal();
                    }
                }
            }
            catch (Exception)
            {


            }
        }

        private void dbgItemDetRet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!cellValueChangedInProgress)
            {
                cellValueChangedInProgress = true;

                // Your event handling code here
                UpdateRotGAmt();
                //disccal();

                cellValueChangedInProgress = false;
                //UpdateDataGridViewStatus();
            }
        }

        private void cboCust_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCust.SelectedItem != null)
            {
                selectedCustomer = cboCust.SelectedItem as string;

                if (!String.IsNullOrEmpty(selectedCustomer))
                {
                    if (dbgPayDet.Enabled)
                    {
                        foreach (DataGridViewRow row in dbgPayDet.Rows)
                        {
                            if (row.IsNewRow) continue; // Skip the new row placeholder

                            string getpaymm = row.Cells[1].Value?.ToString(); // Use ?.ToString() to handle potential null values
                            if (!String.IsNullOrEmpty(getpaymm))
                            {
                                row.Cells["Custid"].Value = selectedCustomer;
                            }
                        }
                    }


                }
                General general = new General();
                //string desc = GetDescCust("m_customer", "cust_id", "cust_name", "C", cboCust.SelectedItem.ToString().Trim());
                DataRow customerData = GetCustomerData("m_customer", "cust_id", "C", selectedCustomer);

                if (customerData != null)
                {
                    custName = customerData["cust_name"].ToString().Trim();
                    custPhoneNo = customerData["phone_1"].ToString().Trim();
                    custAdd1 = customerData["address_1"].ToString().Trim();
                    custAdd2 = customerData["address_2"].ToString().Trim();
                    custEmail = customerData["email"].ToString().Trim();
                }


                rotInvCust.Text = custName;
                rotInvCust.Text = custName;
                //txtAddress.Text = custAdd1 + custAdd2;


            }

            // Get the selected item from cboCust
            string selectedItem = cboCust.SelectedItem?.ToString();
            for (int i = 0; i < dbgPayDet.Rows.Count - 1; i++)
            {
                // Check if there is any selected item in the Paymod column ComboBox in dbgPayDet
                foreach (DataGridViewRow row in dbgPayDet.Rows)
                {
                    DataGridViewComboBoxCell paymodCell = (DataGridViewComboBoxCell)row.Cells["Paymod"];
                    if (paymodCell.Value != null)
                    {
                        // Update the corresponding 7th column ComboBox in the same row
                        // DataGridViewComboBoxCell custIDCell = (DataGridViewComboBoxCell)row.Cells["CustId"];

                        selectedCustomer = cboCust.SelectedItem as string;

                        if (!String.IsNullOrEmpty(selectedCustomer))
                        {

                            row.Cells["Custid"].Value = selectedCustomer;

                        }
                        // Clear the DataSource property
                        //custIDCell.DataSource = null;

                        // Clear any existing item in the 7th column ComboBox
                        //custIDCell.Items.Clear();

                        //if (!string.IsNullOrEmpty(selectedItem) && selectedItem != null)
                        //{
                        //    // Add the selected item from cboCust to the 7th column ComboBox
                        //    custIDCell.Items.Add(selectedItem);
                        //}

                        //// Automatically select the added item in the 7th column ComboBox
                        //custIDCell.Value = selectedItem;
                    }
                }
            }
        }

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
                    if (reader != null && reader.HasRows)
                    {
                        reader.Read(); // Read the first row
                        result = new DataTable().NewRow();

                        // Create DataTable columns based on database schema
                        DataTable schemaTable = reader.GetSchemaTable();
                        foreach (DataRow schemaRow in schemaTable.Rows)
                        {
                            string columnName = schemaRow["ColumnName"].ToString();
                            Type columnType = (Type)schemaRow["DataType"];
                            result.Table.Columns.Add(columnName, columnType);
                        }

                        // Populate DataRow with values from database
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            result[i] = reader[i];
                        }
                    }
                }
            }

            return result;
        }

        private void cboCust_DropDown(object sender, EventArgs e)
        {
            FillCustCombo();
        }


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

        private string GetDescription(string custId)
        {
            // You can implement the logic to retrieve the description based on custId
            General general = new General();
            return GetDescCust("m_customer", "cust_id", "*", "C", custId);
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
                if (currentAmount == Decimal.Parse(rotNetAmt.Text))
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

            // Check if the edited cell is in the 'Paymod' column
            if (e.ColumnIndex == dbgPayDet.Columns["Paymod"].Index && e.RowIndex == 0)
            {
                // Set the 'PayAmt' in the first row directly from rotPayAmt.Text
                dbgPayDet.Rows[0].Cells["PayAmt"].Value = rotNetAmt.Text;
            }
        }

        private void dbgPayDet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the changed cell is in the cash tend column and not in the header row
            if (e.ColumnIndex == 3 && e.RowIndex != -1)
            {
                DataGridViewCell cashTendCell = dbgPayDet.Rows[e.RowIndex].Cells[e.ColumnIndex];
                DataGridViewCell amountCell = dbgPayDet.Rows[e.RowIndex].Cells[2]; // Assuming amount is in the third column

                // Check if the cash tend cell value is not null and can be converted to decimal
                if (cashTendCell.Value != null && decimal.TryParse(cashTendCell.Value.ToString(), out decimal cashTend))
                {
                    // Calculate refund amount
                    decimal amount = Convert.ToDecimal(amountCell.Value ?? 0); // Default to 0 if amount is null
                    decimal refundAmount = cashTend - amount;

                    // Set the refund amount in the fifth column (index 4)
                    dbgPayDet.Rows[e.RowIndex].Cells[4].Value = refundAmount;
                }

            }
            if (e.ColumnIndex == 1 && e.RowIndex != -1)
            {

                DataGridViewComboBoxCell comboBoxCellpaym = dbgPayDet.Rows[e.RowIndex].Cells[1] as DataGridViewComboBoxCell;

                // Check if the ComboBoxCell is not null
                if (comboBoxCellpaym != null)
                {
                    string selectedValuepaym = comboBoxCellpaym.Value?.ToString() ?? "";

                    // Check if the cell at index 6 is a TextBox cell
                    //DataGridViewTextBoxCell textBoxCell = dbgPayDet.Rows[e.RowIndex].Cells[6] as DataGridViewTextBoxCell;
                    //if (textBoxCell != null)
                    //{
                    paymodtxt = selectedValuepaym;
                    //}
                }

                dbConnector = new DbConnector();
                // dbConnector.connectionString= new OdbcConnection();
                dbConnector.connection = new OdbcConnection(dbConnector.connectionString);
                dbConnector.connection.Open();

                if (paymodtxt == "Coup" && paymodtxt != "")
                {

                    string coupon = "select coup_id from m_coupon";
                    using (OdbcCommand cmd = new OdbcCommand(coupon, dbConnector.connection))
                    {
                        OdbcDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                // Assuming the data type of coup_id is string
                                string coupId = reader["coup_id"].ToString();

                                // Assuming dataGridView1 is the name of your DataGridView
                                DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)dbgPayDet.Rows[e.RowIndex].Cells[7];
                                // Check if the second column's combo box cell value is "CC"
                                if (dbgPayDet.Rows[e.RowIndex].Cells[2].Value != "")
                                {
                                    // Add the coup_id from the database to the Items collection of the ComboBoxCell
                                    comboBoxCell.Items.Add(coupId);
                                }
                                // Set the cell to be editable
                                comboBoxCell.ReadOnly = false;
                            }

                        }

                    }
                }

                if (paymodtxt == "CC" && paymodtxt != "")
                {
                    string ccode = "select cc_id from m_cr_card";
                    using (OdbcCommand cmd1 = new OdbcCommand(ccode, dbConnector.connection))
                    {
                        OdbcDataReader reader1 = cmd1.ExecuteReader();
                        if (reader1.HasRows)
                        {
                            while (reader1.Read())
                            {
                                // Assuming the data type of coup_id is string
                                string ccId = reader1["cc_id"].ToString();

                                // Assuming dataGridView1 is the name of your DataGridView
                                DataGridViewComboBoxCell comboBoxCell2 = (DataGridViewComboBoxCell)dbgPayDet.Rows[e.RowIndex].Cells[5];

                                // Check if the second column's combo box cell value is "CC"
                                if (dbgPayDet.Rows[e.RowIndex].Cells[2].Value != "")
                                {
                                    // Add the cc_id from the database to the Items collection of the ComboBoxCell
                                    comboBoxCell2.Items.Add(ccId);
                                }
                                // Set the cell to be editable
                                comboBoxCell2.ReadOnly = false;
                            }

                        }

                    }

                }
            }

            if (e.ColumnIndex == 5 && e.RowIndex != -1) // Assuming the ComboBoxCell is in column index 5
            {
                DataGridViewComboBoxCell comboBoxCell = dbgPayDet.Rows[e.RowIndex].Cells[5] as DataGridViewComboBoxCell;

                // Check if the ComboBoxCell is not null
                if (comboBoxCell != null)
                {
                    string selectedValue = comboBoxCell.Value?.ToString() ?? "";

                    // Check if the cell at index 6 is a TextBox cell
                    //DataGridViewTextBoxCell textBoxCell = dbgPayDet.Rows[e.RowIndex].Cells[6] as DataGridViewTextBoxCell;
                    //if (textBoxCell != null)
                    //{
                    ccodetxt = selectedValue;
                    //}
                }
            }

            if (e.ColumnIndex == 7 && e.RowIndex != -1) // Assuming the ComboBoxCell is in column index 5
            {
                DataGridViewComboBoxCell comboBoxCellcoup = dbgPayDet.Rows[e.RowIndex].Cells[7] as DataGridViewComboBoxCell;

                // Check if the ComboBoxCell is not null
                if (comboBoxCellcoup != null)
                {
                    string selectedValuecoup = comboBoxCellcoup.Value?.ToString() ?? "";

                    // Check if the cell at index 6 is a TextBox cell
                    //DataGridViewTextBoxCell textBoxCell = dbgPayDet.Rows[e.RowIndex].Cells[6] as DataGridViewTextBoxCell;
                    //if (textBoxCell != null)
                    //{
                    couptxt = selectedValuecoup;
                    //}
                }
            }

        }

        private void dbgItemDetRet_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the Delete key is pressed
            if (e.KeyCode == Keys.Delete)
            {
                // Check if there is a selected row
                if (dbgItemDetRet.SelectedRows.Count > 0)
                {
                    // Get the selected row
                    DataGridViewRow selectedRow = dbgItemDetRet.SelectedRows[0];

                    // Get the index of the selected row
                    int rowIndex = selectedRow.Index;

                    // Remove the selected row
                    dbgItemDetRet.Rows.Remove(selectedRow);

                    UpdateRotGAmt();
                    //disccal();
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
            dbgPayDet.Rows[0].Cells[payamtColumnIndex].Value = rotNetAmt.Text;

            if (cboCust.SelectedItem != null)
            {
                selectedCustomer = cboCust.SelectedItem as string;

                if (!String.IsNullOrEmpty(selectedCustomer))
                {
                    if (dbgPayDet.Enabled == true)
                    {
                        for (int i = 0; i < dbgPayDet.Rows.Count - 1; i++)
                        {

                            foreach (DataGridViewRow row in dbgPayDet.Rows)
                            {

                                row.Cells["Custid"].Value = selectedCustomer;

                            }
                        }
                    }

                }
            }
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

        private void dbgPayDet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the changed cell is in the cash tend column and not in the header row
            if (e.ColumnIndex == 3 && e.RowIndex != -1)
            {
                DataGridViewCell cashTendCell = dbgPayDet.Rows[e.RowIndex].Cells[e.ColumnIndex];
                DataGridViewCell amountCell = dbgPayDet.Rows[e.RowIndex].Cells[2]; // Assuming amount is in the third column

                // Check if the cash tend cell value is not null and can be converted to decimal
                if (cashTendCell.Value != null && decimal.TryParse(cashTendCell.Value.ToString(), out decimal cashTend))
                {
                    // Calculate refund amount
                    decimal amount = Convert.ToDecimal(amountCell.Value ?? 0); // Default to 0 if amount is null
                    decimal refundAmount = cashTend - amount;

                    // Set the refund amount in the fifth column (index 4)
                    dbgPayDet.Rows[e.RowIndex].Cells[4].Value = refundAmount;
                }

            }
            if (e.ColumnIndex == 1 && e.RowIndex != -1)
            {

                DataGridViewComboBoxCell comboBoxCellpaym = dbgPayDet.Rows[e.RowIndex].Cells[1] as DataGridViewComboBoxCell;

                // Check if the ComboBoxCell is not null
                if (comboBoxCellpaym != null)
                {
                    string selectedValuepaym = comboBoxCellpaym.Value?.ToString() ?? "";

                    // Check if the cell at index 6 is a TextBox cell
                    //DataGridViewTextBoxCell textBoxCell = dbgPayDet.Rows[e.RowIndex].Cells[6] as DataGridViewTextBoxCell;
                    //if (textBoxCell != null)
                    //{
                    paymodtxt = selectedValuepaym;
                    //}
                }

                dbConnector = new DbConnector();
                // dbConnector.connectionString= new OdbcConnection();
                dbConnector.connection = new OdbcConnection(dbConnector.connectionString);
                dbConnector.connection.Open();

                if (paymodtxt == "Coup" && paymodtxt != "")
                {

                    string coupon = "select coup_id from m_coupon";
                    using (OdbcCommand cmd = new OdbcCommand(coupon, dbConnector.connection))
                    {
                        OdbcDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                // Assuming the data type of coup_id is string
                                string coupId = reader["coup_id"].ToString();

                                // Assuming dataGridView1 is the name of your DataGridView
                                DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)dbgPayDet.Rows[e.RowIndex].Cells[7];
                                // Check if the second column's combo box cell value is "CC"
                                if (dbgPayDet.Rows[e.RowIndex].Cells[2].Value != "")
                                {
                                    // Add the coup_id from the database to the Items collection of the ComboBoxCell
                                    comboBoxCell.Items.Add(coupId);
                                }
                                // Set the cell to be editable
                                comboBoxCell.ReadOnly = false;
                            }

                        }

                    }
                }

                if (paymodtxt == "CC" && paymodtxt != "")
                {
                    string ccode = "select cc_id from m_cr_card";
                    using (OdbcCommand cmd1 = new OdbcCommand(ccode, dbConnector.connection))
                    {
                        OdbcDataReader reader1 = cmd1.ExecuteReader();
                        if (reader1.HasRows)
                        {
                            while (reader1.Read())
                            {
                                // Assuming the data type of coup_id is string
                                string ccId = reader1["cc_id"].ToString();

                                // Assuming dataGridView1 is the name of your DataGridView
                                DataGridViewComboBoxCell comboBoxCell2 = (DataGridViewComboBoxCell)dbgPayDet.Rows[e.RowIndex].Cells[5];

                                // Check if the second column's combo box cell value is "CC"
                                if (dbgPayDet.Rows[e.RowIndex].Cells[2].Value != "")
                                {
                                    // Add the cc_id from the database to the Items collection of the ComboBoxCell
                                    comboBoxCell2.Items.Add(ccId);
                                }
                                // Set the cell to be editable
                                comboBoxCell2.ReadOnly = false;
                            }

                        }

                    }

                }
            }

            if (e.ColumnIndex == 5 && e.RowIndex != -1) // Assuming the ComboBoxCell is in column index 5
            {
                DataGridViewComboBoxCell comboBoxCell = dbgPayDet.Rows[e.RowIndex].Cells[5] as DataGridViewComboBoxCell;

                // Check if the ComboBoxCell is not null
                if (comboBoxCell != null)
                {
                    string selectedValue = comboBoxCell.Value?.ToString() ?? "";

                    // Check if the cell at index 6 is a TextBox cell
                    //DataGridViewTextBoxCell textBoxCell = dbgPayDet.Rows[e.RowIndex].Cells[6] as DataGridViewTextBoxCell;
                    //if (textBoxCell != null)
                    //{
                    ccodetxt = selectedValue;
                    //}
                }
            }

            if (e.ColumnIndex == 7 && e.RowIndex != -1) // Assuming the ComboBoxCell is in column index 5
            {
                DataGridViewComboBoxCell comboBoxCellcoup = dbgPayDet.Rows[e.RowIndex].Cells[7] as DataGridViewComboBoxCell;

                // Check if the ComboBoxCell is not null
                if (comboBoxCellcoup != null)
                {
                    string selectedValuecoup = comboBoxCellcoup.Value?.ToString() ?? "";

                    // Check if the cell at index 6 is a TextBox cell
                    //DataGridViewTextBoxCell textBoxCell = dbgPayDet.Rows[e.RowIndex].Cells[6] as DataGridViewTextBoxCell;
                    //if (textBoxCell != null)
                    //{
                    couptxt = selectedValuecoup;
                    //}
                }
            }

        }
    } ////*****OVER HERE
}
