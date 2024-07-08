namespace softgen
{
    partial class frmM_Manuf
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
            M_Manuf = new Panel();
            txtNotes = new TextBox();
            lblNotes = new Label();
            txtName = new TextBox();
            lblName = new Label();
            txtManufId = new TextBox();
            lblManufId = new Label();
            lblCity = new Label();
            txtZip = new TextBox();
            lblZip = new Label();
            txtCountry = new TextBox();
            lblCountry = new Label();
            txtState = new TextBox();
            lblState = new Label();
            txrDist = new TextBox();
            lblDist = new Label();
            txtCity = new TextBox();
            txtAdd3 = new TextBox();
            txtAdd2 = new TextBox();
            txtAdd1 = new TextBox();
            lblAdd = new Label();
            M_Manuf.SuspendLayout();
            SuspendLayout();
            // 
            // M_Manuf
            // 
            M_Manuf.BackColor = Color.PowderBlue;
            M_Manuf.BorderStyle = BorderStyle.Fixed3D;
            M_Manuf.Controls.Add(txtNotes);
            M_Manuf.Controls.Add(lblNotes);
            M_Manuf.Controls.Add(txtName);
            M_Manuf.Controls.Add(lblName);
            M_Manuf.Controls.Add(txtManufId);
            M_Manuf.Controls.Add(lblManufId);
            M_Manuf.Controls.Add(lblCity);
            M_Manuf.Location = new Point(-2, 1);
            M_Manuf.Name = "M_Manuf";
            M_Manuf.Size = new Size(574, 258);
            M_Manuf.TabIndex = 0;
            // 
            // txtNotes
            // 
            txtNotes.Location = new Point(85, 205);
            txtNotes.Multiline = true;
            txtNotes.Name = "txtNotes";
            txtNotes.ScrollBars = ScrollBars.Vertical;
            txtNotes.Size = new Size(381, 41);
            txtNotes.TabIndex = 207;
            // 
            // lblNotes
            // 
            lblNotes.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblNotes.Location = new Point(15, 213);
            lblNotes.Name = "lblNotes";
            lblNotes.Size = new Size(66, 18);
            lblNotes.TabIndex = 206;
            lblNotes.Text = "Notes";
            // 
            // txtName
            // 
            txtName.Location = new Point(266, 9);
            txtName.Name = "txtName";
            txtName.Size = new Size(271, 23);
            txtName.TabIndex = 24;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblName.ImeMode = ImeMode.NoControl;
            lblName.Location = new Point(212, 13);
            lblName.Name = "lblName";
            lblName.Size = new Size(40, 15);
            lblName.TabIndex = 23;
            lblName.Text = "Name";
            // 
            // txtManufId
            // 
            txtManufId.Location = new Point(85, 9);
            txtManufId.Name = "txtManufId";
            txtManufId.Size = new Size(110, 23);
            txtManufId.TabIndex = 22;
            txtManufId.KeyUp += txtManufId_KeyUp;
            // 
            // lblManufId
            // 
            lblManufId.AutoSize = true;
            lblManufId.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblManufId.ImeMode = ImeMode.NoControl;
            lblManufId.Location = new Point(12, 13);
            lblManufId.Name = "lblManufId";
            lblManufId.Size = new Size(57, 15);
            lblManufId.TabIndex = 21;
            lblManufId.Text = "Manuf Id";
            // 
            // lblCity
            // 
            lblCity.BackColor = Color.PowderBlue;
            lblCity.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblCity.Location = new Point(18, 127);
            lblCity.Name = "lblCity";
            lblCity.Size = new Size(63, 18);
            lblCity.TabIndex = 106;
            lblCity.Text = "City";
            // 
            // txtZip
            // 
            txtZip.Location = new Point(86, 181);
            txtZip.Name = "txtZip";
            txtZip.Size = new Size(178, 23);
            txtZip.TabIndex = 115;
            // 
            // lblZip
            // 
            lblZip.BackColor = Color.PowderBlue;
            lblZip.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblZip.Location = new Point(16, 184);
            lblZip.Name = "lblZip";
            lblZip.Size = new Size(62, 18);
            lblZip.TabIndex = 114;
            lblZip.Text = "Zip";
            // 
            // txtCountry
            // 
            txtCountry.Location = new Point(352, 154);
            txtCountry.Name = "txtCountry";
            txtCountry.Size = new Size(207, 23);
            txtCountry.TabIndex = 113;
            // 
            // lblCountry
            // 
            lblCountry.BackColor = Color.PowderBlue;
            lblCountry.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblCountry.Location = new Point(281, 157);
            lblCountry.Name = "lblCountry";
            lblCountry.Size = new Size(77, 18);
            lblCountry.TabIndex = 112;
            lblCountry.Text = "Country";
            // 
            // txtState
            // 
            txtState.Location = new Point(86, 154);
            txtState.Name = "txtState";
            txtState.Size = new Size(178, 23);
            txtState.TabIndex = 111;
            // 
            // lblState
            // 
            lblState.BackColor = Color.PowderBlue;
            lblState.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblState.Location = new Point(16, 157);
            lblState.Name = "lblState";
            lblState.Size = new Size(66, 18);
            lblState.TabIndex = 110;
            lblState.Text = "State";
            // 
            // txrDist
            // 
            txrDist.Location = new Point(352, 127);
            txrDist.Name = "txrDist";
            txrDist.Size = new Size(207, 23);
            txrDist.TabIndex = 109;
            // 
            // lblDist
            // 
            lblDist.BackColor = Color.PowderBlue;
            lblDist.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblDist.Location = new Point(281, 130);
            lblDist.Name = "lblDist";
            lblDist.Size = new Size(77, 18);
            lblDist.TabIndex = 108;
            lblDist.Text = "District";
            // 
            // txtCity
            // 
            txtCity.Location = new Point(86, 127);
            txtCity.Name = "txtCity";
            txtCity.Size = new Size(178, 23);
            txtCity.TabIndex = 107;
            // 
            // txtAdd3
            // 
            txtAdd3.Location = new Point(86, 100);
            txtAdd3.Name = "txtAdd3";
            txtAdd3.Size = new Size(473, 23);
            txtAdd3.TabIndex = 105;
            // 
            // txtAdd2
            // 
            txtAdd2.Location = new Point(86, 74);
            txtAdd2.Name = "txtAdd2";
            txtAdd2.Size = new Size(473, 23);
            txtAdd2.TabIndex = 104;
            // 
            // txtAdd1
            // 
            txtAdd1.Location = new Point(86, 48);
            txtAdd1.Name = "txtAdd1";
            txtAdd1.Size = new Size(473, 23);
            txtAdd1.TabIndex = 103;
            // 
            // lblAdd
            // 
            lblAdd.BackColor = Color.PowderBlue;
            lblAdd.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblAdd.Location = new Point(15, 52);
            lblAdd.Name = "lblAdd";
            lblAdd.Size = new Size(70, 18);
            lblAdd.TabIndex = 102;
            lblAdd.Text = "Address";
            // 
            // frmM_Manuf
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(567, 255);
            Controls.Add(txtZip);
            Controls.Add(lblZip);
            Controls.Add(txtCountry);
            Controls.Add(lblCountry);
            Controls.Add(txtState);
            Controls.Add(lblState);
            Controls.Add(txrDist);
            Controls.Add(lblDist);
            Controls.Add(txtCity);
            Controls.Add(txtAdd3);
            Controls.Add(txtAdd2);
            Controls.Add(txtAdd1);
            Controls.Add(lblAdd);
            Controls.Add(M_Manuf);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "frmM_Manuf";
            Text = "Manufacturer Master";
            FormClosing += frmM_Manuf_FormClosing;
            FormClosed += frmM_Manuf_FormClosed;
            Load += frmM_Manuf_Load;
            Validating += frmM_Manuf_Validating;
            M_Manuf.ResumeLayout(false);
            M_Manuf.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel M_Manuf;
        private TextBox txtName;
        private Label lblName;
        private TextBox txtManufId;
        private Label lblManufId;
        private TextBox txtZip;
        private Label lblZip;
        private TextBox txtCountry;
        private Label lblCountry;
        private TextBox txtState;
        private Label lblState;
        private TextBox txrDist;
        private Label lblDist;
        private TextBox txtCity;
        private Label lblCity;
        private TextBox txtAdd3;
        private TextBox txtAdd2;
        private Label lblAdd;
        private TextBox txtNotes;
        private Label lblNotes;
        public TextBox txtAdd1;
    }
}