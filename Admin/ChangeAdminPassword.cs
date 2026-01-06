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
                // Giả sử class DatabaseConnection của bạn có hàm GetConnection() trả về SqlConnection:
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Bước A: Kiểm tra mật khẩu cũ
                    string checkSql = "SELECT COUNT(*) FROM ADMINS WHERE AdminID = @ID AND Password = @OldPass";
                    SqlCommand checkCmd = new SqlCommand(checkSql, conn);

                    // TODO: Sau này nhớ thay số 1 bằng biến Session (VD: Program.CurrentAdminID)
                    checkCmd.Parameters.AddWithValue("@ID", 1);
                    checkCmd.Parameters.AddWithValue("@OldPass", txtOldPass.Text);

                    int count = (int)checkCmd.ExecuteScalar();

                    if (count == 0)
                    {
                        MessageBox.Show("Mật khẩu cũ không đúng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Bước B: Cập nhật mật khẩu mới
                    string updateSql = "UPDATE ADMINS SET Password = @NewPass WHERE AdminID = @ID";
                    SqlCommand updateCmd = new SqlCommand(updateSql, conn);

                    updateCmd.Parameters.AddWithValue("@NewPass", txtNewPass.Text);
                    updateCmd.Parameters.AddWithValue("@ID", 1);

                    updateCmd.ExecuteNonQuery();

                    MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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