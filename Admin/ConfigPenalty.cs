using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Elibse.Admin
{
    public partial class ConfigPenalty : Form
    {
        public ConfigPenalty()
        {
            InitializeComponent();
        }

        private void ConfigPenalty_Load(object sender, EventArgs e)
        {
            LoadConfigData();
        }

        // --- HÀM 1: Tải dữ liệu từ SQL lên Form ---
        private void LoadConfigData()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT TOP 1 BaseFineFee, FineCycleDays, GracePeriodDays, FineIncrement FROM SYSTEM_CONFIG";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Kiểm tra null trước khi gán để tránh lỗi
                        if (reader["BaseFineFee"] != DBNull.Value)
                            numBaseFee.Value = Convert.ToDecimal(reader["BaseFineFee"]);

                        if (reader["FineCycleDays"] != DBNull.Value)
                            numCycle.Value = Convert.ToInt32(reader["FineCycleDays"]);

                        if (reader["GracePeriodDays"] != DBNull.Value)
                            numGrace.Value = Convert.ToInt32(reader["GracePeriodDays"]);

                        if (reader["FineIncrement"] != DBNull.Value)
                            numIncrement.Value = Convert.ToDecimal(reader["FineIncrement"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải cấu hình: " + ex.Message);
            }
        }

        // --- HÀM 2: Lưu dữ liệu xuống SQL ---
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();
                    // Update dòng đầu tiên (hoặc duy nhất) trong bảng CONFIG
                    string sql = @"UPDATE SYSTEM_CONFIG 
                                   SET BaseFineFee = @base, 
                                       FineCycleDays = @cycle, 
                                       GracePeriodDays = @grace, 
                                       FineIncrement = @inc";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@base", numBaseFee.Value);
                        cmd.Parameters.AddWithValue("@cycle", numCycle.Value);
                        cmd.Parameters.AddWithValue("@grace", numGrace.Value);
                        cmd.Parameters.AddWithValue("@inc", numIncrement.Value);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Đã lưu cấu hình phạt thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Đóng form sau khi lưu
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu cấu hình: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}