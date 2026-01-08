-- 1. Tạo Database
-- Lưu ý: Nếu Database đã tồn tại, bạn cần xóa cũ (DROP DATABASE ElibseDB) trước khi chạy lại từ đầu, 
-- hoặc chỉ chạy đoạn lệnh tạo bảng SYSTEM_CONFIG nếu dữ liệu cũ đang quan trọng.
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'ElibseDB')
BEGIN
    CREATE DATABASE ElibseDB;
END
GO

USE ElibseDB;
GO

-- 2. Tạo bảng ADMINS (Quản trị viên)
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

-- 3. Tạo bảng CATEGORIES (Thể loại sách)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CATEGORIES]') AND type in (N'U'))
BEGIN
    CREATE TABLE CATEGORIES (
        CategoryID INT IDENTITY(1,1) PRIMARY KEY,
        CategoryName NVARCHAR(100) NOT NULL UNIQUE 
    );
END
GO

-- 4. Tạo bảng BOOKS (Kho sách)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BOOKS]') AND type in (N'U'))
BEGIN
    CREATE TABLE BOOKS (
        BookID VARCHAR(50) PRIMARY KEY,        
        Title NVARCHAR(200) NOT NULL,          
        Author NVARCHAR(100),                  
        CategoryID INT,                        
        ImportDate DATETIME DEFAULT GETDATE(), 
        Price DECIMAL(18, 0),                  
        BookImage VARBINARY(MAX),              
        Status NVARCHAR(50) DEFAULT N'Sẵn sàng', 
        
        FOREIGN KEY (CategoryID) REFERENCES CATEGORIES(CategoryID)
    );
END
GO

-- 5. Tạo bảng READERS (Độc giả)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[READERS]') AND type in (N'U'))
BEGIN
    CREATE TABLE READERS (
        ReaderID VARCHAR(50) PRIMARY KEY,      
        FullName NVARCHAR(100) NOT NULL,       
        DOB DATETIME,                          
        Email VARCHAR(100),                    
        PhoneNumber VARCHAR(20),               
        CreatedDate DATETIME DEFAULT GETDATE(),
        ReaderImage VARBINARY(MAX),            
        Password VARCHAR(100),                 
        Status NVARCHAR(50) DEFAULT N'Active'  
    );
END
GO

-- 6. Tạo bảng LOAN_RECORDS (Hồ sơ Mượn/Trả)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LOAN_RECORDS]') AND type in (N'U'))
BEGIN
    CREATE TABLE LOAN_RECORDS (
        RecordID INT IDENTITY(1,1) PRIMARY KEY, 
        ReaderID VARCHAR(50) NOT NULL,          
        BookID VARCHAR(50) NOT NULL,            
        BorrowDate DATETIME DEFAULT GETDATE(),  
        DueDate DATETIME NOT NULL,              
        ReturnDate DATETIME NULL,               
        ReturnStatus NVARCHAR(100),             
        FineAmount DECIMAL(18, 0) DEFAULT 0,    
        IsPaid BIT DEFAULT 0,                   

        FOREIGN KEY (ReaderID) REFERENCES READERS(ReaderID),
        FOREIGN KEY (BookID) REFERENCES BOOKS(BookID)
    );
END
GO

-- 7. Tạo bảng SYSTEM_CONFIG (Cấu hình hệ thống)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SYSTEM_CONFIG]') AND type in (N'U'))
BEGIN
    CREATE TABLE SYSTEM_CONFIG (
        ConfigID INT PRIMARY KEY IDENTITY(1,1),
        EmailSender NVARCHAR(100) DEFAULT '',
        EmailPassword NVARCHAR(100) DEFAULT ''
    );

    INSERT INTO SYSTEM_CONFIG (EmailSender, EmailPassword) 
    VALUES ('', '');
END
GO

-- 8. [MỚI] Tạo bảng ADMIN_LOGS (Nhật ký hoạt động của Admin)
-- Dùng để lưu lại lịch sử: Ai làm gì? Vào lúc nào?
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ADMIN_LOGS]') AND type in (N'U'))
BEGIN
    CREATE TABLE ADMIN_LOGS (
        LogID INT IDENTITY(1,1) PRIMARY KEY,
        AdminUsername VARCHAR(50),            -- Ai thực hiện? (Lưu Username)
        ActionType NVARCHAR(50),              -- Loại hành động (Ví dụ: "Thêm Sách", "Đăng Nhập")
        ActionDetails NVARCHAR(MAX),          -- Chi tiết (Ví dụ: "Thêm sách Đắc Nhân Tâm...")
        LogTime DATETIME DEFAULT GETDATE()    -- Thời gian thực hiện
    );
END
GO