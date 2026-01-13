using System;
using System.Data;
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
            // Cài đặt ngày sinh mặc định (18 tuổi)
            dtpDOB.Value = DateTime.Today.AddYears(-18);
            CalculateAge();

            picAvatar.Image = null;
            txtAge.ReadOnly = true;
            txtReaderID.ReadOnly = true;

            // Gán mã tạm để giao diện không bị trống (User không cần quan tâm mã này)
            txtReaderID.Text = "(Tự động sinh)";
        }

        private void GenerateAutoId()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    // Lấy mã lớn nhất hiện tại (Sắp xếp theo độ dài rồi đến giá trị để tránh lỗi DG9 > DG10)
                    string sql = "SELECT TOP 1 ReaderID FROM READERS ORDER BY LEN(ReaderID) DESC, ReaderID DESC";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        string lastId = result.ToString(); // Ví dụ: RD00005
                        // Cắt bỏ tiền tố "RD" (2 ký tự đầu)
                        string numberPart = lastId.Substring(2);
                        int nextNumber = int.Parse(numberPart) + 1;

                        // Format lại thành chuỗi 5 số (RD00006)
                        txtReaderID.Text = "RD" + nextNumber.ToString("D5");
                    }
                    else
                    {
                        // Chưa có dữ liệu -> Mã đầu tiên
                        txtReaderID.Text = "RD00001";
                    }
                }
            }
            catch (Exception ex)
            {
                // Fallback nếu mất kết nối DB: Dùng tạm mã thời gian để không crash app
                txtReaderID.Text = "RD" + DateTime.Now.ToString("yyMMddHHmm");
            }
        }

        // 2. Hàm sinh mã trả về chuỗi ID (Thay vì gán trực tiếp vào TextBox)
        private string GenerateNextReaderID(SqlConnection conn, SqlTransaction transaction)
        {
            // Lưu ý: Phải dùng chung Connection và Transaction đang mở để đảm bảo tính nhất quán
            string sql = "SELECT TOP 1 ReaderID FROM READERS WITH (UPDLOCK) ORDER BY LEN(ReaderID) DESC, ReaderID DESC";
            // WITH (UPDLOCK): Khóa tạm thời dòng này để không ai đọc được cho đến khi transaction xong -> Tránh trùng tuyệt đối

            SqlCommand cmd = new SqlCommand(sql, conn, transaction);
            object result = cmd.ExecuteScalar();

            if (result != null)
            {
                string lastId = result.ToString(); // Ví dụ: RD00005
                string numberPart = lastId.Substring(2);
                if (int.TryParse(numberPart, out int nextNumber))
                {
                    return "RD" + (nextNumber + 1).ToString("D5");
                }
            }
            return "RD00001"; // Nếu chưa có ai hoặc lỗi format
        }

        private void CalculateAge()
        {
            DateTime birthDate = dtpDOB.Value.Date; // Chỉ lấy ngày, bỏ giờ
            DateTime today = DateTime.Today;        // Lấy ngày hiện tại, bỏ giờ

            int age = today.Year - birthDate.Year;

            // Nếu chưa đến sinh nhật trong năm nay thì trừ 1 tuổi
            if (birthDate.Date > today.AddYears(-age))
                age--;

            if (age < 0) age = 0;
            txtAge.Text = age.ToString();
        }

        private void dtpDOB_ValueChanged(object sender, EventArgs e)
        {
            CalculateAge();
        }

        private void btnUploadImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Chọn ảnh chân dung";
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Kiểm tra dung lượng < 5MB
                    long fileSize = new FileInfo(ofd.FileName).Length;
                    if (fileSize > 5 * 1024 * 1024)
                    {
                        MessageBox.Show("Ảnh quá lớn! Vui lòng chọn ảnh dưới 5MB.");
                        return;
                    }

                    // [FIX LỖI GDI+]
                    // Dùng FileStream để mở file, sau đó clone sang Bitmap mới
                    using (var stream = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                    {
                        using (var tempImage = Image.FromStream(stream))
                        {
                            // Tạo một bản sao mới của ảnh vào bộ nhớ
                            // Bản sao này không còn dính dáng gì đến file gốc hay stream cũ
                            picAvatar.Image = new Bitmap(tempImage);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải ảnh: " + ex.Message);
                }
            }
        }

        private byte[] ImageToByteArray(Image img)
        {
            if (img == null) return null;

            using (MemoryStream ms = new MemoryStream())
            {
                // Nên dùng format cố định thay vì RawFormat
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // --- Validation ---
            if (dtpDOB.Value > DateTime.Now)
            {
                MessageBox.Show("Ngày sinh không hợp lệ!");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFullName.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin (*)");
                return;
            }

            if (txtFullName.Text.Trim().Length > 100)
            {
                MessageBox.Show("Tên không được quá 100 ký tự!");
                return;
            }

            if (txtEmail.Text.Trim().Length > 100)
            {
                MessageBox.Show("Email không được quá 100 ký tự!");
                return;
            }

            if (txtPhone.Text.Trim().Length > 20)
            {
                MessageBox.Show("Số điện thoại không được quá 20 ký tự!");
                return;
            }

            if (!txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Email không hợp lệ!");
                return;
            }

            if (txtPassword.Text.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự!");
                return;
            }

            if (txtPassword.Text != txtConfirmPass.Text)
            {
                MessageBox.Show("Mật khẩu không khớp!");
                return;
            }

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction(); // Bắt đầu giao dịch

                    try
                    {
                        // Bước 1: Kiểm tra trùng Email/SĐT
                        SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM READERS WHERE Email = @email OR PhoneNumber = @phone", conn, transaction);
                        checkCmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                        checkCmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());

                        if ((int)checkCmd.ExecuteScalar() > 0)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Email hoặc SĐT đã tồn tại!");
                            return;
                        }

                        // Bước 2: SINH MÃ MỚI NHẤT NGAY TẠI ĐÂY
                        string newID = GenerateNextReaderID(conn, transaction);

                        // Cập nhật lên giao diện để user thấy mã của mình sau khi tạo xong
                        txtReaderID.Text = newID;

                        // Bước 3: Insert dữ liệu với mã vừa sinh
                        string sql = @"INSERT INTO READERS (ReaderID, FullName, DOB, PhoneNumber, Email, Address, Password, Status, CreatedDate, ReaderImage) 
                               VALUES (@id, @name, @dob, @phone, @email, @address, @pass, 'Active', GETDATE(), @img)";

                        SqlCommand cmd = new SqlCommand(sql, conn, transaction);

                        cmd.Parameters.AddWithValue("@id", newID); // Dùng biến newID
                        cmd.Parameters.AddWithValue("@name", txtFullName.Text.Trim());
                        cmd.Parameters.AddWithValue("@dob", dtpDOB.Value);
                        cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@address", string.IsNullOrWhiteSpace(txtAddress.Text) ? (object)DBNull.Value : txtAddress.Text.Trim());
                        cmd.Parameters.AddWithValue("@pass", SecurityHelper.HashPassword(txtPassword.Text));

                        if (picAvatar.Image != null)
                        {
                            SqlParameter imgParam = new SqlParameter("@img", SqlDbType.VarBinary);
                            imgParam.Value = ImageToByteArray(picAvatar.Image);
                            cmd.Parameters.Add(imgParam);
                        }
                        else
                        {
                            SqlParameter imgParam = new SqlParameter("@img", SqlDbType.VarBinary);
                            imgParam.Value = DBNull.Value;
                            cmd.Parameters.Add(imgParam);
                        }

                        cmd.ExecuteNonQuery();

                        // Bước 4: Chốt giao dịch
                        transaction.Commit();

                        MessageBox.Show("Đăng ký thành công! Mã độc giả của bạn là: " + newID);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch
                    {
                        transaction.Rollback(); // Gặp lỗi thì hoàn tác hết
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Bỏ sự kiện sinh mã ở TextChanged để tránh mã nhảy lung tung
        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            // Không làm gì cả, mã ID đã cố định từ lúc load form
        }

        private void txtFullName_TextChanged(object sender, EventArgs e) { }
        private void txtEmail_TextChanged(object sender, EventArgs e) { }
        private void txtPassword_TextChanged(object sender, EventArgs e) { }
        private void txtConfirmPass_TextChanged(object sender, EventArgs e) { }
        private void txtAddress_TextChanged(object sender, EventArgs e) { }
    }
}