using System.Data.SqlClient;

namespace Elibse
{
    public class DatabaseConnection
    {
        // Chuỗi mặc định (để fallback nếu người dùng không nhập gì)
        private static string _connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=ElibseDB;Integrated Security=True";

        // Hàm cho phép thay đổi chuỗi kết nối từ bên ngoài
        public static void SetConnectionString(string serverName)
        {
            // Nếu người dùng nhập vào, ta sẽ lắp ghép chuỗi mới
            // Data Source= {Tên Server} ; ...
            if (!string.IsNullOrEmpty(serverName))
            {
                _connectionString = string.Format(@"Data Source={0};Initial Catalog=ElibseDB;Integrated Security=True", serverName);
            }
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}