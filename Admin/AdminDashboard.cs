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
                    lblTotalBorrowed.Text = cmd2.ExecuteScalar().ToString();

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
            string emailNguoiNhan = "";

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
    }
}