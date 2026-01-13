namespace Elibse
{
    partial class AdminHistory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminHistory));
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.btnExport = new System.Windows.Forms.Button();
            this.cboFilterAction = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkFilterDate = new System.Windows.Forms.CheckBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(469, 11);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(113, 26);
            this.txtSearch.TabIndex = 24;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(584, 11);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(60, 23);
            this.btnSearch.TabIndex = 23;
            this.btnSearch.Text = "Tìm";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnReload
            // 
            this.btnReload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReload.Location = new System.Drawing.Point(297, 323);
            this.btnReload.Margin = new System.Windows.Forms.Padding(2);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(50, 23);
            this.btnReload.TabIndex = 22;
            this.btnReload.Text = "Tải lại";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // dgvHistory
            // 
            this.dgvHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistory.Location = new System.Drawing.Point(8, 46);
            this.dgvHistory.Margin = new System.Windows.Forms.Padding(2);
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.RowHeadersWidth = 62;
            this.dgvHistory.RowTemplate.Height = 28;
            this.dgvHistory.Size = new System.Drawing.Size(636, 265);
            this.dgvHistory.TabIndex = 21;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(535, 323);
            this.btnExport.Margin = new System.Windows.Forms.Padding(2);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(99, 23);
            this.btnExport.TabIndex = 22;
            this.btnExport.Text = "Xuất ra .TXT";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // cboFilterAction
            // 
            this.cboFilterAction.FormattingEnabled = true;
            this.cboFilterAction.Items.AddRange(new object[] {
            "Tên tác giả",
            "Danh mục",
            "Ngày mượn"});
            this.cboFilterAction.Location = new System.Drawing.Point(52, 13);
            this.cboFilterAction.Margin = new System.Windows.Forms.Padding(2);
            this.cboFilterAction.Name = "cboFilterAction";
            this.cboFilterAction.Size = new System.Drawing.Size(82, 21);
            this.cboFilterAction.TabIndex = 27;
            this.cboFilterAction.SelectedIndexChanged += new System.EventHandler(this.cboFilterAction_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Bộ lọc";
            // 
            // chkFilterDate
            // 
            this.chkFilterDate.AutoSize = true;
            this.chkFilterDate.Location = new System.Drawing.Point(147, 14);
            this.chkFilterDate.Margin = new System.Windows.Forms.Padding(2);
            this.chkFilterDate.Name = "chkFilterDate";
            this.chkFilterDate.Size = new System.Drawing.Size(94, 17);
            this.chkFilterDate.TabIndex = 29;
            this.chkFilterDate.Text = "Lọc theo ngày";
            this.chkFilterDate.UseVisualStyleBackColor = true;
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(246, 13);
            this.dtpDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(207, 20);
            this.dtpDate.TabIndex = 30;
            // 
            // AdminHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 354);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.chkFilterDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboFilterAction);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.dgvHistory);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AdminHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Elibse: Lịch Sử Log";
            this.Load += new System.EventHandler(this.AdminHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.ComboBox cboFilterAction;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkFilterDate;
        private System.Windows.Forms.DateTimePicker dtpDate;
    }
}