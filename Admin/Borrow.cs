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
        }

        // --- 1. HÀM TÌM ĐỘC GIẢ ---
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
                    // Kiểm tra xem độc giả có tồn tại và đang hoạt động (Active) không
                    string sqlReader = "SELECT FullName, CreatedDate, ReaderImage, Status FROM READERS WHERE ReaderID = @rid";
                    SqlCommand cmd = new SqlCommand(sqlReader, conn);
                    cmd.Parameters.AddWithValue("@rid", readerId);

                    SqlDataReader r = cmd.ExecuteReader();
                    if (r.Read())
                    {
                        // Kiểm tra trạng thái tài khoản
                        string status = r["Status"].ToString();
                        if (status != "Active")
                        {
                            MessageBox.Show($"Tài khoản này đang bị khóa hoặc không hoạt động! (Trạng thái: {status})", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            r.Close();
                            ResetReaderPanel();
                            return;
                        }

                        // Hiển thị thông tin
                        txtReaderName.Text = r["FullName"].ToString();
                        txtCreatedDate.Text = Convert.ToDateTime(r["CreatedDate"]).ToString("dd/MM/yyyy");
                        txtViolationStatus.Text = "Không"; // Mặc định, sẽ update nếu có module phạt

                        // Hiển thị ảnh (nếu có)
                        if (r["ReaderImage"] != DBNull.Value)
                        {
                            byte[] img = (byte[])r["ReaderImage"];
                            MemoryStream ms = new MemoryStream(img);
                            picReaderAvatar.Image = Image.FromStream(ms);
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

                    // B. Đếm số sách đang mượn (Chưa trả)
                    string sqlCount = "SELECT COUNT(*) FROM LOAN_RECORDS WHERE ReaderID = @rid AND ReturnDate IS NULL";
                    SqlCommand cmdCount = new SqlCommand(sqlCount, conn);
                    cmdCount.Parameters.AddWithValue("@rid", readerId);

                    int borrowingCount = (int)cmdCount.ExecuteScalar();
                    txtBorrowCount.Text = borrowingCount.ToString() + "/6";

                    // Cảnh báo nếu đã mượn quá giới hạn
                    if (borrowingCount >= 6)
                    {
                        MessageBox.Show("Độc giả này đã mượn tối đa (6 cuốn). Vui lòng trả sách cũ trước khi mượn thêm!", "Đạt giới hạn", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtBookID.Enabled = false; // Khóa ô nhập sách
                        btnConfirmBorrow.Enabled = false; // Khóa nút mượn
                    }
                    else
                    {
                        txtBookID.Enabled = true;
                        btnConfirmBorrow.Enabled = true;
                        txtBookID.Focus(); // Nhảy sang ô nhập sách luôn cho tiện
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
            txtBookID.Enabled = false;
        }

        // --- 2. HÀM TÌM SÁCH ---
        private void LoadBookInfo()
        {
            string bookId = txtBookID.Text.Trim();
            if (string.IsNullOrEmpty(bookId)) return;

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT Title, Author, Status, BookImage FROM BOOKS WHERE BookID = @bid";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@bid", bookId);

                    SqlDataReader r = cmd.ExecuteReader();
                    if (r.Read())
                    {
                        string status = r["Status"].ToString();

                        // Kiểm tra chặn sách đã thanh lý
                        if (status == "Liquidated")
                        {
                            MessageBox.Show("Sách này đã được thanh lý khỏi thư viện! Không thể thực hiện mượn.",
                                            "Ngừng giao dịch", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            ResetBookPanel(); // Xóa sạch các ô nhập liệu để tránh nhìn thấy dữ liệu rác
                            return; // Dừng hàm ngay lập tức
                        }

                        if (status != "Available")
                        {
                            MessageBox.Show($"Sách này hiện không khả dụng! (Trạng thái: {status})", "Không thể mượn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            ResetBookPanel();
                            return;
                        }

                        txtBookTitle.Text = r["Title"].ToString();
                        txtAuthor.Text = r["Author"].ToString();

                        if (r["BookImage"] != DBNull.Value)
                        {
                            byte[] img = (byte[])r["BookImage"];
                            MemoryStream ms = new MemoryStream(img);
                            picBookCover.Image = Image.FromStream(ms);
                            picBookCover.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                        else picBookCover.Image = null;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sách có mã này!");
                        ResetBookPanel();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm sách: " + ex.Message);
            }
        }

        private void ResetBookPanel()
        {
            txtBookTitle.Clear();
            txtAuthor.Clear();
            picBookCover.Image = null;
        }

        // --- 3. SỰ KIỆN NHẬP MÃ (Dùng KeyDown thay vì DoubleClick) ---

        // Khi nhấn Enter ở ô Độc giả
        private void txtReaderID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadReaderInfo();
                e.SuppressKeyPress = true; // Tắt tiếng bíp
            }
        }

        // Khi nhấn Enter ở ô Mã sách
        private void txtBookID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadBookInfo();
                e.SuppressKeyPress = true;
            }
        }

        // --- 4. NÚT XÁC NHẬN MƯỢN ---
        private void btnConfirmBorrow_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(txtReaderName.Text) || string.IsNullOrEmpty(txtBookTitle.Text))
            {
                MessageBox.Show("Vui lòng xác định Độc giả và Sách trước khi ký mượn!");
                return;
            }

            if (MessageBox.Show($"Xác nhận cho {txtReaderName.Text} mượn sách '{txtBookTitle.Text}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = DatabaseConnection.GetConnection())
                    {
                        conn.Open();

                        // 1. BẮT ĐẦU GIAO DỊCH
                        SqlTransaction transaction = conn.BeginTransaction();

                        try
                        {
                            // A. Thêm vào bảng LOAN_RECORDS
                            string sqlInsert = @"INSERT INTO LOAN_RECORDS (ReaderID, BookID, LoanDate, DueDate, IsPaid)
                                     VALUES (@rid, @bid, GETDATE(), GETDATE() + 7, 0)";

                            SqlCommand cmdInsert = new SqlCommand(sqlInsert, conn);
                            cmdInsert.Transaction = transaction;
                            cmdInsert.Parameters.AddWithValue("@rid", txtReaderID.Text);
                            cmdInsert.Parameters.AddWithValue("@bid", txtBookID.Text);
                            cmdInsert.ExecuteNonQuery();

                            // B. Cập nhật trạng thái sách trong bảng BOOKS
                            string sqlUpdate = "UPDATE BOOKS SET Status = N'Borrowed' WHERE BookID = @bid";
                            SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, conn);
                            cmdUpdate.Transaction = transaction;
                            cmdUpdate.Parameters.AddWithValue("@bid", txtBookID.Text);
                            cmdUpdate.ExecuteNonQuery();

                            // 2. NẾU KHÔNG CÓ LỖI -> LƯU TẤT CẢ (COMMIT)
                            transaction.Commit();

                            // C. Ghi Log (Log không ảnh hưởng dữ liệu chính nên để ngoài cũng được, hoặc cho vào trong tuỳ bạn)
                            Logger.Log("Mượn Sách", $"Độc giả {txtReaderID.Text} mượn sách {txtBookTitle.Text} ({txtBookID.Text})");

                            MessageBox.Show("Mượn sách thành công! Hạn trả: " + DateTime.Now.AddDays(7).ToString("dd/MM/yyyy"));

                            // Reset giao diện
                            ResetBookPanel();
                            txtBookID.Clear();
                            txtBookID.Focus();
                            LoadReaderInfo();
                        }
                        catch (Exception ex)
                        {
                            // 3. NẾU CÓ BẤT KỲ LỖI GÌ -> HOÀN TÁC MỌI THỨ (ROLLBACK)
                            transaction.Rollback();
                            MessageBox.Show("Giao dịch thất bại! Hệ thống đã hoàn tác dữ liệu.\nLỗi: " + ex.Message, "Lỗi Transaction", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hệ thống: " + ex.Message);
                }
            }
        }
    }
}