namespace softgen
{
    partial class frmM_Branch
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
            lblBranchID = new Label();
            txtBranchID = new TextBox();
            txtAdd1 = new TextBox();
            lblAdd = new Label();
            txtAdd2 = new TextBox();
            txtAdd3 = new TextBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.PowderBlue;
            panel1.Controls.Add(txtAdd3);
            panel1.Controls.Add(txtAdd2);
            panel1.Controls.Add(txtAdd1);
            panel1.Controls.Add(lblAdd);
            panel1.Controls.Add(txtBranchID);
            panel1.Controls.Add(lblBranchID);
            panel1.Location = new Point(-1, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(395, 169);
            panel1.TabIndex = 0;
            // 
            // lblBranchID
            // 
            lblBranchID.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblBranchID.Location = new Point(11, 12);
            lblBranchID.Name = "lblBranchID";
            lblBranchID.Size = new Size(70, 18);
            lblBranchID.TabIndex = 0;
            lblBranchID.Text = "Branch Id";
            // 
            // txtBranchID
            // 
            txtBranchID.Location = new Point(82, 9);
            txtBranchID.Name = "txtBranchID";
            txtBranchID.Size = new Size(195, 23);
            txtBranchID.TabIndex = 83;
            // 
            // txtAdd1
            // 
            txtAdd1.Location = new Point(82, 52);
            txtAdd1.Name = "txtAdd1";
            txtAdd1.Size = new Size(285, 23);
            txtAdd1.TabIndex = 85;
            // 
            // lblAdd
            // 
            lblAdd.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblAdd.Location = new Point(11, 55);
            lblAdd.Name = "lblAdd";
            lblAdd.Size = new Size(70, 18);
            lblAdd.TabIndex = 84;
            lblAdd.Text = "Address";
            // 
            // txtAdd2
            // 
            txtAdd2.Location = new Point(82, 83);
            txtAdd2.Name = "txtAdd2";
            txtAdd2.Size = new Size(285, 23);
            txtAdd2.TabIndex = 86;
            // 
            // txtAdd3
            // 
            txtAdd3.Location = new Point(82, 114);
            txtAdd3.Name = "txtAdd3";
            txtAdd3.Size = new Size(285, 23);
            txtAdd3.TabIndex = 87;
            // 
            // frmM_Branch
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(389, 166);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "frmM_Branch";
            Text = "MasBranch";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label lblBranchID;
        private TextBox txtAdd3;
        private TextBox txtAdd2;
        private TextBox txtAdd1;
        private Label lblAdd;
        private TextBox txtBranchID;
    }
}