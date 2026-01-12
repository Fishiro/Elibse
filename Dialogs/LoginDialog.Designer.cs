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
            this.btnReaderChoice.Location = new System.Drawing.Point(37, 29);
            this.btnReaderChoice.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnReaderChoice.Name = "btnReaderChoice";
            this.btnReaderChoice.Size = new System.Drawing.Size(121, 50);
            this.btnReaderChoice.TabIndex = 0;
            this.btnReaderChoice.Text = "Cho độc giả";
            this.btnReaderChoice.UseVisualStyleBackColor = true;
            this.btnReaderChoice.Click += new System.EventHandler(this.btnReaderChoice_Click);
            // 
            // btnAdminChoice
            // 
            this.btnAdminChoice.Location = new System.Drawing.Point(199, 29);
            this.btnAdminChoice.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAdminChoice.Name = "btnAdminChoice";
            this.btnAdminChoice.Size = new System.Drawing.Size(138, 50);
            this.btnAdminChoice.TabIndex = 0;
            this.btnAdminChoice.Text = "Cho Admin";
            this.btnAdminChoice.UseVisualStyleBackColor = true;
            this.btnAdminChoice.Click += new System.EventHandler(this.btnAdminChoice_Click);
            // 
            // fmLoginDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 110);
            this.Controls.Add(this.btnAdminChoice);
            this.Controls.Add(this.btnReaderChoice);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "fmLoginDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Elibse: Chọn đăng nhập";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnReaderChoice;
        private System.Windows.Forms.Button btnAdminChoice;
    }
}

