using System;
using System.Windows.Forms;

namespace Elibse
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // BƯỚC 1: Hiện form cấu hình Server trước
            fmConnectConfig configForm = new fmConnectConfig();

            // Nếu người dùng kết nối thành công (DialogResult.OK) thì mới vào App
            if (configForm.ShowDialog() == DialogResult.OK)
            {
                // BƯỚC 2: Vào màn hình đăng nhập chính
                Application.Run(new fmLoginDialog());
            }
            else
            {
                // Nếu họ tắt form cấu hình mà chưa kết nối -> Thoát luôn
                Application.Exit();
            }
        }
    }
}