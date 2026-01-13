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
                            picBookCover.Image = Image.FromStream(ms);
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

                        string abbreviation = GetAbbreviation(txtTitle.Text);

                        for (int i = 0; i < quantity; i++)
                        {
                            long nextGlobal = GetNextGlobalNumber(conn);
                            int nextLocal = GetNextLocalNumber(conn, abbreviation);

                            // Tạo ID: #0000001-DNT-0001
                            string finalID = $"#{nextGlobal:D7}-{abbreviation}-{nextLocal:D4}";

                            string insertQ = @"INSERT INTO BOOKS (BookID, Title, Author, CategoryID, ImportDate, Price, BookImage, Status)
                                               VALUES (@id, @title, @author, @catID, GETDATE(), @price, @img, N'Available')";

                            SqlCommand cmd = new SqlCommand(insertQ, conn);
                            cmd.Parameters.AddWithValue("@id", finalID);
                            cmd.Parameters.AddWithValue("@title", txtTitle.Text.Trim());
                            cmd.Parameters.AddWithValue("@author", txtAuthor.Text.Trim());
                            cmd.Parameters.AddWithValue("@catID", cboCategory.SelectedValue);
                            cmd.Parameters.AddWithValue("@price", price);

                            if (imageBytes != null) cmd.Parameters.AddWithValue("@img", imageBytes);
                            else cmd.Parameters.Add("@img", SqlDbType.VarBinary).Value = DBNull.Value;

                            cmd.ExecuteNonQuery();
                        }
                        Logger.Log("Thêm Sách", $"Thêm mới {quantity} cuốn: {txtTitle.Text} (Tác giả: {txtAuthor.Text})");

                        MessageBox.Show($"Đã thêm thành công {quantity} quyển sách!");
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

        private long GetNextGlobalNumber(SqlConnection conn)
        {
            string sql = "SELECT ISNULL(MAX(CAST(SUBSTRING(BookID, 2, 7) AS INT)), 0) FROM BOOKS";

            SqlCommand cmd = new SqlCommand(sql, conn);

            // Lấy số lớn nhất tìm được cộng thêm 1 để ra số mới
            return (int)cmd.ExecuteScalar() + 1;
        }

        private int GetNextLocalNumber(SqlConnection conn, string abbr)
        {
            SqlCommand cmd = new SqlCommand("SELECT BookID FROM BOOKS WHERE BookID LIKE @p", conn);
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

        // --- HÀM MỚI: Tìm ID danh mục dựa vào tên (Nếu chưa có thì tự tạo mới) ---
        private int GetCategoryIDByName(SqlConnection conn, string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName)) categoryName = "Chưa phân loại";

            // 1. Tìm xem danh mục đã có chưa
            string sqlFind = "SELECT CategoryID FROM CATEGORIES WHERE CategoryName = @name";
            using (SqlCommand cmd = new SqlCommand(sqlFind, conn, conn.BeginTransaction())) // Lưu ý transaction nếu cần, ở đây mình dùng context của conn ngoài
            {
                // Lưu ý: Code dưới dùng cmd riêng nên không cần transaction nếu gọi từ btnImport
                // Sửa lại đơn giản để tránh lỗi transaction lồng nhau:
                cmd.Transaction = null;
            }

            // Viết lại đơn giản hơn để khớp với logic import bên dưới:
            using (SqlCommand cmd = new SqlCommand("SELECT CategoryID FROM CATEGORIES WHERE CategoryName = @name", conn))
            {
                cmd.Parameters.AddWithValue("@name", categoryName);
                object result = cmd.ExecuteScalar();

                if (result != null) return Convert.ToInt32(result);
            }

            // 2. Nếu chưa có thì INSERT mới và lấy ID về ngay lập tức
            string sqlInsert = "INSERT INTO CATEGORIES (CategoryName) OUTPUT INSERTED.CategoryID VALUES (@name)";
            using (SqlCommand cmd = new SqlCommand(sqlInsert, conn))
            {
                cmd.Parameters.AddWithValue("@name", categoryName);
                return (int)cmd.ExecuteScalar();
            }
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Excel Workbook|*.xlsx|Excel 97-2003 Workbook|*.xls|CSV File|*.csv", ValidateNames = true })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var stream = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read))
                        {
                            // 1. Cấu hình đọc Excel/CSV
                            IExcelDataReader reader;
                            if (Path.GetExtension(ofd.FileName).Equals(".csv", StringComparison.OrdinalIgnoreCase))
                                reader = ExcelReaderFactory.CreateCsvReader(stream);
                            else
                                reader = ExcelReaderFactory.CreateReader(stream);

                            var conf = new ExcelDataSetConfiguration
                            {
                                ConfigureDataTable = _ => new ExcelDataTableConfiguration { UseHeaderRow = true }
                            };

                            DataSet result = reader.AsDataSet(conf);
                            DataTable dt = result.Tables[0];
                            reader.Close();

                            // 2. Bắt đầu xử lý import
                            int totalAdded = 0;
                            int errorCount = 0;

                            using (SqlConnection conn = DatabaseConnection.GetConnection())
                            {
                                conn.Open();

                                foreach (DataRow row in dt.Rows)
                                {
                                    try
                                    {
                                        // --- Lấy dữ liệu từ Excel ---
                                        string tenSach = row["TenSach"].ToString().Trim();
                                        if (string.IsNullOrEmpty(tenSach)) continue; // Bỏ qua dòng trống

                                        string tacGia = row["TacGia"].ToString().Trim();
                                        string tenTheLoai = row["TheLoai"].ToString().Trim();

                                        decimal gia = 0;
                                        decimal.TryParse(row["Gia"].ToString(), out gia);

                                        int soLuong = 1;
                                        int.TryParse(row["SoLuong"].ToString(), out soLuong);
                                        if (soLuong < 1) soLuong = 1;

                                        // --- Gọi các hàm Helper CÓ SẴN trong AddBook.cs ---

                                        // 1. Xử lý Category (Hàm mới thêm ở Bước 2)
                                        int categoryId = GetCategoryIDByName(conn, tenTheLoai);

                                        // 2. Tạo mã viết tắt (Hàm cũ đã có)
                                        string abbreviation = GetAbbreviation(tenSach);

                                        // 3. Vòng lặp Insert theo số lượng
                                        for (int i = 0; i < soLuong; i++)
                                        {
                                            // Gọi hàm sinh mã (Hàm cũ đã có)
                                            long nextGlobal = GetNextGlobalNumber(conn);
                                            int nextLocal = GetNextLocalNumber(conn, abbreviation);

                                            string finalID = $"#{nextGlobal:D7}-{abbreviation}-{nextLocal:D4}";

                                            // Insert
                                            string query = @"INSERT INTO BOOKS (BookID, Title, Author, CategoryID, Price, ImportDate, Status) 
                                                     VALUES (@id, @title, @author, @catID, @price, GETDATE(), N'Available')";

                                            using (SqlCommand cmd = new SqlCommand(query, conn))
                                            {
                                                cmd.Parameters.AddWithValue("@id", finalID);
                                                cmd.Parameters.AddWithValue("@title", tenSach);
                                                cmd.Parameters.AddWithValue("@author", tacGia);
                                                cmd.Parameters.AddWithValue("@catID", categoryId);
                                                cmd.Parameters.AddWithValue("@price", gia);

                                                cmd.ExecuteNonQuery();
                                            }
                                            totalAdded++;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        errorCount++;
                                        Console.WriteLine($"Lỗi dòng excel: {ex.Message}");
                                    }
                                }
                            }

                            // 3. Thông báo kết quả
                            MessageBox.Show($"Hoàn tất!\n- Thêm thành công: {totalAdded} cuốn.\n- Lỗi/Bỏ qua: {errorCount} dòng.",
                                            "Kết quả Import", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Sau khi import xong, bạn có thể muốn đóng form AddBook luôn để quay về màn hình chính xem danh sách
                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi đọc file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}