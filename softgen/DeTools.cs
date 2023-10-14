using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.DirectoryServices.ActiveDirectory;
using softgen.Properties;
using System.Resources;
using static System.Windows.Forms.Design.AxImporter;
using System.Runtime.InteropServices;// for CLass SystemInformation mainly ComputerName
using MySql.Data.MySqlClient;
using System.Security.Cryptography.X509Certificates;

namespace softgen


{
    
        public static class DeTools
    {
        // Define global constants
        public const string ADDMODE = "Add";
        public const string MODIFYMODE = "Modify";
        public const string DELETEMODE = "Delete";
        public const string INQUIREMODE = "Inquire";
        public const string POSTMODE = "Post";
        public const string QUITCAPTION = "    Quit    ";

        private static string gstrSQL;
        public static string gstrloginId;
        public static string gstrloginName;
        public static Form gobjActiveForm;
        public static string[] gstrsetup = new string[6]; // Initialized in a method
        public static int gintFormHeight;
        public static int gintFormWidth;
        //private static Database security;
        private static string securityFile; // Initialized in a method
        private static string rep;
        private static Control activeControl;
        private static ToolStripButton mobjbutton;
        private static ToolStrip toolbar;
        public static ToolStrip mobjToolbar;
        private static int[] tbrIndex;
        private static bool[] maintTBRIndex;
        private static int mintUBound = 0;
        private static string barCode;
        static MySqlConnection dbSecurity;
        public static string strBranch;
        public static string strBrand;
        public static string strCompany;
        public static string strAddress1;
        public static string strAddress2;
        public static string strAddress3;
        public static string strTin;
        public static string strPhone;
        public static string strLst;
        public static string strCst;
        public static string strNote1;
        public static string strNote2;
        public static string strNote3;
        public static string strNote4;
        private static bool priceEdit;
        private static bool saleTax;
        private static bool accounts;
        private static bool itemDisc;
        private static bool roundOff;
        private static int roundUpTo;
        private static bool totDisc;
        private static double totalDiscount;
        private static double totalDiscountCC;
        private static double totalDiscountCoup;
        private static double totalDiscountCredit;
        private static string rptInvoice;
        private static bool saleReturn;
        private static double refundAmount;
        public static Dictionary<Form, ToolStrip> toolbarDictionary = new Dictionary<Form, ToolStrip>();//store key value pair data From is key and Toolstrip will be its Value .For Destroying toolbar
        public static string ConnectionString;





        
        /// //////////////////////////////////////////////////////////////////
        

        public static void ActivateForm(Form form,bool TF,string mode) 
        {
            try
            {
                string strMask = "";

                foreach (Control control in form.Controls)
                {
                    string controlName = control.Name.Trim().ToLower();

                    switch (controlName.Substring(0, 3))
                    {
                        case "txt":
                        case "drv":
                        case "dir":
                        case "fil":
                            control.Enabled = TF;
                            break;
                        case "cbo":
                            control.Enabled = TF;
                            if (control is System.Windows.Forms.ComboBox comboBox && comboBox.DropDownStyle == ComboBoxStyle.DropDown)
                            {
                                comboBox.Text = "";
                            }
                            break;
                        case "msk":
                            control.Enabled = TF;
                            if (control is MaskedTextBox maskedTextBox)
                            {
                                strMask = maskedTextBox.Mask;
                                maskedTextBox.Mask = "";
                                maskedTextBox.Text = "";
                                maskedTextBox.Mask = strMask;
                            }
                            break;
                        case "dtp":
                            control.Enabled = TF;
                            if (control is DateTimePicker dateTimePicker)
                            {
                                dateTimePicker.Value = DateTime.Now; // Replace with your specific default date value
                            }
                            break;
                        case "chk":
                        case "cmd":
                        case "lst":
                        case "opt":
                        case "spn":
                            control.Enabled = TF;
                            break;
                        case "rot":
                            if (control is Label label)
                            {
                                label.Text = "";
                            }
                            break;
                        case "dbg":
                            switch (mode)
                            {
                                case ADDMODE:
                                case MODIFYMODE:
                                    control.Enabled = true;
                                    // Additional logic to allow delete and update based on mode
                                    break;
                                case DELETEMODE:
                                case INQUIREMODE:
                                case POSTMODE:
                                    control.Enabled = true;
                                    // Additional logic to disallow delete and update based on mode
                                    break;
                                default:
                                    // Handle the default case as needed
                                    control.Enabled = false;
                                    break;
                            }
                            break;
                    }
                }
            }
            catch(Exception ex)
                {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static string GetMode(Form form)
        {
            try {
                string caption = form.Text; // Use form.Text instead of form.Caption to get the form's caption

                int startIndex = caption.IndexOf("<");
                int endIndex = caption.IndexOf(">", startIndex);

                if (startIndex >= 0 && endIndex > startIndex)
                {
                    string mode = caption.Substring(startIndex + 1, endIndex - startIndex - 1);
                    return mode.Trim();
                }

                return string.Empty; // Return an empty string if the mode is not found
            }
            catch(System.Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return string.Empty;

            }

        }

        public static void ClearTextNComboControls(Form form)
        {
            form.BackColor = System.Drawing.Color.FromArgb(15, 0, 0, 128); // Use the appropriate color format

            foreach (Control control in form.Controls)
            {
                string controlName = control.Name.Trim().ToLower();

                switch (controlName.Substring(0, 3))
                {
                    case "txt":
                        if (control is System.Windows.Forms.TextBox textBox)
                        {
                            textBox.Text = string.Empty;
                        }
                        break;
                    case "cbo":
                        if (control is System.Windows.Forms.ComboBox comboBox)
                        {
                            if (comboBox.DropDownStyle == ComboBoxStyle.Simple)
                            {
                                comboBox.Text = string.Empty;
                            }
                            else if (comboBox.DropDownStyle == ComboBoxStyle.DropDownList)
                            {
                                comboBox.SelectedIndex = -1;
                            }
                        }
                        break;
                    case "rot":
                        if (control is Label label)
                        {
                            label.Text = string.Empty;
                            label.BackColor = System.Drawing.Color.FromArgb(15, 0, 0, 128);// Set the appropriate color
                        }
                        break;
                    case "msk":
                        if (control is MaskedTextBox maskedTextBox)
                        {
                            string strMask = maskedTextBox.Mask;
                            maskedTextBox.Mask = string.Empty;
                            maskedTextBox.Text = string.Empty;
                            maskedTextBox.Mask = strMask;
                        }
                        break;
                    case "dtp":
                        if (control is DateTimePicker dateTimePicker)
                        {
                            dateTimePicker.Value = DateTime.Now; // Set the default date value
                            dateTimePicker.Format = DateTimePickerFormat.Custom;
                            dateTimePicker.CustomFormat = gstrsetup[3];
                            if (dateTimePicker.Checked)
                            {
                                dateTimePicker.Checked = false;
                            }
                        }
                        break;
                    case "lbl":
                        if (control is Label lbl)
                        {
                            lbl.BackColor = System.Drawing.Color.FromArgb(15, 0, 0, 128); // Use the appropriate color format
                        }
                        break;
                }
            }
        }

        public static int DateChars(int keyAscii)
        {
            // Only numbers, slash, and backspace keys are allowed.
            if ((keyAscii < 47 || keyAscii > 57) && keyAscii != 8)
            {
                return 0;
            }
            else
            {
                return keyAscii;
            }


        }


      

        public static void ExecuteKeyDown(Form form, Keys KeyCode)
        {
            try
            {
                // Access the ToolStrip from MainForm.Instance
                ToolStrip toolStrip = MainForm.Instance.tbrTools1; // Replace with your actual ToolStrip name

                ToolStripItem toolStripItem = null;
          
                string strOptions = GetStrOptions(form); // Assign a value before the switch

                foreach (ToolStripItem item in toolStrip.Items)
                {
                    if (item is ToolStripButton button)
                    {
                        if (item.Tag != null && item.Tag?.ToString() == form.Tag?.ToString())
                        {
                            toolStripItem = item;
                            break;

                        }
                    }

                }
                if (toolStripItem != null && toolStripItem.Tag != null)
                {
                    // Retrieve the Tag property of the ToolStripItem and assign it to strOptions   
                    strOptions = toolStripItem.Tag.ToString();
                }


                switch (KeyCode)
                {
                    case Keys.F2:
                        if (GetMode(form) != null)
                        {
                            //form.ClearForm();
                            //ClearCreatedByPanel();

                        }
                        break;
                    case Keys.F3:
                        if (GetMode(form) == null)
                        {
                            if (HasOption(strOptions, 'A'))
                            {
                                ButtonClick((int)form.Tag, "ADDMODE");
                            }
                        }
                        break;

                    case Keys.F4:
                        if (GetMode(form) == null)
                        {
                            if (HasOption(strOptions, 'M'))
                            {
                                ButtonClick((int)form.Tag, "MODIFYMODE");
                            }
                        }
                        break;

                    case Keys.F5:
                        switch (GetMode(form))
                        {
                            case null:
                                if (HasOption(strOptions, 'D'))
                                {
                                    ButtonClick((int)form.Tag, "DELETEMODE");
                                }
                                break;

                            case "DELETEMODE":
                                //form.DeleteForm();
                                break;
                        }
                        break;

                    case Keys.F6:
                        if (GetMode(form) == null)
                        {
                            if (HasOption(strOptions, 'I'))
                            {
                                ButtonClick((int)form.Tag, "INQUIREMODE");
                            }
                        }
                        break;

                    case Keys.F7:
                        switch (GetMode(form))
                        {
                            case null:
                                if (HasOption(strOptions, 'P'))
                                {
                                    ButtonClick((int)form.Tag, "POSTMODE");
                                }
                                break;

                            case "POSTMODE":
                   //             form.PostForm();
                                break;
                        }
                        break;

                    case Keys.F8:
                        if (GetMode(form) != null)
                        {
                            if (HasOption(strOptions, 'R'))
                            {
                     //           form.PrintDoc();
                            }
                        }
                        break;

                    case Keys.F10:
                        switch (GetMode(form))
                        {
                            case "ADDMODE":
                            case "MODIFYMODE":
                     //           form.SaveForm();
                                break;
                        }
                        break;

                    case Keys.Escape:
                        switch (GetMode(form))
                        {
                            case null:
                                ButtonClick((int)form.Tag, "Quit");
                                break;

                            default:
                                ButtonClick((int)form.Tag, "ModeQuit");
                                break;
                        }
                        break;

                }



            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

//////////Function for ExecuteKeyDown function for Getting the strOption !st Value from Tag////////
        public static bool HasOption(string strOptions, char option)
        {
            return strOptions.Contains(option.ToString());
        }
        private static string GetStrOptions(Form form)
        {
            // In this example, we're returning a hardcoded string for demonstration.
            // In your actual application, you should replace this with logic to
            // determine the options for the given form.
            return "AMR"; // This is just an example.
        }


        public static int CheckKeyPress(int keyAscii)
        {
            // Disable single quote (') key and space key for key fields.
            if (keyAscii == 39 || keyAscii == 32)
            {
                return 0;
            }
            else
            {
                return (int)Char.ToUpper((char)keyAscii);
            }
        }

        public static void ButtonClick(int TBRIndex, string BtnKey)
        {
            try
            {
                string strOptions, strAppMode="";
                mobjToolbar = MainForm.Instance.tbrTools;
              
                MenuStrip menustrip = MainForm.Instance.menuStrip1;
               
                strOptions = mobjToolbar.Tag?.ToString()?? "AMDIPR";

                // Access the ToolStripItem at the specified TBRIndex
                ToolStripItem toolStripItem = mobjToolbar.Items[TBRIndex];

                switch (BtnKey)
                {
                    case ADDMODE:

                        strAppMode = ADDMODE;
                        ActivateForm(gobjActiveForm, true, ADDMODE);
                        //gobjActiveForm.SetSearchVar(true);
                        MainForm.Instance.mnuAdd.Checked = true;      

                        break;

                    case MODIFYMODE:
                        strAppMode = MODIFYMODE;
                        ActivateForm(gobjActiveForm, true, MODIFYMODE);
                        //gobjActiveForm.SetSearchVar(false);
                        MainForm.Instance.mnuModify.Checked = true;
                        break;

                    case DELETEMODE:
                        strAppMode = DELETEMODE;
                        ActivateForm(gobjActiveForm, false, DELETEMODE);
                        //gobjActiveForm.SetSearchVar(false);
                        MainForm.Instance.mnuDeleteMode.Checked = true;
                        break;

                    case INQUIREMODE:
                        strAppMode = INQUIREMODE;
                        ActivateForm(gobjActiveForm, false, INQUIREMODE);
                        //gobjActiveForm.SetSearchVar(false);
                        MainForm.Instance.mnuInquire.Checked = true;
                        break;

                    case POSTMODE:
                        strAppMode = POSTMODE;
                        ActivateForm(gobjActiveForm, false, POSTMODE);
                        //gobjActiveForm.SetSearchVar(false);
                        MainForm.Instance.mnuPost.Checked = true;
                        break;
                }

                switch (BtnKey)
                {
                    case ADDMODE:
                    case MODIFYMODE:
                    case DELETEMODE:
                    case INQUIREMODE:
                    case POSTMODE:

                        ToolStripSeparator separator = new ToolStripSeparator();
                        mobjToolbar.Items.Add(separator);
                        SetModeCaption(gobjActiveForm, strAppMode);
                        RemoveButtons(mobjToolbar);
                        DisableFileMenu();
                        MainForm.Instance.mnuHelp.Enabled = true;

                        break;

                }

                switch (BtnKey)
                {
                    case ADDMODE:
                    case MODIFYMODE:
                    MainForm.Instance.btnSave.Visible = true;
                    mobjbutton= new ToolStripButton("Save", MainForm.Instance.imageList1.Images[8],null,"Save");
                        
                        mobjToolbar.Items.Add(mobjbutton); //added to  the mobjToolbar

                        mobjbutton.AutoSize = false;
                        mobjbutton.BackColor = Color.Lavender;
                        mobjbutton.Font = new Font("Times New Roman", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
                        mobjbutton.ImageAlign = ContentAlignment.TopCenter;
                        mobjbutton.ImageScaling = ToolStripItemImageScaling.None;
                        mobjbutton.Margin = new Padding(3);
                        mobjbutton.Size = new Size(51, 47);
                        mobjbutton.TextAlign = ContentAlignment.BottomCenter;
                        mobjbutton.TextImageRelation = TextImageRelation.Overlay;
                        
                        MainForm.Instance.mnuSave.Enabled = true;

                        break;
                }

                if (BtnKey==DELETEMODE)
                {
                    MainForm.Instance.btnDelete.Visible = true;
                    mobjbutton = new ToolStripButton("Delete", MainForm.Instance.imageList1.Images[13], null, "Delete");
                    mobjbutton.ToolTipText = "Deleting The Current Information";
                    mobjbutton.AutoSize = false;
                    mobjbutton.BackColor = Color.Lavender;
                    mobjbutton.BackgroundImageLayout = ImageLayout.Center;
                    mobjbutton.Font = new Font("Times New Roman", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
                    mobjbutton.ImageAlign = ContentAlignment.TopCenter;
                    mobjbutton.ImageScaling = ToolStripItemImageScaling.None;
                    mobjbutton.Margin = new Padding(3);
                    mobjbutton.Size = new Size(51, 47);
                    mobjbutton.TextAlign = ContentAlignment.BottomCenter;
                    mobjbutton.TextImageRelation = TextImageRelation.Overlay;
                   
                    MainForm.Instance.mnuDeleteRecord.Enabled = true;

                    mobjToolbar.Items.Add(mobjbutton); //added to  the mobjToolbar
                }
                if (BtnKey == POSTMODE)
                {
                   CreateButton(mobjToolbar, "Authorise", "Authorise the Current Information");
                    MainForm.Instance.mnuAuthorise.Enabled = true;
                }

                switch (BtnKey)
                {
                    case ADDMODE:
                    case MODIFYMODE:
                    case DELETEMODE:
                    case INQUIREMODE:
                    case POSTMODE:
                       CreateButton(mobjToolbar, "Fresh", strAppMode + " Fresh Information");
                        MainForm.Instance.mnuRefresh.Enabled = true;
                        if (strAppMode == POSTMODE)
                        {
                            mobjToolbar.Items[mobjToolbar.Items.Count - 1].ToolTipText = "Authorise Fresh Information";
                            mobjbutton.ToolTipText = "Authorise Fresh Information";
                        }

                        if (BtnKey != ADDMODE)
                        {
                            CreateButton(mobjToolbar, "Retrieve", "Find Information for Specified Criteria");
                            MainForm.Instance.mnuRetrieve.Enabled = true;
                        }

                        if (strOptions.EndsWith("R"))
                        {
                          CreateButton(mobjToolbar, "Print", "Print the Current Information");
                            MainForm.Instance.mnuPrint.Enabled = true;
                        }

                        mobjbutton = new ToolStripButton(QUITCAPTION, MainForm.Instance.imageList1.Images[7],null, "ModeQuit");
                        mobjbutton.ToolTipText = "Quit From " + strAppMode + " Mode";

                        mobjbutton.AutoSize = false;
                        mobjbutton.BackColor = Color.Lavender;
                        mobjbutton.BackgroundImageLayout = ImageLayout.Center;
                        mobjbutton.Font = new Font("Times New Roman", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
                        mobjbutton.ImageAlign = ContentAlignment.TopCenter;
                        mobjbutton.ImageScaling = ToolStripItemImageScaling.None;
                        mobjbutton.Margin = new Padding(3);
                        mobjbutton.Size = new Size(51, 47);
                        mobjbutton.TextAlign = ContentAlignment.BottomCenter;
                        mobjbutton.TextImageRelation = TextImageRelation.Overlay;

                        mobjToolbar.Items.Add(mobjbutton);
                        
                        CreateButton(mobjToolbar, "Help", "Help Information");
                        //SendKeys "{F2}"
                        //gobjActiveForm.ClearForm();
                        break;
                        switch (BtnKey)
                        {
                            case "Save":
                               // gobjActiveForm.SaveForm();
                                break;

                            case "DeleteForm":
                                //gobjActiveForm.DeleteForm();
                                break;

                            case "Fresh":
                                //gstrMsg = "Do you want to refresh without saving the changes?";
                                //    if (gobjActiveForm.GetDEStatus() == true)
                                //    {
                                //        if (MessageBox.Show(gstrMsg, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                //        {
                                //            return;
                                //        }
                                //    }
                                //    if (GetMode(gobjActiveForm) == ADDMODE)
                                //    {
                                //        gobjActiveForm.SetSearchVar(true);
                                //    }
                                //    else
                                //    {
                                //        gobjActiveForm.SetSearchVar(false);
                                //    }
                                //    if (gobjActiveForm.Name == "frmT_Invoice" && GetMode(gobjActiveForm) == ADDMODE)
                                //    {
                                //        gobjActiveForm.SaveTempDataForm();
                                //    }
                                //    gobjActiveForm.ClearForm();
                                //    ClearCreatedByPanel();
                                  ClearStatusBarHelp();
                                    break;

                                case "Retrieve":
                                //    gobjActiveForm.SearchForm();
                                    break;

                                case "Print":
                                //    gobjActiveForm.PrintDoc();
                                    break;

                                case "Authorise":
                                //    gobjActiveForm.PostForm();
                                    break;

                                case "Continue":
                                //    gobjActiveForm.PrintReport();
                                    break;

                                case "Help":
                                //    CallHelp();
                                    break;

                                case "ModeQuit":
                                //    gstrMsg = "Do you want to exit without saving the changes?";
                                //    if (gobjActiveForm.GetDEStatus() == true)
                                //    {
                                //        if (MessageBox.Show(gstrMsg, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                //        {
                                //            return;
                                //        }
                                //    }
                                //    if (gobjActiveForm.Name == "frmT_Invoice" && GetMode(gobjActiveForm) == ADDMODE)
                                //    {
                                //        gobjActiveForm.SaveTempDataForm();
                                //    }

                                //    gobjActiveForm.Caption = RestoreCaption(gobjActiveForm);
                                DestroyToolbar(gobjActiveForm);
                                CreateToolbar(gobjActiveForm, strOptions);
                              ActivateForm(gobjActiveForm, false, null);
                                //    gobjActiveForm.Icon = mdiMain.Icon;
                                ClearStatusBarHelp();
                                break;

                            case "Quit":
                            //    gobjActiveForm.Close();
                                DisableFileMenu();
                                break;

                            case "SystemQuit":
                              //if (AskToExit() == DialogResult.Yes)
                            //    {
                            //        frmHelp.Close();
                            //        mdiMain.Close();
                            //    }
                               break;
                        }
                }
            }

            catch (System.Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

        private static void CreateButton( ToolStrip ToolBar,string BtnKey,string ToolTip)
        {
            try
            {
                string imageNameToFind = BtnKey.Trim(); // The name of the image you want to find
                Image foundImage = null;

                if (BtnKey == "Quit")
                {
                    mobjbutton = new ToolStripButton(BtnKey.Trim(), MainForm.Instance.imageList1.Images[7],null,QUITCAPTION.Trim());

                    mobjbutton.AutoSize = false;
                    mobjbutton.BackColor = Color.Lavender;
                    mobjbutton.BackgroundImageLayout = ImageLayout.Center;
                    mobjbutton.Font = new Font("Times New Roman", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
                    mobjbutton.ImageAlign = ContentAlignment.TopCenter;
                    mobjbutton.ImageScaling = ToolStripItemImageScaling.None;
                    mobjbutton.Margin = new Padding(3);
                    mobjbutton.Size = new Size(51, 47);
                    mobjbutton.TextAlign = ContentAlignment.BottomCenter;
                    mobjbutton.TextImageRelation = TextImageRelation.Overlay;
                    mobjbutton.ToolTipText = ToolTip;

                    ToolBar.Items.Add(mobjbutton);
                }

                ImageList imageList = MainForm.Instance.imageList1; // Your ImageList
                                                                    // Iterate through the images in the ImageList
                for (int i = 0; i < imageList.Images.Count; i++)
                {
                    // Compare the image name at index i with the known image name
                    if (imageList.Images.Keys[i] == imageNameToFind)
                    {
                        // The image name at index i matches the known image name
                        foundImage = imageList.Images[i]; // Capture the matched image
                        break; // Exit the loop since you found the image
                    }
                }
                if (foundImage!= null)
                {
                    mobjbutton = new ToolStripButton(BtnKey.Trim(), foundImage,null,BtnKey.Trim());

                    mobjbutton.AutoSize = false;
                    mobjbutton.BackColor = Color.Lavender;
                    mobjbutton.BackgroundImageLayout = ImageLayout.Center;
                    mobjbutton.Font = new Font("Times New Roman", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
                    mobjbutton.ImageAlign = ContentAlignment.TopCenter;
                    mobjbutton.ImageScaling = ToolStripItemImageScaling.None;
                    mobjbutton.Margin = new Padding(3);
                    mobjbutton.Size = new Size(51, 47);
                    mobjbutton.TextAlign = ContentAlignment.BottomCenter;
                    mobjbutton.TextImageRelation = TextImageRelation.Overlay;
                    mobjbutton.ToolTipText = ToolTip;

                    ToolBar.Items.Add(mobjbutton);
                }
                   else
                {
                    MessageBox.Show("FoundImage is Null ","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);                      
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void CreateToolbar(Form form, string Options)
        {
            try
            {
              
                int I = GetFreeIndex(); // Obtain a free index for the toolbar
                                        //mobjToolbar = new ToolStrip();
                mobjToolbar = MainForm.Instance.tbrTools; // Use the existing tbrTools from MainForm
                mobjToolbar.AutoSize = false;
                mobjToolbar.BackColor = Color.CadetBlue;
                mobjToolbar.Dock = DockStyle.None;
                mobjToolbar.GripStyle = ToolStripGripStyle.Hidden;
                mobjToolbar.Location = new Point(1, 629);
                //mobjToolbar.Name = "tbrTools";
                mobjToolbar.Size = new Size(417, 52);
                mobjToolbar.TabIndex = 7;

                mobjToolbar.ImageList = MainForm.Instance.imageList1;
                string formName = form.Name.Trim();
                string strFormType = formName.Length > 3 ? formName.Substring(3, 1).ToUpper():string.Empty;

                if (!form.Controls.Contains(mobjToolbar))
                {
                    

                    form.Controls.Add(mobjToolbar);
                    form.Tag = I;
                }

                if (strFormType =="C") //CheckList Form
                {
                    CreateButton(mobjToolbar, "Continue", "Print CheckList!");
                }
                else if (strFormType== "R") //Report Form
                {
                    CreateButton(mobjToolbar, "Continue", "Print Report!");
                }

                else
                {
                    DisableFileMenu();
                    for (I = 1; I <= Options.Length; I++)
                    {
                        
                        switch (Options[I-1].ToString().ToUpper())
                        {
                            case "A":
                                MainForm.Instance.mnuAdd.Enabled = true;
                                CreateButton(mobjToolbar, ADDMODE, "Add New Information");
                                break;
                            case "M":
                                MainForm.Instance.mnuModify.Enabled = true;
                                CreateButton(mobjToolbar, MODIFYMODE, "Modify Existing Information");
                                break;
                            case "D":
                                MainForm.Instance.mnuDeleteMode.Enabled = true;
                                CreateButton(mobjToolbar, DELETEMODE, "Delete Existing Information");
                                break;
                            case "I":
                                MainForm.Instance.mnuInquire.Enabled = true;
                                CreateButton(mobjToolbar, INQUIREMODE, "Inquire Existing Information");
                                break;
                            case "P":
                                MainForm.Instance.mnuPost.Enabled = true;
                                CreateButton(mobjToolbar, POSTMODE, "Post Existing Information");
                                break;


                        }
                    }


                }

                if (form.Name == "MainForm" )
                {
                    mobjbutton = new ToolStripButton(QUITCAPTION, MainForm.Instance.imageList1.Images[7], null, "SystemQuit");

                    mobjbutton.AutoSize = false;
                    mobjbutton.BackColor = Color.Lavender;
                    mobjbutton.BackgroundImageLayout = ImageLayout.Center;
                    mobjbutton.Font = new Font("Times New Roman", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
                    mobjbutton.ImageAlign = ContentAlignment.TopCenter;
                    mobjbutton.ImageScaling = ToolStripItemImageScaling.None;
                    mobjbutton.Margin = new Padding(3);
                    mobjbutton.Size = new Size(51, 47);
                    mobjbutton.TextAlign = ContentAlignment.BottomCenter;
                    mobjbutton.TextImageRelation = TextImageRelation.Overlay;
                    mobjbutton.ToolTipText = "Quit from the System";

                    mobjToolbar.Items.Add(mobjbutton);

                }
                else
                {
                    if (strFormType== "C") // cheklist
                    {
                        CreateButton(mobjToolbar, "Quit", "Quit from Checklist");
                        
                    }
                    else if (strFormType== "R")
                    {
                        CreateButton(mobjToolbar,"Quit","Quit from Report");

                    }
                    else {
                        CreateButton(mobjToolbar,"Quit","Quit from Entry Screen");
                    }

                }

                CreateButton(mobjToolbar,"Help","Help Information!");

                form.Controls.Add(mobjToolbar);
                form.Tag = I;
                // Attach the created toolbar's reference to the form in the dictionary
                DeTools.toolbarDictionary[form] = mobjToolbar;

                mobjToolbar.Tag = Options;

                mobjToolbar.Visible = true;


            }
            catch(Exception ex)
            {
                Messages.ErrorMsg(ex.ToString());
            }   
        }

        private static int GetFreeIndex()
        {
            for (int i = 1; i <= mintUBound; i++)
            {
                if (!maintTBRIndex[i])
                {
                    maintTBRIndex[i] = true; // Mark this index as in use
                    return i;
                }
            }

            mintUBound++;
            Array.Resize(ref maintTBRIndex, mintUBound + 1);
            maintTBRIndex[mintUBound] = true; // Mark the newly created index as in use
            return mintUBound;
        }


        public static void DestroyToolbar(Form form)
        {
            if (form.Tag != null)
            {
                //form.ClearForm();    todo
                DisableFileMenu();
                UncheckFileMenu();
                ClearStatusBarHelp();
                int toolbarIndex = (int)form.Tag; // Assuming you stored the toolbar index in the Tag property
                if (toolbarDictionary.ContainsKey(form))
                {
                    ToolStrip toolStrip= toolbarDictionary[form];//this have value a toolstrip associated with the specific form where it will be called. and stored in toolstrip.
                    toolStrip.Dispose();
                    toolbarDictionary.Remove(form); //clear the dictionary

                }
                    maintTBRIndex[(int)form.Tag] = true;
                form.Tag = null;




            }

        }

        public static void ClearStatusBarHelp()
        {
            MainForm.Instance.pnlHelp.Text = "";
        }

        public static string Encrypt(string strPwd)
        {
            int intLength = strPwd.Length;
            string encrypted = string.Empty;

            for (int i = 0; i < intLength; i++)
            {
                encrypted += (char)(strPwd[i] + intLength);
            }

            return encrypted;
        }

        public static string Decrypt(string strEncPwd)
        {
            int intLength = strEncPwd.Length;
            string decrypted = "";

            for (int I = 0; I < intLength; I++)
            {
                decrypted += (char)(strEncPwd[I] - intLength);
            }

            return decrypted;
        }

        private static void DisableFileMenu()
        {
            MainForm.Instance.mnuHelp.Enabled = false;
            MainForm.Instance.mnuModify.Enabled = false;
            MainForm.Instance.mnuDeleteMode.Enabled = false;
            MainForm.Instance.mnuInquire.Enabled= false;
            MainForm.Instance.mnuPost.Enabled  = false;
            
            MainForm.Instance.mnuRetrieve.Enabled   = false;
            MainForm.Instance.mnuRefresh.Enabled = false;

            MainForm.Instance.mnuSave.Enabled = false;
            MainForm.Instance.mnuAuthorise.Enabled = false;
            MainForm.Instance.mnuPrint.Enabled = false;
            MainForm.Instance.mnuDeleteRecord.Enabled = false;

        }

        public static class fOSMachineName  //fOSMachineName.GetMachineName() have to use like this. 
        {
            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            public static extern int GetComputerName(StringBuilder lpBuffer, ref int lpnSize);

            public static string GetMachineName()
            {
                int maxLength = 256; // Adjust this value as needed
                StringBuilder buffer = new StringBuilder(maxLength);
                int size = maxLength;
                if (GetComputerName(buffer, ref size) != 0)
                {
                    return buffer.ToString();
                }
                return string.Empty;
            }
        }

        public static string GetOptions(string FormName)
        {

            ConnectionString = "DSN=softgen_db_my;Uid=root;";
            gstrSQL = "SELECT * FROM s_logopt WHERE login_id = @login_id AND prog_id = @formName";

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                using (MySqlCommand cmd = new MySqlCommand(gstrSQL, connection))
                {
                    cmd.Parameters.AddWithValue("@login_id", gstrloginId); // Assuming gstrLogin_id is a global variable
                    cmd.Parameters.AddWithValue("@formName", FormName);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string options = string.Empty;

                            if (Convert.ToInt32(reader["can_add"]) == 1) options += "A";
                            if (Convert.ToInt32(reader["can_modify"]) == 1) options += "M";
                            if (Convert.ToInt32(reader["can_delete"]) == 1) options += "D";
                            if (Convert.ToInt32(reader["can_inquire"]) == 1) options += "I";
                            if (Convert.ToInt32(reader["can_post"]) == 1) options += "P";
                            if (Convert.ToInt32(reader["can_print"]) == 1) options += "R";

                            return options;
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                }
            }
        }

        private static void RemoveButtons(ToolStrip toolStrip)
        {
            int totalButtons = toolStrip.Items.Count;
            for (int Counter=totalButtons-1;Counter>= 0; Counter--)
            {
                toolStrip.Items.RemoveAt(Counter);
            } 

        }

        public static string RestoreCaption(Form form)
        {
            string caption = form.Text; // Assuming the form caption is in the Text property

            for (int i = 0; i < caption.Length; i++)
            {
                if (caption[i] == '<')
                {
                    return caption.Substring(0, i).Trim();
                }
            }

            return caption.Trim(); // Return the original caption if no '<' character is found
        }

        private static void Retrieve(Form form, int keyCode)
        {
            // Call the GetMode function to retrieve the current mode
            var currentMode = GetMode(form);

            // Assuming ADDMODE is a constant or enum representing the ADD mode

            if (keyCode == (int)Keys.Return && currentMode != ADDMODE)
            {
                //form.SearchForm();
            }
        }


        public static void SelectText(Control objcontrol)
        {
            if (objcontrol is TextBoxBase textBoxBase)
            {
                textBoxBase.SelectionStart = 0;
                textBoxBase.SelectionLength= textBoxBase.Text.Length;
            }
        }

        public static void SetModeCaption(Form form, string mode)
        {
            string iconPath = "D:\\C# project\\softgen\\file-add.ico";
            Icon AddmyIcon = new Icon(iconPath);
            string iconPath1 = "D:\\C# project\\softgen\\file-edit.ico";
            Icon ModifymyIcon = new Icon(iconPath1);
            string iconPath2 = "D:\\C# project\\softgen\\document_delete.ico";
            Icon DeletemyIcon = new Icon(iconPath2);
            string iconPath3 = "D:\\C# project\\softgen\\view.ico";
            Icon InquiremyIcon = new Icon(iconPath3);
            string iconPath4 = "D:\\C# project\\softgen\\file_locked.ico";
            Icon PostmyIcon = new Icon(iconPath4);


            form.Text = form.Text.Trim() + "<" + mode + ">";

            switch (mode)
            {
                case "Add":
                    form.Icon = AddmyIcon;
                    break;
                case "Modify":
                    form.Icon = ModifymyIcon;
                    break;
                case "Delete":
                    form.Icon = DeletemyIcon;
                    break;
                case "Inquire":
                    form.Icon = InquiremyIcon;
                    break;
                case "Post":
                    form.Icon = PostmyIcon;
                    break;
            }


        }
        public static void ActiveFileMenu(Form form)
        {
            string strMode = GetMode(form);
            ToolStrip toolStrip = toolbarDictionary[form]; // Retrieve the ToolStrip for this form
            string strOptions = (string)toolStrip.Tag;

            if (string.IsNullOrEmpty(strOptions))
            {
                strOptions = "AMDIPR";
            }

            DisableFileMenu();
            UncheckFileMenu();

            if (string.IsNullOrEmpty(strMode))
            {
                MainForm.Instance.mnuHelp.Enabled = false;

                for (int i = 0; i < strOptions.Length; i++)
                {
                    char option = char.ToUpper(strOptions[i]);
                    switch (option)
                    {
                        case 'A':
                            MainForm.Instance.mnuAdd.Enabled = true;
                            break;
                        case 'M':
                            MainForm.Instance.mnuModify.Enabled = true;
                            break;
                        case 'D':
                            MainForm.Instance.mnuDeleteMode.Enabled = true;
                            break;
                        case 'I':
                            MainForm.Instance.mnuInquire.Enabled = true;
                            break;
                        case 'P':
                            MainForm.Instance.mnuPost.Enabled = true;
                            break;
                    }
                }
            }
            else
            {
                MainForm.Instance.mnuHelp.Enabled = true;
                MainForm.Instance.mnuRefresh.Enabled = true;

                if (strMode != ADDMODE)
                {
                    MainForm.Instance.mnuRetrieve.Enabled = true;
                }

                if (strOptions.EndsWith("R"))
                {
                    MainForm.Instance.mnuPrint.Enabled = true;
                }

                switch (strMode)
                {
                    case ADDMODE:
                        MainForm.Instance.mnuAdd.Checked = true;
                        MainForm.Instance.mnuSave.Enabled = true;
                        break;
                    case MODIFYMODE:
                        MainForm.Instance.mnuModify.Checked = true;
                        MainForm.Instance.mnuSave.Enabled = true;
                        break;
                    case DELETEMODE:
                        MainForm.Instance.mnuDeleteMode.Checked = true;
                        MainForm.Instance.mnuDeleteRecord.Enabled = true;
                        break;
                    case INQUIREMODE:
                        MainForm.Instance.mnuInquire.Checked = true;
                        break;
                    case POSTMODE:
                        MainForm.Instance.mnuPost.Checked = true;
                        MainForm.Instance.mnuAuthorise.Enabled = true;
                        break;
                }
            }
        }


        private static void UncheckFileMenu()
        {
            MainForm.Instance.mnuAdd.Checked = false;
            MainForm.Instance.mnuModify.Checked = false;
            MainForm.Instance.mnuDeleteMode.Checked = false;
            MainForm.Instance.mnuInquire.Checked = false;
            MainForm.Instance.mnuPost.Checked = false;
        
        }
        public static void DisplayForm(Form form, int intFormHeight = 0, int intFormWidth = 0)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                gobjActiveForm = form;
                //form.Icon = MainForm.Icon;
                CreateToolbar(form, GetOptions(form.Name));

                // Determine the form type (Reports or Checklists) based on the form name.
                string strFormType = form.Name.Trim().Substring(3, 1).ToUpper();
                if (strFormType != "R" && strFormType != "C")
                {
                    ActivateForm(form, false, null);
                }

                form.Height = (intFormHeight > 0) ? intFormHeight : gintFormHeight;
                form.Width = (intFormWidth > 0) ? intFormWidth : gintFormWidth;

                General general = new General();
                    general.CenterForm(form);
                form.Show();
            }
            catch (Exception ex)
            {
                // Handle the exception or display an error message
                MessageBox.Show("Error: " + ex.Message, "DisplayForm Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }


        //for MAINFORM
        public static void BringToolStripToFront(string formOptions)
        {
            // Split the formOptions into individual option codes
            string[] options = formOptions.Split(',');

            foreach (ToolStrip toolbar in toolbarDictionary.Values)
            {
                // Hide all toolstrips first
                toolbar.Visible = false;
            }

            foreach (string option in options)
            {
                foreach (var kvp in toolbarDictionary)
                {
                    Form form = kvp.Key;
                    ToolStrip toolbar = kvp.Value;
                    string formName = form.Name.Trim();
                    string strFormType = formName.Length > 3 ? formName.Substring(3, 1).ToUpper() : string.Empty;

                    if (strFormType == "C" && option == "C") // CheckList Form
                    {
                        toolbar.Visible = true;
                    }
                    else if (strFormType == "R" && option == "R") // Report Form
                    {
                        toolbar.Visible = true;
                    }
                    else if (options.Contains(option)) // Check if the form's options list contains the current option
                    {
                        toolbar.Visible = true;
                    }
                }
            }
        }

        public static void CreatedBy(string created_by, string Created_date)
        {
            MainForm.Instance.pnlCreated_by.Text = created_by;
            MainForm.Instance.pnlCreated_date.Text=Created_date;

        }
        public static void PostedBy(string Posted_by,string Posted_date)
        {
            MainForm.Instance.pnlPosted_by.Text =Posted_by;
            MainForm.Instance.pnlPosted_date.Text =Posted_date;

        }


    }//End For Static Class 'DETOOLS'



}
