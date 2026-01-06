namespace Elibse
{
    partial class fmLoginDialog
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
            this.btnReaderChoice = new System.Windows.Forms.Button();
            this.btnAdminChoice = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnReaderChoice
            // 
            this.btnReaderChoice.Location = new System.Drawing.Point(55, 44);
            this.btnReaderChoice.Name = "btnReaderChoice";
            this.btnReaderChoice.Size = new System.Drawing.Size(182, 77);
            this.btnReaderChoice.TabIndex = 0;
            this.btnReaderChoice.Text = "Cho độc giả";
            this.btnReaderChoice.UseVisualStyleBackColor = true;
            // 
            // btnAdminChoice
            // 
            this.btnAdminChoice.Location = new System.Drawing.Point(298, 44);
            this.btnAdminChoice.Name = "btnAdminChoice";
            this.btnAdminChoice.Size = new System.Drawing.Size(207, 77);
            this.btnAdminChoice.TabIndex = 0;
            this.btnAdminChoice.Text = "Cho Admin";
            this.btnAdminChoice.UseVisualStyleBackColor = true;
            this.btnAdminChoice.Click += new System.EventHandler(this.btnAdminChoice_Click);
            // 
            // fmLoginDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 170);
            this.Controls.Add(this.btnAdminChoice);
            this.Controls.Add(this.btnReaderChoice);
            this.Name = "fmLoginDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Elibse: Chọn đăng nhập";
            this.Load += new System.EventHandler(this.fmLoginDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnReaderChoice;
        private System.Windows.Forms.Button btnAdminChoice;
    }
}

