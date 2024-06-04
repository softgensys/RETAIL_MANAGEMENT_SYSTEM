using Microsoft.Reporting.WinForms;

namespace softgen
{
    partial class Sale_Return_Inv_Form
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
            sale_ret_reportViewer = new ReportViewer();
            SuspendLayout();
            // 
            // sale_ret_reportViewer
            // 
            sale_ret_reportViewer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            sale_ret_reportViewer.Location = new Point(0, 0);
            sale_ret_reportViewer.Name = "sale_ret_reportViewer";
            sale_ret_reportViewer.ServerReport.BearerToken = null;
            sale_ret_reportViewer.Size = new Size(769, 489);
            sale_ret_reportViewer.TabIndex = 0;
            // 
            // Sale_Return_Inv_Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(795, 450);
            Controls.Add(sale_ret_reportViewer);
            Name = "Sale_Return_Inv_Form";
            Text = "Form1";
            Load += Sale_Return_Inv_Form_Load;
            ResumeLayout(false);
        }

        #endregion

        private ReportViewer sale_ret_reportViewer;
    }
}