namespace softgen
{
    partial class frmT_Invoice
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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            T_Invoice = new Panel();
            rotTotmrp = new Label();
            lblTotMrp = new Label();
            rotTotdisc = new Label();
            lblTotDisc = new Label();
            rotInvCust = new Label();
            pnlPayDet = new Panel();
            dbgPayDet = new DataGridView();
            PaySrno = new DataGridViewTextBoxColumn();
            Paymod = new DataGridViewComboBoxColumn();
            PayAmt = new DataGridViewTextBoxColumn();
            CashTend = new DataGridViewTextBoxColumn();
            Refund = new DataGridViewTextBoxColumn();
            CCCode = new DataGridViewComboBoxColumn();
            CCNo = new DataGridViewTextBoxColumn();
            Coupon = new DataGridViewComboBoxColumn();
            Custid = new DataGridViewTextBoxColumn();
            Bank = new DataGridViewTextBoxColumn();
            Chno = new DataGridViewTextBoxColumn();
            Chdt = new DataGridViewTextBoxColumn();
            VouchNo = new DataGridViewTextBoxColumn();
            VouchAmt = new DataGridViewTextBoxColumn();
            lblPayDet = new Label();
            rotTotQty = new Label();
            lblTotQty = new Label();
            rotNOI = new Label();
            lblNOI = new Label();
            rotItDesc = new Label();
            lblItDesc = new Label();
            rotPayAmt = new Label();
            lblPayAmt = new Label();
            rotNetAmt = new Label();
            lblNetAmt = new Label();
            rotRO = new Label();
            lblRO = new Label();
            txtDiscAmt = new TextBox();
            lblDiscAmt = new Label();
            txtDiscPer = new TextBox();
            lblDiscPer = new Label();
            rotGAmt = new Label();
            lblGAmt = new Label();
            pnlItemDet = new Panel();
            lblItemDet = new Label();
            dbgItemDet = new DataGridView();
            srno = new DataGridViewTextBoxColumn();
            BarItemCode = new DataGridViewTextBoxColumn();
            Itemname = new DataGridViewTextBoxColumn();
            Qty = new DataGridViewTextBoxColumn();
            Mrp = new DataGridViewTextBoxColumn();
            UnitPrice = new DataGridViewTextBoxColumn();
            Disc = new DataGridViewTextBoxColumn();
            DiscAmt = new DataGridViewTextBoxColumn();
            Gst = new DataGridViewTextBoxColumn();
            Cess = new DataGridViewTextBoxColumn();
            Amount = new DataGridViewTextBoxColumn();
            GstAmt = new DataGridViewTextBoxColumn();
            CessAmt = new DataGridViewTextBoxColumn();
            Itemid = new DataGridViewTextBoxColumn();
            label11 = new Label();
            optBillPrev = new RadioButton();
            label10 = new Label();
            optCoup = new RadioButton();
            txtAddress = new TextBox();
            lblAddress = new Label();
            txtCustName = new TextBox();
            lblCustName = new Label();
            button2 = new Button();
            rotSalesMan = new Label();
            lblSalesMan = new Label();
            cboSalesMan = new ComboBox();
            rotBillTime = new Label();
            lblBillTime = new Label();
            button1 = new Button();
            rotBonusAmt = new Label();
            lblCust = new Label();
            cboCust = new ComboBox();
            lblInvDate = new Label();
            dtpInvDate = new DateTimePicker();
            txtInvNo = new TextBox();
            lblInvNo = new Label();
            T_Invoice.SuspendLayout();
            pnlPayDet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dbgPayDet).BeginInit();
            pnlItemDet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dbgItemDet).BeginInit();
            SuspendLayout();
            // 
            // T_Invoice
            // 
            T_Invoice.BackColor = Color.PowderBlue;
            T_Invoice.BackgroundImageLayout = ImageLayout.None;
            T_Invoice.BorderStyle = BorderStyle.Fixed3D;
            T_Invoice.Controls.Add(rotTotmrp);
            T_Invoice.Controls.Add(lblTotMrp);
            T_Invoice.Controls.Add(rotTotdisc);
            T_Invoice.Controls.Add(lblTotDisc);
            T_Invoice.Controls.Add(rotInvCust);
            T_Invoice.Controls.Add(pnlPayDet);
            T_Invoice.Controls.Add(rotTotQty);
            T_Invoice.Controls.Add(lblTotQty);
            T_Invoice.Controls.Add(rotNOI);
            T_Invoice.Controls.Add(lblNOI);
            T_Invoice.Controls.Add(rotItDesc);
            T_Invoice.Controls.Add(lblItDesc);
            T_Invoice.Controls.Add(rotPayAmt);
            T_Invoice.Controls.Add(lblPayAmt);
            T_Invoice.Controls.Add(rotNetAmt);
            T_Invoice.Controls.Add(lblNetAmt);
            T_Invoice.Controls.Add(rotRO);
            T_Invoice.Controls.Add(lblRO);
            T_Invoice.Controls.Add(txtDiscAmt);
            T_Invoice.Controls.Add(lblDiscAmt);
            T_Invoice.Controls.Add(txtDiscPer);
            T_Invoice.Controls.Add(lblDiscPer);
            T_Invoice.Controls.Add(rotGAmt);
            T_Invoice.Controls.Add(lblGAmt);
            T_Invoice.Controls.Add(pnlItemDet);
            T_Invoice.Controls.Add(label11);
            T_Invoice.Controls.Add(optBillPrev);
            T_Invoice.Controls.Add(label10);
            T_Invoice.Controls.Add(optCoup);
            T_Invoice.Controls.Add(txtAddress);
            T_Invoice.Controls.Add(lblAddress);
            T_Invoice.Controls.Add(txtCustName);
            T_Invoice.Controls.Add(lblCustName);
            T_Invoice.Controls.Add(button2);
            T_Invoice.Controls.Add(rotSalesMan);
            T_Invoice.Controls.Add(lblSalesMan);
            T_Invoice.Controls.Add(cboSalesMan);
            T_Invoice.Controls.Add(rotBillTime);
            T_Invoice.Controls.Add(lblBillTime);
            T_Invoice.Controls.Add(button1);
            T_Invoice.Controls.Add(rotBonusAmt);
            T_Invoice.Controls.Add(lblCust);
            T_Invoice.Controls.Add(cboCust);
            T_Invoice.Controls.Add(lblInvDate);
            T_Invoice.Controls.Add(dtpInvDate);
            T_Invoice.Controls.Add(txtInvNo);
            T_Invoice.Controls.Add(lblInvNo);
            T_Invoice.Location = new Point(0, -1);
            T_Invoice.Name = "T_Invoice";
            T_Invoice.Size = new Size(941, 562);
            T_Invoice.TabIndex = 0;
            T_Invoice.Paint += T_Invoice_Paint;
            // 
            // rotTotmrp
            // 
            rotTotmrp.BackColor = SystemColors.Info;
            rotTotmrp.BorderStyle = BorderStyle.Fixed3D;
            rotTotmrp.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            rotTotmrp.Location = new Point(839, 370);
            rotTotmrp.Name = "rotTotmrp";
            rotTotmrp.Size = new Size(83, 25);
            rotTotmrp.TabIndex = 250;
            rotTotmrp.Click += label2_Click_1;
            // 
            // lblTotMrp
            // 
            lblTotMrp.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblTotMrp.Location = new Point(779, 375);
            lblTotMrp.Name = "lblTotMrp";
            lblTotMrp.Size = new Size(60, 18);
            lblTotMrp.TabIndex = 249;
            lblTotMrp.Text = "Tot. MRP";
            lblTotMrp.Click += lblTotMrp_Click;
            // 
            // rotTotdisc
            // 
            rotTotdisc.BackColor = Color.Bisque;
            rotTotdisc.BorderStyle = BorderStyle.Fixed3D;
            rotTotdisc.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            rotTotdisc.Location = new Point(713, 372);
            rotTotdisc.Name = "rotTotdisc";
            rotTotdisc.Size = new Size(62, 22);
            rotTotdisc.TabIndex = 248;
            rotTotdisc.Click += label1_Click;
            // 
            // lblTotDisc
            // 
            lblTotDisc.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblTotDisc.Location = new Point(636, 369);
            lblTotDisc.Name = "lblTotDisc";
            lblTotDisc.Size = new Size(77, 30);
            lblTotDisc.TabIndex = 247;
            lblTotDisc.Text = "Tot. Savings\r\nOn Mrp";
            lblTotDisc.Click += lblTotDisc_Click;
            // 
            // rotInvCust
            // 
            rotInvCust.BackColor = Color.Bisque;
            rotInvCust.BorderStyle = BorderStyle.Fixed3D;
            rotInvCust.Location = new Point(590, 7);
            rotInvCust.Name = "rotInvCust";
            rotInvCust.Size = new Size(155, 23);
            rotInvCust.TabIndex = 246;
            // 
            // pnlPayDet
            // 
            pnlPayDet.BackColor = Color.Pink;
            pnlPayDet.Controls.Add(dbgPayDet);
            pnlPayDet.Controls.Add(lblPayDet);
            pnlPayDet.Location = new Point(7, 400);
            pnlPayDet.Name = "pnlPayDet";
            pnlPayDet.Size = new Size(924, 142);
            pnlPayDet.TabIndex = 243;
            // 
            // dbgPayDet
            // 
            dbgPayDet.AllowUserToOrderColumns = true;
            dbgPayDet.BackgroundColor = SystemColors.Info;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dbgPayDet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dbgPayDet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dbgPayDet.Columns.AddRange(new DataGridViewColumn[] { PaySrno, Paymod, PayAmt, CashTend, Refund, CCCode, CCNo, Coupon, Custid, Bank, Chno, Chdt, VouchNo, VouchAmt });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dbgPayDet.DefaultCellStyle = dataGridViewCellStyle2;
            dbgPayDet.Location = new Point(5, 20);
            dbgPayDet.Name = "dbgPayDet";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dbgPayDet.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dbgPayDet.RowTemplate.Height = 25;
            dbgPayDet.Size = new Size(916, 116);
            dbgPayDet.TabIndex = 227;
            dbgPayDet.CellContentClick += dbgPayDet_CellContentClick;
            dbgPayDet.CellEndEdit += dbgPayDet_CellEndEdit;
            dbgPayDet.CellValueChanged += dbgPayDet_CellValueChanged_1;
            dbgPayDet.EditingControlShowing += dbgPayDet_EditingControlShowing;
            dbgPayDet.RowLeave += dbgPayDet_RowLeave;
            dbgPayDet.RowPostPaint += dbgPayDet_RowPostPaint;
            // 
            // PaySrno
            // 
            PaySrno.HeaderText = "Sr No.";
            PaySrno.Name = "PaySrno";
            PaySrno.ReadOnly = true;
            PaySrno.Width = 40;
            // 
            // Paymod
            // 
            Paymod.HeaderText = "PAYMODE";
            Paymod.Name = "Paymod";
            Paymod.Width = 80;
            // 
            // PayAmt
            // 
            PayAmt.HeaderText = "AMOUNT";
            PayAmt.Name = "PayAmt";
            PayAmt.Width = 85;
            // 
            // CashTend
            // 
            CashTend.HeaderText = "CASH TEND";
            CashTend.Name = "CashTend";
            CashTend.Width = 85;
            // 
            // Refund
            // 
            Refund.HeaderText = "Refund";
            Refund.Name = "Refund";
            Refund.ReadOnly = true;
            // 
            // CCCode
            // 
            CCCode.HeaderText = "CC CODE";
            CCCode.Name = "CCCode";
            CCCode.ReadOnly = true;
            CCCode.Resizable = DataGridViewTriState.True;
            CCCode.SortMode = DataGridViewColumnSortMode.Automatic;
            CCCode.Width = 50;
            // 
            // CCNo
            // 
            CCNo.HeaderText = "CC NO";
            CCNo.Name = "CCNo";
            CCNo.ReadOnly = true;
            CCNo.Width = 60;
            // 
            // Coupon
            // 
            Coupon.HeaderText = "COUPON";
            Coupon.Name = "Coupon";
            Coupon.ReadOnly = true;
            Coupon.Resizable = DataGridViewTriState.True;
            Coupon.SortMode = DataGridViewColumnSortMode.Automatic;
            Coupon.Width = 60;
            // 
            // Custid
            // 
            Custid.HeaderText = "CUST ID";
            Custid.Name = "Custid";
            Custid.ReadOnly = true;
            Custid.Resizable = DataGridViewTriState.True;
            // 
            // Bank
            // 
            Bank.HeaderText = "BANK";
            Bank.Name = "Bank";
            Bank.ReadOnly = true;
            Bank.Width = 80;
            // 
            // Chno
            // 
            Chno.HeaderText = "CH. NO";
            Chno.Name = "Chno";
            Chno.ReadOnly = true;
            Chno.Width = 70;
            // 
            // Chdt
            // 
            Chdt.HeaderText = "CH. DT";
            Chdt.Name = "Chdt";
            Chdt.ReadOnly = true;
            Chdt.Width = 80;
            // 
            // VouchNo
            // 
            VouchNo.HeaderText = "Vouch. No.";
            VouchNo.Name = "VouchNo";
            // 
            // VouchAmt
            // 
            VouchAmt.HeaderText = "Vouch. Amt";
            VouchAmt.Name = "VouchAmt";
            // 
            // lblPayDet
            // 
            lblPayDet.BackColor = Color.Bisque;
            lblPayDet.Font = new Font("Georgia", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblPayDet.ImeMode = ImeMode.NoControl;
            lblPayDet.Location = new Point(2, 2);
            lblPayDet.Name = "lblPayDet";
            lblPayDet.Size = new Size(918, 18);
            lblPayDet.TabIndex = 226;
            lblPayDet.Text = "Payment Details";
            lblPayDet.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // rotTotQty
            // 
            rotTotQty.BackColor = Color.Bisque;
            rotTotQty.BorderStyle = BorderStyle.Fixed3D;
            rotTotQty.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            rotTotQty.Location = new Point(400, 373);
            rotTotQty.Name = "rotTotQty";
            rotTotQty.Size = new Size(54, 22);
            rotTotQty.TabIndex = 242;
            rotTotQty.Click += rotTotQty_Click;
            // 
            // lblTotQty
            // 
            lblTotQty.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblTotQty.Location = new Point(340, 374);
            lblTotQty.Name = "lblTotQty";
            lblTotQty.Size = new Size(57, 18);
            lblTotQty.TabIndex = 241;
            lblTotQty.Text = "Total Qty";
            // 
            // rotNOI
            // 
            rotNOI.BackColor = Color.Bisque;
            rotNOI.BorderStyle = BorderStyle.Fixed3D;
            rotNOI.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            rotNOI.Location = new Point(556, 371);
            rotNOI.Name = "rotNOI";
            rotNOI.Size = new Size(62, 22);
            rotNOI.TabIndex = 240;
            // 
            // lblNOI
            // 
            lblNOI.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblNOI.Location = new Point(469, 374);
            lblNOI.Name = "lblNOI";
            lblNOI.Size = new Size(80, 18);
            lblNOI.TabIndex = 239;
            lblNOI.Text = "No.  of Items";
            // 
            // rotItDesc
            // 
            rotItDesc.BackColor = Color.PaleGreen;
            rotItDesc.BorderStyle = BorderStyle.Fixed3D;
            rotItDesc.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            rotItDesc.Location = new Point(72, 371);
            rotItDesc.Name = "rotItDesc";
            rotItDesc.Size = new Size(259, 22);
            rotItDesc.TabIndex = 238;
            // 
            // lblItDesc
            // 
            lblItDesc.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblItDesc.Location = new Point(4, 372);
            lblItDesc.Name = "lblItDesc";
            lblItDesc.Size = new Size(68, 18);
            lblItDesc.TabIndex = 237;
            lblItDesc.Text = "Item Desc.";
            // 
            // rotPayAmt
            // 
            rotPayAmt.BackColor = Color.Pink;
            rotPayAmt.BorderStyle = BorderStyle.Fixed3D;
            rotPayAmt.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            rotPayAmt.Location = new Point(797, 335);
            rotPayAmt.Name = "rotPayAmt";
            rotPayAmt.Size = new Size(129, 29);
            rotPayAmt.TabIndex = 236;
            rotPayAmt.Click += label21_Click;
            // 
            // lblPayAmt
            // 
            lblPayAmt.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblPayAmt.Location = new Point(727, 340);
            lblPayAmt.Name = "lblPayAmt";
            lblPayAmt.Size = new Size(77, 19);
            lblPayAmt.TabIndex = 235;
            lblPayAmt.Text = "Amt. To Pay";
            lblPayAmt.Click += label22_Click;
            // 
            // rotNetAmt
            // 
            rotNetAmt.BackColor = Color.Bisque;
            rotNetAmt.BorderStyle = BorderStyle.Fixed3D;
            rotNetAmt.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            rotNetAmt.Location = new Point(479, 340);
            rotNetAmt.Name = "rotNetAmt";
            rotNetAmt.Size = new Size(90, 22);
            rotNetAmt.TabIndex = 234;
            // 
            // lblNetAmt
            // 
            lblNetAmt.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblNetAmt.Location = new Point(417, 342);
            lblNetAmt.Name = "lblNetAmt";
            lblNetAmt.Size = new Size(60, 18);
            lblNetAmt.TabIndex = 233;
            lblNetAmt.Text = "Net Amt.";
            // 
            // rotRO
            // 
            rotRO.BackColor = SystemColors.Info;
            rotRO.BorderStyle = BorderStyle.Fixed3D;
            rotRO.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            rotRO.Location = new Point(637, 339);
            rotRO.Name = "rotRO";
            rotRO.Size = new Size(67, 22);
            rotRO.TabIndex = 232;
            rotRO.Click += rotRO_Click;
            // 
            // lblRO
            // 
            lblRO.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblRO.Location = new Point(575, 342);
            lblRO.Name = "lblRO";
            lblRO.Size = new Size(60, 18);
            lblRO.TabIndex = 231;
            lblRO.Text = "R/O Amt.";
            lblRO.Click += lblRO_Click;
            // 
            // txtDiscAmt
            // 
            txtDiscAmt.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            txtDiscAmt.Location = new Point(339, 341);
            txtDiscAmt.Name = "txtDiscAmt";
            txtDiscAmt.Size = new Size(71, 21);
            txtDiscAmt.TabIndex = 230;
            txtDiscAmt.TextChanged += txtDiscAmt_TextChanged;
            // 
            // lblDiscAmt
            // 
            lblDiscAmt.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblDiscAmt.ImeMode = ImeMode.NoControl;
            lblDiscAmt.Location = new Point(277, 345);
            lblDiscAmt.Name = "lblDiscAmt";
            lblDiscAmt.Size = new Size(69, 14);
            lblDiscAmt.TabIndex = 229;
            lblDiscAmt.Text = "Disc. Amt";
            // 
            // txtDiscPer
            // 
            txtDiscPer.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            txtDiscPer.Location = new Point(209, 341);
            txtDiscPer.Name = "txtDiscPer";
            txtDiscPer.Size = new Size(61, 21);
            txtDiscPer.TabIndex = 228;
            txtDiscPer.TextChanged += txtDiscPer_TextChanged;
            // 
            // lblDiscPer
            // 
            lblDiscPer.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblDiscPer.ImeMode = ImeMode.NoControl;
            lblDiscPer.Location = new Point(156, 344);
            lblDiscPer.Name = "lblDiscPer";
            lblDiscPer.Size = new Size(55, 14);
            lblDiscPer.TabIndex = 227;
            lblDiscPer.Text = "Disc. %";
            // 
            // rotGAmt
            // 
            rotGAmt.BackColor = Color.Bisque;
            rotGAmt.BorderStyle = BorderStyle.Fixed3D;
            rotGAmt.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            rotGAmt.Location = new Point(73, 340);
            rotGAmt.Name = "rotGAmt";
            rotGAmt.Size = new Size(79, 22);
            rotGAmt.TabIndex = 226;
            rotGAmt.Click += label13_Click;
            // 
            // lblGAmt
            // 
            lblGAmt.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblGAmt.Location = new Point(2, 342);
            lblGAmt.Name = "lblGAmt";
            lblGAmt.Size = new Size(69, 18);
            lblGAmt.TabIndex = 225;
            lblGAmt.Text = "Gross Amt.";
            // 
            // pnlItemDet
            // 
            pnlItemDet.BackColor = Color.Pink;
            pnlItemDet.BorderStyle = BorderStyle.Fixed3D;
            pnlItemDet.Controls.Add(lblItemDet);
            pnlItemDet.Controls.Add(dbgItemDet);
            pnlItemDet.Location = new Point(5, 93);
            pnlItemDet.Name = "pnlItemDet";
            pnlItemDet.Size = new Size(929, 238);
            pnlItemDet.TabIndex = 224;
            // 
            // lblItemDet
            // 
            lblItemDet.BackColor = Color.Bisque;
            lblItemDet.Font = new Font("Georgia", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblItemDet.ImeMode = ImeMode.NoControl;
            lblItemDet.Location = new Point(2, 1);
            lblItemDet.Name = "lblItemDet";
            lblItemDet.Size = new Size(920, 18);
            lblItemDet.TabIndex = 225;
            lblItemDet.Text = "Item Details";
            lblItemDet.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dbgItemDet
            // 
            dbgItemDet.BackgroundColor = SystemColors.Info;
            dbgItemDet.BorderStyle = BorderStyle.Fixed3D;
            dbgItemDet.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Control;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dbgItemDet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dbgItemDet.ColumnHeadersHeight = 35;
            dbgItemDet.Columns.AddRange(new DataGridViewColumn[] { srno, BarItemCode, Itemname, Qty, Mrp, UnitPrice, Disc, DiscAmt, Gst, Cess, Amount, GstAmt, CessAmt, Itemid });
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = SystemColors.Window;
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            dbgItemDet.DefaultCellStyle = dataGridViewCellStyle5;
            dbgItemDet.Location = new Point(3, 20);
            dbgItemDet.Name = "dbgItemDet";
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = SystemColors.Control;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            dbgItemDet.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            dbgItemDet.RowTemplate.Height = 25;
            dbgItemDet.Size = new Size(921, 208);
            dbgItemDet.TabIndex = 223;
            dbgItemDet.CellBeginEdit += dbgItemDet_CellBeginEdit;
            dbgItemDet.CellClick += dbgItemDet_CellClick;
            dbgItemDet.CellContentClick += dbgItemDet_CellContentClick;
            dbgItemDet.CellEndEdit += dbgItemDet_CellEndEdit;
            dbgItemDet.CellEnter += dbgItemDet_CellEnter;
            dbgItemDet.CellValidated += dbgItemDet_CellValidated;
            dbgItemDet.CellValidating += dbgItemDet_CellValidating;
            dbgItemDet.CellValueChanged += dbgItemDet_CellValueChanged;
            dbgItemDet.EditingControlShowing += dbgItemDet_EditingControlShowing;
            dbgItemDet.RowLeave += dbgItemDet_RowLeave;
            dbgItemDet.RowPostPaint += dbgItemDet_RowPostPaint;
            dbgItemDet.KeyDown += dbgItemDet_KeyDown;
            // 
            // srno
            // 
            srno.HeaderText = "Sr No.";
            srno.Name = "srno";
            srno.ReadOnly = true;
            srno.SortMode = DataGridViewColumnSortMode.NotSortable;
            srno.Width = 30;
            // 
            // BarItemCode
            // 
            BarItemCode.HeaderText = "Bar/ItemCode";
            BarItemCode.Name = "BarItemCode";
            BarItemCode.SortMode = DataGridViewColumnSortMode.NotSortable;
            BarItemCode.Width = 145;
            // 
            // Itemname
            // 
            Itemname.HeaderText = "Item Name";
            Itemname.Name = "Itemname";
            Itemname.ReadOnly = true;
            Itemname.SortMode = DataGridViewColumnSortMode.NotSortable;
            Itemname.Width = 160;
            // 
            // Qty
            // 
            Qty.HeaderText = "Qty";
            Qty.Name = "Qty";
            Qty.SortMode = DataGridViewColumnSortMode.NotSortable;
            Qty.Width = 75;
            // 
            // Mrp
            // 
            Mrp.HeaderText = "MRP";
            Mrp.Name = "Mrp";
            Mrp.SortMode = DataGridViewColumnSortMode.NotSortable;
            Mrp.Width = 80;
            // 
            // UnitPrice
            // 
            UnitPrice.HeaderText = "Unit Price";
            UnitPrice.Name = "UnitPrice";
            UnitPrice.SortMode = DataGridViewColumnSortMode.NotSortable;
            UnitPrice.Width = 70;
            // 
            // Disc
            // 
            Disc.HeaderText = "Disc%";
            Disc.Name = "Disc";
            Disc.SortMode = DataGridViewColumnSortMode.NotSortable;
            Disc.Width = 45;
            // 
            // DiscAmt
            // 
            DiscAmt.HeaderText = "Disc. Amt";
            DiscAmt.Name = "DiscAmt";
            DiscAmt.ReadOnly = true;
            DiscAmt.SortMode = DataGridViewColumnSortMode.NotSortable;
            DiscAmt.Width = 60;
            // 
            // Gst
            // 
            Gst.HeaderText = "GST%";
            Gst.Name = "Gst";
            Gst.ReadOnly = true;
            Gst.SortMode = DataGridViewColumnSortMode.NotSortable;
            Gst.Width = 50;
            // 
            // Cess
            // 
            Cess.HeaderText = "CESS%";
            Cess.Name = "Cess";
            Cess.ReadOnly = true;
            Cess.SortMode = DataGridViewColumnSortMode.NotSortable;
            Cess.Width = 40;
            // 
            // Amount
            // 
            Amount.HeaderText = "AMOUNT";
            Amount.MinimumWidth = 4;
            Amount.Name = "Amount";
            Amount.ReadOnly = true;
            Amount.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // GstAmt
            // 
            GstAmt.HeaderText = "GST Amt.";
            GstAmt.Name = "GstAmt";
            GstAmt.ReadOnly = true;
            GstAmt.SortMode = DataGridViewColumnSortMode.NotSortable;
            GstAmt.Width = 70;
            // 
            // CessAmt
            // 
            CessAmt.HeaderText = "CESS Amt.";
            CessAmt.Name = "CessAmt";
            CessAmt.ReadOnly = true;
            CessAmt.SortMode = DataGridViewColumnSortMode.NotSortable;
            CessAmt.Width = 60;
            // 
            // Itemid
            // 
            Itemid.HeaderText = "Item Id";
            Itemid.Name = "Itemid";
            Itemid.ReadOnly = true;
            Itemid.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // label11
            // 
            label11.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label11.Location = new Point(781, 67);
            label11.Name = "label11";
            label11.Size = new Size(73, 18);
            label11.TabIndex = 222;
            label11.Text = "Bill Preview";
            // 
            // optBillPrev
            // 
            optBillPrev.AutoSize = true;
            optBillPrev.Enabled = false;
            optBillPrev.Location = new Point(861, 68);
            optBillPrev.Name = "optBillPrev";
            optBillPrev.Size = new Size(14, 13);
            optBillPrev.TabIndex = 221;
            optBillPrev.TabStop = true;
            optBillPrev.TextAlign = ContentAlignment.TopLeft;
            optBillPrev.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            label10.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label10.Location = new Point(669, 66);
            label10.Name = "label10";
            label10.Size = new Size(64, 18);
            label10.TabIndex = 220;
            label10.Text = "Coupon";
            // 
            // optCoup
            // 
            optCoup.AutoSize = true;
            optCoup.Enabled = false;
            optCoup.Location = new Point(734, 68);
            optCoup.Name = "optCoup";
            optCoup.Size = new Size(14, 13);
            optCoup.TabIndex = 219;
            optCoup.TabStop = true;
            optCoup.TextAlign = ContentAlignment.TopLeft;
            optCoup.UseVisualStyleBackColor = true;
            optCoup.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(356, 64);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(304, 23);
            txtAddress.TabIndex = 218;
            // 
            // lblAddress
            // 
            lblAddress.Font = new Font("Georgia", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblAddress.ImeMode = ImeMode.NoControl;
            lblAddress.Location = new Point(292, 68);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(81, 14);
            lblAddress.TabIndex = 217;
            lblAddress.Text = "Address";
            // 
            // txtCustName
            // 
            txtCustName.Location = new Point(91, 64);
            txtCustName.Name = "txtCustName";
            txtCustName.Size = new Size(192, 23);
            txtCustName.TabIndex = 216;
            // 
            // lblCustName
            // 
            lblCustName.Font = new Font("Georgia", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblCustName.ImeMode = ImeMode.NoControl;
            lblCustName.Location = new Point(7, 66);
            lblCustName.Name = "lblCustName";
            lblCustName.Size = new Size(81, 14);
            lblCustName.TabIndex = 215;
            lblCustName.Text = "Cust. Name";
            // 
            // button2
            // 
            button2.Location = new Point(596, 36);
            button2.Name = "button2";
            button2.Size = new Size(60, 25);
            button2.TabIndex = 212;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            // 
            // rotSalesMan
            // 
            rotSalesMan.BackColor = Color.Bisque;
            rotSalesMan.BorderStyle = BorderStyle.Fixed3D;
            rotSalesMan.Location = new Point(394, 36);
            rotSalesMan.Name = "rotSalesMan";
            rotSalesMan.Size = new Size(196, 23);
            rotSalesMan.TabIndex = 211;
            // 
            // lblSalesMan
            // 
            lblSalesMan.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblSalesMan.Location = new Point(211, 38);
            lblSalesMan.Name = "lblSalesMan";
            lblSalesMan.Size = new Size(59, 18);
            lblSalesMan.TabIndex = 210;
            lblSalesMan.Text = "SalesMan";
            // 
            // cboSalesMan
            // 
            cboSalesMan.BackColor = SystemColors.Window;
            cboSalesMan.FlatStyle = FlatStyle.Flat;
            cboSalesMan.FormattingEnabled = true;
            cboSalesMan.ItemHeight = 15;
            cboSalesMan.Location = new Point(276, 36);
            cboSalesMan.Name = "cboSalesMan";
            cboSalesMan.Size = new Size(112, 23);
            cboSalesMan.TabIndex = 209;
            // 
            // rotBillTime
            // 
            rotBillTime.BackColor = Color.Bisque;
            rotBillTime.BorderStyle = BorderStyle.Fixed3D;
            rotBillTime.Location = new Point(92, 35);
            rotBillTime.Name = "rotBillTime";
            rotBillTime.Size = new Size(110, 23);
            rotBillTime.TabIndex = 208;
            rotBillTime.Click += label2_Click;
            // 
            // lblBillTime
            // 
            lblBillTime.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblBillTime.Location = new Point(9, 37);
            lblBillTime.Name = "lblBillTime";
            lblBillTime.Size = new Size(69, 18);
            lblBillTime.TabIndex = 207;
            lblBillTime.Text = "Billed Time";
            lblBillTime.Click += label3_Click;
            // 
            // button1
            // 
            button1.Location = new Point(860, 7);
            button1.Name = "button1";
            button1.Size = new Size(46, 25);
            button1.TabIndex = 206;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // rotBonusAmt
            // 
            rotBonusAmt.BackColor = Color.Bisque;
            rotBonusAmt.BorderStyle = BorderStyle.Fixed3D;
            rotBonusAmt.Location = new Point(753, 7);
            rotBonusAmt.Name = "rotBonusAmt";
            rotBonusAmt.Size = new Size(98, 23);
            rotBonusAmt.TabIndex = 205;
            // 
            // lblCust
            // 
            lblCust.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblCust.Location = new Point(378, 11);
            lblCust.Name = "lblCust";
            lblCust.Size = new Size(65, 18);
            lblCust.TabIndex = 203;
            lblCust.Text = "Customer";
            // 
            // cboCust
            // 
            cboCust.BackColor = SystemColors.Window;
            cboCust.Enabled = false;
            cboCust.FlatStyle = FlatStyle.Flat;
            cboCust.FormattingEnabled = true;
            cboCust.Location = new Point(449, 7);
            cboCust.Name = "cboCust";
            cboCust.Size = new Size(133, 23);
            cboCust.TabIndex = 202;
            cboCust.DropDown += cboCust_DropDown;
            cboCust.SelectedIndexChanged += cboCust_SelectedIndexChanged;
            cboCust.TextChanged += cboCust_TextChanged;
            // 
            // lblInvDate
            // 
            lblInvDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblInvDate.Location = new Point(213, 10);
            lblInvDate.Name = "lblInvDate";
            lblInvDate.Size = new Size(41, 18);
            lblInvDate.TabIndex = 187;
            lblInvDate.Text = "Date";
            // 
            // dtpInvDate
            // 
            dtpInvDate.CustomFormat = "dd/MM/yyyy";
            dtpInvDate.Enabled = false;
            dtpInvDate.Format = DateTimePickerFormat.Custom;
            dtpInvDate.Location = new Point(260, 8);
            dtpInvDate.Name = "dtpInvDate";
            dtpInvDate.Size = new Size(105, 23);
            dtpInvDate.TabIndex = 186;
            dtpInvDate.Value = new DateTime(2024, 4, 20, 0, 0, 0, 0);
            dtpInvDate.ValueChanged += dtpInvDate_ValueChanged;
            // 
            // txtInvNo
            // 
            txtInvNo.Enabled = false;
            txtInvNo.Location = new Point(91, 7);
            txtInvNo.Name = "txtInvNo";
            txtInvNo.Size = new Size(112, 23);
            txtInvNo.TabIndex = 83;
            txtInvNo.TextChanged += txtInvNo_TextChanged;
            // 
            // lblInvNo
            // 
            lblInvNo.AutoSize = true;
            lblInvNo.Font = new Font("Georgia", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblInvNo.ImeMode = ImeMode.NoControl;
            lblInvNo.Location = new Point(8, 11);
            lblInvNo.Name = "lblInvNo";
            lblInvNo.Size = new Size(74, 14);
            lblInvNo.TabIndex = 82;
            lblInvNo.Text = "Invoice No";
            // 
            // frmT_Invoice
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.EnablePreventFocusChange;
            ClientSize = new Size(938, 548);
            Controls.Add(T_Invoice);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "frmT_Invoice";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Invoice Generation";
            FormClosed += frmT_Invoice_FormClosed;
            Load += frmT_Invoice_Load;
            T_Invoice.ResumeLayout(false);
            T_Invoice.PerformLayout();
            pnlPayDet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dbgPayDet).EndInit();
            pnlItemDet.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dbgItemDet).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel T_Invoice;
        private Label lblInvNo;
        private Label lblInvDate;
        private Label lblCust;
        private Button button1;
        private Label lblSalesMan;
        private ComboBox cboSalesMan;
        private Label rotBillTime;
        private Label lblBillTime;
        private Button button2;
        private TextBox txtCustName;
        private Label lblCustName;
        private TextBox txtAddress;
        private Label lblAddress;
        private Label label10;
        private RadioButton optCoup;
        private Label label11;
        private RadioButton optBillPrev;
        private Panel pnlItemDet;
        private Label lblItemDet;
        private Label lblDiscPer;
        private Label lblGAmt;
        private Label lblDiscAmt;
        private Label lblRO;
        private Label lblPayAmt;
        private Label lblNetAmt;
        private Label lblTotQty;
        private Label lblNOI;
        private Label rotItDesc;
        private Label lblItDesc;
        private Panel pnlPayDet;
        private Label lblPayDet;
        private Label rotSalesMan;
        private Label rotBonusAmt;
        private TextBox rotCust;
        private Label rotInvCust;
        public DataGridView dbgItemDet;
        private Label label1;
        private Label lblTotDisc;
        private Label lblTotMrp;
        public TextBox txtDiscPer;
        public Label rotGAmt;
        public TextBox txtDiscAmt;
        public Label rotNetAmt;
        public Label rotTotQty;
        public Label rotPayAmt;
        public Label rotTotdisc;
        public Label rotRO;
        public Label rotNOI;
        public DataGridView dbgPayDet;
        public Label rotTotmrp;
        public TextBox txtInvNo;
        public ComboBox cboCust;
        private DataGridViewTextBoxColumn srno;
        private DataGridViewTextBoxColumn BarItemCode;
        private DataGridViewTextBoxColumn Itemname;
        private DataGridViewTextBoxColumn Qty;
        private DataGridViewTextBoxColumn Mrp;
        private DataGridViewTextBoxColumn UnitPrice;
        private DataGridViewTextBoxColumn Disc;
        private DataGridViewTextBoxColumn DiscAmt;
        private DataGridViewTextBoxColumn Gst;
        private DataGridViewTextBoxColumn Cess;
        private DataGridViewTextBoxColumn Amount;
        private DataGridViewTextBoxColumn GstAmt;
        private DataGridViewTextBoxColumn CessAmt;
        private DataGridViewTextBoxColumn Itemid;
        public DateTimePicker dtpInvDate;
        private DataGridViewTextBoxColumn PaySrno;
        private DataGridViewComboBoxColumn Paymod;
        private DataGridViewTextBoxColumn PayAmt;
        private DataGridViewTextBoxColumn CashTend;
        private DataGridViewTextBoxColumn Refund;
        private DataGridViewComboBoxColumn CCCode;
        private DataGridViewTextBoxColumn CCNo;
        private DataGridViewComboBoxColumn Coupon;
        private DataGridViewTextBoxColumn Custid;
        private DataGridViewTextBoxColumn Bank;
        private DataGridViewTextBoxColumn Chno;
        private DataGridViewTextBoxColumn Chdt;
        private DataGridViewTextBoxColumn VouchNo;
        private DataGridViewTextBoxColumn VouchAmt;
    }
}