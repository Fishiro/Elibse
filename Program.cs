using System;
using System.Windows.Forms;

namespace Elibse
{
    internal static class Program
    {
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new fmLoginDialog());
        }
    }
}