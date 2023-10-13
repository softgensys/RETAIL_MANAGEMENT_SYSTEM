namespace softgen
{
    partial class MasParams
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
            label4 = new Label();
            checkBox1 = new CheckBox();
            SuspendLayout();
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ImeMode = ImeMode.NoControl;
            label4.Location = new Point(8, 13);
            label4.Name = "label4";
            label4.Size = new Size(131, 15);
            label4.TabIndex = 183;
            label4.Text = "Item Coder Automatic";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Checked = true;
            checkBox1.CheckState = CheckState.Checked;
            checkBox1.Location = new Point(146, 14);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(15, 14);
            checkBox1.TabIndex = 184;
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // MasParams
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.PowderBlue;
            ClientSize = new Size(353, 165);
            Controls.Add(checkBox1);
            Controls.Add(label4);
            Name = "MasParams";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Master Parameters";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label4;
        private CheckBox checkBox1;
    }
}