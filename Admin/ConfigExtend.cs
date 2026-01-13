using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Elibse.Admin
{
    public partial class ConfigExtend : Form
    {
        public ConfigExtend()
        {
            InitializeComponent();
        }

        private void ConfigExtend_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    // Lấy giá trị MaxExtendDays hiện tại
                    string sql = "SELECT TOP 1 MaxExtendDays FROM SYSTEM_CONFIG";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                    {
                        numMaxDays.Value = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải cấu hình: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    // Lưu giá trị mới vào DB
                    string sql = "UPDATE SYSTEM_CONFIG SET MaxExtendDays = @days";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@days", numMaxDays.Value);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Đã cập nhật quy định gia hạn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu cấu hình: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}