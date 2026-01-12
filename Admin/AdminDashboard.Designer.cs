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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnHistory = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.chartStats = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnViewOverdue = new System.Windows.Forms.Button();
            this.btnViewViolators = new System.Windows.Forms.Button();
            this.btnViewBorrowed = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.lbl4 = new System.Windows.Forms.Label();
            this.lbl3 = new System.Windows.Forms.Label();
            this.lbl = new System.Windows.Forms.Label();
            this.btnViewTotalBooks = new System.Windows.Forms.Button();
            this.lblTotalOverdue = new System.Windows.Forms.Label();
            this.lblTotalViolations = new System.Windows.Forms.Label();
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
            this.tớiTàiKhoảnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quáHạnTrảSáchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thôngBáoTrạngTháiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.giớiThiệuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuManual = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAboutUs = new System.Windows.Forms.ToolStripMenuItem();
            this.vềTácGiảToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.phầnMềmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFineSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.càiĐặtGiaHạnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tàiKhoảnGửiMailThôngBáoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tựĐộngGửiEmailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quảnLýToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuManageReaders = new System.Windows.Forms.ToolStripMenuItem();
            this.addCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.thêmNhiềuSáchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bằngXLSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bằngCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.giaHạnTrảSáchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnReturn = new System.Windows.Forms.Button();
            this.btnBorrow = new System.Windows.Forms.Button();
            this.btnAddBook = new System.Windows.Forms.Button();
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
            this.panel3.Size = new System.Drawing.Size(936, 585);
            this.panel3.TabIndex = 2;
            // 
            // btnHistory
            // 
            this.btnHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHistory.Location = new System.Drawing.Point(822, 383);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(80, 37);
            this.btnHistory.TabIndex = 9;
            this.btnHistory.Text = "Lịch sử";
            this.btnHistory.UseVisualStyleBackColor = true;
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // btnReload
            // 
            this.btnReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
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
            this.chartStats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.chartStats.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartStats.Legends.Add(legend1);
            this.chartStats.Location = new System.Drawing.Point(12, 298);
            this.chartStats.Name = "chartStats";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartStats.Series.Add(series1);
            this.chartStats.Size = new System.Drawing.Size(538, 260);
            this.chartStats.TabIndex = 7;
            this.chartStats.Text = "chart2";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnViewOverdue);
            this.groupBox1.Controls.Add(this.btnViewViolators);
            this.groupBox1.Controls.Add(this.btnViewBorrowed);
            this.groupBox1.Controls.Add(this.label);
            this.groupBox1.Controls.Add(this.lbl4);
            this.groupBox1.Controls.Add(this.lbl3);
            this.groupBox1.Controls.Add(this.lbl);
            this.groupBox1.Controls.Add(this.btnViewTotalBooks);
            this.groupBox1.Controls.Add(this.lblTotalOverdue);
            this.groupBox1.Controls.Add(this.lblTotalViolations);
            this.groupBox1.Controls.Add(this.lblBorrowedBooks);
            this.groupBox1.Controls.Add(this.lblTotalBooks);
            this.groupBox1.Location = new System.Drawing.Point(12, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(912, 229);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thống kê";
            // 
            // btnViewOverdue
            // 
            this.btnViewOverdue.Location = new System.Drawing.Point(716, 132);
            this.btnViewOverdue.Name = "btnViewOverdue";
            this.btnViewOverdue.Size = new System.Drawing.Size(72, 35);
            this.btnViewOverdue.TabIndex = 2;
            this.btnViewOverdue.Text = "Xem";
            this.btnViewOverdue.UseVisualStyleBackColor = true;
            this.btnViewOverdue.Click += new System.EventHandler(this.btnViewOverdue_Click);
            // 
            // btnViewViolators
            // 
            this.btnViewViolators.Location = new System.Drawing.Point(531, 132);
            this.btnViewViolators.Name = "btnViewViolators";
            this.btnViewViolators.Size = new System.Drawing.Size(72, 35);
            this.btnViewViolators.TabIndex = 2;
            this.btnViewViolators.Text = "Xem";
            this.btnViewViolators.UseVisualStyleBackColor = true;
            this.btnViewViolators.Click += new System.EventHandler(this.btnViewViolators_Click);
            // 
            // btnViewBorrowed
            // 
            this.btnViewBorrowed.Location = new System.Drawing.Point(318, 132);
            this.btnViewBorrowed.Name = "btnViewBorrowed";
            this.btnViewBorrowed.Size = new System.Drawing.Size(72, 35);
            this.btnViewBorrowed.TabIndex = 2;
            this.btnViewBorrowed.Text = "Xem";
            this.btnViewBorrowed.UseVisualStyleBackColor = true;
            this.btnViewBorrowed.Click += new System.EventHandler(this.btnViewBorrowed_Click);
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
            // lbl4
            // 
            this.lbl4.AutoSize = true;
            this.lbl4.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lbl4.Location = new System.Drawing.Point(698, 65);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(111, 20);
            this.lbl4.TabIndex = 0;
            this.lbl4.Text = "Mượn quá hạn";
            this.lbl4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl3
            // 
            this.lbl3.AutoSize = true;
            this.lbl3.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lbl3.Location = new System.Drawing.Point(508, 65);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(121, 20);
            this.lbl3.TabIndex = 0;
            this.lbl3.Text = "Độc giả vi phạm";
            this.lbl3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lbl.Location = new System.Drawing.Point(273, 65);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(168, 20);
            this.lbl.TabIndex = 0;
            this.lbl.Text = "Tổng đang được mượn";
            this.lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnViewTotalBooks
            // 
            this.btnViewTotalBooks.Location = new System.Drawing.Point(96, 132);
            this.btnViewTotalBooks.Name = "btnViewTotalBooks";
            this.btnViewTotalBooks.Size = new System.Drawing.Size(72, 35);
            this.btnViewTotalBooks.TabIndex = 2;
            this.btnViewTotalBooks.Text = "Xem";
            this.btnViewTotalBooks.UseVisualStyleBackColor = true;
            this.btnViewTotalBooks.Click += new System.EventHandler(this.btnViewTotalBooks_Click);
            // 
            // lblTotalOverdue
            // 
            this.lblTotalOverdue.AutoSize = true;
            this.lblTotalOverdue.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalOverdue.Location = new System.Drawing.Point(738, 92);
            this.lblTotalOverdue.Name = "lblTotalOverdue";
            this.lblTotalOverdue.Size = new System.Drawing.Size(21, 24);
            this.lblTotalOverdue.TabIndex = 1;
            this.lblTotalOverdue.Text = "0";
            this.lblTotalOverdue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalViolations
            // 
            this.lblTotalViolations.AutoSize = true;
            this.lblTotalViolations.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalViolations.Location = new System.Drawing.Point(554, 92);
            this.lblTotalViolations.Name = "lblTotalViolations";
            this.lblTotalViolations.Size = new System.Drawing.Size(21, 24);
            this.lblTotalViolations.TabIndex = 1;
            this.lblTotalViolations.Text = "0";
            this.lblTotalViolations.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBorrowedBooks
            // 
            this.lblBorrowedBooks.AutoSize = true;
            this.lblBorrowedBooks.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBorrowedBooks.Location = new System.Drawing.Point(340, 92);
            this.lblBorrowedBooks.Name = "lblBorrowedBooks";
            this.lblBorrowedBooks.Size = new System.Drawing.Size(21, 24);
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
            this.lblTotalBooks.Size = new System.Drawing.Size(21, 24);
            this.lblTotalBooks.TabIndex = 1;
            this.lblTotalBooks.Text = "0";
            this.lblTotalBooks.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tàiKhoảnToolStripMenuItem,
            this.tínhNăngToolStripMenuItem,
            this.giớiThiệuToolStripMenuItem,
            this.phầnMềmToolStripMenuItem,
            this.quảnLýToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(936, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tàiKhoảnToolStripMenuItem
            // 
            this.tàiKhoảnToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuChangePassword,
            this.menuLogout});
            this.tàiKhoảnToolStripMenuItem.Name = "tàiKhoảnToolStripMenuItem";
            this.tàiKhoảnToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.tàiKhoảnToolStripMenuItem.Text = "Tài khoản";
            // 
            // menuChangePassword
            // 
            this.menuChangePassword.Name = "menuChangePassword";
            this.menuChangePassword.Size = new System.Drawing.Size(184, 22);
            this.menuChangePassword.Text = "Đổi mật khẩu Admin";
            this.menuChangePassword.Click += new System.EventHandler(this.menuChangePassword_Click);
            // 
            // menuLogout
            // 
            this.menuLogout.Name = "menuLogout";
            this.menuLogout.Size = new System.Drawing.Size(184, 22);
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
            this.tínhNăngToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.tínhNăngToolStripMenuItem.Text = "Tính năng";
            // 
            // menuExportCSV
            // 
            this.menuExportCSV.Name = "menuExportCSV";
            this.menuExportCSV.Size = new System.Drawing.Size(230, 22);
            this.menuExportCSV.Text = "Xuất toàn bộ CSDL sang CSV";
            // 
            // menuExportExcel
            // 
            this.menuExportExcel.Name = "menuExportExcel";
            this.menuExportExcel.Size = new System.Drawing.Size(230, 22);
            this.menuExportExcel.Text = "Xuất toàn bộ CSDL sang XLSX";
            // 
            // gửiMailCảnhBáoToolStripMenuItem
            // 
            this.gửiMailCảnhBáoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem,
            this.tớiTàiKhoảnToolStripMenuItem,
            this.quáHạnTrảSáchToolStripMenuItem,
            this.thôngBáoTrạngTháiToolStripMenuItem});
            this.gửiMailCảnhBáoToolStripMenuItem.Name = "gửiMailCảnhBáoToolStripMenuItem";
            this.gửiMailCảnhBáoToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.gửiMailCảnhBáoToolStripMenuItem.Text = "Gửi Mail Cảnh Báo";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.testToolStripMenuItem.Text = "Test";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // tớiTàiKhoảnToolStripMenuItem
            // 
            this.tớiTàiKhoảnToolStripMenuItem.Name = "tớiTàiKhoảnToolStripMenuItem";
            this.tớiTàiKhoảnToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.tớiTàiKhoảnToolStripMenuItem.Text = "Tới tài khoản";
            this.tớiTàiKhoảnToolStripMenuItem.Click += new System.EventHandler(this.tớiTàiKhoảnToolStripMenuItem_Click);
            // 
            // quáHạnTrảSáchToolStripMenuItem
            // 
            this.quáHạnTrảSáchToolStripMenuItem.Name = "quáHạnTrảSáchToolStripMenuItem";
            this.quáHạnTrảSáchToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.quáHạnTrảSáchToolStripMenuItem.Text = "Quá hạn trả sách";
            // 
            // thôngBáoTrạngTháiToolStripMenuItem
            // 
            this.thôngBáoTrạngTháiToolStripMenuItem.Name = "thôngBáoTrạngTháiToolStripMenuItem";
            this.thôngBáoTrạngTháiToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.thôngBáoTrạngTháiToolStripMenuItem.Text = "Thông báo trạng thái";
            // 
            // giớiThiệuToolStripMenuItem
            // 
            this.giớiThiệuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuManual,
            this.menuAboutUs,
            this.vềTácGiảToolStripMenuItem});
            this.giớiThiệuToolStripMenuItem.Name = "giớiThiệuToolStripMenuItem";
            this.giớiThiệuToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.giớiThiệuToolStripMenuItem.Text = "Giới Thiệu";
            // 
            // menuManual
            // 
            this.menuManual.Name = "menuManual";
            this.menuManual.Size = new System.Drawing.Size(147, 22);
            this.menuManual.Text = "Cách sử dụng";
            this.menuManual.Click += new System.EventHandler(this.menuManual_Click);
            // 
            // menuAboutUs
            // 
            this.menuAboutUs.Name = "menuAboutUs";
            this.menuAboutUs.Size = new System.Drawing.Size(147, 22);
            this.menuAboutUs.Text = "Về Elibse";
            this.menuAboutUs.Click += new System.EventHandler(this.menuAboutUs_Click);
            // 
            // vềTácGiảToolStripMenuItem
            // 
            this.vềTácGiảToolStripMenuItem.Name = "vềTácGiảToolStripMenuItem";
            this.vềTácGiảToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.vềTácGiảToolStripMenuItem.Text = "Về tác giả";
            this.vềTácGiảToolStripMenuItem.Click += new System.EventHandler(this.vềTácGiảToolStripMenuItem_Click);
            // 
            // phầnMềmToolStripMenuItem
            // 
            this.phầnMềmToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFineSetting,
            this.càiĐặtGiaHạnToolStripMenuItem,
            this.tàiKhoảnGửiMailThôngBáoToolStripMenuItem,
            this.tựĐộngGửiEmailToolStripMenuItem});
            this.phầnMềmToolStripMenuItem.Name = "phầnMềmToolStripMenuItem";
            this.phầnMềmToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.phầnMềmToolStripMenuItem.Text = "Thiết lập";
            // 
            // menuFineSetting
            // 
            this.menuFineSetting.Name = "menuFineSetting";
            this.menuFineSetting.Size = new System.Drawing.Size(235, 22);
            this.menuFineSetting.Text = "Cài đặt phạt";
            // 
            // càiĐặtGiaHạnToolStripMenuItem
            // 
            this.càiĐặtGiaHạnToolStripMenuItem.Name = "càiĐặtGiaHạnToolStripMenuItem";
            this.càiĐặtGiaHạnToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.càiĐặtGiaHạnToolStripMenuItem.Text = "Cài đặt gia hạn";
            // 
            // tàiKhoảnGửiMailThôngBáoToolStripMenuItem
            // 
            this.tàiKhoảnGửiMailThôngBáoToolStripMenuItem.Name = "tàiKhoảnGửiMailThôngBáoToolStripMenuItem";
            this.tàiKhoảnGửiMailThôngBáoToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.tàiKhoảnGửiMailThôngBáoToolStripMenuItem.Text = "Tài khoản gửi Email thông báo";
            this.tàiKhoảnGửiMailThôngBáoToolStripMenuItem.Click += new System.EventHandler(this.tàiKhoảnGửiMailThôngBáoToolStripMenuItem_Click);
            // 
            // tựĐộngGửiEmailToolStripMenuItem
            // 
            this.tựĐộngGửiEmailToolStripMenuItem.Name = "tựĐộngGửiEmailToolStripMenuItem";
            this.tựĐộngGửiEmailToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.tựĐộngGửiEmailToolStripMenuItem.Text = "Tự động gửi Email";
            // 
            // quảnLýToolStripMenuItem
            // 
            this.quảnLýToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuManageReaders,
            this.addCategory,
            this.thêmNhiềuSáchToolStripMenuItem,
            this.giaHạnTrảSáchToolStripMenuItem});
            this.quảnLýToolStripMenuItem.Name = "quảnLýToolStripMenuItem";
            this.quảnLýToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.quảnLýToolStripMenuItem.Text = "Quản lý";
            // 
            // menuManageReaders
            // 
            this.menuManageReaders.Name = "menuManageReaders";
            this.menuManageReaders.Size = new System.Drawing.Size(180, 22);
            this.menuManageReaders.Text = "Độc giả";
            this.menuManageReaders.Click += new System.EventHandler(this.menuManageReaders_Click);
            // 
            // addCategory
            // 
            this.addCategory.Name = "addCategory";
            this.addCategory.Size = new System.Drawing.Size(180, 22);
            this.addCategory.Text = "Danh mục sách";
            this.addCategory.Click += new System.EventHandler(this.addCategory_Click);
            // 
            // thêmNhiềuSáchToolStripMenuItem
            // 
            this.thêmNhiềuSáchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bằngXLSToolStripMenuItem,
            this.bằngCSVToolStripMenuItem});
            this.thêmNhiềuSáchToolStripMenuItem.Name = "thêmNhiềuSáchToolStripMenuItem";
            this.thêmNhiềuSáchToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.thêmNhiềuSáchToolStripMenuItem.Text = "Thêm nhiều sách";
            // 
            // bằngXLSToolStripMenuItem
            // 
            this.bằngXLSToolStripMenuItem.Name = "bằngXLSToolStripMenuItem";
            this.bằngXLSToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.bằngXLSToolStripMenuItem.Text = "Bằng XLSX";
            this.bằngXLSToolStripMenuItem.Click += new System.EventHandler(this.bằngXLSToolStripMenuItem_Click);
            // 
            // bằngCSVToolStripMenuItem
            // 
            this.bằngCSVToolStripMenuItem.Name = "bằngCSVToolStripMenuItem";
            this.bằngCSVToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.bằngCSVToolStripMenuItem.Text = "Bằng CSV";
            this.bằngCSVToolStripMenuItem.Click += new System.EventHandler(this.bằngCSVToolStripMenuItem_Click);
            // 
            // giaHạnTrảSáchToolStripMenuItem
            // 
            this.giaHạnTrảSáchToolStripMenuItem.Name = "giaHạnTrảSáchToolStripMenuItem";
            this.giaHạnTrảSáchToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.giaHạnTrảSáchToolStripMenuItem.Text = "Gia hạn trả sách";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnReturn);
            this.groupBox2.Controls.Add(this.btnBorrow);
            this.groupBox2.Controls.Add(this.btnAddBook);
            this.groupBox2.Location = new System.Drawing.Point(582, 322);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(206, 218);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Nghiệp vụ";
            // 
            // btnReturn
            // 
            this.btnReturn.Location = new System.Drawing.Point(34, 92);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(136, 49);
            this.btnReturn.TabIndex = 2;
            this.btnReturn.Text = "Ký trả sách";
            this.btnReturn.UseVisualStyleBackColor = true;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // btnBorrow
            // 
            this.btnBorrow.Location = new System.Drawing.Point(34, 148);
            this.btnBorrow.Name = "btnBorrow";
            this.btnBorrow.Size = new System.Drawing.Size(136, 49);
            this.btnBorrow.TabIndex = 4;
            this.btnBorrow.Text = "Ký mượn";
            this.btnBorrow.UseVisualStyleBackColor = true;
            this.btnBorrow.Click += new System.EventHandler(this.btnBorrow_Click);
            // 
            // btnAddBook
            // 
            this.btnAddBook.Location = new System.Drawing.Point(34, 37);
            this.btnAddBook.Name = "btnAddBook";
            this.btnAddBook.Size = new System.Drawing.Size(136, 49);
            this.btnAddBook.TabIndex = 2;
            this.btnAddBook.Text = "Thêm sách mới";
            this.btnAddBook.UseVisualStyleBackColor = true;
            this.btnAddBook.Click += new System.EventHandler(this.btnAddBook_Click);
            // 
            // AdminDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 585);
            this.Controls.Add(this.panel3);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AdminDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Elibse: Admin dashboard";
            this.Load += new System.EventHandler(this.AdminDashboard_Load);
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
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label lblTotalBooks;
        private System.Windows.Forms.Button btnAddBook;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnViewTotalBooks;
        private System.Windows.Forms.Button btnBorrow;
        private System.Windows.Forms.Button btnViewBorrowed;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tàiKhoảnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tínhNăngToolStripMenuItem;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartStats;
        private System.Windows.Forms.Button btnViewViolators;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.Label lblTotalViolations;
        private System.Windows.Forms.Label lblBorrowedBooks;
        private System.Windows.Forms.Button btnViewOverdue;
        private System.Windows.Forms.Label lbl4;
        private System.Windows.Forms.Label lblTotalOverdue;
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
        private System.Windows.Forms.ToolStripMenuItem càiĐặtGiaHạnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gửiMailCảnhBáoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tàiKhoảnGửiMailThôngBáoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem giaHạnTrảSáchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tớiTàiKhoảnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quáHạnTrảSáchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tựĐộngGửiEmailToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vềTácGiảToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thôngBáoTrạngTháiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bằngXLSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bằngCSVToolStripMenuItem;
    }
}