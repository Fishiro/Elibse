using System;
using System.Security.Cryptography;
using System.Text;

namespace Elibse
{
    public static class SecurityHelper
    {
        // Hàm băm mật khẩu sang chuỗi MD5 (32 ký tự)
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return "";

            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Chuyển mảng byte thành chuỗi Hex
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2")); // X2 để ra chữ hoa
                }
                return sb.ToString();
            }
        }
    }
}