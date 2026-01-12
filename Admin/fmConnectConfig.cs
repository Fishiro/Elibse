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
            string server = txtServerName.Text.Trim();

            // Cập nhật chuỗi kết nối (Nếu server rỗng, hàm bên kia tự dùng mặc định)
            DatabaseConnection.SetConnectionString(server);

            // Thử kết nối xem có được không
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open(); // Nếu sai tên server, dòng này sẽ văng lỗi
                }

                // Nếu chạy xuống đây nghĩa là kết nối OK
                MessageBox.Show("Kết nối thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK; // Báo cho Program biết là OK
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối đến Server này!\nLỗi: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}