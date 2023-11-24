using System.Resources;
using System.Windows.Forms;

namespace softgen
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            tableLayoutPanel1 = new TableLayoutPanel();
            label2 = new Label();
            pnlDate = new Label();
            pnlLoginTime = new Label();
            label4 = new Label();
            pnlUserName = new Label();
            label3 = new Label();
            fileToolStripMenuItem = new ToolStripMenuItem();
            mnuLock = new ToolStripMenuItem();
            mnuAdd = new ToolStripMenuItem();
            mnuModify = new ToolStripMenuItem();
            mnuDeleteMode = new ToolStripMenuItem();
            mnuInquire = new ToolStripMenuItem();
            mnuPost = new ToolStripMenuItem();
            mnuHelp = new ToolStripMenuItem();
            mnuRefresh = new ToolStripMenuItem();
            mnuRetrieve = new ToolStripMenuItem();
            mnuSave = new ToolStripMenuItem();
            mnuDeleteRecord = new ToolStripMenuItem();
            mnuPrint = new ToolStripMenuItem();
            mnuAuthorise = new ToolStripMenuItem();
            masterToolStripMenuItem = new ToolStripMenuItem();
            Mgroupmenu = new ToolStripMenuItem();
            MItemmenu = new ToolStripMenuItem();
            documentSeriesToolStripMenuItem = new ToolStripMenuItem();
            documentTypeToolStripMenuItem = new ToolStripMenuItem();
            transactionToolStripMenuItem = new ToolStripMenuItem();
            Tbillmodule = new ToolStripMenuItem();
            TInvGenmenu = new ToolStripMenuItem();
            reportToolStripMenuItem = new ToolStripMenuItem();
            utilitiesToolStripMenuItem = new ToolStripMenuItem();
            windowsToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1 = new MenuStrip();
            statusStrip1 = new StatusStrip();
            panel = new Panel();
            pnlPosted_date = new Label();
            pnlPosted_by = new Label();
            pnlCreated_date = new Label();
            pnlHelp = new Label();
            panel1 = new Panel();
            linkLabel1 = new LinkLabel();
            pnlCreated_by = new Label();
            imageList1 = new ImageList(components);
            tmrActiveForm = new System.Windows.Forms.Timer(components);
            mnuMSGroup = new ToolStripMenuItem();
            tableLayoutPanel1.SuspendLayout();
            menuStrip1.SuspendLayout();
            panel.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.FromArgb(192, 192, 255);
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;
            tableLayoutPanel1.ColumnCount = 7;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666718F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(label2, 2, 0);
            tableLayoutPanel1.Controls.Add(pnlDate, 3, 0);
            tableLayoutPanel1.Controls.Add(pnlLoginTime, 5, 0);
            tableLayoutPanel1.Controls.Add(label4, 4, 0);
            tableLayoutPanel1.Controls.Add(pnlUserName, 1, 0);
            tableLayoutPanel1.Controls.Add(label3, 0, 0);
            tableLayoutPanel1.Location = new Point(2, 693);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1207, 21);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // label2
            // 
            label2.Location = new Point(399, 2);
            label2.Name = "label2";
            label2.Size = new Size(148, 15);
            label2.TabIndex = 1;
            label2.Text = "Date:";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // pnlDate
            // 
            pnlDate.Location = new Point(596, 2);
            pnlDate.Name = "pnlDate";
            pnlDate.Size = new Size(132, 15);
            pnlDate.TabIndex = 0;
            pnlDate.Click += pnlDate_Click;
            // 
            // pnlLoginTime
            // 
            pnlLoginTime.Location = new Point(990, 2);
            pnlLoginTime.Name = "pnlLoginTime";
            pnlLoginTime.Size = new Size(136, 15);
            pnlLoginTime.TabIndex = 3;
            // 
            // label4
            // 
            label4.Location = new Point(793, 2);
            label4.Name = "label4";
            label4.Size = new Size(164, 15);
            label4.TabIndex = 4;
            label4.Text = "Login Time";
            label4.TextAlign = ContentAlignment.TopRight;
            // 
            // pnlUserName
            // 
            pnlUserName.Location = new Point(202, 2);
            pnlUserName.Name = "pnlUserName";
            pnlUserName.Size = new Size(132, 15);
            pnlUserName.TabIndex = 2;
            // 
            // label3
            // 
            label3.Location = new Point(5, 2);
            label3.Name = "label3";
            label3.Size = new Size(148, 15);
            label3.TabIndex = 5;
            label3.Text = "User:";
            label3.TextAlign = ContentAlignment.TopRight;
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.BackColor = Color.Transparent;
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { mnuLock, mnuAdd, mnuModify, mnuDeleteMode, mnuInquire, mnuPost, mnuHelp, mnuRefresh, mnuRetrieve, mnuSave, mnuDeleteRecord, mnuPrint, mnuAuthorise });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // mnuLock
            // 
            mnuLock.BackColor = Color.Transparent;
            mnuLock.Name = "mnuLock";
            mnuLock.ShortcutKeys = Keys.F8;
            mnuLock.Size = new Size(197, 22);
            mnuLock.Text = "Lock Screen";
            // 
            // mnuAdd
            // 
            mnuAdd.BackColor = Color.Transparent;
            mnuAdd.Name = "mnuAdd";
            mnuAdd.ShortcutKeys = Keys.Control | Keys.A;
            mnuAdd.Size = new Size(197, 22);
            mnuAdd.Text = "Add Record";
            // 
            // mnuModify
            // 
            mnuModify.BackColor = Color.Transparent;
            mnuModify.Name = "mnuModify";
            mnuModify.ShortcutKeys = Keys.Control | Keys.M;
            mnuModify.Size = new Size(197, 22);
            mnuModify.Text = "Modify Record";
            // 
            // mnuDeleteMode
            // 
            mnuDeleteMode.BackColor = Color.Transparent;
            mnuDeleteMode.Name = "mnuDeleteMode";
            mnuDeleteMode.Size = new Size(197, 22);
            mnuDeleteMode.Text = "Delete Record";
            mnuDeleteMode.Click += mnudelete_Click;
            // 
            // mnuInquire
            // 
            mnuInquire.BackColor = Color.Transparent;
            mnuInquire.Name = "mnuInquire";
            mnuInquire.ShortcutKeyDisplayString = "";
            mnuInquire.ShortcutKeys = Keys.Control | Keys.I;
            mnuInquire.Size = new Size(197, 22);
            mnuInquire.Text = "Inquire Record";
            // 
            // mnuPost
            // 
            mnuPost.BackColor = Color.Transparent;
            mnuPost.Name = "mnuPost";
            mnuPost.ShortcutKeys = Keys.Control | Keys.O;
            mnuPost.Size = new Size(197, 22);
            mnuPost.Text = "Posting Record";
            // 
            // mnuHelp
            // 
            mnuHelp.BackColor = Color.Transparent;
            mnuHelp.Name = "mnuHelp";
            mnuHelp.ShortcutKeys = Keys.F1;
            mnuHelp.Size = new Size(197, 22);
            mnuHelp.Text = "MasterHelp";
            // 
            // mnuRefresh
            // 
            mnuRefresh.Name = "mnuRefresh";
            mnuRefresh.ShortcutKeys = Keys.Control | Keys.F;
            mnuRefresh.Size = new Size(197, 22);
            mnuRefresh.Text = "Refresh";
            mnuRefresh.Click += masterHelpToolStripMenuItem_Click;
            // 
            // mnuRetrieve
            // 
            mnuRetrieve.Name = "mnuRetrieve";
            mnuRetrieve.Size = new Size(197, 22);
            mnuRetrieve.Text = "Retrieve";
            // 
            // mnuSave
            // 
            mnuSave.BackColor = Color.Transparent;
            mnuSave.Name = "mnuSave";
            mnuSave.ShortcutKeys = Keys.Control | Keys.S;
            mnuSave.Size = new Size(197, 22);
            mnuSave.Text = "Save Record";
            // 
            // mnuDeleteRecord
            // 
            mnuDeleteRecord.BackColor = Color.Transparent;
            mnuDeleteRecord.Name = "mnuDeleteRecord";
            mnuDeleteRecord.ShortcutKeys = Keys.Control | Keys.D;
            mnuDeleteRecord.Size = new Size(197, 22);
            mnuDeleteRecord.Text = "Delete Record";
            // 
            // mnuPrint
            // 
            mnuPrint.Name = "mnuPrint";
            mnuPrint.ShortcutKeys = Keys.Control | Keys.P;
            mnuPrint.Size = new Size(197, 22);
            mnuPrint.Text = "Print Record";
            // 
            // mnuAuthorise
            // 
            mnuAuthorise.BackColor = Color.Transparent;
            mnuAuthorise.Name = "mnuAuthorise";
            mnuAuthorise.ShortcutKeys = Keys.Control | Keys.U;
            mnuAuthorise.Size = new Size(197, 22);
            mnuAuthorise.Text = "Lock Record";
            mnuAuthorise.Click += mnuAuthorise_Click;
            // 
            // masterToolStripMenuItem
            // 
            masterToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { MItemmenu, documentSeriesToolStripMenuItem, documentTypeToolStripMenuItem, Mgroupmenu, mnuMSGroup });
            masterToolStripMenuItem.Name = "masterToolStripMenuItem";
            masterToolStripMenuItem.Size = new Size(55, 20);
            masterToolStripMenuItem.Text = "Master";
            // 
            // Mgroupmenu
            // 
            Mgroupmenu.Name = "Mgroupmenu";
            Mgroupmenu.Size = new Size(180, 22);
            Mgroupmenu.Text = "Group";
            Mgroupmenu.Click += groupToolStripMenuItem_Click;
            // 
            // MItemmenu
            // 
            MItemmenu.Name = "MItemmenu";
            MItemmenu.Size = new Size(180, 22);
            MItemmenu.Text = "Item";
            MItemmenu.Click += MItemmenu_Click;
            // 
            // documentSeriesToolStripMenuItem
            // 
            documentSeriesToolStripMenuItem.Name = "documentSeriesToolStripMenuItem";
            documentSeriesToolStripMenuItem.Size = new Size(180, 22);
            documentSeriesToolStripMenuItem.Text = "Document Series";
            // 
            // documentTypeToolStripMenuItem
            // 
            documentTypeToolStripMenuItem.Name = "documentTypeToolStripMenuItem";
            documentTypeToolStripMenuItem.Size = new Size(180, 22);
            documentTypeToolStripMenuItem.Text = "Document Type";
            // 
            // transactionToolStripMenuItem
            // 
            transactionToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { Tbillmodule });
            transactionToolStripMenuItem.Name = "transactionToolStripMenuItem";
            transactionToolStripMenuItem.Size = new Size(79, 20);
            transactionToolStripMenuItem.Text = "Transaction";
            // 
            // Tbillmodule
            // 
            Tbillmodule.DropDownItems.AddRange(new ToolStripItem[] { TInvGenmenu });
            Tbillmodule.Name = "Tbillmodule";
            Tbillmodule.Size = new Size(151, 22);
            Tbillmodule.Text = "Billing Module";
            // 
            // TInvGenmenu
            // 
            TInvGenmenu.Name = "TInvGenmenu";
            TInvGenmenu.Size = new Size(173, 22);
            TInvGenmenu.Text = "Invoice Generation";
            TInvGenmenu.Click += invoiceGenerationToolStripMenuItem_Click;
            // 
            // reportToolStripMenuItem
            // 
            reportToolStripMenuItem.Name = "reportToolStripMenuItem";
            reportToolStripMenuItem.Size = new Size(54, 20);
            reportToolStripMenuItem.Text = "Report";
            // 
            // utilitiesToolStripMenuItem
            // 
            utilitiesToolStripMenuItem.Name = "utilitiesToolStripMenuItem";
            utilitiesToolStripMenuItem.Size = new Size(58, 20);
            utilitiesToolStripMenuItem.Text = "Utilities";
            // 
            // windowsToolStripMenuItem
            // 
            windowsToolStripMenuItem.Name = "windowsToolStripMenuItem";
            windowsToolStripMenuItem.Size = new Size(68, 20);
            windowsToolStripMenuItem.Text = "Windows";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = Color.AntiqueWhite;
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, masterToolStripMenuItem, transactionToolStripMenuItem, reportToolStripMenuItem, utilitiesToolStripMenuItem, windowsToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1199, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "Main_menu";
            menuStrip1.ItemClicked += menuStrip1_ItemClicked;
            // 
            // statusStrip1
            // 
            statusStrip1.AutoSize = false;
            statusStrip1.Location = new Point(0, 610);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1199, 97);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // panel
            // 
            panel.BackColor = Color.AntiqueWhite;
            panel.Controls.Add(pnlPosted_date);
            panel.Controls.Add(pnlPosted_by);
            panel.Controls.Add(pnlCreated_date);
            panel.Controls.Add(pnlHelp);
            panel.Controls.Add(panel1);
            panel.Controls.Add(pnlCreated_by);
            panel.Location = new Point(0, 616);
            panel.Name = "panel";
            panel.Size = new Size(1380, 90);
            panel.TabIndex = 10;
            panel.Paint += panel_Paint;
            // 
            // pnlPosted_date
            // 
            pnlPosted_date.AutoSize = true;
            pnlPosted_date.Location = new Point(1089, 14);
            pnlPosted_date.Name = "pnlPosted_date";
            pnlPosted_date.Size = new Size(0, 15);
            pnlPosted_date.TabIndex = 11;
            pnlPosted_date.Visible = false;
            // 
            // pnlPosted_by
            // 
            pnlPosted_by.AutoSize = true;
            pnlPosted_by.Location = new Point(1013, 14);
            pnlPosted_by.Name = "pnlPosted_by";
            pnlPosted_by.Size = new Size(0, 15);
            pnlPosted_by.TabIndex = 0;
            pnlPosted_by.Visible = false;
            // 
            // pnlCreated_date
            // 
            pnlCreated_date.AutoSize = true;
            pnlCreated_date.Location = new Point(682, 54);
            pnlCreated_date.Name = "pnlCreated_date";
            pnlCreated_date.Size = new Size(0, 15);
            pnlCreated_date.TabIndex = 10;
            pnlCreated_date.Visible = false;
            // 
            // pnlHelp
            // 
            pnlHelp.BackColor = Color.Silver;
            pnlHelp.Location = new Point(470, 59);
            pnlHelp.Name = "pnlHelp";
            pnlHelp.Size = new Size(496, 16);
            pnlHelp.TabIndex = 8;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Gainsboro;
            panel1.Controls.Add(linkLabel1);
            panel1.Location = new Point(690, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(260, 50);
            panel1.TabIndex = 2;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.BackColor = Color.Transparent;
            linkLabel1.Font = new Font("Times New Roman", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            linkLabel1.LinkColor = Color.Crimson;
            linkLabel1.Location = new Point(27, 15);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(207, 15);
            linkLabel1.TabIndex = 5;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "SOFTGEN SYSTEMS (9810256984)";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // pnlCreated_by
            // 
            pnlCreated_by.AutoSize = true;
            pnlCreated_by.Location = new Point(609, 54);
            pnlCreated_by.Name = "pnlCreated_by";
            pnlCreated_by.Size = new Size(0, 15);
            pnlCreated_by.TabIndex = 9;
            pnlCreated_by.Visible = false;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth8Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.IndianRed;
            imageList1.Images.SetKeyName(0, "Add");
            imageList1.Images.SetKeyName(1, "Modify");
            imageList1.Images.SetKeyName(2, "Delete");
            imageList1.Images.SetKeyName(3, "Inquire");
            imageList1.Images.SetKeyName(4, "Post");
            imageList1.Images.SetKeyName(5, "Fresh");
            imageList1.Images.SetKeyName(6, "Help");
            imageList1.Images.SetKeyName(7, "Quit");
            imageList1.Images.SetKeyName(8, "Save");
            imageList1.Images.SetKeyName(9, "Authorization");
            imageList1.Images.SetKeyName(10, "Print");
            imageList1.Images.SetKeyName(11, "Retrieve");
            imageList1.Images.SetKeyName(12, "Continue");
            imageList1.Images.SetKeyName(13, "DeleteMode");
            // 
            // tmrActiveForm
            // 
            tmrActiveForm.Enabled = true;
            tmrActiveForm.Tick += tmrActiveForm_Tick;
            // 
            // mnuMSGroup
            // 
            mnuMSGroup.Name = "mnuMSGroup";
            mnuMSGroup.Size = new Size(180, 22);
            mnuMSGroup.Text = "Sub Group";
            mnuMSGroup.Click += mnuMSGroup_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.MediumTurquoise;
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(1199, 707);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(panel);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Font = new Font("Times New Roman", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            MainMenuStrip = menuStrip1;
            MdiChildrenMinimizedAnchorBottom = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ABC";
            TransparencyKey = Color.PowderBlue;
            WindowState = FormWindowState.Maximized;
            Load += MainForm_Load_1;
            tableLayoutPanel1.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel.ResumeLayout(false);
            panel.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion
        private TableLayoutPanel tableLayoutPanel1;
        public Panel dashpanel;
        private Button button1;
        private Button button2;
        public Panel mainpanel;
        private Button Mainhelpbtn;
        private Button Mainquitbtn;
        private Button delbtn;
        private Button inqbtn;
        private Button postbtn;
        private Button addbtn;
        private Button modifybtn;
        public Panel formpanel;
        private Button dltbtn;
        private Button savebtn;
        private Button freshtbn;
        private Button rtrvbtn;
        private Button helpbtn;
        private Button quitbtn;
        private Button printbtn;
        private Button button10;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem masterToolStripMenuItem;
        public ToolStripMenuItem Mgroupmenu;
        private ToolStripMenuItem documentSeriesToolStripMenuItem;
        private ToolStripMenuItem documentTypeToolStripMenuItem;
        private ToolStripMenuItem transactionToolStripMenuItem;
        private ToolStripMenuItem reportToolStripMenuItem;
        private ToolStripMenuItem utilitiesToolStripMenuItem;
        private ToolStripMenuItem windowsToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        public MenuStrip menuStrip1;
        private StatusStrip statusStrip1;
        private Panel panel;
        public ToolStrip tbrTools1;
        private Panel panel1;
        private LinkLabel linkLabel1;
        public ToolStripButton btnAdd;
        public ToolStripButton btnModify;
        public ToolStripButton btnMDelete;
        public ToolStripButton btnInquire;
        public ToolStripButton btnPost;
        public ToolStripButton btnQuit;
        public ToolStripButton btnHelp;
        public ToolStripButton btnSave;
        public ToolStripButton btnDelete;
        public ToolStripButton btnFresh;
        public ToolStripButton btnRetrieve;
        public ToolStripButton btnPrint;
        public ToolStripButton btnAuth;
        public ToolStripMenuItem mnuAdd;
        public ToolStripMenuItem mnuModify;
        public ToolStripMenuItem mnuLock;
        public ToolStripMenuItem mnuDeleteMode;
        public ToolStripMenuItem mnuInquire;
        public ToolStripMenuItem mnuPost;
        public ToolStripMenuItem mnuHelp;
        public ImageList imageList1;
        public ToolStripMenuItem mnuDeleteRecord;
        public ToolStripMenuItem mnuSave;
        public ToolStripMenuItem mnuAuthorise;
        public ToolStripMenuItem mnuRefresh;
        public ToolStripMenuItem mnuRetrieve;
        public ToolStripMenuItem mnuPrint;
        private Label pnlDate;
        private Label label2;
        private Label pnlLoginTime;
        private Label label4;
        private Label label3;
        private System.Windows.Forms.Timer tmrActiveForm;
        public Label pnlHelp;
        public Label pnlCreated_by;
        public Label pnlCreated_date;
        public Label pnlPosted_date;
        public Label pnlPosted_by;
        public Label pnlUserName;
        public ToolStripMenuItem MItemmenu;
        private ToolStripMenuItem Tbillmodule;
        private ToolStripMenuItem TInvGenmenu;
        private ToolStripMenuItem mnuMSGroup;
    }
}