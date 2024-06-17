namespace softgen
{
    partial class frmU_GrantScreenPermission
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            panel1 = new Panel();
            lbluser = new Label();
            cboUser = new ComboBox();
            dataGridView1 = new DataGridView();
            FormName = new DataGridViewTextBoxColumn();
            ASSIGN = new DataGridViewCheckBoxColumn();
            ADD = new DataGridViewCheckBoxColumn();
            MODIFY = new DataGridViewCheckBoxColumn();
            DELETE = new DataGridViewCheckBoxColumn();
            ENQUIRE = new DataGridViewCheckBoxColumn();
            POST = new DataGridViewCheckBoxColumn();
            PRINT = new DataGridViewCheckBoxColumn();
            splitter1 = new Splitter();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.PowderBlue;
            panel1.Controls.Add(lbluser);
            panel1.Controls.Add(cboUser);
            panel1.Controls.Add(dataGridView1);
            panel1.Controls.Add(splitter1);
            panel1.Location = new Point(-7, -3);
            panel1.Name = "panel1";
            panel1.Size = new Size(959, 567);
            panel1.TabIndex = 1;
            // 
            // lbluser
            // 
            lbluser.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbluser.Location = new Point(73, 30);
            lbluser.Name = "lbluser";
            lbluser.Size = new Size(58, 19);
            lbluser.TabIndex = 261;
            lbluser.Text = "Login Id";
            // 
            // cboUser
            // 
            cboUser.FormattingEnabled = true;
            cboUser.Location = new Point(137, 29);
            cboUser.Name = "cboUser";
            cboUser.Size = new Size(124, 23);
            cboUser.TabIndex = 260;
            cboUser.SelectedIndexChanged += cboUser_SelectedIndexChanged;
            cboUser.KeyDown += cboUser_KeyDown;
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = Color.PeachPuff;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.ActiveCaption;
            dataGridViewCellStyle1.Font = new Font("Times New Roman", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { FormName, ASSIGN, ADD, MODIFY, DELETE, ENQUIRE, POST, PRINT });
            dataGridView1.Location = new Point(62, 99);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(783, 403);
            dataGridView1.TabIndex = 259;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
            dataGridView1.CurrentCellDirtyStateChanged += dataGridView1_CurrentCellDirtyStateChanged;
            // 
            // FormName
            // 
            FormName.Frozen = true;
            FormName.HeaderText = "FORM NAME ";
            FormName.Name = "FormName";
            FormName.ReadOnly = true;
            FormName.Width = 250;
            // 
            // ASSIGN
            // 
            ASSIGN.Frozen = true;
            ASSIGN.HeaderText = "ASSIGN";
            ASSIGN.Name = "ASSIGN";
            ASSIGN.Width = 70;
            // 
            // ADD
            // 
            ADD.Frozen = true;
            ADD.HeaderText = "ADD";
            ADD.Name = "ADD";
            ADD.Width = 70;
            // 
            // MODIFY
            // 
            MODIFY.Frozen = true;
            MODIFY.HeaderText = "MODIFY";
            MODIFY.Name = "MODIFY";
            MODIFY.Width = 70;
            // 
            // DELETE
            // 
            DELETE.HeaderText = "DELETE";
            DELETE.Name = "DELETE";
            DELETE.Width = 70;
            // 
            // ENQUIRE
            // 
            ENQUIRE.HeaderText = "ENQUIRE";
            ENQUIRE.Name = "ENQUIRE";
            ENQUIRE.Width = 70;
            // 
            // POST
            // 
            POST.HeaderText = "POST";
            POST.Name = "POST";
            POST.Width = 70;
            // 
            // PRINT
            // 
            PRINT.HeaderText = "PRINT";
            PRINT.Name = "PRINT";
            PRINT.Width = 70;
            // 
            // splitter1
            // 
            splitter1.BorderStyle = BorderStyle.Fixed3D;
            splitter1.Location = new Point(0, 0);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(3, 567);
            splitter1.TabIndex = 258;
            splitter1.TabStop = false;
            // 
            // frmU_GrantScreenPermission
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(917, 547);
            Controls.Add(panel1);
            Name = "frmU_GrantScreenPermission";
            Text = "frmU_GrantScreenPermission";
            Activated += frmU_GrantScreenPermission_Activated;
            FormClosed += frmU_GrantScreenPermission_FormClosed;
            Load += frmU_GrantScreenPermission_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Splitter splitter1;
        public DataGridView dataGridView1;
        private Label lbluser;
        public ComboBox cboUser;
        private DataGridViewTextBoxColumn FormName;
        private DataGridViewCheckBoxColumn ASSIGN;
        private DataGridViewCheckBoxColumn ADD;
        private DataGridViewCheckBoxColumn MODIFY;
        private DataGridViewCheckBoxColumn DELETE;
        private DataGridViewCheckBoxColumn ENQUIRE;
        private DataGridViewCheckBoxColumn POST;
        private DataGridViewCheckBoxColumn PRINT;
    }
}