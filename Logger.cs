using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Elibse
{
    public static class Logger
    {
        // Hàm ghi lại hành động vào Database
        public static void Log(string actionType, string details)
        {
            // Nếu chưa đăng nhập thì ghi là "Unknown" hoặc bỏ qua
            string user = Session.CurrentAdminUsername;
            if (string.IsNullOrEmpty(user)) user = "System/Unknown";

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO ADMIN_LOGS (AdminUsername, ActionType, ActionDetails, LogTime) 
                                     VALUES (@user, @action, @detail, GETDATE())";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@user", user);
                    cmd.Parameters.AddWithValue("@action", actionType);
                    cmd.Parameters.AddWithValue("@detail", details);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Ghi log thất bại thì không nên làm crash chương trình chính
                // Chỉ hiện lỗi trong cửa sổ Output của Visual Studio để debug
                System.Diagnostics.Debug.WriteLine("Lỗi ghi log: " + ex.Message);
            }
        }
    }
}