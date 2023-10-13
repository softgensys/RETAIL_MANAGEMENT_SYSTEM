namespace softgen
{
    partial class frmStart
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
        public void InitializeComponent()
        {
            panel1 = new Panel();
            lblDate = new Label();
            lblMsg = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.PeachPuff;
            panel1.Controls.Add(lblDate);
            panel1.Controls.Add(lblMsg);
            panel1.Location = new Point(1, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(499, 266);
            panel1.TabIndex = 0;
            // 
            // lblDate
            // 
            lblDate.BackColor = Color.PeachPuff;
            lblDate.BorderStyle = BorderStyle.Fixed3D;
            lblDate.Font = new Font("Georgia", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblDate.ForeColor = Color.MidnightBlue;
            lblDate.Location = new Point(311, 171);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(149, 23);
            lblDate.TabIndex = 1;
            // 
            // lblMsg
            // 
            lblMsg.BackColor = Color.PeachPuff;
            lblMsg.BorderStyle = BorderStyle.Fixed3D;
            lblMsg.Font = new Font("Georgia", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            lblMsg.ForeColor = Color.MidnightBlue;
            lblMsg.Location = new Point(311, 103);
            lblMsg.Name = "lblMsg";
            lblMsg.Size = new Size(149, 23);
            lblMsg.TabIndex = 0;
            lblMsg.Click += label1_Click;
            // 
            // frmStart
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(496, 264);
            Controls.Add(panel1);
            Name = "frmStart";
            Text = "frmStart";
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        public Panel panel1;
        public Label lblMsg;
        public Label lblDate;
    }
}