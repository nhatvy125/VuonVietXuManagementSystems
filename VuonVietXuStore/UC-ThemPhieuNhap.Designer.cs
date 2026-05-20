namespace VuonVietXuStore
{
    partial class UC_ThemPhieuNhap
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelInput = new System.Windows.Forms.Panel();
            this.lblTenSP = new System.Windows.Forms.Label();
            this.txtTenSP = new System.Windows.Forms.TextBox();
            this.lblNCC = new System.Windows.Forms.Label();
            this.txtNCC = new System.Windows.Forms.TextBox();
            this.lblNgayNhap = new System.Windows.Forms.Label();
            this.dtpNgayNhap = new System.Windows.Forms.DateTimePicker();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.lblGiaNhap = new System.Windows.Forms.Label();
            this.txtGiaNhap = new System.Windows.Forms.TextBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.panelGrid = new System.Windows.Forms.Panel();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();

            this.panelHeader.SuspendLayout();
            this.panelInput.SuspendLayout();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();

            // === panelHeader ===
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1000, 65);

            // === lblTitle ===
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(18, 78, 44);
            this.lblTitle.Location = new System.Drawing.Point(30, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(400, 35);
            this.lblTitle.Text = "➕ TẠO MỚI PHIẾU NHẬP HÀNG";

            // === panelInput (Khu vực nhập liệu căn thẳng hàng) ===
            this.panelInput.BackColor = System.Drawing.Color.White;
            this.panelInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelInput.Controls.Add(this.lblTenSP);
            this.panelInput.Controls.Add(this.txtTenSP);
            this.panelInput.Controls.Add(this.lblNCC);
            this.panelInput.Controls.Add(this.txtNCC);
            this.panelInput.Controls.Add(this.lblNgayNhap);
            this.panelInput.Controls.Add(this.dtpNgayNhap);
            this.panelInput.Controls.Add(this.lblSoLuong);
            this.panelInput.Controls.Add(this.txtSoLuong);
            this.panelInput.Controls.Add(this.lblGiaNhap);
            this.panelInput.Controls.Add(this.txtGiaNhap);
            this.panelInput.Controls.Add(this.btnThem);
            this.panelInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelInput.Location = new System.Drawing.Point(0, 65);
            this.panelInput.Name = "panelInput";
            this.panelInput.Size = new System.Drawing.Size(1000, 160);

            // Helper Lambda cho style TextBox trong Designer không dùng được, ghi thủ công chuẩn hóa:
            // Cột 1: Tên sản phẩm & Nhà cung cấp
            this.lblTenSP.Text = "Tên sản phẩm";
            this.lblTenSP.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTenSP.Location = new System.Drawing.Point(30, 15);
            this.lblTenSP.Size = new System.Drawing.Size(120, 25);

            this.txtTenSP.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtTenSP.Location = new System.Drawing.Point(160, 12);
            this.txtTenSP.Size = new System.Drawing.Size(320, 31);
            this.txtTenSP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTenSP.Name = "txtTenSP";

            this.lblNCC.Text = "Nhà cung cấp";
            this.lblNCC.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNCC.Location = new System.Drawing.Point(30, 60);
            this.lblNCC.Size = new System.Drawing.Size(120, 25);

            this.txtNCC.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtNCC.Location = new System.Drawing.Point(160, 57);
            this.txtNCC.Size = new System.Drawing.Size(320, 31);
            this.txtNCC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNCC.Name = "txtNCC";

            // Cột 2: Ngày nhập, Số lượng, Giá nhập
            this.lblNgayNhap.Text = "Ngày nhập";
            this.lblNgayNhap.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNgayNhap.Location = new System.Drawing.Point(520, 15);
            this.lblNgayNhap.Size = new System.Drawing.Size(100, 25);

            this.dtpNgayNhap.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.dtpNgayNhap.Location = new System.Drawing.Point(630, 12);
            this.dtpNgayNhap.Size = new System.Drawing.Size(320, 31);
            this.dtpNgayNhap.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayNhap.Name = "dtpNgayNhap";

            this.lblSoLuong.Text = "Số lượng";
            this.lblSoLuong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSoLuong.Location = new System.Drawing.Point(30, 105);
            this.lblSoLuong.Size = new System.Drawing.Size(120, 25);

            this.txtSoLuong.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtSoLuong.Location = new System.Drawing.Point(160, 102);
            this.txtSoLuong.Size = new System.Drawing.Size(120, 31);
            this.txtSoLuong.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSoLuong.Name = "txtSoLuong";

            this.lblGiaNhap.Text = "Giá nhập";
            this.lblGiaNhap.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblGiaNhap.Location = new System.Drawing.Point(300, 105);
            this.lblGiaNhap.Size = new System.Drawing.Size(80, 25);

            this.txtGiaNhap.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtGiaNhap.Location = new System.Drawing.Point(380, 102);
            this.txtGiaNhap.Size = new System.Drawing.Size(100, 31);
            this.txtGiaNhap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGiaNhap.Name = "txtGiaNhap";

            // Nút "Thêm vào danh sách" tạm thời
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(20, 90, 50);
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(630, 100);
            this.btnThem.Size = new System.Drawing.Size(320, 35);
            this.btnThem.Text = "👇 Thêm vào danh sách chi tiết";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThem.Name = "btnThem";

            // === panelGrid (Vùng chứa bảng, tự co giãn đầy màn hình) ===
            this.panelGrid.BackColor = System.Drawing.Color.Transparent;
            this.panelGrid.Controls.Add(this.dgvChiTiet);
            this.panelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGrid.Location = new System.Drawing.Point(0, 225);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Padding = new System.Windows.Forms.Padding(30, 15, 30, 10);
            this.panelGrid.Size = new System.Drawing.Size(1000, 315);

            // === dgvChiTiet (DataGridView phẳng hiện đại) ===
            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTiet.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvChiTiet.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvChiTiet.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;

            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(20, 90, 50);
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(20, 90, 50);
            this.dgvChiTiet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvChiTiet.ColumnHeadersHeight = 38;
            this.dgvChiTiet.EnableHeadersVisualStyles = false;

            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(235, 247, 238);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(20, 90, 50);
            this.dgvChiTiet.DefaultCellStyle = dataGridViewCellStyle2;

            this.dgvChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTiet.GridColor = System.Drawing.Color.FromArgb(230, 235, 232);
            this.dgvChiTiet.Location = new System.Drawing.Point(30, 15);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.RowHeadersVisible = false;
            this.dgvChiTiet.RowTemplate.Height = 32;
            this.dgvChiTiet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTiet.Size = new System.Drawing.Size(940, 290);
            this.dgvChiTiet.TabIndex = 1;
            this.dgvChiTiet.Name = "dataGridView1"; // Giữ lại name cũ để không lỗi code-behind

            // === panelFooter (Thanh điều hướng dưới cùng) ===
            this.panelFooter.BackColor = System.Drawing.Color.White;
            this.panelFooter.Controls.Add(this.btnLuu);
            this.panelFooter.Controls.Add(this.btnHuy);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 540);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(1000, 60);

            // === btnLuu (Nút Lưu) ===
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(20, 90, 50);
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.FlatAppearance.BorderSize = 0;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(30, 10);
            this.btnLuu.Size = new System.Drawing.Size(120, 40);
            this.btnLuu.Text = "💾 Lưu lại";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuu.Name = "btnLuu"; // Ánh xạ từ nút Lưu cũ

            // === btnHuy (Nút Hủy) ===
            this.btnHuy.BackColor = System.Drawing.Color.Transparent;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(20, 90, 50);
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.FromArgb(20, 90, 50);
            this.btnHuy.Location = new System.Drawing.Point(160, 10);
            this.btnHuy.Size = new System.Drawing.Size(120, 40);
            this.btnHuy.Text = "❌ Hủy bỏ";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuy.Name = "btnHuy"; // Ánh xạ từ nút Hủy cũ

            // === UC_ThemPhieuNhap ===
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(244, 248, 245);
            this.Controls.Add(this.panelGrid);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.panelInput);
            this.Controls.Add(this.panelHeader);
            this.Name = "UC_ThemPhieuNhap";
            this.Size = new System.Drawing.Size(1000, 600);

            this.panelHeader.ResumeLayout(false);
            this.panelInput.ResumeLayout(false);
            this.panelInput.PerformLayout();
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.panelFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelInput;
        private System.Windows.Forms.Label lblTenSP;
        private System.Windows.Forms.TextBox txtTenSP;
        private System.Windows.Forms.Label lblNCC;
        private System.Windows.Forms.TextBox txtNCC;
        private System.Windows.Forms.Label lblNgayNhap;
        private System.Windows.Forms.DateTimePicker dtpNgayNhap;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.Label lblGiaNhap;
        private System.Windows.Forms.TextBox txtGiaNhap;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;
    }
}