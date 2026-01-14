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
            this.currentReaderID = "RD00001";
        }

        // --- SỰ KIỆN KHI FORM LOAD ---
        private void ReaderDashboard_Load(object sender, EventArgs e)
        {
            // Cấu hình giao diện bảng cho đẹp
            dgvBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMyBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // --- BỔ SUNG ĐOẠN NÀY ---
            // Giúp chọn cả hàng khi click, thay vì chỉ chọn 1 ô
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMyBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Chặn chọn nhiều dòng cùng lúc để tránh lỗi logic
            dgvBooks.MultiSelect = false;
            dgvMyBooks.MultiSelect = false;
            // ------------------------

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
                    string query = @"
                SELECT 
                    B.BookID AS [Mã Sách], 
                    B.Title AS [Tên Sách], 
                    B.Author AS [Tác Giả], 
                    C.CategoryName AS [Thể Loại]
                FROM BOOKS B
                LEFT JOIN CATEGORIES C ON B.CategoryID = C.CategoryID
                WHERE B.Status = 'Available'";

                    if (!string.IsNullOrEmpty(keyword))
                    {
                        query += " AND (B.Title LIKE @kw OR B.Author LIKE @kw)";
                    }

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    if (!string.IsNullOrEmpty(keyword))
                        da.SelectCommand.Parameters.AddWithValue("@kw", "%" + keyword + "%");

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvBooks.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải sách: " + ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadAvailableBooks(txtSearch.Text.Trim());
        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra chọn hàng
            if (dgvBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một cuốn sách để mượn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Kiểm tra dữ liệu ô
            if (dgvBooks.SelectedRows[0].Cells["Mã Sách"].Value == null)
            {
                MessageBox.Show("Dữ liệu sách không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Lấy thông tin sách
            string bookId = dgvBooks.SelectedRows[0].Cells["Mã Sách"].Value.ToString();
            // Lấy thêm Tên Sách để hiện trong câu hỏi cho thân thiện
            string bookTitle = dgvBooks.SelectedRows[0].Cells["Tên Sách"].Value.ToString();

            // HỘP THOẠI XÁC NHẬN ---
            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn mượn cuốn sách:\n\n\"{bookTitle}\"\n\nkhông?",
                "Xác nhận mượn sách",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            // Nếu người dùng bấm No (Không), thì dừng hàm tại đây (return)
            if (result == DialogResult.No)
            {
                return;
            }

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // 3. Kiểm tra xem độc giả có đang mượn cuốn này chưa trả không?
                    string checkQuery = "SELECT COUNT(*) FROM LOAN_RECORDS WHERE BookID = @bid AND ReaderID = @rid AND ReturnDate IS NULL";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@bid", bookId);
                    checkCmd.Parameters.AddWithValue("@rid", currentReaderID);

                    if ((int)checkCmd.ExecuteScalar() > 0)
                    {
                        MessageBox.Show($"Bạn đang mượn cuốn \"{bookTitle}\" rồi, không thể mượn thêm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 4. Thực hiện mượn (Transaction)
                    SqlTransaction transaction = conn.BeginTransaction();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.Transaction = transaction;

                    try
                    {
                        // Trừ số lượng (Cập nhật trạng thái)
                        cmd.CommandText = "UPDATE BOOKS SET Status = 'Borrowed' WHERE BookID = @BookID";
                        cmd.Parameters.AddWithValue("@BookID", bookId);
                        cmd.ExecuteNonQuery();

                        // Thêm phiếu mượn
                        cmd.CommandText = @"INSERT INTO LOAN_RECORDS (BookID, ReaderID, LoanDate, DueDate) 
                                    VALUES (@BookID, @ReaderID, GETDATE(), GETDATE() + 7)";
                        cmd.Parameters.AddWithValue("@ReaderID", currentReaderID);
                        cmd.ExecuteNonQuery();

                        transaction.Commit();

                        // Thông báo thành công kèm Icon vui vẻ
                        MessageBox.Show($"Mượn sách \"{bookTitle}\" thành công!\nHạn trả là 7 ngày.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Refresh lại dữ liệu
                        LoadAvailableBooks();
                        LoadMyBooks();
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
                MessageBox.Show("Lỗi mượn sách: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if (dgvMyBooks.SelectedRows[0].Cells["LoanID"].Value == null ||
                dgvMyBooks.SelectedRows[0].Cells["Mã Sách"].Value == null)
            {
                MessageBox.Show("Dữ liệu không hợp lệ!");
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
                            cmd.CommandText = "UPDATE BOOKS SET Status = 'Available' WHERE BookID = @bid";
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
                        lblWelcome.Text = "Xin chào, " + reader["FullName"].ToString();
                        lblUserInfo.Text = "Email: " + reader["Email"].ToString();
                    }
                    reader.Close();

                    // 2. Load Lịch sử
                    string histQuery = @"SELECT B.Title AS [Sách], 
                                        L.LoanDate AS [Ngày Mượn], 
                                        L.ReturnDate AS [Ngày Trả],
                                        (CASE WHEN L.ReturnDate IS NULL THEN N'Đang mượn' ELSE N'Đã trả' END) AS [Trạng Thái]
                                        FROM LOAN_RECORDS L
                                        JOIN BOOKS B ON L.BookID = B.BookID
                                        WHERE L.ReaderID = @rid 
                                        ORDER BY L.LoanDate DESC";

                    SqlDataAdapter da = new SqlDataAdapter(histQuery, conn);
                    da.SelectCommand.Parameters.AddWithValue("@rid", currentReaderID);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvHistory.DataSource = dt;

                    dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin: " + ex.Message);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close(); // Đóng form dashboard để quay về login (nếu login gọi ShowDialog)
        }
    }
}