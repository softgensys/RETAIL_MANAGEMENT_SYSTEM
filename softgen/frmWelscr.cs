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
                // Assuming you have an OdbcParameter array
                var parameters = new OdbcParameter[1];
                parameters[0] = new OdbcParameter("LoginId", txtLogin_Id.Text);


                // Execute the query using your Dbconnector class
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
                        this.Hide();
                        //this.Close();
                        //this.Dispose();
                        //frmStart frmStart = new frmStart();

                        //frmStart.Show();
                        //frmStart.lblMsg.Refresh();

                        // Load mdiMain; // Assuming mdiMain is an MDI form, you need to handle this accordingly
                        MainForm mainForm = new MainForm();
                        mainForm.MdiParent = this.ParentForm; // Set the MDI parent
                        mainForm.Show();
                       // DeTools.DisplayForm(mainForm, 1215, 746);
                        openyn =false;

                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                // Messages.VBError(ex, this.Name, "cmdOk_Click");
                Application.Exit();
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
