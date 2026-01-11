USE ElibseDB;
GO

-- =======================================================
-- PHẦN 1: KIỂM TRA SỐ LƯỢNG DỮ LIỆU HIỆN CÓ
-- =======================================================
PRINT '>> --- THỐNG KÊ DỮ LIỆU HIỆN TẠI ---';
SELECT 'LOAN_RECORDS' AS TableName, COUNT(*) AS TotalRows FROM LOAN_RECORDS
UNION ALL
SELECT 'BOOKS', COUNT(*) FROM BOOKS
UNION ALL
SELECT 'READERS', COUNT(*) FROM READERS
UNION ALL
SELECT 'CATEGORIES', COUNT(*) FROM CATEGORIES
UNION ALL
SELECT 'ADMIN_LOGS', COUNT(*) FROM ADMIN_LOGS;
GO

-- =======================================================
-- PHẦN 2: XÓA SẠCH DỮ LIỆU (RESET HỆ THỐNG)
-- =======================================================
-- Cảnh báo: Chạy đoạn dưới này sẽ mất hết sách, độc giả, lịch sử mượn!
-- =======================================================

BEGIN TRANSACTION; -- Bắt đầu giao dịch an toàn

BEGIN TRY
    -- 1. Xóa bảng con trước (Mượn trả)
    DELETE FROM LOAN_RECORDS;
    PRINT '>> Đã xóa sạch: LOAN_RECORDS';

    -- 2. Xóa bảng Sách (Vì dính khóa ngoại với Category)
    DELETE FROM BOOKS;
    PRINT '>> Đã xóa sạch: BOOKS';

    -- 3. Xóa Độc giả
    DELETE FROM READERS;
    PRINT '>> Đã xóa sạch: READERS';

    -- 4. Xóa Danh mục
    DELETE FROM CATEGORIES;
    PRINT '>> Đã xóa sạch: CATEGORIES';

    -- 5. Xóa Nhật ký & Cấu hình (Tùy chọn)
    DELETE FROM ADMIN_LOGS;
    DELETE FROM SYSTEM_CONFIG;
    -- DELETE FROM ADMINS; -- Khoan xóa Admin vội, để lại đăng nhập

    -- =======================================================
    -- PHẦN 3: RESET BỘ ĐẾM TỰ TĂNG (ID về 1)
    -- =======================================================
    -- Những bảng dùng IDENTITY(1,1) cần reset lại về 0
    DBCC CHECKIDENT ('LOAN_RECORDS', RESEED, 0);
    DBCC CHECKIDENT ('CATEGORIES', RESEED, 0);
    DBCC CHECKIDENT ('ADMIN_LOGS', RESEED, 0);
    DBCC CHECKIDENT ('SYSTEM_CONFIG', RESEED, 0);
    -- DBCC CHECKIDENT ('ADMINS', RESEED, 0); 

    -- =======================================================
    -- PHẦN 4: KHÔI PHỤC DỮ LIỆU MẶC ĐỊNH CẦN THIẾT
    -- =======================================================
    
    -- Tạo lại dòng cấu hình trống (để code không bị lỗi NULL)
    INSERT INTO SYSTEM_CONFIG (EmailSender, EmailPassword) VALUES ('', '');
    
    -- (Nếu bạn lỡ xóa Admin thì bỏ comment dòng dưới để tạo lại)
    -- DELETE FROM ADMINS;
    -- DBCC CHECKIDENT ('ADMINS', RESEED, 0);
    -- INSERT INTO ADMINS (Username, Password, FullName) VALUES ('admin', 'admin', N'Quản trị viên');

    COMMIT TRANSACTION;
    PRINT '==================================================';
    PRINT '>> ✨ HỆ THỐNG ĐÃ ĐƯỢC RESET NHƯ MỚI! ✨';
    PRINT '==================================================';
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION; -- Nếu lỗi thì hoàn tác, không xóa gì cả
    PRINT '❌ LỖI XÓA DỮ LIỆU: ' + ERROR_MESSAGE();
END CATCH;