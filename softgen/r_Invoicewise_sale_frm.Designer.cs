using Microsoft.Reporting.WinForms;

namespace softgen
{
    partial class r_Invoicewise_sale_frm
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
            invwisesalerpt = new Microsoft.Reporting.WinForms.ReportViewer();
            SuspendLayout();
            // 
            // invwisesalerpt
            // 
            invwisesalerpt.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            invwisesalerpt.Location = new Point(0, 0);
            invwisesalerpt.Name = "ReportViewer";
            invwisesalerpt.ServerReport.BearerToken = null;
            invwisesalerpt.Size = new Size(942, 552);
            invwisesalerpt.TabIndex = 0;
            // 
            // r_Invoicewise_sale_frm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(942, 552);
            Controls.Add(invwisesalerpt);
            Name = "r_Invoicewise_sale_frm";
            Text = "r_Invoicewise_sale_frm";
            Load += r_Invoicewise_sale_frm_Load;
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer invwisesalerpt;
    }
}