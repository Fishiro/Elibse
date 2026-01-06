using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Elibse.Admin
{
    // Tên class ở đây PHẢI LÀ NotificationServiceEmail để khớp với file bạn đã tạo
    public partial class NotificationServiceEmail : Form
    {
        public NotificationServiceEmail()
        {
            InitializeComponent();
        }

        // 1. Khi Form hiện lên -> Tự lấy email cũ điền vào
        private void NotificationServiceEmail_Load(object sender, EventArgs e)
        {
            // 1. Đảm bảo các ô nhập luôn mở (đề phòng lỗi do giao diện khóa)
            txtEmail.Enabled = true;
            txtPassword.Enabled = true;
            txtEmail.ReadOnly = false;
            txtPassword.ReadOnly = false;

            try
            {
                // --- CHECKPOINT 1: Bắt đầu ---
                MessageBox.Show("Bước 1: Đang thử kết nối tới Server...", "Kiểm tra");

                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // --- CHECKPOINT 2: Nếu hiện thông báo này -> Kết nối OK ---
                    MessageBox.Show("Bước 2: Kết nối THÀNH CÔNG! Đang bắt đầu lấy dữ liệu...", "Kiểm tra");

                    string query = "SELECT TOP 1 EmailSender, EmailPassword FROM SYSTEM_CONFIG";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // --- CHECKPOINT 3: Nếu hiện thông báo này -> Có dữ liệu ---
                            MessageBox.Show("Bước 3: Đã TÌM THẤY dữ liệu cũ. Đang hiển thị lên màn hình.", "Kiểm tra");

                            txtEmail.Text = reader["EmailSender"].ToString();
                            txtPassword.Text = reader["EmailPassword"].ToString();
                        }
                        else
                        {
                            // Trường hợp này nghĩa là kết nối được, nhưng bảng SYSTEM_CONFIG chưa có dòng nào
                            MessageBox.Show("Bước 3: Kết nối được nhưng BẢNG TRỐNG (Chưa insert dòng ID=1).", "Cảnh báo");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // --- CHECKPOINT LỖI: Nếu hiện cái này -> Lỗi kết nối hoặc SQL sai ---
                MessageBox.Show("LỖI NGHIÊM TRỌNG: " + ex.Message, "Thất bại");
            }
        }

        // 2. Khi bấm nút Lưu -> Lưu email mới vào Database
        private void btnSave_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string pass = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo");
                return;
            }

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // 1. Kiểm tra xem trong bảng đã có dòng nào chưa
                    string checkQuery = "SELECT COUNT(*) FROM SYSTEM_CONFIG";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    int count = (int)checkCmd.ExecuteScalar();

                    string query = "";
                    if (count > 0)
                    {
                        // TRƯỜNG HỢP 1: Đã có dữ liệu -> Chỉ cần UPDATE (Sửa)
                        // Lưu ý: Ta update dòng đầu tiên tìm thấy (không quan tâm ID là mấy để tránh lỗi)
                        query = "UPDATE SYSTEM_CONFIG SET EmailSender = @mail, EmailPassword = @pass WHERE ConfigID = (SELECT TOP 1 ConfigID FROM SYSTEM_CONFIG)";
                    }
                    else
                    {
                        // TRƯỜNG HỢP 2: Chưa có dữ liệu -> Phải INSERT (Thêm mới)
                        query = "INSERT INTO SYSTEM_CONFIG (EmailSender, EmailPassword) VALUES (@mail, @pass)";
                    }

                    // Thực thi lệnh (dù là Update hay Insert)
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@mail", email);
                    cmd.Parameters.AddWithValue("@pass", pass);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Lưu cấu hình thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Không thể lưu dữ liệu. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 3. Nút Thoát
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}