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
                try
                {
                    // 1. Kiểm tra dung lượng (Ví dụ giới hạn 5MB)
                    long fileSize = new FileInfo(ofd.FileName).Length;
                    if (fileSize > 5 * 1024 * 1024)
                    {
                        MessageBox.Show("Ảnh quá lớn! Vui lòng chọn ảnh dưới 5MB.");
                        return;
                    }

                    // 2. Thử load ảnh an toàn
                    using (var stream = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                    {
                        picAvatar.Image = Image.FromStream(stream);
                    }
                }
                catch (OutOfMemoryException)
                {
                    MessageBox.Show("File ảnh bị lỗi hoặc định dạng không hỗ trợ!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không thể tải ảnh: " + ex.Message);
                }
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
            if (dtpDOB.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày sinh không được lớn hơn ngày hiện tại!", "Vô lý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DateTime.Now.Year - dtpDOB.Value.Year > 100)
            {
                MessageBox.Show("Độc giả không thể lớn hơn 100 tuổi (theo quy định thư viện)!", "Cảnh báo");
                return;
            }

            // 1. Validation cơ bản (Kiểm tra rỗng)
            if (string.IsNullOrWhiteSpace(txtFullName.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin (*)");
                return;
            }

            // Kiểm tra độ dài Địa chỉ (Max 500 ký tự)
            if (txtAddress.Text.Trim().Length > 500)
            {
                MessageBox.Show("Địa chỉ quá dài (tối đa 500 ký tự). Vui lòng rút gọn lại!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAddress.Focus(); // Đưa con trỏ về ô địa chỉ để sửa
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

                    // INSERT
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