namespace VuonVietXuStore
{
    partial class FormHome
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHome));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnTrangChu = new Guna.UI2.WinForms.Guna2Button();
            this.pictureBox1 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();
            this.panelDivider = new System.Windows.Forms.Panel();
            this.btnSanPham = new Guna.UI2.WinForms.Guna2Button();
            this.btnKH = new Guna.UI2.WinForms.Guna2Button();
            this.btnDonHang = new Guna.UI2.WinForms.Guna2Button();
            this.btnNCC = new Guna.UI2.WinForms.Guna2Button();
            this.btnNhapHang = new Guna.UI2.WinForms.Guna2Button();
            this.btnBanHang = new Guna.UI2.WinForms.Guna2Button();
            this.btnKho = new Guna.UI2.WinForms.Guna2Button();
            this.button1 = new Guna.UI2.WinForms.Guna2Button();
            this.panelDivider2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(78)))), ((int)(((byte)(44)))));
            this.panel1.Controls.Add(this.btnTrangChu);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.lblUsername);
            this.panel1.Controls.Add(this.lblRole);
            this.panel1.Controls.Add(this.panelDivider);
            this.panel1.Controls.Add(this.btnSanPham);
            this.panel1.Controls.Add(this.btnKH);
            this.panel1.Controls.Add(this.btnDonHang);
            this.panel1.Controls.Add(this.btnNCC);
            this.panel1.Controls.Add(this.btnNhapHang);
            this.panel1.Controls.Add(this.btnBanHang);
            this.panel1.Controls.Add(this.btnKho);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.panelDivider2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(251, 725);
            this.panel1.TabIndex = 1;
            // 
            // btnTrangChu
            // 
            this.btnTrangChu.BorderRadius = 8;
            this.btnTrangChu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTrangChu.FillColor = System.Drawing.Color.Transparent;
            this.btnTrangChu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTrangChu.ForeColor = System.Drawing.Color.White;
            this.btnTrangChu.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnTrangChu.Location = new System.Drawing.Point(11, 187);
            this.btnTrangChu.Name = "btnTrangChu";
            this.btnTrangChu.Size = new System.Drawing.Size(229, 45);
            this.btnTrangChu.TabIndex = 0;
            this.btnTrangChu.Text = "🏠   Trang chủ";
            this.btnTrangChu.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnTrangChu.TextOffset = new System.Drawing.Point(10, 0);
            this.btnTrangChu.Click += new System.EventHandler(this.btnTrangChu_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.ImageRotate = 0F;
            this.pictureBox1.Location = new System.Drawing.Point(78, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.pictureBox1.Size = new System.Drawing.Size(82, 79);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // lblUsername
            // 
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblUsername.ForeColor = System.Drawing.Color.White;
            this.lblUsername.Location = new System.Drawing.Point(-3, 107);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(251, 28);
            this.lblUsername.TabIndex = 2;
            this.lblUsername.Text = "Đàm Vĩnh Viễn";
            this.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRole
            // 
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.lblRole.Location = new System.Drawing.Point(-11, 134);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(251, 23);
            this.lblRole.TabIndex = 3;
            this.lblRole.Text = "Quản lý";
            this.lblRole.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRole.Click += new System.EventHandler(this.lblRole_Click);
            // 
            // panelDivider
            // 
            this.panelDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panelDivider.Location = new System.Drawing.Point(23, 169);
            this.panelDivider.Name = "panelDivider";
            this.panelDivider.Size = new System.Drawing.Size(206, 1);
            this.panelDivider.TabIndex = 4;
            // 
            // btnSanPham
            // 
            this.btnSanPham.BorderRadius = 8;
            this.btnSanPham.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSanPham.FillColor = System.Drawing.Color.Transparent;
            this.btnSanPham.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSanPham.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(220)))));
            this.btnSanPham.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSanPham.Location = new System.Drawing.Point(11, 237);
            this.btnSanPham.Name = "btnSanPham";
            this.btnSanPham.Size = new System.Drawing.Size(229, 45);
            this.btnSanPham.TabIndex = 5;
            this.btnSanPham.Text = "📦   Sản phẩm";
            this.btnSanPham.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnSanPham.TextOffset = new System.Drawing.Point(10, 0);
            this.btnSanPham.Click += new System.EventHandler(this.btnSanPham_Click);
            // 
            // btnKH
            // 
            this.btnKH.BorderRadius = 8;
            this.btnKH.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKH.FillColor = System.Drawing.Color.Transparent;
            this.btnKH.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnKH.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(220)))));
            this.btnKH.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnKH.Location = new System.Drawing.Point(11, 287);
            this.btnKH.Name = "btnKH";
            this.btnKH.Size = new System.Drawing.Size(229, 45);
            this.btnKH.TabIndex = 6;
            this.btnKH.Text = "👤   Khách hàng";
            this.btnKH.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnKH.TextOffset = new System.Drawing.Point(10, 0);
            this.btnKH.Click += new System.EventHandler(this.btnKH_Click);
            // 
            // btnDonHang
            // 
            this.btnDonHang.BorderRadius = 8;
            this.btnDonHang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDonHang.FillColor = System.Drawing.Color.Transparent;
            this.btnDonHang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDonHang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(220)))));
            this.btnDonHang.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnDonHang.Location = new System.Drawing.Point(11, 337);
            this.btnDonHang.Name = "btnDonHang";
            this.btnDonHang.Size = new System.Drawing.Size(229, 45);
            this.btnDonHang.TabIndex = 7;
            this.btnDonHang.Text = "📄   Đơn hàng";
            this.btnDonHang.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnDonHang.TextOffset = new System.Drawing.Point(10, 0);
            this.btnDonHang.Click += new System.EventHandler(this.btnDonHang_Click);
            // 
            // btnNCC
            // 
            this.btnNCC.BorderRadius = 8;
            this.btnNCC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNCC.FillColor = System.Drawing.Color.Transparent;
            this.btnNCC.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnNCC.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(220)))));
            this.btnNCC.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnNCC.Location = new System.Drawing.Point(11, 387);
            this.btnNCC.Name = "btnNCC";
            this.btnNCC.Size = new System.Drawing.Size(229, 45);
            this.btnNCC.TabIndex = 8;
            this.btnNCC.Text = "🏭   Nhà cung cấp";
            this.btnNCC.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnNCC.TextOffset = new System.Drawing.Point(10, 0);
            this.btnNCC.Click += new System.EventHandler(this.btnNCC_Click);
            // 
            // btnNhapHang
            // 
            this.btnNhapHang.BorderRadius = 8;
            this.btnNhapHang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNhapHang.FillColor = System.Drawing.Color.Transparent;
            this.btnNhapHang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnNhapHang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(220)))));
            this.btnNhapHang.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnNhapHang.Location = new System.Drawing.Point(11, 437);
            this.btnNhapHang.Name = "btnNhapHang";
            this.btnNhapHang.Size = new System.Drawing.Size(229, 45);
            this.btnNhapHang.TabIndex = 9;
            this.btnNhapHang.Text = "📥   Nhập hàng";
            this.btnNhapHang.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnNhapHang.TextOffset = new System.Drawing.Point(10, 0);
            this.btnNhapHang.Click += new System.EventHandler(this.btnNhapHang_Click);
            // 
            // btnBanHang
            // 
            this.btnBanHang.BorderRadius = 8;
            this.btnBanHang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBanHang.FillColor = System.Drawing.Color.Transparent;
            this.btnBanHang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnBanHang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(220)))));
            this.btnBanHang.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnBanHang.Location = new System.Drawing.Point(11, 487);
            this.btnBanHang.Name = "btnBanHang";
            this.btnBanHang.Size = new System.Drawing.Size(229, 45);
            this.btnBanHang.TabIndex = 10;
            this.btnBanHang.Text = "🛒   Bán hàng";
            this.btnBanHang.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnBanHang.TextOffset = new System.Drawing.Point(10, 0);
            this.btnBanHang.Click += new System.EventHandler(this.btnBanHang_Click);
            // 
            // btnKho
            // 
            this.btnKho.BorderRadius = 8;
            this.btnKho.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKho.FillColor = System.Drawing.Color.Transparent;
            this.btnKho.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnKho.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(220)))));
            this.btnKho.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnKho.Location = new System.Drawing.Point(11, 538);
            this.btnKho.Name = "btnKho";
            this.btnKho.Size = new System.Drawing.Size(229, 45);
            this.btnKho.TabIndex = 11;
            this.btnKho.Text = "🏪   Kho hàng";
            this.btnKho.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnKho.TextOffset = new System.Drawing.Point(10, 0);
            this.btnKho.Click += new System.EventHandler(this.btnKho_Click);
            // 
            // button1
            // 
            this.button1.BorderRadius = 8;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FillColor = System.Drawing.Color.Transparent;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(220)))));
            this.button1.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.button1.Location = new System.Drawing.Point(11, 588);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(229, 45);
            this.button1.TabIndex = 12;
            this.button1.Text = "📊   Báo cáo";
            this.button1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.button1.TextOffset = new System.Drawing.Point(10, 0);
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panelDivider2
            // 
            this.panelDivider2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panelDivider2.Location = new System.Drawing.Point(23, 645);
            this.panelDivider2.Name = "panelDivider2";
            this.panelDivider2.Size = new System.Drawing.Size(206, 1);
            this.panelDivider2.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.label1.Location = new System.Drawing.Point(23, 661);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 34);
            this.label1.TabIndex = 14;
            this.label1.Text = "⏻   Đăng xuất";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(247)))), ((int)(((byte)(244)))));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(251, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1006, 725);
            this.panel2.TabIndex = 0;
            // 
            // FormHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1257, 725);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FormHome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vườn Việt Xứ Store - Hệ thống quản lý chính";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Panel panel1;
        // Đổi PictureBox thành Guna2CirclePictureBox để làm avatar tròn
        private Guna.UI2.WinForms.Guna2CirclePictureBox pictureBox1;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Panel panelDivider;
        // Đổi toàn bộ các Button thành Guna2Button
        private Guna.UI2.WinForms.Guna2Button btnTrangChu;
        private Guna.UI2.WinForms.Guna2Button btnSanPham;
        private Guna.UI2.WinForms.Guna2Button btnKH;
        private Guna.UI2.WinForms.Guna2Button btnDonHang;
        private Guna.UI2.WinForms.Guna2Button btnNCC;
        private Guna.UI2.WinForms.Guna2Button btnNhapHang;
        private Guna.UI2.WinForms.Guna2Button btnBanHang;
        private Guna.UI2.WinForms.Guna2Button btnKho;
        private Guna.UI2.WinForms.Guna2Button button1;
        private System.Windows.Forms.Panel panelDivider2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
    }
}