using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class EmailService
{
    // Thông tin cấu hình mail của bạn
    private static string fromEmail = ""; 
    private static string password = ""; // 16 ký tự App Password

    public static void SendEmail(string toEmail, string subject, string body)
    {
        try
        {
            // 1. Tạo đối tượng gửi thư
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromEmail);
            mail.To.Add(toEmail);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true; // Cho phép viết nội dung bằng HTML (đậm, nghiêng, màu sắc...)

            // 2. Cấu hình Server gửi thư (SMTP của Google)
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587; // Port chuẩn bảo mật của Gmail
            smtp.EnableSsl = true; // Bắt buộc bật bảo mật SSL

            // Xác thực bằng App Password
            smtp.Credentials = new NetworkCredential(fromEmail, password);

            // 3. Gửi
            smtp.Send(mail);

            MessageBox.Show("Đã gửi email thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            // Nếu lỗi (ví dụ mất mạng, sai pass) sẽ hiện thông báo
            MessageBox.Show("Gửi email thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
