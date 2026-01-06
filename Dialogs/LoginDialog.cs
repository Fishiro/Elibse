using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Elibse
{
    public partial class fmLoginDialog : Form
    {
        public fmLoginDialog()
        {
            InitializeComponent();
        }

        private void fmLoginDialog_Load(object sender, EventArgs e)
        {

        }

        private void btnAdminChoice_Click(object sender, EventArgs e)
        {
            // Tạo mới cửa sổ đăng nhập Admin
            AdminLogin adminForm = new AdminLogin();
            this.Hide();

            // Hiện nó lên
            adminForm.ShowDialog();

            this.Close();
        }
    }
}
