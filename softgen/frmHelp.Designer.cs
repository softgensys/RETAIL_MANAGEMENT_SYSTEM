namespace softgen
{
    partial class frmHelp
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
            grdHelp = new DataGridView();
            pnlText = new Label();
            pnlInstruction = new Panel();
            pnlInstructiontxt = new Label();
            panel3 = new Panel();
            cmdOK = new Button();
            pgbStatus = new ProgressBar();
            txtValue = new TextBox();
            cboRel = new ComboBox();
            cboOrder = new ComboBox();
            cboFields = new ComboBox();
            lblOrder = new Label();
            lblSelect = new Label();
            cboFieldsId = new ComboBox();
            cboOrderId = new ComboBox();
            cboDataType = new ComboBox();
            pnlTop = new Panel();
            pnlToptxt = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grdHelp).BeginInit();
            pnlInstruction.SuspendLayout();
            panel3.SuspendLayout();
            pnlTop.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Wheat;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(grdHelp);
            panel1.Controls.Add(pnlText);
            panel1.Controls.Add(pnlInstruction);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(pnlTop);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(683, 381);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // grdHelp
            // 
            grdHelp.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grdHelp.GridColor = SystemColors.ActiveCaption;
            grdHelp.Location = new Point(2, 102);
            grdHelp.MultiSelect = false;
            grdHelp.Name = "grdHelp";
            grdHelp.RowTemplate.Height = 25;
            grdHelp.Size = new Size(676, 247);
            grdHelp.TabIndex = 3;
            grdHelp.CellContentClick += grdHelp_CellContentClick;
            grdHelp.CellDoubleClick += grdHelp_CellDoubleClick;
            // 
            // pnlText
            // 
            pnlText.AutoSize = true;
            pnlText.Font = new Font("Times New Roman", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            pnlText.Location = new Point(506, 186);
            pnlText.Name = "pnlText";
            pnlText.Size = new Size(0, 17);
            pnlText.TabIndex = 2;
            // 
            // pnlInstruction
            // 
            pnlInstruction.BackColor = Color.Pink;
            pnlInstruction.Controls.Add(pnlInstructiontxt);
            pnlInstruction.Location = new Point(1, 351);
            pnlInstruction.Name = "pnlInstruction";
            pnlInstruction.Size = new Size(681, 24);
            pnlInstruction.TabIndex = 1;
            // 
            // pnlInstructiontxt
            // 
            pnlInstructiontxt.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            pnlInstructiontxt.Location = new Point(218, 3);
            pnlInstructiontxt.Name = "pnlInstructiontxt";
            pnlInstructiontxt.Size = new Size(235, 18);
            pnlInstructiontxt.TabIndex = 0;
            pnlInstructiontxt.Text = "Double click or press Enter to select.";
            pnlInstructiontxt.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            panel3.BackColor = Color.PowderBlue;
            panel3.BorderStyle = BorderStyle.Fixed3D;
            panel3.Controls.Add(cmdOK);
            panel3.Controls.Add(pgbStatus);
            panel3.Controls.Add(txtValue);
            panel3.Controls.Add(cboRel);
            panel3.Controls.Add(cboOrder);
            panel3.Controls.Add(cboFields);
            panel3.Controls.Add(lblOrder);
            panel3.Controls.Add(lblSelect);
            panel3.Controls.Add(cboFieldsId);
            panel3.Controls.Add(cboOrderId);
            panel3.Controls.Add(cboDataType);
            panel3.Location = new Point(1, 25);
            panel3.Name = "panel3";
            panel3.Size = new Size(678, 77);
            panel3.TabIndex = 1;
            // 
            // cmdOK
            // 
            cmdOK.Location = new Point(423, 41);
            cmdOK.Name = "cmdOK";
            cmdOK.Size = new Size(71, 23);
            cmdOK.TabIndex = 7;
            cmdOK.Text = "Search";
            cmdOK.UseVisualStyleBackColor = true;
            cmdOK.Click += cmdOK_Click;
            // 
            // pgbStatus
            // 
            pgbStatus.Location = new Point(242, 45);
            pgbStatus.Name = "pgbStatus";
            pgbStatus.Size = new Size(165, 11);
            pgbStatus.TabIndex = 6;
            // 
            // txtValue
            // 
            txtValue.Location = new Point(285, 7);
            txtValue.Name = "txtValue";
            txtValue.Size = new Size(205, 23);
            txtValue.TabIndex = 5;
            // 
            // cboRel
            // 
            cboRel.FormattingEnabled = true;
            cboRel.Location = new Point(221, 8);
            cboRel.Name = "cboRel";
            cboRel.Size = new Size(52, 23);
            cboRel.TabIndex = 4;
            // 
            // cboOrder
            // 
            cboOrder.FormattingEnabled = true;
            cboOrder.Location = new Point(80, 38);
            cboOrder.Name = "cboOrder";
            cboOrder.Size = new Size(135, 23);
            cboOrder.TabIndex = 3;
            // 
            // cboFields
            // 
            cboFields.FormattingEnabled = true;
            cboFields.Location = new Point(80, 8);
            cboFields.Name = "cboFields";
            cboFields.Size = new Size(135, 23);
            cboFields.TabIndex = 2;
            cboFields.SelectedIndexChanged += cboFields_SelectedIndexChanged;
            cboFields.SelectionChangeCommitted += cboFields_SelectionChangeCommitted;
            cboFields.MouseClick += cboFields_MouseClick;
            // 
            // lblOrder
            // 
            lblOrder.AutoSize = true;
            lblOrder.Font = new Font("Times New Roman", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblOrder.Location = new Point(21, 41);
            lblOrder.Name = "lblOrder";
            lblOrder.Size = new Size(59, 15);
            lblOrder.TabIndex = 1;
            lblOrder.Text = "Order By";
            // 
            // lblSelect
            // 
            lblSelect.AutoSize = true;
            lblSelect.Font = new Font("Times New Roman", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblSelect.Location = new Point(24, 11);
            lblSelect.Name = "lblSelect";
            lblSelect.Size = new Size(38, 15);
            lblSelect.TabIndex = 0;
            lblSelect.Text = "Filter";
            // 
            // cboFieldsId
            // 
            cboFieldsId.FormattingEnabled = true;
            cboFieldsId.Location = new Point(340, 7);
            cboFieldsId.Name = "cboFieldsId";
            cboFieldsId.Size = new Size(135, 23);
            cboFieldsId.TabIndex = 8;
            cboFieldsId.Visible = false;
            // 
            // cboOrderId
            // 
            cboOrderId.FormattingEnabled = true;
            cboOrderId.Location = new Point(304, 8);
            cboOrderId.Name = "cboOrderId";
            cboOrderId.Size = new Size(135, 23);
            cboOrderId.TabIndex = 9;
            cboOrderId.Visible = false;
            // 
            // cboDataType
            // 
            cboDataType.FormattingEnabled = true;
            cboDataType.Location = new Point(426, 7);
            cboDataType.Name = "cboDataType";
            cboDataType.Size = new Size(61, 23);
            cboDataType.TabIndex = 10;
            cboDataType.Visible = false;
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.Pink;
            pnlTop.Controls.Add(pnlToptxt);
            pnlTop.Location = new Point(1, 1);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(678, 24);
            pnlTop.TabIndex = 0;
            // 
            // pnlToptxt
            // 
            pnlToptxt.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            pnlToptxt.Location = new Point(241, 3);
            pnlToptxt.Name = "pnlToptxt";
            pnlToptxt.Size = new Size(193, 18);
            pnlToptxt.TabIndex = 0;
            pnlToptxt.Text = "Permissions";
            pnlToptxt.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // frmHelp
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(686, 381);
            Controls.Add(panel1);
            Name = "frmHelp";
            Text = "Help";
            FormClosing += frmHelp_FormClosing;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)grdHelp).EndInit();
            pnlInstruction.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            pnlTop.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel pnlTop;
        private Panel panel3;
        private Panel pnlInstruction;
        private Label lblSelect;
        private Label lblOrder;
        public ComboBox cboRel;
        public ComboBox cboOrder;
        public ComboBox cboFields;
        public TextBox txtValue;
        public Button cmdOK;
        public Label pnlText;
        public Label pnlToptxt;
        public Label pnlInstructiontxt;
        public ProgressBar pgbStatus;
        public ComboBox cboFieldsId;
        public ComboBox cboOrderId;
        public ComboBox cboDataType;
        public DataGridView grdHelp;
    }
}