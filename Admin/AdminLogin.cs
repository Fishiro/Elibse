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

                    // 4. Tạo câu lệnh SQL: Đếm xem có tài khoản nào trùng mật khẩu không
                    // (Vì bạn chọn phương án đơn giản chỉ check pass)
                    string query = "SELECT COUNT(*) FROM ADMINS WHERE Password = @pass";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@pass", password); // Gán tham số an toàn

                    // 5. Thực thi và lấy kết quả
                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        // --- ĐĂNG NHẬP THÀNH CÔNG ---
                        MessageBox.Show("Đăng nhập thành công!", "Xin chào", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Ẩn form đăng nhập đi
                        this.Hide();

                        // Mở form Dashboard lên
                        AdminDashboard dashboard = new AdminDashboard();
                        dashboard.ShowDialog(); // Dùng ShowDialog để khi tắt Dashboard, code sẽ chạy tiếp dòng dưới

                        // Khi Dashboard tắt, đóng luôn form đăng nhập (để thoát chương trình sạch sẽ)
                        this.Close();
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