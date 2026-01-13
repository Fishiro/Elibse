namespace Elibse.Admin
{
    partial class TotalBook
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TotalBook));
            this.dgvBooks = new System.Windows.Forms.DataGridView();
            this.txtSearchBook = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cboFilterCategory = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReload = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cboSortOrder = new System.Windows.Forms.ComboBox();
            this.btnAddBook = new System.Windows.Forms.Button();
            this.btnDeleteBook = new System.Windows.Forms.Button();
            this.btnEditBook = new System.Windows.Forms.Button();
            this.btnLiquidate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBooks
            // 
            this.dgvBooks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBooks.Location = new System.Drawing.Point(8, 42);
            this.dgvBooks.Margin = new System.Windows.Forms.Padding(2);
            this.dgvBooks.Name = "dgvBooks";
            this.dgvBooks.RowHeadersWidth = 62;
            this.dgvBooks.RowTemplate.Height = 28;
            this.dgvBooks.Size = new System.Drawing.Size(636, 275);
            this.dgvBooks.TabIndex = 0;
            // 
            // txtSearchBook
            // 
            this.txtSearchBook.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchBook.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchBook.Location = new System.Drawing.Point(279, 10);
            this.txtSearchBook.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearchBook.Name = "txtSearchBook";
            this.txtSearchBook.Size = new System.Drawing.Size(302, 26);
            this.txtSearchBook.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(584, 9);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(60, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Tìm sách";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cboFilterCategory
            // 
            this.cboFilterCategory.FormattingEnabled = true;
            this.cboFilterCategory.Items.AddRange(new object[] {
            "Tên tác giả",
            "Danh mục",
            "Ngày nhập"});
            this.cboFilterCategory.Location = new System.Drawing.Point(70, 12);
            this.cboFilterCategory.Margin = new System.Windows.Forms.Padding(2);
            this.cboFilterCategory.Name = "cboFilterCategory";
            this.cboFilterCategory.Size = new System.Drawing.Size(82, 21);
            this.cboFilterCategory.TabIndex = 3;
            this.cboFilterCategory.SelectedIndexChanged += new System.EventHandler(this.cboFilterCategory_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Danh mục";
            // 
            // btnReload
            // 
            this.btnReload.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnReload.Location = new System.Drawing.Point(301, 323);
            this.btnReload.Margin = new System.Windows.Forms.Padding(2);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(50, 23);
            this.btnReload.TabIndex = 5;
            this.btnReload.Text = "Tải lại";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(159, 14);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Thứ tự";
            // 
            // cboSortOrder
            // 
            this.cboSortOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSortOrder.FormattingEnabled = true;
            this.cboSortOrder.Items.AddRange(new object[] {
            "A->Z",
            "Z->A"});
            this.cboSortOrder.Location = new System.Drawing.Point(199, 12);
            this.cboSortOrder.Margin = new System.Windows.Forms.Padding(2);
            this.cboSortOrder.Name = "cboSortOrder";
            this.cboSortOrder.Size = new System.Drawing.Size(50, 21);
            this.cboSortOrder.TabIndex = 19;
            this.cboSortOrder.SelectedIndexChanged += new System.EventHandler(this.cboSortOrder_SelectedIndexChanged);
            // 
            // btnAddBook
            // 
            this.btnAddBook.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddBook.Location = new System.Drawing.Point(485, 323);
            this.btnAddBook.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddBook.Name = "btnAddBook";
            this.btnAddBook.Size = new System.Drawing.Size(81, 23);
            this.btnAddBook.TabIndex = 5;
            this.btnAddBook.Text = "Thêm sách";
            this.btnAddBook.UseVisualStyleBackColor = true;
            this.btnAddBook.Click += new System.EventHandler(this.btnAddBook_Click);
            // 
            // btnDeleteBook
            // 
            this.btnDeleteBook.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteBook.Location = new System.Drawing.Point(571, 323);
            this.btnDeleteBook.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeleteBook.Name = "btnDeleteBook";
            this.btnDeleteBook.Size = new System.Drawing.Size(73, 23);
            this.btnDeleteBook.TabIndex = 5;
            this.btnDeleteBook.Text = "Xóa sách";
            this.btnDeleteBook.UseVisualStyleBackColor = true;
            this.btnDeleteBook.Click += new System.EventHandler(this.btnDeleteBook_Click);
            // 
            // btnEditBook
            // 
            this.btnEditBook.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditBook.Location = new System.Drawing.Point(400, 323);
            this.btnEditBook.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditBook.Name = "btnEditBook";
            this.btnEditBook.Size = new System.Drawing.Size(81, 23);
            this.btnEditBook.TabIndex = 5;
            this.btnEditBook.Text = "Sửa sách";
            this.btnEditBook.UseVisualStyleBackColor = true;
            this.btnEditBook.Click += new System.EventHandler(this.btnEditBook_Click);
            // 
            // btnLiquidate
            // 
            this.btnLiquidate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLiquidate.Location = new System.Drawing.Point(8, 323);
            this.btnLiquidate.Margin = new System.Windows.Forms.Padding(2);
            this.btnLiquidate.Name = "btnLiquidate";
            this.btnLiquidate.Size = new System.Drawing.Size(81, 23);
            this.btnLiquidate.TabIndex = 5;
            this.btnLiquidate.Text = "Thanh lý";
            this.btnLiquidate.UseVisualStyleBackColor = true;
            this.btnLiquidate.Click += new System.EventHandler(this.btnLiquidate_Click);
            // 
            // TotalBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 354);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboSortOrder);
            this.Controls.Add(this.btnDeleteBook);
            this.Controls.Add(this.btnLiquidate);
            this.Controls.Add(this.btnEditBook);
            this.Controls.Add(this.btnAddBook);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboFilterCategory);
            this.Controls.Add(this.txtSearchBook);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dgvBooks);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "TotalBook";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Elibse: Xem Số Lượng Sách";
            this.Load += new System.EventHandler(this.TotalBook_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBooks;
        private System.Windows.Forms.TextBox txtSearchBook;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cboFilterCategory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboSortOrder;
        private System.Windows.Forms.Button btnAddBook;
        private System.Windows.Forms.Button btnDeleteBook;
        private System.Windows.Forms.Button btnEditBook;
        private System.Windows.Forms.Button btnLiquidate;
    }
}