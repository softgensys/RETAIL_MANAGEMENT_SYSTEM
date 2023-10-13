namespace softgen
{
    partial class frmM_ItemType
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
            M_ItemType = new Panel();
            txtITypeDesc = new TextBox();
            txtITypeId = new TextBox();
            lblITypeDesc = new Label();
            lblITypeId = new Label();
            M_ItemType.SuspendLayout();
            SuspendLayout();
            // 
            // M_ItemType
            // 
            M_ItemType.BackColor = Color.PowderBlue;
            M_ItemType.BorderStyle = BorderStyle.Fixed3D;
            M_ItemType.Controls.Add(txtITypeDesc);
            M_ItemType.Controls.Add(txtITypeId);
            M_ItemType.Controls.Add(lblITypeDesc);
            M_ItemType.Controls.Add(lblITypeId);
            M_ItemType.Location = new Point(-1, 0);
            M_ItemType.Name = "M_ItemType";
            M_ItemType.Size = new Size(422, 112);
            M_ItemType.TabIndex = 0;
            M_ItemType.Paint += panel1_Paint;
            // 
            // txtITypeDesc
            // 
            txtITypeDesc.Location = new Point(101, 48);
            txtITypeDesc.Name = "txtITypeDesc";
            txtITypeDesc.Size = new Size(204, 23);
            txtITypeDesc.TabIndex = 21;
            // 
            // txtITypeId
            // 
            txtITypeId.Location = new Point(101, 10);
            txtITypeId.Name = "txtITypeId";
            txtITypeId.Size = new Size(107, 23);
            txtITypeId.TabIndex = 20;
            // 
            // lblITypeDesc
            // 
            lblITypeDesc.AutoSize = true;
            lblITypeDesc.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblITypeDesc.ImeMode = ImeMode.NoControl;
            lblITypeDesc.Location = new Point(23, 51);
            lblITypeDesc.Name = "lblITypeDesc";
            lblITypeDesc.Size = new Size(71, 15);
            lblITypeDesc.TabIndex = 18;
            lblITypeDesc.Text = "Description";
            // 
            // lblITypeId
            // 
            lblITypeId.AutoSize = true;
            lblITypeId.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblITypeId.ImeMode = ImeMode.NoControl;
            lblITypeId.Location = new Point(17, 14);
            lblITypeId.Name = "lblITypeId";
            lblITypeId.Size = new Size(77, 15);
            lblITypeId.TabIndex = 17;
            lblITypeId.Text = "Item Type Id";
            // 
            // frmM_ItemType
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(421, 109);
            Controls.Add(M_ItemType);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "frmM_ItemType";
            Text = "Item Type Master";
            M_ItemType.ResumeLayout(false);
            M_ItemType.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel M_ItemType;
        private TextBox txtITypeDesc;
        private TextBox txtITypeId;
        private Label lblITypeDesc;
        private Label lblITypeId;
    }
}