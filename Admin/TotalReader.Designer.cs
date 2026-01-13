namespace Elibse.Admin
{
    partial class TotalReader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TotalReader));
            this.dgvReaders = new System.Windows.Forms.DataGridView();
            this.picReader = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.txtReaderID = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.btnEditReader = new System.Windows.Forms.Button();
            this.btnDeleteReader = new System.Windows.Forms.Button();
            this.btnAddReader = new System.Windows.Forms.Button();
            this.txtSearchReader = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnResetPass = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpDOB = new System.Windows.Forms.DateTimePicker();
            this.btnChangeImage = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReaders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picReader)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvReaders
            // 
            this.dgvReaders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvReaders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReaders.Location = new System.Drawing.Point(8, 50);
            this.dgvReaders.Margin = new System.Windows.Forms.Padding(2);
            this.dgvReaders.Name = "dgvReaders";
            this.dgvReaders.RowHeadersWidth = 62;
            this.dgvReaders.RowTemplate.Height = 28;
            this.dgvReaders.Size = new System.Drawing.Size(297, 183);
            this.dgvReaders.TabIndex = 0;
            this.dgvReaders.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReaders_CellClick);
            // 
            // picReader
            // 
            this.picReader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picReader.Location = new System.Drawing.Point(61, 35);
            this.picReader.Margin = new System.Windows.Forms.Padding(2);
            this.picReader.Name = "picReader";
            this.picReader.Size = new System.Drawing.Size(67, 65);
            this.picReader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picReader.TabIndex = 1;
            this.picReader.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 115);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tên";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 136);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Mã độc giả";
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(67, 113);
            this.txtFullName.Margin = new System.Windows.Forms.Padding(2);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(121, 20);
            this.txtFullName.TabIndex = 3;
            // 
            // txtReaderID
            // 
            this.txtReaderID.Location = new System.Drawing.Point(67, 134);
            this.txtReaderID.Margin = new System.Windows.Forms.Padding(2);
            this.txtReaderID.Name = "txtReaderID";
            this.txtReaderID.ReadOnly = true;
            this.txtReaderID.Size = new System.Drawing.Size(121, 20);
            this.txtReaderID.TabIndex = 3;
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(67, 176);
            this.txtPhone.Margin = new System.Windows.Forms.Padding(2);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(121, 20);
            this.txtPhone.TabIndex = 3;
            // 
            // btnEditReader
            // 
            this.btnEditReader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditReader.Location = new System.Drawing.Point(105, 229);
            this.btnEditReader.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditReader.Name = "btnEditReader";
            this.btnEditReader.Size = new System.Drawing.Size(55, 22);
            this.btnEditReader.TabIndex = 4;
            this.btnEditReader.Text = "Sửa";
            this.btnEditReader.UseVisualStyleBackColor = true;
            this.btnEditReader.Click += new System.EventHandler(this.btnEditReader_Click);
            // 
            // btnDeleteReader
            // 
            this.btnDeleteReader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteReader.Location = new System.Drawing.Point(164, 229);
            this.btnDeleteReader.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeleteReader.Name = "btnDeleteReader";
            this.btnDeleteReader.Size = new System.Drawing.Size(55, 22);
            this.btnDeleteReader.TabIndex = 4;
            this.btnDeleteReader.Text = "Xóa";
            this.btnDeleteReader.UseVisualStyleBackColor = true;
            this.btnDeleteReader.Click += new System.EventHandler(this.btnDeleteReader_Click);
            // 
            // btnAddReader
            // 
            this.btnAddReader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddReader.Location = new System.Drawing.Point(47, 229);
            this.btnAddReader.Margin = new System.Windows.Forms.Padding(2);
            this.btnAddReader.Name = "btnAddReader";
            this.btnAddReader.Size = new System.Drawing.Size(55, 22);
            this.btnAddReader.TabIndex = 4;
            this.btnAddReader.Text = "Thêm";
            this.btnAddReader.UseVisualStyleBackColor = true;
            this.btnAddReader.Click += new System.EventHandler(this.btnAddReader_Click);
            // 
            // txtSearchReader
            // 
            this.txtSearchReader.Location = new System.Drawing.Point(8, 15);
            this.txtSearchReader.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearchReader.Name = "txtSearchReader";
            this.txtSearchReader.Size = new System.Drawing.Size(211, 20);
            this.txtSearchReader.TabIndex = 5;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(222, 12);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(83, 23);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Tìm độc giả";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // btnReload
            // 
            this.btnReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReload.Location = new System.Drawing.Point(69, 244);
            this.btnReload.Margin = new System.Windows.Forms.Padding(2);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(55, 22);
            this.btnReload.TabIndex = 4;
            this.btnReload.Text = "Tải lại";
            this.btnReload.UseVisualStyleBackColor = true;
            // 
            // btnResetPass
            // 
            this.btnResetPass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnResetPass.Location = new System.Drawing.Point(147, 244);
            this.btnResetPass.Margin = new System.Windows.Forms.Padding(2);
            this.btnResetPass.Name = "btnResetPass";
            this.btnResetPass.Size = new System.Drawing.Size(104, 22);
            this.btnResetPass.TabIndex = 7;
            this.btnResetPass.Text = "Reset mật khẩu";
            this.btnResetPass.UseVisualStyleBackColor = true;
            this.btnResetPass.Click += new System.EventHandler(this.btnResetPass_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 157);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(67, 155);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(2);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(121, 20);
            this.txtEmail.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 177);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "SĐT";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.groupBox1.Controls.Add(this.dtpDOB);
            this.groupBox1.Controls.Add(this.btnChangeImage);
            this.groupBox1.Controls.Add(this.picReader);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnAddReader);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnDeleteReader);
            this.groupBox1.Controls.Add(this.txtFullName);
            this.groupBox1.Controls.Add(this.btnEditReader);
            this.groupBox1.Controls.Add(this.txtReaderID);
            this.groupBox1.Controls.Add(this.txtPhone);
            this.groupBox1.Controls.Add(this.txtEmail);
            this.groupBox1.Location = new System.Drawing.Point(319, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(261, 257);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chỉnh sửa độc giả";
            // 
            // dtpDOB
            // 
            this.dtpDOB.Location = new System.Drawing.Point(67, 196);
            this.dtpDOB.Margin = new System.Windows.Forms.Padding(2);
            this.dtpDOB.Name = "dtpDOB";
            this.dtpDOB.Size = new System.Drawing.Size(180, 20);
            this.dtpDOB.TabIndex = 6;
            // 
            // btnChangeImage
            // 
            this.btnChangeImage.Location = new System.Drawing.Point(143, 57);
            this.btnChangeImage.Margin = new System.Windows.Forms.Padding(2);
            this.btnChangeImage.Name = "btnChangeImage";
            this.btnChangeImage.Size = new System.Drawing.Size(67, 25);
            this.btnChangeImage.TabIndex = 5;
            this.btnChangeImage.Text = "Nhập ảnh";
            this.btnChangeImage.UseVisualStyleBackColor = true;
            this.btnChangeImage.Click += new System.EventHandler(this.btnChangeImage_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 196);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Ngày sinh";
            // 
            // TotalReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 280);
            this.Controls.Add(this.btnResetPass);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearchReader);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.dgvReaders);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "TotalReader";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Elibse: Danh Sách Độc Giả";
            ((System.ComponentModel.ISupportInitialize)(this.dgvReaders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picReader)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvReaders;
        private System.Windows.Forms.PictureBox picReader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.TextBox txtReaderID;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Button btnEditReader;
        private System.Windows.Forms.Button btnDeleteReader;
        private System.Windows.Forms.Button btnAddReader;
        private System.Windows.Forms.TextBox txtSearchReader;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnResetPass;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnChangeImage;
        private System.Windows.Forms.DateTimePicker dtpDOB;
        private System.Windows.Forms.Label label5;
    }
}