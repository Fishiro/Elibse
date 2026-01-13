using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Elibse
{
    public partial class LateReturn : Form
    {
        public LateReturn()
        {
            InitializeComponent();
        }

        // --- SỰ KIỆN KHI MỞ FORM ---
        private void LateReturn_Load(object sender, EventArgs e)
        {
            LoadSortOptions();
            LoadFilterOptions();
            LoadLateReturnData("");
            SetupDataGridView();
        }

        // --- 1. TẢI DỮ LIỆU SÁCH TRẢ CHẬM ---
        private void LoadLateReturnData(string keyword)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Query: Lấy sách chưa trả + đã quá hạn
                    string query = @"
                        SELECT 
                            lr.LoanID,
                            lr.BookID,
                            b.Title AS BookTitle,
                            b.Author,
                            lr.ReaderID,
                            r.FullName AS ReaderName,
                            lr.LoanDate,
                            lr.DueDate,
                            DATEDIFF(DAY, lr.DueDate, GETDATE()) AS DaysLate
                        FROM LOAN_RECORDS lr
                        JOIN BOOKS b ON lr.BookID = b.BookID
                        JOIN READERS r ON lr.ReaderID = r.ReaderID
                        WHERE lr.ReturnDate IS NULL 
                          AND lr.DueDate < GETDATE()
                          AND (b.Title LIKE @kw OR lr.BookID LIKE @kw OR r.FullName LIKE @kw)
                    ";

                    // --- XỬ LÝ LỌC THEO DANH MỤC ---
                    string filterOption = comboBox1.SelectedItem?.ToString() ?? "Tên sách";
                    switch (filterOption)
                    {
                        case "Tên tác giả":
                            query += " AND b.Author LIKE @kw";
                            break;
                        case "Tên độc giả":
                            query += " AND r.FullName LIKE @kw";
                            break;
                            // Mặc định: Tìm theo tên sách (đã có trong WHERE)
                    }

                    // --- XỬ LÝ SẮP XẾP ---
                    string sortOption = comboBox2.SelectedItem?.ToString() ?? "A->Z";
                    switch (sortOption)
                    {
                        case "A->Z":
                            query += " ORDER BY b.Title ASC";
                            break;
                        case "Z->A":
                            query += " ORDER BY b.Title DESC";
                            break;
                        case "Trễ nhiều nhất":
                            query += " ORDER BY DaysLate DESC";
                            break;
                        case "Mượn gần đây":
                            query += " ORDER BY lr.LoanDate DESC";
                            break;
                        default:
                            query += " ORDER BY DaysLate DESC";
                            break;
                    }

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi");
            }
        }

        // --- 2. ĐỊNH DẠNG BẢNG ---
        private void SetupDataGridView()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Đặt tên cột tiếng Việt
            if (dataGridView1.Columns["LoanID"] != null)
                dataGridView1.Columns["LoanID"].HeaderText = "Mã Phiếu";
            if (dataGridView1.Columns["BookID"] != null)
                dataGridView1.Columns["BookID"].HeaderText = "Mã Sách";
            if (dataGridView1.Columns["BookTitle"] != null)
                dataGridView1.Columns["BookTitle"].HeaderText = "Tên Sách";
            if (dataGridView1.Columns["Author"] != null)
                dataGridView1.Columns["Author"].HeaderText = "Tác Giả";
            if (dataGridView1.Columns["ReaderID"] != null)
                dataGridView1.Columns["ReaderID"].HeaderText = "Mã ĐG";
            if (dataGridView1.Columns["ReaderName"] != null)
                dataGridView1.Columns["ReaderName"].HeaderText = "Tên Độc Giả";
            if (dataGridView1.Columns["LoanDate"] != null)
            {
                dataGridView1.Columns["LoanDate"].HeaderText = "Ngày Mượn";
                dataGridView1.Columns["LoanDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            if (dataGridView1.Columns["DueDate"] != null)
            {
                dataGridView1.Columns["DueDate"].HeaderText = "Hạn Trả";
                dataGridView1.Columns["DueDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
            if (dataGridView1.Columns["DaysLate"] != null)
            {
                dataGridView1.Columns["DaysLate"].HeaderText = "Số Ngày Trễ";
                // Tô đỏ nếu trễ > 7 ngày
                dataGridView1.CellFormatting += (s, e) =>
                {
                    if (dataGridView1.Columns[e.ColumnIndex].Name == "DaysLate"
                        && e.Value != null
                        && Convert.ToInt32(e.Value) > 7)
                    {
                        e.CellStyle.BackColor = System.Drawing.Color.LightCoral;
                        e.CellStyle.ForeColor = System.Drawing.Color.White;
                    }
                };
            }
        }

        // --- 3. TẢI DỮ LIỆU CHO COMBOBOX ---
        private void LoadFilterOptions()
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(new string[] { "Tên sách", "Tên tác giả", "Tên độc giả" });
            comboBox1.SelectedIndex = 0;
        }

        private void LoadSortOptions()
        {
            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(new string[] { "A->Z", "Z->A", "Trễ nhiều nhất", "Mượn gần đây" });
            comboBox2.SelectedIndex = 2; // Mặc định: Trễ nhiều nhất
        }

        // --- 4. SỰ KIỆN CÁC NÚT BẤM ---

        // Nút Tìm kiếm
        private void button2_Click(object sender, EventArgs e)
        {
            LoadLateReturnData(textBox1.Text.Trim());
        }

        // Nút Tải lại
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 2;
            LoadLateReturnData("");
        }

        // ComboBox thay đổi -> Tải lại dữ liệu
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLateReturnData(textBox1.Text.Trim());
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLateReturnData(textBox1.Text.Trim());
        }

        // Nhấn Enter ở ô tìm kiếm
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadLateReturnData(textBox1.Text.Trim());
                e.SuppressKeyPress = true;
            }
        }
    }
}