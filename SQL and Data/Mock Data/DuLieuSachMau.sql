USE ElibseDB;
GO

-- Xóa dữ liệu cũ nếu muốn làm sạch lại từ đầu (Cẩn thận khi dùng)
-- DELETE FROM LOAN_RECORDS;
-- DELETE FROM BOOKS;
-- GO

PRINT '>> Bat dau them du lieu mau cho bang BOOKS...';

INSERT INTO BOOKS (BookID, Title, Author, CategoryID, Price, ImportDate, Status, Description, BookImage) VALUES 
-- 1. [CNTT] Clean Code (Số lượng: 3 cuốn)
('#0000001-CC-0001', N'Clean Code: A Handbook of Agile Software Craftsmanship', N'Robert C. Martin', 1, 350000, GETDATE(), N'Available', N'Cuốn sách gối đầu giường cho lập trình viên muốn viết code sạch, dễ bảo trì.', NULL),
('#0000002-CC-0002', N'Clean Code: A Handbook of Agile Software Craftsmanship', N'Robert C. Martin', 1, 350000, GETDATE(), N'Available', N'Cuốn sách gối đầu giường cho lập trình viên muốn viết code sạch, dễ bảo trì.', NULL),
('#0000003-CC-0003', N'Clean Code: A Handbook of Agile Software Craftsmanship', N'Robert C. Martin', 1, 350000, GETDATE(), N'Lost',      N'Đã báo mất ngày hôm qua.', NULL), -- Test trạng thái Lost

-- 2. [Văn học] Nhà Giả Kim (Số lượng: 5 cuốn)
('#0000004-NGK-0001', N'Nhà Giả Kim', N'Paulo Coelho', 3, 79000, GETDATE(), N'Available', N'Tiểu thuyết bán chạy nhất mọi thời đại về hành trình theo đuổi ước mơ.', NULL),
('#0000005-NGK-0002', N'Nhà Giả Kim', N'Paulo Coelho', 3, 79000, GETDATE(), N'Available', N'Tiểu thuyết bán chạy nhất mọi thời đại về hành trình theo đuổi ước mơ.', NULL),
('#0000006-NGK-0003', N'Nhà Giả Kim', N'Paulo Coelho', 3, 79000, GETDATE(), N'Available', N'Tiểu thuyết bán chạy nhất mọi thời đại về hành trình theo đuổi ước mơ.', NULL),
('#0000007-NGK-0004', N'Nhà Giả Kim', N'Paulo Coelho', 3, 79000, GETDATE(), N'Available', N'Tiểu thuyết bán chạy nhất mọi thời đại về hành trình theo đuổi ước mơ.', NULL),
('#0000008-NGK-0005', N'Nhà Giả Kim', N'Paulo Coelho', 3, 79000, GETDATE(), N'Damaged',   N'Bìa sách bị rách nhẹ.', NULL), -- Test trạng thái Damaged

-- 3. [Kỹ năng] Đắc Nhân Tâm (Số lượng: 4 cuốn)
('#0000009-DNT-0001', N'Đắc Nhân Tâm', N'Dale Carnegie', 5, 86000, GETDATE(), N'Available', N'Nghệ thuật thu phục lòng người.', NULL),
('#0000010-DNT-0002', N'Đắc Nhân Tâm', N'Dale Carnegie', 5, 86000, GETDATE(), N'Available', N'Nghệ thuật thu phục lòng người.', NULL),
('#0000011-DNT-0003', N'Đắc Nhân Tâm', N'Dale Carnegie', 5, 86000, GETDATE(), N'Available', N'Nghệ thuật thu phục lòng người.', NULL),
('#0000012-DNT-0004', N'Đắc Nhân Tâm', N'Dale Carnegie', 5, 86000, GETDATE(), N'Available', N'Nghệ thuật thu phục lòng người.', NULL),

-- 4. [Truyện tranh] Doraemon Tập 1 (Số lượng: 6 cuốn - Số lượng nhiều để test biểu đồ cột)
('#0000013-DRM-0001', N'Doraemon Truyện Ngắn - Tập 1', N'Fujiko F. Fujio', 4, 25000, GETDATE(), N'Available', N'Chú mèo máy đến từ tương lai.', NULL),
('#0000014-DRM-0002', N'Doraemon Truyện Ngắn - Tập 1', N'Fujiko F. Fujio', 4, 25000, GETDATE(), N'Available', N'Chú mèo máy đến từ tương lai.', NULL),
('#0000015-DRM-0003', N'Doraemon Truyện Ngắn - Tập 1', N'Fujiko F. Fujio', 4, 25000, GETDATE(), N'Available', N'Chú mèo máy đến từ tương lai.', NULL),
('#0000016-DRM-0004', N'Doraemon Truyện Ngắn - Tập 1', N'Fujiko F. Fujio', 4, 25000, GETDATE(), N'Available', N'Chú mèo máy đến từ tương lai.', NULL),
('#0000017-DRM-0005', N'Doraemon Truyện Ngắn - Tập 1', N'Fujiko F. Fujio', 4, 25000, GETDATE(), N'Available', N'Chú mèo máy đến từ tương lai.', NULL),
('#0000018-DRM-0006', N'Doraemon Truyện Ngắn - Tập 1', N'Fujiko F. Fujio', 4, 25000, GETDATE(), N'Available', N'Chú mèo máy đến từ tương lai.', NULL),

-- 5. [Kinh tế] Chiến tranh tiền tệ (Số lượng: 2 cuốn)
('#0000019-CTTT-0001', N'Chiến tranh tiền tệ', N'Song Hongbing', 2, 120000, GETDATE(), N'Available', N'Sự thật về giới tài chính ngân hàng quốc tế.', NULL),
('#0000020-CTTT-0002', N'Chiến tranh tiền tệ', N'Song Hongbing', 2, 120000, GETDATE(), N'Available', N'Sự thật về giới tài chính ngân hàng quốc tế.', NULL);

PRINT '>> Da them xong 20 cuon sach mau!';
GO