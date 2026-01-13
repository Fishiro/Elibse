-- =============================================
-- DATABASE: ELIBSE (FULL VERSION - UPDATED PENALTY CONFIG)
-- Date: 13/01/2026
-- =============================================

-- 1. KHỞI TẠO DATABASE
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
        Password VARCHAR(100) NOT NULL,
        FullName NVARCHAR(100)
    );
END
GO

-- [DATA INIT] Admin mặc định
IF NOT EXISTS (SELECT * FROM ADMINS WHERE Username = 'admin')
BEGIN
    INSERT INTO ADMINS (Username, Password, FullName) VALUES ('admin', '', N'Quản trị viên');
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
END
GO

-- [DATA INIT] Danh mục mặc định
IF NOT EXISTS (SELECT TOP 1 1 FROM CATEGORIES)
BEGIN
    INSERT INTO CATEGORIES (CategoryName) VALUES 
    (N'Công nghệ thông tin'), (N'Kinh tế - Tài chính'), (N'Văn học - Tiểu thuyết'),
    (N'Truyện tranh - Manga'), (N'Kỹ năng sống'), (N'Sách giáo khoa'),
    (N'Ngoại ngữ'), (N'Khoa học - Kỹ thuật');
END
GO

-- =============================================
-- 4. BẢNG BOOKS (Sách)
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BOOKS]') AND type in (N'U'))
BEGIN
    CREATE TABLE BOOKS (
        BookID VARCHAR(50) PRIMARY KEY,
        Title NVARCHAR(200) NOT NULL,
        Author NVARCHAR(100),
        CategoryID INT,
        Price DECIMAL(18, 0),
        ImportDate DATETIME DEFAULT GETDATE(),
        Status NVARCHAR(50) DEFAULT 'Available',
        BookImage VARBINARY(MAX),
        Description NVARCHAR(MAX),
        FOREIGN KEY (CategoryID) REFERENCES CATEGORIES(CategoryID)
    );
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
        Address NVARCHAR(500),
        CreatedDate DATETIME DEFAULT GETDATE(),
        Status NVARCHAR(50) DEFAULT 'Active',
        ReaderImage VARBINARY(MAX),
        Password VARCHAR(100) DEFAULT '123456'
    );
END
GO

-- =============================================
-- 6. BẢNG LOAN_RECORDS (Mượn trả)
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LOAN_RECORDS]') AND type in (N'U'))
BEGIN
    CREATE TABLE LOAN_RECORDS (
        LoanID INT IDENTITY(1,1) PRIMARY KEY,
        BookID VARCHAR(50),
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
-- 7. BẢNG SYSTEM_CONFIG (Cấu hình hệ thống - ĐÃ CẬP NHẬT)
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SYSTEM_CONFIG]') AND type in (N'U'))
BEGIN
    CREATE TABLE SYSTEM_CONFIG (
        ConfigID INT IDENTITY(1,1) PRIMARY KEY,
        
        -- Cấu hình Email
        EmailSender NVARCHAR(100) DEFAULT '',
        EmailPassword NVARCHAR(100) DEFAULT '',

        -- Cấu hình Phạt (MỚI)
        BaseFineFee DECIMAL(18, 0) DEFAULT 5000,   -- Phí phạt cơ bản
        FineCycleDays INT DEFAULT 1,               -- Chu kỳ tính (ngày)
        GracePeriodDays INT DEFAULT 0,             -- Số ngày ân hạn
        FineIncrement DECIMAL(18, 0) DEFAULT 0     -- Số tiền tăng thêm mỗi chu kỳ (lũy tiến)
    );

    -- Tạo dòng cấu hình mặc định (Chỉ chạy 1 lần khi tạo bảng)
    INSERT INTO SYSTEM_CONFIG (EmailSender, EmailPassword, BaseFineFee, FineCycleDays, GracePeriodDays, FineIncrement) 
    VALUES ('', '', 5000, 1, 0, 0);

    PRINT '>> Table SYSTEM_CONFIG created with Penalty Configs.';
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
        ActionType NVARCHAR(50),
        ActionDetails NVARCHAR(MAX),
        LogTime DATETIME DEFAULT GETDATE()
    );
END
GO

PRINT '=============================================';
PRINT '>> DATABASE ELIBSE (FULL) READY TO USE! <<';
PRINT '=============================================';