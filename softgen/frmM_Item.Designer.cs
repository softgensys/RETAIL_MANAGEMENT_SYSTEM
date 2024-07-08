namespace softgen
{
    partial class frmM_Item
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblStock = new Label();
            txtDisc = new TextBox();
            cboLoc = new ComboBox();
            lblLoc = new Label();
            chkBarYN = new CheckBox();
            lblBarYN = new Label();
            chkAct = new CheckBox();
            lblAct = new Label();
            txtCess = new TextBox();
            lblCess = new Label();
            cboSaleTax = new ComboBox();
            lblSaleTax = new Label();
            txtHSN = new TextBox();
            lblHSN = new Label();
            txtDecimalupto = new TextBox();
            lblDecimalupto = new Label();
            chkDecimal = new CheckBox();
            lblDecimal = new Label();
            txtLT = new TextBox();
            lblLT = new Label();
            txtexisper = new TextBox();
            lblExcis = new Label();
            txtStyle = new TextBox();
            lblStyle = new Label();
            txtMaxLevel = new TextBox();
            lblMaxLevel = new Label();
            txtReOLevel = new TextBox();
            lblReOLevel = new Label();
            txtMinLevel = new TextBox();
            lblMinLevel = new Label();
            lblSaleUnit = new Label();
            cboColorDesc = new ComboBox();
            lblColorDesc = new Label();
            cboManuf = new ComboBox();
            lblManuf = new Label();
            cboSSGroupDesc = new ComboBox();
            lblSSGroupDesc = new Label();
            cboGroup = new ComboBox();
            lblGroup = new Label();
            lblType = new Label();
            txtItemDesc = new TextBox();
            lblItemDesc = new Label();
            txtItemId = new TextBox();
            lblItemId = new Label();
            rotGroup = new Label();
            rotSSGroupDesc = new Label();
            rotColorDesc = new Label();
            rotManuf = new Label();
            rotSaleUnit = new Label();
            rotSaleTax = new Label();
            rotLoc = new Label();
            rotStock = new Label();
            cboSaleUnit = new ComboBox();
            panel3 = new Panel();
            dbgBarDet = new DataGridView();
            PLU = new DataGridViewTextBoxColumn();
            BARCODE = new DataGridViewTextBoxColumn();
            cp = new DataGridViewTextBoxColumn();
            MRP = new DataGridViewTextBoxColumn();
            salepric = new DataGridViewTextBoxColumn();
            netrate = new DataGridViewTextBoxColumn();
            margin_per = new DataGridViewTextBoxColumn();
            ACTIVE = new DataGridViewCheckBoxColumn();
            label25 = new Label();
            chkNodisc = new CheckBox();
            label1 = new Label();
            rotNetRate = new Label();
            cboType = new ComboBox();
            rotType = new Label();
            rotSGroup = new Label();
            lblSubGroup = new Label();
            cboSGroup = new ComboBox();
            lblOpBal = new Label();
            txtConv = new TextBox();
            lblConv = new Label();
            txtOpBal = new TextBox();
            lblPurUnit = new Label();
            rotPurUnit = new Label();
            cboPurUnit = new ComboBox();
            textBox3 = new TextBox();
            lblSizeDesc = new Label();
            cboSizeDesc = new ComboBox();
            rotSizeDesc = new Label();
            lblShDesc = new Label();
            txtShDesc = new TextBox();
            lblDisc = new Label();
            ((System.ComponentModel.ISupportInitialize)dbgBarDet).BeginInit();
            SuspendLayout();
            // 
            // lblStock
            // 
            lblStock.AutoSize = true;
            lblStock.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblStock.ImeMode = ImeMode.NoControl;
            lblStock.Location = new Point(538, 260);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(39, 15);
            lblStock.TabIndex = 150;
            lblStock.Text = "Stock";
            // 
            // txtDisc
            // 
            txtDisc.Enabled = false;
            txtDisc.Location = new Point(470, 254);
            txtDisc.Name = "txtDisc";
            txtDisc.Size = new Size(50, 23);
            txtDisc.TabIndex = 29;
            // 
            // cboLoc
            // 
            cboLoc.DropDownHeight = 60;
            cboLoc.Enabled = false;
            cboLoc.FormattingEnabled = true;
            cboLoc.IntegralHeight = false;
            cboLoc.Location = new Point(268, 254);
            cboLoc.Name = "cboLoc";
            cboLoc.Size = new Size(61, 23);
            cboLoc.TabIndex = 28;
            // 
            // lblLoc
            // 
            lblLoc.AutoSize = true;
            lblLoc.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblLoc.ImeMode = ImeMode.NoControl;
            lblLoc.Location = new Point(221, 258);
            lblLoc.Name = "lblLoc";
            lblLoc.Size = new Size(40, 15);
            lblLoc.TabIndex = 145;
            lblLoc.Text = "Loc Id";
            // 
            // chkBarYN
            // 
            chkBarYN.AutoSize = true;
            chkBarYN.Checked = true;
            chkBarYN.CheckState = CheckState.Checked;
            chkBarYN.Enabled = false;
            chkBarYN.Location = new Point(198, 257);
            chkBarYN.Name = "chkBarYN";
            chkBarYN.Size = new Size(15, 14);
            chkBarYN.TabIndex = 27;
            chkBarYN.UseVisualStyleBackColor = true;
            // 
            // lblBarYN
            // 
            lblBarYN.AutoSize = true;
            lblBarYN.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblBarYN.ImeMode = ImeMode.NoControl;
            lblBarYN.Location = new Point(77, 256);
            lblBarYN.Name = "lblBarYN";
            lblBarYN.Size = new Size(117, 15);
            lblBarYN.TabIndex = 143;
            lblBarYN.Text = "Bar Code Applicable";
            // 
            // chkAct
            // 
            chkAct.AutoSize = true;
            chkAct.Checked = true;
            chkAct.CheckState = CheckState.Checked;
            chkAct.Enabled = false;
            chkAct.Location = new Point(57, 256);
            chkAct.Name = "chkAct";
            chkAct.Size = new Size(15, 14);
            chkAct.TabIndex = 26;
            chkAct.UseVisualStyleBackColor = true;
            // 
            // lblAct
            // 
            lblAct.AutoSize = true;
            lblAct.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblAct.ImeMode = ImeMode.NoControl;
            lblAct.Location = new Point(8, 255);
            lblAct.Name = "lblAct";
            lblAct.Size = new Size(48, 15);
            lblAct.TabIndex = 141;
            lblAct.Text = "Active*";
            // 
            // txtCess
            // 
            txtCess.Enabled = false;
            txtCess.Location = new Point(684, 225);
            txtCess.Name = "txtCess";
            txtCess.Size = new Size(53, 23);
            txtCess.TabIndex = 25;
            // 
            // lblCess
            // 
            lblCess.AutoSize = true;
            lblCess.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblCess.ImeMode = ImeMode.NoControl;
            lblCess.Location = new Point(639, 228);
            lblCess.Name = "lblCess";
            lblCess.Size = new Size(41, 15);
            lblCess.TabIndex = 139;
            lblCess.Text = "Cess%";
            // 
            // cboSaleTax
            // 
            cboSaleTax.DropDownHeight = 100;
            cboSaleTax.Enabled = false;
            cboSaleTax.FormattingEnabled = true;
            cboSaleTax.IntegralHeight = false;
            cboSaleTax.Location = new Point(457, 224);
            cboSaleTax.Name = "cboSaleTax";
            cboSaleTax.Size = new Size(88, 23);
            cboSaleTax.TabIndex = 24;
            cboSaleTax.DropDown += cboSaleTax_DropDown;
            cboSaleTax.SelectedIndexChanged += cboSaleTax_SelectedIndexChanged;
            cboSaleTax.KeyUp += cboSaleTax_KeyUp;
            cboSaleTax.MouseDown += gstperccomb_MouseDown;
            // 
            // lblSaleTax
            // 
            lblSaleTax.AutoSize = true;
            lblSaleTax.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblSaleTax.ImeMode = ImeMode.NoControl;
            lblSaleTax.Location = new Point(403, 228);
            lblSaleTax.Name = "lblSaleTax";
            lblSaleTax.Size = new Size(56, 15);
            lblSaleTax.TabIndex = 136;
            lblSaleTax.Text = "GST (%)*";
            // 
            // txtHSN
            // 
            txtHSN.Enabled = false;
            txtHSN.Location = new Point(312, 224);
            txtHSN.Name = "txtHSN";
            txtHSN.Size = new Size(85, 23);
            txtHSN.TabIndex = 23;
            // 
            // lblHSN
            // 
            lblHSN.AutoSize = true;
            lblHSN.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblHSN.ImeMode = ImeMode.NoControl;
            lblHSN.Location = new Point(275, 228);
            lblHSN.Name = "lblHSN";
            lblHSN.Size = new Size(32, 15);
            lblHSN.TabIndex = 134;
            lblHSN.Text = "HSN";
            // 
            // txtDecimalupto
            // 
            txtDecimalupto.Enabled = false;
            txtDecimalupto.Location = new Point(243, 224);
            txtDecimalupto.Name = "txtDecimalupto";
            txtDecimalupto.Size = new Size(24, 23);
            txtDecimalupto.TabIndex = 22;
            // 
            // lblDecimalupto
            // 
            lblDecimalupto.AutoSize = true;
            lblDecimalupto.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblDecimalupto.ImeMode = ImeMode.NoControl;
            lblDecimalupto.Location = new Point(179, 228);
            lblDecimalupto.Name = "lblDecimalupto";
            lblDecimalupto.Size = new Size(60, 15);
            lblDecimalupto.TabIndex = 132;
            lblDecimalupto.Text = "Dec Upto";
            // 
            // chkDecimal
            // 
            chkDecimal.AutoSize = true;
            chkDecimal.Enabled = false;
            chkDecimal.Location = new Point(154, 229);
            chkDecimal.Name = "chkDecimal";
            chkDecimal.Size = new Size(15, 14);
            chkDecimal.TabIndex = 21;
            chkDecimal.UseVisualStyleBackColor = true;
            // 
            // lblDecimal
            // 
            lblDecimal.AutoSize = true;
            lblDecimal.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblDecimal.ImeMode = ImeMode.NoControl;
            lblDecimal.Location = new Point(96, 228);
            lblDecimal.Name = "lblDecimal";
            lblDecimal.Size = new Size(52, 15);
            lblDecimal.TabIndex = 130;
            lblDecimal.Text = "Decimal";
            // 
            // txtLT
            // 
            txtLT.Enabled = false;
            txtLT.Location = new Point(66, 223);
            txtLT.Name = "txtLT";
            txtLT.Size = new Size(24, 23);
            txtLT.TabIndex = 20;
            txtLT.TextChanged += txtLT_TextChanged;
            // 
            // lblLT
            // 
            lblLT.AutoSize = true;
            lblLT.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblLT.ImeMode = ImeMode.NoControl;
            lblLT.Location = new Point(4, 227);
            lblLT.Name = "lblLT";
            lblLT.Size = new Size(63, 15);
            lblLT.TabIndex = 128;
            lblLT.Text = "Req. Freq.";
            // 
            // txtexisper
            // 
            txtexisper.Enabled = false;
            txtexisper.Location = new Point(699, 191);
            txtexisper.Name = "txtexisper";
            txtexisper.Size = new Size(38, 23);
            txtexisper.TabIndex = 19;
            // 
            // lblExcis
            // 
            lblExcis.AutoSize = true;
            lblExcis.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblExcis.ImeMode = ImeMode.NoControl;
            lblExcis.Location = new Point(654, 194);
            lblExcis.Name = "lblExcis";
            lblExcis.Size = new Size(44, 15);
            lblExcis.TabIndex = 126;
            lblExcis.Text = "Excis%";
            // 
            // txtStyle
            // 
            txtStyle.Enabled = false;
            txtStyle.Location = new Point(570, 191);
            txtStyle.Name = "txtStyle";
            txtStyle.Size = new Size(68, 23);
            txtStyle.TabIndex = 18;
            // 
            // lblStyle
            // 
            lblStyle.AutoSize = true;
            lblStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblStyle.ImeMode = ImeMode.NoControl;
            lblStyle.Location = new Point(533, 194);
            lblStyle.Name = "lblStyle";
            lblStyle.Size = new Size(35, 15);
            lblStyle.TabIndex = 124;
            lblStyle.Text = "Style";
            // 
            // txtMaxLevel
            // 
            txtMaxLevel.Enabled = false;
            txtMaxLevel.Location = new Point(440, 191);
            txtMaxLevel.Name = "txtMaxLevel";
            txtMaxLevel.Size = new Size(85, 23);
            txtMaxLevel.TabIndex = 17;
            txtMaxLevel.TextChanged += txtMaxLevel_TextChanged;
            txtMaxLevel.Validating += txtMaxLevel_Validating;
            txtMaxLevel.Validated += txtMaxLevel_Validated;
            // 
            // lblMaxLevel
            // 
            lblMaxLevel.AutoSize = true;
            lblMaxLevel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblMaxLevel.ImeMode = ImeMode.NoControl;
            lblMaxLevel.Location = new Point(368, 194);
            lblMaxLevel.Name = "lblMaxLevel";
            lblMaxLevel.Size = new Size(64, 15);
            lblMaxLevel.TabIndex = 122;
            lblMaxLevel.Text = "Max Level";
            // 
            // txtReOLevel
            // 
            txtReOLevel.Location = new Point(277, 191);
            txtReOLevel.Name = "txtReOLevel";
            txtReOLevel.Size = new Size(84, 23);
            txtReOLevel.TabIndex = 16;
            txtReOLevel.Validating += txtReOLevel_Validating;
            // 
            // lblReOLevel
            // 
            lblReOLevel.AutoSize = true;
            lblReOLevel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblReOLevel.ImeMode = ImeMode.NoControl;
            lblReOLevel.Location = new Point(180, 194);
            lblReOLevel.Name = "lblReOLevel";
            lblReOLevel.Size = new Size(94, 15);
            lblReOLevel.TabIndex = 120;
            lblReOLevel.Text = "Re- order Level";
            // 
            // txtMinLevel
            // 
            txtMinLevel.Location = new Point(79, 190);
            txtMinLevel.Name = "txtMinLevel";
            txtMinLevel.Size = new Size(91, 23);
            txtMinLevel.TabIndex = 15;
            txtMinLevel.Validating += txtMinLevel_Validating;
            // 
            // lblMinLevel
            // 
            lblMinLevel.AutoSize = true;
            lblMinLevel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblMinLevel.ImeMode = ImeMode.NoControl;
            lblMinLevel.Location = new Point(12, 193);
            lblMinLevel.Name = "lblMinLevel";
            lblMinLevel.Size = new Size(65, 15);
            lblMinLevel.TabIndex = 118;
            lblMinLevel.Text = "MIn. Level";
            // 
            // lblSaleUnit
            // 
            lblSaleUnit.AutoSize = true;
            lblSaleUnit.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblSaleUnit.ImeMode = ImeMode.NoControl;
            lblSaleUnit.Location = new Point(5, 159);
            lblSaleUnit.Name = "lblSaleUnit";
            lblSaleUnit.Size = new Size(57, 15);
            lblSaleUnit.TabIndex = 111;
            lblSaleUnit.Text = "Sale Unit";
            // 
            // cboColorDesc
            // 
            cboColorDesc.DropDownHeight = 50;
            cboColorDesc.FormattingEnabled = true;
            cboColorDesc.IntegralHeight = false;
            cboColorDesc.Location = new Point(61, 96);
            cboColorDesc.Name = "cboColorDesc";
            cboColorDesc.Size = new Size(88, 23);
            cboColorDesc.TabIndex = 7;
            cboColorDesc.DropDown += cboColorDesc_DropDown;
            cboColorDesc.SelectedIndexChanged += colidcomb_SelectedIndexChanged;
            cboColorDesc.KeyDown += cboColorDesc_KeyDown;
            cboColorDesc.KeyUp += cboColorDesc_KeyUp;
            cboColorDesc.MouseDown += colidcomb_MouseDown;
            // 
            // lblColorDesc
            // 
            lblColorDesc.AutoSize = true;
            lblColorDesc.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblColorDesc.ImeMode = ImeMode.NoControl;
            lblColorDesc.Location = new Point(4, 99);
            lblColorDesc.Name = "lblColorDesc";
            lblColorDesc.Size = new Size(50, 15);
            lblColorDesc.TabIndex = 106;
            lblColorDesc.Text = "Color Id";
            // 
            // cboManuf
            // 
            cboManuf.DropDownHeight = 100;
            cboManuf.FormattingEnabled = true;
            cboManuf.IntegralHeight = false;
            cboManuf.Location = new Point(55, 127);
            cboManuf.Name = "cboManuf";
            cboManuf.Size = new Size(106, 23);
            cboManuf.TabIndex = 9;
            cboManuf.DropDown += cboManuf_DropDown;
            cboManuf.SelectedIndexChanged += manufcomb_SelectedIndexChanged;
            cboManuf.KeyUp += cboManuf_KeyUp;
            cboManuf.MouseDown += comboBox2_MouseDown;
            // 
            // lblManuf
            // 
            lblManuf.AutoSize = true;
            lblManuf.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblManuf.ImeMode = ImeMode.NoControl;
            lblManuf.Location = new Point(3, 130);
            lblManuf.Name = "lblManuf";
            lblManuf.Size = new Size(45, 15);
            lblManuf.TabIndex = 100;
            lblManuf.Text = "Manuf.";
            // 
            // cboSSGroupDesc
            // 
            cboSSGroupDesc.DropDownHeight = 100;
            cboSSGroupDesc.Enabled = false;
            cboSSGroupDesc.FormattingEnabled = true;
            cboSSGroupDesc.IntegralHeight = false;
            cboSSGroupDesc.Location = new Point(65, 65);
            cboSSGroupDesc.Name = "cboSSGroupDesc";
            cboSSGroupDesc.Size = new Size(88, 23);
            cboSSGroupDesc.TabIndex = 5;
            cboSSGroupDesc.DropDown += cboSSGroupDesc_DropDown;
            cboSSGroupDesc.SelectedIndexChanged += ssgrpcomb_SelectedIndexChanged;
            cboSSGroupDesc.KeyDown += cboSSGroupDesc_KeyDown;
            cboSSGroupDesc.KeyUp += cboSSGroupDesc_KeyUp;
            cboSSGroupDesc.MouseDown += ssgrpcomb_MouseDown;
            // 
            // lblSSGroupDesc
            // 
            lblSSGroupDesc.AutoSize = true;
            lblSSGroupDesc.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblSSGroupDesc.ImeMode = ImeMode.NoControl;
            lblSSGroupDesc.Location = new Point(3, 68);
            lblSSGroupDesc.Name = "lblSSGroupDesc";
            lblSSGroupDesc.Size = new Size(62, 15);
            lblSSGroupDesc.TabIndex = 94;
            lblSSGroupDesc.Text = "S.S Group";
            // 
            // cboGroup
            // 
            cboGroup.DropDownHeight = 100;
            cboGroup.FormattingEnabled = true;
            cboGroup.IntegralHeight = false;
            cboGroup.ItemHeight = 15;
            cboGroup.Location = new Point(61, 34);
            cboGroup.MaxDropDownItems = 5;
            cboGroup.Name = "cboGroup";
            cboGroup.Size = new Size(111, 23);
            cboGroup.TabIndex = 3;
            cboGroup.DropDown += cboGroup_DropDown;
            cboGroup.SelectedIndexChanged += grpcomb_SelectedIndexChanged;
            cboGroup.KeyDown += cboGroup_KeyDown;
            cboGroup.KeyPress += cboGroup_KeyPress;
            cboGroup.KeyUp += cboGroup_KeyUp;
            cboGroup.MouseDown += grpcomb_MouseDown;
            // 
            // lblGroup
            // 
            lblGroup.AutoSize = true;
            lblGroup.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblGroup.ImeMode = ImeMode.NoControl;
            lblGroup.Location = new Point(12, 37);
            lblGroup.Name = "lblGroup";
            lblGroup.Size = new Size(42, 15);
            lblGroup.TabIndex = 88;
            lblGroup.Text = "Group";
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblType.ImeMode = ImeMode.NoControl;
            lblType.Location = new Point(476, 7);
            lblType.Name = "lblType";
            lblType.Size = new Size(33, 15);
            lblType.TabIndex = 85;
            lblType.Text = "Type";
            // 
            // txtItemDesc
            // 
            txtItemDesc.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtItemDesc.AutoCompleteSource = AutoCompleteSource.HistoryList;
            txtItemDesc.Location = new Point(222, 4);
            txtItemDesc.Name = "txtItemDesc";
            txtItemDesc.Size = new Size(246, 23);
            txtItemDesc.TabIndex = 1;
            txtItemDesc.TabIndexChanged += txtItemDesc_TabIndexChanged;
            txtItemDesc.KeyDown += txtItemDesc_KeyDown;
            txtItemDesc.KeyUp += txtItemDesc_KeyUp;
            txtItemDesc.Validating += txtItemDesc_Validating;
            // 
            // lblItemDesc
            // 
            lblItemDesc.AutoSize = true;
            lblItemDesc.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblItemDesc.ForeColor = SystemColors.MenuText;
            lblItemDesc.ImeMode = ImeMode.NoControl;
            lblItemDesc.Location = new Point(182, 7);
            lblItemDesc.Name = "lblItemDesc";
            lblItemDesc.Size = new Size(42, 15);
            lblItemDesc.TabIndex = 83;
            lblItemDesc.Text = "Desc.*";
            // 
            // txtItemId
            // 
            txtItemId.Location = new Point(61, 4);
            txtItemId.Name = "txtItemId";
            txtItemId.Size = new Size(109, 23);
            txtItemId.TabIndex = 0;
            txtItemId.TabStop = false;
            txtItemId.TextChanged += itemidtxt_TextChanged;
            txtItemId.KeyUp += txtItemId_KeyUp;
            // 
            // lblItemId
            // 
            lblItemId.AutoSize = true;
            lblItemId.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblItemId.ImeMode = ImeMode.NoControl;
            lblItemId.Location = new Point(6, 7);
            lblItemId.Name = "lblItemId";
            lblItemId.Size = new Size(48, 15);
            lblItemId.TabIndex = 81;
            lblItemId.Text = "Item Id";
            lblItemId.Click += lblItemId_Click;
            // 
            // rotGroup
            // 
            rotGroup.BackColor = Color.Bisque;
            rotGroup.BorderStyle = BorderStyle.Fixed3D;
            rotGroup.Location = new Point(180, 34);
            rotGroup.Name = "rotGroup";
            rotGroup.Size = new Size(200, 23);
            rotGroup.TabIndex = 167;
            rotGroup.Click += lblgrp_Click;
            // 
            // rotSSGroupDesc
            // 
            rotSSGroupDesc.BackColor = Color.Bisque;
            rotSSGroupDesc.BorderStyle = BorderStyle.Fixed3D;
            rotSSGroupDesc.Location = new Point(157, 65);
            rotSSGroupDesc.Name = "rotSSGroupDesc";
            rotSSGroupDesc.Size = new Size(223, 23);
            rotSSGroupDesc.TabIndex = 169;
            // 
            // rotColorDesc
            // 
            rotColorDesc.BackColor = Color.Bisque;
            rotColorDesc.BorderStyle = BorderStyle.Fixed3D;
            rotColorDesc.Location = new Point(154, 96);
            rotColorDesc.Name = "rotColorDesc";
            rotColorDesc.Size = new Size(226, 23);
            rotColorDesc.TabIndex = 171;
            // 
            // rotManuf
            // 
            rotManuf.BackColor = Color.Bisque;
            rotManuf.BorderStyle = BorderStyle.Fixed3D;
            rotManuf.Location = new Point(170, 126);
            rotManuf.Name = "rotManuf";
            rotManuf.Size = new Size(217, 23);
            rotManuf.TabIndex = 172;
            // 
            // rotSaleUnit
            // 
            rotSaleUnit.BackColor = Color.Bisque;
            rotSaleUnit.BorderStyle = BorderStyle.Fixed3D;
            rotSaleUnit.Enabled = false;
            rotSaleUnit.Location = new Point(166, 157);
            rotSaleUnit.Name = "rotSaleUnit";
            rotSaleUnit.Size = new Size(130, 23);
            rotSaleUnit.TabIndex = 174;
            // 
            // rotSaleTax
            // 
            rotSaleTax.BackColor = Color.Bisque;
            rotSaleTax.BorderStyle = BorderStyle.Fixed3D;
            rotSaleTax.Enabled = false;
            rotSaleTax.Location = new Point(552, 224);
            rotSaleTax.Name = "rotSaleTax";
            rotSaleTax.Size = new Size(73, 23);
            rotSaleTax.TabIndex = 175;
            // 
            // rotLoc
            // 
            rotLoc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            rotLoc.BackColor = Color.Bisque;
            rotLoc.BorderStyle = BorderStyle.Fixed3D;
            rotLoc.Enabled = false;
            rotLoc.Location = new Point(335, 254);
            rotLoc.Name = "rotLoc";
            rotLoc.Size = new Size(71, 23);
            rotLoc.TabIndex = 176;
            // 
            // rotStock
            // 
            rotStock.BackColor = Color.Bisque;
            rotStock.BorderStyle = BorderStyle.Fixed3D;
            rotStock.Enabled = false;
            rotStock.Location = new Point(577, 256);
            rotStock.Name = "rotStock";
            rotStock.Size = new Size(89, 23);
            rotStock.TabIndex = 177;
            // 
            // cboSaleUnit
            // 
            cboSaleUnit.DropDownHeight = 100;
            cboSaleUnit.Enabled = false;
            cboSaleUnit.FormattingEnabled = true;
            cboSaleUnit.IntegralHeight = false;
            cboSaleUnit.Location = new Point(61, 157);
            cboSaleUnit.Name = "cboSaleUnit";
            cboSaleUnit.Size = new Size(98, 23);
            cboSaleUnit.TabIndex = 11;
            cboSaleUnit.DropDown += cboSaleUnit_DropDown;
            cboSaleUnit.SelectedIndexChanged += salunitcomb_SelectedIndexChanged;
            cboSaleUnit.MouseDown += salunitcomb_MouseDown;
            // 
            // panel3
            // 
            panel3.Location = new Point(-2, 339);
            panel3.Name = "panel3";
            panel3.Size = new Size(733, 114);
            panel3.TabIndex = 185;
            // 
            // dbgBarDet
            // 
            dbgBarDet.BorderStyle = BorderStyle.Fixed3D;
            dbgBarDet.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            dbgBarDet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dbgBarDet.Columns.AddRange(new DataGridViewColumn[] { PLU, BARCODE, cp, MRP, salepric, netrate, margin_per, ACTIVE });
            dbgBarDet.Location = new Point(-2, 310);
            dbgBarDet.Name = "dbgBarDet";
            dbgBarDet.RowTemplate.Height = 25;
            dbgBarDet.Size = new Size(758, 134);
            dbgBarDet.TabIndex = 30;
            dbgBarDet.CellBeginEdit += dbgBarDet_CellBeginEdit;
            dbgBarDet.CellEndEdit += dataGridView1_CellEndEdit;
            dbgBarDet.CellEnter += dbgBarDet_CellEnter;
            dbgBarDet.CellFormatting += dataGridView1_CellFormatting;
            dbgBarDet.CellLeave += dbgBarDet_CellLeave;
            dbgBarDet.CellValidating += dbgBarDet_CellValidating;
            dbgBarDet.CellValueChanged += dbgBarDet_CellValueChanged;
            dbgBarDet.CurrentCellChanged += dbgBarDet_CurrentCellChanged;
            dbgBarDet.CurrentCellDirtyStateChanged += dbgBarDet_CurrentCellDirtyStateChanged;
            dbgBarDet.EditingControlShowing += dataGridView1_EditingControlShowing;
            dbgBarDet.RowsAdded += dbgBarDet_RowsAdded;
            dbgBarDet.KeyDown += dataGridView1_KeyDown;
            dbgBarDet.KeyPress += dbgBarDet_KeyPress;
            // 
            // PLU
            // 
            PLU.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            PLU.Frozen = true;
            PLU.HeaderText = "PLU";
            PLU.Name = "PLU";
            PLU.ReadOnly = true;
            PLU.Width = 80;
            // 
            // BARCODE
            // 
            BARCODE.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            BARCODE.HeaderText = "BARCODE";
            BARCODE.Name = "BARCODE";
            BARCODE.Width = 120;
            // 
            // cp
            // 
            cp.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            cp.HeaderText = "Cost Price*";
            cp.Name = "cp";
            // 
            // MRP
            // 
            MRP.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            MRP.HeaderText = "MRP*";
            MRP.Name = "MRP";
            MRP.ReadOnly = true;
            // 
            // salepric
            // 
            salepric.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            salepric.HeaderText = "SALE PRICE*";
            salepric.Name = "salepric";
            salepric.ReadOnly = true;
            // 
            // netrate
            // 
            netrate.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            netrate.HeaderText = "NET RATE";
            netrate.Name = "netrate";
            netrate.ReadOnly = true;
            netrate.Width = 80;
            // 
            // margin_per
            // 
            margin_per.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            margin_per.HeaderText = "MARGIN%";
            margin_per.Name = "margin_per";
            margin_per.ReadOnly = true;
            margin_per.Width = 80;
            // 
            // ACTIVE
            // 
            ACTIVE.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            ACTIVE.FalseValue = "N";
            ACTIVE.HeaderText = "ACTIVE*";
            ACTIVE.Name = "ACTIVE";
            ACTIVE.Resizable = DataGridViewTriState.True;
            ACTIVE.SortMode = DataGridViewColumnSortMode.Automatic;
            ACTIVE.TrueValue = "Y";
            ACTIVE.Width = 50;
            // 
            // label25
            // 
            label25.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label25.BackColor = Color.CadetBlue;
            label25.Font = new Font("Comic Sans MS", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label25.ImeMode = ImeMode.NoControl;
            label25.Location = new Point(1, 291);
            label25.Name = "label25";
            label25.Size = new Size(758, 17);
            label25.TabIndex = 186;
            label25.Text = "Barcode Linking";
            label25.TextAlign = ContentAlignment.TopCenter;
            // 
            // chkNodisc
            // 
            chkNodisc.AutoSize = true;
            chkNodisc.Enabled = false;
            chkNodisc.Location = new Point(370, 162);
            chkNodisc.Name = "chkNodisc";
            chkNodisc.Size = new Size(15, 14);
            chkNodisc.TabIndex = 12;
            chkNodisc.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ImeMode = ImeMode.NoControl;
            label1.Location = new Point(312, 161);
            label1.Name = "label1";
            label1.Size = new Size(52, 15);
            label1.TabIndex = 187;
            label1.Text = "No Disc.";
            // 
            // rotNetRate
            // 
            rotNetRate.BackColor = Color.Bisque;
            rotNetRate.BorderStyle = BorderStyle.Fixed3D;
            rotNetRate.Enabled = false;
            rotNetRate.Location = new Point(681, 256);
            rotNetRate.Name = "rotNetRate";
            rotNetRate.Size = new Size(57, 23);
            rotNetRate.TabIndex = 179;
            // 
            // cboType
            // 
            cboType.DropDownHeight = 100;
            cboType.Enabled = false;
            cboType.FormattingEnabled = true;
            cboType.IntegralHeight = false;
            cboType.ItemHeight = 15;
            cboType.Location = new Point(513, 4);
            cboType.MaxDropDownItems = 5;
            cboType.Name = "cboType";
            cboType.Size = new Size(55, 23);
            cboType.TabIndex = 2;
            cboType.DropDown += cboType_DropDown_2;
            cboType.SelectedIndexChanged += cboType_SelectedIndexChanged_1;
            // 
            // rotType
            // 
            rotType.BackColor = Color.Bisque;
            rotType.BorderStyle = BorderStyle.Fixed3D;
            rotType.Location = new Point(577, 4);
            rotType.Name = "rotType";
            rotType.Size = new Size(161, 23);
            rotType.TabIndex = 190;
            // 
            // rotSGroup
            // 
            rotSGroup.BackColor = Color.Bisque;
            rotSGroup.BorderStyle = BorderStyle.Fixed3D;
            rotSGroup.Location = new Point(561, 33);
            rotSGroup.Name = "rotSGroup";
            rotSGroup.Size = new Size(184, 23);
            rotSGroup.TabIndex = 193;
            // 
            // lblSubGroup
            // 
            lblSubGroup.AutoSize = true;
            lblSubGroup.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblSubGroup.ImeMode = ImeMode.NoControl;
            lblSubGroup.Location = new Point(397, 37);
            lblSubGroup.Name = "lblSubGroup";
            lblSubGroup.Size = new Size(66, 15);
            lblSubGroup.TabIndex = 192;
            lblSubGroup.Text = "Sub Group";
            // 
            // cboSGroup
            // 
            cboSGroup.DropDownHeight = 100;
            cboSGroup.Enabled = false;
            cboSGroup.FormattingEnabled = true;
            cboSGroup.IntegralHeight = false;
            cboSGroup.Location = new Point(470, 33);
            cboSGroup.Name = "cboSGroup";
            cboSGroup.Size = new Size(85, 23);
            cboSGroup.TabIndex = 4;
            cboSGroup.SelectedIndexChanged += subgrpcomb_SelectedIndexChanged;
            cboSGroup.DragDrop += cboSGroup_DropDown;
            cboSGroup.KeyDown += cboSGroup_KeyDown;
            // 
            // lblOpBal
            // 
            lblOpBal.AutoSize = true;
            lblOpBal.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblOpBal.ImeMode = ImeMode.NoControl;
            lblOpBal.Location = new Point(581, 159);
            lblOpBal.Name = "lblOpBal";
            lblOpBal.Size = new Size(69, 15);
            lblOpBal.TabIndex = 116;
            lblOpBal.Text = "OP Balance";
            // 
            // txtConv
            // 
            txtConv.Enabled = false;
            txtConv.Location = new Point(486, 155);
            txtConv.Name = "txtConv";
            txtConv.Size = new Size(85, 23);
            txtConv.TabIndex = 13;
            // 
            // lblConv
            // 
            lblConv.AutoSize = true;
            lblConv.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblConv.ImeMode = ImeMode.NoControl;
            lblConv.Location = new Point(411, 160);
            lblConv.Name = "lblConv";
            lblConv.Size = new Size(69, 15);
            lblConv.TabIndex = 114;
            lblConv.Text = "Conversion";
            // 
            // txtOpBal
            // 
            txtOpBal.Enabled = false;
            txtOpBal.Location = new Point(657, 155);
            txtOpBal.Name = "txtOpBal";
            txtOpBal.Size = new Size(84, 23);
            txtOpBal.TabIndex = 14;
            // 
            // lblPurUnit
            // 
            lblPurUnit.AutoSize = true;
            lblPurUnit.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblPurUnit.ImeMode = ImeMode.NoControl;
            lblPurUnit.Location = new Point(401, 129);
            lblPurUnit.Name = "lblPurUnit";
            lblPurUnit.Size = new Size(58, 15);
            lblPurUnit.TabIndex = 103;
            lblPurUnit.Text = "Pur.  Unit";
            // 
            // rotPurUnit
            // 
            rotPurUnit.BackColor = Color.Bisque;
            rotPurUnit.BorderStyle = BorderStyle.Fixed3D;
            rotPurUnit.Location = new Point(558, 126);
            rotPurUnit.Name = "rotPurUnit";
            rotPurUnit.Size = new Size(184, 23);
            rotPurUnit.TabIndex = 173;
            // 
            // cboPurUnit
            // 
            cboPurUnit.DropDownHeight = 100;
            cboPurUnit.Enabled = false;
            cboPurUnit.FormattingEnabled = true;
            cboPurUnit.IntegralHeight = false;
            cboPurUnit.Location = new Point(467, 126);
            cboPurUnit.Name = "cboPurUnit";
            cboPurUnit.Size = new Size(88, 23);
            cboPurUnit.TabIndex = 10;
            cboPurUnit.DropDown += cboPurUnit_DropDown;
            cboPurUnit.SelectedIndexChanged += purunitcomb_SelectedIndexChanged;
            cboPurUnit.MouseDown += purunitcomb_MouseDown;
            // 
            // textBox3
            // 
            textBox3.Font = new Font("Segoe UI", 5.25F, FontStyle.Regular, GraphicsUnit.Point);
            textBox3.Location = new Point(68, 8);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(9, 17);
            textBox3.TabIndex = 182;
            // 
            // lblSizeDesc
            // 
            lblSizeDesc.AutoSize = true;
            lblSizeDesc.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblSizeDesc.ImeMode = ImeMode.NoControl;
            lblSizeDesc.Location = new Point(409, 69);
            lblSizeDesc.Name = "lblSizeDesc";
            lblSizeDesc.Size = new Size(44, 15);
            lblSizeDesc.TabIndex = 195;
            lblSizeDesc.Text = "Size Id";
            // 
            // cboSizeDesc
            // 
            cboSizeDesc.DropDownHeight = 100;
            cboSizeDesc.Enabled = false;
            cboSizeDesc.FormattingEnabled = true;
            cboSizeDesc.IntegralHeight = false;
            cboSizeDesc.Location = new Point(465, 65);
            cboSizeDesc.Name = "cboSizeDesc";
            cboSizeDesc.Size = new Size(85, 23);
            cboSizeDesc.TabIndex = 6;
            cboSizeDesc.DropDown += cboSizeDesc_DropDown;
            cboSizeDesc.SelectedIndexChanged += sizecomb_SelectedIndexChanged;
            cboSizeDesc.KeyDown += cboSizeDesc_KeyDown;
            // 
            // rotSizeDesc
            // 
            rotSizeDesc.BackColor = Color.Bisque;
            rotSizeDesc.BorderStyle = BorderStyle.Fixed3D;
            rotSizeDesc.Location = new Point(556, 65);
            rotSizeDesc.Name = "rotSizeDesc";
            rotSizeDesc.Size = new Size(184, 23);
            rotSizeDesc.TabIndex = 196;
            // 
            // lblShDesc
            // 
            lblShDesc.AutoSize = true;
            lblShDesc.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblShDesc.ImeMode = ImeMode.NoControl;
            lblShDesc.Location = new Point(393, 100);
            lblShDesc.Name = "lblShDesc";
            lblShDesc.Size = new Size(71, 15);
            lblShDesc.TabIndex = 197;
            lblShDesc.Text = "Short Desc.";
            // 
            // txtShDesc
            // 
            txtShDesc.AutoCompleteSource = AutoCompleteSource.HistoryList;
            txtShDesc.Enabled = false;
            txtShDesc.Location = new Point(470, 96);
            txtShDesc.Name = "txtShDesc";
            txtShDesc.Size = new Size(275, 23);
            txtShDesc.TabIndex = 8;
            // 
            // lblDisc
            // 
            lblDisc.AutoSize = true;
            lblDisc.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblDisc.ImeMode = ImeMode.NoControl;
            lblDisc.Location = new Point(412, 260);
            lblDisc.Name = "lblDisc";
            lblDisc.Size = new Size(51, 15);
            lblDisc.TabIndex = 199;
            lblDisc.Text = "Disc (%)";
            // 
            // frmM_Item
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.PowderBlue;
            ClientSize = new Size(755, 447);
            Controls.Add(lblDisc);
            Controls.Add(lblShDesc);
            Controls.Add(txtShDesc);
            Controls.Add(cboPurUnit);
            Controls.Add(rotPurUnit);
            Controls.Add(lblSizeDesc);
            Controls.Add(lblPurUnit);
            Controls.Add(cboSizeDesc);
            Controls.Add(txtOpBal);
            Controls.Add(rotSizeDesc);
            Controls.Add(lblConv);
            Controls.Add(txtConv);
            Controls.Add(rotSGroup);
            Controls.Add(lblOpBal);
            Controls.Add(lblSubGroup);
            Controls.Add(cboSGroup);
            Controls.Add(cboType);
            Controls.Add(rotType);
            Controls.Add(chkNodisc);
            Controls.Add(label1);
            Controls.Add(label25);
            Controls.Add(cboSaleUnit);
            Controls.Add(rotNetRate);
            Controls.Add(rotStock);
            Controls.Add(rotLoc);
            Controls.Add(rotSaleTax);
            Controls.Add(rotSaleUnit);
            Controls.Add(rotManuf);
            Controls.Add(rotColorDesc);
            Controls.Add(rotSSGroupDesc);
            Controls.Add(rotGroup);
            Controls.Add(dbgBarDet);
            Controls.Add(lblStock);
            Controls.Add(txtDisc);
            Controls.Add(cboLoc);
            Controls.Add(lblLoc);
            Controls.Add(chkBarYN);
            Controls.Add(lblBarYN);
            Controls.Add(chkAct);
            Controls.Add(lblAct);
            Controls.Add(txtCess);
            Controls.Add(lblCess);
            Controls.Add(cboSaleTax);
            Controls.Add(lblSaleTax);
            Controls.Add(txtHSN);
            Controls.Add(lblHSN);
            Controls.Add(txtDecimalupto);
            Controls.Add(lblDecimalupto);
            Controls.Add(chkDecimal);
            Controls.Add(lblDecimal);
            Controls.Add(txtLT);
            Controls.Add(lblLT);
            Controls.Add(txtexisper);
            Controls.Add(lblExcis);
            Controls.Add(txtStyle);
            Controls.Add(lblStyle);
            Controls.Add(txtMaxLevel);
            Controls.Add(lblMaxLevel);
            Controls.Add(txtReOLevel);
            Controls.Add(lblReOLevel);
            Controls.Add(txtMinLevel);
            Controls.Add(lblMinLevel);
            Controls.Add(lblSaleUnit);
            Controls.Add(cboColorDesc);
            Controls.Add(lblColorDesc);
            Controls.Add(cboManuf);
            Controls.Add(lblManuf);
            Controls.Add(cboSSGroupDesc);
            Controls.Add(lblSSGroupDesc);
            Controls.Add(cboGroup);
            Controls.Add(lblGroup);
            Controls.Add(lblType);
            Controls.Add(txtItemDesc);
            Controls.Add(lblItemDesc);
            Controls.Add(txtItemId);
            Controls.Add(lblItemId);
            Controls.Add(panel3);
            Controls.Add(textBox3);
            Location = new Point(10, 10);
            MdiChildrenMinimizedAnchorBottom = false;
            Name = "frmM_Item";
            StartPosition = FormStartPosition.Manual;
            Text = "Item Master";
            TopMost = true;
            Deactivate += frmM_Item_Deactivate;
            FormClosing += frmM_Item_FormClosing;
            FormClosed += Item_FormClosed;
            Load += Item_Load;
            KeyPress += frmM_Item_KeyPress;
            Resize += frmM_Item_Resize;
            ((System.ComponentModel.ISupportInitialize)dbgBarDet).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lblStock;
        private TextBox txtDisc;
        private ComboBox cboLoc;
        private Label lblLoc;
        private CheckBox chkBarYN;
        private Label lblBarYN;
        private CheckBox chkAct;
        private Label lblAct;
        private TextBox txtCess;
        private Label lblCess;
        public ComboBox cboSaleTax;
        private Label lblSaleTax;
        private TextBox txtHSN;
        private Label lblHSN;
        private TextBox txtDecimalupto;
        private Label lblDecimalupto;
        private CheckBox chkDecimal;
        private Label lblDecimal;
        private TextBox txtLT;
        private Label lblLT;
        private TextBox txtexisper;
        private Label lblExcis;
        private TextBox txtStyle;
        private Label lblStyle;
        private TextBox txtMaxLevel;
        private Label lblMaxLevel;
        private TextBox txtReOLevel;
        private Label lblReOLevel;
        private TextBox txtMinLevel;
        private Label lblMinLevel;
        private TextBox textBox5;
        private ComboBox comboBox3;
        private Label lblSaleUnit;
        private TextBox textBox4;
        private Label lblColorDesc;
        private TextBox textBox1;
        private TextBox textBox2;
        private ComboBox cboManuf;
        private Label lblManuf;
        private Label lblSSGroupDesc;
        private Label lblGroup;
        private Label lblType;
        private TextBox txtItemDesc;
        private Label lblItemDesc;
        private TextBox txtItemId;
        private Label lblItemId;
        private Label rotSSGroupDesc;
        private Label rotManuf;
        private Label rotSaleUnit;
        private Label rotSaleTax;
        private Label rotLoc;
        private Label rotStock;
        private ComboBox cboSaleUnit;
        private Panel panel3;
        private Label label25;
        public ComboBox cboGroup;
        public ComboBox cboSSGroupDesc;
        public DataGridView dbgBarDet;
        private CheckBox chkNodisc;
        private Label label1;
        public Label rotGroup;
        private Label rotNetRate;
        public ComboBox cboColorDesc;
        public Label rotColorDesc;
        public ComboBox cboType;
        private Label rotType;
        private Label rotSGroup;
        private Label lblSubGroup;
        public ComboBox cboSGroup;
        private Label lblOpBal;
        private TextBox txtConv;
        private Label lblConv;
        private TextBox txtOpBal;
        private Label lblPurUnit;
        private Label rotPurUnit;
        private ComboBox cboPurUnit;
        private TextBox textBox3;
        private Label lblSizeDesc;
        public ComboBox cboSizeDesc;
        public Label rotSizeDesc;
        public Label lblShDesc;
        public TextBox txtShDesc;
        private Label lblDisc;
        private DataGridViewTextBoxColumn PLU;
        private DataGridViewTextBoxColumn BARCODE;
        private DataGridViewTextBoxColumn cp;
        private DataGridViewTextBoxColumn MRP;
        private DataGridViewTextBoxColumn salepric;
        private DataGridViewTextBoxColumn netrate;
        private DataGridViewTextBoxColumn margin_per;
        private DataGridViewCheckBoxColumn ACTIVE;
    }
}