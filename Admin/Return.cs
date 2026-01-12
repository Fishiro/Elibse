using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Elibse
{
    public partial class Return : Form
    {
        public Return()
        {
            InitializeComponent();
        }

        // --- 1. HÀM TẢI THÔNG TIN ĐỘC GIẢ & SÁCH ĐANG MƯỢN ---
        private void LoadReaderInfo()
        {
            string readerId = txtReaderID.Text.Trim();
            if (string.IsNullOrEmpty(readerId)) return;

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // A. Tải thông tin cá nhân độc giả
                    string sqlReader = "SELECT FullName, CreatedDate FROM READERS WHERE ReaderID = @rid";
                    SqlCommand cmdReader = new SqlCommand(sqlReader, conn);
                    cmdReader.Parameters.AddWithValue("@rid", readerId);

                    SqlDataReader r = cmdReader.ExecuteReader();
                    if (r.Read())
                    {
                        txtReaderName.Text = r["FullName"].ToString();
                        txtCreatedDate.Text = Convert.ToDateTime(r["CreatedDate"]).ToString("dd/MM/yyyy");
                        txtViolationStatus.Text = "Không"; // Tạm để mặc định
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy độc giả có mã này!");
                        ResetForm();
                        return;
                    }
                    r.Close();

                    // B. Tải danh sách sách ĐANG MƯỢN (ReturnDate is NULL)
                    string sqlLoan = @"SELECT b.BookID, b.Title 
                                       FROM LOAN_RECORDS lr 
                                       JOIN BOOKS b ON lr.BookID = b.BookID 
                                       WHERE lr.ReaderID = @rid AND lr.ReturnDate IS NULL";

                    SqlDataAdapter da = new SqlDataAdapter(sqlLoan, conn);
                    da.SelectCommand.Parameters.AddWithValue("@rid", readerId);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cboBorrowedBooks.DataSource = dt;
                    cboBorrowedBooks.DisplayMember = "Title";
                    cboBorrowedBooks.ValueMember = "BookID";

                    txtBorrowedCount.Text = dt.Rows.Count.ToString() + "/6";

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Độc giả này hiện không mượn cuốn sách nào.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private decimal GetBookPrice(string bookId)
        {
            decimal price = 0;
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    // Giả định cột giá sách trong bảng BOOKS tên là 'Price'
                    // Nếu trong CSDL của bạn tên khác (ví dụ 'Cost', 'GiaTien'...), hãy sửa lại nhé!
                    string sql = "SELECT Price FROM BOOKS WHERE BookID = @bid";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@bid", bookId);

                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        price = Convert.ToDecimal(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể lấy giá sách: " + ex.Message);
            }
            return price;
        }

        // --- 2. HÀM XÓA TRẮNG FORM ---
        private void ResetForm()
        {
            txtReaderName.Clear();
            txtCreatedDate.Clear();
            txtBorrowedCount.Text = "0/6";
            cboBorrowedBooks.DataSource = null;
            txtBookTitle.Clear();
            txtAuthor.Clear();
            txtBookID.Clear();
            rbNormal.Checked = true;
        }

        // --- SỰ KIỆN ENTER ---
        private void txtReaderID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadReaderInfo();
                e.SuppressKeyPress = true;
            }
        }

        // --- SỰ KIỆN RỜI CHUỘT ---
        private void txtReaderID_Leave(object sender, EventArgs e)
        {
            // Kiểm tra để tránh load lại khi không cần thiết hoặc khi form đang đóng
            if (!string.IsNullOrEmpty(txtReaderID.Text)) LoadReaderInfo();
        }

        // --- SỰ KIỆN CHỌN SÁCH ---
        private void cboBorrowedBooks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboBorrowedBooks.SelectedValue == null) return;

            string selectedBookID = cboBorrowedBooks.SelectedValue.ToString();
            txtBookID.Text = selectedBookID;

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT Title, Author FROM BOOKS WHERE BookID = @bid";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@bid", selectedBookID);

                    SqlDataReader r = cmd.ExecuteReader();
                    if (r.Read())
                    {
                        txtBookTitle.Text = r["Title"].ToString();
                        txtAuthor.Text = r["Author"].ToString();
                    }
                }
            }
            catch { }
        }

        private void btnConfirmReturn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReaderID.Text) || string.IsNullOrEmpty(txtBookID.Text))
            {
                MessageBox.Show("Vui lòng chọn độc giả và sách cần trả!");
                return;
            }

            // 1. Xác định trạng thái và mức phạt
            string newBookStatus = "Available";
            string loanNote = "Bình thường";
            decimal fineAmount = 0;
            bool isViolation = false;

            if (rbDamaged.Checked)
            {
                newBookStatus = "Damaged";
                loanNote = "Hư hỏng";
                isViolation = true;
                // Ví dụ: Hư hỏng đền 50% giá trị sách
                fineAmount = GetBookPrice(txtBookID.Text) * 0.5m;
            }
            else if (rbLost.Checked)
            {
                newBookStatus = "Lost";
                loanNote = "Mất";
                isViolation = true;
                // Ví dụ: Mất đền 100% giá trị sách
                fineAmount = GetBookPrice(txtBookID.Text);
            }

            // 2. Nếu có vi phạm -> Yêu cầu thanh toán trước
            if (isViolation && fineAmount > 0)
            {
                // Gọi form PaymentDialog mà chúng ta vừa tạo ở Bước 1
                PaymentDialog paymentForm = new PaymentDialog(fineAmount);

                if (paymentForm.ShowDialog() == DialogResult.OK)
                {
                    // Nếu thanh toán OK -> Ghi chú thêm vào log (tuỳ chọn)
                    loanNote += $" (Đã đền bù: {paymentForm.AmountPaid.ToString("N0")} VNĐ)";
                }
                else
                {
                    // Nếu huỷ thanh toán -> Dừng quy trình trả sách
                    MessageBox.Show("Bạn đã huỷ thanh toán. Quy trình trả sách bị huỷ.", "Thông báo");
                    return;
                }
            }

            // 3. Thực hiện cập nhật vào CSDL (Code cũ giữ nguyên logic)
            if (MessageBox.Show($"Xác nhận trả sách '{txtBookTitle.Text}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = DatabaseConnection.GetConnection())
                    {
                        conn.Open();
                        SqlTransaction transaction = conn.BeginTransaction(); // 1. Mở Transaction

                        try
                        {
                            // A. Cập nhật LOAN_RECORDS (Ghi ngày trả)
                            string sqlUpdateLoan = @"UPDATE LOAN_RECORDS 
                                         SET ReturnDate = GETDATE(), ReturnStatus = @rStatus
                                         WHERE ReaderID = @rid AND BookID = @bid AND ReturnDate IS NULL";

                            SqlCommand cmdLoan = new SqlCommand(sqlUpdateLoan, conn);
                            cmdLoan.Transaction = transaction; // <--- Quan trọng
                            cmdLoan.Parameters.AddWithValue("@rStatus", loanNote); // Biến loanNote lấy từ logic phía trên của bạn
                            cmdLoan.Parameters.AddWithValue("@rid", txtReaderID.Text);
                            cmdLoan.Parameters.AddWithValue("@bid", txtBookID.Text);

                            int rowsAffected = cmdLoan.ExecuteNonQuery();

                            // Kiểm tra xem có dòng nào được update không?
                            if (rowsAffected > 0)
                            {
                                // B. Cập nhật trạng thái sách (Available/Lost/Damaged)
                                string sqlUpdateBook = "UPDATE BOOKS SET Status = @stt WHERE BookID = @bid";
                                SqlCommand cmdBook = new SqlCommand(sqlUpdateBook, conn);
                                cmdBook.Transaction = transaction; // <--- Quan trọng
                                cmdBook.Parameters.AddWithValue("@stt", newBookStatus); // Biến newBookStatus lấy từ logic trên
                                cmdBook.Parameters.AddWithValue("@bid", txtBookID.Text);
                                cmdBook.ExecuteNonQuery();

                                // 2. COMMIT: Lưu thay đổi
                                transaction.Commit();

                                MessageBox.Show("Trả sách thành công!");
                                Logger.Log("Trả Sách", $"Độc giả {txtReaderID.Text} trả sách {txtBookTitle.Text}. Ghi chú: {loanNote}");

                                // Reset giao diện
                                LoadReaderInfo();
                                ResetForm(); // Cần đảm bảo bạn có hàm này hoặc reset các ô nhập thủ công
                            }
                            else
                            {
                                // Không tìm thấy hồ sơ mượn -> Rollback cho chắc (dù chưa sửa gì)
                                transaction.Rollback();
                                MessageBox.Show("Lỗi: Không tìm thấy hồ sơ mượn hợp lệ (Có thể sách đã được trả trước đó).");
                            }
                        }
                        catch (Exception ex)
                        {
                            // 3. ROLLBACK: Hoàn tác nếu lỗi
                            transaction.Rollback();
                            MessageBox.Show("Lỗi hệ thống (Đã hoàn tác): " + ex.Message);
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