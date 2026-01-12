using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Elibse
{
    public partial class fmConnectConfig : Form
    {
        public fmConnectConfig()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Cấu hình Máy chủ";
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            // Lấy tên server từ ô nhập (Thay txtServerName bằng tên thực tế của ô nhập trong form bạn)
            string serverInput = txtServerName.Text.Trim();

            if (string.IsNullOrEmpty(serverInput))
            {
                serverInput = @".\SQLEXPRESS";
            }

            // Kiểm tra kết nối thử
            if (DatabaseConnection.TestConnection(serverInput))
            {
                // Lưu tên server này lại vĩnh viễn
                DatabaseConnection.SaveConnectionString(serverInput);

                MessageBox.Show("Kết nối thành công! Cấu hình đã được lưu.");

                // Chuyển sang form Đăng nhập
                this.Hide();
                fmLoginDialog frm = new fmLoginDialog();
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Kết nối thất bại. Vui lòng kiểm tra lại tên Server!");
            }
        }
    }
}