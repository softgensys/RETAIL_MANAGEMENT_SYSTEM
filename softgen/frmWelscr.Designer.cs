namespace softgen
{
    partial class frmWelscr
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
            panel1 = new Panel();
            cmdExit = new Button();
            cmdOK = new Button();
            cboServer = new ComboBox();
            label4 = new Label();
            txtPassword = new TextBox();
            txtLogin_Id = new TextBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Thistle;
            panel1.Controls.Add(cmdExit);
            panel1.Controls.Add(cmdOK);
            panel1.Controls.Add(cboServer);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(txtPassword);
            panel1.Controls.Add(txtLogin_Id);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(3, -2);
            panel1.Name = "panel1";
            panel1.Size = new Size(426, 199);
            panel1.TabIndex = 0;
            // 
            // cmdExit
            // 
            cmdExit.BackColor = Color.FromArgb(192, 255, 255);
            cmdExit.Location = new Point(219, 164);
            cmdExit.Name = "cmdExit";
            cmdExit.Size = new Size(75, 23);
            cmdExit.TabIndex = 8;
            cmdExit.Text = "Close";
            cmdExit.UseVisualStyleBackColor = false;
            // 
            // cmdOK
            // 
            cmdOK.BackColor = Color.FromArgb(192, 255, 255);
            cmdOK.Location = new Point(94, 164);
            cmdOK.Name = "cmdOK";
            cmdOK.Size = new Size(75, 23);
            cmdOK.TabIndex = 7;
            cmdOK.Text = "Login";
            cmdOK.UseVisualStyleBackColor = false;
            cmdOK.Click += cmdOK_Click;
            // 
            // cboServer
            // 
            cboServer.FormattingEnabled = true;
            cboServer.Items.AddRange(new object[] { "SERVER" });
            cboServer.Location = new Point(143, 120);
            cboServer.Name = "cboServer";
            cboServer.Size = new Size(121, 23);
            cboServer.TabIndex = 6;
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(74, 121);
            label4.Name = "label4";
            label4.Size = new Size(72, 18);
            label4.TabIndex = 5;
            label4.Text = "Server:";
            // 
            // txtPassword
            // 
            txtPassword.BackColor = SystemColors.Menu;
            txtPassword.Location = new Point(143, 89);
            txtPassword.MaxLength = 10;
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(180, 23);
            txtPassword.TabIndex = 4;
            // 
            // txtLogin_Id
            // 
            txtLogin_Id.BackColor = SystemColors.Menu;
            txtLogin_Id.Location = new Point(143, 60);
            txtLogin_Id.Name = "txtLogin_Id";
            txtLogin_Id.Size = new Size(180, 23);
            txtLogin_Id.TabIndex = 3;
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(75, 91);
            label3.Name = "label3";
            label3.Size = new Size(72, 18);
            label3.TabIndex = 2;
            label3.Text = "Password:";
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(76, 63);
            label2.Name = "label2";
            label2.Size = new Size(47, 18);
            label2.TabIndex = 1;
            label2.Text = "User:";
            // 
            // label1
            // 
            label1.BackColor = Color.FromArgb(192, 255, 255);
            label1.BorderStyle = BorderStyle.Fixed3D;
            label1.Font = new Font("Algerian", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(26, 12);
            label1.Name = "label1";
            label1.Size = new Size(389, 23);
            label1.TabIndex = 0;
            label1.Text = "Welcome To Retail Management System";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // frmWelscr
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(432, 197);
            Controls.Add(panel1);
            Name = "frmWelscr";
            Text = "frmWelscr";
            Load += frmWelscr_Load;
            KeyUp += frmWelscr_KeyUp;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private TextBox txtLogin_Id;
        private Label label3;
        private Label label2;
        private Label label4;
        private TextBox txtPassword;
        private ComboBox cboServer;
        private Button cmdExit;
        private Button cmdOK;
    }
}