using System;
using System.Data.SqlClient;
using System.Drawing; // Cần thiết để vẽ giao diện
using System.Windows.Forms;

namespace Elibse
{
    // Kế thừa từ Form để trở thành một cửa sổ
    public partial class fmFirstSetup : Form
    {
        // Khai báo các control
        private Label lblTitle;
        private Label lblPass;
        private TextBox txtNewPass;
        private Label lblConfirm;
        private TextBox txtConfirmPass;

        public fmFirstSetup()
        {
            // Thiết lập thông số cho Form
            this.Text = "Khởi tạo hệ thống lần đầu";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Gọi hàm tự vẽ giao diện
            InitializeCustomComponents();
        }

        // Hàm này thay thế cho việc Kéo Thả (Designer)
        private void InitializeCustomComponents()
        {
            // 1. Tiêu đề
            lblTitle = new Label();
            lblTitle.Text = "Chào mừng Admin!\nVui lòng thiết lập mật khẩu quản trị:";
            lblTitle.Location = new Point(20, 20);
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Arial", 10, FontStyle.Bold);
            this.Controls.Add(lblTitle);

            // 2. Nhãn Mật khẩu mới
            lblPass = new Label();
            lblPass.Text = "Mật khẩu mới:";
            lblPass.Location = new Point(20, 70);
            lblPass.AutoSize = true;
            this.Controls.Add(lblPass);

            // 3. Ô nhập Mật khẩu mới
            txtNewPass = new TextBox();
            txtNewPass.Location = new Point(20, 95);
            txtNewPass.Width = 340;
            txtNewPass.UseSystemPasswordChar = true; // Ẩn password bằng dấu chấm
            this.Controls.Add(txtNewPass);

            // 4. Nhãn Nhập lại
            lblConfirm = new Label();
            lblConfirm.Text = "Nhập lại mật khẩu:";
            lblConfirm.Location = new Point(20, 135);
            lblConfirm.AutoSize = true;
            this.Controls.Add(lblConfirm);

            // 5. Ô nhập Nhập lại
            txtConfirmPass = new TextBox();
            txtConfirmPass.Location = new Point(20, 160);
            txtConfirmPass.Width = 340;
            txtConfirmPass.UseSystemPasswordChar = true;
            this.Controls.Add(txtConfirmPass);

            // 6. Nút Lưu
            btnSave = new Button();
            btnSave.Text = "Lưu và Bắt đầu";
            btnSave.Location = new Point(100, 200);
            btnSave.Size = new Size(180, 40);
            btnSave.BackColor = Color.Teal;
            btnSave.ForeColor = Color.White;
            btnSave.Font = new Font("Arial", 10, FontStyle.Bold);
            btnSave.Cursor = Cursors.Hand;

            // --- LIÊN KẾT SỰ KIỆN Ở ĐÂY ---
            // Dòng này nghĩa là: Khi click nút này, hãy chạy hàm BtnSave_Click
            btnSave.Click += BtnSave_Click;

            this.Controls.Add(btnSave);
        }

        // --- SỰ KIỆN XỬ LÝ LƯU ---
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

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    // Cập nhật pass cho admin (Username mặc định là 'admin')
                    string query = "UPDATE ADMINS SET Password = @p WHERE Username = 'admin'";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@p", pass);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Thiết lập thành công! Hãy đăng nhập ngay.", "Hoàn tất");
                        this.DialogResult = DialogResult.OK; // Báo OK để Program.cs biết
                        this.Close();
                    }
                    else
                    {
                        // Trường hợp chưa có dòng admin nào (dù hiếm vì SQL đã insert rồi)
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