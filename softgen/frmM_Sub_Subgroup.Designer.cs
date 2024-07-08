namespace softgen
{
    partial class frmM_Sub_Subgroup
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
            M_Sub_Subgroup = new Panel();
            rotSubGrpDesc = new Label();
            chkDisc = new CheckBox();
            cboSubGrpId = new ComboBox();
            lblSubSGrpId = new Label();
            txtDisc = new TextBox();
            label4 = new Label();
            lblDisc = new Label();
            chkAct = new CheckBox();
            label2 = new Label();
            chkSPChange = new CheckBox();
            txtSTaxPer = new TextBox();
            lblSPChange = new Label();
            lblSTaxPer = new Label();
            txtSubSGrpId = new TextBox();
            txtSubSGrpDesc = new TextBox();
            lblSubGrpDesc = new Label();
            lblSubGrpId = new Label();
            rotGrpDesc = new Label();
            cboGrpId = new ComboBox();
            lblGrpId = new Label();
            M_Sub_Subgroup.SuspendLayout();
            SuspendLayout();
            // 
            // M_Sub_Subgroup
            // 
            M_Sub_Subgroup.BackColor = Color.PowderBlue;
            M_Sub_Subgroup.BorderStyle = BorderStyle.Fixed3D;
            M_Sub_Subgroup.Controls.Add(rotSubGrpDesc);
            M_Sub_Subgroup.Controls.Add(chkDisc);
            M_Sub_Subgroup.Controls.Add(cboSubGrpId);
            M_Sub_Subgroup.Controls.Add(lblSubSGrpId);
            M_Sub_Subgroup.Controls.Add(txtDisc);
            M_Sub_Subgroup.Controls.Add(label4);
            M_Sub_Subgroup.Controls.Add(lblDisc);
            M_Sub_Subgroup.Controls.Add(chkAct);
            M_Sub_Subgroup.Controls.Add(label2);
            M_Sub_Subgroup.Controls.Add(chkSPChange);
            M_Sub_Subgroup.Controls.Add(txtSTaxPer);
            M_Sub_Subgroup.Controls.Add(lblSPChange);
            M_Sub_Subgroup.Controls.Add(lblSTaxPer);
            M_Sub_Subgroup.Controls.Add(txtSubSGrpId);
            M_Sub_Subgroup.Controls.Add(txtSubSGrpDesc);
            M_Sub_Subgroup.Controls.Add(lblSubGrpDesc);
            M_Sub_Subgroup.Controls.Add(lblSubGrpId);
            M_Sub_Subgroup.Controls.Add(rotGrpDesc);
            M_Sub_Subgroup.Controls.Add(cboGrpId);
            M_Sub_Subgroup.Controls.Add(lblGrpId);
            M_Sub_Subgroup.Location = new Point(0, 0);
            M_Sub_Subgroup.Name = "M_Sub_Subgroup";
            M_Sub_Subgroup.Size = new Size(629, 254);
            M_Sub_Subgroup.TabIndex = 0;
            // 
            // rotSubGrpDesc
            // 
            rotSubGrpDesc.BackColor = Color.Bisque;
            rotSubGrpDesc.BorderStyle = BorderStyle.Fixed3D;
            rotSubGrpDesc.Location = new Point(264, 37);
            rotSubGrpDesc.Name = "rotSubGrpDesc";
            rotSubGrpDesc.Size = new Size(237, 23);
            rotSubGrpDesc.TabIndex = 208;
            // 
            // chkDisc
            // 
            chkDisc.AutoSize = true;
            chkDisc.ImeMode = ImeMode.NoControl;
            chkDisc.Location = new Point(455, 161);
            chkDisc.Name = "chkDisc";
            chkDisc.Size = new Size(15, 14);
            chkDisc.TabIndex = 207;
            chkDisc.UseVisualStyleBackColor = true;
            // 
            // cboSubGrpId
            // 
            cboSubGrpId.BackColor = SystemColors.Window;
            cboSubGrpId.FlatStyle = FlatStyle.Flat;
            cboSubGrpId.FormattingEnabled = true;
            cboSubGrpId.Location = new Point(135, 38);
            cboSubGrpId.Name = "cboSubGrpId";
            cboSubGrpId.Size = new Size(121, 23);
            cboSubGrpId.TabIndex = 206;
            cboSubGrpId.DropDown += cboSubGrpId_DropDown;
            cboSubGrpId.SelectedIndexChanged += cboSubGrpId_SelectedIndexChanged;
            cboSubGrpId.KeyUp += cboSubGrpId_KeyUp;
            cboSubGrpId.Layout += cboSubGrpId_Layout;
            // 
            // lblSubSGrpId
            // 
            lblSubSGrpId.AutoSize = true;
            lblSubSGrpId.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblSubSGrpId.ImeMode = ImeMode.NoControl;
            lblSubSGrpId.Location = new Point(17, 73);
            lblSubSGrpId.Name = "lblSubSGrpId";
            lblSubSGrpId.Size = new Size(104, 15);
            lblSubSGrpId.TabIndex = 205;
            lblSubSGrpId.Text = "Sub Sub Group Id";
            // 
            // txtDisc
            // 
            txtDisc.Location = new Point(363, 124);
            txtDisc.Name = "txtDisc";
            txtDisc.Size = new Size(138, 23);
            txtDisc.TabIndex = 204;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ImeMode = ImeMode.NoControl;
            label4.Location = new Point(267, 128);
            label4.Name = "label4";
            label4.Size = new Size(93, 15);
            label4.TabIndex = 203;
            label4.Text = "Max Discount%";
            // 
            // lblDisc
            // 
            lblDisc.AutoSize = true;
            lblDisc.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblDisc.ImeMode = ImeMode.NoControl;
            lblDisc.Location = new Point(345, 161);
            lblDisc.Name = "lblDisc";
            lblDisc.Size = new Size(104, 15);
            lblDisc.TabIndex = 202;
            lblDisc.Text = "Discount Allowed";
            // 
            // chkAct
            // 
            chkAct.AutoSize = true;
            chkAct.ImeMode = ImeMode.NoControl;
            chkAct.Location = new Point(72, 159);
            chkAct.Name = "chkAct";
            chkAct.Size = new Size(15, 14);
            chkAct.TabIndex = 201;
            chkAct.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ImeMode = ImeMode.NoControl;
            label2.Location = new Point(20, 159);
            label2.Name = "label2";
            label2.Size = new Size(43, 15);
            label2.TabIndex = 200;
            label2.Text = "Active";
            // 
            // chkSPChange
            // 
            chkSPChange.AutoSize = true;
            chkSPChange.ImeMode = ImeMode.NoControl;
            chkSPChange.Location = new Point(267, 162);
            chkSPChange.Name = "chkSPChange";
            chkSPChange.Size = new Size(15, 14);
            chkSPChange.TabIndex = 199;
            chkSPChange.UseVisualStyleBackColor = true;
            // 
            // txtSTaxPer
            // 
            txtSTaxPer.Location = new Point(133, 124);
            txtSTaxPer.Name = "txtSTaxPer";
            txtSTaxPer.Size = new Size(126, 23);
            txtSTaxPer.TabIndex = 198;
            // 
            // lblSPChange
            // 
            lblSPChange.AutoSize = true;
            lblSPChange.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblSPChange.ImeMode = ImeMode.NoControl;
            lblSPChange.Location = new Point(155, 160);
            lblSPChange.Name = "lblSPChange";
            lblSPChange.Size = new Size(105, 15);
            lblSPChange.TabIndex = 197;
            lblSPChange.Text = "Sale Price Change";
            // 
            // lblSTaxPer
            // 
            lblSTaxPer.AutoSize = true;
            lblSTaxPer.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblSTaxPer.ImeMode = ImeMode.NoControl;
            lblSTaxPer.Location = new Point(20, 128);
            lblSTaxPer.Name = "lblSTaxPer";
            lblSTaxPer.Size = new Size(70, 15);
            lblSTaxPer.TabIndex = 196;
            lblSTaxPer.Text = "Sales Tax %";
            // 
            // txtSubSGrpId
            // 
            txtSubSGrpId.Location = new Point(134, 68);
            txtSubSGrpId.Name = "txtSubSGrpId";
            txtSubSGrpId.Size = new Size(165, 23);
            txtSubSGrpId.TabIndex = 193;
            // 
            // txtSubSGrpDesc
            // 
            txtSubSGrpDesc.Location = new Point(133, 96);
            txtSubSGrpDesc.Name = "txtSubSGrpDesc";
            txtSubSGrpDesc.Size = new Size(368, 23);
            txtSubSGrpDesc.TabIndex = 195;
            // 
            // lblSubGrpDesc
            // 
            lblSubGrpDesc.BackColor = Color.PowderBlue;
            lblSubGrpDesc.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblSubGrpDesc.Location = new Point(18, 98);
            lblSubGrpDesc.Name = "lblSubGrpDesc";
            lblSubGrpDesc.Size = new Size(75, 18);
            lblSubGrpDesc.TabIndex = 194;
            lblSubGrpDesc.Text = "Description";
            // 
            // lblSubGrpId
            // 
            lblSubGrpId.AutoSize = true;
            lblSubGrpId.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblSubGrpId.ImeMode = ImeMode.NoControl;
            lblSubGrpId.Location = new Point(17, 43);
            lblSubGrpId.Name = "lblSubGrpId";
            lblSubGrpId.Size = new Size(80, 15);
            lblSubGrpId.TabIndex = 192;
            lblSubGrpId.Text = "Sub Group Id";
            // 
            // rotGrpDesc
            // 
            rotGrpDesc.BackColor = Color.Bisque;
            rotGrpDesc.BorderStyle = BorderStyle.Fixed3D;
            rotGrpDesc.Location = new Point(265, 8);
            rotGrpDesc.Name = "rotGrpDesc";
            rotGrpDesc.Size = new Size(236, 23);
            rotGrpDesc.TabIndex = 191;
            // 
            // cboGrpId
            // 
            cboGrpId.BackColor = SystemColors.Window;
            cboGrpId.FlatStyle = FlatStyle.Flat;
            cboGrpId.FormattingEnabled = true;
            cboGrpId.Location = new Point(135, 9);
            cboGrpId.Name = "cboGrpId";
            cboGrpId.Size = new Size(121, 23);
            cboGrpId.TabIndex = 190;
            cboGrpId.SelectedIndexChanged += cboGrpId_SelectedIndexChanged;
            // 
            // lblGrpId
            // 
            lblGrpId.AutoSize = true;
            lblGrpId.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblGrpId.ImeMode = ImeMode.NoControl;
            lblGrpId.Location = new Point(20, 15);
            lblGrpId.Name = "lblGrpId";
            lblGrpId.Size = new Size(56, 15);
            lblGrpId.TabIndex = 189;
            lblGrpId.Text = "Group Id";
            // 
            // frmM_Sub_Subgroup
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(509, 190);
            Controls.Add(M_Sub_Subgroup);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "frmM_Sub_Subgroup";
            Text = "Sub Subgroup Master";
            FormClosing += frmM_Sub_Subgroup_FormClosing;
            FormClosed += frmM_Sub_Subgroup_FormClosed;
            Load += frmM_Sub_Subgroup_Load;
            Validating += frmM_Sub_Subgroup_Validating;
            M_Sub_Subgroup.ResumeLayout(false);
            M_Sub_Subgroup.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel M_Sub_Subgroup;
        private Label label4;
        private Label lblDisc;
        private Label label2;
        private Label lblSPChange;
        private Label lblSTaxPer;
        private Label lblSubGrpDesc;
        private Label lblSubGrpId;
        private Label lblGrpId;
        private Label lblSubSGrpId;
        public TextBox txtDisc;
        public TextBox txtSTaxPer;
        public TextBox txtSubSGrpId;
        public TextBox txtSubSGrpDesc;
        public Label rotGrpDesc;
        public ComboBox cboGrpId;
        public ComboBox cboSubGrpId;
        public Label rotSubGrpDesc;
        public CheckBox chkAct;
        public CheckBox chkSPChange;
        public CheckBox chkDisc;
    }
}