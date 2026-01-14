using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using Elibse; // Để dùng DatabaseConnection

namespace Elibse
{
    public class EmailService
    {
        public static bool SendEmail(string toEmail, string subject, string body)
        {
            string fromEmail = "";
            string password = "";

            // 1. Lấy thông tin từ Database thay vì hardcode
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT TOP 1 EmailSender, EmailPassword FROM SYSTEM_CONFIG";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            fromEmail = reader["EmailSender"].ToString();
                            password = reader["EmailPassword"].ToString();
                        }
                    }
                }

                if (string.IsNullOrEmpty(fromEmail) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Chưa cấu hình Email gửi đi! Vui lòng liên hệ Admin.", "Lỗi cấu hình", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // 2. Cấu hình gửi mail (Giữ nguyên logic cũ)
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(fromEmail);
                mail.To.Add(toEmail);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential(fromEmail, password);

                smtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}