using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO; // Thư viện xử lý file
using System.Text;
using System.Text.RegularExpressions; // Thư viện Regex
using System.Windows.Forms;
using ExcelDataReader;

namespace Elibse.Admin
{
    public partial class AddBook : Form
    {
        // Biến lưu trạng thái: Nếu null => Thêm mới; Nếu có ID => Đang Sửa
        private string _bookIDToEdit = null;
        private string currentImagePath = "";
        private byte[] _currentImageBytes = null;

        public AddBook()
        {
            InitializeComponent();
        }

        // --- 1. SỰ KIỆN LOAD FORM ---
        private void AddBook_Load(object sender, EventArgs e)
        {
            LoadCategories();
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;

            // Nếu là chế độ Thêm mới thì hiển thị Preview mã
            if (_bookIDToEdit == null)
            {
                UpdatePreview();
            }
        }

        // --- HÀM KÍCH HOẠT CHẾ ĐỘ SỬA (Gọi từ TotalBook) ---
        public void EnableEditMode(string bookID)
        {
            _bookIDToEdit = bookID;
            this.Text = "Cập nhật thông tin sách";
            btnSave.Text = "Lưu thay đổi";

            // Khóa các ô không được sửa
            txtQuantity.Enabled = false;
            txtQuantity.Text = "1"; // Để tránh lỗi parse số lượng

            LoadDataForEdit();
        }

        // --- TẢI DỮ LIỆU CŨ LÊN ĐỂ SỬA ---
        private void LoadDataForEdit()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT Title, Author, CategoryID, Price, BookImage FROM BOOKS WHERE BookID = @id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", _bookIDToEdit);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtTitle.Text = reader["Title"].ToString();
                        txtAuthor.Text = reader["Author"].ToString();

                        if (reader["Price"] != DBNull.Value)
                            txtPrice.Text = Convert.ToInt32(reader["Price"]).ToString(); // Chuyển về số nguyên cho đẹp

                        cboCategory.SelectedValue = reader["CategoryID"];

                        // Load ảnh từ DB lên PictureBox
                        if (reader["BookImage"] != DBNull.Value)
                        {
                            byte[] imgData = (byte[])reader["BookImage"];
                            MemoryStream ms = new MemoryStream(imgData);
                            picBookCover.Image = new Bitmap(Image.FromStream(ms));
                            picBookCover.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi tải dữ liệu sửa: " + ex.Message); }
        }

        // --- 2. XỬ LÝ NÚT LƯU (QUAN TRỌNG NHẤT) ---
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate dữ liệu chung
            if (string.IsNullOrWhiteSpace(txtTitle.Text) ||
                string.IsNullOrWhiteSpace(txtAuthor.Text) ||
                string.IsNullOrWhiteSpace(txtPrice.Text) ||
                cboCategory.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin (Tên, Tác giả, Giá, Danh mục)!", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal price = 0;
            if (!decimal.TryParse(txtPrice.Text, out price))
            {
                MessageBox.Show("Giá tiền phải là số (không chứa chữ cái)!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (price < 0)
            {
                MessageBox.Show("Giá tiền không được nhỏ hơn 0!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xử lý ảnh (Chuyển file ảnh sang byte[])
            byte[] imageBytes = _currentImageBytes;

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    // === TRƯỜNG HỢP 1: THÊM MỚI ===
                    if (_bookIDToEdit == null)
                    {
                        int quantity = 0;
                        if (!int.TryParse(txtQuantity.Text, out quantity) || quantity <= 0)
                        {
                            MessageBox.Show("Số lượng phải là số dương!", "Lỗi nhập liệu");
                            return;
                        }

                        SqlTransaction transaction = conn.BeginTransaction();

                        try
                        {
                            string abbreviation = GetAbbreviation(txtTitle.Text);

                            for (int i = 0; i < quantity; i++)
                            {
                                long nextGlobal = GetNextGlobalNumber(conn);
                                int nextLocal = GetNextLocalNumber(conn, abbreviation);

                                string finalID = $"#{nextGlobal:D7}-{abbreviation}-{nextLocal:D4}";

                                string insertQ = @"INSERT INTO BOOKS (BookID, Title, Author, CategoryID, ImportDate, Price, BookImage, Status)
                               VALUES (@id, @title, @author, @catID, GETDATE(), @price, @img, N'Available')";

                                SqlCommand cmd = new SqlCommand(insertQ, conn, transaction);
                                cmd.Parameters.AddWithValue("@id", finalID);
                                cmd.Parameters.AddWithValue("@title", txtTitle.Text.Trim());
                                cmd.Parameters.AddWithValue("@author", txtAuthor.Text.Trim());
                                cmd.Parameters.AddWithValue("@catID", cboCategory.SelectedValue);
                                cmd.Parameters.AddWithValue("@price", price);

                                if (imageBytes != null)
                                    cmd.Parameters.Add("@img", SqlDbType.VarBinary, -1).Value = imageBytes;
                                else
                                    cmd.Parameters.Add("@img", SqlDbType.VarBinary).Value = DBNull.Value;

                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            Logger.Log("Thêm Sách", $"Thêm mới {quantity} cuốn: {txtTitle.Text} (Tác giả: {txtAuthor.Text})");
                            MessageBox.Show($"Đã thêm thành công {quantity} quyển sách!");
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                    // === TRƯỜNG HỢP 2: CẬP NHẬT (SỬA) ===
                    else
                    {
                        // Logic cập nhật: Nếu có ảnh mới thì update cả ảnh, không thì giữ nguyên ảnh cũ
                        string updateQ = @"UPDATE BOOKS 
                                           SET Title = @t, Author = @a, CategoryID = @c, Price = @p 
                                           WHERE BookID = @id";

                        if (imageBytes != null) // Nếu người dùng có chọn ảnh mới
                        {
                            updateQ = @"UPDATE BOOKS 
                                        SET Title = @t, Author = @a, CategoryID = @c, Price = @p, BookImage = @img 
                                        WHERE BookID = @id";
                        }

                        SqlCommand cmd = new SqlCommand(updateQ, conn);
                        cmd.Parameters.AddWithValue("@id", _bookIDToEdit);
                        cmd.Parameters.AddWithValue("@t", txtTitle.Text.Trim());
                        cmd.Parameters.AddWithValue("@a", txtAuthor.Text.Trim());
                        cmd.Parameters.AddWithValue("@c", cboCategory.SelectedValue);
                        cmd.Parameters.AddWithValue("@p", price);

                        if (imageBytes != null) cmd.Parameters.AddWithValue("@img", imageBytes);

                        cmd.ExecuteNonQuery();
                        Logger.Log("Sửa Sách", $"Cập nhật thông tin sách ID: {_bookIDToEdit} ({txtTitle.Text})");
                        MessageBox.Show("Cập nhật sách thành công!");
                    }
                }
                this.Close(); // Đóng form sau khi xong
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // --- CÁC HÀM HỖ TRỢ ---
        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    currentImagePath = ofd.FileName;

                    // 1. Đọc toàn bộ file ảnh thành mảng byte và lưu vào biến toàn cục
                    _currentImageBytes = File.ReadAllBytes(currentImagePath);

                    // 2. Tạo một MemoryStream từ mảng byte này để hiển thị lên PictureBox
                    using (MemoryStream ms = new MemoryStream(_currentImageBytes))
                    {
                        // Copy sang một bitmap mới để tránh lỗi GDI+ khi stream đóng
                        picBookCover.Image = new Bitmap(Image.FromStream(ms));
                    }

                    picBookCover.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải ảnh: " + ex.Message);
                }
            }
        }

        private void LoadCategories()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT CategoryID, CategoryName FROM CATEGORIES", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    cboCategory.DataSource = dt;
                    cboCategory.DisplayMember = "CategoryName";
                    cboCategory.ValueMember = "CategoryID";
                    cboCategory.SelectedIndex = -1;
                }
            }
            catch { }
        }

        private void UpdatePreview()
        {
            if (_bookIDToEdit != null) return; // Không preview khi sửa

            int qty = 1;
            int.TryParse(txtQuantity.Text, out qty);
            if (qty <= 0) qty = 1;

            string abbr = GetAbbreviation(txtTitle.Text);
            txtPreviewAbbr.Text = abbr;

            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    long startGlobal = GetNextGlobalNumber(conn);
                    long endGlobal = startGlobal + qty - 1;

                    if (qty > 1) txtPreviewGlobal.Text = $"#{startGlobal:D7} ➔ #{endGlobal:D7}";
                    else txtPreviewGlobal.Text = $"#{startGlobal:D7}";

                    if (!string.IsNullOrEmpty(abbr))
                    {
                        int startLocal = GetNextLocalNumber(conn, abbr);
                        int endLocal = startLocal + qty - 1;
                        if (qty > 1) txtPreviewLocal.Text = $"{startLocal:D4} ➔ {endLocal:D4}";
                        else txtPreviewLocal.Text = $"{startLocal:D4}";
                    }
                    else txtPreviewLocal.Text = "....";
                }
            }
            catch { }
        }

        private void txtTitle_TextChanged(object sender, EventArgs e) { UpdatePreview(); }
        private void txtQuantity_TextChanged(object sender, EventArgs e) { UpdatePreview(); }

        // Hàm sinh mã
        private string GetAbbreviation(string title)
        {
            if (string.IsNullOrEmpty(title)) return "";
            string[] words = title.Split(' ');
            string abbr = "";
            foreach (string word in words)
                if (!string.IsNullOrWhiteSpace(word)) abbr += word[0].ToString().ToUpper();
            return ConvertToUnSign(abbr);
        }

        private long GetNextGlobalNumber(SqlConnection conn, SqlTransaction transaction = null)
        {
            string sql = @"SELECT ISNULL(MAX(CAST(SUBSTRING(BookID, 2, 7) AS INT)), 0) 
                   FROM BOOKS WITH (UPDLOCK, HOLDLOCK)";

            SqlCommand cmd = new SqlCommand(sql, conn);
            if (transaction != null) cmd.Transaction = transaction;

            return (int)cmd.ExecuteScalar() + 1;
        }

        private int GetNextLocalNumber(SqlConnection conn, string abbr, SqlTransaction transaction = null)
        {
            SqlCommand cmd = new SqlCommand(@"SELECT BookID FROM BOOKS WITH (UPDLOCK, HOLDLOCK) 
                                      WHERE BookID LIKE @p", conn);
            if (transaction != null) cmd.Transaction = transaction;

            cmd.Parameters.AddWithValue("@p", $"%-{abbr}-%");
            SqlDataReader reader = cmd.ExecuteReader();
            int maxVal = 0;
            while (reader.Read())
            {
                string[] parts = reader["BookID"].ToString().Split('-');
                if (parts.Length >= 3 && int.TryParse(parts[parts.Length - 1], out int val))
                    if (val > maxVal) maxVal = val;
            }
            reader.Close();
            return maxVal + 1;
        }

        private string ConvertToUnSign(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
    }
}