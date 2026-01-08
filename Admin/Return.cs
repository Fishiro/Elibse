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

        // --- SỰ KIỆN KÝ TRẢ (Đã sửa lại SQL chuẩn theo bảng LOAN_RECORDS) ---
        private void btnConfirmReturn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReaderID.Text) || string.IsNullOrEmpty(txtBookID.Text))
            {
                MessageBox.Show("Vui lòng chọn độc giả và sách cần trả!");
                return;
            }

            // Logic xác định trạng thái
            string newBookStatus = "Available"; // Trạng thái cập nhật vào bảng BOOKS
            string loanNote = "Bình thường";    // Trạng thái cập nhật vào bảng LOAN_RECORDS (ReturnStatus)

            if (rbDamaged.Checked) { newBookStatus = "Damaged"; loanNote = "Hư hỏng"; }
            if (rbLost.Checked) { newBookStatus = "Lost"; loanNote = "Mất"; }

            if (MessageBox.Show($"Xác nhận trả sách '{txtBookTitle.Text}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = DatabaseConnection.GetConnection())
                    {
                        conn.Open();

                        // Bước A: Cập nhật LOAN_RECORDS
                        // SỬA ĐỔI: Không update cột Status (vì không có), mà update ReturnStatus
                        string sqlUpdateLoan = @"UPDATE LOAN_RECORDS 
                                                 SET ReturnDate = GETDATE(), ReturnStatus = @rStatus
                                                 WHERE ReaderID = @rid AND BookID = @bid AND ReturnDate IS NULL";

                        SqlCommand cmdLoan = new SqlCommand(sqlUpdateLoan, conn);
                        cmdLoan.Parameters.AddWithValue("@rStatus", loanNote); // Ghi chú: Bình thường/Hư hỏng...
                        cmdLoan.Parameters.AddWithValue("@rid", txtReaderID.Text);
                        cmdLoan.Parameters.AddWithValue("@bid", txtBookID.Text);

                        int rowsAffected = cmdLoan.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Bước B: Cập nhật bảng BOOKS (Để sách hiển thị đúng trạng thái trong kho)
                            string sqlUpdateBook = "UPDATE BOOKS SET Status = @stt WHERE BookID = @bid";
                            SqlCommand cmdBook = new SqlCommand(sqlUpdateBook, conn);
                            cmdBook.Parameters.AddWithValue("@stt", newBookStatus);
                            cmdBook.Parameters.AddWithValue("@bid", txtBookID.Text);
                            cmdBook.ExecuteNonQuery();

                            MessageBox.Show("Trả sách thành công!");
                            LoadReaderInfo(); // Load lại để cập nhật danh sách
                        }
                        else
                        {
                            MessageBox.Show("Lỗi: Không tìm thấy hồ sơ mượn hợp lệ.");
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