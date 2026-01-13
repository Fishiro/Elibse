using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine; // Thư viện Crystal Report

namespace Elibse.Admin
{
    public partial class fmReportBook : Form
    {
        public fmReportBook()
        {
            InitializeComponent();
        }

        private void fmReportBook_Load(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void LoadReport()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    conn.Open();

                    string query = @"
                SELECT 
                    b.BookID, 
                    b.Title, 
                    b.Author, 
                    ISNULL(c.CategoryName, N'Chưa phân loại') AS CategoryName,  
                    b.Price, 
                    b.Status 
                FROM BOOKS b
                LEFT JOIN CATEGORIES c ON b.CategoryID = c.CategoryID";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            rptBookList rpt = new rptBookList();
                            rpt.SetDataSource(dt);
                            cryViewer.ReportSource = rpt;
                            cryViewer.Refresh();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi tải báo cáo Crystal Report: " + ex.Message +
                                          "\n\nVui lòng đảm bảo file rptBookList.rpt tồn tại trong project!",
                                          "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy dữ liệu sách nào!", "Thông báo");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải báo cáo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}