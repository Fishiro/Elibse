using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing; // Để xử lý ảnh
using System.IO;      // Để lưu file ảnh
using System.Windows.Forms;

namespace Elibse.Admin
{
    public partial class TotalReader : Form
    {
        public TotalReader()
        {
            InitializeComponent();
        }

        // --- 1. KHI FORM VỪA MỞ LÊN ---
        private void TotalReader_Load(object sender, EventArgs e)
        {
            LoadReaderData("");
            SetupDataGridView();
            ClearInput();
        }

        // --- 2. CÁC HÀM HỖ TRỢ (Tải dữ liệu, Xóa trắng...) ---

        private void LoadReaderData(string keyword)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    // Lấy đầy đủ thông tin để hiển thị
                    string query = @"SELECT ReaderID, FullName, DOB, PhoneNumber, Email, ReaderImage, CreatedDate, Status 
                                     FROM READERS 
                                     WHERE FullName LIKE @kw OR ReaderID LIKE @kw
                                     ORDER BY CreatedDate DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvReaders.DataSource = dt;
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message); }
        }

        private void SetupDataGridView()
        {
            dgvReaders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // Ẩn cột ảnh và ngày tạo cho bảng đỡ rối
            if (dgvReaders.Columns["ReaderImage"] != null) dgvReaders.Columns["ReaderImage"].Visible = false;
            if (dgvReaders.Columns["CreatedDate"] != null) dgvReaders.Columns["CreatedDate"].Visible = false;

            // Đặt tên cột tiếng Việt
            if (dgvReaders.Columns["ReaderID"] != null) dgvReaders.Columns["ReaderID"].HeaderText = "Mã ĐG";
            if (dgvReaders.Columns["FullName"] != null) dgvReaders.Columns["FullName"].HeaderText = "Họ Tên";
            if (dgvReaders.Columns["Status"] != null) dgvReaders.Columns["Status"].HeaderText = "Trạng thái";
        }

        private void ClearInput()
        {
            txtReaderID.Clear();
            txtFullName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            dtpDOB.Value = DateTime.Now;
            picReader.Image = null;
        }

        // Hàm biến đổi ảnh thành mảng byte để lưu vào SQL
        private byte[] ImageToByteArray(Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Lưu định dạng PNG cho nét
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        // --- 3. SỰ KIỆN CLICK VÀO BẢNG (Đổ dữ liệu lên ô nhập) ---
        // Quan trọng: Phải liên kết sự kiện CellClick
        private void dgvReaders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvReaders.Rows[e.RowIndex];

                // Đổ dữ liệu chữ
                txtReaderID.Text = row.Cells["ReaderID"].Value.ToString();
                txtFullName.Text = row.Cells["FullName"].Value.ToString();
                txtPhone.Text = row.Cells["PhoneNumber"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();

                if (row.Cells["DOB"].Value != DBNull.Value)
                    dtpDOB.Value = Convert.ToDateTime(row.Cells["DOB"].Value);

                // Đổ dữ liệu ảnh
                if (row.Cells["ReaderImage"].Value != DBNull.Value)
                {
                    byte[] imgData = (byte[])row.Cells["ReaderImage"].Value;
                    // Chuyển từ byte[] ngược lại thành Image để hiện lên

                    MemoryStream ms = new MemoryStream(imgData); // Tạo dòng chảy
                    picReader.Image = new Bitmap(Image.FromStream(ms));      // Gán vào ảnh
                    picReader.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    picReader.Image = null;
                }
            }
        }

        // --- 4. CÁC NÚT CHỨC NĂNG CHÍNH ---

        // Nút THÊM: Mở form Đăng ký bên ngoài
        private void btnAddReader_Click(object sender, EventArgs e)
        {
            // Mở form đăng ký của phân hệ Reader
            // Lưu ý: Đảm bảo bạn có namespace Elibse (hoặc Elibse.Reader) chứa form ReaderRegister
            Elibse.ReaderRegister frm = new Elibse.ReaderRegister();
            frm.ShowDialog();

            // Sau khi tắt form kia đi thì tải lại bảng để thấy người mới
            LoadReaderData("");
            ClearInput();
        }

        // Nút ĐỔI ẢNH: Chọn ảnh từ máy tính
        private void btnChangeImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image Files|*.jpg;*.jpeg;*.png";
            dlg.Title = "Chọn ảnh chân dung";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                picReader.ImageLocation = dlg.FileName;
                picReader.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        // Nút SỬA (Lưu cập nhật)
        private void btnEditReader_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReaderID.Text))
            {
                MessageBox.Show("Vui lòng chọn độc giả cần sửa!");
                return;
            }

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    // Cập nhật thông tin + Ảnh
                    string sql = @"UPDATE READERS 
                                   SET FullName = @name, DOB = @dob, PhoneNumber = @phone, Email = @email, ReaderImage = @img 
                                   WHERE ReaderID = @id";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", txtReaderID.Text);
                    cmd.Parameters.AddWithValue("@name", txtFullName.Text);
                    cmd.Parameters.AddWithValue("@dob", dtpDOB.Value);
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);

                    // Xử lý ảnh: Nếu có ảnh thì lưu, không thì lưu NULL
                    if (picReader.Image != null)
                        cmd.Parameters.AddWithValue("@img", ImageToByteArray(picReader.Image));
                    else
                        cmd.Parameters.Add(new SqlParameter("@img", SqlDbType.VarBinary) { Value = DBNull.Value });

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Cập nhật thành công!");
                    LoadReaderData(txtSearchReader.Text); // Tải lại nhưng giữ từ khóa tìm kiếm
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        // Nút XÓA
        private void btnDeleteReader_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReaderID.Text)) return;

            if (MessageBox.Show($"Xóa độc giả {txtFullName.Text}?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = DatabaseConnection.GetConnection())
                    {
                        conn.Open();
                        // Kiểm tra nợ sách
                        string check = "SELECT COUNT(*) FROM LOAN_RECORDS WHERE ReaderID = @id AND ReturnDate IS NULL";
                        SqlCommand cmdCheck = new SqlCommand(check, conn);
                        cmdCheck.Parameters.AddWithValue("@id", txtReaderID.Text);

                        if ((int)cmdCheck.ExecuteScalar() > 0)
                        {
                            MessageBox.Show("Không thể xóa vì độc giả này đang nợ sách!");
                            return;
                        }

                        // Xóa
                        string del = "DELETE FROM READERS WHERE ReaderID = @id";
                        SqlCommand cmdDel = new SqlCommand(del, conn);
                        cmdDel.Parameters.AddWithValue("@id", txtReaderID.Text);
                        cmdDel.ExecuteNonQuery();

                        // Ghi log
                        Logger.Log("Xóa Độc Giả", $"Đã xóa: {txtFullName.Text} ({txtReaderID.Text})");

                        MessageBox.Show("Đã xóa!");
                        LoadReaderData("");
                        ClearInput();
                    }
                }
                catch (SqlException sqlEx)
                {
                    // Số 547 là mã lỗi của SQL Server khi vi phạm khóa ngoại (Foreign Key)
                    if (sqlEx.Number == 547)
                    {
                        MessageBox.Show("Không thể xóa độc giả này vì họ đang có lịch sử mượn trả sách!\nHãy xóa dữ liệu mượn trả trước.", "Ràng buộc dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        MessageBox.Show("Lỗi SQL: " + sqlEx.Message);
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi ràng buộc dữ liệu: " + ex.Message); }
            }
        }

        // Nút RESET PASSWORD
        private void btnResetPass_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReaderID.Text)) return;

            if (MessageBox.Show($"Reset mật khẩu cho {txtFullName.Text} về '123456'?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = DatabaseConnection.GetConnection())
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE READERS SET Password = '123456' WHERE ReaderID = @id", conn);
                        cmd.Parameters.AddWithValue("@id", txtReaderID.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Mật khẩu đã được đặt lại thành 123456");
                    }
                }
                catch { }
            }
        }

        // Các nút phụ: Tìm kiếm, Tải lại
        private void btnSearch_Click(object sender, EventArgs e) => LoadReaderData(txtSearchReader.Text.Trim());
        private void btnReload_Click(object sender, EventArgs e) { ClearInput(); LoadReaderData(""); }
    }
}