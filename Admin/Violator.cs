using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Elibse
{
    public partial class Violator : Form
    {
        public Violator()
        {
            InitializeComponent();
        }

        // --- SỰ KIỆN KHI MỞ FORM ---
        private void Violator_Load(object sender, EventArgs e)
        {
            // 1. Cài đặt các lựa chọn cho ComboBox sắp xếp
            // Lưu ý: Đảm bảo bạn đã đặt tên ComboBox là cboSort trong Design
            if (cboSort.Items.Count == 0)
            {
                cboSort.Items.Add("Tên A->Z");
                cboSort.Items.Add("Tên Z->A");
                cboSort.Items.Add("Mới bị khóa gần đây");
            }
            cboSort.SelectedIndex = 0; // Mặc định chọn cái đầu tiên

            LoadViolators("");
            SetupDataGridView();
        }

        // --- 1. TẢI DANH SÁCH (CÓ SẮP XẾP) ---
        private void LoadViolators(string keyword)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Câu lệnh cơ bản
                    string query = @"SELECT ReaderID, FullName, DOB, PhoneNumber, Email, Status, CreatedDate 
                                     FROM READERS 
                                     WHERE Status = 'Locked' 
                                     AND (FullName LIKE @kw OR ReaderID LIKE @kw)";

                    // --- XỬ LÝ LOGIC SẮP XẾP ---
                    // Lấy giá trị đang được chọn trong ComboBox
                    string sortOption = cboSort.SelectedItem?.ToString() ?? "";

                    switch (sortOption)
                    {
                        case "Tên A->Z":
                            query += " ORDER BY FullName ASC";
                            break;
                        case "Tên Z->A":
                            query += " ORDER BY FullName DESC";
                            break;
                        case "Mới bị khóa gần đây":
                            // Sắp xếp theo ngày tạo (hoặc ngày khóa nếu có) giảm dần
                            query += " ORDER BY CreatedDate DESC";
                            break;
                        default:
                            query += " ORDER BY FullName ASC";
                            break;
                    }

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvViolators.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        // --- 2. ĐỊNH DẠNG BẢNG ---
        private void SetupDataGridView()
        {
            dgvViolators.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (dgvViolators.Columns["ReaderID"] != null) dgvViolators.Columns["ReaderID"].HeaderText = "Mã Độc Giả";
            if (dgvViolators.Columns["FullName"] != null) dgvViolators.Columns["FullName"].HeaderText = "Họ Tên";
            if (dgvViolators.Columns["PhoneNumber"] != null) dgvViolators.Columns["PhoneNumber"].HeaderText = "SĐT";
            if (dgvViolators.Columns["Email"] != null) dgvViolators.Columns["Email"].HeaderText = "Email";
            if (dgvViolators.Columns["Status"] != null) dgvViolators.Columns["Status"].HeaderText = "Trạng Thái";

            // Ẩn cột ngày tạo (chỉ dùng để sắp xếp, không cần hiện)
            if (dgvViolators.Columns["CreatedDate"] != null) dgvViolators.Columns["CreatedDate"].Visible = false;
            // Ẩn cột DOB nếu không cần thiết
            if (dgvViolators.Columns["DOB"] != null) dgvViolators.Columns["DOB"].Visible = false;
        }

        // --- 3. CÁC SỰ KIỆN NÚT BẤM & TÌM KIẾM ---

        // Nút Tìm kiếm
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadViolators(txtSearch.Text.Trim());
        }

        // Nhấn Enter tại ô tìm kiếm
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadViolators(txtSearch.Text.Trim());
                e.SuppressKeyPress = true; // Tắt tiếng bíp
            }
        }

        // Nút Tải lại (Reset bộ lọc)
        private void btnReload_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cboSort.SelectedIndex = 0; // Reset về mặc định
            LoadViolators("");
        }

        // --- SỰ KIỆN QUAN TRỌNG: KHI CHỌN SẮP XẾP KHÁC ---
        // (Nhớ quay lại Design, chọn cboSort -> Tab Events -> Double click vào SelectedIndexChanged để gán hàm này)
        private void cboSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Tải lại dữ liệu ngay lập tức khi người dùng chọn kiểu sắp xếp mới
            LoadViolators(txtSearch.Text.Trim());
        }

        // --- 4. XỬ LÝ MỞ KHÓA (ÂN XÁ) ---
        private void btnUnlock_Click(object sender, EventArgs e)
        {
            if (dgvViolators.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn độc giả cần mở khóa!", "Thông báo");
                return;
            }

            // Lấy thông tin dòng đang chọn
            string readerID = dgvViolators.SelectedRows[0].Cells["ReaderID"].Value.ToString();
            string readerName = dgvViolators.SelectedRows[0].Cells["FullName"].Value.ToString();

            if (MessageBox.Show($"Bạn có chắc muốn mở khóa cho độc giả {readerName}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = DatabaseConnection.GetConnection())
                    {
                        conn.Open();
                        string sql = "UPDATE READERS SET Status = 'Active' WHERE ReaderID = @rid";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@rid", readerID);
                        cmd.ExecuteNonQuery();

                        // Ghi Log
                        Logger.Log("Quản lý Độc giả", $"Đã mở khóa tài khoản cho độc giả: {readerName} ({readerID})");

                        MessageBox.Show("Đã mở khóa thành công!");

                        // Tải lại danh sách (độc giả vừa mở khóa sẽ biến mất khỏi danh sách vi phạm)
                        LoadViolators(txtSearch.Text.Trim());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }
    }
}