-- 1. Tạo Database
CREATE DATABASE ElibseDB;
GO

USE ElibseDB;
GO

-- 2. Tạo bảng ADMINS (Quản trị viên)
-- Dù bạn chọn phương án đơn giản, ta vẫn cần bảng này để lưu mật khẩu.
-- Sau này nếu muốn mở rộng nhiều Admin thì chỉ cần thêm dòng vào đây.
CREATE TABLE ADMINS (
    AdminID INT IDENTITY(1,1) PRIMARY KEY, -- Mã tự tăng
    Username VARCHAR(50) NOT NULL UNIQUE,  -- Tên đăng nhập (Mặc định sẽ dùng 'admin')
    Password VARCHAR(100) NOT NULL,        -- Mật khẩu đăng nhập
    FullName NVARCHAR(100)                 -- Tên hiển thị (VD: Quản Trị Viên)
);
GO

-- 3. Tạo bảng CATEGORIES (Thể loại sách)
-- Dùng cho ComboBox chọn thể loại
CREATE TABLE CATEGORIES (
    CategoryID INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL UNIQUE -- Tên thể loại (VD: Kinh tế, IT...)
);
GO

-- 4. Tạo bảng BOOKS (Kho sách)
-- Mỗi dòng trong bảng này là một cuốn sách vật lý có mã định danh riêng
CREATE TABLE BOOKS (
    BookID VARCHAR(50) PRIMARY KEY,        -- Mã sách dạng chuỗi (#0000001-TENSACH-0001)
    Title NVARCHAR(200) NOT NULL,          -- Tên sách
    Author NVARCHAR(100),                  -- Tác giả
    CategoryID INT,                        -- Liên kết sang bảng CATEGORIES
    ImportDate DATETIME DEFAULT GETDATE(), -- Ngày nhập sách
    Price DECIMAL(18, 0),                  -- Giá tiền (Dùng để tính phạt đền bù)
    BookImage VARBINARY(MAX),              -- Lưu ảnh bìa sách trực tiếp vào DB
    Status NVARCHAR(50) DEFAULT N'Sẵn sàng', -- Trạng thái: Sẵn sàng, Đang mượn, Hỏng, Mất
    
    -- Tạo khóa ngoại liên kết với bảng Categories
    FOREIGN KEY (CategoryID) REFERENCES CATEGORIES(CategoryID)
);
GO

-- 5. Tạo bảng READERS (Độc giả)
CREATE TABLE READERS (
    ReaderID VARCHAR(50) PRIMARY KEY,      -- Mã độc giả dạng chuỗi (#0000001-12302025)
    FullName NVARCHAR(100) NOT NULL,       -- Họ tên
    DOB DATETIME,                          -- Ngày sinh (để tính tuổi)
    Email VARCHAR(100),                    -- Email liên hệ
    PhoneNumber VARCHAR(20),               -- Số điện thoại
    CreatedDate DATETIME DEFAULT GETDATE(),-- Ngày tạo tài khoản
    ReaderImage VARBINARY(MAX),            -- Ảnh thẻ độc giả
    Password VARCHAR(100),                 -- Mật khẩu đăng nhập của độc giả
    Status NVARCHAR(50) DEFAULT N'Active'  -- Trạng thái: Active (Hoạt động), Locked (Bị khóa)
);
GO

-- 6. Tạo bảng LOAN_RECORDS (Hồ sơ Mượn/Trả)
-- Bảng này quan trọng nhất, lưu lịch sử ai mượn sách gì
CREATE TABLE LOAN_RECORDS (
    RecordID INT IDENTITY(1,1) PRIMARY KEY, -- Mã giao dịch tự tăng
    ReaderID VARCHAR(50) NOT NULL,          -- Ai mượn?
    BookID VARCHAR(50) NOT NULL,            -- Mượn cuốn nào?
    BorrowDate DATETIME DEFAULT GETDATE(),  -- Ngày mượn
    DueDate DATETIME NOT NULL,              -- Hạn phải trả
    ReturnDate DATETIME NULL,               -- Ngày trả thực tế (NULL = Chưa trả)
    ReturnStatus NVARCHAR(100),             -- Tình trạng khi trả (Bình thường, Hư hỏng...)
    FineAmount DECIMAL(18, 0) DEFAULT 0,    -- Số tiền phạt phát sinh
    IsPaid BIT DEFAULT 0,                   -- Trạng thái nộp phạt (0: Chưa, 1: Rồi)

    -- Khóa ngoại
    FOREIGN KEY (ReaderID) REFERENCES READERS(ReaderID),
    FOREIGN KEY (BookID) REFERENCES BOOKS(BookID)
);
GO