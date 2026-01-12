using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Elibse;

namespace Elibse.Admin
{
    public partial class ChangeAdminPassword : Form
    {
        public ChangeAdminPassword()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 1. Validate dữ liệu
            if (string.IsNullOrEmpty(txtOldPass.Text) || string.IsNullOrEmpty(txtNewPass.Text) || string.IsNullOrEmpty(txtConfirmPass.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtNewPass.Text != txtConfirmPass.Text)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Xử lý Database
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // 1. Lấy mật khẩu HIỆN TẠI trong Database của Admin đang đăng nhập
                    string query = "SELECT Password FROM ADMINS WHERE Username = @user";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Lấy username từ Session (người đang đăng nhập)
                    cmd.Parameters.AddWithValue("@user", Session.CurrentAdminUsername);

                    string currentDbPass = cmd.ExecuteScalar()?.ToString();

                    // 2. XỬ LÝ MÃ HÓA ====================================================
                    // Lấy mật khẩu cũ người dùng nhập vào
                    string oldPassInput = txtOldPass.Text;

                    // Băm nó ra thành MD5 để giống định dạng trong Database
                    string oldPassHash = SecurityHelper.HashPassword(oldPassInput);
                    // ====================================================================

                    // 3. So sánh: Phải so sánh 2 chuỗi ĐÃ MÃ HÓA với nhau
                    if (currentDbPass != oldPassHash)
                    {
                        MessageBox.Show("Mật khẩu cũ không đúng! Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtOldPass.Focus();
                        return;
                    }

                    // 4. Nếu đúng thì mới mã hóa mật khẩu MỚI để lưu lại
                    string newPassHash = SecurityHelper.HashPassword(txtNewPass.Text);

                    string updateQuery = "UPDATE ADMINS SET Password = @newPass WHERE Username = @user";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                    updateCmd.Parameters.AddWithValue("@newPass", newPassHash);
                    updateCmd.Parameters.AddWithValue("@user", Session.CurrentAdminUsername);

                    updateCmd.ExecuteNonQuery();

                    MessageBox.Show("Đổi mật khẩu thành công!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}