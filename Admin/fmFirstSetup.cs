using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Elibse
{
    public partial class fmFirstSetup : Form
    {
        public fmFirstSetup()
        {
            InitializeComponent();

            // Nếu muốn thay đổi giao diện, làm ở đây:
            this.Text = "Elibse: Khởi Tạo Mật Khẩu Hệ Thống Lần Đầu";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string pass = txtNewPass.Text.Trim();
            string confirm = txtConfirmPass.Text.Trim();

            if (string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Mật khẩu không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (pass != confirm)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string passHash = SecurityHelper.HashPassword(pass);

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE ADMINS SET Password = @p WHERE Username = 'admin'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@p", passHash);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Thiết lập thành công! Hãy đăng nhập ngay.", "Hoàn tất");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi: Không tìm thấy tài khoản 'admin' trong CSDL để cập nhật.", "Lỗi dữ liệu");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }
    }
}