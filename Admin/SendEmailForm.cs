using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing; // Để chỉnh màu Status
using System.Windows.Forms;

namespace Elibse.Admin
{
    public partial class SendEmailForm : Form
    {
        public SendEmailForm()
        {
            InitializeComponent();
        }

        private void SendEmailForm_Load(object sender, EventArgs e)
        {
            LoadReadersWithEmail();
            lblStatus.Text = "Sẵn sàng.";
            lblStatus.ForeColor = Color.Black;
        }

        // --- 1. TẢI DANH SÁCH ĐỘC GIẢ ---
        private void LoadReadersWithEmail()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT FullName, Email FROM READERS WHERE Email IS NOT NULL AND Email != ''";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Hiển thị: "Nguyễn Văn A (email@gmail.com)"
                    dt.Columns.Add("DisplayColumn", typeof(string), "FullName + ' (' + Email + ')'");

                    cboReaders.DataSource = dt;
                    cboReaders.DisplayMember = "DisplayColumn";
                    cboReaders.ValueMember = "Email";

                    if (dt.Rows.Count > 0) cboReaders.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Lỗi tải dữ liệu.";
                lblStatus.ForeColor = Color.Red;
            }
        }

        // --- 2. NÚT GỬI EMAIL (Kèm Chữ Ký "Inspired by Ngân") ---
        private void btnSend_Click(object sender, EventArgs e)
        {
            // Kiểm tra nhập liệu
            if (cboReaders.SelectedValue == null)
            {
                MessageBox.Show("Chưa chọn người nhận!", "Thiếu thông tin");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSubject.Text) || string.IsNullOrWhiteSpace(rtbContent.Text))
            {
                MessageBox.Show("Vui lòng nhập tiêu đề và nội dung.", "Thiếu thông tin");
                return;
            }

            // 1. Cập nhật Trạng thái: ĐANG GỬI...
            lblStatus.Text = "Đang gửi email... Vui lòng đợi.";
            lblStatus.ForeColor = Color.Blue;
            btnSend.Enabled = false; // Khóa nút để không bấm liên tục
            Cursor = Cursors.WaitCursor; // Hiện con chuột xoay vòng
            Application.DoEvents(); // Cập nhật giao diện ngay lập tức

            try
            {
                // 2. Chuẩn bị dữ liệu
                string emailNguoiNhan = cboReaders.SelectedValue.ToString();
                string tieuDe = txtSubject.Text.Trim();

                // Lấy nội dung Admin nhập (thay xuống dòng bằng thẻ <br> để HTML hiểu)
                string noiDungNhap = rtbContent.Text.Replace("\n", "<br>");

                // --- CHỮ KÝ THƯƠNG HIỆU CỦA QUÍ ---
                string signature = "<br><br><hr>" +
                                   "<div style='color: #999999; font-style: italic; font-size: 12px; font-family: Arial, sans-serif;'>" +
                                   "Email này được gửi tự động từ Hệ thống Quản lý Thư viện (Elibse).<br>" +
                                   "Vui lòng không trả lời tin nhắn này.<br>" +
                                   "<b>Developed by Quí | Inspired by Ngân</b>" + // Điểm nhấn ở đây
                                   "</div>";

                // Ghép nội dung hoàn chỉnh
                string finalBody = $"<h3>Chào bạn,</h3><p>{noiDungNhap}</p>{signature}";

                
                EmailService.SendEmail(emailNguoiNhan, tieuDe, finalBody);
            }
            catch (Exception ex)
            {
                lblStatus.Text = "Lỗi hệ thống: " + ex.Message;
                lblStatus.ForeColor = Color.Red;
            }
            finally
            {
                // Mở khóa lại nút
                btnSend.Enabled = true;
                Cursor = Cursors.Default;
            }

            lblStatus.Text = "Gửi thành công!";
        }
    }
}