using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Elibse
{
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        // Sự kiện khi nhấn nút Đăng Nhập
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // 1. Lấy mật khẩu người dùng nhập
            string password = txtPassword.Text.Trim(); // Trim() để cắt khoảng trắng thừa

            // 2. Kiểm tra nếu để trống
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 3. Kết nối CSDL
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // SỬA ĐỔI: Thay vì đếm COUNT, ta lấy luôn cột Username để biết ai đang vào
                    string query = "SELECT Username FROM ADMINS WHERE Password = @pass";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@pass", password);

                    // Dùng ExecuteScalar để lấy giá trị cột đầu tiên (Username)
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        // --- ĐĂNG NHẬP THÀNH CÔNG ---
                        string currentAdmin = result.ToString();

                        // 1. Lưu tên Admin vào phiên làm việc (Session)
                        Session.CurrentAdminUsername = currentAdmin;

                        // 2. GHI LOG: Đăng nhập thành công
                        Logger.Log("Đăng Nhập", "Đăng nhập vào hệ thống");

                        MessageBox.Show("Xin chào " + currentAdmin + "!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Hide(); // Ẩn form đăng nhập đi

                        AdminDashboard dashboard = new AdminDashboard();
                        dashboard.ShowDialog(); // Chương trình sẽ dừng ở đây cho đến khi Dashboard đóng lại

                        // Khi Dashboard đóng lại (do đăng xuất hoặc tắt), dòng này mới chạy:
                        this.Show(); // Hiện lại form đăng nhập để người khác vào
                        txtPassword.Clear(); // Xóa mật khẩu cũ đi cho an toàn
                    }
                    else
                    {
                        // --- ĐĂNG NHẬP THẤT BẠI ---
                        MessageBox.Show("Mật khẩu không đúng! Vui lòng thử lại.", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPassword.Focus(); // Đưa con trỏ chuột về ô nhập lại
                        txtPassword.SelectAll(); // Bôi đen mật khẩu sai cho tiện sửa
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu không kết nối được Database
                MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}