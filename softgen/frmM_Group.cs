using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;


namespace softgen
{
    public partial class frmM_Group : Form, Interface_for_Common_methods.ISearchableForm
    {
        private DbConnector dbConnector;
        private string mstrEntBy, mstrEntOn, mstrAuthBy, mstrAuthOn;
        public bool mblnSearch, mblnDataEntered;
        private OdbcTransaction transaction;
        public frmM_Group()
        {
            InitializeComponent();
            dbConnector = new DbConnector();

            this.Activated += MyForm_Activated;
        }


        private void MyForm_Activated(object sender, EventArgs e)
        {
            DeTools.ClearStatusBarHelp();
            DeTools.ActiveFileMenu(this);
            DeTools.CreatedBy(mstrEntBy, mstrEntOn);
            DeTools.PostedBy(mstrAuthBy, mstrAuthOn);



        }

        private void Group_Load(object sender, EventArgs e)
        {
            DeTools.DisplayForm(this, 300, 420);
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(txtGrpId, "Enter Group Id.");
            toolTip.SetToolTip(txtGrpDesc, "Enter Group Description.");
            Help.controlToHelpTopicMapping.Add(txtGrpId, "1004"); /////For Help ContextId///IMP...

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

                // Check if the record with the specified Group_id exists
                DeTools.gstrSQL = "SELECT * FROM m_Group WHERE Group_id = '" + txtGrpId.Text.Trim() + "'";
                OdbcCommand cmd = new OdbcCommand(DeTools.gstrSQL, dbConnector.connection);
                dbConnector.connection.Open();
                OdbcDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    // The record exists, so update it
                    reader.Close();
                    transaction = dbConnector.connection.BeginTransaction();
                    cmd.Transaction = transaction;
                    Messages.SavingMsg();

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

                    string quer = "update temp_m_group set closed_yn='N' where group_id=? order by ent_on desc ";

                    transaction.Commit();
                }
                else
                {
                    // The record does not exist, so insert a new one
                    reader.Close();
                    transaction = dbConnector.connection.BeginTransaction();
                    cmd.Transaction = transaction;
                    if (DeTools.CheckTemporaryTableExists("m_group") != null)
                    {

                        Messages.SavingMsg();

                        DeTools.gstrSQL = "INSERT INTO temp_m_Group (Group_id, group_desc, active_yn, sales_tax, ent_by, ent_on, trans_status, closed_yn,comp_name) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";
                        cmd.CommandText = DeTools.gstrSQL;
                        cmd.Parameters.Add(new OdbcParameter("Group_id", txtGrpId.Text));
                        cmd.Parameters.Add(new OdbcParameter("group_desc", txtGrpDesc.Text));
                        cmd.Parameters.Add(new OdbcParameter("active_yn", chkStatus.Checked ? "Y" : "N"));
                        cmd.Parameters.Add(new OdbcParameter("sales_tax", txtSTaxPer.Text));
                        cmd.Parameters.Add(new OdbcParameter("ent_by", DeTools.gstrloginId));
                        cmd.Parameters.Add(new OdbcParameter("ent_on", OdbcType.DateTime)).Value = DateTime.Now;
                        cmd.Parameters.Add(new OdbcParameter("trans_status", "N"));
                        cmd.Parameters.Add(new OdbcParameter("closed_yn", "N"));
                        cmd.Parameters.Add(new OdbcParameter("comp_name", DeTools.fOSMachineName.GetMachineName()));
                        //cmd.Parameters.Add(new OdbcParameter("status", "V"));

                        cmd.ExecuteNonQuery();

                        //DeTools.SelectDataFromTemporaryTable("m_group");

                        transaction.Commit();
                        DeTools.InsertDataIntoMainTable("m_group", "group_id", txtGrpId.Text.ToString().Trim());
                    }
                }
                dbConnector.connection.Close();

                Messages.SavedMsg();

                // Additional logic here for clearing fields, displaying messages, etc.
            }
            catch (Exception ex)
            {
                // Handle the exception or display an error message
                Console.WriteLine(ex.Message);

                // Rollback the transaction if an error occurs
                if (transaction != null)
                {
                    transaction.Rollback();
                }

                // Additional error handling and messages
            }
            finally
            {
                dbConnector.CloseConnection();
            }

        }

        private void txtGrpId_Validating(object sender, CancelEventArgs e)

        {
            if (this.Text.Contains("<Add>"))
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

                transaction = dbConnector.connection.BeginTransaction();
                string insert = "INSERT INTO temp_m_Group (Group_id, group_desc, active_yn, sales_tax, ent_by, ent_on, trans_status, closed_yn) VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
                OdbcCommand cmd = new OdbcCommand(insert, dbConnector.connection);
                cmd.Transaction = transaction;


                cmd.CommandText = insert;
                cmd.Parameters.Add(new OdbcParameter("Group_id", txtGrpId.Text));
                cmd.Parameters.Add(new OdbcParameter("group_desc", txtGrpDesc.Text));
                cmd.Parameters.Add(new OdbcParameter("active_yn", chkStatus.Checked ? "Y" : "N"));
                cmd.Parameters.Add(new OdbcParameter("sales_tax", txtSTaxPer.Text));
                cmd.Parameters.Add(new OdbcParameter("ent_by", DeTools.gstrloginId));
                cmd.Parameters.Add(new OdbcParameter("ent_on", OdbcType.DateTime)).Value = DateTime.Now;
                cmd.Parameters.Add(new OdbcParameter("trans_status", "N"));
                cmd.Parameters.Add(new OdbcParameter("closed_yn", "Y"));
                //cmd.Parameters.Add(new OdbcParameter("status", "V"));

                cmd.ExecuteNonQuery();

                //DeTools.SelectDataFromTemporaryTable("m_group");

                transaction.Commit();
                dbConnector.connection.Close();
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


                    // Define your SQL query
                    string query = "SELECT * FROM temp_m_group WHERE closed_yn='Y' order by ent_on desc limit 1;";

                    OdbcParameter[] parameters = new OdbcParameter[0];


                    using (OdbcDataReader reader = dbConnector.ExecuteReader(query, parameters))
                    {
                        if (reader.Read())
                        {
                            // Populate text fields with the data from the reader
                            txtGrpId.Text = reader["group_id"].ToString();
                            txtGrpDesc.Text = reader["group_desc"].ToString();
                            // Read the 'active_yn' value from the query result
                            string activeYnValue = reader["active_yn"].ToString(); // Replace with the actual column name

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


    }////////////////End///////////
}
