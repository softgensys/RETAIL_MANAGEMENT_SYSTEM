namespace softgen
{
    partial class frmM_Sub_Group
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
            lblGrpId = new Label();
            cboGrpId = new ComboBox();
            rotGrpDesc = new Label();
            lblSubGrpId = new Label();
            lblSubGrpDesc = new Label();
            txtSubGrpDesc = new TextBox();
            txtSubGrpId = new TextBox();
            M_Sub_Group = new Panel();
            textBox1 = new TextBox();
            label4 = new Label();
            chkDisc = new CheckBox();
            lblDisc = new Label();
            checkBox1 = new CheckBox();
            label2 = new Label();
            chkSPChange = new CheckBox();
            txtSTaxPer = new TextBox();
            lblSPChange = new Label();
            lblSTaxPer = new Label();
            M_Sub_Group.SuspendLayout();
            SuspendLayout();
            // 
            // lblGrpId
            // 
            lblGrpId.AutoSize = true;
            lblGrpId.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblGrpId.ImeMode = ImeMode.NoControl;
            lblGrpId.Location = new Point(15, 16);
            lblGrpId.Name = "lblGrpId";
            lblGrpId.Size = new Size(56, 15);
            lblGrpId.TabIndex = 25;
            lblGrpId.Text = "Group Id";
            // 
            // cboGrpId
            // 
            cboGrpId.BackColor = SystemColors.Window;
            cboGrpId.FlatStyle = FlatStyle.Flat;
            cboGrpId.FormattingEnabled = true;
            cboGrpId.Location = new Point(106, 10);
            cboGrpId.Name = "cboGrpId";
            cboGrpId.Size = new Size(121, 23);
            cboGrpId.TabIndex = 27;
            // 
            // rotGrpDesc
            // 
            rotGrpDesc.BackColor = Color.Bisque;
            rotGrpDesc.BorderStyle = BorderStyle.Fixed3D;
            rotGrpDesc.Location = new Point(236, 9);
            rotGrpDesc.Name = "rotGrpDesc";
            rotGrpDesc.Size = new Size(236, 23);
            rotGrpDesc.TabIndex = 172;
            // 
            // lblSubGrpId
            // 
            lblSubGrpId.AutoSize = true;
            lblSubGrpId.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblSubGrpId.ImeMode = ImeMode.NoControl;
            lblSubGrpId.Location = new Point(12, 44);
            lblSubGrpId.Name = "lblSubGrpId";
            lblSubGrpId.Size = new Size(80, 15);
            lblSubGrpId.TabIndex = 173;
            lblSubGrpId.Text = "Sub Group Id";
            // 
            // lblSubGrpDesc
            // 
            lblSubGrpDesc.BackColor = Color.PowderBlue;
            lblSubGrpDesc.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblSubGrpDesc.Location = new Point(13, 70);
            lblSubGrpDesc.Name = "lblSubGrpDesc";
            lblSubGrpDesc.Size = new Size(75, 18);
            lblSubGrpDesc.TabIndex = 177;
            lblSubGrpDesc.Text = "Description";
            lblSubGrpDesc.Click += lblAdd_Click;
            // 
            // txtSubGrpDesc
            // 
            txtSubGrpDesc.Location = new Point(104, 68);
            txtSubGrpDesc.Name = "txtSubGrpDesc";
            txtSubGrpDesc.Size = new Size(368, 23);
            txtSubGrpDesc.TabIndex = 178;
            // 
            // txtSubGrpId
            // 
            txtSubGrpId.Location = new Point(105, 40);
            txtSubGrpId.Name = "txtSubGrpId";
            txtSubGrpId.Size = new Size(165, 23);
            txtSubGrpId.TabIndex = 175;
            // 
            // M_Sub_Group
            // 
            M_Sub_Group.BackColor = Color.PowderBlue;
            M_Sub_Group.BorderStyle = BorderStyle.Fixed3D;
            M_Sub_Group.Controls.Add(textBox1);
            M_Sub_Group.Controls.Add(label4);
            M_Sub_Group.Controls.Add(chkDisc);
            M_Sub_Group.Controls.Add(lblDisc);
            M_Sub_Group.Controls.Add(checkBox1);
            M_Sub_Group.Controls.Add(label2);
            M_Sub_Group.Controls.Add(chkSPChange);
            M_Sub_Group.Controls.Add(txtSTaxPer);
            M_Sub_Group.Controls.Add(lblSPChange);
            M_Sub_Group.Controls.Add(lblSTaxPer);
            M_Sub_Group.Controls.Add(txtSubGrpId);
            M_Sub_Group.Controls.Add(txtSubGrpDesc);
            M_Sub_Group.Controls.Add(lblSubGrpDesc);
            M_Sub_Group.Controls.Add(lblSubGrpId);
            M_Sub_Group.Controls.Add(rotGrpDesc);
            M_Sub_Group.Controls.Add(cboGrpId);
            M_Sub_Group.Controls.Add(lblGrpId);
            M_Sub_Group.Location = new Point(-1, -1);
            M_Sub_Group.Name = "M_Sub_Group";
            M_Sub_Group.Size = new Size(483, 166);
            M_Sub_Group.TabIndex = 0;
            M_Sub_Group.Paint += panel1_Paint;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(334, 96);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(138, 23);
            textBox1.TabIndex = 188;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ImeMode = ImeMode.NoControl;
            label4.Location = new Point(238, 100);
            label4.Name = "label4";
            label4.Size = new Size(93, 15);
            label4.TabIndex = 187;
            label4.Text = "Max Discount%";
            // 
            // chkDisc
            // 
            chkDisc.AutoSize = true;
            chkDisc.ImeMode = ImeMode.NoControl;
            chkDisc.Location = new Point(452, 133);
            chkDisc.Name = "chkDisc";
            chkDisc.Size = new Size(15, 14);
            chkDisc.TabIndex = 186;
            chkDisc.UseVisualStyleBackColor = true;
            // 
            // lblDisc
            // 
            lblDisc.AutoSize = true;
            lblDisc.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblDisc.ImeMode = ImeMode.NoControl;
            lblDisc.Location = new Point(340, 133);
            lblDisc.Name = "lblDisc";
            lblDisc.Size = new Size(104, 15);
            lblDisc.TabIndex = 185;
            lblDisc.Text = "Discount Allowed";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.ImeMode = ImeMode.NoControl;
            checkBox1.Location = new Point(67, 131);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(15, 14);
            checkBox1.TabIndex = 184;
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ImeMode = ImeMode.NoControl;
            label2.Location = new Point(15, 131);
            label2.Name = "label2";
            label2.Size = new Size(43, 15);
            label2.TabIndex = 183;
            label2.Text = "Active";
            // 
            // chkSPChange
            // 
            chkSPChange.AutoSize = true;
            chkSPChange.ImeMode = ImeMode.NoControl;
            chkSPChange.Location = new Point(262, 134);
            chkSPChange.Name = "chkSPChange";
            chkSPChange.Size = new Size(15, 14);
            chkSPChange.TabIndex = 182;
            chkSPChange.UseVisualStyleBackColor = true;
            // 
            // txtSTaxPer
            // 
            txtSTaxPer.Location = new Point(103, 96);
            txtSTaxPer.Name = "txtSTaxPer";
            txtSTaxPer.Size = new Size(126, 23);
            txtSTaxPer.TabIndex = 181;
            // 
            // lblSPChange
            // 
            lblSPChange.AutoSize = true;
            lblSPChange.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblSPChange.ImeMode = ImeMode.NoControl;
            lblSPChange.Location = new Point(150, 132);
            lblSPChange.Name = "lblSPChange";
            lblSPChange.Size = new Size(105, 15);
            lblSPChange.TabIndex = 180;
            lblSPChange.Text = "Sale Price Change";
            lblSPChange.Click += lblStatus_Click;
            // 
            // lblSTaxPer
            // 
            lblSTaxPer.AutoSize = true;
            lblSTaxPer.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblSTaxPer.ImeMode = ImeMode.NoControl;
            lblSTaxPer.Location = new Point(15, 100);
            lblSTaxPer.Name = "lblSTaxPer";
            lblSTaxPer.Size = new Size(70, 15);
            lblSTaxPer.TabIndex = 179;
            lblSTaxPer.Text = "Sales Tax %";
            // 
            // frmM_Sub_Group
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 162);
            Controls.Add(M_Sub_Group);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "frmM_Sub_Group";
            Text = "Sub Group Master";
            M_Sub_Group.ResumeLayout(false);
            M_Sub_Group.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lblGrpId;
        private ComboBox cboGrpId;
        private Label rotGrpDesc;
        private Label lblSubGrpId;
        private Label lblSubGrpDesc;
        private TextBox txtSubGrpDesc;
        private TextBox txtSubGrpId;
        private Panel M_Sub_Group;
        private CheckBox chkSPChange;
        private TextBox txtSTaxPer;
        private Label lblSPChange;
        private Label lblSTaxPer;
        private TextBox textBox1;
        private Label label4;
        private CheckBox chkDisc;
        private Label lblDisc;
        private CheckBox checkBox1;
        private Label label2;
    }
}