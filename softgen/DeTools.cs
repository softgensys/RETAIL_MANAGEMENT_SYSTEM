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
using System.Data.Odbc;
using System.Data;

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

        public static string gstrSQL;
        //public static string gstrMsg;
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
        public static Control mobjActiveControl;
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
        public static Dictionary<string, ToolStrip> toolbarDictionarywith_frmnm = new Dictionary<string, ToolStrip>();
        //public static Dictionary<string, ToolStrip> newToolbarDictionary = new Dictionary<string, ToolStrip>();



        // Define a new data structure to manage multiple toolbars per form
        public static Dictionary<string, List<ToolStrip>> toolbarDictionary1 = new Dictionary<string, List<ToolStrip>>();
        public static Dictionary<string, List<ToolStrip>> newToolbarDictionary = new Dictionary<string, List<ToolStrip>>();
        public static ImageList imageList= new ImageList();

        public static string ConnectionString;
        public static int startIndex=-1;
        public static frmM_Group frmM_Group= new frmM_Group();
        ///////////-------------
        public static string currentKey;
        public static string newKey;
        public static DataGridViewCell dgvCell;
        public static DataGridView dgv;


        ///////
        /// //////////////////////////////////////////////////////////////////

        
        public static void ActivateForm(Form form,bool TF,string mode) 
        {
            try
            {
                string strMask = "";

                foreach (Control control in form.Controls)
                {
                    string controlName = control.Name.Trim().ToLower();
                    bool endswithId= controlName.EndsWith("Id",StringComparison.OrdinalIgnoreCase);

                    switch (controlName.Substring(0, 3))
                    {
                        case "txt":
                        case "drv":
                        case "dir":
                        case "fil":
                            control.Enabled = TF;
                            if (endswithId)
                            {
                            control.Focus();
                            }
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
                
                startIndex = caption.IndexOf("<");

                if (startIndex >= 0)
                {

                    int endIndex = caption.IndexOf(">", startIndex);

                    if (startIndex >= 0 && endIndex > startIndex)
                    {
                        string mode = caption.Substring(startIndex + 1, endIndex - startIndex - 1);
                        return mode.Trim();
                    }

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
           // form.BackColor = System.Drawing.Color.FromArgb(15, 0, 0, 128); // Use the appropriate color format

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
                // Check if gobjActiveForm is not null and implements the ISearchableForm interface
                if (gobjActiveForm is Interface_for_Common_methods.ISearchableForm searchableForm)
                {
                    // Retrieve the current form's name
                    string formName = form.Name;

                    // Determine the key for the toolbar dictionary
                    string mode = GetMode(form);
                    string key = string.IsNullOrEmpty(mode) ? form.Name : $"{form.Name}-{mode}";

                    // Access the ToolStrip for the current form from the dictionary
                    ToolStrip toolStrip = toolbarDictionary1[key].Last();

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
                                searchableForm.ResetControls(gobjActiveForm.Controls);
                                // ClearCreatedByPanel();
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
                                    // form.DeleteForm();
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
                                    // form.PostForm();
                                    break;
                            }
                            break;
                        case Keys.F8:
                            if (GetMode(form) != null)
                            {
                                if (HasOption(strOptions, 'R'))
                                {
                                    // form.PrintDoc();
                                }
                            }
                            break;
                        case Keys.F10:
                            switch (GetMode(form))
                            {
                                case "ADDMODE":
                                case "MODIFYMODE":
                                    searchableForm.SaveForm();
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
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        /// <summary>
        /// --------------------old--------------
        /// </summary>

        /// ---------newest old
        //public static void ExecuteKeyDown(Form form, Keys KeyCode)
        //{
        //    try
        //    {
        //        // Access the ToolStrip from MainForm.Instance
        //        //  ToolStrip toolStrip = MainForm.Instance.tbrTools1; // Replace with your actual ToolStrip name

        //        // Access the ToolStrip for the current form----old
        //        //ToolStrip toolStrip = toolbarDictionary[form]; // Retrieve the ToolStrip for the current form from the dictionary
        //        ToolStrip toolStrip = toolbarDictionarywith_frmnm[form.Text]; // Retrieve the ToolStrip for the current form from the dictionary


        //        ToolStripItem toolStripItem = null;

        //        string strOptions = GetStrOptions(form); // Assign a value before the switch

        //        foreach (ToolStripItem item in toolStrip.Items)
        //        {
        //            if (item is ToolStripButton button)
        //            {
        //                if (item.Tag != null && item.Tag?.ToString() == form.Tag?.ToString())
        //                {
        //                    toolStripItem = item;
        //                    break;

        //                }
        //            }

        //        }
        //        if (toolStripItem != null && toolStripItem.Tag != null)
        //        {
        //            // Retrieve the Tag property of the ToolStripItem and assign it to strOptions   
        //            strOptions = toolStripItem.Tag.ToString();
        //        }


        //        switch (KeyCode)
        //        {
        //            case Keys.F2:
        //                if (GetMode(form) != null)
        //                {
        //                    //form.ClearForm();
        //                    //ClearCreatedByPanel();

        //                }
        //                break;
        //            case Keys.F3:
        //                if (GetMode(form) == null)
        //                {
        //                    if (HasOption(strOptions, 'A'))
        //                    {
        //                        ButtonClick((int)form.Tag, "ADDMODE");
        //                    }
        //                }
        //                break;

        //            case Keys.F4:
        //                if (GetMode(form) == null)
        //                {
        //                    if (HasOption(strOptions, 'M'))
        //                    {
        //                        ButtonClick((int)form.Tag, "MODIFYMODE");
        //                    }
        //                }
        //                break;

        //            case Keys.F5:
        //                switch (GetMode(form))
        //                {
        //                    case null:
        //                        if (HasOption(strOptions, 'D'))
        //                        {
        //                            ButtonClick((int)form.Tag, "DELETEMODE");
        //                        }
        //                        break;

        //                    case "DELETEMODE":
        //                        //form.DeleteForm();
        //                        break;
        //                }
        //                break;

        //            case Keys.F6:
        //                if (GetMode(form) == null)
        //                {
        //                    if (HasOption(strOptions, 'I'))
        //                    {
        //                        ButtonClick((int)form.Tag, "INQUIREMODE");
        //                    }
        //                }
        //                break;

        //            case Keys.F7:
        //                switch (GetMode(form))
        //                {
        //                    case null:
        //                        if (HasOption(strOptions, 'P'))
        //                        {
        //                            ButtonClick((int)form.Tag, "POSTMODE");
        //                        }
        //                        break;

        //                    case "POSTMODE":
        //                        //             form.PostForm();
        //                        break;
        //                }
        //                break;

        //            case Keys.F8:
        //                if (GetMode(form) != null)
        //                {
        //                    if (HasOption(strOptions, 'R'))
        //                    {
        //                        //           form.PrintDoc();
        //                    }
        //                }
        //                break;

        //            case Keys.F10:
        //                switch (GetMode(form))
        //                {
        //                    case "ADDMODE":
        //                    case "MODIFYMODE":
        //                        //           form.SaveForm();
        //                        break;
        //                }
        //                break;

        //            case Keys.Escape:
        //                switch (GetMode(form))
        //                {
        //                    case null:
        //                        ButtonClick((int)form.Tag, "Quit");
        //                        break;

        //                    default:
        //                        ButtonClick((int)form.Tag, "ModeQuit");
        //                        break;
        //                }
        //                break;

        //        }



        //    }
        //    catch (System.Exception ex)
        //    {
        //        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        //    }


        //}

        //public static void ExecuteKeyDown(Form form, Keys KeyCode)
        //{
        //    try
        //    {
        //        if (toolbarDictionary1.TryGetValue(form, out List<ToolStrip> toolbars))
        //        {
        //            ToolStrip toolStrip = toolbars.FirstOrDefault(); // Get the first toolbar associated with the form

        //            ToolStripItem toolStripItem = null;
        //            string strOptions = GetStrOptions(form);

        //            foreach (ToolStripItem item in toolStrip.Items)
        //            {
        //                if (item is ToolStripButton button)
        //                {
        //                    if (item.Tag != null && item.Tag?.ToString() == form.Tag?.ToString())
        //                    {
        //                        toolStripItem = item;
        //                        break;
        //                    }
        //                }
        //            }

        //            if (toolStripItem != null && toolStripItem.Tag != null)
        //            {
        //                strOptions = toolStripItem.Tag.ToString();
        //            }

        //            switch (KeyCode)
        //            {
        //                case Keys.F2:
        //                    if (GetMode(form) != null)
        //                    {
        //                        //form.ClearForm();
        //                        //ClearCreatedByPanel();
        //                    }
        //                    break;
        //                case Keys.F3:
        //                    if (GetMode(form) == null)
        //                    {
        //                        if (HasOption(strOptions, 'A'))
        //                        {
        //                            ButtonClick(toolbars.IndexOf(toolStrip), "ADDMODE");
        //                        }
        //                    }
        //                    break;

        //                case Keys.F4:
        //                    if (GetMode(form) == null)
        //                    {
        //                        if (HasOption(strOptions, 'M'))
        //                        {
        //                            ButtonClick(toolbars.IndexOf(toolStrip), "MODIFYMODE");
        //                        }
        //                    }
        //                    break;

        //                case Keys.F5:
        //                    switch (GetMode(form))
        //                    {
        //                        case null:
        //                            if (HasOption(strOptions, 'D'))
        //                            {
        //                                ButtonClick(toolbars.IndexOf(toolStrip), "DELETEMODE");
        //                            }
        //                            break;

        //                        case "DELETEMODE":
        //                            //form.DeleteForm();
        //                            break;
        //                    }
        //                    break;

        //                case Keys.F6:
        //                    if (GetMode(form) == null)
        //                    {
        //                        if (HasOption(strOptions, 'I'))
        //                        {
        //                            ButtonClick(toolbars.IndexOf(toolStrip), "INQUIREMODE");
        //                        }
        //                    }
        //                    break;

        //                case Keys.F7:
        //                    switch (GetMode(form))
        //                    {
        //                        case null:
        //                            if (HasOption(strOptions, 'P'))
        //                            {
        //                                ButtonClick(toolbars.IndexOf(toolStrip), "POSTMODE");
        //                            }
        //                            break;

        //                        case "POSTMODE":
        //                            //form.PostForm();
        //                            break;
        //                    }
        //                    break;

        //                case Keys.F8:
        //                    if (GetMode(form) != null)
        //                    {
        //                        if (HasOption(strOptions, 'R'))
        //                        {
        //                            //form.PrintDoc();
        //                        }
        //                    }
        //                    break;

        //                case Keys.F10:
        //                    switch (GetMode(form))
        //                    {
        //                        case "ADDMODE":
        //                        case "MODIFYMODE":
        //                            //form.SaveForm();
        //                            break;
        //                    }
        //                    break;

        //                case Keys.Escape:
        //                    switch (GetMode(form))
        //                    {
        //                        case null:
        //                            ButtonClick(toolbars.IndexOf(toolStrip), "Quit");
        //                            break;

        //                        default:
        //                            ButtonClick(toolbars.IndexOf(toolStrip), "ModeQuit");
        //                            break;
        //                    }
        //                    break;
        //            }
        //        }
        //        else
        //        {
        //            Messages.ErrorMsg("No toolbar found for the form.");
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}


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
                string strOptions, strAppMode = "",key="",mode="";

                // Retrieve the current form's name
                string formName = gobjActiveForm.Name;

                // Determine the key for the toolbar dictionary
                mode = GetMode(gobjActiveForm);
                key = string.IsNullOrEmpty(mode) ? gobjActiveForm.Name : $"{gobjActiveForm.Name}-{mode}";
                ToolStrip mobjToolbar = toolbarDictionary1[key].Last(); // Get the last toolbar associated with the form

                MenuStrip menustrip = MainForm.Instance.menuStrip1;

                strOptions = mobjToolbar.Tag?.ToString() ?? "AMDIPR";

                // Access the ToolStripItem at the specified TBRIndex
                ToolStripItem toolStripItem = mobjToolbar.Items[TBRIndex];

                //string imageFolderPath = System.IO.Path.Combine(Application.StartupPath, "Icons");
                //// string imageFolderPath = Path.Combine("Softgen//", "Icons");
                //string imagePath = System.IO.Path.Combine(imageFolderPath, "your_image.png");




                //ImageList dynamicImageList = new ImageList();

                //foreach (string imageFile in Directory.GetFiles(imageFolderPath, "*.png")) // Change the file extension to match your image type
                //{
                //    string imageName = Path.GetFileNameWithoutExtension(imageFile);
                //    Image image = Image.FromFile(imageFile);
                //    dynamicImageList.Images.Add(imageName, image);
                //}

                string imageNameToFind = BtnKey.Trim(); // The name of the image you want to find
                Image foundImage = null;

                ImageList dynamicImageList = new ImageList();
                dynamicImageList.ImageSize = new Size(26, 26);

                // Assuming "ImageKey1" and "ImageKey2" are the resource names
                dynamicImageList.Images.Add("Add", Images.Add);
                dynamicImageList.Images.Add("Authorization", Images.Authorization);
                dynamicImageList.Images.Add("Continue", Images.Continue);
                dynamicImageList.Images.Add("Delete", Images.Delete);
                dynamicImageList.Images.Add("DeleteMode", Images.DeleteMode);
                dynamicImageList.Images.Add("Fresh", Images.Fresh);
                dynamicImageList.Images.Add("Help", Images.Help);
                dynamicImageList.Images.Add("Inquire", Images.Inquire);
                dynamicImageList.Images.Add("Modify", Images.Modify);
                dynamicImageList.Images.Add("Post", Images.Post);
                dynamicImageList.Images.Add("Print", Images.Print);
                dynamicImageList.Images.Add("Quit", Images.Quit);
                dynamicImageList.Images.Add("Retrieve", Images.Retrieve);
                dynamicImageList.Images.Add("Save", Images.Save);


                // Add more images as needed

                for (int i = 0; i < dynamicImageList.Images.Count; i++)
                {
                    // Compare the image name at index i with the known image name
                    if (dynamicImageList.Images.Keys[i] == imageNameToFind)
                    {
                        // The image name at index i matches the known image name
                        foundImage = dynamicImageList.Images[i]; // Capture the matched image
                        break; // Exit the loop since you found the image
                    }
                }


                // Check if gobjActiveForm is not null and implements the ISearchableForm interface
                if (gobjActiveForm is Interface_for_Common_methods.ISearchableForm searchableForm)
                {
                  
                   

                    switch (BtnKey)
                    {
                        
                        case ADDMODE:
                            
                            strAppMode = ADDMODE;
                            ActivateForm(gobjActiveForm, true, ADDMODE);
                            searchableForm.SetSearchVar(true);
                            MainForm.Instance.mnuAdd.Checked = true;
                            searchableForm.check_temp_login_sytemname();//for loading unsaved data
                            
                            

                            break;
                        case MODIFYMODE:
                            strAppMode = MODIFYMODE;
                            ActivateForm(gobjActiveForm, true, MODIFYMODE);
                            searchableForm.SetSearchVar(false);
                            MainForm.Instance.mnuModify.Checked = true;
                            break;
                        case DELETEMODE:
                            strAppMode = DELETEMODE;
                            ActivateForm(gobjActiveForm, false, DELETEMODE);
                            searchableForm.SetSearchVar(false);
                            MainForm.Instance.mnuDeleteMode.Checked = true;
                            break;
                        case INQUIREMODE:
                            strAppMode = INQUIREMODE;
                            ActivateForm(gobjActiveForm, false, INQUIREMODE);
                            searchableForm.SetSearchVar(false);
                            MainForm.Instance.mnuInquire.Checked = true;
                            break;
                        case POSTMODE:
                            strAppMode = POSTMODE;
                            ActivateForm(gobjActiveForm, false, POSTMODE);
                            searchableForm.SetSearchVar(false);
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
                            SwitchMode(gobjActiveForm, strAppMode);
                            //SetModeCaption(gobjActiveForm, strAppMode);
                            //RemoveButtons(mobjToolbar);
                            mobjToolbar.Items.Clear();
                            DisableFileMenu();
                            MainForm.Instance.mnuHelp.Enabled = true;
                            break;
                    }

                    switch (BtnKey)
                    {
                        case ADDMODE:
                        case MODIFYMODE:
                            mobjbutton = new ToolStripButton("Save", dynamicImageList.Images[13], null, "Save");
                            mobjbutton.AutoSize = false;
                            mobjbutton.BackColor = Color.Lavender;
                            mobjbutton.Font = new Font("Times New Roman", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
                            mobjbutton.ImageAlign = ContentAlignment.TopCenter;
                            mobjbutton.ImageScaling = ToolStripItemImageScaling.None;
                            mobjbutton.Margin = new Padding(3);
                            mobjbutton.Size = new Size(51, 47);
                            mobjbutton.TextAlign = ContentAlignment.BottomCenter;
                            mobjbutton.TextImageRelation = TextImageRelation.Overlay;

                            
                            // Attach a click event handler for the button
                            mobjbutton.Click += (sender, e) =>
                            {
                                // Call searchableForm's SaveForm method
                                searchableForm.SaveForm();
                            };

                            MainForm.Instance.mnuSave.Enabled = true;
                            mobjToolbar.Items.Add(mobjbutton); // added to the mobjToolbar
                            break;
                    }

                    if (BtnKey == DELETEMODE)
                    {
                        mobjbutton = new ToolStripButton("Delete", dynamicImageList.Images[4], null, "Delete");
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
                        mobjToolbar.Items.Add(mobjbutton); // added to the mobjToolbar
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

                            mobjbutton = new ToolStripButton(QUITCAPTION, dynamicImageList.Images[11], null, "ModeQuit");
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

                            if (gobjActiveForm.Name == "frmM_Item")
                            {
                                searchableForm.ResetControls(gobjActiveForm.Controls);
                            }
                            else
                            {
                                searchableForm.ResetControls(gobjActiveForm.Controls);
                            }
                            break;


                       

                        //switch (BtnKey)
                        //{
                        case "Save":
                            searchableForm.SaveForm();


                            break;

                        case "DeleteForm":
                            //gobjActiveForm.DeleteForm();

                            break;

                        case "Fresh":
                            Messages.gstrMsg = "Do you want to refresh without saving the changes?";

                            if (searchableForm.GetDEStatus() == true)
                            {
                                //if (MessageBox.Show(gstrMsg, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                if (MessageBox.Show(Messages.gstrMsg, null, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                {
                                    return;
                                }
                            }
                            if (GetMode(gobjActiveForm) == ADDMODE)
                            {
                                searchableForm.SetSearchVar(true);
                            }
                            else
                            {
                                searchableForm.SetSearchVar(false);
                            }
                            if (gobjActiveForm.Name == "frmT_Invoice" && GetMode(gobjActiveForm) == ADDMODE)
                            {
                                //  gobjActiveForm.SaveTempDataForm();
                            }
                            searchableForm.ResetControls(gobjActiveForm.Controls);
                            //ClearCreatedByPanel();
                            ClearStatusBarHelp();

                            break;

                        case "Retrieve":
                            searchableForm.SearchForm();

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
                            //mobjbutton.Click += (sender, e) =>
                            //{
                            //    // Call searchableForm's SaveForm method

                            //    CallHelp();
                            //};
                            CallHelp();

                            break;

                        case "ModeQuit":
                            Messages.gstrMsg = "Do you want to exit without saving the changes?";
                            if (searchableForm.GetDEStatus() == true)
                            {
                                if (MessageBox.Show(Messages.gstrMsg, null, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                {
                                    return;
                                }
                            }
                            if (gobjActiveForm.Name == "frmT_Invoice" && GetMode(gobjActiveForm) == ADDMODE)
                            {
                                // gobjActiveForm.SaveTempDataForm();
                            }

                            gobjActiveForm.Text = RestoreCaption(gobjActiveForm);
                            DestroyToolbar(gobjActiveForm);
                            CreateToolbar(gobjActiveForm, strOptions);
                            ActivateForm(gobjActiveForm, false, null);
                            //    gobjActiveForm.Icon = mdiMain.Icon;
                            ClearStatusBarHelp();

                            break;

                        case "Quit":
                            gobjActiveForm.Close();
                            DisableFileMenu();

                            break;

                        case "SystemQuit":
                            if (Messages.ConfirmationMsg("Do you Want to close?") == DialogResult.Yes)
                            {
                                frmHelp frmHelp = new frmHelp();
                                frmHelp.Close();
                                MainForm.Instance.Close();
                            }
                            break;



                    }
                    //Again getting mode and key because added new toolstrip in SwitchMode Function

                    gobjActiveForm.Refresh();

                    
                    toolbarDictionary1[newKey].Add(mobjToolbar);

                    //if (toolbarDictionary1.ContainsKey(currentKey) && newToolbarDictionary.ContainsKey(newKey))
                    //{
                    //    // Get the list of ToolStrip objects from both dictionaries
                    //    List<ToolStrip> oldToolstrips = toolbarDictionary1[currentKey];
                    //    List<ToolStrip> newToolstrips = newToolbarDictionary[newKey];

                    //    // Create a list to store common ToolStripItems
                    //    List<ToolStripItem> commonItems = new List<ToolStripItem>();

                    //    // Iterate through the toolstrips and add their items to the commonItems list
                    //    foreach (ToolStrip toolstrip in oldToolstrips.Concat(newToolstrips))
                    //    {
                    //        commonItems.AddRange(toolstrip.Items.OfType<ToolStripItem>());
                    //    }

                    //    // Create a new ToolStrip for the common items
                    //    ToolStrip commonToolbar = new ToolStrip();
                    //    commonToolbar.Items.AddRange(commonItems.ToArray());

                    //    // Clear the common items from both dictionaries
                    //    oldToolstrips.RemoveAll(toolstrip => commonItems.Any(item => toolstrip.Items.Contains(item)));
                    //    newToolstrips.RemoveAll(toolstrip => commonItems.Any(item => toolstrip.Items.Contains(item)));

                    //    // Add the common ToolStrip to the old dictionary
                    //    oldToolstrips.Add(commonToolbar);

                    //    // Remove the common ToolStrip from the new dictionary
                    //    newToolstrips.Remove(commonToolbar);

                    //    // Add the remaining ToolStrip objects from newToolstrips to mobjToolbar
                    //    foreach (var newToolStrip in newToolstrips)
                    //    {
                    //        mobjToolbar.Items.AddRange(newToolStrip.Items.Cast<ToolStripItem>().ToArray());
                    //    }

                    //}
                    mobjToolbar.BringToFront();


                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //-------old
        //       public static void ButtonClick(int TBRIndex, string BtnKey)
        //       {
        //           try
        //           {
        //               string strOptions, strAppMode="";
        //               //  mobjToolbar = MainForm.Instance.tbrTools;
        //               //mobjToolbar = toolbarDictionary[gobjActiveForm]; --old



        //               mobjToolbar = toolbarDictionarywith_frmnm[gobjActiveForm.Text]; 



        //               //if (!toolbarDictionary1.TryGetValue(gobjActiveForm, out List<ToolStrip> toolbars))
        //               //{
        //               //    // Create a new toolbar for the active form if it doesn't exist
        //               //    toolbars = new List<ToolStrip>();
        //               //    toolbarDictionary1[gobjActiveForm] = toolbars;
        //               //}

        //               //// Access the most recent toolbar for this form
        //               //mobjToolbar = toolbars.LastOrDefault();

        //               MenuStrip menustrip = MainForm.Instance.menuStrip1;

        //               strOptions = mobjToolbar.Tag?.ToString()?? "AMDIPR";

        //               // Access the ToolStripItem at the specified TBRIndex
        //               ToolStripItem toolStripItem = mobjToolbar.Items[TBRIndex];

        //                       // Check if gobjActiveForm is not null and implements the ISearchableForm interface
        //               if (gobjActiveForm is Interface_for_Common_methods.ISearchableForm searchableForm)
        //               {
        //                   switch (BtnKey)
        //                   {
        //                       case ADDMODE:

        //                           strAppMode = ADDMODE;
        //                           ActivateForm(gobjActiveForm, true, ADDMODE);


        //                           searchableForm.SetSearchVar(true);
        //                           //gobjActiveForm.SetSearchVar(true);

        //                       MainForm.Instance.mnuAdd.Checked = true;

        //                           searchableForm.check_temp_login_sytemname();//for loading unsaved data



        //                           break;

        //                       case MODIFYMODE:
        //                           strAppMode = MODIFYMODE;
        //                           ActivateForm(gobjActiveForm, true, MODIFYMODE);

        //                               searchableForm.SetSearchVar(false);

        //                              MainForm.Instance.mnuModify.Checked = true;

        //                           break;

        //                       case DELETEMODE:
        //                           strAppMode = DELETEMODE;
        //                           ActivateForm(gobjActiveForm, false, DELETEMODE);

        //                               searchableForm.SetSearchVar(false);
        //                               MainForm.Instance.mnuDeleteMode.Checked = true;

        //                           break;

        //                       case INQUIREMODE:
        //                                   strAppMode = INQUIREMODE;
        //                                   ActivateForm(gobjActiveForm, false, INQUIREMODE);
        //                                   searchableForm.SetSearchVar(false);
        //                                   MainForm.Instance.mnuInquire.Checked = true;

        //                           break;

        //                               case POSTMODE:
        //                                   strAppMode = POSTMODE;
        //                                   ActivateForm(gobjActiveForm, false, POSTMODE);
        //                                   searchableForm.SetSearchVar(false);
        //                                   MainForm.Instance.mnuPost.Checked = true;


        //                           break;
        //                   }


        //               switch (BtnKey)
        //               {
        //                   case ADDMODE:
        //                   case MODIFYMODE:
        //                   case DELETEMODE:
        //                   case INQUIREMODE:
        //                   case POSTMODE:

        //                       ToolStripSeparator separator = new ToolStripSeparator();
        //                       mobjToolbar.Items.Add(separator);
        //                       SetModeCaption(gobjActiveForm, strAppMode);
        //                       RemoveButtons(mobjToolbar, strAppMode);
        //                       DisableFileMenu();
        //                       MainForm.Instance.mnuHelp.Enabled = true;
        //                      //toolbarDictionarywith_frmnm[gobjActiveForm.Text] = mobjToolbar;



        //                           break;

        //               }

        //               switch (BtnKey)
        //               {
        //                   case ADDMODE:
        //                   case MODIFYMODE:
        //                           mobjbutton = new ToolStripButton("Save", MainForm.Instance.imageList1.Images[8], null, "Save");

        //                           mobjbutton.AutoSize = false;
        //                           mobjbutton.BackColor = Color.Lavender;
        //                           mobjbutton.Font = new Font("Times New Roman", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
        //                           mobjbutton.ImageAlign = ContentAlignment.TopCenter;
        //                           mobjbutton.ImageScaling = ToolStripItemImageScaling.None;
        //                           mobjbutton.Margin = new Padding(3);
        //                           mobjbutton.Size = new Size(51, 47);
        //                           mobjbutton.TextAlign = ContentAlignment.BottomCenter;
        //                           mobjbutton.TextImageRelation = TextImageRelation.Overlay;

        //                           // Attach a click event handler for the button
        //                           mobjbutton.Click += (sender, e) =>
        //                           {
        //                               // Call searchableForm's SaveForm method
        //                               searchableForm.SaveForm();
        //                           };

        //                           MainForm.Instance.mnuSave.Enabled = true;

        //                           mobjToolbar.Items.Add(mobjbutton); //added to the  mobjToolbar

        //                           toolbarDictionarywith_frmnm[gobjActiveForm.Text] = mobjToolbar;



        //                           break;

        //               }

        //               if (BtnKey==DELETEMODE)
        //               {
        ////                   MainForm.Instance.btnDelete.Visible = true;
        //                   mobjbutton = new ToolStripButton("Delete", MainForm.Instance.imageList1.Images[13], null, "Delete");
        //                   mobjbutton.ToolTipText = "Deleting The Current Information";
        //                   mobjbutton.AutoSize = false;
        //                   mobjbutton.BackColor = Color.Lavender;
        //                   mobjbutton.BackgroundImageLayout = ImageLayout.Center;
        //                   mobjbutton.Font = new Font("Times New Roman", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
        //                   mobjbutton.ImageAlign = ContentAlignment.TopCenter;
        //                   mobjbutton.ImageScaling = ToolStripItemImageScaling.None;
        //                   mobjbutton.Margin = new Padding(3);
        //                   mobjbutton.Size = new Size(51, 47);
        //                   mobjbutton.TextAlign = ContentAlignment.BottomCenter;
        //                   mobjbutton.TextImageRelation = TextImageRelation.Overlay;

        //                   MainForm.Instance.mnuDeleteRecord.Enabled = true;       

        //                       mobjToolbar.Items.Add(mobjbutton); //added to  the mobjToolbar


        //                   }
        //               if (BtnKey == POSTMODE)
        //               {
        //                  CreateButton(mobjToolbar, "Authorise", "Authorise the Current Information");
        //                   MainForm.Instance.mnuAuthorise.Enabled = true;

        //                   }

        //                   switch (BtnKey)
        //                   {
        //                       case ADDMODE:
        //                       case MODIFYMODE:
        //                       case DELETEMODE:
        //                       case INQUIREMODE:
        //                       case POSTMODE:
        //                           CreateButton(mobjToolbar, "Fresh", strAppMode + " Fresh Information");
        //                           MainForm.Instance.mnuRefresh.Enabled = true;


        //                           if (strAppMode == POSTMODE)
        //                           {
        //                               mobjToolbar.Items[mobjToolbar.Items.Count - 1].ToolTipText = "Authorise Fresh Information";
        //                               mobjbutton.ToolTipText = "Authorise Fresh Information";

        //                           }

        //                           if (BtnKey != ADDMODE)
        //                           {
        //                               CreateButton(mobjToolbar, "Retrieve", "Find Information for Specified Criteria");
        //                               MainForm.Instance.mnuRetrieve.Enabled = true;
        //                           }

        //                           if (strOptions.EndsWith("R"))
        //                           {
        //                               CreateButton(mobjToolbar, "Print", "Print the Current Information");
        //                               MainForm.Instance.mnuPrint.Enabled = true;
        //                           }


        //                           mobjbutton = new ToolStripButton(QUITCAPTION, MainForm.Instance.imageList1.Images[7], null, "ModeQuit");
        //                           mobjbutton.ToolTipText = "Quit From " + strAppMode + " Mode";

        //                           mobjbutton.AutoSize = false;
        //                           mobjbutton.BackColor = Color.Lavender;
        //                           mobjbutton.BackgroundImageLayout = ImageLayout.Center;
        //                           mobjbutton.Font = new Font("Times New Roman", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
        //                           mobjbutton.ImageAlign = ContentAlignment.TopCenter;
        //                           mobjbutton.ImageScaling = ToolStripItemImageScaling.None;
        //                           mobjbutton.Margin = new Padding(3);
        //                           mobjbutton.Size = new Size(51, 47);
        //                           mobjbutton.TextAlign = ContentAlignment.BottomCenter;
        //                           mobjbutton.TextImageRelation = TextImageRelation.Overlay;

        //                           mobjToolbar.Items.Add(mobjbutton);

        //                           CreateButton(mobjToolbar, "Help", "Help Information");
        //                           //SendKeys "{F2}"
        //                           //gobjActiveForm.ClearForm();


        //                           break;

        //                           //switch (BtnKey)
        //                           //{
        //                               case "Save":
        //                                   searchableForm.SaveForm();


        //                           break;

        //                               case "DeleteForm":
        //                           //gobjActiveForm.DeleteForm();

        //                           break;

        //                               case "Fresh":
        //                                   Messages.gstrMsg = "Do you want to refresh without saving the changes?";

        //                                   if (searchableForm.GetDEStatus() == true)
        //                                   {
        //                                       //if (MessageBox.Show(gstrMsg, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
        //                                       if(MessageBox.Show(Messages.gstrMsg,null,MessageBoxButtons.YesNo,MessageBoxIcon.Question)== DialogResult.No)
        //                                       {
        //                                           return;
        //                                       }
        //                                   }
        //                                   if (GetMode(gobjActiveForm) == ADDMODE)
        //                                   {
        //                                       searchableForm.SetSearchVar(true);
        //                                   }
        //                                   else
        //                                   {
        //                                       searchableForm.SetSearchVar(false);
        //                                   }
        //                                   if (gobjActiveForm.Name == "frmT_Invoice" && GetMode(gobjActiveForm) == ADDMODE)
        //                                   {
        //                                     //  gobjActiveForm.SaveTempDataForm();
        //                                   }
        //                                   //gobjActiveForm.ClearForm();
        //                                   //ClearCreatedByPanel();
        //                                   ClearStatusBarHelp();

        //                               break;

        //                               case "Retrieve":
        //                                   //gobjActiveForm.SearchForm();

        //                               break;

        //                               case "Print":
        //                                   //    gobjActiveForm.PrintDoc();
        //                               break;

        //                               case "Authorise":
        //                                   //    gobjActiveForm.PostForm();
        //                               break;

        //                               case "Continue":
        //                                   //    gobjActiveForm.PrintReport();
        //                               break;

        //                               case "Help":
        //                                   mobjbutton.Click += (sender, e) =>
        //                                   {
        //                                       // Call searchableForm's SaveForm method

        //                                   CallHelp();
        //                                   };

        //                               break;

        //                               case "ModeQuit":
        //                                   Messages.gstrMsg = "Do you want to exit without saving the changes?";
        //                                   if (searchableForm.GetDEStatus() == true)
        //                                   {
        //                                       if (MessageBox.Show(Messages.gstrMsg, null, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
        //                                       {
        //                                           return;
        //                                       }
        //                                   }
        //                                   if (gobjActiveForm.Name == "frmT_Invoice" && GetMode(gobjActiveForm) == ADDMODE)
        //                                   {
        //                                      // gobjActiveForm.SaveTempDataForm();
        //                                   }

        //                                   gobjActiveForm.Text = RestoreCaption(gobjActiveForm);
        //                                   DestroyToolbar(gobjActiveForm);
        //                                   CreateToolbar(gobjActiveForm, strOptions);
        //                                   ActivateForm(gobjActiveForm, false, null);
        //                                   //    gobjActiveForm.Icon = mdiMain.Icon;
        //                                   ClearStatusBarHelp();

        //                               break;

        //                               case "Quit":
        //                                   gobjActiveForm.Close();
        //                                   DisableFileMenu();

        //                               break;

        //                               case "SystemQuit":
        //                                   if (Messages.ConfirmationMsg("Do you Want to close?") == DialogResult.Yes)
        //                                       {
        //                                           frmHelp frmHelp = new frmHelp();
        //                                           frmHelp.Close();
        //                                           MainForm.Instance.Close();
        //                                       }
        //                               break;


        //                   }
        //                   toolbarDictionarywith_frmnm[gobjActiveForm.Text] = mobjToolbar;
        //                   //mobjToolbar.BringToFront();
        //               }

        //           }

        //           catch (System.Exception ex)
        //           {
        //               MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        //           }


        //       }

        public static void Form_KeyUp(object sender, KeyEventArgs e)
        {
            // Check if F1 key is pressed
            if (e.KeyCode == Keys.F1)
            {
                // Call your Help function
                CallHelp();
            }
           
        }

        private static void CreateButton(ToolStrip ToolBar, string BtnKey, string ToolTip)
        {
            try
            {
                string imageNameToFind = BtnKey.Trim(); // The name of the image you want to find
                Image foundImage = null;



                // string imageFolderPath = System.IO.Path.Combine(Application.StartupPath, "Icons");
                // // string imageFolderPath = Path.Combine("Softgen//", "Icons");
                //  string imagePath = System.IO.Path.Combine(imageFolderPath, "your_image.png");




                //  ImageList dynamicImageList = new ImageList();
                //  dynamicImageList.ImageSize = new Size(23, 23); // Set the image size as needed

                //  // Load images from file locations and add them to the ImageList
                ////  dynamicImageList.Images.Add("ImageKey1", Image.FromFile("softgen\\Icons.png"));
                //  //dynamicImageList.Images.Add("ImageKey2", Image.FromFile("path_to_image2.png"));

                //  // Load images from the folder and add them to the ImageList
                //  foreach (string imageFile in Directory.GetFiles(imageFolderPath, "*.png")) // Change the file extension to match your image type
                //  {
                //      string imageName = Path.GetFileNameWithoutExtension(imageFile);
                //      Image image = Image.FromFile(imageFile);
                //      dynamicImageList.Images.Add(imageName, image);
                //  }

                ImageList dynamicImageList = new ImageList();
                dynamicImageList.ImageSize = new Size(26, 26);

                // Assuming "ImageKey1" and "ImageKey2" are the resource names
                dynamicImageList.Images.Add("Add", Images.Add);
                dynamicImageList.Images.Add("Authorization", Images.Authorization);
                dynamicImageList.Images.Add("Continue", Images.Continue);
                dynamicImageList.Images.Add("Delete", Images.Delete);
                dynamicImageList.Images.Add("DeleteMode", Images.DeleteMode);
                dynamicImageList.Images.Add("Fresh", Images.Fresh);
                dynamicImageList.Images.Add("Help", Images.Help);
                dynamicImageList.Images.Add("Inquire", Images.Inquire);
                dynamicImageList.Images.Add("Modify", Images.Modify);
                dynamicImageList.Images.Add("Post", Images.Post);
                dynamicImageList.Images.Add("Print", Images.Print);
                dynamicImageList.Images.Add("Quit", Images.Quit);
                dynamicImageList.Images.Add("Retrieve", Images.Retrieve);
                dynamicImageList.Images.Add("Save", Images.Save);
              
                
                // Add more images as needed

                for (int i = 0; i < dynamicImageList.Images.Count; i++)
                {
                    // Compare the image name at index i with the known image name
                    if (dynamicImageList.Images.Keys[i] == imageNameToFind)
                    {
                        // The image name at index i matches the known image name
                        foundImage = dynamicImageList.Images[i]; // Capture the matched image
                        break; // Exit the loop since you found the image
                    }
                }

                if (BtnKey == "Quit")
                {
                    mobjbutton = new ToolStripButton(BtnKey.Trim(), dynamicImageList.Images[3], null, QUITCAPTION.Trim());

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
                    ////// Imp....                    // Attach a click event handler for the button
                    mobjbutton.Click += (sender, e) =>
                    {
                        // Call the ButtonClick method with the appropriate parameters
                        ButtonClick(mobjToolbar.Items.IndexOf(mobjbutton), BtnKey);
                    };
                }

                if (BtnKey != "Quit")
                {

                    if (foundImage != null)
                    {

                        mobjbutton = new ToolStripButton(BtnKey.Trim(), foundImage, null, BtnKey.Trim());

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
                     

                        ////// Imp....                    // Attach a click event handler for the button
                       
                        mobjbutton.Click += (sender, e) =>
                        {
                            // Call the ButtonClick method with the appropriate parameters
                            ButtonClick(mobjToolbar.Items.IndexOf(mobjbutton), BtnKey);
                        };

                        ToolBar.Items.Add(mobjbutton);
                    }
                    else
                    {
                        MessageBox.Show("FoundImage is Null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //---------newest old
        //private static void CreateButton(ToolStrip ToolBar, string BtnKey, string ToolTip)
        //{
        //    try
        //    {
        //        //string mode = GetMode(gobjActiveForm);
        //        //string key =gobjActiveForm.Name;
        //        //ToolStrip mobjToolbar = toolbarDictionary1[key].Last(); // Get the last toolbar associated with the form



        //        string imageNameToFind = BtnKey.Trim(); // The name of the image you want to find
        //        Image foundImage = null;

        //        if (BtnKey == "Quit")
        //        {
        //            mobjbutton = new ToolStripButton(BtnKey.Trim(), MainForm.Instance.imageList1.Images[7], null, QUITCAPTION.Trim());

        //            mobjbutton.AutoSize = false;
        //            mobjbutton.BackColor = Color.Lavender;
        //            mobjbutton.BackgroundImageLayout = ImageLayout.Center;
        //            mobjbutton.Font = new Font("Times New Roman", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
        //            mobjbutton.ImageAlign = ContentAlignment.TopCenter;
        //            mobjbutton.ImageScaling = ToolStripItemImageScaling.None;
        //            mobjbutton.Margin = new Padding(3);
        //            mobjbutton.Size = new Size(51, 47);
        //            mobjbutton.TextAlign = ContentAlignment.BottomCenter;
        //            mobjbutton.TextImageRelation = TextImageRelation.Overlay;
        //            mobjbutton.ToolTipText = ToolTip;

        //            ToolBar.Items.Add(mobjbutton);
        //            ////// Imp....                    // Attach a click event handler for the button
        //            mobjbutton.Click += (sender, e) =>
        //            {
        //                // Call the ButtonClick method with the appropriate parameters
        //                ButtonClick(mobjToolbar.Items.IndexOf(mobjbutton), BtnKey);
        //            };

        //        }


        //            imageList = MainForm.Instance.imageList1; // Your ImageList                                                                                                                   

        //            // Iterate through the images in the ImageList
        //            for (int i = 0; i < imageList.Images.Count; i++)
        //            {
        //                // Compare the image name at index i with the known image name
        //                if (imageList.Images.Keys[i] == imageNameToFind)
        //                {
        //                    // The image name at index i matches the known image name
        //                    foundImage = imageList.Images[i]; // Capture the matched image
        //                    break; // Exit the loop since you found the image
        //                }
        //            }
        //            if (foundImage != null)
        //            {
        //                mobjbutton = new ToolStripButton(BtnKey.Trim(), foundImage, null, BtnKey.Trim());

        //                mobjbutton.AutoSize = false;
        //                mobjbutton.BackColor = Color.Lavender;
        //                mobjbutton.BackgroundImageLayout = ImageLayout.Center;
        //                mobjbutton.Font = new Font("Times New Roman", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
        //                mobjbutton.ImageAlign = ContentAlignment.TopCenter;
        //                mobjbutton.ImageScaling = ToolStripItemImageScaling.None;
        //                mobjbutton.Margin = new Padding(3);
        //                mobjbutton.Size = new Size(51, 47);
        //                mobjbutton.TextAlign = ContentAlignment.BottomCenter;
        //                mobjbutton.TextImageRelation = TextImageRelation.Overlay;
        //                mobjbutton.ToolTipText = ToolTip;

        //                ////// Imp....                    // Attach a click event handler for the button
        //                mobjbutton.Click += (sender, e) =>
        //                {
        //                    // Call the ButtonClick method with the appropriate parameters
        //                    ButtonClick(mobjToolbar.Items.IndexOf(mobjbutton), BtnKey);
        //                };

        //                ToolBar.Items.Add(mobjbutton);
        //            }

        //        else
        //        {
        //            MessageBox.Show("FoundImage is Null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}


        //----------old
        //private static void CreateButton( ToolStrip ToolBar,string BtnKey,string ToolTip)
        //{
        //    try
        //    {

        //       // gobjActiveForm = Form.ActiveForm;
        //        string imageNameToFind = BtnKey.Trim(); // The name of the image you want to find
        //        Image foundImage = null;

        //        if (BtnKey == "Quit")
        //        {
        //            mobjbutton = new ToolStripButton(BtnKey.Trim(), MainForm.Instance.imageList1.Images[7],null,QUITCAPTION.Trim());

        //            mobjbutton.AutoSize = false;
        //            mobjbutton.BackColor = Color.Lavender;
        //            mobjbutton.BackgroundImageLayout = ImageLayout.Center;
        //            mobjbutton.Font = new Font("Times New Roman", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
        //            mobjbutton.ImageAlign = ContentAlignment.TopCenter;
        //            mobjbutton.ImageScaling = ToolStripItemImageScaling.None;
        //            mobjbutton.Margin = new Padding(3);
        //            mobjbutton.Size = new Size(51, 47);
        //            mobjbutton.TextAlign = ContentAlignment.BottomCenter;
        //            mobjbutton.TextImageRelation = TextImageRelation.Overlay;
        //            mobjbutton.ToolTipText = ToolTip;

        //            ToolBar.Items.Add(mobjbutton);
        //            ////// Imp....                    // Attach a click event handler for the button
        //            mobjbutton.Click += (sender, e) =>
        //            {
        //                // Call the ButtonClick method with the appropriate parameters
        //                ButtonClick(mobjToolbar.Items.IndexOf(mobjbutton), BtnKey);
        //            };

        //        }

        //        ImageList imageList = MainForm.Instance.imageList1; // Your ImageList
        //                                                            // Iterate through the images in the ImageList
        //        for (int i = 0; i < imageList.Images.Count; i++)
        //        {
        //            // Compare the image name at index i with the known image name
        //            if (imageList.Images.Keys[i] == imageNameToFind)
        //            {
        //                // The image name at index i matches the known image name
        //                foundImage = imageList.Images[i]; // Capture the matched image
        //                break; // Exit the loop since you found the image
        //            }
        //        }
        //        if (foundImage!= null)
        //        {

        //            mobjbutton = new ToolStripButton(BtnKey.Trim(), foundImage,null,BtnKey.Trim());

        //            mobjbutton.AutoSize = false;
        //            mobjbutton.BackColor = Color.Lavender;
        //            mobjbutton.BackgroundImageLayout = ImageLayout.Center;
        //            mobjbutton.Font = new Font("Times New Roman", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
        //            mobjbutton.ImageAlign = ContentAlignment.TopCenter;
        //            mobjbutton.ImageScaling = ToolStripItemImageScaling.None;
        //            mobjbutton.Margin = new Padding(3);
        //            mobjbutton.Size = new Size(51, 47);
        //            mobjbutton.TextAlign = ContentAlignment.BottomCenter;
        //            mobjbutton.TextImageRelation = TextImageRelation.Overlay;
        //            mobjbutton.ToolTipText = ToolTip;


        //            ////// Imp....                    // Attach a click event handler for the button
        //            mobjbutton.Click += (sender, e) =>
        //            {
        //                // Call the ButtonClick method with the appropriate parameters
        //                ButtonClick(mobjToolbar.Items.IndexOf(mobjbutton), BtnKey);
        //            };

        //            ToolBar.Items.Add(mobjbutton);
        //        }

        //        else
        //        {
        //            MessageBox.Show("FoundImage is Null ","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);                      
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        public static void CreateToolbar(Form form, string Options)
        {
            try
            {
                string mode = GetMode(form);
                string key = string.IsNullOrEmpty(mode) ? form.Name : $"{form.Name}-{mode}";

                // Check if the dictionary already contains the key (form name + mode)
                if (!toolbarDictionary1.ContainsKey(key))
                {
                    toolbarDictionary1[key] = new List<ToolStrip>();
                }

                int I = GetFreeIndex(); // Obtain a free index for the toolbar

                mobjToolbar = new ToolStrip();

                mobjToolbar.Items.Clear(); // Clear existing buttons

                mobjToolbar.AutoSize = false;
                mobjToolbar.BackColor = Color.CadetBlue;
                mobjToolbar.Dock = DockStyle.None;
                mobjToolbar.GripStyle = ToolStripGripStyle.Hidden;
                mobjToolbar.Location = new Point(2, 550);
                mobjToolbar.Size = new Size(417, 52);
                mobjToolbar.TabIndex = 7;

                mobjToolbar.ImageList = MainForm.Instance.imageList1;
                string formName = form.Name.Trim();
                string strFormType = formName.Length > 3 ? formName.Substring(3, 1).ToUpper() : string.Empty;

                if (!MainForm.Instance.Controls.Contains(mobjToolbar))
                {
                    MainForm.Instance.Controls.Add(mobjToolbar);
                    form.Tag = I;
                    mobjToolbar.AutoSize = false;
                    mobjToolbar.BackColor = Color.CadetBlue;
                    mobjToolbar.Dock = DockStyle.None;
                    mobjToolbar.GripStyle = ToolStripGripStyle.Hidden;
                    mobjToolbar.Location = new Point(1, 629);
                    mobjToolbar.Size = new Size(417, 52);
                    mobjToolbar.TabIndex = 7;
                }

                if (strFormType == "C")
                {
                    CreateButton(mobjToolbar, "Continue", "Print CheckList!");
                }
                else if (strFormType == "R")
                {
                    CreateButton(mobjToolbar, "Continue", "Print Report!");
                }
                else
                {
                    DisableFileMenu();
                    for (I = 1; I <= Options.Length; I++)
                    {
                        switch (Options[I - 1].ToString().ToUpper())
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

                if (form.Name == "MainForm")
                {
                    if (MainForm.Instance.MdiChildren.Length == 0)
                    {
                        mobjbutton = new ToolStripButton(QUITCAPTION, MainForm.Instance.imageList1.Images[14], null, "SystemQuit");

                        mobjbutton.AutoSize = false;
                        mobjbutton.BackColor = Color.Lavender;
                        mobjbutton.BackgroundImageLayout = ImageLayout.Center;
                        mobjbutton.Font = new Font("Times New Roman", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
                        mobjbutton.ImageAlign = ContentAlignment.TopCenter;
                        mobjbutton.ImageScaling = ToolStripItemImageScaling.SizeToFit;
                        mobjbutton.Margin = new Padding(3);
                        MainForm.Instance.imageList1.ImageSize = new Size(48, 48);
                        mobjbutton.Size = new Size(51, 47);
                        mobjbutton.TextAlign = ContentAlignment.BottomCenter;
                        mobjbutton.TextImageRelation = TextImageRelation.Overlay;
                        mobjbutton.ToolTipText = "Quit from the System";

                        mobjToolbar.Items.Add(mobjbutton);
                    }
                }
                else
                {
                    if (strFormType == "C")
                    {
                        CreateButton(mobjToolbar, "Quit", "Quit from Checklist");
                    }
                    else if (strFormType == "R")
                    {
                        CreateButton(mobjToolbar, "Quit", "Quit from Report");
                    }
                    else
                    {
                        CreateButton(mobjToolbar, "Quit", "Quit from Entry Screen");
                    }
                }

                CreateButton(mobjToolbar, "Help", "Help Information!");

                MainForm.Instance.Controls.Add(mobjToolbar);
                form.Tag = I;

                // Attach the created toolbar's reference to the form in the dictionary
                toolbarDictionary1[key].Add(mobjToolbar);

                // Update the tag with the options
                mobjToolbar.Tag = Options;

                mobjToolbar.Visible = true;
                mobjToolbar.BringToFront();
            }
            catch (Exception ex)
            {
                Messages.ErrorMsg(ex.ToString());
            }
        }
        //----old
        //  public static void CreateToolbar(Form form, string Options)
        //{
        //    try
        //    {

        //        int I = GetFreeIndex(); // Obtain a free index for the toolbar
        //                                //mobjToolbar = new ToolStrip();
        //                                //mobjToolbar = MainForm.Instance.tbrTools; // Use the existing tbrTools from MainForm
        //        mobjToolbar = new ToolStrip(); // Create a new ToolStrip for each form


        //        mobjToolbar.Items.Clear(); // Clear existing buttons

        //        mobjToolbar.AutoSize = false;
        //        mobjToolbar.BackColor = Color.CadetBlue;
        //        mobjToolbar.Dock = DockStyle.None;
        //        mobjToolbar.GripStyle = ToolStripGripStyle.Hidden;
        //        mobjToolbar.Location = new Point(2, 550);
        //        //mobjToolbar.Name = "tbrTools";
        //        mobjToolbar.Size = new Size(417, 52);
        //        mobjToolbar.TabIndex = 7;

        //        mobjToolbar.ImageList = MainForm.Instance.imageList1;
        //        string formName = form.Name.Trim();
        //        string strFormType = formName.Length > 3 ? formName.Substring(3, 1).ToUpper():string.Empty;

        //        if (!MainForm.Instance.Controls.Contains(mobjToolbar))
        //        {


        //            MainForm.Instance.Controls.Add(mobjToolbar);
        //            form.Tag = I;
        //            mobjToolbar.AutoSize = false;
        //            mobjToolbar.BackColor = Color.CadetBlue;
        //            mobjToolbar.Dock = DockStyle.None;
        //            mobjToolbar.GripStyle = ToolStripGripStyle.Hidden;
        //            mobjToolbar.Location = new Point(1, 629);
        //            //mobjToolbar.Name = "tbrTools";
        //            mobjToolbar.Size = new Size(417, 52);
        //            mobjToolbar.TabIndex = 7;
        //        }

        //        if (strFormType =="C") //CheckList Form
        //        {
        //            CreateButton(mobjToolbar, "Continue", "Print CheckList!");
        //        }
        //        else if (strFormType== "R") //Report Form
        //        {
        //            CreateButton(mobjToolbar, "Continue", "Print Report!");
        //        }

        //        else
        //        {
        //            DisableFileMenu();
        //            for (I = 1; I <= Options.Length; I++)
        //            {

        //                switch (Options[I-1].ToString().ToUpper())
        //                {
        //                    case "A":
        //                        MainForm.Instance.mnuAdd.Enabled = true;
        //                        CreateButton(mobjToolbar, ADDMODE, "Add New Information");
        //                        break;
        //                    case "M":
        //                        MainForm.Instance.mnuModify.Enabled = true;
        //                        CreateButton(mobjToolbar, MODIFYMODE, "Modify Existing Information");
        //                        break;
        //                    case "D":
        //                        MainForm.Instance.mnuDeleteMode.Enabled = true;
        //                        CreateButton(mobjToolbar, DELETEMODE, "Delete Existing Information");
        //                        break;
        //                    case "I":
        //                        MainForm.Instance.mnuInquire.Enabled = true;
        //                        CreateButton(mobjToolbar, INQUIREMODE, "Inquire Existing Information");
        //                        break;
        //                    case "P":
        //                        MainForm.Instance.mnuPost.Enabled = true;
        //                        CreateButton(mobjToolbar, POSTMODE, "Post Existing Information");
        //                        break;


        //                }
        //            }


        //        }

        //            if (form.Name == "MainForm" )
        //        {
        //            if (MainForm.Instance.MdiChildren.Length == 0)
        //            {
        //                mobjbutton = new ToolStripButton(QUITCAPTION, MainForm.Instance.imageList1.Images[7], null, "SystemQuit");

        //                mobjbutton.AutoSize = false;
        //                mobjbutton.BackColor = Color.Lavender;
        //                mobjbutton.BackgroundImageLayout = ImageLayout.Center;
        //                mobjbutton.Font = new Font("Times New Roman", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
        //                mobjbutton.ImageAlign = ContentAlignment.TopCenter;
        //                mobjbutton.ImageScaling = ToolStripItemImageScaling.None;
        //                mobjbutton.Margin = new Padding(3);
        //                mobjbutton.Size = new Size(51, 47);
        //                mobjbutton.TextAlign = ContentAlignment.BottomCenter;
        //                mobjbutton.TextImageRelation = TextImageRelation.Overlay;
        //                mobjbutton.ToolTipText = "Quit from the System";

        //                mobjToolbar.Items.Add(mobjbutton);
        //            }
        //        }
        //        else
        //        {
        //            if (strFormType== "C") // cheklist
        //            {
        //                CreateButton(mobjToolbar, "Quit", "Quit from Checklist");

        //            }
        //            else if (strFormType== "R")
        //            {
        //                CreateButton(mobjToolbar,"Quit","Quit from Report");

        //            }
        //            else {
        //                CreateButton(mobjToolbar,"Quit","Quit from Entry Screen");
        //            }

        //        }

        //        CreateButton(mobjToolbar,"Help","Help Information!");

        //        MainForm.Instance.Controls.Add(mobjToolbar);
        //        form.Tag = I;
        //        // Attach the created toolbar's reference to the form in the dictionary
        //        //DeTools.toolbarDictionary[form] = mobjToolbar;--old
        //        DeTools.toolbarDictionarywith_frmnm[form.Text] = mobjToolbar;


        //        mobjToolbar.Tag = Options;

        //        mobjToolbar.Visible = true;
        //        mobjToolbar.BringToFront();


        //    }
        //    catch(Exception ex)
        //    {
        //        Messages.ErrorMsg(ex.ToString());
        //    }   
        //}


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


        //public static void DestroyToolbar(Form form)
        //{
        //    if (form.Tag != null)
        //    {
        //        //form.ClearForm();    todo
        //        DisableFileMenu();
        //        UncheckFileMenu();
        //        ClearStatusBarHelp();
        //        int toolbarIndex = (int)form.Tag; // Assuming you stored the toolbar index in the Tag property
        //        if (toolbarDictionary.ContainsKey(form))
        //        {
        //            ToolStrip toolStrip= toolbarDictionary[form];//this have value a toolstrip associated with the specific form where it will be called. and stored in toolstrip.
        //            toolStrip.Dispose();
        //            toolbarDictionary.Remove(form); //clear the dictionary

        //        }
        //            maintTBRIndex[(int)form.Tag] = true;
        //        form.Tag = null;




        //    }

        //}

        public static void DestroyToolbar(Form form)
        {
            string formName = form.Name;
            string mode = GetMode(form);
            string key = string.IsNullOrEmpty(mode) ? formName : $"{formName}-{mode}";

            if (toolbarDictionary1.ContainsKey(key))
            {
                List<ToolStrip> toolbars = toolbarDictionary1[key];

                if (toolbars.Any())
                {
                    ToolStrip mobjToolbar = toolbars.Last();

                    // Remove the ToolStrip from the parent form's controls
                    if (mobjToolbar.Parent != null)
                    {
                        mobjToolbar.Parent.Controls.Remove(mobjToolbar);
                    }

                    // Clear the ToolStrip's items and dispose of it
                    mobjToolbar.Items.Clear();
                    mobjToolbar.Dispose();

                    // Remove the toolbar from the list of toolbars
                    toolbars.RemoveAt(toolbars.Count - 1);

                    // If there are no more toolbars for this form and mode, remove the key
                    if (toolbars.Count == 0)
                    {
                        toolbarDictionary1.Remove(key);
                        int index = (int)form.Tag;
                        DecrementIndex(index);
                    }

                
                }
            }
        }

        private static void DecrementIndex(int index)
        {
            if (index >= 1 && index <= mintUBound)
            {
                maintTBRIndex[index] = false; // Mark the index as not in use
            }
        }

        //--------newest old
        //public static void DestroyToolbar(Form form)
        //{
        //    //if (toolbarDictionary.ContainsKey(form))--old
        //    if (toolbarDictionarywith_frmnm.ContainsKey(form.Text))
        //    {
        //        //ToolStrip mobjToolbar = toolbarDictionary[form];--old
        //        ToolStrip mobjToolbar = toolbarDictionarywith_frmnm[form.Text];

        //        // Remove the ToolStrip from the parent form's controls
        //        if (mobjToolbar.Parent != null)
        //        {
        //            mobjToolbar.Parent.Controls.Remove(mobjToolbar);
        //        }

        //        // Clear the ToolStrip's items and dispose of it
        //        mobjToolbar.Items.Clear();
        //        mobjToolbar.Dispose();

        //        // Remove the form from the dictionary
        //        //toolbarDictionary.Remove(form);--old
        //           toolbarDictionarywith_frmnm.Remove(form.Text);
        //    }
        //}

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

        //public static string GetOptions(string FormName)
        //{

        //    ConnectionString = "DSN=softgen_db_my;Uid=root;";
        //    gstrSQL = "SELECT * FROM s_logopt WHERE login_id =" +"login_id+ AND prog_id = @formName";

        //    using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        //    {
        //        connection.Open();

        //        using (MySqlCommand cmd = new MySqlCommand(gstrSQL, connection))
        //        {
        //            cmd.Parameters.AddWithValue("@login_id", gstrloginId); // Assuming gstrLogin_id is a global variable
        //            cmd.Parameters.AddWithValue("@formName", FormName);

        //            using (MySqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    string options = string.Empty;

        //                    if (Convert.ToInt32(reader["can_add"]) == 1) options += "A";
        //                    if (Convert.ToInt32(reader["can_modify"]) == 1) options += "M";
        //                    if (Convert.ToInt32(reader["can_delete"]) == 1) options += "D";
        //                    if (Convert.ToInt32(reader["can_inquire"]) == 1) options += "I";
        //                    if (Convert.ToInt32(reader["can_post"]) == 1) options += "P";
        //                    if (Convert.ToInt32(reader["can_print"]) == 1) options += "R";

        //                    return options;
        //                }
        //                else
        //                {
        //                    return string.Empty;
        //                }
        //            }
        //        }
        //    }
        //}

        public static string GetOptions(string FormName)
        {
            string query = "SELECT * FROM s_logopt WHERE login_id = ? AND prog_id = ?";
            string options = string.Empty;

            try
            {
                DbConnector db = new DbConnector(); // Create an instance of DbConnector
                OdbcParameter[] parameters = new OdbcParameter[2];
                parameters[0] = new OdbcParameter("login_id", gstrloginId);
                parameters[1] = new OdbcParameter("formName", FormName);

                using (OdbcDataReader reader = db.ExecuteReader(query, parameters))
                {
                    if (reader.Read())
                    {
                        if (Convert.ToInt32(reader["can_add"]) == 1) options += "A";
                        if (Convert.ToInt32(reader["can_modify"]) == 1) options += "M";
                        if (Convert.ToInt32(reader["can_delete"]) == 1) options += "D";
                        if (Convert.ToInt32(reader["can_inquire"]) == 1) options += "I";
                        if (Convert.ToInt32(reader["can_post"]) == 1) options += "P";
                        if (Convert.ToInt32(reader["can_print"]) == 1) options += "R";
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                Console.WriteLine(ex.Message);
            }

            return options;
        }

        private static void RemoveButtons(ToolStrip toolStrip)
        {
            // Clear all items (buttons) from the ToolStrip
            toolStrip.Items.Clear();

            // Retrieve the current form's name
            string formName = gobjActiveForm.Name;

            // Determine the key for the toolbar dictionary
            string mode = GetMode(gobjActiveForm);
            string key = string.IsNullOrEmpty(mode) ? gobjActiveForm.Name : $"{gobjActiveForm.Name}-{mode}";

            if (toolbarDictionary1.ContainsKey(key))
            {
                // The key exists, so add a new ToolStrip to it
                ToolStrip newToolbar = new ToolStrip();
                toolbarDictionary1[key].Add(newToolbar);

                // Now you can add this newToolbar to the dictionary
            }
            else
            {
                // Handle the case when the key doesn't exist (e.g., create a new list and add the toolbar)
                var newToolbarList = new List<ToolStrip>();
                newToolbarList.Add(new ToolStrip());
                toolbarDictionary1[key] = newToolbarList;
            }
        }


        //-------newest old
        //private static void RemoveButtons(ToolStrip toolStrip)
        //{
        //    // Clear all items (buttons) from the ToolStrip
        //    toolStrip.Items.Clear();

        //    // Create a new ToolStrip object
        //    ToolStrip newToolbar = new ToolStrip();

        //    // Update the dictionary with the newToolbar for gobjActiveForm
        //    // Retrieve the current form's name
        //    string formName = gobjActiveForm.Name;

        //    // Determine the key for the toolbar dictionary
        //    string mode = GetMode(gobjActiveForm);
        //    string key = string.IsNullOrEmpty(mode) ? gobjActiveForm.Name : $"{gobjActiveForm.Name}-{mode}";
        //    toolbarDictionary1[key].Add(newToolbar);

        //    // Now you can add this newToolbar to the dictionary

        //    // Don't create a new toolbar here, just update the dictionary with the newToolbar
        //}


        //-------------old

        //private static void RemoveButtons(ToolStrip toolStrip, string strAppMode)
        //{
        //    int totalButtons = toolStrip.Items.Count;

        //    for (int Counter = totalButtons - 1; Counter >= 0; Counter--)
        //    {
        //        toolStrip.Items.RemoveAt(Counter);
        //    }
        //    //--------old//
        //    // Create a new ToolStrip object
        //    mobjToolbar = new ToolStrip();

        //    // Update the dictionary with the newToolbar for gobjActiveForm
        //    //toolbarDictionary[gobjActiveForm] = newToolbar;
        //    toolbarDictionarywith_frmnm[gobjActiveForm.Text] = mobjToolbar;

        //    //----------------
        //    // Clear the list of toolbars associated with gobjActiveForm
        //    //if (toolbarDictionary1.ContainsKey(gobjActiveForm))
        //    //{
        //    //    toolbarDictionary1[gobjActiveForm].Clear();
        //    //}
        //    // CreateToolbar(gobjActiveForm, GetOptions(gobjActiveForm.Name));

        //}


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
            string formName = form.Name;
            string strMode = GetMode(form);
            string key = string.IsNullOrEmpty(strMode) ? formName : $"{formName}-{strMode}";

            if (toolbarDictionary1.ContainsKey(key))
            {
                List<ToolStrip> toolbars = toolbarDictionary1[key];

                if (toolbars.Any())
                {
                    ToolStrip toolStrip = toolbars.Last();
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
            }
        }


        //newest old-----------------------------------------------
        //public static void ActiveFileMenu(Form form)
        //{
        //    string strMode = GetMode(form);
        //    //ToolStrip toolStrip = toolbarDictionary[form]; // Retrieve the ToolStrip for this form--old
        //    ToolStrip toolStrip = toolbarDictionarywith_frmnm[form.Text]; // Retrieve the ToolStrip for this form
        //    string strOptions = (string)toolStrip.Tag;

        //    if (string.IsNullOrEmpty(strOptions))
        //    {
        //        strOptions = "AMDIPR";
        //    }

        //    DisableFileMenu();
        //    UncheckFileMenu();

        //    if (string.IsNullOrEmpty(strMode))
        //    {
        //        MainForm.Instance.mnuHelp.Enabled = false;

        //        for (int i = 0; i < strOptions.Length; i++)
        //        {
        //            char option = char.ToUpper(strOptions[i]);
        //            switch (option)
        //            {
        //                case 'A':
        //                    MainForm.Instance.mnuAdd.Enabled = true;
        //                    break;
        //                case 'M':
        //                    MainForm.Instance.mnuModify.Enabled = true;
        //                    break;
        //                case 'D':
        //                    MainForm.Instance.mnuDeleteMode.Enabled = true;
        //                    break;
        //                case 'I':
        //                    MainForm.Instance.mnuInquire.Enabled = true;
        //                    break;
        //                case 'P':
        //                    MainForm.Instance.mnuPost.Enabled = true;
        //                    break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        MainForm.Instance.mnuHelp.Enabled = true;
        //        MainForm.Instance.mnuRefresh.Enabled = true;

        //        if (strMode != ADDMODE)
        //        {
        //            MainForm.Instance.mnuRetrieve.Enabled = true;
        //        }

        //        if (strOptions.EndsWith("R"))
        //        {
        //            MainForm.Instance.mnuPrint.Enabled = true;
        //        }

        //        switch (strMode)
        //        {
        //            case ADDMODE:
        //                MainForm.Instance.mnuAdd.Checked = true;
        //                MainForm.Instance.mnuSave.Enabled = true;
        //                break;
        //            case MODIFYMODE:
        //                MainForm.Instance.mnuModify.Checked = true;
        //                MainForm.Instance.mnuSave.Enabled = true;
        //                break;
        //            case DELETEMODE:
        //                MainForm.Instance.mnuDeleteMode.Checked = true;
        //                MainForm.Instance.mnuDeleteRecord.Enabled = true;
        //                break;
        //            case INQUIREMODE:
        //                MainForm.Instance.mnuInquire.Checked = true;
        //                break;
        //            case POSTMODE:
        //                MainForm.Instance.mnuPost.Checked = true;
        //                MainForm.Instance.mnuAuthorise.Enabled = true;
        //                break;
        //        }
        //    }
        //}

        //public static void ActiveFileMenu(Form form)
        //{
        //    string strMode = GetMode(form);


        //        string strOptions = (string)toolStrip.Tag;

        //        if (string.IsNullOrEmpty(strOptions))
        //        {
        //            strOptions = "AMDIPR";
        //        }

        //        DisableFileMenu();
        //        UncheckFileMenu();

        //        if (string.IsNullOrEmpty(strMode))
        //        {
        //            MainForm.Instance.mnuHelp.Enabled = false;

        //            for (int i = 0; i < strOptions.Length; i++)
        //            {
        //                char option = char.ToUpper(strOptions[i]);
        //                switch (option)
        //                {
        //                    case 'A':
        //                        MainForm.Instance.mnuAdd.Enabled = true;
        //                        break;
        //                    case 'M':
        //                        MainForm.Instance.mnuModify.Enabled = true;
        //                        break;
        //                    case 'D':
        //                        MainForm.Instance.mnuDeleteMode.Enabled = true;
        //                        break;
        //                    case 'I':
        //                        MainForm.Instance.mnuInquire.Enabled = true;
        //                        break;
        //                    case 'P':
        //                        MainForm.Instance.mnuPost.Enabled = true;
        //                        break;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            MainForm.Instance.mnuHelp.Enabled = true;
        //            MainForm.Instance.mnuRefresh.Enabled = true;

        //            if (strMode != ADDMODE)
        //            {
        //                MainForm.Instance.mnuRetrieve.Enabled = true;
        //            }

        //            if (strOptions.EndsWith("R"))
        //            {
        //                MainForm.Instance.mnuPrint.Enabled = true;
        //            }

        //            switch (strMode)
        //            {
        //                case ADDMODE:
        //                    MainForm.Instance.mnuAdd.Checked = true;
        //                    MainForm.Instance.mnuSave.Enabled = true;
        //                    break;
        //                case MODIFYMODE:
        //                    MainForm.Instance.mnuModify.Checked = true;
        //                    MainForm.Instance.mnuSave.Enabled = true;
        //                    break;
        //                case DELETEMODE:
        //                    MainForm.Instance.mnuDeleteMode.Checked = true;
        //                    MainForm.Instance.mnuDeleteRecord.Enabled = true;
        //                    break;
        //                case INQUIREMODE:
        //                    MainForm.Instance.mnuInquire.Checked = true;
        //                    break;
        //                case POSTMODE:
        //                    MainForm.Instance.mnuPost.Checked = true;
        //                    MainForm.Instance.mnuAuthorise.Enabled = true;
        //                    break;
        //            }
        //        }
        //}



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
                //if (gobjActiveForm is Interface_for_Common_methods.ISearchableForm searchableForm)
                //{
                //searchableForm.UnsavedData();//for loading unsaved data
                //}
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

        public static void BringToolStripToFront(string formOptions)
        {
            // Split the formOptions into individual option codes
            string[] options = formOptions.Split(',');

            foreach (List<ToolStrip> toolbars in toolbarDictionary1.Values)
            {
                foreach (ToolStrip toolbar in toolbars)
                {
                    // Hide all toolstrips first
                    toolbar.Visible = false;
                }
            }

            string formName = gobjActiveForm.Name; // Get the Form object

            foreach (string option in options)
            {
                string mode = GetMode(gobjActiveForm);
                string key = string.IsNullOrEmpty(mode) ? formName : $"{formName}-{mode}";
                if (toolbarDictionary1.ContainsKey(key))
                {
                    List<ToolStrip> toolbars = toolbarDictionary1[key];

                    foreach (ToolStrip toolbar in toolbars)
                    {
                        toolbar.Visible = true;
                    }
                }
            }
        }


        //-------------- newst old
        ////for MAINFORM
        //public static void BringToolStripToFront(string formOptions)
        //{
        //    // Split the formOptions into individual option codes
        //    string[] options = formOptions.Split(',');

        //    foreach (ToolStrip toolbar in toolbarDictionarywith_frmnm.Values)
        //    {
        //        // Hide all toolstrips first
        //        toolbar.Visible = false;
        //    }
        //    //------old
        //    //foreach (string option in options)
        //    //{
        //    //    foreach (var kvp in toolbarDictionary)
        //    //    {
        //    //        Form form = kvp.Key;
        //    //        ToolStrip toolbar = kvp.Value;
        //    //        string formName = form.Name.Trim();
        //    //        string strFormType = formName.Length > 3 ? formName.Substring(3, 1).ToUpper() : string.Empty;

        //    //        if (strFormType == "C" && option == "C") // CheckList Form
        //    //        {
        //    //            toolbar.Visible = true;
        //    //        }
        //    //        else if (strFormType == "R" && option == "R") // Report Form
        //    //        {
        //    //            toolbar.Visible = true;
        //    //        }
        //    //        else if (options.Contains(option)) // Check if the form's options list contains the current option
        //    //        {
        //    //            toolbar.Visible = true;
        //    //        }
        //    //    }
        //    //}
        //    foreach (string option in options)
        //    {
        //        foreach (var kvp in toolbarDictionarywith_frmnm)
        //        {
        //            if (gobjActiveForm.Text.ToLower() == kvp.Key.ToString().ToLower())
        //            {
        //                string formName = gobjActiveForm.Name; // Get the Form object
        //                ToolStrip toolbar = kvp.Value;
        //                // string formName = form.Name.Trim();--old
        //                string strFormType = formName.Length > 3 ? formName.Substring(3, 1).ToUpper() : string.Empty;

        //                if (strFormType == "C" && option == "C") // CheckList Form
        //                {
        //                    toolbar.Visible = true;
        //                }
        //                else if (strFormType == "R" && option == "R") // Report Form
        //                {
        //                    toolbar.Visible = true;
        //                }
        //                else if (options.Contains(option)) // Check if the form's options list contains the current option
        //                {
        //                    toolbar.Visible = true;
        //                }
        //            }
        //        }
        //    }

        //}
        //public static void BringToolStripToFront(string formOptions)
        //{
        //    // Split the formOptions into individual option codes
        //    string[] options = formOptions.Split(',');

        //    foreach (var kvp in toolbarDictionary1)
        //    {
        //        Form form = kvp.Key;
        //        List<ToolStrip> toolbars = kvp.Value;

        //        // Hide all toolstrips for this form first
        //        foreach (ToolStrip toolbar in toolbars)
        //        {
        //            toolbar.Visible = false;
        //        }

        //        string formName = form.Name.Trim();
        //        string strFormType = formName.Length > 3 ? formName.Substring(3, 1).ToUpper() : string.Empty;

        //        foreach (string option in options)
        //        {
        //            if (strFormType == "C" && option == "C") // CheckList Form
        //            {
        //                // Set the visibility of all toolbars for this form to true
        //                foreach (ToolStrip toolbar in toolbars)
        //                {
        //                    toolbar.Visible = true;
        //                }
        //            }
        //            else if (strFormType == "R" && option == "R") // Report Form
        //            {
        //                // Set the visibility of all toolbars for this form to true
        //                foreach (ToolStrip toolbar in toolbars)
        //                {
        //                    toolbar.Visible = true;
        //                }
        //            }
        //            else if (options.Contains(option)) // Check if the form's options list contains the current option
        //            {
        //                // Set the visibility of all toolbars for this form to true
        //                foreach (ToolStrip toolbar in toolbars)
        //                {
        //                    toolbar.Visible = true;
        //                }
        //            }
        //        }
        //    }
        //}

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

        public static bool IsFieldUnique(string tableName, string fieldName, string value)
        {
            DeTools.gstrSQL = "SELECT COUNT(*) FROM "+tableName+ " WHERE "+ fieldName +" = '"+value+"'";

            // Use your database connector and OdbcDataReader to execute the query
            DbConnector dbConnector = new DbConnector();
            OdbcDataReader reader = null;

            try
            {
                dbConnector.OpenConnection();

                using (OdbcDataReader reader1 = dbConnector.CreateResultset(DeTools.gstrSQL))
                {

                    if (reader1.HasRows)
                    {
                        int count = reader1.GetInt32(0);
                        return count == 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                dbConnector.CloseConnection();
            }

            // Default to false in case of exceptions or errors
            return false;
        }

        public static bool CheckTemporaryTableExists(string tableName)
        {
            DbConnector dbConnector = new DbConnector();
            //dbConnector.connection = new OdbcConnection(dbConnector.connectionString);
            dbConnector.connection = new OdbcConnection(dbConnector.connectionString);
            using (OdbcCommand cmd = new OdbcCommand("SHOW TABLES LIKE 'temp_"+tableName+"'", dbConnector.connection))
            {
                dbConnector.connection.Open();
                object result = cmd.ExecuteScalar();
                dbConnector.connection.Close();

                if (result == null)
                {
                    CreateTemporaryTable(tableName);
                }
                    return result != null;
               

            }
        }

        public static void CreateTemporaryTable(string tableName)
        {
            DbConnector dbConnector = new DbConnector();
            dbConnector.connection = new OdbcConnection(dbConnector.connectionString);
            // Define the column name and its data type
            string columnName = "open_yn"; //for checking y/n if data oen in temp or sended to main tbl  
            string columnType = "CHAR(1)";  // Modify the data type as needed
            string columnName1 = "comp_name";
            string columnType1 = "VARCHAR(100)";  // Modify the data type as needed
            string createTableQuery = "CREATE TABLE temp_"+ tableName + " AS SELECT *, '" + columnType + "' 'N' AS " + columnName + ",'"+ columnType1+"' 'null' AS "+columnName1+" FROM " + tableName+" WHERE 1=0";
            using (OdbcCommand cmd = new OdbcCommand(createTableQuery, dbConnector.connection))
            {
                dbConnector.connection.Open();
                cmd.ExecuteNonQuery();
                dbConnector.connection.Close();
            }
        }

        public static DataTable SelectDataFromTemporaryTable(string tableName,string whereid,string wherecolumn)
        {
         
            DbConnector dbConnector = new DbConnector();
            //dbConnector.connection.Open();
            dbConnector.OpenConnection();
            string query = "SELECT * FROM temp_"+tableName+" where "+wherecolumn+" = '"+whereid+"' order by ent_on limit 1"; // Your ODBC query here

            dbConnector.CloseConnection();
            return dbConnector.ExecuteQuery(query);
        }

        //public static void InsertDataIntoMainTable(DataTable data, string mainTableName)
        //{
        //    DbConnector dbConnector = new DbConnector();
        //    dbConnector.connection = new OdbcConnection(dbConnector.connectionString);
        //    //dbConnector.connection.Open();
        //    dbConnector.OpenConnection();

        //    using (OdbcDataAdapter adapter = new OdbcDataAdapter())
        //    {
        //        adapter.SelectCommand = new OdbcCommand($"SELECT * FROM {mainTableName} WHERE 1=0", dbConnector.connection);

        //        using (OdbcCommandBuilder builder = new OdbcCommandBuilder(adapter))
        //        {
        //            // Update the data in the main table
        //            adapter.Update(data);
        //        }
        //        //dbConnector.connection.Close();
        //        dbConnector.CloseConnection();
        //    }
        //}


        //public static void InsertDataIntoMainTable(string mainTableName,string wherecol,string whereid)
        //{
        //    DbConnector dbConnector = new DbConnector();

        //    try
        //    {
        //        dbConnector.OpenConnection();

        //        // Your SQL query for inserting data from the temporary table to the main table
        //        string query = "insert into "+mainTableName+" SELECT * FROM temp_"+mainTableName+" WHERE "+wherecol+" = '"+whereid+"' ORDER BY ent_on LIMIT 1;";

        //        // Create an OdbcCommand with the query and execute it
        //        using (OdbcCommand cmd = new OdbcCommand(query, dbConnector.GetConnection()))
        //        {
        //            cmd.ExecuteNonQuery();
        //        }

        //        dbConnector.CloseConnection();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error: " + ex.Message);

        //        // Handle the exception as needed, including rolling back the transaction if required
        //    }
        //    finally
        //    {
        //        dbConnector.CloseConnection();
        //    }
        //}

        public static void InsertDataIntoMainTable(string mainTableName, string wherecol, string whereid)
        {
            MySqlConnection connection = new MySqlConnection("Server=localhost;Database=softgen_db;Uid=root;");

            try
            {
                connection.Open();

                // Your SQL query for inserting data from the temporary table to the main table
                string query = $"INSERT INTO {mainTableName}  (group_id, group_desc, active_yn, sales_tax, status, ent_on, ent_by, auth_on, auth_by, trans_status) SELECT  group_id, group_desc, active_yn, sales_tax, status, ent_on, ent_by, auth_on, auth_by, trans_status FROM temp_{mainTableName} WHERE {wherecol} = @whereid ORDER BY ent_on LIMIT 1;";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@whereid", whereid);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                // Handle the exception as needed, including rolling back the transaction if required
            }
            finally
            {
                connection.Close();
            }
        }

        /////////////////////////////////Help///////////////////
        /// <summary>
        /// ----------------old
        /// </summary>
        public static void CallHelp()
        {
            try
            {
                Control cntrl;
                bool isMainformAct = false;
                DataGridViewCell focusedcell;
               
                foreach (Form OpenForm in Application.OpenForms)
                {
                    if (OpenForm.Name == "MainForm")
                    {
                        isMainformAct = true;
                        break;
                    }
                }

                if (isMainformAct)
                {
                    frmHelp frmHelp = new frmHelp();

                    if (Help.dgvCellToHelpTopicMapping.Count > 0 && gobjActiveForm.ActiveControl is DataGridView)
                    {
                        dgv = (DataGridView)gobjActiveForm.ActiveControl;
                        Help.o_form = gobjActiveForm;
                        Help.s_Mode = GetMode(gobjActiveForm);

                        // Get the current cell
                        dgvCell = dgv.CurrentCell;

                        if (dgvCell != null)
                        {
                            Tuple<DataGridView, int, int> key = Tuple.Create(dgv, dgvCell.RowIndex, dgvCell.ColumnIndex);

                            if (Help.dgvCellToHelpTopicMapping.TryGetValue(key, out var helpTopic))

                            {
                                //Help.i_Help_id = Int32.Parse(helpTopic);

                                //    // Handle the help topic as needed
                                //    switch (Help.i_Help_id)
                                //    {
                                //        case 0:
                                //            // Currently Help is not available
                                //            frmHelp.pnlText.Text = "Currently Help is not available.";
                                //            frmHelp.pnlText.Visible = true;
                                //            break;

                                //        case var _ when Help.i_Help_id < 3000:
                                //            // In add mode, don't display the grid help.
                                //            if (Help.s_Mode == ADDMODE)
                                //            {
                                //                Help.PrepareGridHelp();
                                //            }
                                //            else
                                //            {
                                //                Help.PrepareGridHelp();
                                //            }
                                //            break;

                                //        case var _ when Help.i_Help_id > 9000:
                                //            // These helps are for a substitute for combo help.
                                //            Help.PrepareGridHelp();
                                //            break;

                                //        case var _ when Help.i_Help_id > 3000:
                                //            Help.PrepareTextHelp();
                                //            break;
                                //    }
                                //    frmHelp.pnlToptxt.Text = DeTools.RestoreCaption(gobjActiveForm) + " (" + Help.i_NoOfRecords.ToString().Trim() + ")";
                                //    frmHelp.pnlToptxt.Tag = DeTools.RestoreCaption(gobjActiveForm);
                                //    Help.FillFields(frmHelp.cboFields, frmHelp.cboFieldsId);
                                //    Help.FillFields(frmHelp.cboOrder, frmHelp.cboOrderId);
                                //    frmHelp.Show();

                                //    switch (Help.i_Help_id)
                                //    {
                                //        case 9001:
                                //            frmHelp.cboDataType.Items.Add(1);
                                //            frmHelp.cboFields.Items.Add("Item Description");
                                //            break;
                                //        case 9002:
                                //            frmHelp.cboDataType.Items.Add(1);
                                //            frmHelp.cboFields.Items.Add("Customer Name");
                                //            break;
                                //        case 9005:
                                //            frmHelp.cboDataType.Items.Add(1);
                                //            frmHelp.cboFields.Items.Add("Supplier Name");
                                //            break;
                                //        case 9010:
                                //            frmHelp.cboDataType.Items.Add(1);
                                //            frmHelp.cboFields.Items.Add("Item Description");
                                //            break;
                                //        case 9011:
                                //            frmHelp.cboDataType.Items.Add(1);
                                //            frmHelp.cboFields.Items.Add("Item Description");
                                //            break;
                                //        case 1005:
                                //            frmHelp.cboDataType.Items.Add(1);
                                //            frmHelp.cboFields.Items.Add("Customer Name");
                                //            break;
                                //        case 1006:
                                //            frmHelp.cboDataType.Items.Add(1);
                                //            frmHelp.cboFields.Items.Add("Item Name");
                                //            break;
                                //        case 1013:
                                //            frmHelp.cboDataType.Items.Add(1);
                                //            frmHelp.cboFields.Items.Add("Supplier Name");
                                //            break;
                                //        case 9014:
                                //            frmHelp.cboDataType.Items.Add(1);
                                //            frmHelp.cboFields.Items.Add("Description");
                                //            break;
                                //        case 9018:
                                //            frmHelp.cboDataType.Items.Add(1);
                                //            frmHelp.cboFields.Items.Add("Description");
                                //            break;
                                //        case 9006:
                                //            frmHelp.cboDataType.Items.Add(1);
                                //            frmHelp.cboFields.Items.Add("Description");
                                //            break;
                                //    }
                                //}
                                mobjActiveControl = gobjActiveForm.ActiveControl;
                                Help.DisplayHelp(gobjActiveForm, (int)Keys.F1, mobjActiveControl);
                            }
                        }
                    }

                    else if (toolbarDictionary1.Count <= 1)
                         {
                            Messages.ErrorMsg("Help Not Ready for Menu.");

                         }
                    else
                    {
                      
                       mobjActiveControl = gobjActiveForm.ActiveControl;
                       Help.DisplayHelp(gobjActiveForm, (int)Keys.F1, mobjActiveControl);
                    
                    }
                }

            }
            catch (Exception ex)
            {
                if (ex is COMException && ((COMException)ex).ErrorCode == 91)
                {
                    frmHelp frmhelp = new frmHelp();

                    frmhelp.pnlToptxt.Text = RestoreCaption(gobjActiveForm);
                    Messages.gstrMsg = "Help cannot be opened for non-active fields.";
                    Messages.gstrMsg += " Clear the form for reactivating the fields.";
                    frmhelp.pnlText.Text = Messages.gstrMsg;
                    frmhelp.pnlText.Visible = true;
                    frmhelp.Show();
                }
                else if (ex is COMException && ((COMException)ex).ErrorCode == 438)
                {
                    // HelpContextId property not supported.
                    return;
                }
                else
                    {
                    Messages msg = new Messages();
                    msg.VBError(ex, "DeTools", "CallHelp", null);
                }
            }
        }

        //public static void CallHelp()
        //{
        //    try
        //    {
        //        if (gobjActiveForm == null)
        //        {
        //            Messages.ErrorMsg("No active form available.");
        //            return;
        //        }

        //        if (toolbarDictionary1.TryGetValue(gobjActiveForm, out List<ToolStrip> toolbars))
        //        {
        //            //Ensure there are at least two toolbars(MainForm + a specific form's toolbar)
        //            if (toolbars.Count >= 2)
        //            {
        //                mobjActiveControl = gobjActiveForm.ActiveControl;
        //                Help.DisplayHelp(gobjActiveForm, (int)Keys.F1, mobjActiveControl);
        //            }
        //            else
        //            {
        //                Messages.ErrorMsg("Help Not Ready for Menu.");
        //            }
        //        }
        //        else
        //        {
        //            Messages.ErrorMsg("No toolbar found for the active form.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex is COMException comEx && comEx.ErrorCode == 91)
        //        {
        //            frmHelp frmhelp = new frmHelp();

        //            frmhelp.pnlToptxt.Text = RestoreCaption(gobjActiveForm);
        //            Messages.gstrMsg = "Help cannot be opened for non-active fields.";
        //            Messages.gstrMsg += " Clear the form for reactivating the fields.";
        //            frmhelp.pnlText.Text = Messages.gstrMsg;
        //            frmhelp.pnlText.Visible = true;
        //            frmhelp.Show();
        //        }
        //        else if (ex is COMException comEx1 && comEx1.ErrorCode == 438)
        //        {
        //            //HelpContextId property not supported.
        //            return;
        //        }
        //        else
        //        {
        //            Messages msg = new Messages();
        //            msg.VBError(ex, "DeTools", "CallHelp", null);
        //        }
        //    }
        //}




        public static void SwitchMode(Form form, string strAppMode)
        {
            // Retrieve the current form's name
            string formName = form.Name;

            // Determine the key for the toolbar dictionary
            string currentMode = GetMode(form);
            currentKey = string.IsNullOrEmpty(currentMode) ? formName : $"{formName}-{currentMode}";

            SetModeCaption(gobjActiveForm, strAppMode);
            string getmode = GetMode(gobjActiveForm);
            // Create a new key for the selected mode
            newKey = $"{formName}-{getmode}";

            // Check if the current mode and the new mode are the same; no need to switch
            if (currentKey == newKey)
            {
                return;
            }

            if (toolbarDictionary1.ContainsKey(currentKey))
            {
                // Get the list of ToolStrip objects from the current dictionary
                List<ToolStrip> toolstrips = toolbarDictionary1[currentKey];

                if (!newToolbarDictionary.ContainsKey(newKey))
                {
                    // If the new dictionary doesn't contain the new mode's key yet, create a new list of ToolStrip
                    newToolbarDictionary[newKey] = new List<ToolStrip>();
                }

                // Copy the ToolStrip objects to the new dictionary
                newToolbarDictionary[newKey].AddRange(toolstrips);

                // Create a new ToolStrip for the gobjActiveForm
                ToolStrip newToolbar = new ToolStrip();

                // Clear the items from the current mode's ToolStrip
                toolbarDictionary1.Remove(currentKey);

                // Check how many items are in the newToolStrip
                int itemCountInNewToolbar = newToolbar.Items.Count;
                // itemCountInNewToolbar now contains the number of items in the newToolStrip

                // Add the new ToolStrip to the old dictionary
                if (!toolbarDictionary1.ContainsKey(newKey))
                {
                    // If the key doesn't exist, create it and associate it with a new ToolStrip
                    toolbarDictionary1[newKey] = new List<ToolStrip>();
                }

                toolbarDictionary1[newKey].Add(newToolbar);
            
            }

        }




    }//End For Static Class DETOOLS'



}
