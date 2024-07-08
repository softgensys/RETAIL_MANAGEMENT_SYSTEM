﻿using System.Data;
using System.Data.Odbc;

namespace softgen
{
    public partial class frmM_Sub_Group : Form, Interface_for_Common_methods.ISearchableForm
    {
        private DbConnector dbConnector;
        private string mstrEntBy, mstrEntOn, mstrAuthBy, mstrAuthOn;
        public bool mblnSearch, mblnDataEntered;
        private OdbcTransaction transaction;
        Messages msg = new Messages();
        General general = new General();
        bool saveflag = true;


        public frmM_Sub_Group()
        {
            InitializeComponent();
            dbConnector = new DbConnector();

            this.Activated += MyForm_Activated;
            this.KeyPreview = true; // Make sure the form has key preview enabled
            this.KeyUp += new KeyEventHandler(DeTools.Form_KeyUp);

            this.FormClosed += frmM_Sub_Group_FormClosed;
        }

        private void MyForm_Activated(object sender, EventArgs e)
        {
            DeTools.ClearStatusBarHelp();
            DeTools.ActiveFileMenu(this);
            DeTools.CreatedBy(mstrEntBy, mstrEntOn);
            DeTools.PostedBy(mstrAuthBy, mstrAuthOn);
            DeTools.MakeTextBoxUppercase(txtSubGrpDesc);


        }

        public void ClearForm()
        {
            mblnDataEntered = false;
            mstrEntBy = null;
            mstrEntOn = null;
            mstrAuthBy = null;
            mstrAuthOn = null;
            DeTools.ClearTextNComboControls(this);

            ChkAct.Checked = true;
            cboGrpId.Enabled = true;
            cboGrpId.Focus();
        }

        public void PrintForm()
        {

        }

        public void ResetControls(Control.ControlCollection controls)
        {
            if (mblnSearch == true)
            {
                foreach (Control control in controls)
                {
                    // Check if the control is a TextBox and its ID starts with "txt"
                    if (control is TextBox && control.Name != null && control.Name.StartsWith("txt"))
                    {
                        TextBox textBox = (TextBox)control;

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

                    if (control is Label && control.Name != null && control.Name.StartsWith("rot"))
                    {
                        Label lbl = (Label)control;
                        lbl.Text = "";
                        //textBox.Enabled = true;
                    }

                    // Check if the control is a ComboBox
                    if (control is ComboBox && control.Name != null && control.Name.StartsWith("cbo"))
                    {
                        ComboBox comboBox = (ComboBox)control;
                        comboBox.SelectedIndex = -1; // Select none
                    }
                }


            }
            else
            {
                foreach (Control control in controls)
                {
                    // Check if the control is a TextBox and its ID starts with "txt"
                    if (control is TextBox && control.Name != null && control.Name.StartsWith("txt"))
                    {
                        TextBox textBox = (TextBox)control;

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

                    //txtSubGrpId.Enabled = false;
                }

            }
        }

        private void lblAdd_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblStatus_Click(object sender, EventArgs e)
        {

        }

        private void frmM_Sub_Group_Load(object sender, EventArgs e)
        {
            General general = new General();
            DeTools.DisplayForm(this, 205, 504);
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(cboGrpId, "Select Group Id.");
            toolTip.SetToolTip(txtSubGrpDesc, "Enter Sub Group Description.");
            toolTip.SetToolTip(txtSubGrpId, "Enter Sub Group Id.");
            Help.controlToHelpTopicMapping.Add(cboGrpId, "9007"); /////For Help ContextId///IMP...
            Help.controlToHelpTopicMapping.Add(txtSubGrpId, "1020"); /////For Help ContextId///IMP...
            general.FillCombo(cboGrpId, "group_id", "m_group", false);
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

        public void SaveForm()
        {
            try
            {


                dbConnector = new DbConnector();
                // dbConnector.connectionString= new OdbcConnection();
                dbConnector.connection = new OdbcConnection(dbConnector.connectionString);

                // saveflag = true;

                bool blnItem_H, blnItem_D;
                int J;

                blnItem_H = true;
                DeTools.gstrSQL = "select * from m_sub_group where sub_group_id='" + txtSubGrpId.Text.Trim() + "' limit 1;  ";
                OdbcCommand cmd = new OdbcCommand(DeTools.gstrSQL, dbConnector.connection);
                dbConnector.connection.Open();

                OdbcDataReader reader = cmd.ExecuteReader();

                if (DeTools.GetMode(this) != DeTools.ADDMODE)
                {
                    if (reader.HasRows)
                    {

                        if (DeTools.CheckTemporaryTableExists("m_sub_group") != null)
                        {

                            // The record exists, so update it
                            reader.Close();
                            Cursor.Current = Cursors.WaitCursor;

                            string gstrSQL1 = "Insert into temp_m_sub_group(group_id, sub_group_id, sub_group_desc, active_yn, sales_tax, sp_change_yn, disc_yn, disc_per, status, ent_by, ent_on, Trans_status, open_yn, comp_name , mod_date, mod_by)" +
                                               "Select group_id, sub_group_id, sub_group_desc, active_yn, sales_tax, sp_change_yn, disc_yn, disc_per, status, ent_by, ent_on, Trans_status, 'Y' AS open_yn, '" + DeTools.fOSMachineName.GetMachineName() + "' AS comp_name ," +
                                               " mod_date, mod_by from m_sub_group where sub_group_id= '" + txtSubGrpId.Text.Trim() + "'";

                            using (OdbcCommand insertintemp1 = new OdbcCommand(gstrSQL1, dbConnector.connection))
                            {
                                insertintemp1.ExecuteNonQuery();
                            }

                            string gstrSQL2 = "Select * from temp_m_sub_group where sub_group_id='" + txtSubGrpId.Text.Trim() + "' and open_yn='Y'";
                            OdbcCommand selectintemp1 = new OdbcCommand(DeTools.gstrSQL, dbConnector.connection);

                            OdbcDataReader selectread = selectintemp1.ExecuteReader();

                            if (selectread.HasRows)
                            {
                                string delSQL = "Delete FROM m_sub_group WHERE sub_group_id = '" + txtSubGrpId.Text.Trim() + "'; ";

                                using (OdbcCommand delfrmhdr1 = new OdbcCommand(delSQL, dbConnector.connection))
                                {
                                    delfrmhdr1.ExecuteNonQuery();
                                }

                                DeTools.gstrSQL = "update temp_m_sub_group set group_id = ?, sub_group_id = ?, sub_group_desc = ?, active_yn = ?, sales_tax = ?, sp_change_yn = ?," +
                                    " disc_yn = ?, disc_per = ?, status = ?, Trans_status = ?," +
                                    " comp_name  = '" + DeTools.fOSMachineName.GetMachineName() + "', mod_date = ?, mod_by = ? where sub_group_id= '" + txtSubGrpId.Text.Trim() + "'";

                                cmd.CommandText = DeTools.gstrSQL;

                                string grp = cboGrpId.SelectedItem != null ? cboGrpId.SelectedItem.ToString().Trim() : string.Empty;
                                cmd.Parameters.Add(new OdbcParameter("group_id", grp));
                                cmd.Parameters.Add(new OdbcParameter("sub_group_id", txtSubGrpId.Text.Trim()));
                                cmd.Parameters.Add(new OdbcParameter("sub_group_desc", txtSubGrpDesc.Text.Trim()));
                                cmd.Parameters.Add(new OdbcParameter("active_yn", ChkAct.Checked ? "Y" : "N"));
                                cmd.Parameters.Add(new OdbcParameter("sales_tax", txtSTaxPer.Text.Trim()));
                                cmd.Parameters.Add(new OdbcParameter("sp_change_yn", chkSPChange.Checked ? "Y" : "N"));
                                cmd.Parameters.Add(new OdbcParameter("disc_yn", chkDisc.Checked ? "Y" : "N"));
                                cmd.Parameters.Add(new OdbcParameter("disc_per", txtDisc.Text.Trim()));
                                cmd.Parameters.Add(new OdbcParameter("status", "V"));
                                cmd.Parameters.Add(new OdbcParameter("Trans_status", "N"));
                                cmd.Parameters.Add(new OdbcParameter("mod_date", OdbcType.DateTime)).Value = DateTime.Now;
                                cmd.Parameters.Add(new OdbcParameter("mod_by", DeTools.gstrloginId));

                                cmd.ExecuteNonQuery();

                                Cursor.Current = Cursors.Default;

                                Messages.SavingMsg();


                                //DeTools.SelectDataFromTemporaryTable("m_group");
                                reader.Close();

                                string insertQuery = "Insert into m_sub_group (group_id,sub_group_id,sub_group_desc,active_yn,sales_tax,sp_change_yn," +
                                                      "disc_yn,disc_per,status,Trans_status,mod_date,mod_by) Select group_id, sub_group_id, sub_group_desc," +
                                                      " active_yn, sales_tax, sp_change_yn, disc_yn, disc_per, status,Trans_status, mod_date, mod_by" +
                                                      " from temp_m_sub_group where sub_group_id='" + txtSubGrpId.Text.Trim() + "'";

                                using (OdbcCommand insertCmd = new OdbcCommand(insertQuery, dbConnector.connection))
                                {
                                    insertCmd.ExecuteNonQuery();

                                }
                                string querupdN1 = "update temp_m_sub_group set open_yn='N' where sub_group_id=? order by ent_on desc ";

                                using (OdbcCommand querupdNCmd = new OdbcCommand(querupdN1, dbConnector.connection))
                                {
                                    querupdNCmd.Parameters.Add(new OdbcParameter("sub_group_id", txtSubGrpId.Text.ToString().Trim()));
                                    querupdNCmd.ExecuteNonQuery();


                                }

                                string querdel1 = "delete from temp_m_sub_group where sub_group_id=? order by ent_on desc ";

                                using (OdbcCommand querdelCmd = new OdbcCommand(querdel1, dbConnector.connection))
                                {
                                    querdelCmd.Parameters.Add(new OdbcParameter("sub_group_id", txtSubGrpId.Text.ToString().Trim()));
                                    querdelCmd.ExecuteNonQuery();


                                }
                            }
                        }
                    }
                }

                else
                {

                    // The record does not exist, so insert a new one
                    reader.Close();

                    if (DeTools.CheckTemporaryTableExists("m_sub_group") != null)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        DeTools.gstrSQL = "INSERT INTO temp_m_sub_Group (group_id, sub_group_id, sub_group_desc, active_yn, sales_tax, sp_change_yn, disc_yn, disc_per, status, ent_by, ent_on, Trans_status, comp_name, open_yn) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                        OdbcCommand cmdd = new OdbcCommand(DeTools.gstrSQL, dbConnector.connection);

                        cmdd.CommandText = DeTools.gstrSQL;

                        string grp = cboGrpId.SelectedItem != null ? cboGrpId.SelectedItem.ToString().Trim() : string.Empty;
                        cmdd.Parameters.Add(new OdbcParameter("group_id", grp));
                        cmdd.Parameters.Add(new OdbcParameter("sub_group_id", txtSubGrpId.Text));
                        cmdd.Parameters.Add(new OdbcParameter("sub_group_desc", txtSubGrpDesc.Text));
                        cmdd.Parameters.Add(new OdbcParameter("active_yn", ChkAct.Checked ? "Y" : "N"));
                        cmdd.Parameters.Add(new OdbcParameter("sales_tax", txtSTaxPer.Text));
                        cmdd.Parameters.Add(new OdbcParameter("sp_change_yn", chkSPChange.Checked ? "Y" : "N"));
                        cmdd.Parameters.Add(new OdbcParameter("disc_yn", chkDisc.Checked ? "Y" : "N"));
                        cmdd.Parameters.Add(new OdbcParameter("disc_per", txtDisc.Text));
                        cmdd.Parameters.Add(new OdbcParameter("status", "V"));
                        cmdd.Parameters.Add(new OdbcParameter("ent_by", DeTools.gstrloginId));
                        cmdd.Parameters.Add(new OdbcParameter("ent_on", OdbcType.DateTime)).Value = DateTime.Now;
                        cmdd.Parameters.Add(new OdbcParameter("trans_status", "N"));
                        cmdd.Parameters.Add(new OdbcParameter("comp_name", DeTools.fOSMachineName.GetMachineName()));
                        cmdd.Parameters.Add(new OdbcParameter("open_yn", "Y"));

                        cmdd.ExecuteNonQuery();

                        //DeTools.SelectDataFromTemporaryTable("m_group");
                        //reader.Close();

                        string insertQuery = "Insert into m_sub_group(group_id, sub_group_id, sub_group_desc, active_yn, sales_tax, sp_change_yn, disc_yn, disc_per, status, ent_by, ent_on, Trans_status)" +
                                               "Select group_id, sub_group_id, sub_group_desc, active_yn, sales_tax, sp_change_yn, disc_yn, disc_per, status, ent_by, ent_on, Trans_status" +
                                               " from temp_m_sub_group where sub_group_id= '" + txtSubGrpId.Text.Trim() + "'";

                        using (OdbcCommand insertCmd = new OdbcCommand(insertQuery, dbConnector.connection))
                        {
                            insertCmd.ExecuteNonQuery();

                        }

                        reader.Close();
                        Messages.SavingMsg();
                        Cursor.Current = Cursors.Default;

                        string quer1 = "update temp_m_sub_group set open_yn='N' where sub_group_id='" + txtSubGrpId.Text.ToString().Trim() + "' order by ent_on desc ";
                        using (OdbcCommand qurCmd = new OdbcCommand(quer1, dbConnector.connection))
                        {
                            qurCmd.ExecuteNonQuery();
                        }
                    }

                }

                Messages.SavedMsg();
                dbConnector.connection.Close();
                ResetControls(this.Controls);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void SearchForm()
        {
            try
            {

                //for getting unsaved data
                dbConnector = new DbConnector();
                // dbConnector.connectionString= new OdbcConnection();
                dbConnector.connection = new OdbcConnection(dbConnector.connectionString);

                General general = new General();
                if (!string.IsNullOrWhiteSpace(txtSubGrpId.Text) && !mblnSearch)
                {
                    msg.HelpMsg("Information retrieving. Please wait...");

                    DeTools.gstrSQL = "SELECT * FROM m_sub_Group WHERE sub_group_id = '" + txtSubGrpId.Text.Trim() + "'";

                    OdbcCommand cmd = new OdbcCommand(DeTools.gstrSQL, dbConnector.connection);
                    dbConnector.connection.Open();
                    OdbcDataReader reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        msg.HelpMsg("Information Not Found");
                        Messages.InfoMsg("No information available for this criteria.!");
                        mblnSearch = false;
                        reader.Close();
                        return;
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            if (reader["status"].ToString() == "A") // Posted
                            {
                                switch (DeTools.GetMode(this))
                                {
                                    case DeTools.DELETEMODE:
                                    case DeTools.POSTMODE:
                                        //AuthorisedMsg();
                                        mblnSearch = false;
                                        reader.Close();
                                        return;
                                }
                            }

                            txtSubGrpId.Enabled = false;
                            txtSubGrpDesc.Text = reader["sub_group_desc"].ToString();
                            if (reader["active_yn"].ToString() == "Y")
                                ChkAct.Checked = true;
                            else
                                ChkAct.Checked = false;

                            if (reader["sp_change_yn"].ToString() == "Y")
                                chkSPChange.Checked = true;
                            else
                                ChkAct.Checked = false;

                            if (reader["disc_yn"].ToString() == "Y")
                                chkSPChange.Checked = true;
                            else
                                ChkAct.Checked = false;
                            txtDisc.Text = reader["disc_per"].ToString().Trim();

                            txtSTaxPer.Text = reader["sales_tax"].ToString();
                            mstrEntBy = general.GetuserName(reader["ent_by"].ToString());
                            DateTime entOn = Convert.ToDateTime(reader["ent_on"]);
                            mstrEntOn = entOn.ToString("dd/MM/yyyy");
                            DeTools.CreatedBy(mstrEntBy, mstrEntOn);
                            cboGrpId.SelectedItem = reader["group_id"].ToString().Trim();


                            if (reader["status"].ToString() == "A")
                            {
                                mstrAuthBy = general.GetuserName(reader["auth_by"].ToString());
                                DateTime AuthOn = Convert.ToDateTime(reader["auth_on"].ToString());
                                mstrAuthOn = AuthOn.ToString("dd/MM/yyyy");
                                DeTools.PostedBy(mstrAuthBy, mstrAuthOn);
                            }
                        }

                        reader.Close();
                        //msg.FoundMsg("Information Found!");
                        msg.HelpMsg("Information Found!");
                        mblnSearch = true;
                    }
                }
                else
                {
                    return;
                }


            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                txtSubGrpId.Enabled = false;
            }
        }

        public void UnsavedData()
        {
            DbConnector dbConnector = new DbConnector();


            try
            {
                if (DeTools.CheckTemporaryTableExists("m_sub_group") != null)
                {
                    dbConnector.OpenConnection();

                    string compname = DeTools.fOSMachineName.GetMachineName();
                    string user = MainForm.Instance.pnlUserName.Text.Trim();

                    string query = "SELECT * FROM temp_m_sub_group WHERE open_yn='Y' and ent_by='" + user.Trim() + "' and comp_name='" + compname.Trim() + "' order by ent_on desc limit 1;";

                    OdbcParameter[] parameters = new OdbcParameter[0];


                    using (OdbcDataReader reader = dbConnector.ExecuteReader(query, parameters))
                    {
                        if (reader == null)
                        {

                        }

                        else if (reader.HasRows)
                        {
                            // Populate text fields with the data from the reader
                            txtSubGrpId.Text = reader["sub_group_id"].ToString();
                            txtSubGrpDesc.Text = reader["sub_group_desc"].ToString();
                            txtSTaxPer.Text = reader["sales_tax"].ToString();
                            txtDisc.Text = reader["disc_per"].ToString();


                            // Read the 'active_yn' value from the query result
                            string activeYnValue = reader["active_yn"].ToString(); // Replace with the actual column name

                            if (activeYnValue == "Y")
                            {
                                ChkAct.Checked = true;
                            }
                            else
                            {
                                ChkAct.Checked = false;
                            }

                            string sp_change_yn = reader["sp_change_yn"].ToString(); // Replace with the actual column name

                            if (sp_change_yn == "Y")
                            {
                                chkSPChange.Checked = true;
                            }
                            else
                            {
                                chkSPChange.Checked = false;
                            }

                            string disc_yn = reader["disc_yn"].ToString(); // Replace with the actual column name

                            if (disc_yn == "Y")
                            {
                                chkDisc.Checked = true;
                            }
                            else
                            {
                                chkDisc.Checked = false;
                            }


                            string groupIDFromDatabase = reader["group_id"].ToString().Trim();

                            if (groupIDFromDatabase != "")
                            {
                                // Find the item in the ComboBox's items collection
                                object selectedItem1 = cboGrpId.Items.Cast<object>().FirstOrDefault(item => item.ToString() == groupIDFromDatabase);

                                // Set the selected item if found
                                if (selectedItem1 != null)
                                {
                                    cboGrpId.SelectedItem = selectedItem1;
                                    rotGrpDesc.Text = general.GetDesc("m_group", "group_id", "group_desc", "C", cboGrpId.SelectedItem.ToString().Trim());
                                }

                                // Populate other text fields similarly
                                Messages.UnsavedMsg(txtSubGrpId.Text);
                            }

                        }
                        else
                        {
                            // Handle case when no data is found for the given groupID
                            // You can clear the text fields or display a message
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
            string id = "group_id";
            string tblname = "m_group";

            DeTools.gstrSQL = "SELECT " + id + ",ent_by,comp_name FROM softgen_db.temp_" + tblname + " order by ent_on desc limit 1;";

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

        private void cboGrpId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboGrpId.SelectedItem != null)
            {
                string desc = general.GetDesc("m_group", "group_id", "group_desc", "C", cboGrpId.SelectedItem.ToString().Trim());
                rotGrpDesc.Text = desc;
            }
        }

        private void frmM_Sub_Group_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (txtSubGrpId.Text != "" || txtSubGrpDesc.Text != "" || cboGrpId.SelectedIndex != -1)
            {
                dbConnector = new DbConnector();
                // dbConnector.connectionString= new OdbcConnection();
                dbConnector.connection = new OdbcConnection(dbConnector.connectionString);
                dbConnector.connection.Open();

                // transaction = dbConnector.connection.BeginTransaction();

                string selectid_toenter = "select sub_group_id from m_sub_group where sub_group_id='" + txtSubGrpId.Text.Trim() + "'";
                string selectidtemp_toenter = "select sub_group_id from temp_m_sub_group where sub_group_id='" + txtSubGrpId.Text.Trim() + "'";

                using (OdbcDataReader readertemp = dbConnector.CreateResultset(selectidtemp_toenter))
                {
                    if (!readertemp.HasRows)
                    {

                    }


                    using (OdbcDataReader reader = dbConnector.CreateResultset(selectid_toenter))
                    {
                        if (!reader.HasRows)
                        {
                            string insert = "INSERT INTO temp_m_sub_Group (group_id, sub_group_id, sub_group_desc, active_yn, sales_tax, sp_change_yn, disc_yn, disc_per, status, ent_by, ent_on, Trans_status, open_yn, comp_name) VALUES (? , ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                            OdbcCommand cmd = new OdbcCommand(insert, dbConnector.connection);
                            //cmd.Transaction = transaction;


                            cmd.CommandText = insert;
                            string grp = cboGrpId.SelectedItem != null ? cboGrpId.SelectedItem.ToString().Trim() : string.Empty;
                            cmd.Parameters.Add(new OdbcParameter("group_id", grp));
                            cmd.Parameters.Add(new OdbcParameter("sub_group_id", txtSubGrpId.Text));
                            cmd.Parameters.Add(new OdbcParameter("sub_group_desc", txtSubGrpDesc.Text));
                            cmd.Parameters.Add(new OdbcParameter("active_yn", ChkAct.Checked ? "Y" : "N"));
                            cmd.Parameters.Add(new OdbcParameter("sales_tax", txtSTaxPer.Text));
                            cmd.Parameters.Add(new OdbcParameter("sp_change_yn", chkSPChange.Checked ? "Y" : "N"));
                            cmd.Parameters.Add(new OdbcParameter("disc_yn", chkDisc.Checked ? "Y" : "N"));
                            cmd.Parameters.Add(new OdbcParameter("disc_per", txtDisc.Text));
                            cmd.Parameters.Add(new OdbcParameter("status", "V"));
                            cmd.Parameters.Add(new OdbcParameter("ent_by", DeTools.gstrloginId));
                            cmd.Parameters.Add(new OdbcParameter("ent_on", OdbcType.DateTime)).Value = DateTime.Now;
                            cmd.Parameters.Add(new OdbcParameter("trans_status", "N"));
                            cmd.Parameters.Add(new OdbcParameter("open_yn", "Y"));
                            cmd.Parameters.Add(new OdbcParameter("comp_name", DeTools.fOSMachineName.GetMachineName()));

                            cmd.ExecuteNonQuery();

                            //DeTools.SelectDataFromTemporaryTable("m_group");
                            reader.Close();
                        }
                    }


                    // transaction.Commit();
                    dbConnector.connection.Close();
                }
            }
        }

        private void frmM_Sub_Group_FormClosed(object sender, FormClosedEventArgs e)
        {
            DeTools.DestroyToolbar(this);
            MainForm.Instance.mnuMSGroup.Enabled = true;
        }

        private void txtSubGrpId_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.Text.Contains("<Add>"))
            {


                if (!DeTools.IsFieldUnique("m_sub_group", "sub_group_id", txtSubGrpId.Text.ToString().Trim()))
                {
                    MessageBox.Show("Id :" + txtSubGrpId.Text + " already Exists.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSubGrpId.Text = null;
                    txtSubGrpId.Refresh();
                    txtSubGrpId.Focus();
                    // You can also clear the control or perform other actions
                }
            }
        }

        private void cboGrpId_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                //string mode = DeTools.GetMode(DeTools.gobjActiveForm);
                //if (mode == DeTools.MODIFYMODE)
                //{
                    
                //}
                general.FillCombo(cboGrpId, "group_id", "m_group", false);

                // Suppress the default Tab behavior
                e.SuppressKeyPress = true;

                ComboBox comboBox = sender as ComboBox;
                if (comboBox != null && !string.IsNullOrEmpty(comboBox.Text))
                {
                    string enteredText = comboBox.Text.Trim();

                    // Check if the entered text matches any item in the ComboBox
                    foreach (var item in comboBox.Items)
                    {
                        if (item.ToString().Equals(enteredText, StringComparison.OrdinalIgnoreCase))
                        {
                            // If a match is found, set it as the selected item
                            comboBox.SelectedItem = item;

                            // Perform the required action
                            if (comboBox.SelectedItem != null)
                            {
                                string desc = general.GetDesc("m_group", "group_id", "group_desc", "C", comboBox.SelectedItem.ToString().Trim());
                                rotGrpDesc.Text = desc;
                                
                            }
                            else
                            {
                                MessageBox.Show("No Such Id Present In This!", "NOT FOUND!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }

                            return;
                        }
                    }

                    // If no match is found, clear the selection
                    comboBox.SelectedItem = null;
                }
                txtSubGrpId.Focus();
            }
        }

        private void txtSubGrpId_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string mode = DeTools.GetMode(DeTools.gobjActiveForm);
                if (mode == DeTools.MODIFYMODE)
                {
                    SearchForm();
                    txtSubGrpId.Enabled = false;
                    

                }
             
                

            }

        }
    }//-----------END-//
}
