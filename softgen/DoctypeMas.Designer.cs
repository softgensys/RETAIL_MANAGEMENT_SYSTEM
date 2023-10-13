namespace softgen
{
    partial class DoctypeMas
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
            txt = new TextBox();
            txtstartno = new TextBox();
            label4 = new Label();
            textBox3 = new TextBox();
            label9 = new Label();
            label1 = new Label();
            SuspendLayout();
            // 
            // txt
            // 
            txt.Location = new Point(128, 40);
            txt.Name = "txt";
            txt.Size = new Size(290, 23);
            txt.TabIndex = 184;
            // 
            // txtstartno
            // 
            txtstartno.Location = new Point(128, 9);
            txtstartno.Name = "txtstartno";
            txtstartno.Size = new Size(116, 23);
            txtstartno.TabIndex = 183;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ImeMode = ImeMode.NoControl;
            label4.Location = new Point(16, 13);
            label4.Name = "label4";
            label4.Size = new Size(109, 15);
            label4.TabIndex = 182;
            label4.Text = "Document Type Id";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(127, 71);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.ScrollBars = ScrollBars.Vertical;
            textBox3.Size = new Size(293, 123);
            textBox3.TabIndex = 190;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label9.ImeMode = ImeMode.NoControl;
            label9.Location = new Point(75, 82);
            label9.Name = "label9";
            label9.Size = new Size(40, 15);
            label9.TabIndex = 189;
            label9.Text = "Notes";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ImeMode = ImeMode.NoControl;
            label1.Location = new Point(20, 45);
            label1.Name = "label1";
            label1.Size = new Size(102, 15);
            label1.TabIndex = 191;
            label1.Text = "Document Name";
            // 
            // DoctypeMas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.PowderBlue;
            ClientSize = new Size(434, 204);
            Controls.Add(label1);
            Controls.Add(textBox3);
            Controls.Add(label9);
            Controls.Add(txt);
            Controls.Add(txtstartno);
            Controls.Add(label4);
            Name = "DoctypeMas";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Document Type Master";
            FormClosed += DoctypeMas_FormClosed;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txt;
        private TextBox txtstartno;
        private Label label4;
        private TextBox textBox3;
        private Label label9;
        private Label label1;
    }
}