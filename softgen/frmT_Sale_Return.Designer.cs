namespace softgen
{
    partial class frmT_Sale_Return
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
            T_Sale_Return = new Panel();
            rotInvCust = new Label();
            lblCust = new Label();
            cboCust = new ComboBox();
            lblInvDate = new Label();
            dtpInvDate = new DateTimePicker();
            txtInvNo = new TextBox();
            lblInvNo = new Label();
            label1 = new Label();
            checkBox1 = new CheckBox();
            label2 = new Label();
            dateTimePicker1 = new DateTimePicker();
            label3 = new Label();
            comboBox1 = new ComboBox();
            label4 = new Label();
            label5 = new Label();
            T_Sale_Return.SuspendLayout();
            SuspendLayout();
            // 
            // T_Sale_Return
            // 
            T_Sale_Return.BackColor = Color.PowderBlue;
            T_Sale_Return.BorderStyle = BorderStyle.Fixed3D;
            T_Sale_Return.Controls.Add(label5);
            T_Sale_Return.Controls.Add(label4);
            T_Sale_Return.Controls.Add(label3);
            T_Sale_Return.Controls.Add(comboBox1);
            T_Sale_Return.Controls.Add(label2);
            T_Sale_Return.Controls.Add(dateTimePicker1);
            T_Sale_Return.Controls.Add(checkBox1);
            T_Sale_Return.Controls.Add(label1);
            T_Sale_Return.Controls.Add(rotInvCust);
            T_Sale_Return.Controls.Add(lblCust);
            T_Sale_Return.Controls.Add(cboCust);
            T_Sale_Return.Controls.Add(lblInvDate);
            T_Sale_Return.Controls.Add(dtpInvDate);
            T_Sale_Return.Controls.Add(txtInvNo);
            T_Sale_Return.Controls.Add(lblInvNo);
            T_Sale_Return.Location = new Point(-3, 0);
            T_Sale_Return.Name = "T_Sale_Return";
            T_Sale_Return.Size = new Size(927, 525);
            T_Sale_Return.TabIndex = 0;
            // 
            // rotInvCust
            // 
            rotInvCust.BackColor = Color.Bisque;
            rotInvCust.BorderStyle = BorderStyle.Fixed3D;
            rotInvCust.Location = new Point(590, 9);
            rotInvCust.Name = "rotInvCust";
            rotInvCust.Size = new Size(155, 23);
            rotInvCust.TabIndex = 253;
            // 
            // lblCust
            // 
            lblCust.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblCust.Location = new Point(378, 13);
            lblCust.Name = "lblCust";
            lblCust.Size = new Size(65, 18);
            lblCust.TabIndex = 252;
            lblCust.Text = "Customer";
            // 
            // cboCust
            // 
            cboCust.BackColor = SystemColors.Window;
            cboCust.FlatStyle = FlatStyle.Flat;
            cboCust.FormattingEnabled = true;
            cboCust.Location = new Point(449, 9);
            cboCust.Name = "cboCust";
            cboCust.Size = new Size(133, 23);
            cboCust.TabIndex = 251;
            // 
            // lblInvDate
            // 
            lblInvDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblInvDate.Location = new Point(212, 12);
            lblInvDate.Name = "lblInvDate";
            lblInvDate.Size = new Size(52, 18);
            lblInvDate.TabIndex = 250;
            lblInvDate.Text = "SR Date";
            // 
            // dtpInvDate
            // 
            dtpInvDate.CustomFormat = "dd/MM/yyyy";
            dtpInvDate.Enabled = false;
            dtpInvDate.Format = DateTimePickerFormat.Custom;
            dtpInvDate.Location = new Point(265, 10);
            dtpInvDate.Name = "dtpInvDate";
            dtpInvDate.Size = new Size(105, 23);
            dtpInvDate.TabIndex = 249;
            dtpInvDate.Value = new DateTime(2023, 8, 25, 0, 0, 0, 0);
            // 
            // txtInvNo
            // 
            txtInvNo.Enabled = false;
            txtInvNo.Location = new Point(91, 9);
            txtInvNo.Name = "txtInvNo";
            txtInvNo.Size = new Size(112, 23);
            txtInvNo.TabIndex = 248;
            // 
            // lblInvNo
            // 
            lblInvNo.AutoSize = true;
            lblInvNo.Font = new Font("Georgia", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblInvNo.ImeMode = ImeMode.NoControl;
            lblInvNo.Location = new Point(8, 13);
            lblInvNo.Name = "lblInvNo";
            lblInvNo.Size = new Size(79, 14);
            lblInvNo.TabIndex = 247;
            lblInvNo.Text = "Sale Ret No";
            lblInvNo.Click += lblInvNo_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Georgia", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ImeMode = ImeMode.NoControl;
            label1.Location = new Point(23, 44);
            label1.Name = "label1";
            label1.Size = new Size(64, 28);
            label1.TabIndex = 254;
            label1.Text = "Through\r\nInvoice";
            // 
            // checkBox1
            // 
            checkBox1.FlatStyle = FlatStyle.System;
            checkBox1.Location = new Point(94, 44);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(22, 24);
            checkBox1.TabIndex = 255;
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(122, 48);
            label2.Name = "label2";
            label2.Size = new Size(80, 18);
            label2.TabIndex = 257;
            label2.Text = "Invoice Date";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            dateTimePicker1.Enabled = false;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new Point(207, 46);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(105, 23);
            dateTimePicker1.TabIndex = 256;
            dateTimePicker1.Value = new DateTime(2023, 8, 25, 0, 0, 0, 0);
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(329, 49);
            label3.Name = "label3";
            label3.Size = new Size(72, 18);
            label3.TabIndex = 259;
            label3.Text = "Invoice NO.";
            label3.Click += label3_Click;
            // 
            // comboBox1
            // 
            comboBox1.BackColor = SystemColors.Window;
            comboBox1.FlatStyle = FlatStyle.Flat;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(405, 45);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(133, 23);
            comboBox1.TabIndex = 258;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label4
            // 
            label4.BackColor = Color.Bisque;
            label4.BorderStyle = BorderStyle.Fixed3D;
            label4.Location = new Point(612, 42);
            label4.Name = "label4";
            label4.Size = new Size(155, 23);
            label4.TabIndex = 260;
            // 
            // label5
            // 
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(544, 47);
            label5.Name = "label5";
            label5.Size = new Size(65, 18);
            label5.TabIndex = 261;
            label5.Text = "Customer";
            // 
            // frmT_Sale_Return
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(925, 523);
            Controls.Add(T_Sale_Return);
            Name = "frmT_Sale_Return";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmT_Sale_Return";
            T_Sale_Return.ResumeLayout(false);
            T_Sale_Return.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel T_Sale_Return;
        private Label rotInvCust;
        private Label lblCust;
        public ComboBox cboCust;
        private Label lblInvDate;
        private DateTimePicker dtpInvDate;
        public TextBox txtInvNo;
        private Label lblInvNo;
        private Label label1;
        private CheckBox checkBox1;
        private Label label2;
        private DateTimePicker dateTimePicker1;
        private Label label3;
        public ComboBox comboBox1;
        private Label label5;
        private Label label4;
    }
}