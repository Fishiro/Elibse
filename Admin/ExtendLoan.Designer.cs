namespace Elibse.Admin
{
    partial class ExtendLoan
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
            this.label1 = new System.Windows.Forms.Label();
            this.cboReaders = new System.Windows.Forms.ComboBox();
            this.dgvLoans = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.numDays = new System.Windows.Forms.NumericUpDown();
            this.btnExtend = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDays)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chọn Độc giả đang mượn:";
            // 
            // cboReaders
            // 
            this.cboReaders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReaders.FormattingEnabled = true;
            this.cboReaders.Location = new System.Drawing.Point(211, 24);
            this.cboReaders.Name = "cboReaders";
            this.cboReaders.Size = new System.Drawing.Size(244, 28);
            this.cboReaders.TabIndex = 1;
            // 
            // dgvLoans
            // 
            this.dgvLoans.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLoans.Location = new System.Drawing.Point(12, 68);
            this.dgvLoans.Name = "dgvLoans";
            this.dgvLoans.RowTemplate.Height = 28;
            this.dgvLoans.Size = new System.Drawing.Size(443, 307);
            this.dgvLoans.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 396);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Số ngày muốn gia hạn:";
            // 
            // numDays
            // 
            this.numDays.Location = new System.Drawing.Point(189, 394);
            this.numDays.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numDays.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDays.Name = "numDays";
            this.numDays.Size = new System.Drawing.Size(81, 26);
            this.numDays.TabIndex = 3;
            this.numDays.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // btnExtend
            // 
            this.btnExtend.Location = new System.Drawing.Point(325, 388);
            this.btnExtend.Name = "btnExtend";
            this.btnExtend.Size = new System.Drawing.Size(130, 37);
            this.btnExtend.TabIndex = 4;
            this.btnExtend.Text = "Gia hạn sách";
            this.btnExtend.UseVisualStyleBackColor = true;
            this.btnExtend.Click += new System.EventHandler(this.btnExtend_Click);
            // 
            // ExtendLoan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 440);
            this.Controls.Add(this.btnExtend);
            this.Controls.Add(this.numDays);
            this.Controls.Add(this.dgvLoans);
            this.Controls.Add(this.cboReaders);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ExtendLoan";
            this.Text = "Elibse: Gia Hạn Sách";
            this.Load += new System.EventHandler(this.ExtendLoan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDays)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboReaders;
        private System.Windows.Forms.DataGridView dgvLoans;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numDays;
        private System.Windows.Forms.Button btnExtend;
    }
}