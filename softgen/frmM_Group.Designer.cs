namespace softgen
{
    partial class frmM_Group
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmM_Group));
            chkStatus = new CheckBox();
            txtSTaxPer = new TextBox();
            txtGrpDesc = new TextBox();
            txtGrpId = new TextBox();
            lblStatus = new Label();
            lblSTaxPer = new Label();
            lblGrpDesc = new Label();
            lblGrpId = new Label();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // chkStatus
            // 
            resources.ApplyResources(chkStatus, "chkStatus");
            chkStatus.Checked = true;
            chkStatus.CheckState = CheckState.Checked;
            chkStatus.Name = "chkStatus";
            chkStatus.UseVisualStyleBackColor = true;
            // 
            // txtSTaxPer
            // 
            resources.ApplyResources(txtSTaxPer, "txtSTaxPer");
            txtSTaxPer.Name = "txtSTaxPer";
            // 
            // txtGrpDesc
            // 
            resources.ApplyResources(txtGrpDesc, "txtGrpDesc");
            txtGrpDesc.Name = "txtGrpDesc";
            txtGrpDesc.Validating += txtGrpDesc_Validating;
            // 
            // txtGrpId
            // 
            txtGrpId.AutoCompleteMode = AutoCompleteMode.Suggest;
            txtGrpId.AutoCompleteSource = AutoCompleteSource.HistoryList;
            resources.ApplyResources(txtGrpId, "txtGrpId");
            txtGrpId.Name = "txtGrpId";
            txtGrpId.TextChanged += txtGrpId_TextChanged;
            txtGrpId.KeyDown += txtGrpId_KeyDown;
            txtGrpId.KeyPress += txtGrpId_KeyPress;
            txtGrpId.KeyUp += txtGrpId_KeyUp;
            txtGrpId.Validating += txtGrpId_Validating;
            // 
            // lblStatus
            // 
            resources.ApplyResources(lblStatus, "lblStatus");
            lblStatus.Name = "lblStatus";
            // 
            // lblSTaxPer
            // 
            resources.ApplyResources(lblSTaxPer, "lblSTaxPer");
            lblSTaxPer.Name = "lblSTaxPer";
            // 
            // lblGrpDesc
            // 
            resources.ApplyResources(lblGrpDesc, "lblGrpDesc");
            lblGrpDesc.Name = "lblGrpDesc";
            // 
            // lblGrpId
            // 
            resources.ApplyResources(lblGrpId, "lblGrpId");
            lblGrpId.Name = "lblGrpId";
            // 
            // textBox1
            // 
            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.HistoryList;
            resources.ApplyResources(textBox1, "textBox1");
            textBox1.Name = "textBox1";
            // 
            // frmM_Group
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.PowderBlue;
            Controls.Add(chkStatus);
            Controls.Add(txtSTaxPer);
            Controls.Add(txtGrpDesc);
            Controls.Add(txtGrpId);
            Controls.Add(lblStatus);
            Controls.Add(lblSTaxPer);
            Controls.Add(lblGrpDesc);
            Controls.Add(lblGrpId);
            Controls.Add(textBox1);
            Name = "frmM_Group";
            Deactivate += frmM_Group_Deactivate;
            FormClosing += frmM_Group_FormClosing;
            FormClosed += frmM_Group_FormClosed;
            Load += Group_Load;
            KeyPress += frmM_Group_KeyPress;
            KeyUp += frmM_Group_KeyUp;
            Resize += frmM_Group_Resize;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private CheckBox chkStatus;
        private TextBox txtSTaxPer;
        private TextBox txtGrpDesc;
        private Label lblStatus;
        private Label lblSTaxPer;
        private Label lblGrpDesc;
        private Label lblGrpId;
        public TextBox txtGrpId;
        public TextBox textBox1;
    }
}