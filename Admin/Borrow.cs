using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient; // Kết nối SQL
using System.Drawing;
using System.IO; // Xử lý ảnh
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Elibse
{
    public partial class Borrow : Form
    {
        public Borrow()
        {
            InitializeComponent();
            // Đăng ký sự kiện Load bằng code (cho chắc chắn)
            this.Load += Borrow_Load;
        }

        // --- SỰ KIỆN FORM LOAD ---
        private void Borrow_Load(object sender, EventArgs e)
        {
            // Tự động tải danh sách sách khi mở form
            LoadAvailableBooksIntoComboBox();
        }

        // --- 1. HÀM TÌM ĐỘC GIẢ (Giữ nguyên) ---
        private void LoadReaderInfo()
        {
            string readerId = txtReaderID.Text.Trim();
            if (string.IsNullOrEmpty(readerId)) return;

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // A. Lấy thông tin độc giả
                    string sqlReader = "SELECT FullName, CreatedDate, ReaderImage, Status FROM READERS WHERE ReaderID = @rid";
                    SqlCommand cmd = new SqlCommand(sqlReader, conn);
                    cmd.Parameters.AddWithValue("@rid", readerId);

                    SqlDataReader r = cmd.ExecuteReader();
                    if (r.Read())
                    {
                        string status = r["Status"].ToString();
                        if (status != "Active")
                        {
                            MessageBox.Show($"Tài khoản này đang bị khóa hoặc không hoạt động! (Trạng thái: {status})", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            r.Close();
                            ResetReaderPanel();
                            return;
                        }

                        txtReaderName.Text = r["FullName"].ToString();
                        txtCreatedDate.Text = Convert.ToDateTime(r["CreatedDate"]).ToString("dd/MM/yyyy");
                        txtViolationStatus.Text = "Không";

                        if (r["ReaderImage"] != DBNull.Value)
                        {
                            byte[] img = (byte[])r["ReaderImage"];
                            MemoryStream ms = new MemoryStream(img);
                            picReaderAvatar.Image = new Bitmap(Image.FromStream(ms));
                            picReaderAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                        else picReaderAvatar.Image = null;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy độc giả có mã này!");
                        ResetReaderPanel();
                        return;
                    }
                    r.Close();

                    // B. Đếm số sách đang mượn
                    string sqlCount = "SELECT COUNT(*) FROM LOAN_RECORDS WHERE ReaderID = @rid AND ReturnDate IS NULL";
                    SqlCommand cmdCount = new SqlCommand(sqlCount, conn);
                    cmdCount.Parameters.AddWithValue("@rid", readerId);

                    int borrowingCount = (int)cmdCount.ExecuteScalar();
                    txtBorrowCount.Text = borrowingCount.ToString() + "/6";

                    if (borrowingCount >= 6)
                    {
                        MessageBox.Show("Độc giả này đã mượn tối đa (6 cuốn). Vui lòng trả sách cũ trước khi mượn thêm!", "Đạt giới hạn", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        cboBookSelect.Enabled = false; // Khóa chọn sách
                        btnConfirmBorrow.Enabled = false;
                    }
                    else
                    {
                        cboBookSelect.Enabled = true;
                        btnConfirmBorrow.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void ResetReaderPanel()
        {
            txtReaderName.Clear();
            txtCreatedDate.Clear();
            txtBorrowCount.Text = "?/6";
            picReaderAvatar.Image = null;
            cboBookSelect.Enabled = false;
        }

        // --- 2. HÀM LOAD SÁCH VÀO COMBOBOX (Mới) ---
        private void LoadAvailableBooksIntoComboBox()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    // Chỉ lấy sách Available
                    string query = "SELECT BookID, Title, Author FROM BOOKS WHERE Status = 'Available'";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cboBookSelect.DataSource = dt;
                    cboBookSelect.DisplayMember = "Title"; // Hiển thị Tên
                    cboBookSelect.ValueMember = "BookID";  // Giá trị ngầm là ID

                    cboBookSelect.SelectedIndex = -1; // Mặc định chưa chọn
                    txtAuthor.Text = ""; // Xóa tác giả cũ
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải sách: " + ex.Message);
            }
        }

        private void cboBookSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboBookSelect.SelectedIndex == -1 || cboBookSelect.SelectedItem == null)
            {
                txtAuthor.Text = "";
                return;
            }

            try
            {
                DataRowView row = cboBookSelect.SelectedItem as DataRowView;
                if (row != null)
                {
                    txtAuthor.Text = row["Author"].ToString();
                }
            }
            catch { }
        }

        // --- 3. SỰ KIỆN NHẬP MÃ ĐỘC GIẢ ---
        private void txtReaderID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadReaderInfo();
                e.SuppressKeyPress = true;
            }
        }

        // (Đã xóa các sự kiện KeyDown của txtBookID cũ vì không cần nữa)

        // --- 4. NÚT XÁC NHẬN MƯỢN (Đã sửa để dùng ComboBox) ---
        private void btnConfirmBorrow_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra Độc giả
            if (string.IsNullOrEmpty(txtReaderName.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã độc giả và nhấn Enter để xác thực!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReaderID.Focus();
                return;
            }

            // 2. Kiểm tra Sách (Từ ComboBox)
            if (cboBookSelect.SelectedIndex == -1 || cboBookSelect.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn một cuốn sách từ danh sách!", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboBookSelect.Focus();
                return;
            }

            // Lấy dữ liệu từ ComboBox
            string bookId = cboBookSelect.SelectedValue.ToString();
            string bookTitle = cboBookSelect.Text;
            string readerName = txtReaderName.Text;

            // Hộp thoại xác nhận
            if (MessageBox.Show($"Xác nhận cho độc giả {readerName}\nmượn cuốn sách: \"{bookTitle}\"?",
                "Xác nhận mượn", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = DatabaseConnection.GetConnection())
                    {
                        conn.Open();
                        SqlTransaction transaction = conn.BeginTransaction();

                        try
                        {
                            // A. Thêm vào LOAN_RECORDS
                            string sqlInsert = @"INSERT INTO LOAN_RECORDS (ReaderID, BookID, LoanDate, DueDate, IsPaid)
                                                 VALUES (@rid, @bid, GETDATE(), GETDATE() + 7, 0)";

                            SqlCommand cmdInsert = new SqlCommand(sqlInsert, conn);
                            cmdInsert.Transaction = transaction;
                            cmdInsert.Parameters.AddWithValue("@rid", txtReaderID.Text);
                            cmdInsert.Parameters.AddWithValue("@bid", bookId); // Dùng ID từ ComboBox
                            cmdInsert.ExecuteNonQuery();

                            // B. Cập nhật trạng thái BOOKS
                            string sqlUpdate = "UPDATE BOOKS SET Status = 'Borrowed' WHERE BookID = @bid";
                            SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, conn);
                            cmdUpdate.Transaction = transaction;
                            cmdUpdate.Parameters.AddWithValue("@bid", bookId);
                            cmdUpdate.ExecuteNonQuery();

                            transaction.Commit();

                            // Ghi Log
                            Logger.Log("Mượn Sách", $"Độc giả {txtReaderID.Text} mượn sách {bookTitle} ({bookId})");

                            MessageBox.Show("Mượn sách thành công! Hạn trả: " + DateTime.Now.AddDays(7).ToString("dd/MM/yyyy"), "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // --- CẬP NHẬT LẠI GIAO DIỆN ---
                            // 1. Load lại danh sách sách (để cuốn vừa mượn biến mất)
                            LoadAvailableBooksIntoComboBox();

                            // 2. Load lại thông tin độc giả (để cập nhật số lượng mượn ?/6)
                            LoadReaderInfo();

                            // 3. Reset ô chọn sách
                            cboBookSelect.SelectedIndex = -1;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Giao dịch thất bại! Lỗi: " + ex.Message, "Lỗi Transaction", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hệ thống: " + ex.Message);
                }
            }
        }

        private void btnViewBooks_Click(object sender, EventArgs e)
        {
            Elibse.Admin.TotalBook frm = new Elibse.Admin.TotalBook();
            frm.ShowDialog();
            // Sau khi đóng form xem sách, load lại ComboBox phòng trường hợp có thay đổi
            LoadAvailableBooksIntoComboBox();
        }

        private void btnViewReaders_Click(object sender, EventArgs e)
        {
            Elibse.Admin.TotalReader frm = new Elibse.Admin.TotalReader();
            frm.ShowDialog();
        }
    }
}