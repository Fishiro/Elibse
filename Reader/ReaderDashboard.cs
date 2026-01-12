using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing; // Để dùng Color nếu cần
using System.Windows.Forms;

namespace Elibse.Reader
{
    public partial class ReaderDashboard : Form
    {
        // Biến lưu mã độc giả hiện tại
        private string currentReaderID;

        // Constructor nhận ID từ Login
        public ReaderDashboard(string readerId)
        {
            InitializeComponent();
            this.currentReaderID = readerId;
        }

        // Constructor mặc định (để tránh lỗi Designer)
        public ReaderDashboard()
        {
            InitializeComponent();
            this.currentReaderID = "R001"; // ID giả lập để test nếu chạy thẳng form này
        }

        // --- SỰ KIỆN KHI FORM LOAD ---
        private void ReaderDashboard_Load(object sender, EventArgs e)
        {
            // Cấu hình giao diện bảng cho đẹp
            dgvBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMyBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Nếu có dgvHistory ở Tab 3 thì uncomment dòng dưới
            // dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Tải dữ liệu cho cả 3 tab
            LoadAvailableBooks();
            LoadMyBooks();
            LoadUserInfoAndHistory();
        }

        // --- TAB 1: TÌM KIẾM & MƯỢN SÁCH ---

        // Hàm load sách có sẵn
        private void LoadAvailableBooks(string keyword = "")
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT BookID AS [Mã Sách], Title AS [Tên Sách], 
                                            Author AS [Tác Giả], Category AS [Thể Loại], Quantity AS [Còn Lại]
                                     FROM BOOKS 
                                     WHERE Quantity > 0 AND Status = 'Available'";

                    if (!string.IsNullOrEmpty(keyword))
                    {
                        query += " AND (Title LIKE @kw OR Author LIKE @kw)";
                    }

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    if (!string.IsNullOrEmpty(keyword))
                        da.SelectCommand.Parameters.AddWithValue("@kw", "%" + keyword + "%");

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvBooks.DataSource = dt;
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải sách: " + ex.Message); }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadAvailableBooks(txtSearch.Text.Trim());
        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một cuốn sách để mượn!");
                return;
            }

            string bookId = dgvBooks.SelectedRows[0].Cells["Mã Sách"].Value.ToString();

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // 1. Kiểm tra xem độc giả có đang mượn cuốn này mà chưa trả không?
                    string checkQuery = "SELECT COUNT(*) FROM LOAN_RECORDS WHERE BookID = @bid AND ReaderID = @rid AND ReturnDate IS NULL";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@bid", bookId);
                    checkCmd.Parameters.AddWithValue("@rid", currentReaderID);

                    if ((int)checkCmd.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("Bạn đang mượn cuốn này rồi, không thể mượn thêm!");
                        return;
                    }

                    // 2. Thực hiện mượn (Trừ số lượng sách + Thêm vào LoanRecords)
                    // Dùng Transaction để đảm bảo cả 2 việc cùng thành công
                    SqlTransaction transaction = conn.BeginTransaction();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.Transaction = transaction;

                    try
                    {
                        // Trừ số lượng sách
                        cmd.CommandText = "UPDATE BOOKS SET Quantity = Quantity - 1 WHERE BookID = @BookID";
                        cmd.Parameters.AddWithValue("@BookID", bookId);
                        cmd.ExecuteNonQuery();

                        // Thêm phiếu mượn (Hạn trả mặc định +7 ngày)
                        cmd.CommandText = @"INSERT INTO LOAN_RECORDS (BookID, ReaderID, LoanDate, DueDate) 
                                            VALUES (@BookID, @ReaderID, GETDATE(), GETDATE() + 7)";
                        cmd.Parameters.AddWithValue("@ReaderID", currentReaderID);
                        cmd.ExecuteNonQuery();

                        transaction.Commit();
                        MessageBox.Show("Mượn sách thành công! Hạn trả là 7 ngày.");

                        // Refresh lại dữ liệu các tab
                        LoadAvailableBooks();
                        LoadMyBooks();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw; // Ném lỗi ra ngoài để catch bắt
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mượn sách: " + ex.Message);
            }
        }

        // --- TAB 2: TỦ SÁCH CỦA TÔI & TRẢ SÁCH ---

        private void LoadMyBooks()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    // Load danh sách đang mượn (ReturnDate IS NULL)
                    string query = @"SELECT L.LoanID, B.BookID AS [Mã Sách], B.Title AS [Tên Sách], 
                                            L.LoanDate AS [Ngày Mượn], L.DueDate AS [Hạn Trả]
                                     FROM LOAN_RECORDS L
                                     JOIN BOOKS B ON L.BookID = B.BookID
                                     WHERE L.ReaderID = @rid AND L.ReturnDate IS NULL";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.SelectCommand.Parameters.AddWithValue("@rid", currentReaderID);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvMyBooks.DataSource = dt;

                    // Ẩn cột LoanID vì user không cần thấy
                    if (dgvMyBooks.Columns["LoanID"] != null) dgvMyBooks.Columns["LoanID"].Visible = false;
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải tủ sách: " + ex.Message); }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (dgvMyBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sách muốn trả!");
                return;
            }

            // Lấy LoanID (đã ẩn) và BookID từ dòng được chọn
            int loanId = Convert.ToInt32(dgvMyBooks.SelectedRows[0].Cells["LoanID"].Value);
            string bookId = dgvMyBooks.SelectedRows[0].Cells["Mã Sách"].Value.ToString();

            if (MessageBox.Show("Bạn có chắc muốn trả cuốn sách này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = DatabaseConnection.GetConnection())
                    {
                        conn.Open();
                        SqlTransaction transaction = conn.BeginTransaction();
                        SqlCommand cmd = conn.CreateCommand();
                        cmd.Transaction = transaction;

                        try
                        {
                            // Cập nhật ngày trả
                            cmd.CommandText = "UPDATE LOAN_RECORDS SET ReturnDate = GETDATE() WHERE LoanID = @lid";
                            cmd.Parameters.AddWithValue("@lid", loanId);
                            cmd.ExecuteNonQuery();

                            // Cộng lại số lượng sách vào kho
                            cmd.CommandText = "UPDATE BOOKS SET Quantity = Quantity + 1 WHERE BookID = @bid";
                            cmd.Parameters.AddWithValue("@bid", bookId);
                            cmd.ExecuteNonQuery();

                            transaction.Commit();
                            MessageBox.Show("Trả sách thành công!");

                            // Refresh dữ liệu
                            LoadMyBooks();
                            LoadAvailableBooks();
                            LoadUserInfoAndHistory();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi trả sách: " + ex.Message);
                }
            }
        }

        // --- TAB 3: TÀI KHOẢN & LỊCH SỬ ---

        private void LoadUserInfoAndHistory()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // 1. Load Thông tin cá nhân
                    string userQuery = "SELECT FullName, Email FROM READERS WHERE ReaderID = @rid";
                    SqlCommand cmd = new SqlCommand(userQuery, conn);
                    cmd.Parameters.AddWithValue("@rid", currentReaderID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Tìm Label theo tên bạn đã đặt (nhớ kiểm tra đúng tên trong Designer)
                        Control[] welcome = this.Controls.Find("lblWelcome", true);
                        if (welcome.Length > 0) welcome[0].Text = "Xin chào, " + reader["FullName"].ToString();

                        Control[] info = this.Controls.Find("lblUserInfo", true);
                        if (info.Length > 0) info[0].Text = reader["Email"].ToString();
                    }
                    reader.Close();

                    // 2. Load Lịch sử (Những sách đã trả) - Nếu bạn có dgvHistory
                    Control[] historyGrid = this.Controls.Find("dgvHistory", true);
                    if (historyGrid.Length > 0)
                    {
                        DataGridView dgvHist = (DataGridView)historyGrid[0];
                        string histQuery = @"SELECT B.Title AS [Sách], L.LoanDate AS [Ngày Mượn], 
                                                    L.ReturnDate AS [Ngày Trả]
                                             FROM LOAN_RECORDS L
                                             JOIN BOOKS B ON L.BookID = B.BookID
                                             WHERE L.ReaderID = @rid AND L.ReturnDate IS NOT NULL
                                             ORDER BY L.ReturnDate DESC";

                        SqlDataAdapter da = new SqlDataAdapter(histQuery, conn);
                        da.SelectCommand.Parameters.AddWithValue("@rid", currentReaderID);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvHist.DataSource = dt;
                    }
                }
            }
            catch { /* Có thể bỏ qua lỗi hiển thị lịch sử nếu chưa cần gấp */ }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close(); // Đóng form dashboard để quay về login (nếu login gọi ShowDialog)
        }
    }
}