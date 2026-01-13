using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Elibse.Admin
{
    public partial class ExtendLoan : Form
    {
        public ExtendLoan()
        {
            InitializeComponent();
        }

        private void ExtendLoan_Load(object sender, EventArgs e)
        {
            LoadBorrowingReaders();
            ConfigureGrid();
        }

        // --- 1. TẢI DANH SÁCH ĐỘC GIẢ ĐANG CÓ SÁCH MƯỢN ---
        private void LoadBorrowingReaders()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    // Chỉ lấy những độc giả có ít nhất 1 cuốn sách chưa trả (ReturnDate IS NULL)
                    string query = @"SELECT DISTINCT r.ReaderID, r.FullName 
                                     FROM READERS r
                                     JOIN LOAN_RECORDS lr ON r.ReaderID = lr.ReaderID
                                     WHERE lr.ReturnDate IS NULL";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Thêm cột hiển thị kết hợp (Mã - Tên) cho dễ nhìn
                    dt.Columns.Add("DisplayInfo", typeof(string), "ReaderID + ' - ' + FullName");

                    cboReaders.DataSource = dt;
                    cboReaders.DisplayMember = "DisplayInfo";
                    cboReaders.ValueMember = "ReaderID";
                    cboReaders.SelectedIndex = -1; // Mặc định chưa chọn ai
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách độc giả: " + ex.Message);
            }
        }

        // --- 2. CẤU HÌNH BẢNG (GRID) ---
        private void ConfigureGrid()
        {
            dgvLoans.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLoans.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLoans.MultiSelect = false; // Chỉ cho phép chọn 1 dòng để gia hạn mỗi lần
            dgvLoans.AllowUserToAddRows = false;
            dgvLoans.ReadOnly = true;
        }

        // --- 3. KHI CHỌN ĐỘC GIẢ -> TẢI SÁCH HỌ ĐANG MƯỢN ---
        private void cboReaders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboReaders.SelectedValue == null) return;

            string readerID = cboReaders.SelectedValue.ToString();
            LoadReaderLoans(readerID);
        }

        private void LoadReaderLoans(string readerID)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    // Lấy thông tin sách và ngày hết hạn hiện tại
                    string query = @"SELECT lr.LoanID, b.BookID, b.Title, lr.LoanDate, lr.DueDate 
                                     FROM LOAN_RECORDS lr
                                     JOIN BOOKS b ON lr.BookID = b.BookID
                                     WHERE lr.ReaderID = @rid AND lr.ReturnDate IS NULL";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@rid", readerID);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvLoans.DataSource = dt;

                    // Đặt tên cột tiếng Việt cho đẹp
                    if (dgvLoans.Columns["LoanID"] != null) dgvLoans.Columns["LoanID"].Visible = false; // Ẩn ID
                    if (dgvLoans.Columns["BookID"] != null) dgvLoans.Columns["BookID"].HeaderText = "Mã Sách";
                    if (dgvLoans.Columns["Title"] != null) dgvLoans.Columns["Title"].HeaderText = "Tên Sách";
                    if (dgvLoans.Columns["LoanDate"] != null) dgvLoans.Columns["LoanDate"].HeaderText = "Ngày mượn";
                    if (dgvLoans.Columns["DueDate"] != null) dgvLoans.Columns["DueDate"].HeaderText = "Hạn trả hiện tại";
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải sách: " + ex.Message); }
        }

        // --- 4. XỬ LÝ NÚT GIA HẠN ---
        private void btnExtend_Click(object sender, EventArgs e)
        {
            // Kiểm tra đầu vào
            if (cboReaders.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn độc giả!", "Chưa chọn dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvLoans.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn cuốn sách cần gia hạn trong bảng!", "Chưa chọn dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy thông tin từ dòng đang chọn
            string loanID = dgvLoans.SelectedRows[0].Cells["LoanID"].Value.ToString();
            string bookTitle = dgvLoans.SelectedRows[0].Cells["Title"].Value.ToString();
            DateTime currentDueDate = Convert.ToDateTime(dgvLoans.SelectedRows[0].Cells["DueDate"].Value);

            int daysToAdd = (int)numDays.Value; // Lấy số ngày từ NumericUpDown

            int maxAllowedDays = GetMaxExtendDays(); // Gọi hàm lấy cấu hình từ DB

            if (daysToAdd > maxAllowedDays)
            {
                MessageBox.Show($"Quy định thư viện chỉ cho phép gia hạn tối đa {maxAllowedDays} ngày mỗi lần.\n" +
                                $"Bạn đang chọn: {daysToAdd} ngày.",
                                "Vi phạm quy định", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                // Tự động chỉnh lại số ngày về mức tối đa cho người dùng đỡ phải gõ lại
                numDays.Value = maxAllowedDays;
                return; // Dừng lại, không cho thực hiện tiếp
            }

            // Tính ngày mới
            DateTime newDueDate = currentDueDate.AddDays(daysToAdd);

            // Xác nhận
            string msg = $"Bạn có chắc muốn gia hạn sách '{bookTitle}' thêm {daysToAdd} ngày?\n" +
                         $"Hạn trả mới sẽ là: {newDueDate:dd/MM/yyyy}";

            if (MessageBox.Show(msg, "Xác nhận gia hạn", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                PerformExtension(loanID, newDueDate, daysToAdd, bookTitle);
            }
        }

        // --- HÀM LẤY SỐ NGÀY GIA HẠN TỐI ĐA TỪ CSDL ---
        private int GetMaxExtendDays()
        {
            int maxDays = 7; // Giá trị mặc định nếu chưa cấu hình
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    // Lấy dòng cấu hình đầu tiên
                    string query = "SELECT TOP 1 MaxExtendDays FROM SYSTEM_CONFIG";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        maxDays = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Lỗi Hệ Thống", "Không lấy được cấu hình gia hạn: " + ex.Message);
            }
            return maxDays;
        }

        private void PerformExtension(string loanID, DateTime newDate, int days, string bookTitle)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE LOAN_RECORDS SET DueDate = @newDate WHERE LoanID = @lid";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@newDate", newDate);
                    cmd.Parameters.AddWithValue("@lid", loanID);

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Logger.Log("Gia hạn sách", $"Gia hạn thêm {days} ngày cho sách '{bookTitle}'. Hạn mới: {newDate:dd/MM/yyyy}");
                        MessageBox.Show("Gia hạn thành công!", "Thông báo");

                        // Tải lại danh sách để cập nhật ngày mới lên bảng
                        if (cboReaders.SelectedValue != null)
                            LoadReaderLoans(cboReaders.SelectedValue.ToString());
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