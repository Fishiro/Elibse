using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Elibse
{
    internal static class Program
    {
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 1. Vẫn giữ tính năng: Kiểm tra xem mật khẩu Admin có rỗng không?
            // (Để đảm bảo Admin luôn có mật khẩu trước khi dùng phần mềm)
            if (IsAdminPasswordEmpty())
            {
                fmFirstSetup setup = new fmFirstSetup();

                // Nếu người dùng không chịu tạo mật khẩu mà tắt đi -> Tắt luôn app
                if (setup.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            // 2. SỬA LẠI DÒNG NÀY: Mở lại form chọn vai trò (LoginDialog)
            // Thay vì new AdminLogin() như lúc nãy
            Application.Run(new fmLoginDialog());
        }

        // --- Hàm kiểm tra mật khẩu rỗng (Giữ nguyên) ---
        static bool IsAdminPasswordEmpty()
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
                return false;
            }
            return false;
        }
    }
}