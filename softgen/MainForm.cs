using System.Data;
using System.Data.Odbc;
using System.Diagnostics;

namespace softgen
{
    public partial class MainForm : Form
    {
        private bool blnMDI_Loaded = false;
        //frmStart frmStart = new frmStart();
        DbConnector dbConnector = new DbConnector();
        public DbConnector dbConnector1;
        General general = new General();
        Messages messages = new Messages();
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public static bool Menu_Visible_yn = true;
        public static bool Main_Menu_Visible_yn = true;


        // Static property to hold the instance of MainForm
        public static MainForm Instance { get; private set; }

        public MainForm()
        {
            try
            {


                InitializeComponent();

                // Initialize the static property with the current instance
                Instance = this;



                IsMdiContainer = true; // Set the form as an MDI container
                                       //MainMenuStrip.BackColor = Color.LightPink;
                this.Load += MainForm_Load;


                this.Activated += MyForm_Activated;
                this.KeyPreview = true;
                //this.KeyDown += MainForm_KeyDown;
                this.KeyUp += new KeyEventHandler(DeTools.Form_KeyUp);

            }
            catch (Exception ex)
            {

            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

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

                //frmStart.Close();
                //frmStart.lblMsg.Text = "Checking permissions...";
                //frmStart.lblMsg.Refresh();
                DeTools.CreateToolbar(this, "1");

                //frmStart.lblMsg.Text = "Connecting...";
                //frmStart.lblMsg.Refresh();

                if (!dbConnector.ConnectSGS_db())
                {
                    Cursor.Current = Cursors.Default;
                    Application.Exit();
                }

                blnDBOpen = true;

                //frmStart.lblMsg.Text = "Initializing...";
                //frmStart.lblMsg.Refresh();

                if (!DayBegin())
                {
                    //frmStart.Close();
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

                //---old ------toolstrip dictionary
                //if (DeTools.toolbarDictionarywith_frmnm.ContainsKey(this.Text))
                //{
                //    ToolStrip currentToolbar = DeTools.toolbarDictionarywith_frmnm[this.Text];
                //    currentToolbar.BringToFront();
                //}
                string mode = DeTools.GetMode(this);
                string key = string.IsNullOrEmpty(mode) ? this.Name : $"{this.Name}-{mode}";

                if (DeTools.toolbarDictionary1.ContainsKey(key))
                {
                    List<ToolStrip> toolbars = DeTools.toolbarDictionary1[key];

                    foreach (ToolStrip currentToolbar in toolbars)
                    {
                        currentToolbar.BringToFront();
                    }
                }


                //----------------------

                // Close the start form, set the cursor to default, and continue
                //if (DeTools.toolbarDictionary1.TryGetValue(this, out List<ToolStrip> toolbars))
                //{
                //    // Retrieve the most recent toolbar for this form
                //    ToolStrip currentToolbar = toolbars.LastOrDefault();

                //    if (currentToolbar != null)
                //    {
                //        currentToolbar.BringToFront();
                //    }
                //}
                //frmStart.Close();
                //frmWelscr frmWelscr = new frmWelscr();

                //----*******for child menu*****************-------//               

                FetchMenuPermissions();

                //--------*********FOR SUB MENU*************-------------//
                SubMenu_Permission();
                //--------*********FOR MAIN MENU*************-------------//
                MainMenu_Permission();


                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Cursor.Current = Cursors.Default;
                messages.VBError(ex, this.Name, "MainForm_Load");
                //frmStart.Close();
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
            childForm.Activate();
        }
        //for item
        private void OpenChildForm2()
        {
            frmM_Item childForm2 = new frmM_Item();
            childForm2.MdiParent = this; // Set the MDI parent form
            childForm2.Show();
            childForm2.Activate();

        }

        //for item
        private void OpenChildForm5()
        {
            frmT_Invoice childForm5 = new frmT_Invoice();
            childForm5.MdiParent = this; // Set the MDI parent form
            childForm5.Show();
            childForm5.Activate();
        }

        //for doc series
        private void OpenChildForm3()
        {
            DocSeriesMas childForm3 = new DocSeriesMas();
            childForm3.MdiParent = this; // Set the MDI parent form
            childForm3.Show();
            childForm3.Activate();
        }
        //for doc type
        private void OpenChildForm4()
        {
            DoctypeMas childForm4 = new DoctypeMas();
            childForm4.MdiParent = this; // Set the MDI parent form
            childForm4.Show();
            childForm4.Activate();
        }
        //for sub group
        private void OpenChildForm6()
        {
            frmM_Sub_Group childForm = new frmM_Sub_Group();
            childForm.MdiParent = this; // Set the MDI parent form
            childForm.Show();
            childForm.Activate();
        }
        //for sub sub group
        private void OpenChildForm7()
        {
            frmM_Sub_Subgroup childForm = new frmM_Sub_Subgroup();
            childForm.MdiParent = this; // Set the MDI parent form
            childForm.Show();
            childForm.Activate();
        }
        //for Customer Master
        private void OpenChildForm8()
        {
            frmM_Cust childForm = new frmM_Cust();
            childForm.MdiParent = this; // Set the MDI parent form
            childForm.Show();
            childForm.Activate();
        }
        //for Supplier Master
        private void OpenChildForm9()
        {
            frmM_Supplier childForm = new frmM_Supplier();
            childForm.MdiParent = this; // Set the MDI parent form
            childForm.Show();
            childForm.Activate();
        }
        //for Manuf Master
        private void OpenChildForm10()
        {
            frmM_Manuf childForm = new frmM_Manuf();
            childForm.MdiParent = this; // Set the MDI parent form
            childForm.Show();
            childForm.Activate();
        }
        //for Sale Return
        private void OpenChildFormSR()
        {
            frmT_Sale_Return childForm = new frmT_Sale_Return();
            childForm.MdiParent = this; // Set the MDI parent form
            childForm.Show();
            childForm.Activate();
        }

        //for item
        private void OpenChildFormSale_Rpt_InvWiseSaleRpt()
        {
            frmR_invoice_wise_sale_rpt childForm = new frmR_invoice_wise_sale_rpt();
            childForm.MdiParent = this; // Set the MDI parent form
            childForm.Show();
            childForm.Activate();

        }

        //for Grant Screen Perm
        private void OpenChildFormfrmU_Grantscreen_perm()
        {
            frmU_GrantScreenPermission childForm = new frmU_GrantScreenPermission();
            childForm.MdiParent = this; // Set the MDI parent form
            childForm.Show();
            childForm.Activate();

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
                DbConnector dbConnector = new DbConnector();
                dbConnector.connection = new OdbcConnection(dbConnector.connectionString);
                dbConnector.connection.Open();

                string gstrSQL = "{ CALL GetServerDate(?, ?) }";

                using (OdbcCommand command = new OdbcCommand(gstrSQL, dbConnector.connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new OdbcParameter("@Date", OdbcType.DateTime));
                    command.Parameters["@Date"].Direction = ParameterDirection.Output;

                    command.Parameters.Add(new OdbcParameter("@PreDate", OdbcType.DateTime));
                    command.Parameters["@PreDate"].Direction = ParameterDirection.Output;

                    command.ExecuteNonQuery();

                    d_Date = (DateTime)command.Parameters["@Date"].Value;
                    p_Date = (DateTime)command.Parameters["@PreDate"].Value;
                }

                dbConnector.connection.Close();

                DeTools.gstrsetup[3] = d_Date.ToString("dd MMMM yyyy, dddd");
                DeTools.gstrsetup[4] = p_Date.ToString("dd-MM-yyyy");

                // frmStart.lblDate.Text = DeTools.gstrsetup[3];
                // frmStart.lblDate.Refresh();

                // frmStart.lblMsg.Text = "Starting...";
                // frmStart.lblMsg.Refresh();

                general.DayClose(DeTools.gstrsetup[4]);
                return true;
            }
            catch (OdbcException ex)
            {
                Logger.Error(ex, "Failed to execute DayBegin.");
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

        private string GetFormKey(Form form)
        {
            string mode = DeTools.GetMode(form);
            if (mode != "")
            {
                return string.IsNullOrEmpty(mode) ? form.Name : $"{form.Name}-{mode}";
            }
            else
            {
                return form.Name;
            }
        }

        private void tmrActiveForm_Tick(object sender, EventArgs e)
        {
            try
            {
                Messages messages = new Messages();
                string msg = "Click on any of the above buttons to complete the desired task.";

                // Hide all toolbars initially
                foreach (var toolbars in DeTools.toolbarDictionary1.Values)
                {
                    foreach (ToolStrip toolbar in toolbars)
                    {
                        toolbar.Visible = false;
                    }
                }

                if (ActiveMdiChild != null && ActiveMdiChild.Name != Name)
                {
                    DeTools.gobjActiveForm = ActiveMdiChild;
                    switch (DeTools.GetMode(DeTools.gobjActiveForm))
                    {
                        case DeTools.DELETEMODE:
                        case DeTools.INQUIREMODE:
                        case DeTools.POSTMODE:
                        case "":
                            messages.HelpMsg(msg);
                            break;
                    }

                    string formKey = GetFormKey(DeTools.gobjActiveForm);
                    if (formKey != null && DeTools.toolbarDictionary1.ContainsKey(formKey))
                    {
                        List<ToolStrip> toolbars = DeTools.toolbarDictionary1[formKey];
                        foreach (ToolStrip currentToolbar in toolbars)
                        {
                            currentToolbar.Visible = true;
                            currentToolbar.BringToFront();
                        }
                    }
                }
                else if (ActiveMdiChild == null)
                {
                    // There is no active child form, so display the main form's toolbars.
                    if (DeTools.toolbarDictionary1.ContainsKey(this.Name))
                    {
                        List<ToolStrip> mainToolbars = DeTools.toolbarDictionary1[this.Name];
                        foreach (ToolStrip mainToolbar in mainToolbars)
                        {
                            mainToolbar.Visible = true;
                            mainToolbar.BringToFront();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                messages.VBError(ex, Name, "tmrActiveForm_Tick", "");
            }
        }



        //-------******02-jul-2024
        //private void tmrActiveForm_Tick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Messages messages = new Messages();
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
        //                    messages.HelpMsg(msg);
        //                    break;
        //            }
        //        }

        //        if (ActiveMdiChild != null && ActiveMdiChild.Tag != null)
        //        {
        //            if (int.TryParse(ActiveMdiChild.Tag.ToString(), out int tagValue))
        //            {
        //                string formKey = GetFormKey(DeTools.gobjActiveForm);
        //                if (formKey != null)
        //                {
        //                    if (DeTools.toolbarDictionary1.ContainsKey(formKey))
        //                    {
        //                        List<ToolStrip> toolbars = DeTools.toolbarDictionary1[formKey];

        //                        foreach (ToolStrip currentToolbar in toolbars)
        //                        {
        //                            currentToolbar.Visible = true;
        //                            currentToolbar.BringToFront();

        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        else if (ActiveMdiChild == null)
        //        {

        //            // There is no active child form, so you can ensure the main form's tbrtools is displayed here.
        //            if (DeTools.toolbarDictionary1.ContainsKey(this.Name))
        //            {
        //                List<ToolStrip> mainToolbars = DeTools.toolbarDictionary1[this.Name];

        //                foreach (ToolStrip mainToolbar in mainToolbars)
        //                {
        //                    mainToolbar.Visible = true;
        //                    mainToolbar.BringToFront();
        //                }
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        messages.VBError(ex, Name, "tmrActiveForm_Tick", "");
        //    }
        //}

        private void MItemmenu_Click(object sender, EventArgs e)
        {
            OpenChildForm2();
            MItemmenu.Enabled = false;

        }

        private void invoiceGenerationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm5();
            mnuTBInvGenmenu.Enabled = false;

        }

        private void mnuMSGroup_Click(object sender, EventArgs e)
        {
            OpenChildForm6();
            mnuMSGroup.Enabled = false;
        }

        private void mnuMSSGroup_Click(object sender, EventArgs e)
        {
            OpenChildForm7();
            mnuMSSGroup.Enabled = false;
        }

        private void mnuMCust_Click(object sender, EventArgs e)
        {
            OpenChildForm8();
            mnuMCust.Enabled = false;
        }

        private void mnuMSupplier_Click(object sender, EventArgs e)
        {
            OpenChildForm9();
            mnuMSupplier.Enabled = false;
        }

        private void mnuMManuf_Click(object sender, EventArgs e)
        {
            OpenChildForm10();
            mnuMManuf.Enabled = false;
        }

        private void mnuWTile_Click(object sender, EventArgs e)
        {
            // Arrange MDI child forms horizontally
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void mnuWCascade_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }


        private void muWTIleV_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void saleReturnCancellationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildFormSR();
            mnuTBSRGenmenu.Enabled = false;
        }

        private void TinvoiceWiseSaleReportMenu_Click(object sender, EventArgs e)
        {
            OpenChildFormSale_Rpt_InvWiseSaleRpt();
            mnuSinvoiceWiseSaleReportMenu.Enabled = false;
        }

        private void frmU_Permissions_Click(object sender, EventArgs e)
        {
            OpenChildFormfrmU_Grantscreen_perm();
            UPermissions.Enabled = false;
        }


        public void FetchMenuPermissions()
        {
            try
            {
                DbConnector dbConnector = new DbConnector();
                dbConnector.connection = new OdbcConnection(dbConnector.connectionString);
                dbConnector.connection.Open();

                string query = @"SELECT a.Menu_name as menu FROM s_logopt a
                         WHERE a.Login_id = ? AND a.Can_add = 0 AND
                               a.Can_modify = 0 AND a.Can_delete = 0 AND
                               a.Can_inquire = 0 AND a.Can_post = 0 AND
                               a.Can_print = 0;";

                using (var cmd = new OdbcCommand(query, dbConnector.connection))
                {
                    cmd.Parameters.AddWithValue("@Login_id", DeTools.gstrloginId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string menuName = reader["menu"].ToString();

                            // Find the ToolStripMenuItem by its name recursively
                            ToolStripMenuItem mnuName = FindMenuItem(menuStrip1.Items, menuName);

                            // Now you can use mnuName
                            if (mnuName != null)
                            {
                                // Set the visibility based on your condition
                                mnuName.Visible = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ToolStripMenuItem FindMenuItem(ToolStripItemCollection items, string menuName)
            {
                foreach (ToolStripItem item in items)
                {
                    if (item is ToolStripMenuItem menuItem)
                    {
                        if (menuItem.Name == menuName)
                        {
                            return menuItem;
                        }

                        // Recursively search for the menu item in the drop-down items
                        ToolStripMenuItem foundItem = FindMenuItem(menuItem.DropDownItems, menuName);
                        if (foundItem != null)
                        {
                            return foundItem;
                        }
                    }
                }
                return null;
            }
        }


        public void SubMenu_Permission()
        {
            try
            {
                DbConnector dbConnector = new DbConnector();
                dbConnector.connection = new OdbcConnection(dbConnector.connectionString);
                dbConnector.connection.Open();

                string query1 = @"SELECT m.Parent_id as SubMenu
                          FROM s_menu m
                          JOIN s_logopt l ON m.Prog_id = l.Prog_id
                          WHERE l.Login_id = ?
                          GROUP BY m.Parent_id
                          HAVING SUM(l.Can_Add) = 0 
                             AND SUM(l.Can_Modify) = 0 
                             AND SUM(l.Can_Delete) = 0 
                             AND SUM(l.Can_Inquire) = 0 
                             AND SUM(l.Can_Post) = 0 
                             AND SUM(l.Can_Print) = 0;";

                using (var cmd1 = new OdbcCommand(query1, dbConnector.connection))
                {
                    cmd1.Parameters.AddWithValue("@Login_id", DeTools.gstrloginId);

                    using (var reader1 = cmd1.ExecuteReader())
                    {
                        while (reader1.Read())
                        {
                            string subMenu = reader1["SubMenu"].ToString();

                            // Find the ToolStripMenuItem by its name
                            ToolStripMenuItem mnuSubMenu = null;
                            foreach (ToolStripMenuItem item in menuStrip1.Items)
                            {
                                if (item.Name == subMenu)
                                {
                                    mnuSubMenu = item;
                                    break;
                                }
                            }

                            // Now you can use mnuSubMenu
                            if (mnuSubMenu != null)
                            {
                                // Set the visibility based on your condition
                                mnuSubMenu.Visible = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (log it, show message, etc.)
                MessageBox.Show(ex.Message);
            }
        }

        public void MainMenu_Permission()
        {
            try
            {
                DbConnector dbConnector = new DbConnector();
                dbConnector.connection = new OdbcConnection(dbConnector.connectionString);
                dbConnector.connection.Open();

                string mainMenuQuery = @"SELECT DISTINCT
                                       m1.Parent_id as SubMenu,
                                       CASE
                                           WHEN SUBSTRING(m1.Parent_id, 4, 1) = 'M' THEN 'mnuMaster'
                                           WHEN SUBSTRING(m1.Parent_id, 4, 1) = 'T' THEN 'mnuTransaction'
                                           WHEN SUBSTRING(m1.Parent_id, 4, 1) = 'R' THEN 'mnuReport'
                                           WHEN SUBSTRING(m1.Parent_id, 4, 1) = 'U' THEN 'mnuUtilities'
                                           ELSE 'Other'
                                       END AS mainMenu
                                FROM s_menu m1
                                WHERE m1.Parent_id IN (
                                    SELECT m2.Parent_id
                                    FROM s_menu m2
                                    JOIN s_logopt l ON m2.Prog_id = l.Prog_id
                                    WHERE l.Login_id = ?
                                    GROUP BY m2.Parent_id
                                    HAVING SUM(l.Can_Add) = 0 
                                       AND SUM(l.Can_Modify) = 0 
                                       AND SUM(l.Can_Delete) = 0 
                                       AND SUM(l.Can_Inquire) = 0 
                                       AND SUM(l.Can_Post) = 0 
                                       AND SUM(l.Can_Print) = 0);";

                using (var cmdMainMenu = new OdbcCommand(mainMenuQuery, dbConnector.connection))
                {
                    cmdMainMenu.Parameters.AddWithValue("@Login_id", DeTools.gstrloginId);

                    using (var reader1 = cmdMainMenu.ExecuteReader())
                    {
                        while (reader1.Read())
                        {
                            string mainMenu = reader1["mainMenu"].ToString();

                            // Find the ToolStripMenuItem by its name
                            ToolStripMenuItem mnuMainMenu = null;
                            foreach (ToolStripMenuItem item in menuStrip1.Items)
                            {
                                if (item.Name == mainMenu)
                                {
                                    mnuMainMenu = item;
                                    break;
                                }
                            }

                            // Now you can use mnuMainMenu
                            if (mnuMainMenu != null)
                            {
                                // Set the visibility based on your condition
                                mnuMainMenu.Visible = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception (log it, show message, etc.)
                MessageBox.Show(ex.Message);
            }
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {


            //if (e.KeyCode == Keys.A)
            //{
            //    this.KeyDown += new KeyEventHandler(DeTools.Form_KeyDown);

            //}
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.Control && e.KeyCode == Keys.M)
            //{
            //    this.KeyDown += new KeyEventHandler(DeTools.Form_KeyDown);

            //}
        }


        //-----newest old

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

        //        //if (ActiveMdiChild != null && !string.IsNullOrEmpty(ActiveMdiChild.Tag as string))
        //        if (ActiveMdiChild != null && ActiveMdiChild.Tag != null)
        //        {
        //            if (int.TryParse(ActiveMdiChild.Tag.ToString(), out int tagValue))
        //            {
        //                if (DeTools.toolbarDictionarywith_frmnm.ContainsKey(ActiveMdiChild.Text))
        //                {
        //                    ToolStrip currentToolbar = DeTools.toolbarDictionarywith_frmnm[ActiveMdiChild.Text];
        //                    currentToolbar.BringToFront();
        //                }
        //            }
        //        }
        //        else if (ActiveMdiChild == null)
        //        {
        //            // There is no active child form, so you can ensure the main form's tbrtools is displayed here.
        //            if (DeTools.toolbarDictionarywith_frmnm.ContainsKey(this.Text))
        //            {
        //                ToolStrip mainToolbar = DeTools.toolbarDictionarywith_frmnm[this.Text];
        //                mainToolbar.BringToFront();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        messages.VBError(ex, Name, "tmrActiveForm_Tick", "");
        //    }
        //}



    }///////////////////////////////////
}
