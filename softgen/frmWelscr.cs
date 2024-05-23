using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace softgen
{
    public partial class frmWelscr : Form
    {
        public string gstrSQl1;
        frmStart frmStart1 = new frmStart();
        public bool openyn= true;

        public frmWelscr()
        {
            InitializeComponent();
           

        }

        //private void cmdOK_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string gstrRep = "";
        //        DbConnector dbconnector = new DbConnector();

        //        if (string.IsNullOrWhiteSpace(cboServer.Text))
        //        {
        //            MessageBox.Show("Select the server.", null, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }
        //        else
        //        {
        //            gstrRep = cboServer.Text.Trim();
        //        }

        //        if (string.IsNullOrWhiteSpace(txtLogin_Id.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
        //        {
        //            MessageBox.Show("Please enter both login ID and password.");
        //            return;
        //        }

        //        gstrSQl1 = "SELECT * FROM s_login WHERE Login_id = ?";
        //        // Assuming you have an OdbcParameter array
        //        var parameters = new OdbcParameter[1];
        //        parameters[0] = new OdbcParameter("LoginId", txtLogin_Id.Text);


        //        // Execute the query using your Dbconnector class
        //        var reader = dbconnector.ExecuteReader(gstrSQl1, parameters);

        //        if (!reader.Read())
        //        {
        //            reader.Close();
        //            Messages.IncorrectLoginMsg();
        //            txtLogin_Id.Focus();
        //        }
        //        else
        //        {
        //            if (string.Compare(DeTools.Decrypt(reader["Password"].ToString()), txtPassword.Text, StringComparison.OrdinalIgnoreCase) != 0)
        //            {
        //                reader.Close();
        //                Messages.IncorrectLoginMsg();
        //                txtLogin_Id.Focus();
        //            }
        //            else
        //            {
        //                DeTools.gstrloginId = reader["Login_id"].ToString();
        //                DeTools.gstrloginName = reader["Login_name"].ToString();
        //                reader.Close();
        //                General.InitialiseSetup(DeTools.gstrloginId, gstrRep);
        //                this.Hide();
        //                //this.Close();
        //                //this.Dispose();
        //                //frmStart frmStart = new frmStart();

        //                //frmStart.Show();
        //                //frmStart.lblMsg.Refresh();

        //                // Load mdiMain; // Assuming mdiMain is an MDI form, you need to handle this accordingly
        //                MainForm mainForm = new MainForm();
        //                mainForm.MdiParent = this.ParentForm; // Set the MDI parent
        //                mainForm.Show();
        //               // DeTools.DisplayForm(mainForm, 1215, 746);
        //                openyn =false;

        //            }
        //        }

        //        //----------------for day closing r_closing entry of today's date for validation back date billing
        //        DateTime closedt;
        //        string check_clsdt = "Select cls_dt from r_closing where doc_type_id='INV' order by cls_dt desc limit 1";
        //        OdbcDataReader check_clsdt_read = dbconnector.CreateResultset(check_clsdt);
        //        if (check_clsdt_read != null)
        //        {
        //            if (check_clsdt_read.HasRows && !check_clsdt_read.IsDBNull(0))
        //            {
        //                closedt = check_clsdt_read.GetDateTime(0);
        //                DateTime curdt = DateTime.Now.Date;

        //                if (closedt < curdt)
        //                {
        //                    string entry_clsdt = "Insert into r_closing (doc_type_id,cls_dt) values (?,?);";

        //                    OdbcCommand entry_clsdt_cmd = new OdbcCommand(entry_clsdt, dbconnector.connection);

        //                    entry_clsdt_cmd.Parameters.Add("doc_type_id", "INV");
        //                    entry_clsdt_cmd.Parameters.Add("cls_dt", DateTime.Now.Date);

        //                    entry_clsdt_cmd.ExecuteNonQuery();
        //                }

        //            }

        //        }
        //        check_clsdt_read.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exceptions
        //        // Messages.VBError(ex, this.Name, "cmdOk_Click");
        //        Application.Exit();
        //    }
        //}

        private void cmdOK_Click(object sender, EventArgs e)
        {
            try
            {
                string gstrRep = "";
                DbConnector dbconnector = new DbConnector();

                if (string.IsNullOrWhiteSpace(cboServer.Text))
                {
                    MessageBox.Show("Select the server.", null, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    gstrRep = cboServer.Text.Trim();
                }

                if (string.IsNullOrWhiteSpace(txtLogin_Id.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Please enter both login ID and password.");
                    return;
                }

                gstrSQl1 = "SELECT * FROM s_login WHERE Login_id = ?";
                var parameters = new OdbcParameter[1];
                parameters[0] = new OdbcParameter("LoginId", txtLogin_Id.Text);

                var reader = dbconnector.ExecuteReader(gstrSQl1, parameters);

                if (!reader.Read())
                {
                    reader.Close();
                    Messages.IncorrectLoginMsg();
                    txtLogin_Id.Focus();
                }
                else
                {
                    if (string.Compare(DeTools.Decrypt(reader["Password"].ToString()), txtPassword.Text, StringComparison.OrdinalIgnoreCase) != 0)
                    {
                        reader.Close();
                        Messages.IncorrectLoginMsg();
                        txtLogin_Id.Focus();
                    }
                    else
                    {
                        DeTools.gstrloginId = reader["Login_id"].ToString();
                        DeTools.gstrloginName = reader["Login_name"].ToString();
                        reader.Close();
                        General.InitialiseSetup(DeTools.gstrloginId, gstrRep);
                        openyn = false;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }

                // Additional code for day closing r_closing entry
                // ...

            }
            catch (Exception ex)
            {
                // Log the exception
                Logger.Log(ex.ToString());
                MessageBox.Show("An error occurred while opening the main form. Please check the log for details.");
            }
        }

        private void frmWelscr_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
               // frmStart1.Close();
            }
        }


        private void frmWelscr_Load(object sender, EventArgs e)
        {

        }
    }
}
