-- =============================================================
-- MOCK DATA SCRIPT - DỰ ÁN ELIBSE (FULL DEMO VERSION)
-- Mục đích: Tạo dữ liệu giả để thuyết trình và test full chức năng
-- Bao gồm: Danh mục, Sách, Độc giả, Lịch sử mượn trả, Phạt
-- =============================================================

USE ElibseDB;
GO

-- 1. XÓA DỮ LIỆU CŨ (Nếu muốn làm sạch trước khi demo)
-- Bỏ comment các dòng dưới nếu bạn muốn Reset toàn bộ dữ liệu mỗi lần chạy
/*
DELETE FROM LOAN_RECORDS;
DELETE FROM BOOKS;
DELETE FROM READERS;
DELETE FROM CATEGORIES;
DBCC CHECKIDENT ('CATEGORIES', RESEED, 0);
DBCC CHECKIDENT ('LOAN_RECORDS', RESEED, 0);
*/

PRINT N'>> BẮT ĐẦU KHỞI TẠO DỮ LIỆU DEMO...';

-- =============================================
-- 2. TẠO DANH MỤC SÁCH (CATEGORIES)
-- =============================================
IF NOT EXISTS (SELECT * FROM CATEGORIES WHERE CategoryName = N'Công nghệ thông tin')
    INSERT INTO CATEGORIES (CategoryName) VALUES (N'Công nghệ thông tin');
IF NOT EXISTS (SELECT * FROM CATEGORIES WHERE CategoryName = N'Kinh tế - Tài chính')
    INSERT INTO CATEGORIES (CategoryName) VALUES (N'Kinh tế - Tài chính');
IF NOT EXISTS (SELECT * FROM CATEGORIES WHERE CategoryName = N'Văn học - Tiểu thuyết')
    INSERT INTO CATEGORIES (CategoryName) VALUES (N'Văn học - Tiểu thuyết');
IF NOT EXISTS (SELECT * FROM CATEGORIES WHERE CategoryName = N'Truyện tranh - Manga')
    INSERT INTO CATEGORIES (CategoryName) VALUES (N'Truyện tranh - Manga');

-- Lấy ID danh mục để dùng (tránh hardcode số)
DECLARE @CatID_CNTT INT = (SELECT TOP 1 CategoryID FROM CATEGORIES WHERE CategoryName = N'Công nghệ thông tin');
DECLARE @CatID_KT   INT = (SELECT TOP 1 CategoryID FROM CATEGORIES WHERE CategoryName = N'Kinh tế - Tài chính');
DECLARE @CatID_VH   INT = (SELECT TOP 1 CategoryID FROM CATEGORIES WHERE CategoryName = N'Văn học - Tiểu thuyết');
DECLARE @CatID_TT   INT = (SELECT TOP 1 CategoryID FROM CATEGORIES WHERE CategoryName = N'Truyện tranh - Manga');

-- =============================================
-- 3. TẠO DỮ LIỆU SÁCH (BOOKS)
-- Kết hợp cả mã ngắn (B001) và mã vạch dài (#000...) để demo sự linh hoạt
-- =============================================
PRINT N'... Đang thêm sách ...';

-- Sách CNTT
IF NOT EXISTS (SELECT * FROM BOOKS WHERE BookID = 'B001')
    INSERT INTO BOOKS (BookID, Title, Author, CategoryID, Price, ImportDate, Status, Description) 
    VALUES ('B001', N'Lập trình C# WinForm', N'Nguyễn Văn Code', @CatID_CNTT, 150000, GETDATE(), 'Available', N'Giáo trình cơ bản cho người mới bắt đầu.');

IF NOT EXISTS (SELECT * FROM BOOKS WHERE BookID = 'B002')
    INSERT INTO BOOKS (BookID, Title, Author, CategoryID, Price, ImportDate, Status, Description) 
    VALUES ('B002', N'SQL Server Toàn tập', N'Trần Database', @CatID_CNTT, 120000, GETDATE(), 'Borrowed', N'Sách chuyên sâu về truy vấn dữ liệu.');

-- Sách Clean Code (Dùng mã dài kiểu thư viện lớn)
IF NOT EXISTS (SELECT * FROM BOOKS WHERE BookID = '#CC-001')
    INSERT INTO BOOKS (BookID, Title, Author, CategoryID, Price, ImportDate, Status, Description) 
    VALUES ('#CC-001', N'Clean Code: Mã sạch', N'Robert C. Martin', @CatID_CNTT, 350000, GETDATE(), 'Borrowed', N'Cuốn sách gối đầu giường của lập trình viên.');

-- Sách Truyện tranh (Số lượng nhiều để test List)
IF NOT EXISTS (SELECT * FROM BOOKS WHERE BookID = '#DRM-001')
    INSERT INTO BOOKS (BookID, Title, Author, CategoryID, Price, ImportDate, Status, Description) 
    VALUES ('#DRM-001', N'Doraemon Tập 1', N'Fujiko F. Fujio', @CatID_TT, 25000, GETDATE(), 'Available', N'Chú mèo máy đến từ tương lai.');

IF NOT EXISTS (SELECT * FROM BOOKS WHERE BookID = '#DRM-002')
    INSERT INTO BOOKS (BookID, Title, Author, CategoryID, Price, ImportDate, Status, Description) 
    VALUES ('#DRM-002', N'Doraemon Tập 2', N'Fujiko F. Fujio', @CatID_TT, 25000, GETDATE(), 'Available', N'Nobita và những người bạn.');

-- Sách Văn học
IF NOT EXISTS (SELECT * FROM BOOKS WHERE BookID = 'B005')
    INSERT INTO BOOKS (BookID, Title, Author, CategoryID, Price, ImportDate, Status, Description) 
    VALUES ('B005', N'Nhà Giả Kim', N'Paulo Coelho', @CatID_VH, 80000, GETDATE(), 'Lost', N'Sách bán chạy nhất mọi thời đại.');

-- =============================================
-- 4. TẠO DỮ LIỆU ĐỘC GIẢ (READERS)
-- =============================================
PRINT N'... Đang thêm độc giả ...';

-- 1. Độc giả VIP (Demo mượn trả suôn sẻ)
IF NOT EXISTS (SELECT * FROM READERS WHERE ReaderID = 'R001')
    INSERT INTO READERS (ReaderID, FullName, DOB, PhoneNumber, Email, Address, CreatedDate, Status)
    VALUES ('R001', N'Nguyễn Dư Quí', '2006-01-01', '0942655776', 'phamd4869@gmail.com', N'Cà Mau', GETDATE(), 'Active');

-- 2. Độc giả QUÁ HẠN (Demo tính năng gửi Email nhắc nhở)
IF NOT EXISTS (SELECT * FROM READERS WHERE ReaderID = 'R002')
    INSERT INTO READERS (ReaderID, FullName, DOB, PhoneNumber, Email, Address, CreatedDate, Status)
    VALUES ('R002', N'Trần Thị Quá Hạn', '1999-05-05', '0902222222', 'peanutbutter262626@outlook.com', N'TP.HCM', GETDATE(), 'Active');

-- 3. Độc giả NỢ TIỀN (Demo tính năng chặn mượn sách)
IF NOT EXISTS (SELECT * FROM READERS WHERE ReaderID = 'R003')
    INSERT INTO READERS (ReaderID, FullName, DOB, PhoneNumber, Email, Address, CreatedDate, Status)
    VALUES ('R003', N'Lê Văn Nợ', '2006-10-12', '0903333333', 'impeanut2266@gmail.com', N'Đà Nẵng', GETDATE(), 'Active');

-- 4. Độc giả BỊ KHÓA (Demo validation trạng thái)
IF NOT EXISTS (SELECT * FROM READERS WHERE ReaderID = 'R004')
    INSERT INTO READERS (ReaderID, FullName, DOB, PhoneNumber, Email, Address, CreatedDate, Status)
    VALUES ('R004', N'Phạm Vi Phạm', '2000-01-01', '0904444444', 'blocked@example.com', N'Hà Nội', GETDATE(), 'Locked');

-- =============================================
-- 5. TẠO KỊCH BẢN MƯỢN TRẢ (LOAN_RECORDS)
-- =============================================
PRINT N'... Đang tạo kịch bản giao dịch ...';

-- CASE 1: ĐANG MƯỢN BÌNH THƯỜNG (Chưa đến hạn)
-- Độc giả R001 mượn sách B001 (C#)
IF NOT EXISTS (SELECT * FROM LOAN_RECORDS WHERE ReaderID = 'R001' AND BookID = 'B001' AND ReturnDate IS NULL)
BEGIN
    INSERT INTO LOAN_RECORDS (BookID, ReaderID, LoanDate, DueDate, ReturnDate, ReturnStatus, FineAmount, IsPaid)
    VALUES ('B001', 'R001', GETDATE(), DATEADD(day, 7, GETDATE()), NULL, NULL, 0, 0);
    
    -- Cập nhật trạng thái sách thành Borrowed
    UPDATE BOOKS SET Status = 'Borrowed' WHERE BookID = 'B001';
END

-- CASE 2: ĐANG MƯỢN QUÁ HẠN (Để test Email Service)
-- Độc giả R002 mượn sách #CC-001 (Clean Code)
-- Mượn từ 20 ngày trước, hạn 7 ngày -> Đã trễ 13 ngày
IF NOT EXISTS (SELECT * FROM LOAN_RECORDS WHERE ReaderID = 'R002' AND BookID = '#CC-001' AND ReturnDate IS NULL)
BEGIN
    INSERT INTO LOAN_RECORDS (BookID, ReaderID, LoanDate, DueDate, ReturnDate, ReturnStatus, FineAmount, IsPaid)
    VALUES ('#CC-001', 'R002', DATEADD(day, -20, GETDATE()), DATEADD(day, -13, GETDATE()), NULL, NULL, 0, 0);
    
    -- Cập nhật trạng thái sách thành Borrowed
    UPDATE BOOKS SET Status = 'Borrowed' WHERE BookID = '#CC-001';
END

-- CASE 3: ĐÃ TRẢ NHƯNG CHƯA ĐÓNG PHẠT (Để test chặn mượn tiếp)
-- Độc giả R003 (Lê Văn Nợ) đã trả sách B002 nhưng còn nợ 50k
IF NOT EXISTS (SELECT * FROM LOAN_RECORDS WHERE ReaderID = 'R003' AND FineAmount > 0 AND IsPaid = 0)
BEGIN
    INSERT INTO LOAN_RECORDS (BookID, ReaderID, LoanDate, DueDate, ReturnDate, ReturnStatus, FineAmount, IsPaid)
    VALUES ('B002', 'R003', DATEADD(day, -30, GETDATE()), DATEADD(day, -23, GETDATE()), DATEADD(day, -5, GETDATE()), N'Quá hạn', 50000, 0);
    
    -- Sách đã trả nên trạng thái là Available (nhưng người dùng thì bị dính nợ)
    UPDATE BOOKS SET Status = 'Available' WHERE BookID = 'B002';
END

-- CASE 4: SÁCH BỊ MẤT (Lost)
-- Độc giả R001 báo mất sách B005 (Nhà Giả Kim) và đã đền tiền
IF NOT EXISTS (SELECT * FROM LOAN_RECORDS WHERE BookID = 'B005' AND ReturnStatus = N'Mất')
BEGIN
    INSERT INTO LOAN_RECORDS (BookID, ReaderID, LoanDate, DueDate, ReturnDate, ReturnStatus, FineAmount, IsPaid)
    VALUES ('B005', 'R001', DATEADD(month, -1, GETDATE()), DATEADD(day, 7, DATEADD(month, -1, GETDATE())), GETDATE(), N'Mất', 80000, 1);
    
    -- Cập nhật trạng thái sách vĩnh viễn là Lost (hoặc Liquidated)
    UPDATE BOOKS SET Status = 'Lost' WHERE BookID = 'B005';
END

-- =============================================
-- 6. TỔNG KẾT
-- =============================================
PRINT N'==========================================================';
PRINT N'>> ĐÃ TẠO DỮ LIỆU TEST THÀNH CÔNG!';
PRINT N'1. Sách B001 (C#): Đang được mượn bởi R001 (Đúng hạn).';
PRINT N'2. Sách #CC-001 (Clean Code): Đang được mượn bởi R002 (QUÁ HẠN -> Test gửi Mail).';
PRINT N'3. Độc giả R003 (Lê Văn Nợ): Đang nợ 50k -> Thử mượn sách mới sẽ bị chặn.';
PRINT N'4. Sách B005 (Nhà Giả Kim): Trạng thái LOST -> Không thể mượn.';
PRINT N'==========================================================';