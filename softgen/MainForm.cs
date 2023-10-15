using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using softgen;
using System.Drawing.Text;
using System.DirectoryServices.ActiveDirectory;
using MySql.Data.MySqlClient;

namespace softgen
{
    public partial class MainForm : Form
    {
        private bool blnMDI_Loaded = false;
        frmStart frmStart = new frmStart();
        DbConnector dbConnector = new DbConnector();
        General general = new General();
        Messages messages = new Messages();

        // Static property to hold the instance of MainForm
        public static MainForm Instance { get; private set; }

        public MainForm()
        {
            InitializeComponent();

            // Initialize the static property with the current instance
            Instance = this;



            IsMdiContainer = true; // Set the form as an MDI container
            //MainMenuStrip.BackColor = Color.LightPink;
            this.Load += MainForm_Load;
            //btnAdd.Visible = false;
            //btnModify.Visible = false;
            //btnInquire.Visible = false;
            //btnMDelete.Visible = false;
            //btnDelete.Visible = false;
            //btnFresh.Visible = false;
            //btnAuth.Visible = false;
            //btnPost.Visible = false;
            //btnPrint.Visible = false;
            //btnSave.Visible = false;
            //btnRetrieve.Visible = false;

            this.Activated += MyForm_Activated;

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // foreach (Control ctrl in this.Controls)
            // {
            //     if (ctrl is MdiClient)
            //     {
            //         ctrl.BackColor = Color.PowderBlue;

            //     }
            // }

            // this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            //// this.mainpanel.Visible = true;
            // //this.formpanel.Visible = false;

            //bool blnDBOpen = false;
            //try
            //{


            //    foreach (Control ctrl in this.Controls)
            //    {
            //        if (ctrl is MdiClient)
            //        {
            //            ctrl.BackColor = Color.PowderBlue;

            //        }
            //    }

            //    if (!string.IsNullOrWhiteSpace(DeTools.strBrand))
            //    {
            //        this.Text = DeTools.strBrand.Trim();
            //    }
            //    else
            //    {
            //        this.Text = DeTools.strCompany.Trim();
            //    }

            //    Cursor.Current = Cursors.WaitCursor;

            //    frmStart.lblMsg.Text = "Checking permissions...";
            //    frmStart.lblMsg.Refresh();
            //    DeTools.CreateToolbar(this, "1");

            //    frmStart.lblMsg.Text = "Connecting...";
            //    frmStart.lblMsg.Refresh();

            //    if (!dbConnector.ConnectSGS_db())
            //    {
            //        Cursor.Current = Cursors.Default;
            //        Application.Exit();
            //    }

            //    blnDBOpen = true;

            //    frmStart.lblMsg.Text = "Initializing...";
            //    frmStart.lblMsg.Refresh();

            //    if (!DayBegin())
            //    {
            //        frmStart.Close();
            //        // dbConnector.CloseConnection();
            //        Application.Exit();
            //    }

            //    if (DeTools.strBrand.ToUpper().Trim() == "SA")
            //    {
            //        pnlUserName.Text = "Super user";
            //        Messages.InfoMsg("user: Super user");
            //    }
            //    else
            //    {
            //        string pnlUserName = general.GetuserName(DeTools.gstrloginId.Trim());
            //        Messages.InfoMsg("user: " + pnlUserName.Trim());
            //    }

            //    messages.gstrMsg = messages.gstrMsg + "                                    ";
            //    pnlDate.Text = DateTime.Parse(DeTools.gstrsetup[3]).ToString("d MMMM yyyy");
            //    messages.gstrMsg = messages.gstrMsg + "Date: " + pnlDate;

            //    messages.gstrMsg = messages.gstrMsg + "                                    ";
            //    pnlLoginTime.Text = DateTime.Now.ToString("h:mm tt");
            //    messages.gstrMsg = messages.gstrMsg + "Login Time: " + pnlLoginTime;
            //    //pnlUser = gstrMsg;

            //    Show();

            //    DeTools.BringToolStripToFront(this.Name); // Use the new method for bringing ToolStrip to front

            //    frmStart.Close();
            //    Cursor.Current = Cursors.Default;
            //}
            //catch (Exception ex)
            //{
            //    Cursor.Current = Cursors.Default;
            //    messages.VBError(ex, this.Name, "MainForm_Load_1");
            //    //frmHelp.Close();
            //    frmStart.Close();
            //    if (blnDBOpen)
            //        dbConnector.CloseConnection();
            //    Application.Exit();
            //}
            bool blnDBOpen = false;
            try
            {
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is MdiClient)
                    {
                        ctrl.BackColor = Color.PowderBlue;
                    }
                }

                if (!string.IsNullOrWhiteSpace(DeTools.strBrand))
                {
                    this.Text = DeTools.strBrand.Trim();
                }
                else
                {
                    this.Text = DeTools.strCompany.Trim();
                }

                Cursor.Current = Cursors.WaitCursor;

                frmStart.lblMsg.Text = "Checking permissions...";
                frmStart.lblMsg.Refresh();
                DeTools.CreateToolbar(this, "1");

                frmStart.lblMsg.Text = "Connecting...";
                frmStart.lblMsg.Refresh();

                if (!dbConnector.ConnectSGS_db())
                {
                    Cursor.Current = Cursors.Default;
                    Application.Exit();
                }

                blnDBOpen = true;

                frmStart.lblMsg.Text = "Initializing...";
                frmStart.lblMsg.Refresh();

                if (!DayBegin())
                {
                    frmStart.Close();
                    // dbConnector.CloseConnection();
                    Application.Exit();
                }

                if (DeTools.strBrand.ToUpper().Trim() == "SA")
                {
                    pnlUserName.Text = "Super user";
                    Messages.InfoMsg("user: Super user");
                }
                else
                {
                    pnlUserName.Text = general.GetuserName(DeTools.gstrloginId.Trim());
                    //Messages.InfoMsg("user: " + pnlUserName.Trim());
                }

                Messages.gstrMsg = Messages.gstrMsg + "                                    ";
                pnlDate.Text = DateTime.Parse(DeTools.gstrsetup[3]).ToString("d MMMM yyyy");
                Messages.gstrMsg = Messages.gstrMsg + "Date: " + pnlDate;

                Messages.gstrMsg = Messages.gstrMsg + "                                    ";
                pnlLoginTime.Text = DateTime.Now.ToString("h:mm tt");
                Messages.gstrMsg = Messages.gstrMsg + "Login Time: " + pnlLoginTime;
                //pnlUser = gstrMsg;

                Show();


                if (DeTools.toolbarDictionary.ContainsKey(this))
                {
                    ToolStrip currentToolbar = DeTools.toolbarDictionary[this];
                    currentToolbar.BringToFront();
                }

                // Close the start form, set the cursor to default, and continue
                frmStart.Close();
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Cursor.Current = Cursors.Default;
                messages.VBError(ex, this.Name, "MainForm_Load");
                frmStart.Close();
                if (blnDBOpen)
                    dbConnector.CloseConnection();
                Application.Exit();
            }

        }

        private void MyForm_Activated(object sender, EventArgs e)
        {
            if (!blnMDI_Loaded)
            {
                DeTools.gintFormHeight = this.ClientSize.Height;
                DeTools.gintFormWidth = this.ClientSize.Width;
                blnMDI_Loaded = true;
            }


        }

        /// <summary>
        /// ////////////////MDI CHILD FOR MENU
        /// </summary>
        //////////////////////// for MASTER////////////////////////////////////
        //for group
        private void OpenChildForm()
        {
            frmM_Group childForm = new frmM_Group();
            childForm.MdiParent = this; // Set the MDI parent form
            childForm.Show();

        }
        //for item
        private void OpenChildForm2()
        {
            Item childForm2 = new Item();
            childForm2.MdiParent = this; // Set the MDI parent form
            childForm2.Show();

        }
        //for doc series
        private void OpenChildForm3()
        {
            DocSeriesMas childForm3 = new DocSeriesMas();
            childForm3.MdiParent = this; // Set the MDI parent form
            childForm3.Show();

        }
        //for doc type
        private void OpenChildForm4()
        {
            DoctypeMas childForm4 = new DoctypeMas();
            childForm4.MdiParent = this; // Set the MDI parent form
            childForm4.Show();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "http://www.softgensys.com/contact.html",
                UseShellExecute = true
            };

            Process.Start(psi);


        }

        private void groupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm();
            Mgroupmenu.Enabled = false;
            //dashpanel.Visible = false;
            // formpanel.Visible = false;
            //mainpanel.Visible = true;

        }

        private void itemToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        //private void button9_Click(object sender, EventArgs e)
        //{
        //    FlagManager.Flag = "Add";
        //    dashpanel.Visible = false;
        //    formpanel.Visible = true;
        //    //formpanel.BringToFront();

        //    // Add the FormPanel to the mainPanel's Controls collection
        //    panel.Controls.Add(formpanel);

        //    // Position the FormPanel within the mainPanel
        //    formpanel.Location = new Point(0, 1); // Adjust as needed

        //    // Bring the FormPanel to the front
        //    formpanel.BringToFront();
        //    mainpanel.Visible = false;
        //    button10.Visible = false;
        //    dltbtn.Visible = false;
        //    savebtn.Visible = true;

        //    /*Item ItemForm = new Item();

        //    if (FlagManager.Flag == "Add")
        //    {
        //        ItemForm.Tag = "<Add>";
        //    }
        //    else if (FlagManager.Flag == "Modify")
        //    {
        //        ItemForm.Text = "Item <Modify>";
        //    }
        //    else
        //    {
        //        // Default text if Flag is not recognized
        //        ItemForm.Text = "Item";
        //    }*/


        //    ////////////////////////////
        //    string newTitle = "Item<Add>";

        //    // Call the method in Form 1 to update the title

        //}


        //private void button8_Click(object sender, EventArgs e)
        //{

        //    FlagManager.Flag = "Modify";
        //    dashpanel.Visible = false;
        //    formpanel.Visible = true;
        //    mainpanel.Visible = false;


        //}


        //private void button7_Click(object sender, EventArgs e)
        //{
        //    FlagManager.Flag = "Delete";
        //    dashpanel.Visible = false;
        //    formpanel.Visible = true;
        //    mainpanel.Visible = false;


        //}

        //private void button6_Click(object sender, EventArgs e)
        //{
        //    FlagManager.Flag = "Inquire";
        //    dashpanel.Visible = false;
        //    formpanel.Visible = true;
        //    mainpanel.Visible = false;
        //}

        //private void button5_Click(object sender, EventArgs e)
        //{
        //    FlagManager.Flag = "Post";
        //    dashpanel.Visible = false;
        //    formpanel.Visible = true;
        //    mainpanel.Visible = false;
        //}

        //private void MItemmenu_Click(object sender, EventArgs e)
        //{

        //    //Item itemForm = new Item();
        //    //itemForm.Show();
        //    OpenChildForm2();
        //    dashpanel.Visible = false;
        //    formpanel.Visible = false;
        //    mainpanel.Visible = true;
        //}

        //private void documentSeriesToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    OpenChildForm3();
        //    dashpanel.Visible = false;
        //    formpanel.Visible = false;
        //    mainpanel.Visible = true;
        //}

        //private void documentTypeToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    OpenChildForm4();
        //    dashpanel.Visible = false;
        //    formpanel.Visible = false;
        //    mainpanel.Visible = true;
        //}

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void mainpanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void Mainquitbtn_Click(object sender, EventArgs e)
        {

        }

        private void tbrTools_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void mnudelete_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        //private void MainForm_Load_1(object sender, EventArgs e)
        //{
        //    {
        //        bool blnDBOpen = false;
        //        try
        //        {
        //            if (!string.IsNullOrWhiteSpace(DeTools.strBrand))
        //            {
        //                this.Text = DeTools.strBrand.Trim();
        //            }
        //            else
        //            {
        //                this.Text = DeTools.strCompany.Trim();
        //            }

        //            Cursor.Current = Cursors.WaitCursor;

        //            // Uncomment and replace with appropriate code if you want to load an image
        //            // Me.Picture = LoadPicture("\\" + gastrSetup[2] + "\\Icons\\start2.BMP");
        //            // or use a resource file:
        //            // Me.Picture = LoadResPicture(101, vbResBitmap);


        //            frmStart.lblMsg.Text = "Checking permissions...";
        //            frmStart.lblMsg.Refresh();
        //            DeTools.CreateToolbar(this, null);
        //            //DeTools.DisplayMenu();

        //            frmStart.lblMsg.Text = "Connecting...";
        //            frmStart.lblMsg.Refresh();

        //            if (!dbConnector.ConnectSGS_db())
        //            {
        //                Cursor.Current = Cursors.Default;
        //                Application.Exit();
        //            }

        //            blnDBOpen = true;

        //            frmStart.lblMsg.Text = "Initializing...";
        //            frmStart.lblMsg.Refresh();

        //            if (!DayBegin())
        //            {
        //                // frmHelp.Close();
        //                frmStart.Close();
        //                dbConnector.CloseConnection();
        //                Application.Exit();
        //            }

        //            if (DeTools.strBrand.ToUpper().Trim() == "SA")
        //            {
        //                pnlUserName.Text = "Super user";
        //                Messages.InfoMsg("user : Super user");
        //            }
        //            else
        //            {
        //                string pnlUserName = general.GetuserName(DeTools.gstrloginId.Trim());
        //                Messages.InfoMsg("user : " + pnlUserName.Trim());
        //            }

        //            messages.gstrMsg = messages.gstrMsg + "                                    ";
        //            pnlDate.Text = DateTime.Parse(DeTools.gstrsetup[3]).ToString("d MMMM yyyy");
        //            messages.gstrMsg = messages.gstrMsg + "Date : " + pnlDate;

        //            messages.gstrMsg = messages.gstrMsg + "                                    ";
        //            pnlLoginTime.Text = DateTime.Now.ToString("h:mm tt");
        //            messages.gstrMsg = messages.gstrMsg + "Login Time : " + pnlLoginTime;
        //            pnlUser = gstrMsg;

        //            Show();

        //            //tbrTools[int.Parse(this.Tag.ToString())].BringToFront();
        //            int tagValue;
        //            if (int.TryParse(this.Tag.ToString(), out tagValue))
        //            {
        //                if (tagValue >= 0 && tagValue < tbrTools.Length)
        //                {
        //                    tbrTools[tagValue].BringToFront();
        //                }
        //            }
        //            frmStart.Close();
        //            Cursor.Current = Cursors.Default;
        //        }
        //        catch (Exception ex)
        //        {
        //            Cursor.Current = Cursors.Default;
        //            VBError(ex, this.Name, "MDIForm_Load");
        //            frmHelp.Close();
        //            frmStart.Close();
        //            if (blnDBOpen)
        //                gconSGS_db.Close();
        //            Application.Exit();
        //        }
        //    }
        //}

        private void MainForm_Load_1(object sender, EventArgs e)
        {

        }



        //private bool DayBegin()
        //{
        //    try
        //    {
        //        DateTime d_Date, p_Date;
        //        string connectionString = "Server=localhost;Database=softgen_db;Uid=root";
        //        string sql = "CALL GetServerDate(@d_Date, @p_Date);";

        //        using (MySqlConnection connection = new MySqlConnection(connectionString))
        //        {
        //            connection.Open();
        //            MySqlCommand cmd = new MySqlCommand(sql, connection);

        //            cmd.Parameters.AddWithValue("@d_Date", MySqlDbType.DateTime);
        //            cmd.Parameters["@d_Date"].Direction = ParameterDirection.Output;

        //            cmd.Parameters.AddWithValue("@p_Date", MySqlDbType.DateTime);
        //            cmd.Parameters["@p_Date"].Direction = ParameterDirection.Output;

        //            cmd.ExecuteNonQuery();

        //            d_Date = (DateTime)cmd.Parameters["@d_Date"].Value;
        //            p_Date = (DateTime)cmd.Parameters["@p_Date"].Value;
        //        }

        //        DeTools.gstrsetup[3] = d_Date.ToString("dd MMMM yyyy, dddd");
        //        DeTools.gstrsetup[4] = p_Date.ToString("dd-MM-yyyy");

        //        frmStart.lblDate.Text = DeTools.gstrsetup[3];
        //        frmStart.lblDate.Refresh();

        //        frmStart.lblMsg.Text = "Starting...";
        //        frmStart.lblMsg.Refresh();

        //        general.DayClose(DeTools.gstrsetup[4]);
        //        return true;
        //    }
        //    catch (MySqlException ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return false;
        //    }
        //}

        private bool DayBegin()
        {
            try
            {
                DateTime d_Date, p_Date;
                string connectionString = "Server=localhost;Database=softgen_db;Uid=root;"; // Add a semicolon at the end

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand("GetServerDate", connection); // Assuming "GetServerDate" is the name of your stored procedure

                    cmd.CommandType = CommandType.StoredProcedure; // Set the command type to stored procedure

                    cmd.Parameters.Add(new MySqlParameter("@Date", MySqlDbType.DateTime));
                    cmd.Parameters["@Date"].Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(new MySqlParameter("@PreDate", MySqlDbType.DateTime));
                    cmd.Parameters["@PreDate"].Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    d_Date = (DateTime)cmd.Parameters["@Date"].Value;
                    p_Date = (DateTime)cmd.Parameters["@PreDate"].Value;
                }

                DeTools.gstrsetup[3] = d_Date.ToString("dd MMMM yyyy, dddd");
                DeTools.gstrsetup[4] = p_Date.ToString("dd-MM-yyyy");

                frmStart.lblDate.Text = DeTools.gstrsetup[3];
                frmStart.lblDate.Refresh();

                frmStart.lblMsg.Text = "Starting...";
                frmStart.lblMsg.Refresh();

                general.DayClose(DeTools.gstrsetup[4]);
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }





        private void mnuAuthorise_Click(object sender, EventArgs e)
        {

        }

        private void masterHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pnlDate_Click(object sender, EventArgs e)
        {

        }

        //private void tmrActiveForm_Tick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string msg = "Click on any of the above buttons to complete the desired task.";

        //        if (ActiveMdiChild != null && ActiveMdiChild.Name != Name)
        //        {
        //            DeTools.gobjActiveForm = ActiveMdiChild;
        //            switch (DeTools.GetMode(DeTools.gobjActiveForm))
        //            {
        //                case DeTools.DELETEMODE:
        //                case DeTools.INQUIREMODE:
        //                case DeTools.POSTMODE:
        //                case "":
        //                    Messages.HelpMsg(msg);
        //                    break;
        //            }
        //        }

        //         else if (ActiveMdiChild != null && !string.IsNullOrEmpty(ActiveMdiChild.Tag as string))
        //            {

        //                if (int.TryParse(ActiveMdiChild.Tag.ToString(), out int tagValue))
        //                {
        //                    if (DeTools.toolbarDictionary.ContainsKey(ActiveMdiChild))
        //                    {
        //                        ToolStrip currentToolbar = DeTools.toolbarDictionary[ActiveMdiChild];
        //                        currentToolbar.BringToFront();
        //                    }
        //                }

        //            }




        //    }
        //    catch (Exception ex)
        //    {

        //        messages.VBError(ex, Name, "tmrActiveForm_Tick", "");
        //    }
        //}

        private void tmrActiveForm_Tick(object sender, EventArgs e)
        {
            try
            {
                string msg = "Click on any of the above buttons to complete the desired task.";

                if (ActiveMdiChild != null && ActiveMdiChild.Name != Name)
                {
                    DeTools.gobjActiveForm = ActiveMdiChild;
                    switch (DeTools.GetMode(DeTools.gobjActiveForm))
                    {
                        case DeTools.DELETEMODE:
                        case DeTools.INQUIREMODE:
                        case DeTools.POSTMODE:
                        case "":
                            Messages.HelpMsg(msg);
                            break;
                    }
                }

                if (ActiveMdiChild != null && !string.IsNullOrEmpty(ActiveMdiChild.Tag as string))
                {
                    if (int.TryParse(ActiveMdiChild.Tag.ToString(), out int tagValue))
                    {
                        if (DeTools.toolbarDictionary.ContainsKey(ActiveMdiChild))
                        {
                            ToolStrip currentToolbar = DeTools.toolbarDictionary[ActiveMdiChild];
                            currentToolbar.BringToFront();
                        }
                    }
                }
                else if (ActiveMdiChild == null)
                {
                    // There is no active child form, so you can ensure the main form's tbrtools is displayed here.
                    if (DeTools.toolbarDictionary.ContainsKey(this))
                    {
                        ToolStrip mainToolbar = DeTools.toolbarDictionary[this];
                        mainToolbar.BringToFront();
                    }
                }
            }
            catch (Exception ex)
            {
                messages.VBError(ex, Name, "tmrActiveForm_Tick", "");
            }
        }



    }///////////////////////////////////
}