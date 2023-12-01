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

namespace softgen
{
    public partial class frmM_Manuf : Form, Interface_for_Common_methods.ISearchableForm
    {

        private DbConnector dbConnector;
        private string mstrEntBy, mstrEntOn, mstrAuthBy, mstrAuthOn;
        public bool mblnSearch, mblnDataEntered;
        private OdbcTransaction transaction;
        Messages msg = new Messages();
        General general = new General();
        bool saveflag = true;

        public frmM_Manuf()
        {
            InitializeComponent();
            DbConnector dbConnector = new DbConnector();
            this.Activated += MyForm_Activated;
            this.KeyPreview = true; // Make sure the form has key preview enabled
            this.KeyUp += DeTools.Form_KeyUp; // Subscribe to the KeyUp event
        }

        private void MyForm_Activated(object sender, EventArgs e)
        {
            DeTools.ClearStatusBarHelp();
            DeTools.ActiveFileMenu(this);
            DeTools.CreatedBy(mstrEntBy, mstrEntOn);
            DeTools.PostedBy(mstrAuthBy, mstrAuthOn);

        }

        public void ClearForm()
        {
            mblnDataEntered = false;
            mstrEntBy = null;
            mstrEntOn = null;
            mstrAuthBy = null;
            mstrAuthOn = null;
            DeTools.ClearTextNComboControls(this);

            txtManufId.Enabled = true;
            txtManufId.Focus();
        }


        private void frmM_Manuf_Load(object sender, EventArgs e)
        {

            General general = new General();
            DeTools.DisplayForm(this, 298, 587);
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(txtManufId, "Enter Manufacturer Id.");
            toolTip.SetToolTip(txtName, "Enter Manufacturer Name.");

            Help.controlToHelpTopicMapping.Add(txtManufId, "1048"); /////For Help ContextId///IMP..
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
                DeTools.gstrSQL = "select * from m_manuf where manuf_id='" + txtManufId.Text.Trim() + "' limit 1;  ";
                OdbcCommand cmd = new OdbcCommand(DeTools.gstrSQL, dbConnector.connection);
                dbConnector.connection.Open();

                OdbcDataReader reader = cmd.ExecuteReader();

                if (DeTools.GetMode(this) != DeTools.ADDMODE)
                {
                    if (reader.HasRows)
                    {

                        if (DeTools.CheckTemporaryTableExists("m_manuf") != null)
                        {

                            // The record exists, so update it
                            reader.Close();
                            Cursor.Current = Cursors.WaitCursor;

                            string gstrSQL1 = "Insert into temp_m_manuf (manuf_id, manuf_name, address_1, address_2, address_3," +
                                                " city, state_id, zip, country_id," +
                                                " notes, status, ent_by, ent_on, trans_status, open_yn, comp_name , mod_date, mod_by)" +
                                               "Select manuf_id, manuf_name, address_1, address_2, address_3," +
                                                " city, state_id, zip, country_id," +
                                                " notes, status, ent_by, ent_on, trans_status, open_yn, comp_name , mod_date, mod_by from m_manuf where manuf_id= '" + txtManufId.Text.Trim() + "'";

                            using (OdbcCommand insertintemp1 = new OdbcCommand(gstrSQL1, dbConnector.connection))
                            {
                                insertintemp1.ExecuteNonQuery();
                            }

                            string gstrSQL2 = "Select * from temp_m_manuf where manuf_id='" + txtManufId.Text.Trim() + "' and open_yn='Y'";
                            OdbcCommand selectintemp1 = new OdbcCommand(DeTools.gstrSQL, dbConnector.connection);

                            OdbcDataReader selectread = selectintemp1.ExecuteReader();

                            if (selectread.HasRows)
                            {
                                string delSQL = "Delete FROM m_manuf WHERE manuf_id = '" + txtManufId.Text.Trim() + "'; ";

                                using (OdbcCommand delfrmhdr1 = new OdbcCommand(delSQL, dbConnector.connection))
                                {
                                    delfrmhdr1.ExecuteNonQuery();
                                }

                                DeTools.gstrSQL = "update temp_m_manuf set manuf_id = ?, manuf_name = ?, address_1 = ?, address_2 = ?, address_3 = ?," +
                                    " city = ?, state_id = ?, zip = ?, country_id = ?," +
                                    " notes = ?, status = ?, trans_status = ?," +
                                    " comp_name  =  '" + DeTools.fOSMachineName.GetMachineName() + "' , mod_date = ?, mod_by = ? where manuf_id= '" + txtManufId.Text.Trim() + "'";

                                cmd.CommandText = DeTools.gstrSQL;


                                cmd.Parameters.Add(new OdbcParameter("manuf_id", txtManufId.Text.Trim()));
                                cmd.Parameters.Add(new OdbcParameter("manuf_name", txtName.Text.Trim()));

                                cmd.Parameters.Add(new OdbcParameter("address_1", txtAdd1.Text.Trim()));
                                cmd.Parameters.Add(new OdbcParameter("address_2", txtAdd2.Text.Trim()));
                                cmd.Parameters.Add(new OdbcParameter("address_3", txtAdd3.Text.Trim()));
                                cmd.Parameters.Add(new OdbcParameter("city", txtCity.Text.Trim()));

                                cmd.Parameters.Add(new OdbcParameter("state_id", txtState.Text.Trim()));
                                cmd.Parameters.Add(new OdbcParameter("zip", txtZip.Text.Trim()));
                                cmd.Parameters.Add(new OdbcParameter("country_id", txtCountry.Text.Trim()));
                                cmd.Parameters.Add(new OdbcParameter("notes", txtNotes.Text.Trim()));
                                cmd.Parameters.Add(new OdbcParameter("status", "V"));
                                cmd.Parameters.Add(new OdbcParameter("Trans_status", "N"));

                                cmd.Parameters.Add(new OdbcParameter("mod_date", OdbcType.DateTime)).Value = DateTime.Now;
                                cmd.Parameters.Add(new OdbcParameter("mod_by", DeTools.gstrloginId));


                                cmd.ExecuteNonQuery();

                                Cursor.Current = Cursors.Default;

                                Messages.SavingMsg();


                                //DeTools.SelectDataFromTemporaryTable("m_group");
                                reader.Close();

                                string insertQuery = "Insert into m_manuf  (manuf_id, manuf_name, address_1, address_2, address_3," +
                                                " city, state_id, zip, country_id," +
                                                " notes, status, ent_by, ent_on, trans_status, mod_date, mod_by) " +
                                                "Select manuf_id, manuf_name, address_1, address_2, address_3," +
                                                " city, state_id, zip, country_id," +
                                                " notes, status, ent_by, ent_on, trans_status," +
                                                 " mod_date, mod_by from temp_m_manuf where manuf_id='" + txtManufId.Text.Trim() + "'";

                                using (OdbcCommand insertCmd = new OdbcCommand(insertQuery, dbConnector.connection))
                                {
                                    insertCmd.ExecuteNonQuery();

                                }
                                string querupdN1 = "update temp_m_manuf set open_yn='N' where manuf_id=? order by ent_on desc ";

                                using (OdbcCommand querupdNCmd = new OdbcCommand(querupdN1, dbConnector.connection))
                                {
                                    querupdNCmd.Parameters.Add(new OdbcParameter("manuf_id", txtManufId.Text.ToString().Trim()));
                                    querupdNCmd.ExecuteNonQuery();


                                }

                                string querdel1 = "delete from temp_m_manuf where manuf_id=? order by ent_on desc ";

                                using (OdbcCommand querdelCmd = new OdbcCommand(querdel1, dbConnector.connection))
                                {
                                    querdelCmd.Parameters.Add(new OdbcParameter("manuf_id", txtManufId.Text.ToString().Trim()));
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

                    if (DeTools.CheckTemporaryTableExists("m_manuf") != null)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        DeTools.gstrSQL = "INSERT INTO temp_m_manuf (manuf_id, manuf_name, address_1, address_2, address_3," +
                                                " city, state_id, zip, country_id," +
                                                " notes, status, ent_by, ent_on, trans_status, open_yn, comp_name )" +
                                         " VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                        OdbcCommand cmdd = new OdbcCommand(DeTools.gstrSQL, dbConnector.connection);

                        cmdd.CommandText = DeTools.gstrSQL;


                        cmdd.Parameters.Add(new OdbcParameter("manuf_id", txtManufId.Text.Trim()));
                        cmdd.Parameters.Add(new OdbcParameter("manuf_name", txtName.Text.Trim()));

                        cmdd.Parameters.Add(new OdbcParameter("address_1", txtAdd1.Text.Trim()));
                        cmdd.Parameters.Add(new OdbcParameter("address_2", txtAdd2.Text.Trim()));
                        cmdd.Parameters.Add(new OdbcParameter("address_3", txtAdd3.Text.Trim()));
                        cmdd.Parameters.Add(new OdbcParameter("city", txtCity.Text.Trim()));

                        cmdd.Parameters.Add(new OdbcParameter("state_id", txtState.Text.Trim()));
                        cmdd.Parameters.Add(new OdbcParameter("zip", txtZip.Text.Trim()));
                        cmdd.Parameters.Add(new OdbcParameter("country_id", txtCountry.Text.Trim()));
                        cmdd.Parameters.Add(new OdbcParameter("notes", txtNotes.Text.Trim()));
                        cmdd.Parameters.Add(new OdbcParameter("status", "V"));
                        cmdd.Parameters.Add(new OdbcParameter("ent_by", DeTools.gstrloginId));
                        cmdd.Parameters.Add(new OdbcParameter("ent_on", OdbcType.DateTime)).Value = DateTime.Now;

                        cmdd.Parameters.Add(new OdbcParameter("Trans_status", "N"));

                        cmdd.Parameters.Add(new OdbcParameter("comp_name", DeTools.fOSMachineName.GetMachineName()));
                        cmdd.Parameters.Add(new OdbcParameter("open_yn", "Y"));

                        cmdd.ExecuteNonQuery();

                        //DeTools.SelectDataFromTemporaryTable("m_group");
                        //reader.Close();

                        string insertQuery = "Insert into m_manuf (manuf_id, manuf_name, address_1, address_2, address_3," +
                                                " city, state_id, zip, country_id," +
                                                " notes, status, ent_by, ent_on, trans_status)" +
                                             "Select manuf_id, manuf_name, address_1, address_2, address_3," +
                                                " city, state_id, zip, country_id," +
                                                " notes, status, ent_by, ent_on, trans_status" +
                                               " from temp_m_manuf where manuf_id= '" + txtManufId.Text.Trim() + "'";

                        using (OdbcCommand insertCmd = new OdbcCommand(insertQuery, dbConnector.connection))
                        {
                            insertCmd.ExecuteNonQuery();

                        }

                        reader.Close();
                        Messages.SavingMsg();
                        Cursor.Current = Cursors.Default;

                        string quer1 = "update temp_m_manuf set open_yn='N' where manuf_id='" + txtManufId.Text.ToString().Trim() + "' order by ent_on desc ";
                        using (OdbcCommand qurCmd = new OdbcCommand(quer1, dbConnector.connection))
                        {
                            qurCmd.ExecuteNonQuery();
                        }

                        string querdel1 = "delete from temp_m_manuf where manuf_id=? order by ent_on desc ";

                        using (OdbcCommand querdelCmd = new OdbcCommand(querdel1, dbConnector.connection))
                        {
                            querdelCmd.Parameters.Add(new OdbcParameter("manuf_id", txtManufId.Text.ToString().Trim()));
                            querdelCmd.ExecuteNonQuery();

                        }
                    }

                }

                Messages.SavedMsg();
                dbConnector.connection.Close();
                ClearForm();

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
                if (!string.IsNullOrWhiteSpace(txtManufId.Text) && !mblnSearch)
                {
                    msg.HelpMsg("Information retrieving. Please wait...");

                    DeTools.gstrSQL = "SELECT * FROM m_manuf WHERE manuf_id = '" + txtManufId.Text.Trim() + "'";

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

                            else
                            {
                                txtManufId.Enabled = false;
                                txtName.Text = reader["manuf_name"].ToString();

                                txtAdd1.Text = reader["address_1"].ToString().Trim();
                                txtAdd2.Text = reader["address_2"].ToString();
                                txtAdd3.Text = reader["address_3"].ToString();
                                txtCity.Text = reader["city"].ToString();

                                txtState.Text = reader["state_id"].ToString();
                                txtZip.Text = reader["zip"].ToString();
                                txtCountry.Text = reader["country_id"].ToString();
                                txtNotes.Text = reader["notes"].ToString().Trim();


                                mstrEntBy = general.GetuserName(reader["ent_by"].ToString());
                                DateTime entOn = Convert.ToDateTime(reader["ent_on"]);
                                mstrEntOn = entOn.ToString("dd/MM/yyyy");
                                DeTools.CreatedBy(mstrEntBy, mstrEntOn);
                                txtManufId.Text = reader["manuf_id"].ToString().Trim();


                                if (reader["status"].ToString() == "A")
                                {
                                    mstrAuthBy = general.GetuserName(reader["auth_by"].ToString());
                                    DateTime AuthOn = Convert.ToDateTime(reader["auth_on"].ToString());
                                    mstrAuthOn = AuthOn.ToString("dd/MM/yyyy");
                                    DeTools.PostedBy(mstrAuthBy, mstrAuthOn);
                                }
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

                ClearForm();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UnsavedData()
        {
            DbConnector dbConnector = new DbConnector();


            try
            {
                if (DeTools.CheckTemporaryTableExists("m_manuf") != null)
                {
                    dbConnector.OpenConnection();

                    string compname = DeTools.fOSMachineName.GetMachineName();
                    string user = MainForm.Instance.pnlUserName.Text.Trim();

                    string query = "SELECT * FROM temp_m_manuf WHERE open_yn='Y' and ent_by='" + user.Trim() + "' and comp_name='" + compname.Trim() + "' order by ent_on desc limit 1;";

                    OdbcParameter[] parameters = new OdbcParameter[0];


                    using (OdbcDataReader reader = dbConnector.ExecuteReader(query, parameters))
                    {
                        if (reader == null)
                        {

                        }

                        else if (reader.HasRows)
                        {

                            txtManufId.Text = reader["manuf_id"].ToString().Trim();
                            txtName.Text = reader["manuf_name"].ToString();

                            txtAdd1.Text = reader["address_1"].ToString();
                            txtAdd2.Text = reader["address_2"].ToString();
                            txtAdd3.Text = reader["address_3"].ToString();
                            txtCity.Text = reader["city"].ToString();

                            txtState.Text = reader["state_id"].ToString();
                            txtZip.Text = reader["zip"].ToString();
                            txtCountry.Text = reader["country_id"].ToString();
                            txtNotes.Text = reader["notes"].ToString().Trim();


                            mstrEntBy = general.GetuserName(reader["ent_by"].ToString());
                            DateTime entOn = Convert.ToDateTime(reader["ent_on"]);
                            mstrEntOn = entOn.ToString("dd/MM/yyyy");
                            DeTools.CreatedBy(mstrEntBy, mstrEntOn);


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

        private void frmM_Manuf_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (txtManufId.Text != "" || txtName.Text != "")
            {
                dbConnector = new DbConnector();
                // dbConnector.connectionString= new OdbcConnection();
                dbConnector.connection = new OdbcConnection(dbConnector.connectionString);
                dbConnector.connection.Open();

                // transaction = dbConnector.connection.BeginTransaction();

                string selectid_toenter = "select manuf_id from m_manuf where manuf_id='" + txtManufId.Text.Trim() + "'";
                string selectidtemp_toenter = "select manuf_id from temp_m_manuf where manuf_id='" + txtManufId.Text.Trim() + "'";

                using (OdbcDataReader readertemp = dbConnector.CreateResultset(selectidtemp_toenter))
                {
                    if (!readertemp.HasRows)
                    {

                    }


                    using (OdbcDataReader reader = dbConnector.CreateResultset(selectid_toenter))
                    {
                        if (!reader.HasRows)
                        {
                            string insert = "INSERT INTO temp_m_manuf (manuf_id, manuf_name, address_1, address_2, address_3," +
                                                " city, state_id, zip, country_id," +
                                                " notes, status, ent_by, ent_on, trans_status, open_yn, comp_name )" +
                                            " VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
                            OdbcCommand cmd = new OdbcCommand(insert, dbConnector.connection);
                            //cmd.Transaction = transaction;


                            cmd.CommandText = insert;
                            cmd.Parameters.Add(new OdbcParameter("manuf_id", txtManufId.Text.Trim()));
                            cmd.Parameters.Add(new OdbcParameter("manuf_name", txtName.Text.Trim()));

                            cmd.Parameters.Add(new OdbcParameter("address_1", txtAdd1.Text.Trim()));
                            cmd.Parameters.Add(new OdbcParameter("address_2", txtAdd2.Text.Trim()));
                            cmd.Parameters.Add(new OdbcParameter("address_3", txtAdd3.Text.Trim()));
                            cmd.Parameters.Add(new OdbcParameter("city", txtCity.Text.Trim()));

                            cmd.Parameters.Add(new OdbcParameter("state_id", txtState.Text.Trim()));
                            cmd.Parameters.Add(new OdbcParameter("zip", txtZip.Text.Trim()));
                            cmd.Parameters.Add(new OdbcParameter("country_id", txtCountry.Text.Trim()));
                            cmd.Parameters.Add(new OdbcParameter("notes", txtNotes.Text.Trim()));
                            cmd.Parameters.Add(new OdbcParameter("status", "V"));
                            cmd.Parameters.Add(new OdbcParameter("ent_by", DeTools.gstrloginId));
                            cmd.Parameters.Add(new OdbcParameter("ent_on", OdbcType.DateTime)).Value = DateTime.Now;

                            cmd.Parameters.Add(new OdbcParameter("Trans_status", "N"));

                            cmd.Parameters.Add(new OdbcParameter("comp_name", DeTools.fOSMachineName.GetMachineName()));
                            cmd.Parameters.Add(new OdbcParameter("open_yn", "Y"));

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

        private void frmM_Manuf_FormClosed(object sender, FormClosedEventArgs e)
        {
            DeTools.DestroyToolbar(this);
            MainForm.Instance.mnuMManuf.Enabled = true;
        }

    }//-END
}
