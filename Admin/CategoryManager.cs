using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Elibse.Admin
{
    public partial class CategoryManager : Form
    {
        public CategoryManager()
        {
            InitializeComponent();
        }

        // --- 1. SỰ KIỆN LOAD FORM ---
        private void CategoryManager_Load(object sender, EventArgs e)
        {
            LoadCategoryData("");
            SetupDataGridView();
        }

        // --- 2. HÀM TẢI DỮ LIỆU ---
        private void LoadCategoryData(string keyword)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    // SỬA LẠI CÂU QUERY: Thêm ORDER BY CategoryID ASC
                    string query = "SELECT * FROM CATEGORIES WHERE CategoryName LIKE @kw ORDER BY CategoryID ASC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvCategories.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục: " + ex.Message);
            }
        }

        // --- 3. TRANG TRÍ BẢNG ---
        private void SetupDataGridView()
        {
            // --- ẨN CỘT ID ĐI CHO ĐẸP ---
            if (dgvCategories.Columns["CategoryID"] != null)
            {
                dgvCategories.Columns["CategoryID"].Visible = false; // Ẩn đi
            }

            if (dgvCategories.Columns["CategoryName"] != null)
            {
                dgvCategories.Columns["CategoryName"].HeaderText = "Tên Danh Mục";
            }

            dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategories.MultiSelect = false;
        }

        // --- 4. SỰ KIỆN KHI CLICK VÀO BẢNG (Để hiện lên ô sửa) ---
        private void dgvCategories_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Tránh click vào tiêu đề cột
            {
                DataGridViewRow row = dgvCategories.Rows[e.RowIndex];
                txtCategoryName.Text = row.Cells["CategoryName"].Value.ToString();

                // Lưu ID tạm vào Tag của ô text để dùng khi bấm nút Sửa/Xóa
                txtCategoryName.Tag = row.Cells["CategoryID"].Value.ToString();
            }
        }

        // --- 5. CÁC CHỨC NĂNG CRUD ---

        // A. Nút THÊM
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtCategoryName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Vui lòng nhập tên danh mục!", "Thông báo");
                return;
            }

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // Kiểm tra trùng tên
                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM CATEGORIES WHERE CategoryName = @name", conn);
                    checkCmd.Parameters.AddWithValue("@name", name);
                    if ((int)checkCmd.ExecuteScalar() > 0)
                    {
                        MessageBox.Show("Danh mục này đã tồn tại!", "Trùng lặp", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Thêm mới
                    string query = "INSERT INTO CATEGORIES (CategoryName) VALUES (@name)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Thêm thành công!");
                    LoadCategoryData("");
                    txtCategoryName.Clear();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        // B. Nút SỬA
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (txtCategoryName.Tag == null || string.IsNullOrEmpty(txtCategoryName.Text))
            {
                MessageBox.Show("Vui lòng chọn một danh mục từ bảng để sửa!", "Chưa chọn");
                return;
            }

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE CATEGORIES SET CategoryName = @name WHERE CategoryID = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", txtCategoryName.Text.Trim());
                    cmd.Parameters.AddWithValue("@id", txtCategoryName.Tag.ToString());
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Cập nhật thành công!");
                    LoadCategoryData("");
                    txtCategoryName.Clear();
                    txtCategoryName.Tag = null; // Reset ID tạm
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        // C. Nút XÓA
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtCategoryName.Tag == null)
            {
                MessageBox.Show("Vui lòng chọn danh mục cần xóa!", "Chưa chọn");
                return;
            }

            string id = txtCategoryName.Tag.ToString();

            if (MessageBox.Show("Bạn có chắc muốn xóa danh mục này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = DatabaseConnection.GetConnection())
                    {
                        conn.Open();

                        // ⚠️ QUAN TRỌNG: Kiểm tra xem có sách nào đang thuộc danh mục này không?
                        string checkQuery = "SELECT COUNT(*) FROM BOOKS WHERE CategoryID = @id";
                        SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                        checkCmd.Parameters.AddWithValue("@id", id);

                        if ((int)checkCmd.ExecuteScalar() > 0)
                        {
                            MessageBox.Show("Không thể xóa danh mục này vì đang có sách thuộc về nó!\nHãy xóa sách trước hoặc chuyển sách sang danh mục khác.", "Ràng buộc dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }

                        // Xóa
                        string delQuery = "DELETE FROM CATEGORIES WHERE CategoryID = @id";
                        SqlCommand cmd = new SqlCommand(delQuery, conn);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Đã xóa thành công!");
                        LoadCategoryData("");
                        txtCategoryName.Clear();
                        txtCategoryName.Tag = null;
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        // D. Nút TÌM KIẾM & TẢI LẠI
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadCategoryData(txtSearch.Text.Trim());
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            txtCategoryName.Clear();
            txtCategoryName.Tag = null;
            LoadCategoryData("");
        }
    }
}