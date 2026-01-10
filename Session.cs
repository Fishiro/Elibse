using System;

namespace Elibse
{
    // Class này dùng để lưu trữ thông tin toàn cục (Global)
    // Giúp truy cập thông tin người đang đăng nhập từ bất kỳ Form nào
    public static class Session
    {
        // Lưu Username của Admin đang đăng nhập
        public static string CurrentAdminUsername { get; set; } = "";

        // Kiểm tra xem đã đăng nhập chưa
        public static bool IsLoggedIn()
        {
            return !string.IsNullOrEmpty(CurrentAdminUsername);
        }

        // Xóa thông tin khi đăng xuất
        public static void Logout()
        {
            CurrentAdminUsername = "";
        }
    }
}