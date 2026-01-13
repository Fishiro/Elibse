using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Elibse
{
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        // Sự kiện khi nhấn nút Đăng Nhập
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string passwordCheck = SecurityHelper.HashPassword(password);

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT Username FROM ADMINS WHERE Password = @pass";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@pass", passwordCheck);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        string currentAdmin = result.ToString();
                        Session.CurrentAdminUsername = currentAdmin;
                        Logger.Log("Đăng Nhập", "Đăng nhập vào hệ thống");

                        MessageBox.Show("Xin chào " + currentAdmin + "!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Hide();
                        AdminDashboard dashboard = new AdminDashboard();
                        dashboard.ShowDialog();

                        Session.CurrentAdminUsername = null;

                        this.Show();
                        txtPassword.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Mật khẩu không đúng! Vui lòng thử lại.", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPassword.Focus();
                        txtPassword.SelectAll();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}