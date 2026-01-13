using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Elibse
{
    public partial class AdminHistory : Form
    {
        public AdminHistory()
        {
            InitializeComponent();
        }

        // --- 1. SỰ KIỆN KHI FORM MỞ ---
        private void AdminHistory_Load(object sender, EventArgs e)
        {
            LoadActionTypes();
            LoadHistoryData();
            SetupDataGridView();
        }

        // --- 2. TẢI DANH SÁCH LOẠI HÀNH ĐỘNG (CHO COMBOBOX) ---
        private void LoadActionTypes()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    // Lấy danh sách các loại hành động KHÔNG TRÙNG từ bảng LOG
                    string query = "SELECT DISTINCT ActionType FROM ADMIN_LOGS ORDER BY ActionType";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    cboFilterAction.Items.Clear();
                    cboFilterAction.Items.Add("Tất cả"); // Thêm lựa chọn xem tất cả

                    while (reader.Read())
                    {
                        string actionType = reader["ActionType"].ToString();
                        if (!string.IsNullOrEmpty(actionType))
                            cboFilterAction.Items.Add(actionType);
                    }

                    cboFilterAction.SelectedIndex = 0; // Mặc định chọn "Tất cả"
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách hành động: " + ex.Message);
            }
        }

        // --- 3. TẢI DỮ LIỆU LỊCH SỬ ---
        private void LoadHistoryData()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Xây dựng câu truy vấn động
                    StringBuilder queryBuilder = new StringBuilder();
                    queryBuilder.Append(@"SELECT LogID, AdminUsername, ActionType, ActionDetails, LogTime 
                                          FROM ADMIN_LOGS 
                                          WHERE 1=1"); // WHERE 1=1 để dễ thêm điều kiện

                    // A. LỌC THEO LOẠI HÀNH ĐỘNG (COMBOBOX)
                    string selectedAction = cboFilterAction.SelectedItem?.ToString();
                    if (!string.IsNullOrEmpty(selectedAction) && selectedAction != "Tất cả")
                    {
                        queryBuilder.Append(" AND ActionType = @actionType");
                    }

                    // B. LỌC THEO NGÀY (CHECKBOX + DATETIMEPICKER)
                    if (chkFilterDate.Checked)
                    {
                        // Lọc các log trong cùng ngày được chọn
                        queryBuilder.Append(" AND CAST(LogTime AS DATE) = @selectedDate");
                    }

                    // C. TÌM KIẾM THEO TỪ KHÓA (TEXTBOX)
                    string keyword = txtSearch.Text.Trim();
                    if (!string.IsNullOrEmpty(keyword))
                    {
                        queryBuilder.Append(" AND (ActionDetails LIKE @keyword OR AdminUsername LIKE @keyword)");
                    }

                    // D. SẮP XẾP: Mới nhất trước
                    queryBuilder.Append(" ORDER BY LogTime DESC");

                    // Thực thi truy vấn
                    SqlCommand cmd = new SqlCommand(queryBuilder.ToString(), conn);

                    // Gán giá trị tham số
                    if (!string.IsNullOrEmpty(selectedAction) && selectedAction != "Tất cả")
                        cmd.Parameters.AddWithValue("@actionType", selectedAction);

                    if (chkFilterDate.Checked)
                        cmd.Parameters.AddWithValue("@selectedDate", dtpDate.Value.Date);

                    if (!string.IsNullOrEmpty(keyword))
                        cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                    // Đổ dữ liệu vào DataGridView
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvHistory.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        // --- 4. ĐỊNH DẠNG BẢNG ---
        private void SetupDataGridView()
        {
            dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistory.AllowUserToAddRows = false;
            dgvHistory.ReadOnly = true;

            // Đặt tên cột tiếng Việt
            if (dgvHistory.Columns["LogID"] != null)
            {
                dgvHistory.Columns["LogID"].HeaderText = "ID";
                dgvHistory.Columns["LogID"].Width = 50;
            }
            if (dgvHistory.Columns["AdminUsername"] != null)
                dgvHistory.Columns["AdminUsername"].HeaderText = "Admin";

            if (dgvHistory.Columns["ActionType"] != null)
                dgvHistory.Columns["ActionType"].HeaderText = "Loại hành động";

            if (dgvHistory.Columns["ActionDetails"] != null)
            {
                dgvHistory.Columns["ActionDetails"].HeaderText = "Chi tiết";
                dgvHistory.Columns["ActionDetails"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            if (dgvHistory.Columns["LogTime"] != null)
            {
                dgvHistory.Columns["LogTime"].HeaderText = "Thời gian";
                dgvHistory.Columns["LogTime"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                dgvHistory.Columns["LogTime"].Width = 130;
            }
        }

        // --- 5. CÁC SỰ KIỆN ---

        // Khi thay đổi lựa chọn trong ComboBox
        private void cboFilterAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadHistoryData(); // Tải lại dữ liệu với bộ lọc mới
        }

        // Khi tick/untick CheckBox lọc theo ngày
        private void chkFilterDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpDate.Enabled = chkFilterDate.Checked; // Bật/tắt DateTimePicker
            LoadHistoryData();
        }

        // Khi thay đổi ngày trong DateTimePicker
        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            if (chkFilterDate.Checked)
                LoadHistoryData();
        }

        // Nút Tìm kiếm
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadHistoryData();
        }

        // Nhấn Enter ở ô tìm kiếm
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadHistoryData();
                e.SuppressKeyPress = true;
            }
        }

        // Nút Tải lại (Reset bộ lọc)
        private void btnReload_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cboFilterAction.SelectedIndex = 0;
            chkFilterDate.Checked = false;
            dtpDate.Value = DateTime.Now;
            LoadHistoryData();
        }

        // --- 6. NÚT XUẤT RA FILE TXT ---
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvHistory.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo");
                return;
            }

            // Mở hộp thoại lưu file
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files (*.txt)|*.txt";
            sfd.FileName = $"Log_History_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                    {
                        // Tiêu đề file
                        writer.WriteLine("==============================================");
                        writer.WriteLine("       LỊCH SỬ HOẠT ĐỘNG HỆ THỐNG ELIBSE");
                        writer.WriteLine($"       Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                        writer.WriteLine("==============================================");
                        writer.WriteLine();

                        // Ghi từng dòng
                        foreach (DataGridViewRow row in dgvHistory.Rows)
                        {
                            if (row.IsNewRow) continue;

                            writer.WriteLine($"[ID: {row.Cells["LogID"].Value}]");
                            writer.WriteLine($"Admin: {row.Cells["AdminUsername"].Value}");
                            writer.WriteLine($"Hành động: {row.Cells["ActionType"].Value}");
                            writer.WriteLine($"Chi tiết: {row.Cells["ActionDetails"].Value}");
                            writer.WriteLine($"Thời gian: {Convert.ToDateTime(row.Cells["LogTime"].Value):dd/MM/yyyy HH:mm:ss}");
                            writer.WriteLine("----------------------------------------------");
                        }

                        writer.WriteLine();
                        writer.WriteLine($"Tổng số bản ghi: {dgvHistory.Rows.Count}");
                    }

                    MessageBox.Show($"Đã xuất thành công!\nFile: {sfd.FileName}", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Hỏi có muốn mở file ngay không
                    if (MessageBox.Show("Bạn có muốn mở file vừa xuất không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xuất file: " + ex.Message, "Lỗi");
                }
            }
        }
    }
}