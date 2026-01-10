using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace Elibse
{
    public partial class AdminHistory : Form
    {
        public AdminHistory()
        {
            InitializeComponent();
        }

        private void AdminHistory_Load(object sender, EventArgs e)
        {
            SetupFilters();      // 1. Cài đặt các lựa chọn lọc
            LoadHistoryData();   // 2. Tải dữ liệu ban đầu
            SetupDataGridView(); // 3. Định dạng bảng
        }

        // --- 1. CÀI ĐẶT BỘ LỌC ---
        private void SetupFilters()
        {
            // Thêm các loại hành động vào ComboBox
            cboFilterAction.Items.Clear();
            cboFilterAction.Items.Add("Tất cả hành động"); // Index 0
            cboFilterAction.Items.Add("Đăng Nhập");
            cboFilterAction.Items.Add("Thêm Sách");
            cboFilterAction.Items.Add("Sửa Sách");
            cboFilterAction.Items.Add("Xóa Sách");
            cboFilterAction.Items.Add("Mượn Sách");
            cboFilterAction.Items.Add("Trả Sách");
            cboFilterAction.Items.Add("Xóa Độc Giả");

            cboFilterAction.SelectedIndex = 0; // Mặc định chọn Tất cả
            dtpDate.Value = DateTime.Now;      // Mặc định là hôm nay
        }

        // --- 2. TẢI DỮ LIỆU THÔNG MINH (CÓ TÌM KIẾM) ---
        private void LoadHistoryData()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Câu lệnh SQL cơ bản
                    string query = "SELECT LogID, AdminUsername, ActionType, ActionDetails, LogTime FROM ADMIN_LOGS WHERE 1=1";

                    // A. Điều kiện tìm từ khóa (Tên hoặc Chi tiết)
                    if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
                    {
                        query += " AND (AdminUsername LIKE @kw OR ActionDetails LIKE @kw)";
                    }

                    // B. Điều kiện lọc theo Hành động (Nếu không chọn 'Tất cả')
                    if (cboFilterAction.SelectedIndex > 0)
                    {
                        query += " AND ActionType = @action";
                    }

                    // C. Điều kiện lọc theo Ngày (Nếu CheckBox được tích)
                    if (chkFilterDate.Checked)
                    {
                        // So sánh ngày tháng năm (bỏ qua giờ phút)
                        query += " AND CAST(LogTime AS DATE) = CAST(@date AS DATE)";
                    }

                    // Luôn sắp xếp mới nhất lên đầu
                    query += " ORDER BY LogTime DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    // Truyền tham số an toàn
                    if (!string.IsNullOrEmpty(txtSearch.Text.Trim()))
                        cmd.Parameters.AddWithValue("@kw", "%" + txtSearch.Text.Trim() + "%");

                    if (cboFilterAction.SelectedIndex > 0)
                        cmd.Parameters.AddWithValue("@action", cboFilterAction.SelectedItem.ToString());

                    if (chkFilterDate.Checked)
                        cmd.Parameters.AddWithValue("@date", dtpDate.Value);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvHistory.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch sử: " + ex.Message);
            }
        }

        private void SetupDataGridView()
        {
            dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // Đặt tên cột tiếng Việt
            if (dgvHistory.Columns["LogID"] != null) dgvHistory.Columns["LogID"].HeaderText = "ID";
            if (dgvHistory.Columns["AdminUsername"] != null) dgvHistory.Columns["AdminUsername"].HeaderText = "Người thực hiện";
            if (dgvHistory.Columns["ActionType"] != null) dgvHistory.Columns["ActionType"].HeaderText = "Hành động";
            if (dgvHistory.Columns["ActionDetails"] != null) dgvHistory.Columns["ActionDetails"].HeaderText = "Chi tiết";
            if (dgvHistory.Columns["LogTime"] != null)
            {
                dgvHistory.Columns["LogTime"].HeaderText = "Thời gian";
                dgvHistory.Columns["LogTime"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
            }
        }

        // --- CÁC SỰ KIỆN NÚT BẤM ---

        // Nút Tìm kiếm
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadHistoryData();
        }

        // Nút Tải lại (Reset bộ lọc về mặc định)
        private void btnReload_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cboFilterAction.SelectedIndex = 0;
            chkFilterDate.Checked = false;
            dtpDate.Value = DateTime.Now;
            LoadHistoryData();
        }

        // Checkbox thay đổi -> Tự động tìm kiếm luôn cho tiện
        private void chkFilterDate_CheckedChanged(object sender, EventArgs e)
        {
            LoadHistoryData();
        }

        // Nút Xuất File (Giữ nguyên như cũ)
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvHistory.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text File|*.txt";
            sfd.FileName = "NhatKyHoatDong_" + DateTime.Now.ToString("ddMMyyyy_HHmm") + ".txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName))
                    {
                        sw.WriteLine("=== NHẬT KÝ HOẠT ĐỘNG HỆ THỐNG ELIBSE ===");
                        sw.WriteLine($"Thời gian xuất: {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");
                        sw.WriteLine($"Người xuất: {Session.CurrentAdminUsername}");
                        sw.WriteLine("--------------------------------------------------\n");

                        foreach (DataGridViewRow row in dgvHistory.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                string time = Convert.ToDateTime(row.Cells["LogTime"].Value).ToString("dd/MM/yyyy HH:mm:ss");
                                string user = row.Cells["AdminUsername"].Value?.ToString() ?? "Unknown";
                                string action = row.Cells["ActionType"].Value?.ToString() ?? "";
                                string detail = row.Cells["ActionDetails"].Value?.ToString() ?? "";

                                sw.WriteLine($"[{time}] - ADMIN: {user}");
                                sw.WriteLine($"   Hành động: {action}");
                                sw.WriteLine($"   Chi tiết:  {detail}");
                                sw.WriteLine("- - - - - - - - - - - - -");
                            }
                        }
                    }
                    MessageBox.Show("Xuất file thành công!\nĐường dẫn: " + sfd.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi ghi file: " + ex.Message);
                }
            }
        }
    }
}