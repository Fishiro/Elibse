using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient; // Đừng quên dòng này để kết nối SQL
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Elibse
{
    // Lưu ý: Đảm bảo Namespace trùng với dự án của bạn (Elibse hoặc Elibse.Admin)
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
                        // Giả sử chưa có module vi phạm, tạm để mặc định
                        txtViolationStatus.Text = "Không";
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy độc giả có mã này!");
                        ResetForm();
                        return;
                    }
                    r.Close();

                    // B. Tải danh sách sách ĐANG MƯỢN (ReturnDate is NULL)
                    // Lấy BookID để xử lý, lấy Title để hiển thị
                    string sqlLoan = @"SELECT b.BookID, b.Title 
                                       FROM LOAN_RECORDS lr 
                                       JOIN BOOKS b ON lr.BookID = b.BookID 
                                       WHERE lr.ReaderID = @rid AND lr.ReturnDate IS NULL";

                    SqlDataAdapter da = new SqlDataAdapter(sqlLoan, conn);
                    da.SelectCommand.Parameters.AddWithValue("@rid", readerId);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Đổ dữ liệu vào ComboBox
                    cboBorrowedBooks.DataSource = dt;
                    cboBorrowedBooks.DisplayMember = "Title"; // Cái hiển thị cho người dùng thấy
                    cboBorrowedBooks.ValueMember = "BookID";  // Cái giá trị ẩn bên trong để mình code

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

        // --- 2. HÀM XÓA TRẮNG FORM (Dùng khi tìm không thấy hoặc sau khi trả xong) ---
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

        // --- SỰ KIỆN 1: KHI NHẤN ENTER Ở Ô MÃ ĐỘC GIẢ ---
        private void txtReaderID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadReaderInfo();
                e.SuppressKeyPress = true; // Tắt tiếng 'bíp' của windows
            }
        }

        // --- SỰ KIỆN 2: KHI CHUỘT RỜI KHỎI Ô MÃ ĐỘC GIẢ ---
        private void txtReaderID_Leave(object sender, EventArgs e)
        {
            LoadReaderInfo();
        }

        // --- SỰ KIỆN 3: KHI CHỌN SÁCH TRONG COMBOBOX ---
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

        // --- SỰ KIỆN 4: NÚT KÝ TRẢ (QUAN TRỌNG NHẤT) ---
        private void btnConfirmReturn_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra đầu vào
            if (string.IsNullOrEmpty(txtReaderID.Text) || string.IsNullOrEmpty(txtBookID.Text))
            {
                MessageBox.Show("Vui lòng chọn độc giả và sách cần trả!");
                return;
            }

            // 2. Xác định trạng thái sách trả về
            string returnStatus = "Available"; // Mặc định trả xong thì sách Sẵn sàng
            string note = "Đã trả";

            if (rbDamaged.Checked) { returnStatus = "Damaged"; note = "Trả (Hư hỏng)"; }
            if (rbLost.Checked) { returnStatus = "Lost"; note = "Làm mất"; }

            // 3. Thực hiện Update Database
            if (MessageBox.Show($"Xác nhận trả sách '{txtBookTitle.Text}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = DatabaseConnection.GetConnection())
                    {
                        conn.Open();

                        // Bước A: Cập nhật bảng LOAN_RECORDS (Tìm đúng dòng reader đang mượn book đó)
                        string sqlUpdateLoan = @"UPDATE LOAN_RECORDS 
                                                 SET ReturnDate = GETDATE(), Status = 'Returned'
                                                 WHERE ReaderID = @rid AND BookID = @bid AND ReturnDate IS NULL";

                        SqlCommand cmdLoan = new SqlCommand(sqlUpdateLoan, conn);
                        cmdLoan.Parameters.AddWithValue("@rid", txtReaderID.Text);
                        cmdLoan.Parameters.AddWithValue("@bid", txtBookID.Text);

                        int rowsAffected = cmdLoan.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Bước B: Cập nhật trạng thái trong bảng BOOKS
                            string sqlUpdateBook = "UPDATE BOOKS SET Status = @stt WHERE BookID = @bid";
                            SqlCommand cmdBook = new SqlCommand(sqlUpdateBook, conn);
                            cmdBook.Parameters.AddWithValue("@stt", returnStatus);
                            cmdBook.Parameters.AddWithValue("@bid", txtBookID.Text);
                            cmdBook.ExecuteNonQuery();

                            MessageBox.Show("Trả sách thành công!");

                            // Tải lại thông tin để cập nhật danh sách (sách vừa trả sẽ biến mất khỏi combobox)
                            LoadReaderInfo();
                        }
                        else
                        {
                            MessageBox.Show("Lỗi: Không tìm thấy bản ghi mượn hợp lệ (Có thể sách đã được trả trước đó).");
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