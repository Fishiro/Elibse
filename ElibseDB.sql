-- =============================================
-- DATABASE: ELIBSE (Phiên bản Final v2 - Fix Lỗi Address)
-- Updated by: Quí & QA Lead
-- =============================================

-- 1. TẠO DATABASE
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'ElibseDB')
BEGIN
    CREATE DATABASE ElibseDB;
    PRINT '>> Database ElibseDB created.';
END
GO

USE ElibseDB;
GO

-- =============================================
-- 2. BẢNG ADMINS
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ADMINS]') AND type in (N'U'))
BEGIN
    CREATE TABLE ADMINS (
        AdminID INT IDENTITY(1,1) PRIMARY KEY,
        Username VARCHAR(50) NOT NULL UNIQUE,
        Password VARCHAR(100) NOT NULL,
        FullName NVARCHAR(100)
    );
    INSERT INTO ADMINS (Username, Password, FullName) VALUES ('admin', 'admin', N'Quản trị viên');
END
GO

-- =============================================
-- 3. BẢNG CATEGORIES
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CATEGORIES]') AND type in (N'U'))
BEGIN
    CREATE TABLE CATEGORIES (
        CategoryID INT IDENTITY(1,1) PRIMARY KEY,
        CategoryName NVARCHAR(100) NOT NULL UNIQUE
    );
END
GO

-- =============================================
-- 4. BẢNG BOOKS
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BOOKS]') AND type in (N'U'))
BEGIN
    CREATE TABLE BOOKS (
        BookID VARCHAR(20) PRIMARY KEY,
        Title NVARCHAR(200) NOT NULL,
        Author NVARCHAR(100),
        CategoryID INT,
        Price DECIMAL(18, 0),
        Quantity INT DEFAULT 0,
        ImportDate DATETIME DEFAULT GETDATE(),
        Status NVARCHAR(50) DEFAULT 'Available',
        BookImage VARBINARY(MAX),
        Description NVARCHAR(MAX),
        FOREIGN KEY (CategoryID) REFERENCES CATEGORIES(CategoryID)
    );
END
ELSE
BEGIN
    -- Cập nhật cột thiếu cho Books
    IF COL_LENGTH('BOOKS', 'BookImage') IS NULL ALTER TABLE BOOKS ADD BookImage VARBINARY(MAX);
    IF COL_LENGTH('BOOKS', 'Description') IS NULL ALTER TABLE BOOKS ADD Description NVARCHAR(MAX);
END
GO

-- =============================================
-- 5. BẢNG READERS (Đã Fix lỗi logic Address)
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[READERS]') AND type in (N'U'))
BEGIN
    CREATE TABLE READERS (
        ReaderID VARCHAR(20) PRIMARY KEY,
        FullName NVARCHAR(100) NOT NULL,
        DOB DATETIME,
        PhoneNumber VARCHAR(20),
        Email VARCHAR(100),
        Address NVARCHAR(500), -- Tạo mới 500 ký tự ngay từ đầu
        CreatedDate DATETIME DEFAULT GETDATE(),
        Status NVARCHAR(50) DEFAULT 'Active',
        ReaderImage VARBINARY(MAX),
        Password VARCHAR(100) DEFAULT '123456'
    );
END
ELSE
BEGIN
    -- Xử lý thông minh: Chưa có thì Thêm, Có rồi thì Sửa
    IF COL_LENGTH('READERS', 'Address') IS NULL
        ALTER TABLE READERS ADD Address NVARCHAR(500);
    ELSE
        ALTER TABLE READERS ALTER COLUMN Address NVARCHAR(500);

    -- Các cột khác
    IF COL_LENGTH('READERS', 'ReaderImage') IS NULL ALTER TABLE READERS ADD ReaderImage VARBINARY(MAX);
    IF COL_LENGTH('READERS', 'Password') IS NULL ALTER TABLE READERS ADD Password VARCHAR(100) DEFAULT '123456';
    IF COL_LENGTH('READERS', 'CreatedDate') IS NULL ALTER TABLE READERS ADD CreatedDate DATETIME DEFAULT GETDATE();
END
GO

-- =============================================
-- 6. BẢNG LOAN_RECORDS
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LOAN_RECORDS]') AND type in (N'U'))
BEGIN
    CREATE TABLE LOAN_RECORDS (
        LoanID INT IDENTITY(1,1) PRIMARY KEY,
        BookID VARCHAR(20),
        ReaderID VARCHAR(20),
        LoanDate DATETIME DEFAULT GETDATE(),
        DueDate DATETIME NOT NULL,
        ReturnDate DATETIME NULL,
        ReturnStatus NVARCHAR(100),
        FineAmount DECIMAL(18, 0) DEFAULT 0,
        IsPaid BIT DEFAULT 0,
        FOREIGN KEY (BookID) REFERENCES BOOKS(BookID),
        FOREIGN KEY (ReaderID) REFERENCES READERS(ReaderID)
    );
END
GO

-- =============================================
-- 7. BẢNG SYSTEM_CONFIG
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SYSTEM_CONFIG]') AND type in (N'U'))
BEGIN
    CREATE TABLE SYSTEM_CONFIG (
        ConfigID INT IDENTITY(1,1) PRIMARY KEY,
        EmailSender NVARCHAR(100) DEFAULT '',
        EmailPassword NVARCHAR(100) DEFAULT ''
    );
    INSERT INTO SYSTEM_CONFIG (EmailSender, EmailPassword) VALUES ('', '');
END
GO

-- =============================================
-- 8. BẢNG ADMIN_LOGS
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ADMIN_LOGS]') AND type in (N'U'))
BEGIN
    CREATE TABLE ADMIN_LOGS (
        LogID INT IDENTITY(1,1) PRIMARY KEY,
        AdminUsername VARCHAR(50),
        ActionType NVARCHAR(50),
        ActionDetails NVARCHAR(MAX),
        LogTime DATETIME DEFAULT GETDATE()
    );
END
ELSE
BEGIN
    -- Đổi tên cột ActionDate thành LogTime nếu lỡ đặt sai
    IF COL_LENGTH('ADMIN_LOGS', 'ActionDate') IS NOT NULL
        EXEC sp_rename 'ADMIN_LOGS.ActionDate', 'LogTime', 'COLUMN';
END
GO

PRINT '>> DATABASE ELIBSE ĐÃ SẴN SÀNG! (Final v2)';