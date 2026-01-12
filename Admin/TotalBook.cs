using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Elibse.Admin
{
    public partial class TotalBook : Form
    {
        public TotalBook()
        {
            InitializeComponent();
        }

        private void TotalBook_Load(object sender, EventArgs e)
        {
            LoadCategoryFilter();
            LoadSortOptions();
            LoadBookData("");
            SetupDataGridView();
        }

        // --- 1. TẢI DỮ LIỆU ---
        private void LoadBookData(string keyword)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT b.BookID, b.Title, b.Author, c.CategoryName, b.Price, b.Status, b.ImportDate 
                                     FROM BOOKS b
                                     LEFT JOIN CATEGORIES c ON b.CategoryID = c.CategoryID
                                     WHERE (b.Title LIKE @kw OR b.BookID LIKE @kw OR b.Author LIKE @kw)";

                    if (cboFilterCategory.SelectedIndex > 0)
                        query += " AND c.CategoryName = @catName";

                    string sortOption = cboSortOrder.SelectedItem?.ToString() ?? "";
                    switch (sortOption)
                    {
                        case "Tên A->Z": query += " ORDER BY b.Title ASC"; break;
                        case "Tên Z->A": query += " ORDER BY b.Title DESC"; break;
                        case "Giá thấp->cao": query += " ORDER BY b.Price ASC"; break;
                        case "Giá cao->thấp": query += " ORDER BY b.Price DESC"; break;
                        default: query += " ORDER BY b.ImportDate DESC"; break;
                    }

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");
                    if (cboFilterCategory.SelectedIndex > 0)
                        cmd.Parameters.AddWithValue("@catName", cboFilterCategory.SelectedItem.ToString());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvBooks.DataSource = dt;
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải sách: " + ex.Message); }
        }

        private void LoadCategoryFilter()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    SqlDataReader reader = new SqlCommand("SELECT CategoryName FROM CATEGORIES", conn).ExecuteReader();
                    cboFilterCategory.Items.Clear();
                    cboFilterCategory.Items.Add("Tất cả");
                    while (reader.Read()) cboFilterCategory.Items.Add(reader["CategoryName"].ToString());
                    cboFilterCategory.SelectedIndex = 0;
                }
            }
            catch { }
        }

        private void LoadSortOptions()
        {
            cboSortOrder.Items.Clear();
            cboSortOrder.Items.AddRange(new string[] { "Mới nhập nhất", "Tên A->Z", "Tên Z->A", "Giá thấp->cao", "Giá cao->thấp" });
            cboSortOrder.SelectedIndex = 0;
        }

        private void SetupDataGridView()
        {
            if (dgvBooks.Columns["BookID"] != null) dgvBooks.Columns["BookID"].HeaderText = "Mã Sách";
            if (dgvBooks.Columns["Title"] != null) dgvBooks.Columns["Title"].HeaderText = "Tên Sách";
            if (dgvBooks.Columns["Author"] != null) dgvBooks.Columns["Author"].HeaderText = "Tác Giả";
            if (dgvBooks.Columns["CategoryName"] != null) dgvBooks.Columns["CategoryName"].HeaderText = "Thể Loại";
            if (dgvBooks.Columns["Price"] != null)
            {
                dgvBooks.Columns["Price"].HeaderText = "Giá Tiền";
                dgvBooks.Columns["Price"].DefaultCellStyle.Format = "N0";
            }
            if (dgvBooks.Columns["Status"] != null) dgvBooks.Columns["Status"].HeaderText = "Trạng Thái";
            dgvBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Chọn cả dòng
        }

        // --- 2. CÁC NÚT BẤM ---

        // Nút Thêm: Mở form AddBook ở chế độ thêm
        private void btnAddBook_Click(object sender, EventArgs e)
        {
            AddBook frm = new AddBook();
            frm.ShowDialog();
            LoadBookData(txtSearchBook.Text.Trim());
        }

        // Nút Sửa: Mở form AddBook ở chế độ Sửa (Truyền ID sang)
        private void btnEditBook_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count > 0)
            {
                string bookID = dgvBooks.SelectedRows[0].Cells["BookID"].Value.ToString();
                AddBook frm = new AddBook();
                frm.EnableEditMode(bookID); // Kích hoạt chế độ sửa
                frm.ShowDialog();
                LoadBookData(txtSearchBook.Text.Trim());
            }
            else MessageBox.Show("Vui lòng chọn sách cần sửa!");
        }

        // Nút Xóa
        private void btnDeleteBook_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count > 0)
            {
                string id = dgvBooks.SelectedRows[0].Cells["BookID"].Value.ToString();
                if (MessageBox.Show($"Bạn có chắc muốn xóa sách {id}?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection conn = DatabaseConnection.GetConnection())
                        {
                            conn.Open();
                            // Check ràng buộc
                            SqlCommand check = new SqlCommand("SELECT COUNT(*) FROM LOAN_RECORDS WHERE BookID=@id", conn);
                            check.Parameters.AddWithValue("@id", id);
                            if ((int)check.ExecuteScalar() > 0)
                            {
                                MessageBox.Show("Sách này đã từng được mượn, không thể xóa!"); return;
                            }
                            // Xóa
                            SqlCommand del = new SqlCommand("DELETE FROM BOOKS WHERE BookID=@id", conn);
                            del.Parameters.AddWithValue("@id", id);
                            del.ExecuteNonQuery();
                            Logger.Log("Xóa Sách", $"Đã xóa vĩnh viễn sách có ID: {id}");
                            MessageBox.Show("Đã xóa!");
                            LoadBookData(txtSearchBook.Text.Trim());
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        // Số 547 là mã lỗi của SQL Server khi vi phạm khóa ngoại (Foreign Key)
                        if (sqlEx.Number == 547)
                        {
                            MessageBox.Show("Không thể xóa độc giả này vì họ đang có lịch sử mượn trả sách!\nHãy xóa dữ liệu mượn trả trước.", "Ràng buộc dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        else
                        {
                            MessageBox.Show("Lỗi SQL: " + sqlEx.Message);
                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }
            else MessageBox.Show("Vui lòng chọn sách cần xóa!");
        }

        private void btnSearch_Click(object sender, EventArgs e) => LoadBookData(txtSearchBook.Text.Trim());

        private void btnReload_Click(object sender, EventArgs e)
        {
            txtSearchBook.Clear();
            cboFilterCategory.SelectedIndex = 0;
            cboSortOrder.SelectedIndex = 0;
            LoadBookData("");
        }

        private void cboFilterCategory_SelectedIndexChanged(object sender, EventArgs e) => LoadBookData(txtSearchBook.Text.Trim());
        private void cboSortOrder_SelectedIndexChanged(object sender, EventArgs e) => LoadBookData(txtSearchBook.Text.Trim());
    }
}