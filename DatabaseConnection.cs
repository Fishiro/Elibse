using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Elibse
{
    public class DatabaseConnection
    {
        

        private static string strCon = @"Data Source=.\SQLEXPRESS;Initial Catalog=ElibseDB;Integrated Security=True";

        // Hàm lấy đối tượng kết nối (dùng chung cho cả dự án)
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(strCon);
        }
    }
}