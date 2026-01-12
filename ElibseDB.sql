-- =============================================
-- DATABASE: ELIBSE (FINAL VERSION - INTEGRATED)
-- Date: 12/01/2026
-- =============================================

-- 1. KHỞI TẠO DATABASE
-- Kiểm tra xem DB đã có chưa, chưa có thì tạo mới
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'ElibseDB')
BEGIN
    CREATE DATABASE ElibseDB;
    PRINT '>> Database ElibseDB created.';
END
GO

USE ElibseDB;
GO

-- =============================================
-- 2. BẢNG ADMINS (Quản trị viên)
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ADMINS]') AND type in (N'U'))
BEGIN
    CREATE TABLE ADMINS (
        AdminID INT IDENTITY(1,1) PRIMARY KEY,
        Username VARCHAR(50) NOT NULL UNIQUE,
        Password VARCHAR(100) NOT NULL, -- Mật khẩu (lưu plain text hoặc hash tùy logic)
        FullName NVARCHAR(100)
    );
    PRINT '>> Table ADMINS created.';
END
GO

-- [DATA INIT] Tạo tài khoản Admin mặc định nếu chưa có
IF NOT EXISTS (SELECT * FROM ADMINS WHERE Username = 'admin')
BEGIN
    -- Mật khẩu rỗng '' để kích hoạt tính năng "Thiết lập mật khẩu lần đầu" trong App
    INSERT INTO ADMINS (Username, Password, FullName) VALUES ('admin', '', N'Quản trị viên');
    PRINT '>> Default Admin created (Password is empty).';
END
GO

-- =============================================
-- 3. BẢNG CATEGORIES (Danh mục sách)
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CATEGORIES]') AND type in (N'U'))
BEGIN
    CREATE TABLE CATEGORIES (
        CategoryID INT IDENTITY(1,1) PRIMARY KEY,
        CategoryName NVARCHAR(100) NOT NULL UNIQUE
    );
    PRINT '>> Table CATEGORIES created.';
END
GO

-- [DATA INIT] Tự động thêm danh mục phổ biến nếu bảng đang trống
IF NOT EXISTS (SELECT TOP 1 1 FROM CATEGORIES)
BEGIN
    INSERT INTO CATEGORIES (CategoryName) VALUES 
    (N'Công nghệ thông tin'),
    (N'Kinh tế - Tài chính'),
    (N'Văn học - Tiểu thuyết'),
    (N'Truyện tranh - Manga'),
    (N'Kỹ năng sống'),
    (N'Sách giáo khoa'),
    (N'Ngoại ngữ'),
    (N'Khoa học - Kỹ thuật');
    PRINT '>> Default categories inserted.';
END
GO

-- =============================================
-- 4. BẢNG BOOKS (Sách)
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BOOKS]') AND type in (N'U'))
BEGIN
    CREATE TABLE BOOKS (
        BookID VARCHAR(50) PRIMARY KEY, -- Mã sách định dạng chuỗi (#000001-CNTT-001)
        Title NVARCHAR(200) NOT NULL,
        Author NVARCHAR(100),
        CategoryID INT,
        Price DECIMAL(18, 0),
        ImportDate DATETIME DEFAULT GETDATE(),
        Status NVARCHAR(50) DEFAULT 'Available', -- Available / Borrowed / Lost / Damaged
        BookImage VARBINARY(MAX), -- Lưu ảnh bìa
        Description NVARCHAR(MAX),
        FOREIGN KEY (CategoryID) REFERENCES CATEGORIES(CategoryID)
    );
    PRINT '>> Table BOOKS created.';
END
ELSE
BEGIN
    -- Cập nhật cấu trúc nếu thiếu cột (Dành cho phiên bản cũ nâng cấp lên)
    IF COL_LENGTH('BOOKS', 'BookImage') IS NULL 
        ALTER TABLE BOOKS ADD BookImage VARBINARY(MAX);
    IF COL_LENGTH('BOOKS', 'Description') IS NULL 
        ALTER TABLE BOOKS ADD Description NVARCHAR(MAX);
END
GO

-- =============================================
-- 5. BẢNG READERS (Độc giả)
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[READERS]') AND type in (N'U'))
BEGIN
    CREATE TABLE READERS (
        ReaderID VARCHAR(20) PRIMARY KEY,
        FullName NVARCHAR(100) NOT NULL,
        DOB DATETIME,
        PhoneNumber VARCHAR(20),
        Email VARCHAR(100),
        Address NVARCHAR(500), -- Địa chỉ 500 ký tự
        CreatedDate DATETIME DEFAULT GETDATE(),
        Status NVARCHAR(50) DEFAULT 'Active', -- Active / Locked
        ReaderImage VARBINARY(MAX),
        Password VARCHAR(100) DEFAULT '123456' -- Mật khẩu mặc định cho độc giả
    );
    PRINT '>> Table READERS created.';
END
ELSE
BEGIN
    -- Fix lỗi logic Address: Nếu chưa có thì thêm, nếu có rồi mà ngắn quá thì nới rộng
    IF COL_LENGTH('READERS', 'Address') IS NULL
        ALTER TABLE READERS ADD Address NVARCHAR(500);
    ELSE
        ALTER TABLE READERS ALTER COLUMN Address NVARCHAR(500);

    -- Bổ sung các cột mới nếu thiếu
    IF COL_LENGTH('READERS', 'ReaderImage') IS NULL ALTER TABLE READERS ADD ReaderImage VARBINARY(MAX);
    IF COL_LENGTH('READERS', 'Password') IS NULL ALTER TABLE READERS ADD Password VARCHAR(100) DEFAULT '123456';
    IF COL_LENGTH('READERS', 'CreatedDate') IS NULL ALTER TABLE READERS ADD CreatedDate DATETIME DEFAULT GETDATE();
END
GO

-- =============================================
-- 6. BẢNG LOAN_RECORDS (Mượn trả)
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LOAN_RECORDS]') AND type in (N'U'))
BEGIN
    CREATE TABLE LOAN_RECORDS (
        LoanID INT IDENTITY(1,1) PRIMARY KEY,
        BookID VARCHAR(20),
        ReaderID VARCHAR(20),
        LoanDate DATETIME DEFAULT GETDATE(), -- Ngày mượn
        DueDate DATETIME NOT NULL,           -- Hạn trả
        ReturnDate DATETIME NULL,            -- Ngày trả thực tế (NULL là chưa trả)
        ReturnStatus NVARCHAR(100),          -- Tình trạng khi trả (Bình thường/Hư hỏng)
        FineAmount DECIMAL(18, 0) DEFAULT 0, -- Tiền phạt
        IsPaid BIT DEFAULT 0,                -- Đã đóng phạt chưa
        FOREIGN KEY (BookID) REFERENCES BOOKS(BookID),
        FOREIGN KEY (ReaderID) REFERENCES READERS(ReaderID)
    );
    PRINT '>> Table LOAN_RECORDS created.';
END
GO

-- =============================================
-- 7. BẢNG SYSTEM_CONFIG (Cấu hình Email)
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SYSTEM_CONFIG]') AND type in (N'U'))
BEGIN
    CREATE TABLE SYSTEM_CONFIG (
        ConfigID INT IDENTITY(1,1) PRIMARY KEY,
        EmailSender NVARCHAR(100) DEFAULT '',
        EmailPassword NVARCHAR(100) DEFAULT ''
    );
    -- Tạo dòng cấu hình mặc định
    INSERT INTO SYSTEM_CONFIG (EmailSender, EmailPassword) VALUES ('', '');
    PRINT '>> Table SYSTEM_CONFIG created.';
END
GO

-- =============================================
-- 8. BẢNG ADMIN_LOGS (Nhật ký hoạt động)
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ADMIN_LOGS]') AND type in (N'U'))
BEGIN
    CREATE TABLE ADMIN_LOGS (
        LogID INT IDENTITY(1,1) PRIMARY KEY,
        AdminUsername VARCHAR(50),
        ActionType NVARCHAR(50),      -- Ví dụ: Đăng nhập, Thêm sách, Xóa độc giả
        ActionDetails NVARCHAR(MAX),  -- Chi tiết hành động
        LogTime DATETIME DEFAULT GETDATE()
    );
    PRINT '>> Table ADMIN_LOGS created.';
END
ELSE
BEGIN
    -- Đổi tên cột ActionDate thành LogTime nếu lỡ đặt sai trong phiên bản cũ
    IF COL_LENGTH('ADMIN_LOGS', 'ActionDate') IS NOT NULL
        EXEC sp_rename 'ADMIN_LOGS.ActionDate', 'LogTime', 'COLUMN';
END
GO

PRINT '=============================================';
PRINT '>> DATABASE ELIBSE READY TO USE! <<';
PRINT '=============================================';