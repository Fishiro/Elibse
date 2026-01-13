namespace Elibse.Admin
{
    partial class ConfigPenalty
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigPenalty));
            this.label1 = new System.Windows.Forms.Label();
            this.numBaseFee = new System.Windows.Forms.NumericUpDown();
            this.numCycle = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numGrace = new System.Windows.Forms.NumericUpDown();
            this.numIncrement = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numBaseFee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCycle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGrace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIncrement)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(105, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Phí phạt cơ bản (VNĐ):";
            // 
            // numBaseFee
            // 
            this.numBaseFee.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numBaseFee.Location = new System.Drawing.Point(230, 32);
            this.numBaseFee.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numBaseFee.Name = "numBaseFee";
            this.numBaseFee.Size = new System.Drawing.Size(120, 20);
            this.numBaseFee.TabIndex = 1;
            // 
            // numCycle
            // 
            this.numCycle.Location = new System.Drawing.Point(230, 58);
            this.numCycle.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCycle.Name = "numCycle";
            this.numCycle.Size = new System.Drawing.Size(120, 20);
            this.numCycle.TabIndex = 1;
            this.numCycle.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(123, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Chu kỳ phạt (Ngày):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(139, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Số ngày ân hạn:";
            // 
            // numGrace
            // 
            this.numGrace.Location = new System.Drawing.Point(230, 84);
            this.numGrace.Name = "numGrace";
            this.numGrace.Size = new System.Drawing.Size(120, 20);
            this.numGrace.TabIndex = 2;
            // 
            // numIncrement
            // 
            this.numIncrement.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numIncrement.Location = new System.Drawing.Point(230, 111);
            this.numIncrement.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numIncrement.Name = "numIncrement";
            this.numIncrement.Size = new System.Drawing.Size(120, 20);
            this.numIncrement.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(193, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Số tiền tăng thêm mỗi chu kỳ (Lũy tiến):";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(142, 152);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(108, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Lưu cấu hình";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ConfigPenalty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 197);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.numIncrement);
            this.Controls.Add(this.numGrace);
            this.Controls.Add(this.numCycle);
            this.Controls.Add(this.numBaseFee);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ConfigPenalty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Elibse: Cài Đặt Gia Hạn";
            ((System.ComponentModel.ISupportInitialize)(this.numBaseFee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCycle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGrace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIncrement)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numBaseFee;
        private System.Windows.Forms.NumericUpDown numCycle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numGrace;
        private System.Windows.Forms.NumericUpDown numIncrement;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSave;
    }
}