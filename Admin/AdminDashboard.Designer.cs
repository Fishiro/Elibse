namespace Elibse
{
    partial class AdminDashboard
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnHistory = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.chartStats = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnViewOverdue = new System.Windows.Forms.Button();
            this.btnViewViolators = new System.Windows.Forms.Button();
            this.btnViewBorrowed = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.lblTotalOverdue = new System.Windows.Forms.Label();
            this.lblTotalViolations = new System.Windows.Forms.Label();
            this.lblTotalBorrowed = new System.Windows.Forms.Label();
            this.btnViewTotalBooks = new System.Windows.Forms.Button();
            this.lblOverdue = new System.Windows.Forms.Label();
            this.lblViolations = new System.Windows.Forms.Label();
            this.lblBorrowedBooks = new System.Windows.Forms.Label();
            this.lblTotalBooks = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tàiKhoảnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuChangePassword = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.tínhNăngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExportCSV = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.gửiMailCảnhBáoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.giớiThiệuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuManual = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAboutUs = new System.Windows.Forms.ToolStripMenuItem();
            this.phầnMềmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFineSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.càiĐặtGiaHạnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tàiKhoảnGửiMailThôngBáoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quảnLýToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuManageReaders = new System.Windows.Forms.ToolStripMenuItem();
            this.addCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.thêmNhiềuSáchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thanhLýSáchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBorrow = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnAddBook = new System.Windows.Forms.Button();
            this.giaHạnTrảSáchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartStats)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnHistory);
            this.panel3.Controls.Add(this.btnReload);
            this.panel3.Controls.Add(this.chartStats);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.menuStrip1);
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(936, 584);
            this.panel3.TabIndex = 2;
            // 
            // btnHistory
            // 
            this.btnHistory.Location = new System.Drawing.Point(822, 383);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(80, 37);
            this.btnHistory.TabIndex = 9;
            this.btnHistory.Text = "Lịch sử";
            this.btnHistory.UseVisualStyleBackColor = true;
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(822, 432);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(80, 37);
            this.btnReload.TabIndex = 4;
            this.btnReload.Text = "Tải lại";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // chartStats
            // 
            chartArea4.Name = "ChartArea1";
            this.chartStats.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chartStats.Legends.Add(legend4);
            this.chartStats.Location = new System.Drawing.Point(12, 299);
            this.chartStats.Name = "chartStats";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chartStats.Series.Add(series4);
            this.chartStats.Size = new System.Drawing.Size(539, 260);
            this.chartStats.TabIndex = 7;
            this.chartStats.Text = "chart2";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnViewOverdue);
            this.groupBox1.Controls.Add(this.btnViewViolators);
            this.groupBox1.Controls.Add(this.btnViewBorrowed);
            this.groupBox1.Controls.Add(this.label);
            this.groupBox1.Controls.Add(this.lblTotalOverdue);
            this.groupBox1.Controls.Add(this.lblTotalViolations);
            this.groupBox1.Controls.Add(this.lblTotalBorrowed);
            this.groupBox1.Controls.Add(this.btnViewTotalBooks);
            this.groupBox1.Controls.Add(this.lblOverdue);
            this.groupBox1.Controls.Add(this.lblViolations);
            this.groupBox1.Controls.Add(this.lblBorrowedBooks);
            this.groupBox1.Controls.Add(this.lblTotalBooks);
            this.groupBox1.Location = new System.Drawing.Point(12, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(912, 229);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thống kê";
            // 
            // btnViewOverdue
            // 
            this.btnViewOverdue.Location = new System.Drawing.Point(716, 133);
            this.btnViewOverdue.Name = "btnViewOverdue";
            this.btnViewOverdue.Size = new System.Drawing.Size(72, 36);
            this.btnViewOverdue.TabIndex = 2;
            this.btnViewOverdue.Text = "Xem";
            this.btnViewOverdue.UseVisualStyleBackColor = true;
            // 
            // btnViewViolators
            // 
            this.btnViewViolators.Location = new System.Drawing.Point(531, 133);
            this.btnViewViolators.Name = "btnViewViolators";
            this.btnViewViolators.Size = new System.Drawing.Size(72, 36);
            this.btnViewViolators.TabIndex = 2;
            this.btnViewViolators.Text = "Xem";
            this.btnViewViolators.UseVisualStyleBackColor = true;
            // 
            // btnViewBorrowed
            // 
            this.btnViewBorrowed.Location = new System.Drawing.Point(318, 133);
            this.btnViewBorrowed.Name = "btnViewBorrowed";
            this.btnViewBorrowed.Size = new System.Drawing.Size(72, 36);
            this.btnViewBorrowed.TabIndex = 2;
            this.btnViewBorrowed.Text = "Xem";
            this.btnViewBorrowed.UseVisualStyleBackColor = true;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label.Location = new System.Drawing.Point(80, 65);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(104, 20);
            this.label.TabIndex = 0;
            this.label.Text = "Tổng số sách";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalOverdue
            // 
            this.lblTotalOverdue.AutoSize = true;
            this.lblTotalOverdue.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblTotalOverdue.Location = new System.Drawing.Point(698, 65);
            this.lblTotalOverdue.Name = "lblTotalOverdue";
            this.lblTotalOverdue.Size = new System.Drawing.Size(111, 20);
            this.lblTotalOverdue.TabIndex = 0;
            this.lblTotalOverdue.Text = "Mượn quá hạn";
            this.lblTotalOverdue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalViolations
            // 
            this.lblTotalViolations.AutoSize = true;
            this.lblTotalViolations.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblTotalViolations.Location = new System.Drawing.Point(508, 65);
            this.lblTotalViolations.Name = "lblTotalViolations";
            this.lblTotalViolations.Size = new System.Drawing.Size(121, 20);
            this.lblTotalViolations.TabIndex = 0;
            this.lblTotalViolations.Text = "Độc giả vi phạm";
            this.lblTotalViolations.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalBorrowed
            // 
            this.lblTotalBorrowed.AutoSize = true;
            this.lblTotalBorrowed.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblTotalBorrowed.Location = new System.Drawing.Point(273, 65);
            this.lblTotalBorrowed.Name = "lblTotalBorrowed";
            this.lblTotalBorrowed.Size = new System.Drawing.Size(168, 20);
            this.lblTotalBorrowed.TabIndex = 0;
            this.lblTotalBorrowed.Text = "Tổng đang được mượn";
            this.lblTotalBorrowed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnViewTotalBooks
            // 
            this.btnViewTotalBooks.Location = new System.Drawing.Point(96, 133);
            this.btnViewTotalBooks.Name = "btnViewTotalBooks";
            this.btnViewTotalBooks.Size = new System.Drawing.Size(72, 36);
            this.btnViewTotalBooks.TabIndex = 2;
            this.btnViewTotalBooks.Text = "Xem";
            this.btnViewTotalBooks.UseVisualStyleBackColor = true;
            // 
            // lblOverdue
            // 
            this.lblOverdue.AutoSize = true;
            this.lblOverdue.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOverdue.Location = new System.Drawing.Point(738, 92);
            this.lblOverdue.Name = "lblOverdue";
            this.lblOverdue.Size = new System.Drawing.Size(31, 32);
            this.lblOverdue.TabIndex = 1;
            this.lblOverdue.Text = "0";
            this.lblOverdue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblViolations
            // 
            this.lblViolations.AutoSize = true;
            this.lblViolations.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViolations.Location = new System.Drawing.Point(553, 92);
            this.lblViolations.Name = "lblViolations";
            this.lblViolations.Size = new System.Drawing.Size(31, 32);
            this.lblViolations.TabIndex = 1;
            this.lblViolations.Text = "0";
            this.lblViolations.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBorrowedBooks
            // 
            this.lblBorrowedBooks.AutoSize = true;
            this.lblBorrowedBooks.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBorrowedBooks.Location = new System.Drawing.Point(340, 92);
            this.lblBorrowedBooks.Name = "lblBorrowedBooks";
            this.lblBorrowedBooks.Size = new System.Drawing.Size(31, 32);
            this.lblBorrowedBooks.TabIndex = 1;
            this.lblBorrowedBooks.Text = "0";
            this.lblBorrowedBooks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalBooks
            // 
            this.lblTotalBooks.AutoSize = true;
            this.lblTotalBooks.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalBooks.Location = new System.Drawing.Point(117, 92);
            this.lblTotalBooks.Name = "lblTotalBooks";
            this.lblTotalBooks.Size = new System.Drawing.Size(31, 32);
            this.lblTotalBooks.TabIndex = 1;
            this.lblTotalBooks.Text = "0";
            this.lblTotalBooks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tàiKhoảnToolStripMenuItem,
            this.tínhNăngToolStripMenuItem,
            this.giớiThiệuToolStripMenuItem,
            this.phầnMềmToolStripMenuItem,
            this.quảnLýToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(936, 36);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tàiKhoảnToolStripMenuItem
            // 
            this.tàiKhoảnToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuChangePassword,
            this.menuLogout});
            this.tàiKhoảnToolStripMenuItem.Name = "tàiKhoảnToolStripMenuItem";
            this.tàiKhoảnToolStripMenuItem.Size = new System.Drawing.Size(102, 32);
            this.tàiKhoảnToolStripMenuItem.Text = "Tài khoản";
            // 
            // menuChangePassword
            // 
            this.menuChangePassword.Name = "menuChangePassword";
            this.menuChangePassword.Size = new System.Drawing.Size(279, 34);
            this.menuChangePassword.Text = "Đổi mật khẩu Admin";
            this.menuChangePassword.Click += new System.EventHandler(this.menuChangePassword_Click);
            // 
            // menuLogout
            // 
            this.menuLogout.Name = "menuLogout";
            this.menuLogout.Size = new System.Drawing.Size(279, 34);
            this.menuLogout.Text = "Đăng xuất";
            this.menuLogout.Click += new System.EventHandler(this.menuLogout_Click);
            // 
            // tínhNăngToolStripMenuItem
            // 
            this.tínhNăngToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuExportCSV,
            this.menuExportExcel,
            this.gửiMailCảnhBáoToolStripMenuItem});
            this.tínhNăngToolStripMenuItem.Name = "tínhNăngToolStripMenuItem";
            this.tínhNăngToolStripMenuItem.Size = new System.Drawing.Size(106, 32);
            this.tínhNăngToolStripMenuItem.Text = "Tính năng";
            // 
            // menuExportCSV
            // 
            this.menuExportCSV.Name = "menuExportCSV";
            this.menuExportCSV.Size = new System.Drawing.Size(353, 34);
            this.menuExportCSV.Text = "Xuất toàn bộ CSDL sang CSV";
            // 
            // menuExportExcel
            // 
            this.menuExportExcel.Name = "menuExportExcel";
            this.menuExportExcel.Size = new System.Drawing.Size(353, 34);
            this.menuExportExcel.Text = "Xuất toàn bộ CSDL sang XLSX";
            // 
            // gửiMailCảnhBáoToolStripMenuItem
            // 
            this.gửiMailCảnhBáoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem});
            this.gửiMailCảnhBáoToolStripMenuItem.Name = "gửiMailCảnhBáoToolStripMenuItem";
            this.gửiMailCảnhBáoToolStripMenuItem.Size = new System.Drawing.Size(353, 34);
            this.gửiMailCảnhBáoToolStripMenuItem.Text = "Gửi Mail Cảnh Báo";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(144, 34);
            this.testToolStripMenuItem.Text = "Test";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // giớiThiệuToolStripMenuItem
            // 
            this.giớiThiệuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuManual,
            this.menuAboutUs});
            this.giớiThiệuToolStripMenuItem.Name = "giớiThiệuToolStripMenuItem";
            this.giớiThiệuToolStripMenuItem.Size = new System.Drawing.Size(106, 32);
            this.giớiThiệuToolStripMenuItem.Text = "Giới Thiệu";
            // 
            // menuManual
            // 
            this.menuManual.Name = "menuManual";
            this.menuManual.Size = new System.Drawing.Size(342, 34);
            this.menuManual.Text = "Cách sử dụng";
            // 
            // menuAboutUs
            // 
            this.menuAboutUs.Name = "menuAboutUs";
            this.menuAboutUs.Size = new System.Drawing.Size(342, 34);
            this.menuAboutUs.Text = "Thông tin giới thiệu về Elibse";
            // 
            // phầnMềmToolStripMenuItem
            // 
            this.phầnMềmToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFineSetting,
            this.càiĐặtGiaHạnToolStripMenuItem,
            this.tàiKhoảnGửiMailThôngBáoToolStripMenuItem});
            this.phầnMềmToolStripMenuItem.Name = "phầnMềmToolStripMenuItem";
            this.phầnMềmToolStripMenuItem.Size = new System.Drawing.Size(95, 32);
            this.phầnMềmToolStripMenuItem.Text = "Thiết lập";
            // 
            // menuFineSetting
            // 
            this.menuFineSetting.Name = "menuFineSetting";
            this.menuFineSetting.Size = new System.Drawing.Size(355, 34);
            this.menuFineSetting.Text = "Cài đặt phạt";
            // 
            // càiĐặtGiaHạnToolStripMenuItem
            // 
            this.càiĐặtGiaHạnToolStripMenuItem.Name = "càiĐặtGiaHạnToolStripMenuItem";
            this.càiĐặtGiaHạnToolStripMenuItem.Size = new System.Drawing.Size(355, 34);
            this.càiĐặtGiaHạnToolStripMenuItem.Text = "Cài đặt gia hạn";
            // 
            // tàiKhoảnGửiMailThôngBáoToolStripMenuItem
            // 
            this.tàiKhoảnGửiMailThôngBáoToolStripMenuItem.Name = "tàiKhoảnGửiMailThôngBáoToolStripMenuItem";
            this.tàiKhoảnGửiMailThôngBáoToolStripMenuItem.Size = new System.Drawing.Size(355, 34);
            this.tàiKhoảnGửiMailThôngBáoToolStripMenuItem.Text = "Tài khoản gửi Email thông báo";
            this.tàiKhoảnGửiMailThôngBáoToolStripMenuItem.Click += new System.EventHandler(this.tàiKhoảnGửiMailThôngBáoToolStripMenuItem_Click);
            // 
            // quảnLýToolStripMenuItem
            // 
            this.quảnLýToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuManageReaders,
            this.addCategory,
            this.thêmNhiềuSáchToolStripMenuItem,
            this.thanhLýSáchToolStripMenuItem,
            this.giaHạnTrảSáchToolStripMenuItem});
            this.quảnLýToolStripMenuItem.Name = "quảnLýToolStripMenuItem";
            this.quảnLýToolStripMenuItem.Size = new System.Drawing.Size(89, 32);
            this.quảnLýToolStripMenuItem.Text = "Quản lý";
            // 
            // menuManageReaders
            // 
            this.menuManageReaders.Name = "menuManageReaders";
            this.menuManageReaders.Size = new System.Drawing.Size(363, 34);
            this.menuManageReaders.Text = "Đọc giả";
            // 
            // addCategory
            // 
            this.addCategory.Name = "addCategory";
            this.addCategory.Size = new System.Drawing.Size(363, 34);
            this.addCategory.Text = "Danh mục sách";
            this.addCategory.Click += new System.EventHandler(this.addCategory_Click);
            // 
            // thêmNhiềuSáchToolStripMenuItem
            // 
            this.thêmNhiềuSáchToolStripMenuItem.Name = "thêmNhiềuSáchToolStripMenuItem";
            this.thêmNhiềuSáchToolStripMenuItem.Size = new System.Drawing.Size(363, 34);
            this.thêmNhiềuSáchToolStripMenuItem.Text = "Thêm nhiều sách bằng file Excel";
            // 
            // thanhLýSáchToolStripMenuItem
            // 
            this.thanhLýSáchToolStripMenuItem.Name = "thanhLýSáchToolStripMenuItem";
            this.thanhLýSáchToolStripMenuItem.Size = new System.Drawing.Size(363, 34);
            this.thanhLýSáchToolStripMenuItem.Text = "Thanh lý sách";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnBorrow);
            this.groupBox2.Controls.Add(this.btnReturn);
            this.groupBox2.Controls.Add(this.btnAddBook);
            this.groupBox2.Location = new System.Drawing.Point(582, 321);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(205, 218);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Nghiệp vụ";
            // 
            // btnBorrow
            // 
            this.btnBorrow.Location = new System.Drawing.Point(35, 92);
            this.btnBorrow.Name = "btnBorrow";
            this.btnBorrow.Size = new System.Drawing.Size(136, 49);
            this.btnBorrow.TabIndex = 2;
            this.btnBorrow.Text = "Ký trả sách";
            this.btnBorrow.UseVisualStyleBackColor = true;
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(35, 147);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(136, 49);
            this.btnReturn.TabIndex = 4;
            this.btnReturn.Text = "Ký mượn";
            this.btnReturn.UseVisualStyleBackColor = true;
            // 
            // btnAddBook
            // 
            this.btnAddBook.Location = new System.Drawing.Point(35, 37);
            this.btnAddBook.Name = "btnAddBook";
            this.btnAddBook.Size = new System.Drawing.Size(136, 49);
            this.btnAddBook.TabIndex = 2;
            this.btnAddBook.Text = "Thêm sách mới";
            this.btnAddBook.UseVisualStyleBackColor = true;
            this.btnAddBook.Click += new System.EventHandler(this.btnAddBook_Click);
            // 
            // giaHạnTrảSáchToolStripMenuItem
            // 
            this.giaHạnTrảSáchToolStripMenuItem.Name = "giaHạnTrảSáchToolStripMenuItem";
            this.giaHạnTrảSáchToolStripMenuItem.Size = new System.Drawing.Size(363, 34);
            this.giaHạnTrảSáchToolStripMenuItem.Text = "Gia hạn trả sách";
            // 
            // AdminDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 584);
            this.Controls.Add(this.panel3);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AdminDashboard";
            this.Text = "Elibse: Admin dashboard";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartStats)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblTotalBorrowed;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label lblTotalBooks;
        private System.Windows.Forms.Button btnAddBook;
        private System.Windows.Forms.Button btnBorrow;
        private System.Windows.Forms.Button btnViewTotalBooks;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnViewBorrowed;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tàiKhoảnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tínhNăngToolStripMenuItem;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartStats;
        private System.Windows.Forms.Button btnViewViolators;
        private System.Windows.Forms.Label lblTotalViolations;
        private System.Windows.Forms.Label lblViolations;
        private System.Windows.Forms.Label lblBorrowedBooks;
        private System.Windows.Forms.Button btnViewOverdue;
        private System.Windows.Forms.Label lblTotalOverdue;
        private System.Windows.Forms.Label lblOverdue;
        private System.Windows.Forms.ToolStripMenuItem menuChangePassword;
        private System.Windows.Forms.ToolStripMenuItem menuLogout;
        private System.Windows.Forms.ToolStripMenuItem menuExportCSV;
        private System.Windows.Forms.ToolStripMenuItem menuExportExcel;
        private System.Windows.Forms.ToolStripMenuItem giớiThiệuToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.ToolStripMenuItem menuManual;
        private System.Windows.Forms.ToolStripMenuItem menuAboutUs;
        private System.Windows.Forms.ToolStripMenuItem phầnMềmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuFineSetting;
        private System.Windows.Forms.ToolStripMenuItem quảnLýToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuManageReaders;
        private System.Windows.Forms.ToolStripMenuItem addCategory;
        private System.Windows.Forms.ToolStripMenuItem thêmNhiềuSáchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thanhLýSáchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem càiĐặtGiaHạnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gửiMailCảnhBáoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tàiKhoảnGửiMailThôngBáoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem giaHạnTrảSáchToolStripMenuItem;
    }
}