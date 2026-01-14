using System;
using System.Windows.Forms;

namespace Elibse
{
    internal static class Program
    {
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                fmConnectConfig configForm = new fmConnectConfig();
                if (configForm.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new fmLoginDialog());
                }
                else
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi nghiêm trọng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}