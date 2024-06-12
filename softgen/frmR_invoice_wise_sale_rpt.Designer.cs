namespace softgen
{
    partial class frmR_invoice_wise_sale_rpt
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
            splitter1 = new Splitter();
            chkInvGstDet = new CheckBox();
            chkItemGstDet = new CheckBox();
            chkInvtIME = new CheckBox();
            lblToDt = new Label();
            dtpToDate = new DateTimePicker();
            lblFromdt = new Label();
            dtpFromDate = new DateTimePicker();
            chkGstdet = new CheckBox();
            lblPaym = new Label();
            cboPaymod = new ComboBox();
            chkPaymAll = new CheckBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.PowderBlue;
            panel1.Controls.Add(splitter1);
            panel1.Controls.Add(chkInvGstDet);
            panel1.Controls.Add(chkItemGstDet);
            panel1.Controls.Add(chkInvtIME);
            panel1.Controls.Add(lblToDt);
            panel1.Controls.Add(dtpToDate);
            panel1.Controls.Add(lblFromdt);
            panel1.Controls.Add(dtpFromDate);
            panel1.Controls.Add(chkGstdet);
            panel1.Controls.Add(lblPaym);
            panel1.Controls.Add(cboPaymod);
            panel1.Controls.Add(chkPaymAll);
            panel1.Location = new Point(-6, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(480, 180);
            panel1.TabIndex = 0;
            // 
            // splitter1
            // 
            splitter1.BorderStyle = BorderStyle.Fixed3D;
            splitter1.Location = new Point(0, 0);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(3, 180);
            splitter1.TabIndex = 258;
            splitter1.TabStop = false;
            // 
            // chkInvGstDet
            // 
            chkInvGstDet.AutoSize = true;
            chkInvGstDet.CheckAlign = ContentAlignment.MiddleRight;
            chkInvGstDet.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            chkInvGstDet.Location = new Point(173, 56);
            chkInvGstDet.Name = "chkInvGstDet";
            chkInvGstDet.Size = new Size(147, 19);
            chkInvGstDet.TabIndex = 257;
            chkInvGstDet.Text = "InvoiceWise/Gst Detail";
            chkInvGstDet.UseVisualStyleBackColor = true;
            chkInvGstDet.CheckedChanged += chkInvGstDet_CheckedChanged;
            // 
            // chkItemGstDet
            // 
            chkItemGstDet.AutoSize = true;
            chkItemGstDet.CheckAlign = ContentAlignment.MiddleRight;
            chkItemGstDet.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            chkItemGstDet.Location = new Point(16, 56);
            chkItemGstDet.Name = "chkItemGstDet";
            chkItemGstDet.Size = new Size(136, 19);
            chkItemGstDet.TabIndex = 256;
            chkItemGstDet.Text = "With Item/Gst Detail";
            chkItemGstDet.UseVisualStyleBackColor = true;
            chkItemGstDet.CheckedChanged += chkItemGstDet_CheckedChanged;
            // 
            // chkInvtIME
            // 
            chkInvtIME.AutoSize = true;
            chkInvtIME.CheckAlign = ContentAlignment.MiddleRight;
            chkInvtIME.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            chkInvtIME.Location = new Point(354, 56);
            chkInvtIME.Name = "chkInvtIME";
            chkInvtIME.Size = new Size(97, 19);
            chkInvtIME.TabIndex = 255;
            chkInvtIME.Text = "Invoice/Time";
            chkInvtIME.UseVisualStyleBackColor = true;
            // 
            // lblToDt
            // 
            lblToDt.AutoSize = true;
            lblToDt.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblToDt.Location = new Point(250, 114);
            lblToDt.Name = "lblToDt";
            lblToDt.Size = new Size(48, 15);
            lblToDt.TabIndex = 253;
            lblToDt.Text = "To Date";
            // 
            // dtpToDate
            // 
            dtpToDate.CustomFormat = "dd/MM/yyyy";
            dtpToDate.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dtpToDate.Format = DateTimePickerFormat.Custom;
            dtpToDate.Location = new Point(305, 112);
            dtpToDate.Name = "dtpToDate";
            dtpToDate.Size = new Size(101, 23);
            dtpToDate.TabIndex = 252;
            dtpToDate.Value = new DateTime(2024, 6, 3, 0, 0, 0, 0);
            dtpToDate.ValueChanged += dtpToDate_ValueChanged;
            // 
            // lblFromdt
            // 
            lblFromdt.AutoSize = true;
            lblFromdt.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblFromdt.Location = new Point(47, 114);
            lblFromdt.Name = "lblFromdt";
            lblFromdt.Size = new Size(63, 15);
            lblFromdt.TabIndex = 251;
            lblFromdt.Text = "From Date";
            // 
            // dtpFromDate
            // 
            dtpFromDate.CustomFormat = "dd/MM/yyyy";
            dtpFromDate.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dtpFromDate.Format = DateTimePickerFormat.Custom;
            dtpFromDate.Location = new Point(115, 112);
            dtpFromDate.Name = "dtpFromDate";
            dtpFromDate.Size = new Size(105, 23);
            dtpFromDate.TabIndex = 250;
            dtpFromDate.Value = new DateTime(2024, 6, 3, 0, 0, 0, 0);
            dtpFromDate.ValueChanged += dtpFromDate_ValueChanged;
            // 
            // chkGstdet
            // 
            chkGstdet.AutoSize = true;
            chkGstdet.CheckAlign = ContentAlignment.MiddleRight;
            chkGstdet.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            chkGstdet.Location = new Point(136, 16);
            chkGstdet.Name = "chkGstdet";
            chkGstdet.Size = new Size(106, 19);
            chkGstdet.TabIndex = 3;
            chkGstdet.Text = "With Gst Detail";
            chkGstdet.UseVisualStyleBackColor = true;
            chkGstdet.CheckedChanged += chkGstdet_CheckedChanged;
            // 
            // lblPaym
            // 
            lblPaym.AutoSize = true;
            lblPaym.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblPaym.Location = new Point(253, 18);
            lblPaym.Name = "lblPaym";
            lblPaym.Size = new Size(88, 15);
            lblPaym.TabIndex = 2;
            lblPaym.Text = "Payment Mode";
            // 
            // cboPaymod
            // 
            cboPaymod.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            cboPaymod.FormattingEnabled = true;
            cboPaymod.Location = new Point(344, 14);
            cboPaymod.Name = "cboPaymod";
            cboPaymod.Size = new Size(121, 23);
            cboPaymod.TabIndex = 1;
            cboPaymod.DropDown += cboPaymod_DropDown;
            cboPaymod.SelectedIndexChanged += cboPaymod_SelectedIndexChanged;
            cboPaymod.SelectedValueChanged += cboPaymod_SelectedValueChanged;
            // 
            // chkPaymAll
            // 
            chkPaymAll.AutoSize = true;
            chkPaymAll.CheckAlign = ContentAlignment.MiddleRight;
            chkPaymAll.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            chkPaymAll.Location = new Point(18, 14);
            chkPaymAll.Name = "chkPaymAll";
            chkPaymAll.Size = new Size(93, 19);
            chkPaymAll.TabIndex = 0;
            chkPaymAll.Text = "All Paymode";
            chkPaymAll.UseVisualStyleBackColor = true;
            chkPaymAll.CheckedChanged += chkPaymAll_CheckedChanged;
            // 
            // frmR_invoice_wise_sale_rpt
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(464, 152);
            Controls.Add(panel1);
            Name = "frmR_invoice_wise_sale_rpt";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Invoicewise Sale Report";
            FormClosed += frmR_invoice_wise_sale_rpt_FormClosed;
            Load += frmR_invoice_wise_sale_rpt_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label lblFromdt;
        private Splitter splitter1;
        public Label lblPaym;
        public ComboBox cboPaymod;
        public CheckBox chkPaymAll;
        public CheckBox chkGstdet;
        public Label lblToDt;
        public DateTimePicker dtpToDate;
        public DateTimePicker dtpFromDate;
        public CheckBox chkInvGstDet;
        public CheckBox chkItemGstDet;
        public CheckBox chkInvtIME;
    }
}