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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnTrangChu = new Guna.UI2.WinForms.Guna2Button();
            this.pictureBox1 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();
            this.panelDivider = new System.Windows.Forms.Panel();
            this.btnSanPham = new Guna.UI2.WinForms.Guna2Button();
            this.btnKH = new Guna.UI2.WinForms.Guna2Button();
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

            // === panel1 (Sidebar bên trái) ===
            this.panel1.BackColor = System.Drawing.Color.FromArgb(18, 78, 44);
            this.panel1.Controls.Add(this.btnTrangChu);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.lblUsername);
            this.panel1.Controls.Add(this.lblRole);
            this.panel1.Controls.Add(this.panelDivider);
            this.panel1.Controls.Add(this.btnSanPham);
            this.panel1.Controls.Add(this.btnKH);
            this.panel1.Controls.Add(this.btnNCC);
            this.panel1.Controls.Add(this.btnNhapHang);
            this.panel1.Controls.Add(this.btnBanHang);
            this.panel1.Controls.Add(this.btnKho);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.panelDivider2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Size = new System.Drawing.Size(220, 680); // Tăng nhẹ độ rộng sidebar cho thoáng
            this.panel1.Name = "panel1";

            // === pictureBox1 (Avatar tròn xịn bằng Guna2) ===
            this.pictureBox1.Image = global::VuonVietXuStore.Properties.Resources.User;
            this.pictureBox1.Location = new System.Drawing.Point(80, 25);
            this.pictureBox1.Size = new System.Drawing.Size(65, 65);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;

            // === lblUsername ===
            this.lblUsername.Text = "Admin";
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblUsername.ForeColor = System.Drawing.Color.White;
            this.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUsername.Location = new System.Drawing.Point(0, 100);
            this.lblUsername.Size = new System.Drawing.Size(220, 26);
            this.lblUsername.Name = "lblUsername";

            // === lblRole ===
            this.lblRole.Text = "Quản trị viên";
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRole.ForeColor = System.Drawing.Color.FromArgb(160, 220, 180);
            this.lblRole.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRole.Location = new System.Drawing.Point(0, 126);
            this.lblRole.Size = new System.Drawing.Size(220, 22);
            this.lblRole.Name = "lblRole";

            // === panelDivider ===
            this.panelDivider.BackColor = System.Drawing.Color.FromArgb(60, 255, 255, 255);
            this.panelDivider.Location = new System.Drawing.Point(20, 158);
            this.panelDivider.Size = new System.Drawing.Size(180, 1);
            this.panelDivider.Name = "panelDivider";

            // --- Hàm mẫu cấu hình chung cho nút bấm Guna ---
            // (Các thuộc tính chung như Bo góc BorderRadius, màu Hover được thiết lập bên dưới)

            // === btnTrangChu ===
            this.btnTrangChu.Text = "🏠   Trang chủ";
            this.btnTrangChu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTrangChu.ForeColor = System.Drawing.Color.White;
            this.btnTrangChu.FillColor = System.Drawing.Color.Transparent;
            this.btnTrangChu.BorderRadius = 8; // Tạo bo góc nút mềm mại
            this.btnTrangChu.HoverState.FillColor = System.Drawing.Color.FromArgb(40, 255, 255, 255);
            this.btnTrangChu.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnTrangChu.TextOffset = new System.Drawing.Point(10, 0);
            this.btnTrangChu.Location = new System.Drawing.Point(10, 175);
            this.btnTrangChu.Size = new System.Drawing.Size(200, 42);
            this.btnTrangChu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTrangChu.Name = "btnTrangChu";
            this.btnTrangChu.Click += new System.EventHandler(this.btnTrangChu_Click);

            // === btnSanPham ===
            this.btnSanPham.Text = "📦   Sản phẩm";
            this.btnSanPham.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSanPham.ForeColor = System.Drawing.Color.FromArgb(220, 255, 220);
            this.btnSanPham.FillColor = System.Drawing.Color.Transparent;
            this.btnSanPham.BorderRadius = 8;
            this.btnSanPham.HoverState.FillColor = System.Drawing.Color.FromArgb(40, 255, 255, 255);
            this.btnSanPham.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnSanPham.TextOffset = new System.Drawing.Point(10, 0);
            this.btnSanPham.Location = new System.Drawing.Point(10, 222);
            this.btnSanPham.Size = new System.Drawing.Size(200, 42);
            this.btnSanPham.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSanPham.Name = "btnSanPham";
            this.btnSanPham.Click += new System.EventHandler(this.btnSanPham_Click);

            // === btnKH ===
            this.btnKH.Text = "👤   Khách hàng";
            this.btnKH.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnKH.ForeColor = System.Drawing.Color.FromArgb(220, 255, 220);
            this.btnKH.FillColor = System.Drawing.Color.Transparent;
            this.btnKH.BorderRadius = 8;
            this.btnKH.HoverState.FillColor = System.Drawing.Color.FromArgb(40, 255, 255, 255);
            this.btnKH.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnKH.TextOffset = new System.Drawing.Point(10, 0);
            this.btnKH.Location = new System.Drawing.Point(10, 269);
            this.btnKH.Size = new System.Drawing.Size(200, 42);
            this.btnKH.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKH.Name = "btnKH";
            this.btnKH.Click += new System.EventHandler(this.btnKH_Click);

            // === btnNCC ===
            this.btnNCC.Text = "🏭   Nhà cung cấp";
            this.btnNCC.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnNCC.ForeColor = System.Drawing.Color.FromArgb(220, 255, 220);
            this.btnNCC.FillColor = System.Drawing.Color.Transparent;
            this.btnNCC.BorderRadius = 8;
            this.btnNCC.HoverState.FillColor = System.Drawing.Color.FromArgb(40, 255, 255, 255);
            this.btnNCC.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnNCC.TextOffset = new System.Drawing.Point(10, 0);
            this.btnNCC.Location = new System.Drawing.Point(10, 316);
            this.btnNCC.Size = new System.Drawing.Size(200, 42);
            this.btnNCC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNCC.Name = "btnNCC";
            this.btnNCC.Click += new System.EventHandler(this.btnNCC_Click);

            // === btnNhapHang ===
            this.btnNhapHang.Text = "📥   Nhập hàng";
            this.btnNhapHang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnNhapHang.ForeColor = System.Drawing.Color.FromArgb(220, 255, 220);
            this.btnNhapHang.FillColor = System.Drawing.Color.Transparent;
            this.btnNhapHang.BorderRadius = 8;
            this.btnNhapHang.HoverState.FillColor = System.Drawing.Color.FromArgb(40, 255, 255, 255);
            this.btnNhapHang.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnNhapHang.TextOffset = new System.Drawing.Point(10, 0);
            this.btnNhapHang.Location = new System.Drawing.Point(10, 363);
            this.btnNhapHang.Size = new System.Drawing.Size(200, 42);
            this.btnNhapHang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNhapHang.Name = "btnNhapHang";
            this.btnNhapHang.Click += new System.EventHandler(this.btnNhapHang_Click);

            // === btnBanHang ===
            this.btnBanHang.Text = "🛒   Bán hàng";
            this.btnBanHang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnBanHang.ForeColor = System.Drawing.Color.FromArgb(220, 255, 220);
            this.btnBanHang.FillColor = System.Drawing.Color.Transparent;
            this.btnBanHang.BorderRadius = 8;
            this.btnBanHang.HoverState.FillColor = System.Drawing.Color.FromArgb(40, 255, 255, 255);
            this.btnBanHang.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnBanHang.TextOffset = new System.Drawing.Point(10, 0);
            this.btnBanHang.Location = new System.Drawing.Point(10, 410);
            this.btnBanHang.Size = new System.Drawing.Size(200, 42);
            this.btnBanHang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBanHang.Name = "btnBanHang";
            this.btnBanHang.Click += new System.EventHandler(this.btnBanHang_Click);

            // === btnKho ===
            this.btnKho.Text = "🏪   Kho hàng";
            this.btnKho.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnKho.ForeColor = System.Drawing.Color.FromArgb(220, 255, 220);
            this.btnKho.FillColor = System.Drawing.Color.Transparent;
            this.btnKho.BorderRadius = 8;
            this.btnKho.HoverState.FillColor = System.Drawing.Color.FromArgb(40, 255, 255, 255);
            this.btnKho.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnKho.TextOffset = new System.Drawing.Point(10, 0);
            this.btnKho.Location = new System.Drawing.Point(10, 457);
            this.btnKho.Size = new System.Drawing.Size(200, 42);
            this.btnKho.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKho.Name = "btnKho";
            this.btnKho.Click += new System.EventHandler(this.btnKho_Click);

            // === button1 (Báo cáo) ===
            this.button1.Text = "📊   Báo cáo";
            this.button1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.button1.ForeColor = System.Drawing.Color.FromArgb(220, 255, 220);
            this.button1.FillColor = System.Drawing.Color.Transparent;
            this.button1.BorderRadius = 8;
            this.button1.HoverState.FillColor = System.Drawing.Color.FromArgb(40, 255, 255, 255);
            this.button1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.button1.TextOffset = new System.Drawing.Point(10, 0);
            this.button1.Location = new System.Drawing.Point(10, 504);
            this.button1.Size = new System.Drawing.Size(200, 42);
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Name = "button1";
            this.button1.Click += new System.EventHandler(this.button1_Click);

            // === panelDivider2 ===
            this.panelDivider2.BackColor = System.Drawing.Color.FromArgb(60, 255, 255, 255);
            this.panelDivider2.Location = new System.Drawing.Point(20, 565);
            this.panelDivider2.Size = new System.Drawing.Size(180, 1);
            this.panelDivider2.Name = "panelDivider2";

            // === label1 (Đăng xuất) ===
            this.label1.Text = "⏻   Đăng xuất";
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(255, 140, 140);
            this.label1.Location = new System.Drawing.Point(20, 585);
            this.label1.Size = new System.Drawing.Size(180, 32);
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Name = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);

            // === panel2 (Vùng chứa trang chủ & Background) ===
            this.panel2.BackColor = System.Drawing.Color.FromArgb(242, 247, 244);
            // THÊM BACKGROUND: Bạn hãy thêm ảnh nền vào Resources rồi gọi ở đây nhé, ví dụ:
            // this.panel2.BackgroundImage = global::VuonVietXuStore.Properties.Resources.BackgroundHome;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(220, 0);
            this.panel2.Size = new System.Drawing.Size(880, 680);
            this.panel2.Name = "panel2";

            // === FormHome ===
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 680);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vườn Việt Xứ Store - Hệ thống quản lý chính";
            this.Name = "FormHome";
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