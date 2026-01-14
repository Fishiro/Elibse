# Elibse - Hệ Thống Quản Lý Thư Viện Hiện Đại

**Elibse** là một ứng dụng desktop mạnh mẽ được xây dựng trên nền tảng .NET Windows Forms, hỗ trợ quản lý toàn diện các hoạt động của một thư viện từ việc quản lý kho sách, thông tin độc giả cho đến quy trình mượn trả và hệ thống thông báo tự động qua Email.

## 🚀 Tính năng chính

### 1. Dành cho Quản trị viên (Admin)

* **Bảng điều khiển (Dashboard):** Thống kê trực quan số lượng sách, sách đang mượn, độc giả vi phạm và sách quá hạn bằng biểu đồ cột.
* **Quản lý Sách & Danh mục:** Thêm, sửa, xóa thông tin sách và các thể loại sách (Công nghệ thông tin, Kinh tế, Văn học...).
* **Quản lý Độc giả:** Quản lý danh sách thành viên và trạng thái tài khoản (Active/Locked).
* **Xử lý Mượn - Trả:** Ghi nhận phiếu mượn, cập nhật trạng thái sách và tính toán phí phạt tự động.
* **Hệ thống thông báo Email:** Tự động gửi email cảnh báo quá hạn hoặc thông báo trạng thái mượn sách cho độc giả.
* **Cấu hình hệ thống:** Tùy chỉnh phí phạt, thời gian gia hạn tối đa và thông tin tài khoản SMTP gửi mail.
* **Báo cáo & Nhật ký:** Xuất báo cáo danh sách sách và xem lịch sử hoạt động của Admin.

### 2. Dành cho Độc giả (Reader)

* **Tra cứu sách:** Tìm kiếm sách theo tên hoặc tác giả trong kho sách sẵn có.
* **Mượn sách trực tuyến:** Đăng ký mượn sách ngay trên ứng dụng với thời hạn mặc định là 7 ngày.
* **Tủ sách cá nhân:** Theo dõi danh sách các cuốn sách đang mượn và thực hiện trả sách.
* **Lịch sử & Profile:** Xem lại lịch sử mượn trả và quản lý thông tin cá nhân.

---

## 🛠 Công nghệ sử dụng

* **Ngôn ngữ:** C# (.NET Framework 4.7.2).
* **Cơ sở dữ liệu:** Microsoft SQL Server.
* **Báo cáo:** Crystal Reports.
* **Thư viện hỗ trợ:** * `ExcelDataReader`: Hỗ trợ nhập dữ liệu từ Excel.
* `System.Net.Mail`: Xử lý gửi Email thông báo.



---

## 📂 Cấu trúc dự án

* `Elibse/Admin/`: Chứa các Form xử lý nghiệp vụ của quản trị viên (Dashboard, Quản lý sách, Độc giả).
* `Elibse/Reader/`: Chứa giao diện và logic dành cho người dùng mượn sách.
* `Elibse/SQL and Data/`: Chứa tệp `ElibseDB.sql` để khởi tạo cơ sở dữ liệu.
* `DatabaseConnection.cs`: Lớp trung tâm quản lý kết nối SQL Server và cấu hình Server.
* `EmailService.cs`: Dịch vụ gửi email thông báo dựa trên cấu hình trong CSDL.
* `Program.cs`: Điểm khởi chạy ứng dụng, kiểm tra cấu hình kết nối trước khi vào màn hình đăng nhập.

---

## ⚙️ Hướng dẫn cài đặt

### Bước 1: Khởi tạo Cơ sở dữ liệu

1. Mở SQL Server Management Studio (SSMS).
2. Mở tệp `Elibse/SQL and Data/ElibseDB.sql`.
3. Nhấn `Execute` để tạo Database `ElibseDB` và các bảng dữ liệu cần thiết.
* *Lưu ý:* Tài khoản Admin mặc định là `admin` (mật khẩu trống).



### Bước 2: Cấu hình kết nối

1. Chạy ứng dụng lần đầu tiên.
2. Form cấu hình kết nối (`fmConnectConfig`) sẽ xuất hiện. Nhập tên Server SQL của bạn (Ví dụ: `.\SQLEXPRESS`) và nhấn Lưu.

### Bước 3: Cấu hình Email (Để dùng tính năng thông báo)

1. Đăng nhập với quyền Admin.
2. Vào menu **Hệ thống** -> **Cài đặt Email**.
3. Nhập Email gửi (Gmail) và App Password để hệ thống có thể gửi thông báo tự động.

---

## 📖 Quy trình sử dụng

### Đối với Admin:

1. **Thiết lập:** Thêm các danh mục sách trước, sau đó mới thêm sách vào kho.
2. **Vận hành:** Sử dụng mục mượn/trả để hỗ trợ độc giả tại quầy.
3. **Giám sát:** Kiểm tra Dashboard thường xuyên để biết số lượng sách quá hạn và nhấn "Gửi cảnh báo quá hạn" khi cần thiết.

### Đối với Độc giả:

1. **Đăng ký:** Tạo tài khoản mới tại màn hình đăng ký.
2. **Mượn sách:** Tìm cuốn sách yêu thích tại Tab "Tìm sách", nhấn "Mượn".
3. **Trả sách:** Khi đọc xong, vào Tab "Tủ sách của tôi" để nhấn trả sách.

---

## 👤 Thông tin tác giả

Dự án được thực hiện bởi nhóm sinh viên lớp CNTT K17:

* **Nguyễn Dư Quí** (CK2402A33)
* **Sơn Yến Vy** (CK2402A32)
* **Bùi Nguyễn Minh Thư** (CK2402A25)

**Học phần:** Lập trình Windows.
