-- =============================================================
-- MOCK DATA SCRIPT - DỰ ÁN ELIBSE (FULL DEMO VERSION - SYNCHRONIZED)
-- Ngày cập nhật: 14/01/2026
-- Pass mặc định cho mọi User: 123456
-- =============================================================

USE ElibseDB;
GO

-- 1. DỌN DẸP DỮ LIỆU CŨ (Để đảm bảo ID không bị trùng lặp khi chạy lại)
DELETE FROM LOAN_RECORDS;
DELETE FROM BOOKS;
DELETE FROM READERS;
DELETE FROM CATEGORIES;
-- Reset lại bộ đếm tự tăng (Identity) nếu có
DBCC CHECKIDENT ('CATEGORIES', RESEED, 0);
DBCC CHECKIDENT ('LOAN_RECORDS', RESEED, 0);

PRINT N'>> BẮT ĐẦU KHỞI TẠO DỮ LIỆU DEMO...';

-- =============================================
-- 2. TẠO DANH MỤC SÁCH (CATEGORIES)
-- =============================================
INSERT INTO CATEGORIES (CategoryName) VALUES (N'Công nghệ thông tin');
INSERT INTO CATEGORIES (CategoryName) VALUES (N'Kinh tế - Tài chính');
INSERT INTO CATEGORIES (CategoryName) VALUES (N'Văn học - Tiểu thuyết');
INSERT INTO CATEGORIES (CategoryName) VALUES (N'Truyện tranh - Manga');

-- Lấy ID danh mục tự động
DECLARE @CatID_CNTT INT = (SELECT TOP 1 CategoryID FROM CATEGORIES WHERE CategoryName = N'Công nghệ thông tin');
DECLARE @CatID_KT   INT = (SELECT TOP 1 CategoryID FROM CATEGORIES WHERE CategoryName = N'Kinh tế - Tài chính');
DECLARE @CatID_VH   INT = (SELECT TOP 1 CategoryID FROM CATEGORIES WHERE CategoryName = N'Văn học - Tiểu thuyết');
DECLARE @CatID_TT   INT = (SELECT TOP 1 CategoryID FROM CATEGORIES WHERE CategoryName = N'Truyện tranh - Manga');

-- =============================================
-- 3. TẠO DỮ LIỆU SÁCH (BOOKS)
-- Chuẩn hóa theo format của AddBook.cs: #{Global:D7}-{Abbr}-{Local:D4}
-- =============================================
PRINT N'... Đang thêm sách (Chuẩn hóa ID) ...';

-- Sách 1: Lập trình C# WinForm -> Abbr: LTCW
INSERT INTO BOOKS (BookID, Title, Author, CategoryID, Price, ImportDate, Status, Description) 
VALUES ('#0000001-LTCW-0001', N'Lập trình C# WinForm', N'Nguyễn Văn Code', @CatID_CNTT, 150000, GETDATE(), 'Borrowed', N'Giáo trình cơ bản cho người mới bắt đầu.');

-- Sách 2: SQL Server Toàn tập -> Abbr: SSTT
INSERT INTO BOOKS (BookID, Title, Author, CategoryID, Price, ImportDate, Status, Description) 
VALUES ('#0000002-SSTT-0001', N'SQL Server Toàn tập', N'Trần Database', @CatID_CNTT, 120000, DATEADD(month, -2, GETDATE()), 'Available', N'Sách chuyên sâu về truy vấn dữ liệu.');

-- Sách 3: Clean Code: Mã sạch -> Abbr: CCMS
INSERT INTO BOOKS (BookID, Title, Author, CategoryID, Price, ImportDate, Status, Description) 
VALUES ('#0000003-CCMS-0001', N'Clean Code: Mã sạch', N'Robert C. Martin', @CatID_CNTT, 350000, GETDATE(), 'Borrowed', N'Cuốn sách gối đầu giường của lập trình viên.');

-- Sách 4: Doraemon Tập 1 -> Abbr: DT1
INSERT INTO BOOKS (BookID, Title, Author, CategoryID, Price, ImportDate, Status, Description) 
VALUES ('#0000004-DT1-0001', N'Doraemon Tập 1', N'Fujiko F. Fujio', @CatID_TT, 25000, GETDATE(), 'Available', N'Chú mèo máy đến từ tương lai.');

-- Sách 5: Doraemon Tập 2 -> Abbr: DT2
INSERT INTO BOOKS (BookID, Title, Author, CategoryID, Price, ImportDate, Status, Description) 
VALUES ('#0000005-DT2-0001', N'Doraemon Tập 2', N'Fujiko F. Fujio', @CatID_TT, 25000, GETDATE(), 'Available', N'Nobita và những người bạn.');

-- Sách 6: Nhà Giả Kim -> Abbr: NGK
INSERT INTO BOOKS (BookID, Title, Author, CategoryID, Price, ImportDate, Status, Description) 
VALUES ('#0000006-NGK-0001', N'Nhà Giả Kim', N'Paulo Coelho', @CatID_VH, 80000, DATEADD(year, -1, GETDATE()), 'Lost', N'Sách bán chạy nhất mọi thời đại.');

-- =============================================
-- 4. TẠO DỮ LIỆU ĐỘC GIẢ (READERS)
-- Pass: 123456 (Hash SHA256 + Salt chuẩn)
-- =============================================
PRINT N'... Đang thêm độc giả ...';

DECLARE @DefaultPassHash VARCHAR(100) = '398DE8522A0CF03BD64C48026A2E1EF675A59EEA2373F6E73D789339FF56AA51';

-- 1. Độc giả VIP (Nguyễn Dư Quí)
INSERT INTO READERS (ReaderID, FullName, DOB, PhoneNumber, Email, Address, CreatedDate, Status, Password)
VALUES ('RD00001', N'Nguyễn Dư Quí', '2006-01-01', '0942655776', 'phamd4869@gmail.com', N'Cà Mau', GETDATE(), 'Active', @DefaultPassHash);

-- 2. Độc giả QUÁ HẠN (Trần Thị Quá Hạn)
INSERT INTO READERS (ReaderID, FullName, DOB, PhoneNumber, Email, Address, CreatedDate, Status, Password)
VALUES ('RD00002', N'Trần Thị Quá Hạn', '1999-05-05', '0902222222', 'peanutbutter262626@outlook.com', N'TP.HCM', GETDATE(), 'Active', @DefaultPassHash);

-- 3. Độc giả NỢ TIỀN (Lê Văn Nợ)
INSERT INTO READERS (ReaderID, FullName, DOB, PhoneNumber, Email, Address, CreatedDate, Status, Password)
VALUES ('RD00003', N'Lê Văn Nợ', '2006-10-12', '0903333333', 'impeanut2266@gmail.com', N'Đà Nẵng', GETDATE(), 'Active', @DefaultPassHash);

-- 4. Độc giả BỊ KHÓA (Phạm Vi Phạm)
INSERT INTO READERS (ReaderID, FullName, DOB, PhoneNumber, Email, Address, CreatedDate, Status, Password)
VALUES ('RD00004', N'Phạm Vi Phạm', '2000-01-01', '0904444444', 'blocked@example.com', N'Hà Nội', GETDATE(), 'Locked', @DefaultPassHash);

-- =============================================
-- 5. TẠO KỊCH BẢN MƯỢN TRẢ (LOAN_RECORDS)
-- Cập nhật BookID theo mã mới
-- =============================================
PRINT N'... Đang tạo kịch bản giao dịch ...';

-- CASE 1: ĐANG MƯỢN BÌNH THƯỜNG (Chưa đến hạn)
-- Độc giả RD00001 mượn sách #0000001-LTCW-0001 (C#)
INSERT INTO LOAN_RECORDS (BookID, ReaderID, LoanDate, DueDate, ReturnDate, ReturnStatus, FineAmount, IsPaid)
VALUES ('#0000001-LTCW-0001', 'RD00001', GETDATE(), DATEADD(day, 7, GETDATE()), NULL, NULL, 0, 0);

-- CASE 2: ĐANG MƯỢN QUÁ HẠN (Test Email)
-- Độc giả RD00002 mượn sách #0000003-CCMS-0001 (Clean Code)
-- Mượn từ 20 ngày trước -> Trễ 13 ngày
INSERT INTO LOAN_RECORDS (BookID, ReaderID, LoanDate, DueDate, ReturnDate, ReturnStatus, FineAmount, IsPaid)
VALUES ('#0000003-CCMS-0001', 'RD00002', DATEADD(day, -20, GETDATE()), DATEADD(day, -13, GETDATE()), NULL, NULL, 0, 0);

-- CASE 3: ĐÃ TRẢ NHƯNG CHƯA ĐÓNG PHẠT (Test chặn mượn)
-- Độc giả RD00003 (Lê Văn Nợ) đã trả sách #0000002-SSTT-0001 (SQL) nhưng nợ 50k
INSERT INTO LOAN_RECORDS (BookID, ReaderID, LoanDate, DueDate, ReturnDate, ReturnStatus, FineAmount, IsPaid)
VALUES ('#0000002-SSTT-0001', 'RD00003', DATEADD(day, -30, GETDATE()), DATEADD(day, -23, GETDATE()), DATEADD(day, -5, GETDATE()), N'Quá hạn', 50000, 0);

-- CASE 4: SÁCH BỊ MẤT (Lost)
-- Độc giả RD00001 làm mất sách #0000006-NGK-0001 (Nhà Giả Kim)
INSERT INTO LOAN_RECORDS (BookID, ReaderID, LoanDate, DueDate, ReturnDate, ReturnStatus, FineAmount, IsPaid)
VALUES ('#0000006-NGK-0001', 'RD00001', DATEADD(month, -1, GETDATE()), DATEADD(day, 7, DATEADD(month, -1, GETDATE())), GETDATE(), N'Mất', 80000, 1);

-- =============================================
-- 6. TỔNG KẾT
-- =============================================
PRINT N'==========================================================';
PRINT N'>> ĐÃ TẠO DỮ LIỆU TEST THÀNH CÔNG (SYNCHRONIZED)!';
PRINT N'1. RD00001 (Pass: 123456): Đang mượn C# (Đúng hạn).';
PRINT N'2. RD00002 (Pass: 123456): Đang mượn Clean Code (QUÁ HẠN).';
PRINT N'3. RD00003 (Pass: 123456): Đã trả SQL Server nhưng NỢ 50k.';
PRINT N'4. Sách Nhà Giả Kim: Trạng thái LOST.';
PRINT N'==========================================================';