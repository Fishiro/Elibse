using Elibse.Admin; // Để nhận diện được form AddBook trong thư mục Admin
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Elibse
{
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }

        // --- 1. SỰ KIỆN KHI FORM VỪA MỞ ---
        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            LoadDashboardStats(); // Tải số liệu
            LoadChartData();      // Tải biểu đồ
        }

        // --- 2. HÀM LẤY SỐ LIỆU THỐNG KÊ ---
        private void LoadDashboardStats()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // a. Tổng số sách
                    // SỬA LỖI: Phải dùng đúng tên lblTotalBooks thay vì 'label'
                    string q1 = "SELECT COUNT(*) FROM BOOKS";
                    SqlCommand cmd1 = new SqlCommand(q1, conn);
                    lblTotalBooks.Text = cmd1.ExecuteScalar().ToString();

                    // b. Sách đang được mượn
                    string q2 = "SELECT COUNT(*) FROM LOAN_RECORDS WHERE ReturnDate IS NULL";
                    SqlCommand cmd2 = new SqlCommand(q2, conn);
                    lblBorrowedBooks.Text = cmd2.ExecuteScalar().ToString();

                    // c. Độc giả vi phạm
                    string q3 = "SELECT COUNT(*) FROM READERS WHERE Status = 'Locked'";
                    SqlCommand cmd3 = new SqlCommand(q3, conn);
                    lblTotalViolations.Text = cmd3.ExecuteScalar().ToString();

                    // d. Mượn quá hạn
                    string q4 = "SELECT COUNT(*) FROM LOAN_RECORDS WHERE ReturnDate IS NULL AND DueDate < GETDATE()";
                    SqlCommand cmd4 = new SqlCommand(q4, conn);
                    lblTotalOverdue.Text = cmd4.ExecuteScalar().ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu Dashboard: " + ex.Message);
            }
        }

        // --- 3. HÀM VẼ BIỂU ĐỒ ---
        private void LoadChartData()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT c.CategoryName, COUNT(b.BookID) as Qty 
                                     FROM BOOKS b 
                                     JOIN CATEGORIES c ON b.CategoryID = c.CategoryID 
                                     GROUP BY c.CategoryName";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    chartStats.Series.Clear();

                    Series series = new Series("BooksByCategory");
                    series.ChartType = SeriesChartType.Column;
                    series.IsValueShownAsLabel = true;

                    while (reader.Read())
                    {
                        string catName = reader["CategoryName"].ToString();
                        int qty = Convert.ToInt32(reader["Qty"]);
                        series.Points.AddXY(catName, qty);
                    }

                    chartStats.Series.Add(series);
                }
            }
            catch (Exception ex) { }
        }

        // --- 4. CÁC NÚT BẤM ---

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadDashboardStats();
            LoadChartData();
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            AddBook frm = new AddBook();
            frm.ShowDialog(); // Chặn luồng

            // SỬA LỖI: Gọi đúng tên hàm LoadDashboardStats
            LoadDashboardStats();
        }

        private void addCategory_Click(object sender, EventArgs e)
        {
            CategoryManager frm = new CategoryManager();
            frm.ShowDialog();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Lấy email độc giả từ TextBox (ví dụ)
            string emailNguoiNhan = "nguyenduquicm1@gmail.com";

            string tieuDe = "Thông báo quá hạn trả sách - Thư Viện Số";

            // Nội dung email (có thể dùng thẻ HTML để trình bày đẹp hơn)
            string noiDung = "<h3>Chào bạn,</h3>" +
                             "<p>Bạn đang có sách mượn quá hạn tại thư viện. Vui lòng đến trả sớm để tránh phát sinh phí phạt.</p>" +
                             "<p>Trân trọng,<br><b>Admin Quí</b></p>";

            string signature = "<br><br><hr>" +
                           "<div style='color: #999999; font-style: italic; font-size: 12px; font-family: Arial, sans-serif;'>" +
                           "Email này được gửi tự động từ Hệ thống Quản lý Thư viện.<br>" +
                           "Vui lòng không trả lời tin nhắn này.<br>" +
                           "Developed by Quí | Inspired by Ngân" + // Dòng chi ân tinh tế ở đây
                           "</div>";

            // Gọi hàm gửi mail đã viết ở trên
            EmailService.SendEmail(emailNguoiNhan, tieuDe, noiDung + signature);
        }

        private void tàiKhoảnGửiMailThôngBáoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Admin.NotificationServiceEmail configForm = new Admin.NotificationServiceEmail();
            configForm.ShowDialog();
        }

        private void menuLogout_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại Yes/No chuẩn của Windows
            DialogResult check = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (check == DialogResult.Yes)
            {
                this.Hide(); // Ẩn Dashboard hiện tại

                fmLoginDialog loginDialog = new fmLoginDialog();
                loginDialog.ShowDialog(); // Hiện lại form đăng nhập

                this.Close();
            }
        }

        private void menuChangePassword_Click(object sender, EventArgs e)
        {
            // 1. Khởi tạo form Đổi mật khẩu
            // Lưu ý: Nếu báo lỗi đỏ, hãy chuột phải chọn Quick Actions -> using Elibse.Admin;
            Elibse.Admin.ChangeAdminPassword fm = new Elibse.Admin.ChangeAdminPassword();

            // 2. Hiển thị form dưới dạng Dialog (Cửa sổ con)
            // Dùng ShowDialog() để bắt buộc người dùng xử lý xong form này mới được quay lại Dashboard
            fm.ShowDialog();
        }

        private void btnViewTotalBooks_Click(object sender, EventArgs e)
        {
            // Tạo một đối tượng form TotalBook
            Elibse.Admin.TotalBook frm = new Elibse.Admin.TotalBook();

            // Hiển thị form lên
            // ShowDialog() sẽ chặn không cho thao tác ở Dashboard cho đến khi tắt form con đi (an toàn hơn)
            frm.ShowDialog();

            LoadDashboardStats();
        }

        private void btnViewBorrowed_Click(object sender, EventArgs e)
        {
            // Tạo một đối tượng form TotalBook
            Elibse.BeingBorrowed frm = new Elibse.BeingBorrowed();
            frm.ShowDialog();
        }

        private void btnViewViolators_Click(object sender, EventArgs e)
        {
            
            Elibse.Violator frm = new Elibse.Violator();
            frm.ShowDialog();

            //Tải lại số liệu trên Dashboard ngay sau khi đóng form
            LoadDashboardStats();
        }

        private void btnViewOverdue_Click(object sender, EventArgs e)
        {
            // Tạo một đối tượng form TotalBook
            Elibse.LateReturn frm = new Elibse.LateReturn();
            frm.ShowDialog();
        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            // 1. Khởi tạo form Mượn
            Elibse.Borrow frm = new Elibse.Borrow();

            // 2. Hiện form lên
            frm.ShowDialog();

            // 3. Sau khi tắt form mượn, tải lại số liệu Dashboard (để cập nhật số sách đang mượn tăng lên)
            LoadDashboardStats();
        }

        // --- XỬ LÝ NÚT KÝ TRẢ ---
        private void btnReturn_Click(object sender, EventArgs e)
        {
            // 1. Khởi tạo form Trả (Form này nằm trong namespace Elibse.Admin)
            Elibse.Return frm = new Elibse.Return();

            frm.ShowDialog();

            // 3. Cập nhật lại số liệu Dashboard
            LoadDashboardStats();
        }

        // --- 1. MENU QUẢN LÝ ĐỘC GIẢ (QUAN TRỌNG) ---
        private void menuManageReaders_Click(object sender, EventArgs e)
        {
            // Mở form Quản lý độc giả (TotalReader)
            // Lưu ý: Chúng ta sẽ cần kiểm tra xem form TotalReader đã code xong chưa ở bước sau
            Elibse.Admin.TotalReader frm = new Elibse.Admin.TotalReader();
            frm.ShowDialog();

            // Tải lại thống kê Dashboard sau khi đóng form (vì có thể đã xóa độc giả)
            LoadDashboardStats();
        }

        // --- 2. CÁC MENU GIỚI THIỆU (Làm nhanh để lấy điểm) ---

        private void menuManual_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hướng dẫn sử dụng phần mềm Elibse:\n\n" +
                            "1. Quản lý sách: Thêm, sửa, xóa, nhập kho.\n" +
                            "2. Mượn/Trả: Nhập mã độc giả và mã sách để thực hiện.\n" +
                            "3. Thống kê: Xem các chỉ số trên Dashboard.\n" +
                            "4. Hệ thống: Cấu hình email, đổi mật khẩu.\n\n" +
                            "Mọi thắc mắc vui lòng liên hệ Admin.",
                            "Hướng dẫn sử dụng", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void menuAboutUs_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Phần mềm Quản lý Thư viện (Elibse)\n" +
                            "Phiên bản: 1.0.0 (Beta)\n" +
                            "Ngày phát hành: 01/2026\n" +
                            "Nền tảng: .NET Framework / SQL Server",
                            "Về Elibse", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void vềTácGiảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sinh viên thực hiện:\n" +
                            "- Nguyễn Du Quí\n" +
                            "- (Và sự hỗ trợ tinh thần từ Ngân)\n\n" +
                            "Lớp: Công nghệ thông tin\n" +
                            "Đồ án môn học: Lập trình Windows",
                            "Tác giả", MessageBoxButtons.OK);
        }
    }
}