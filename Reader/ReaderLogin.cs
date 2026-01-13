using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Elibse.Reader; // Để gọi được ReaderDashboard

namespace Elibse
{
    public partial class ReaderLogin : Form
    {
        public ReaderLogin()
        {
            InitializeComponent();
            // Mẹo thẩm mỹ: Đặt password char mặc định nếu quên chỉnh trong designer
            if (txtPassword != null) txtPassword.UseSystemPasswordChar = true;
        }

        // --- NÚT ĐĂNG NHẬP: Mở ReaderDashboard ---
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim(); // Có thể là ReaderID hoặc Email
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu!");
                return;
            }

            string passwordCheck = SecurityHelper.HashPassword(password);

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    // Kiểm tra đăng nhập (Cho phép nhập ID hoặc Email đều được)
                    // Chỉ cho phép nếu Status = 'Active'
                    string query = @"SELECT ReaderID, FullName FROM READERS 
                                     WHERE (ReaderID = @u OR Email = @u) 
                                     AND Password = @p AND Status = 'Active'";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@p", passwordCheck);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string readerID = reader["ReaderID"].ToString();
                            string fullName = reader["FullName"].ToString();

                            MessageBox.Show("Chào mừng trở lại, " + fullName + "!");

                            // --- LIÊN KẾT QUAN TRỌNG NHẤT ---
                            // Truyền ID sang Dashboard để bên đó load đúng dữ liệu
                            ReaderDashboard dash = new ReaderDashboard(readerID);

                            this.Hide(); // Ẩn form đăng nhập đi
                            dash.ShowDialog(); // Hiện Dashboard
                            this.Show(); // Khi tắt Dashboard thì hiện lại form đăng nhập (hoặc dùng Close() tùy bạn)
                        }
                        else
                        {
                            MessageBox.Show("Sai thông tin đăng nhập hoặc tài khoản bị khóa!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            ReaderRegister frmReg = new ReaderRegister();
            frmReg.ShowDialog(); // Dùng ShowDialog để khi tắt form đăng ký mới quay lại đây
        }
    }
}