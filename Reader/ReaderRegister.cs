using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Elibse
{
    public partial class ReaderRegister : Form
    {
        public ReaderRegister()
        {
            InitializeComponent();
        }

        private void ReaderRegister_Load(object sender, EventArgs e)
        {
            GenerateReaderID();
            dtpDOB.Value = DateTime.Now.AddYears(-18);
            CalculateAge();
            // Mặc định ảnh trống
            picAvatar.Image = null;
        }

        private void GenerateReaderID()
        {
            // 1. Lấy thời gian chi tiết đến từng GIÂY
            // Định dạng: yyMMddHHmmss (Ví dụ: 250107103059)
            // Tổng cộng 12 ký tự số -> Đảm bảo tính duy nhất cực cao
            string timeCode = DateTime.Now.ToString("yyMMddHHmmss");

            string phoneCode = "000";
            // Lấy 3 số cuối SĐT (nếu có)
            if (txtPhone.Text.Length >= 3)
            {
                phoneCode = txtPhone.Text.Substring(txtPhone.Text.Length - 3);
            }

            // Mã cuối cùng: RD + 3 số ĐT + 12 số thời gian = 17 ký tự
            // Vẫn nằm trong giới hạn VARCHAR(20) của Database
            txtReaderID.Text = "RD" + phoneCode + timeCode;
        }

        private void dtpDOB_ValueChanged(object sender, EventArgs e)
        {
            CalculateAge();
        }

        private void CalculateAge()
        {
            DateTime birthDate = dtpDOB.Value;
            DateTime now = DateTime.Now;
            int age = now.Year - birthDate.Year;
            if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day)) age--;
            if (age < 0) age = 0;
            txtAge.Text = age.ToString();
        }

        // =============================================================
        // [MỚI 1] SỰ KIỆN NÚT CHỌN ẢNH (btnUploadImg)
        // =============================================================
        private void btnUploadImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Chọn ảnh chân dung";
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"; // Chỉ cho chọn ảnh

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // Hiển thị ảnh lên PictureBox để xem trước
                picAvatar.Image = Image.FromFile(ofd.FileName);
            }
        }

        // =============================================================
        // [MỚI 2] HÀM PHỤ TRỢ: CHUYỂN ẢNH SANG NHỊ PHÂN (BYTE[])
        // =============================================================
        private byte[] ImageToByteArray(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Lưu ảnh vào dòng nhớ tạm với định dạng gốc
                img.Save(ms, img.RawFormat);
                return ms.ToArray();
            }
        }

        // =============================================================
        // [CẬP NHẬT] NÚT ĐĂNG KÝ (Đã thêm xử lý lưu ảnh)
        // =============================================================
        private void btnRegister_Click(object sender, EventArgs e)
        {
            // 1. Validation cơ bản
            if (string.IsNullOrWhiteSpace(txtFullName.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin (*)");
                return;
            }
            if (txtPassword.Text != txtConfirmPass.Text)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp!");
                return;
            }

            // 2. Lưu vào CSDL
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Check trùng
                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM READERS WHERE Email = @email OR PhoneNumber = @phone", conn);
                    checkCmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                    checkCmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
                    if ((int)checkCmd.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("Email hoặc SĐT đã tồn tại!");
                        return;
                    }

                    // INSERT (Đã thêm cột ReaderImage và tham số @img)
                    string sql = @"INSERT INTO READERS (ReaderID, FullName, DOB, PhoneNumber, Email, Address, Password, Status, CreatedDate, ReaderImage) 
                                   VALUES (@id, @name, @dob, @phone, @email, @address, @pass, 'Active', GETDATE(), @img)";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", txtReaderID.Text);
                    cmd.Parameters.AddWithValue("@name", txtFullName.Text.Trim());
                    cmd.Parameters.AddWithValue("@dob", dtpDOB.Value);
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                    if (string.IsNullOrWhiteSpace(txtAddress.Text))
                    {
                        // Nếu không nhập gì thì lưu giá trị NULL vào SQL
                        cmd.Parameters.AddWithValue("@address", DBNull.Value);
                    }
                    else
                    {
                        // Nếu có nhập thì lưu bình thường
                        cmd.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
                    }

                    string hashedPassword = SecurityHelper.HashPassword(txtPassword.Text);
                    cmd.Parameters.AddWithValue("@pass", hashedPassword);

                    // --- XỬ LÝ ẢNH ---
                    if (picAvatar.Image != null)
                    {
                        // Nếu có ảnh -> Chuyển sang byte[] để lưu
                        cmd.Parameters.AddWithValue("@img", ImageToByteArray(picAvatar.Image));
                    }
                    else
                    {
                        // Nếu không chọn ảnh -> Lưu NULL (hoặc DBNull.Value)
                        cmd.Parameters.AddWithValue("@img", DBNull.Value);
                    }
                    // -----------------

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đăng ký thành công!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Sự kiện cho nút "Đăng nhập ngay" (nếu đã có tài khoản)
        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Close(); // Đóng form Đăng ký hiện tại để quay lại Form Đăng nhập đang chờ phía sau
        }

        private void txtFullName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            GenerateReaderID();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtConfirmPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }
    }
}