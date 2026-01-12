using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Elibse.Admin; // Để gọi được AdminLogin và fmFirstSetup

namespace Elibse
{
    public partial class fmLoginDialog : Form
    {
        public fmLoginDialog()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        // --- 1. NÚT ĐỘC GIẢ (Giữ nguyên) ---
        private void btnReaderChoice_Click(object sender, EventArgs e)
        {
            this.Hide();
            ReaderLogin frmReader = new ReaderLogin();
            frmReader.ShowDialog();
            this.Show();
        }

        // --- 2. NÚT ADMIN (Sửa lại logic kiểm tra tại đây) ---
        private void btnAdminChoice_Click(object sender, EventArgs e)
        {
            // Kiểm tra: Nếu chưa có mật khẩu thì bắt tạo trước
            if (IsAdminPasswordEmpty())
            {
                // Mở form thiết lập lần đầu
                fmFirstSetup setup = new fmFirstSetup();

                // Nếu tạo xong OK thì mới cho qua form đăng nhập
                if (setup.ShowDialog() == DialogResult.OK)
                {
                    OpenAdminLogin();
                }
                // Nếu họ tắt form setup đi (Cancel) thì không làm gì cả (ở lại đây)
            }
            else
            {
                // Nếu có mật khẩu rồi thì vào đăng nhập luôn
                OpenAdminLogin();
            }
        }

        // Hàm mở form đăng nhập Admin (cho gọn code)
        private void OpenAdminLogin()
        {
            this.Hide();
            AdminLogin frmAdmin = new AdminLogin();
            frmAdmin.ShowDialog();
            this.Show();
        }

        // --- 3. HÀM KIỂM TRA MẬT KHẨU (Mang từ Program.cs sang) ---
        private bool IsAdminPasswordEmpty()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    // Lấy mật khẩu của username 'admin'
                    SqlCommand cmd = new SqlCommand("SELECT Password FROM ADMINS WHERE Username = 'admin'", conn);
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        // Trả về TRUE nếu mật khẩu là chuỗi rỗng
                        return string.IsNullOrEmpty(result.ToString());
                    }
                }
            }
            catch
            {
                // Nếu lỗi kết nối, tạm thời coi như không rỗng để tránh kẹt
                return false;
            }
            return false;
        }
    }
}