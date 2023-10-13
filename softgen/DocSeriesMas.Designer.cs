namespace softgen
{
    partial class DocSeriesMas
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
            lbldoctype = new Label();
            doctypeidcomb = new ComboBox();
            grplbl = new Label();
            panel1 = new Panel();
            todt = new DateTimePicker();
            label3 = new Label();
            fromdt = new DateTimePicker();
            label2 = new Label();
            label1 = new Label();
            panel2 = new Panel();
            panel3 = new Panel();
            lblnextno = new Label();
            label7 = new Label();
            txtendno = new TextBox();
            label5 = new Label();
            txtstartno = new TextBox();
            label4 = new Label();
            panel4 = new Panel();
            label6 = new Label();
            label9 = new Label();
            textBox2 = new TextBox();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // lbldoctype
            // 
            lbldoctype.BackColor = Color.Bisque;
            lbldoctype.BorderStyle = BorderStyle.Fixed3D;
            lbldoctype.Location = new Point(262, 6);
            lbldoctype.Name = "lbldoctype";
            lbldoctype.Size = new Size(214, 23);
            lbldoctype.TabIndex = 170;
            // 
            // doctypeidcomb
            // 
            doctypeidcomb.AutoCompleteMode = AutoCompleteMode.Suggest;
            doctypeidcomb.DropDownHeight = 80;
            doctypeidcomb.FormattingEnabled = true;
            doctypeidcomb.IntegralHeight = false;
            doctypeidcomb.Location = new Point(127, 6);
            doctypeidcomb.Name = "doctypeidcomb";
            doctypeidcomb.Size = new Size(124, 23);
            doctypeidcomb.TabIndex = 169;
            doctypeidcomb.SelectedIndexChanged += doctypeidcomb_SelectedIndexChanged;
            doctypeidcomb.MouseDown += doctypeidcomb_MouseDown;
            // 
            // grplbl
            // 
            grplbl.AutoSize = true;
            grplbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            grplbl.ImeMode = ImeMode.NoControl;
            grplbl.Location = new Point(12, 9);
            grplbl.Name = "grplbl";
            grplbl.Size = new Size(109, 15);
            grplbl.TabIndex = 168;
            grplbl.Text = "Document Type Id";
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(todt);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(fromdt);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(12, 52);
            panel1.Name = "panel1";
            panel1.Size = new Size(474, 55);
            panel1.TabIndex = 171;
            // 
            // todt
            // 
            todt.CustomFormat = "dd/mm/yyyy";
            todt.Location = new Point(323, 16);
            todt.Name = "todt";
            todt.Size = new Size(121, 23);
            todt.TabIndex = 177;
            todt.Value = new DateTime(2023, 7, 2, 15, 24, 28, 0);
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ImeMode = ImeMode.NoControl;
            label3.Location = new Point(263, 20);
            label3.Name = "label3";
            label3.Size = new Size(50, 15);
            label3.TabIndex = 176;
            label3.Text = "To Date";
            // 
            // fromdt
            // 
            fromdt.CustomFormat = "dd/MM/yyyy";
            fromdt.Location = new Point(93, 16);
            fromdt.Name = "fromdt";
            fromdt.Size = new Size(123, 23);
            fromdt.TabIndex = 175;
            fromdt.Value = new DateTime(2023, 7, 2, 15, 24, 28, 0);
            fromdt.Leave += fromdt_Leave;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ImeMode = ImeMode.NoControl;
            label2.Location = new Point(24, 20);
            label2.Name = "label2";
            label2.Size = new Size(66, 15);
            label2.TabIndex = 174;
            label2.Text = "From Date";
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ImeMode = ImeMode.NoControl;
            label1.Location = new Point(3, 5);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 172;
            label1.Text = "Period";
            // 
            // panel2
            // 
            panel2.Controls.Add(label1);
            panel2.Location = new Point(25, 40);
            panel2.Name = "panel2";
            panel2.Size = new Size(52, 24);
            panel2.TabIndex = 173;
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(lblnextno);
            panel3.Controls.Add(label7);
            panel3.Controls.Add(txtendno);
            panel3.Controls.Add(label5);
            panel3.Controls.Add(txtstartno);
            panel3.Controls.Add(label4);
            panel3.Location = new Point(12, 125);
            panel3.Name = "panel3";
            panel3.Size = new Size(474, 73);
            panel3.TabIndex = 178;
            // 
            // lblnextno
            // 
            lblnextno.BackColor = Color.Bisque;
            lblnextno.BorderStyle = BorderStyle.Fixed3D;
            lblnextno.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblnextno.ImeMode = ImeMode.NoControl;
            lblnextno.Location = new Point(93, 45);
            lblnextno.Name = "lblnextno";
            lblnextno.Size = new Size(110, 22);
            lblnextno.TabIndex = 183;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label7.ImeMode = ImeMode.NoControl;
            label7.Location = new Point(31, 50);
            label7.Name = "label7";
            label7.Size = new Size(57, 15);
            label7.TabIndex = 182;
            label7.Text = "Next No.";
            // 
            // txtendno
            // 
            txtendno.Location = new Point(325, 13);
            txtendno.Name = "txtendno";
            txtendno.Size = new Size(112, 23);
            txtendno.TabIndex = 181;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ImeMode = ImeMode.NoControl;
            label5.Location = new Point(266, 17);
            label5.Name = "label5";
            label5.Size = new Size(49, 15);
            label5.TabIndex = 180;
            label5.Text = "End No.";
            // 
            // txtstartno
            // 
            txtstartno.Location = new Point(93, 13);
            txtstartno.Name = "txtstartno";
            txtstartno.Size = new Size(112, 23);
            txtstartno.TabIndex = 179;
            txtstartno.TextChanged += txtstartno_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ImeMode = ImeMode.NoControl;
            label4.Location = new Point(32, 17);
            label4.Name = "label4";
            label4.Size = new Size(57, 15);
            label4.TabIndex = 178;
            label4.Text = "Start No.";
            // 
            // panel4
            // 
            panel4.Controls.Add(label6);
            panel4.Location = new Point(25, 113);
            panel4.Name = "panel4";
            panel4.Size = new Size(111, 21);
            panel4.TabIndex = 174;
            // 
            // label6
            // 
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ImeMode = ImeMode.NoControl;
            label6.Location = new Point(3, 2);
            label6.Name = "label6";
            label6.Size = new Size(105, 15);
            label6.TabIndex = 172;
            label6.Text = "Document Series";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label9.ImeMode = ImeMode.NoControl;
            label9.Location = new Point(20, 218);
            label9.Name = "label9";
            label9.Size = new Size(40, 15);
            label9.TabIndex = 183;
            label9.Text = "Notes";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(68, 205);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(401, 41);
            textBox2.TabIndex = 184;
            // 
            // DocSeriesMas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.PowderBlue;
            ClientSize = new Size(498, 254);
            Controls.Add(textBox2);
            Controls.Add(label9);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(lbldoctype);
            Controls.Add(doctypeidcomb);
            Controls.Add(panel1);
            Controls.Add(grplbl);
            Name = "DocSeriesMas";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Document Series Master";
            FormClosed += DocSeriesMas_FormClosed;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbldoctype;
        private ComboBox doctypeidcomb;
        private Label grplbl;
        private Panel panel1;
        private Label label1;
        private Panel panel2;
        private Label label2;
        private DateTimePicker fromdt;
        private DateTimePicker todt;
        private Label label3;
        private Panel panel3;
        private Panel panel4;
        private Label label6;
        private Label label4;
        private Label lblnextno;
        private Label label7;
        private TextBox txtendno;
        private Label label5;
        private TextBox txtstartno;
        private Label label9;
        private TextBox textBox2;
    }
}