namespace Elibse
{
    partial class Borrow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnConfirmBorrow = new System.Windows.Forms.Button();
            this.txtReaderName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtReaderID = new System.Windows.Forms.TextBox();
            this.txtCreatedDate = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.picReaderAvatar = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBorrowCount = new System.Windows.Forms.TextBox();
            this.txtViolationStatus = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.picBookCover = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBookTitle = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.txtBookID = new System.Windows.Forms.TextBox();
            this.btnViewReaders = new System.Windows.Forms.Button();
            this.btnViewBooks = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picReaderAvatar)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBookCover)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConfirmBorrow
            // 
            this.btnConfirmBorrow.Location = new System.Drawing.Point(867, 316);
            this.btnConfirmBorrow.Name = "btnConfirmBorrow";
            this.btnConfirmBorrow.Size = new System.Drawing.Size(158, 53);
            this.btnConfirmBorrow.TabIndex = 0;
            this.btnConfirmBorrow.Text = "Ký mượn";
            this.btnConfirmBorrow.UseVisualStyleBackColor = true;
            this.btnConfirmBorrow.Click += new System.EventHandler(this.btnConfirmBorrow_Click);
            // 
            // txtReaderName
            // 
            this.txtReaderName.Location = new System.Drawing.Point(160, 218);
            this.txtReaderName.Name = "txtReaderName";
            this.txtReaderName.Size = new System.Drawing.Size(198, 26);
            this.txtReaderName.TabIndex = 1;
            this.txtReaderName.Text = "Nguyen Ngoc Ngan";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 179);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mã độc giả";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 221);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tên độc giả";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 263);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Ngày tạo tài khoản";
            // 
            // txtReaderID
            // 
            this.txtReaderID.Location = new System.Drawing.Point(160, 176);
            this.txtReaderID.Name = "txtReaderID";
            this.txtReaderID.Size = new System.Drawing.Size(198, 26);
            this.txtReaderID.TabIndex = 1;
            this.txtReaderID.Text = "#0000000-12302025";
            this.txtReaderID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReaderID_KeyDown);
            // 
            // txtCreatedDate
            // 
            this.txtCreatedDate.Location = new System.Drawing.Point(160, 260);
            this.txtCreatedDate.Name = "txtCreatedDate";
            this.txtCreatedDate.ReadOnly = true;
            this.txtCreatedDate.Size = new System.Drawing.Size(148, 26);
            this.txtCreatedDate.TabIndex = 1;
            this.txtCreatedDate.Text = "20/03/2004";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.picReaderAvatar);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtReaderName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtBorrowCount);
            this.groupBox1.Controls.Add(this.txtViolationStatus);
            this.groupBox1.Controls.Add(this.txtCreatedDate);
            this.groupBox1.Controls.Add(this.txtReaderID);
            this.groupBox1.Location = new System.Drawing.Point(29, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(390, 404);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin độc giả";
            // 
            // picReaderAvatar
            // 
            this.picReaderAvatar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picReaderAvatar.Location = new System.Drawing.Point(147, 42);
            this.picReaderAvatar.Name = "picReaderAvatar";
            this.picReaderAvatar.Size = new System.Drawing.Size(100, 100);
            this.picReaderAvatar.TabIndex = 3;
            this.picReaderAvatar.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(43, 347);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 20);
            this.label8.TabIndex = 2;
            this.label8.Text = "Số sách mượn";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(78, 305);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 20);
            this.label7.TabIndex = 2;
            this.label7.Text = "Vi phạm?";
            // 
            // txtBorrowCount
            // 
            this.txtBorrowCount.Location = new System.Drawing.Point(160, 344);
            this.txtBorrowCount.Name = "txtBorrowCount";
            this.txtBorrowCount.ReadOnly = true;
            this.txtBorrowCount.Size = new System.Drawing.Size(75, 26);
            this.txtBorrowCount.TabIndex = 1;
            this.txtBorrowCount.Text = "?/6";
            this.txtBorrowCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtViolationStatus
            // 
            this.txtViolationStatus.Location = new System.Drawing.Point(160, 302);
            this.txtViolationStatus.Name = "txtViolationStatus";
            this.txtViolationStatus.ReadOnly = true;
            this.txtViolationStatus.Size = new System.Drawing.Size(75, 26);
            this.txtViolationStatus.TabIndex = 1;
            this.txtViolationStatus.Text = "Không";
            this.txtViolationStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.picBookCover);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtBookTitle);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtAuthor);
            this.groupBox2.Controls.Add(this.txtBookID);
            this.groupBox2.Location = new System.Drawing.Point(458, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(624, 200);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thông tin sách mượn";
            // 
            // picBookCover
            // 
            this.picBookCover.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBookCover.Location = new System.Drawing.Point(443, 24);
            this.picBookCover.Name = "picBookCover";
            this.picBookCover.Size = new System.Drawing.Size(124, 158);
            this.picBookCover.TabIndex = 3;
            this.picBookCover.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Tên tác giả";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(51, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 20);
            this.label5.TabIndex = 2;
            this.label5.Text = "Tên sách";
            // 
            // txtBookTitle
            // 
            this.txtBookTitle.Location = new System.Drawing.Point(131, 95);
            this.txtBookTitle.Name = "txtBookTitle";
            this.txtBookTitle.ReadOnly = true;
            this.txtBookTitle.Size = new System.Drawing.Size(260, 26);
            this.txtBookTitle.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(56, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 20);
            this.label6.TabIndex = 2;
            this.label6.Text = "Mã sách";
            // 
            // txtAuthor
            // 
            this.txtAuthor.Location = new System.Drawing.Point(131, 143);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.ReadOnly = true;
            this.txtAuthor.Size = new System.Drawing.Size(260, 26);
            this.txtAuthor.TabIndex = 1;
            // 
            // txtBookID
            // 
            this.txtBookID.Location = new System.Drawing.Point(131, 47);
            this.txtBookID.Name = "txtBookID";
            this.txtBookID.Size = new System.Drawing.Size(260, 26);
            this.txtBookID.TabIndex = 1;
            this.txtBookID.Text = "#0000000-TENSACH-0000";
            this.txtBookID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBookID_KeyDown);
            // 
            // btnViewReaders
            // 
            this.btnViewReaders.Location = new System.Drawing.Point(94, 100);
            this.btnViewReaders.Name = "btnViewReaders";
            this.btnViewReaders.Size = new System.Drawing.Size(158, 44);
            this.btnViewReaders.TabIndex = 4;
            this.btnViewReaders.Text = "Danh sách độc giả";
            this.btnViewReaders.UseVisualStyleBackColor = true;
            // 
            // btnViewBooks
            // 
            this.btnViewBooks.Location = new System.Drawing.Point(94, 46);
            this.btnViewBooks.Name = "btnViewBooks";
            this.btnViewBooks.Size = new System.Drawing.Size(158, 44);
            this.btnViewBooks.TabIndex = 4;
            this.btnViewBooks.Text = "Xem kho sách";
            this.btnViewBooks.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnViewBooks);
            this.groupBox3.Controls.Add(this.btnViewReaders);
            this.groupBox3.Location = new System.Drawing.Point(458, 248);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(328, 183);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Xem nhanh";
            // 
            // Borrow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1108, 474);
            this.Controls.Add(this.btnConfirmBorrow);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Name = "Borrow";
            this.Text = "Elibse: Độc Giả Ký Mượn ";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picReaderAvatar)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBookCover)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConfirmBorrow;
        private System.Windows.Forms.TextBox txtReaderName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtReaderID;
        private System.Windows.Forms.TextBox txtCreatedDate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBookTitle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.TextBox txtBookID;
        private System.Windows.Forms.Button btnViewReaders;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtViolationStatus;
        private System.Windows.Forms.Button btnViewBooks;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.PictureBox picReaderAvatar;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBorrowCount;
        private System.Windows.Forms.PictureBox picBookCover;
    }
}