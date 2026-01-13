namespace Elibse
{
    partial class Violator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Violator));
            this.dgvViolators = new System.Windows.Forms.DataGridView();
            this.btnReload = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cboSort = new System.Windows.Forms.ComboBox();
            this.btnUnlock = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvViolators)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvViolators
            // 
            this.dgvViolators.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvViolators.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvViolators.Location = new System.Drawing.Point(8, 44);
            this.dgvViolators.Margin = new System.Windows.Forms.Padding(2);
            this.dgvViolators.Name = "dgvViolators";
            this.dgvViolators.RowHeadersWidth = 62;
            this.dgvViolators.RowTemplate.Height = 28;
            this.dgvViolators.Size = new System.Drawing.Size(636, 268);
            this.dgvViolators.TabIndex = 1;
            // 
            // btnReload
            // 
            this.btnReload.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnReload.Location = new System.Drawing.Point(301, 323);
            this.btnReload.Margin = new System.Windows.Forms.Padding(2);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(50, 23);
            this.btnReload.TabIndex = 8;
            this.btnReload.Text = "Tải lại";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(241, 5);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(302, 26);
            this.txtSearch.TabIndex = 10;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(547, 8);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(97, 23);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "Tìm độc giả";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 13);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Thứ tự";
            // 
            // cboSort
            // 
            this.cboSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSort.FormattingEnabled = true;
            this.cboSort.Items.AddRange(new object[] {
            "A->Z",
            "Z->A"});
            this.cboSort.Location = new System.Drawing.Point(52, 11);
            this.cboSort.Margin = new System.Windows.Forms.Padding(2);
            this.cboSort.Name = "cboSort";
            this.cboSort.Size = new System.Drawing.Size(50, 21);
            this.cboSort.TabIndex = 11;
            this.cboSort.SelectedIndexChanged += new System.EventHandler(this.cboSort_SelectedIndexChanged);
            // 
            // btnUnlock
            // 
            this.btnUnlock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUnlock.Location = new System.Drawing.Point(573, 323);
            this.btnUnlock.Margin = new System.Windows.Forms.Padding(2);
            this.btnUnlock.Name = "btnUnlock";
            this.btnUnlock.Size = new System.Drawing.Size(71, 23);
            this.btnUnlock.TabIndex = 8;
            this.btnUnlock.Text = "Mở khóa";
            this.btnUnlock.UseVisualStyleBackColor = true;
            this.btnUnlock.Click += new System.EventHandler(this.btnUnlock_Click);
            // 
            // Violator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 354);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboSort);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnUnlock);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.dgvViolators);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Violator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Elibse: Độc Giả Vi Phạm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvViolators)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvViolators;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboSort;
        private System.Windows.Forms.Button btnUnlock;
    }
}