using Elibse.Admin;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading.Tasks;
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

        // --- 2. HÀM LẤY SỐ LIỆU THỐNG KÊ (Đã chuẩn theo DB Tiếng Anh) ---
        private void LoadDashboardStats()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // a. Tổng số sách
                    SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM BOOKS", conn);
                    object result1 = cmd1.ExecuteScalar();
                    lblTotalBooks.Text = (result1 != null) ? result1.ToString() : "0";

                    // b. Sách đang được mượn (Chưa trả)
                    SqlCommand cmd2 = new SqlCommand("SELECT COUNT(*) FROM LOAN_RECORDS WHERE ReturnDate IS NULL", conn);
                    object result2 = cmd2.ExecuteScalar();
                    lblBorrowedBooks.Text = (result2 != null) ? result2.ToString() : "0";

                    // c. Độc giả vi phạm (Trạng thái Locked)
                    SqlCommand cmd3 = new SqlCommand("SELECT COUNT(*) FROM READERS WHERE Status = 'Locked'", conn);
                    object result3 = cmd3.ExecuteScalar();
                    lblTotalViolations.Text = (result3 != null) ? result3.ToString() : "0";

                    // d. Mượn quá hạn (Chưa trả + Hạn trả < Hôm nay)
                    SqlCommand cmd4 = new SqlCommand("SELECT COUNT(*) FROM LOAN_RECORDS WHERE ReturnDate IS NULL AND DueDate < GETDATE()", conn);
                    object result4 = cmd4.ExecuteScalar();
                    lblTotalOverdue.Text = (result4 != null) ? result4.ToString() : "0";
                }
            }
            catch (Exception ex)
            {
                // Ghi log thay vì hiện popup để tránh phiền khi mở app
                Console.WriteLine("Lỗi tải thống kê: " + ex.Message);
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
                    // Thống kê sách theo danh mục
                    string query = @"SELECT c.CategoryName, COUNT(b.BookID) as Qty 
                                     FROM BOOKS b 
                                     JOIN CATEGORIES c ON b.CategoryID = c.CategoryID 
                                     GROUP BY c.CategoryName";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    chartStats.Series.Clear();

                    Series series = new Series("BooksByCategory");
                    series.ChartType = SeriesChartType.Column; // Biểu đồ cột
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
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi vẽ biểu đồ: " + ex.Message);
            }
        }

        // --- 4. CÁC NÚT BẤM (BUTTON EVENTS) ---

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadDashboardStats();
            LoadChartData();
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            AddBook frm = new AddBook();
            frm.ShowDialog(); // Chặn luồng để người dùng nhập xong mới về
            LoadDashboardStats(); // Tải lại số liệu sau khi thêm
        }

        private void addCategory_Click(object sender, EventArgs e)
        {
            CategoryManager frm = new CategoryManager();
            frm.ShowDialog();
        }

        private void tàiKhoảnGửiMailThôngBáoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Admin.ConfigNotificationServiceEmail configForm = new Admin.ConfigNotificationServiceEmail();
            configForm.ShowDialog();
        }

        private void menuLogout_Click(object sender, EventArgs e)
        {
            DialogResult check = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (check == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void menuChangePassword_Click(object sender, EventArgs e)
        {
            Elibse.Admin.ChangeAdminPassword fm = new Elibse.Admin.ChangeAdminPassword();
            fm.ShowDialog();
        }

        private void btnViewTotalBooks_Click(object sender, EventArgs e)
        {
            Elibse.Admin.TotalBook frm = new Elibse.Admin.TotalBook();
            frm.ShowDialog();
            LoadDashboardStats();
        }

        private void btnViewBorrowed_Click(object sender, EventArgs e)
        {
            Elibse.BeingBorrowed frm = new Elibse.BeingBorrowed();
            frm.ShowDialog();
        }

        private void btnViewViolators_Click(object sender, EventArgs e)
        {
            Elibse.Violator frm = new Elibse.Violator();
            frm.ShowDialog();
            LoadDashboardStats();
        }

        private void btnViewOverdue_Click(object sender, EventArgs e)
        {
            Elibse.LateReturn frm = new Elibse.LateReturn();
            frm.ShowDialog();
        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            Elibse.Borrow frm = new Elibse.Borrow();
            frm.ShowDialog();
            LoadDashboardStats();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            Elibse.Return frm = new Elibse.Return();
            frm.ShowDialog();
            LoadDashboardStats();
        }

        private void menuManageReaders_Click(object sender, EventArgs e)
        {
            Elibse.Admin.TotalReader frm = new Elibse.Admin.TotalReader();
            frm.ShowDialog();
            LoadDashboardStats();
        }

        // --- 5. CÁC MENU GIỚI THIỆU & TIỆN ÍCH ---

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
                            "Phiên bản: 1.0.0 (Release)\n" +
                            "Ngày phát hành: 01/2026\n" +
                            "Nền tảng: .NET Framework / SQL Server\n" +
                            "Hỗ trợ Reporting: Crystal Report\n" +
                            "Mã nguồn: https://github.com/Fishiro/Elibse", 
                            "Về Elibse", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void vềTácGiảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sinh viên thực hiện:\n" +
                            "- Nguyễn Dư Quí (CK2402A33)\n" +
                            "- Sơn Yến Vy (CK2402A32)\n" +
                            "- Bùi Nguyễn Minh Thư (CK2402A25)\n" +
                            "\n\n" +
                            "Lớp: CNTT K17\n" +
                            "Đồ án môn học: Lập trình Windows",
                            "Tác giả", MessageBoxButtons.OK);
        }

        private void tớiTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Elibse.Admin.SendEmailForm frm = new Elibse.Admin.SendEmailForm();
            frm.ShowDialog();
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            Elibse.AdminHistory frm = new Elibse.AdminHistory();
            frm.ShowDialog();
        }

        private void giaHạnTrảSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExtendLoan frm = new ExtendLoan();
            frm.ShowDialog();
        }

        private void menuFineSetting_Click(object sender, EventArgs e)
        {
            Admin.ConfigPenalty fm = new Admin.ConfigPenalty();
            fm.ShowDialog();
        }

        private void xuấtBáoCáoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmReportBook reportForm = new fmReportBook();
            reportForm.ShowDialog();
        }

        private void càiĐặtGiaHạnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigExtend frm = new ConfigExtend();
            frm.ShowDialog();
        }

        // --- 6. LOGIC GỬI EMAIL TỰ ĐỘNG (QUAN TRỌNG NHẤT) ---

        // Menu: Gửi cảnh báo quá hạn
        private void quáHạnTrảSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn gửi email nhắc nhở đến tất cả độc giả đang bị QUÁ HẠN trả sách không?",
                "Xác nhận gửi email hàng loạt",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                ThucHienGuiEmail(onlyOverdue: true);
            }
        }

        // Menu: Gửi thông báo trạng thái
        private void thôngBáoTrạngTháiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có muốn gửi email thông báo danh sách sách đang mượn cho TOÀN BỘ độc giả không?",
                "Xác nhận gửi thông báo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                ThucHienGuiEmail(onlyOverdue: false);
            }
        }

        // Hàm xử lý chung
        private async void ThucHienGuiEmail(bool onlyOverdue)
        {
            try
            {
                await Task.Run(() =>
                {
                    // 1. Chuẩn bị câu truy vấn SQL
                    // Điều kiện cơ bản: Sách chưa trả (ReturnDate IS NULL)
                    string condition = "lr.ReturnDate IS NULL";

                    if (onlyOverdue)
                    {
                        // Nếu là quá hạn: Thêm điều kiện DueDate < Ngày hiện tại
                        condition += " AND lr.DueDate < GETDATE()";
                    }

                    // Query kết nối 3 bảng: LOAN_RECORDS - READERS - BOOKS
                    string query = $@"
                        SELECT 
                            r.FullName,
                            r.Email,
                            b.Title,
                            lr.LoanDate,
                            lr.DueDate
                        FROM LOAN_RECORDS lr
                        JOIN READERS r ON lr.ReaderID = r.ReaderID
                        JOIN BOOKS b ON lr.BookID = b.BookID
                        WHERE {condition} 
                        AND r.Email IS NOT NULL 
                        AND r.Email <> ''";

                    DataTable dt = DatabaseConnection.GetDataTable(query);

                    if (dt.Rows.Count == 0)
                    {
                        Invoke(new Action(() => MessageBox.Show("Không tìm thấy dữ liệu phù hợp để gửi mail!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)));
                        return;
                    }

                    // 2. Gom nhóm sách theo Email
                    var mailQueue = new Dictionary<string, (string TenDocGia, StringBuilder NoiDungSach)>();

                    foreach (DataRow row in dt.Rows)
                    {
                        string email = row["Email"].ToString();
                        string tenDocGia = row["FullName"].ToString(); // Tiếng Anh: FullName
                        string tenSach = row["Title"].ToString();      // Tiếng Anh: Title
                        string hanTra = Convert.ToDateTime(row["DueDate"]).ToString("dd/MM/yyyy"); // Tiếng Anh: DueDate

                        if (!mailQueue.ContainsKey(email))
                        {
                            mailQueue[email] = (tenDocGia, new StringBuilder());
                        }

                        // Format dòng sách
                        mailQueue[email].NoiDungSach.AppendLine($"- {tenSach} (Hạn trả: {hanTra})");
                    }

                    // 3. Gửi Email
                    int sentCount = 0;
                    int errorCount = 0;

                    foreach (var item in mailQueue)
                    {
                        string emailNhan = item.Key;
                        string tenNhan = item.Value.TenDocGia;
                        string listSach = item.Value.NoiDungSach.ToString();

                        string subject = "";
                        string body = "";

                        if (onlyOverdue)
                        {
                            subject = "[THƯ VIỆN ELIBSE] CẢNH BÁO QUÁ HẠN TRẢ SÁCH";
                            body = $"Chào {tenNhan},<br/><br/>" +
                                   $"Hệ thống ghi nhận bạn đang có các cuốn sách <b>ĐÃ QUÁ HẠN</b> trả:<br/>" +
                                   $"<pre style='font-family:Arial; font-size:14px;'>{listSach}</pre><br/>" +
                                   $"Vui lòng mang sách đến trả gấp để tránh phí phạt.<br/>Trân trọng,<br/>Thư viện Elibse";
                        }
                        else
                        {
                            subject = "[THƯ VIỆN ELIBSE] THÔNG BÁO TRẠNG THÁI MƯỢN SÁCH";
                            body = $"Chào {tenNhan},<br/><br/>" +
                                   $"Đây là danh sách sách bạn đang mượn:<br/>" +
                                   $"<pre style='font-family:Arial; font-size:14px;'>{listSach}</pre><br/>" +
                                   $"Vui lòng lưu ý hạn trả.<br/>Trân trọng,<br/>Thư viện Elibse";
                        }

                        // Gọi hàm SendEmail (Trả về bool, không popup)
                        bool isSuccess = EmailService.SendEmail(emailNhan, subject, body);

                        if (isSuccess) sentCount++;
                        else errorCount++;
                    }

                    // 4. Thông báo kết quả
                    Invoke(new Action(() =>
                    {
                        string msg = $"Đã gửi thành công: {sentCount} email.";
                        if (errorCount > 0) msg += $"\nGửi thất bại: {errorCount} email (Kiểm tra lại cấu hình).";

                        MessageBoxIcon icon = (errorCount > 0) ? MessageBoxIcon.Warning : MessageBoxIcon.Information;
                        MessageBox.Show(msg, "Hoàn tất tác vụ", MessageBoxButtons.OK, icon);
                    }));
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
            }
        }
    }
}