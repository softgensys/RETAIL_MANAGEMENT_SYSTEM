﻿using System.ComponentModel;
using System.Data.Odbc;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;


namespace softgen
{
    public partial class frmM_Group : Form, Interface_for_Common_methods.ISearchableForm
    {
        private DbConnector dbConnector;
        private string mstrEntBy, mstrEntOn, mstrAuthBy, mstrAuthOn;
        public bool mblnSearch, mblnDataEntered;
        private OdbcTransaction transaction;
        Messages msg = new Messages();
        private bool isUpdating = false;

        public frmM_Group()
        {
            InitializeComponent();
            dbConnector = new DbConnector();

            this.Activated += MyForm_Activated;
            // Add this event handler in the form's constructor or Load event
            this.KeyPreview = true; // Make sure the form has key preview enabled
            this.KeyUp += new KeyEventHandler(DeTools.Form_KeyUp);
            //txtGrpId.KeyDown += txtGrpId_KeyDown;
            txtGrpId.TextChanged += txtGrpId_TextChanged;
            DeTools.MakeTextBoxUppercase(txtGrpDesc);




        }


        private void MyForm_Activated(object sender, EventArgs e)
        {
            DeTools.ClearStatusBarHelp();
            DeTools.ActiveFileMenu(this);
            DeTools.CreatedBy(mstrEntBy, mstrEntOn);
            DeTools.PostedBy(mstrAuthBy, mstrAuthOn);
            //ResetControls(this.Controls);

            //UpdateToolbarVisibility();


        }

        //private Form lastActiveChildForm;

        public void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Convert the text to uppercase
                textBox.Text = textBox.Text.ToUpper();
            }
        }


        private void UpdateToolbarVisibility()
        {
            foreach (var kvp in DeTools.toolbarDictionary1)
            {
                string formKey = kvp.Key;
                List<ToolStrip> toolstrips = kvp.Value;

                // Split the formKey to get form name and mode
                string[] keyParts = formKey.Split('-');
                string formName = keyParts[0];
                string mode = keyParts.Length > 1 ? keyParts[1] : null;

                // Find the form by name
                Form form = Application.OpenForms.OfType<Form>().FirstOrDefault(f => f.Name == formName);

                if (form != null)
                {
                    bool isFormActive = form == DeTools.gobjActiveForm;
                    bool shouldShowToolbar = !form.Visible || (isFormActive && form.WindowState != FormWindowState.Minimized);

                    foreach (ToolStrip toolStrip in toolstrips)
                    {
                        toolStrip.Visible = shouldShowToolbar;
                    }
                }
            }
        }



        // Get a unique key for each form based on its type and mode
        private string GetFormKey(Form form)
        {
            string mode = DeTools.GetMode(form);
            return string.IsNullOrEmpty(mode) ? form.Name : $"{form.Name}-{mode}";
        }


        private void Group_Load(object sender, EventArgs e)
        {
            DeTools.DisplayForm(this, 300, 420);
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(txtGrpId, "Enter Group Id.");
            toolTip.SetToolTip(txtGrpDesc, "Enter Group Description.");
            Help.controlToHelpTopicMapping.Add(txtGrpId, "1004"); /////For Help ContextId///IMP...
            DeTools.CheckTemporaryTableExists("m_group");

            //UpdateToolbarVisibility();
            this.Deactivate += frmM_Group_Deactivate;
            this.Resize += frmM_Group_Resize;

        }

        //private void Group_FormClosed(object sender, FormClosedEventArgs e)
        //{
        //    MainForm parentform = (MainForm)this.MdiParent;

        //    parentform.dashpanel.Visible = true;
        //    parentform.mainpanel.Visible = false;
        //    parentform.formpanel.Visible = false;
        //}

        private void frmM_Group_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (DeTools.GetMode(this))
            {
                case DeTools.ADDMODE:
                case DeTools.MODIFYMODE:
                    mblnDataEntered = true;
                    break;
            }
        }

        public bool GetDEStatus()
        {
            return mblnDataEntered == true ? true : false;
        }


        public void SetSearchVar(bool StartVal)
        {
            // Implementation of SetSearchVar
            // You can define the behavior of SetSearchVar here

            mblnSearch = StartVal;
        }

        public void ClearForm()
        {
            mblnDataEntered = false;
            mstrEntBy = null;
            mstrEntOn = null;
            mstrAuthBy = null;
            mstrAuthOn = null;
            DeTools.ClearTextNComboControls(this);

            chkStatus.Checked = true;
            txtGrpId.Enabled = true;
            txtGrpId.Focus();

            //if (txtGrpId.Text != "" )
            //{
            //    txtGrpId.Text = string.Empty;
            //}
            //if (txtGrpDesc.Text != "" )
            //{
            //    txtGrpDesc.Text = string.Empty;
            //}

        }

        public void PrintForm()
        {

        }

        private void frmM_Group_FormClosed(object sender, FormClosedEventArgs e)
        {
            DeTools.DestroyToolbar(this);
            MainForm.Instance.Mgroupmenu.Enabled = true;

        }


        public void SaveForm()
        {
            try
            {

                //for getting unsaved data
                dbConnector = new DbConnector();
                // dbConnector.connectionString= new OdbcConnection();
                dbConnector.connection = new OdbcConnection(dbConnector.connectionString);


                //dbConnector.transactiono=new OdbcTransaction;
                if (txtGrpId.Text != "" || txtGrpDesc.Text != "")
                {


                    DeTools.gstrSQL = "SELECT * FROM m_Group WHERE Group_id = '" + txtGrpId.Text.Trim() + "'";
                    OdbcCommand cmd = new OdbcCommand(DeTools.gstrSQL, dbConnector.connection);
                    dbConnector.connection.Open();
                    OdbcDataReader reader = cmd.ExecuteReader();

                    // Check if the record with the specified Group_id exists
                    if (DeTools.GetMode(this) != DeTools.ADDMODE)
                    {

                        if (reader.HasRows)
                        {


                            // The record exists, so update it
                            reader.Close();
                            //transaction = dbConnector.connection.BeginTransaction();

                            Cursor.Current = Cursors.WaitCursor;

                            DeTools.gstrSQL = "UPDATE m_Group SET group_desc = ?, active_yn = ?, sales_tax = ?, ent_by = ?, ent_on = ?, trans_status = ? WHERE Group_id = ?";
                            cmd.CommandText = DeTools.gstrSQL;
                            cmd.Parameters.Add(new OdbcParameter("group_desc", txtGrpDesc.Text));
                            cmd.Parameters.Add(new OdbcParameter("active_yn", chkStatus.Checked ? "Y" : "N"));
                            cmd.Parameters.Add(new OdbcParameter("sales_tax", txtSTaxPer.Text));
                            cmd.Parameters.Add(new OdbcParameter("ent_by", DeTools.gstrloginId));
                            //cmd.Parameters.Add(new OdbcParameter("ent_on", "NOW()"));
                            cmd.Parameters.Add(new OdbcParameter("ent_on", OdbcType.DateTime)).Value = DateTime.Now;
                            cmd.Parameters.Add(new OdbcParameter("trans_status", "N"));
                            cmd.Parameters.Add(new OdbcParameter("Group_id", txtGrpId.Text));

                            cmd.ExecuteNonQuery();

                            Messages.SavingMsg();
                            string quer1 = "update temp_m_group set open_yn='N' where group_id=? order by ent_on desc ";
                            Cursor.Current = Cursors.Default;

                            //string quer = "update temp_m_group set open_yn='N' where group_id=? order by ent_on desc ";

                            //transaction.Commit();
                        }
                    }

                    else
                    {
                        // cmd.Transaction = transaction;

                        // The record does not exist, so insert a new one
                        reader.Close();

                        if (DeTools.CheckTemporaryTableExists("m_group") != null)
                        {
                            Cursor.Current = Cursors.WaitCursor;

                            DeTools.gstrSQL = "INSERT INTO temp_m_Group (Group_id, group_desc, active_yn, sales_tax, ent_by, ent_on, trans_status, open_yn,comp_name) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";
                            cmd.CommandText = DeTools.gstrSQL;
                            cmd.Parameters.Add(new OdbcParameter("Group_id", txtGrpId.Text));
                            cmd.Parameters.Add(new OdbcParameter("group_desc", txtGrpDesc.Text));
                            cmd.Parameters.Add(new OdbcParameter("active_yn", chkStatus.Checked ? "Y" : "N"));
                            cmd.Parameters.Add(new OdbcParameter("sales_tax", txtSTaxPer.Text));
                            cmd.Parameters.Add(new OdbcParameter("ent_by", DeTools.gstrloginId));
                            cmd.Parameters.Add(new OdbcParameter("ent_on", OdbcType.DateTime)).Value = DateTime.Now;
                            cmd.Parameters.Add(new OdbcParameter("trans_status", "N"));
                            cmd.Parameters.Add(new OdbcParameter("open_yn", "N"));
                            cmd.Parameters.Add(new OdbcParameter("comp_name", DeTools.fOSMachineName.GetMachineName()));
                            //cmd.Parameters.Add(new OdbcParameter("status", "V"));

                            cmd.ExecuteNonQuery();

                            //DeTools.SelectDataFromTemporaryTable("m_group");

                            DeTools.InsertDataIntoMainTable("m_group", "group_id", txtGrpId.Text.ToString().Trim());
                            // transaction.Commit();

                            Messages.SavingMsg();
                            Cursor.Current = Cursors.Default;
                            string quer1 = "update temp_m_group set open_yn='N' where group_id=? order by ent_on desc ";
                        }
                    }
                    dbConnector.connection.Close();

                    Messages.SavedMsg();

                    ResetControls(this.Controls);
                    txtGrpId.Focus();

                    // Additional logic here for clearing fields, displaying messages, etc.
                }
                else
                {
                    MessageBox.Show("Pls Enter Group Id or Group Name!", "Group Id or Group Name Is Empty!",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                }

            }
            catch (Exception ex)
            {
                msg.VBError(ex, "frmM_Group", "SaveForm", null);
                // Handle the exception or display an error message
                Console.WriteLine(ex.Message);

                // Rollback the transaction if an error occurs
                if (transaction != null)
                {
                    //transaction.Rollback();
                }

                // Additional error handling and messages

            }
            finally
            {
                //transaction.Dispose();
                dbConnector.CloseConnection();
            }

        }

        public void ResetControls(Control.ControlCollection controls)
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
                    if (Help.o_control != null)
                    {
                        Help.o_control.Focus();
                    }

                    txtGrpId.Focus();
                }

                // Recursively call the method for nested controls
                if (control.Controls.Count > 0)
                {
                    ResetControls(control.Controls);
                }
            }
        }

        //-------to get the data in modified form-------//
        public void SearchForm()
        {
            try
            {
                //for getting unsaved data
                dbConnector = new DbConnector();
                // dbConnector.connectionString= new OdbcConnection();
                dbConnector.connection = new OdbcConnection(dbConnector.connectionString);

                General general = new General();
                if (!string.IsNullOrWhiteSpace(txtGrpId.Text) && !mblnSearch)
                {
                    msg.HelpMsg("Information retrieving. Please wait...");

                    DeTools.gstrSQL = "SELECT * FROM m_Group WHERE Group_id = '" + txtGrpId.Text.Trim() + "'";

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

                            txtGrpId.Enabled = false;
                            txtGrpDesc.Text = reader["group_desc"].ToString();
                            if (reader["active_yn"].ToString() == "Y")
                                chkStatus.Checked = true;
                            else
                                chkStatus.Checked = false;
                            txtSTaxPer.Text = reader["sales_tax"].ToString();
                            mstrEntBy = general.GetuserName(reader["ent_by"].ToString());
                            DateTime entOn = Convert.ToDateTime(reader["ent_on"]);
                            mstrEntOn = entOn.ToString("dd/MM/yyyy");
                            DeTools.CreatedBy(mstrEntBy, mstrEntOn);

                            if (reader["status"].ToString() == "A")
                            {
                                mstrAuthBy = general.GetuserName(reader["auth_by"].ToString());
                                DateTime AuthOn = Convert.ToDateTime(reader["auth_on"].ToString());
                                mstrAuthOn = AuthOn.ToString("dd/MM/yyyy");
                                DeTools.PostedBy(mstrAuthBy, mstrAuthOn);
                            }
                            // Add debug statements
                            Console.WriteLine($"txtSTaxPer.Text: {txtSTaxPer.Text}");
                            Console.WriteLine($"mstrEntBy: {mstrEntBy}");
                            Console.WriteLine($"mstrEntOn: {mstrEntOn}");
                        }

                        reader.Close();
                        //msg.FoundMsg("Information Found!");
                        msg.HelpMsg("Information Found!");
                        mblnSearch = true;
                        txtGrpId.Enabled = false;
                    }
                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {

                msg.VBError(ex, "frmM_Group", "SearchForm", null);
            }

        }


        private void txtGrpId_Validating(object sender, CancelEventArgs e)

        {
            string mode = DeTools.GetMode(DeTools.gobjActiveForm);
            if (mode == DeTools.ADDMODE)
            {


                if (!DeTools.IsFieldUnique("m_group", "group_id", txtGrpId.Text.ToString().Trim()))
                {
                    MessageBox.Show("Id :" + txtGrpId.Text + " already Exists.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtGrpId.Text = null;
                    txtGrpId.Refresh();
                    txtGrpId.Focus();
                    // You can also clear the control or perform other actions
                }
            }
        }

        private void frmM_Group_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (txtGrpId.Text != "" || txtGrpDesc.Text != "")
            {
                dbConnector = new DbConnector();
                // dbConnector.connectionString= new OdbcConnection();
                dbConnector.connection = new OdbcConnection(dbConnector.connectionString);
                dbConnector.connection.Open();

                // transaction = dbConnector.connection.BeginTransaction();

                string selectid_toenter = "select group_id from m_group where group_id='" + txtGrpId.Text.Trim() + "'";
                string selectidtemp_toenter = "select group_id from temp_m_group where group_id='" + txtGrpId.Text.Trim() + "'";

                using (OdbcDataReader readertemp = dbConnector.CreateResultset(selectidtemp_toenter))
                {
                    if (!readertemp.HasRows)
                    {

                    }


                    using (OdbcDataReader reader = dbConnector.CreateResultset(selectid_toenter))
                    {
                        if (!reader.HasRows)
                        {
                            string insert = "INSERT INTO temp_m_Group (Group_id, group_desc, active_yn, sales_tax, ent_by, ent_on, trans_status, open_yn, comp_name) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";
                            OdbcCommand cmd = new OdbcCommand(insert, dbConnector.connection);
                            //cmd.Transaction = transaction;


                            cmd.CommandText = insert;
                            cmd.Parameters.Add(new OdbcParameter("Group_id", txtGrpId.Text));
                            cmd.Parameters.Add(new OdbcParameter("group_desc", txtGrpDesc.Text));
                            cmd.Parameters.Add(new OdbcParameter("active_yn", chkStatus.Checked ? "Y" : "N"));
                            cmd.Parameters.Add(new OdbcParameter("sales_tax", txtSTaxPer.Text));
                            cmd.Parameters.Add(new OdbcParameter("ent_by", DeTools.gstrloginId));
                            cmd.Parameters.Add(new OdbcParameter("ent_on", OdbcType.DateTime)).Value = DateTime.Now;
                            cmd.Parameters.Add(new OdbcParameter("trans_status", "N"));
                            cmd.Parameters.Add(new OdbcParameter("open_yn", "Y"));
                            cmd.Parameters.Add(new OdbcParameter("comp_name", DeTools.fOSMachineName.GetMachineName()));
                            //cmd.Parameters.Add(new OdbcParameter("status", "V"));

                            cmd.ExecuteNonQuery();

                            //DeTools.SelectDataFromTemporaryTable("m_group");

                        }
                    }


                    // transaction.Commit();
                    dbConnector.connection.Close();
                }
            }

            // Check if there are any other child forms open
            if (this.MdiParent.MdiChildren.Length > 1)
            {
                // Bring another child form to the front
                foreach (Form child in this.MdiParent.MdiChildren)
                {
                    if (child != this)
                    {
                        child.Focus();
                        break;
                    }
                }
            }
            else
            {
                // Bring the main form to the front
                this.MdiParent.Focus();
            }
        }

        public void UnsavedData()
        {
            DbConnector dbConnector = new DbConnector();


            try
            {
                if (DeTools.CheckTemporaryTableExists("m_group") != null)
                {
                    dbConnector.OpenConnection();

                    string compname = DeTools.fOSMachineName.GetMachineName();
                    string user = MainForm.Instance.pnlUserName.Text.Trim();

                    string query = "SELECT * FROM temp_m_group WHERE open_yn='Y' and ent_by='" + user.Trim() + "' and comp_name='" + compname.Trim() + "' order by ent_on desc limit 1;";

                    OdbcParameter[] parameters = new OdbcParameter[0];


                    using (OdbcDataReader reader1 = dbConnector.ExecuteReader(query, parameters))
                    {
                        if (reader1 == null)
                        {

                        }

                        if (reader1.HasRows)
                        {
                            // Populate text fields with the data from the reader
                            txtGrpId.Text = reader1["group_id"].ToString();
                            txtGrpDesc.Text = reader1["group_desc"].ToString();
                            // Read the 'active_yn' value from the query result
                            string activeYnValue = reader1["active_yn"].ToString(); // Replace with the actual column name

                            if (activeYnValue == "Y")
                            {
                                chkStatus.Checked = true;
                            }
                            else
                            {
                                chkStatus.Checked = false;
                            }
                            // Populate other text fields similarly
                            Messages.UnsavedMsg(txtGrpId.Text);

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

        private void frmM_Group_Resize(object sender, EventArgs e)
        {
            UpdateToolbarVisibility();
        }

        private void frmM_Group_Deactivate(object sender, EventArgs e)
        {
            //UpdateToolbarVisibility();
        }

        private void txtGrpId_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Convert the text to uppercase
                textBox.Text = textBox.Text.ToUpper();
                // Set the cursor at the end of the text
                textBox.SelectionStart = textBox.Text.Length;
            }
        }

        private void txtGrpId_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtGrpId_KeyDown(object sender, KeyEventArgs e)
        {
            //    if (sender is TextBox textBox)
            //    {

            //        // Convert the text to uppercase
            //        textBox.Text = textBox.Text.ToUpper();

            //    }
        }

        private void txtGrpId_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string mode = DeTools.GetMode(DeTools.gobjActiveForm);
                if (mode == DeTools.MODIFYMODE)
                {
                    SearchForm();
                    //txtGrpId.Enabled=false;
                    txtGrpDesc.Focus();
                }
            }
        }

        private void frmM_Group_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void txtGrpDesc_Validating(object sender, CancelEventArgs e)
        {

        }
    }////////////////End///////////
}
