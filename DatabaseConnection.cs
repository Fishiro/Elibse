using System.Data.SqlClient;

namespace Elibse
{
    public class DatabaseConnection
    {
        private static string ConnectionString
        {
            get
            {
                // 1. Lấy tên Server đã lưu trong cài đặt
                string serverName = Properties.Settings.Default.ServerName;

                // 2. Nếu chưa có gì (lần đầu chạy), dùng mặc định .\SQLEXPRESS
                if (string.IsNullOrEmpty(serverName))
                {
                    serverName = @".\SQLEXPRESS";
                }

                // 3. Trả về chuỗi kết nối hoàn chỉnh
                return string.Format(@"Data Source={0};Initial Catalog=ElibseDB;Integrated Security=True", serverName);
            }
        }

        public static SqlConnection GetConnection()
        {
            // Sử dụng Property ConnectionString vừa viết ở trên
            return new SqlConnection(ConnectionString);
        }

        // Hàm này dùng để TEST kết nối từ form cấu hình
        public static bool TestConnection(string serverName)
        {
            string tempConnStr = string.Format(@"Data Source={0};Initial Catalog=ElibseDB;Integrated Security=True", serverName);
            try
            {
                using (SqlConnection conn = new SqlConnection(tempConnStr))
                {
                    conn.Open();
                    return true; // Kết nối thành công
                }
            }
            catch
            {
                return false; // Thất bại
            }
        }

        // Hàm LƯU cấu hình (Gọi khi người dùng bấm Lưu ở form Config)
        public static void SaveConnectionString(string newServerName)
        {
            // Lưu vào Settings của Project
            Properties.Settings.Default.ServerName = newServerName;
            Properties.Settings.Default.Save(); // Lệnh này quan trọng để ghi xuống ổ cứng
        }
    }
}