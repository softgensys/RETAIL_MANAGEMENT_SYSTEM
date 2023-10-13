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
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            chkStatus = new CheckBox();
            txtSTaxPer = new TextBox();
            txtGrpDesc = new TextBox();
            txtGrpId = new TextBox();
            lblStatus = new Label();
            lblSTaxPer = new Label();
            lblGrpDesc = new Label();
            lblGrpId = new Label();
            SuspendLayout();
            // 
            // button3
            // 
            resources.ApplyResources(button3, "button3");
            button3.Name = "button3";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            resources.ApplyResources(button2, "button2");
            button2.Name = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            resources.ApplyResources(button1, "button1");
            button1.Name = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // chkStatus
            // 
            resources.ApplyResources(chkStatus, "chkStatus");
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
            // 
            // txtGrpId
            // 
            resources.ApplyResources(txtGrpId, "txtGrpId");
            txtGrpId.Name = "txtGrpId";
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
            // frmM_Group
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.PowderBlue;
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(chkStatus);
            Controls.Add(txtSTaxPer);
            Controls.Add(txtGrpDesc);
            Controls.Add(txtGrpId);
            Controls.Add(lblStatus);
            Controls.Add(lblSTaxPer);
            Controls.Add(lblGrpDesc);
            Controls.Add(lblGrpId);
            Name = "frmM_Group";
            FormClosed += Group_FormClosed;
            Load += Group_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion


        private Button button3;
        private Button button2;
        private Button button1;
        private CheckBox chkStatus;
        private TextBox txtSTaxPer;
        private TextBox txtGrpDesc;
        private TextBox txtGrpId;
        private Label lblStatus;
        private Label lblSTaxPer;
        private Label lblGrpDesc;
        private Label lblGrpId;
    }
}