using System;
using System.Security.Cryptography;
using System.Text;

namespace Elibse
{
    public static class SecurityHelper
    {
        // Thêm Salt cứng để chống lại Rainbow Table cơ bản
        private static readonly string _fixedSalt = "Salt_Elibse_Project_2025_SecretKey_!@#MOTIVATEDBY_NGOCNGAN";

        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return "";

            // Nối password với chuỗi bí mật trước khi băm
            string passwordWithSalt = password + _fixedSalt;

            // Dùng SHA256
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(passwordWithSalt);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}