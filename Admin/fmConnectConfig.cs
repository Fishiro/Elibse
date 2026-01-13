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
            string serverInput = txtServerName.Text.Trim();

            if (string.IsNullOrEmpty(serverInput))
            {
                serverInput = @".\SQLEXPRESS";
                MessageBox.Show($"Sử dụng server mặc định: {serverInput}", "Thông báo");
            }

            if (DatabaseConnection.TestConnection(serverInput))
            {
                DatabaseConnection.SaveConnectionString(serverInput);
                MessageBox.Show("Kết nối thành công! Cấu hình đã được lưu.");

                this.Hide();
                fmLoginDialog frm = new fmLoginDialog();
                frm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Kết nối thất bại. Vui lòng kiểm tra lại tên Server!");
            }
        }
    }
}