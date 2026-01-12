using System;
using System.Windows.Forms;

namespace Elibse
{
    public partial class PaymentDialog : Form
    {
        // Property công khai để truyền số tiền phạt vào form
        public decimal AmountDue { get; set; }

        // Property để lấy số tiền người dùng thực trả (nếu cần dùng sau này)
        public decimal AmountPaid { get; private set; }

        public PaymentDialog()
        {
            InitializeComponent();
        }

        // Overload constructor để truyền tiền phạt ngay khi new Form
        public PaymentDialog(decimal amountDue) : this()
        {
            this.AmountDue = amountDue;
        }

        // Sự kiện khi Form load
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // Hiển thị số tiền cần trả lên textBox1 (ReadOnly)
            // Định dạng tiền tệ (N0) cho dễ nhìn
            textBox1.Text = AmountDue.ToString("N0");
            textBox2.Focus(); // Đặt con trỏ vào ô nhập tiền
        }

        // Sự kiện click nút Thanh toán (Bạn cần gán sự kiện này cho button1 bên Design)
        private void btnPay_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra đầu vào
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Vui lòng nhập số tiền khách trả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(textBox2.Text, out decimal payAmount))
            {
                MessageBox.Show("Số tiền không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Kiểm tra đủ tiền chưa
            if (payAmount < AmountDue)
            {
                MessageBox.Show("Số tiền trả chưa đủ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Nếu đủ tiền -> Gán giá trị và đóng form với kết quả OK
            this.AmountPaid = payAmount;

            // Tính tiền thừa (nếu muốn hiển thị thông báo)
            decimal change = payAmount - AmountDue;
            if (change > 0)
            {
                MessageBox.Show($"Thanh toán thành công!\nTiền thừa trả lại: {change.ToString("N0")} VNĐ", "Hoàn tất");
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}