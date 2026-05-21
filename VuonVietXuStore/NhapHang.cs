// ============================================================
//  NhapHang.cs – CẬP NHẬT: Thêm nút Bán Hàng & Quản lý Kho
//
//  Thay thế phần thân NhapHang.cs hiện tại bằng code bên dưới.
//  Giữ nguyên NhapHang.Designer.cs (layout form).
//  Chỉ cần thêm 2 nút mới (btnBanHang, btnKho) vào Designer.
// ============================================================

using System;
using System.Windows.Forms;

namespace VuonVietXuStore
{
    public partial class NhapHang : Form
    {
        public NhapHang()
        {
            InitializeComponent();
            LoadUserControl(new UC_Welcomeback());
        }

        // ─── Helper: Load UserControl vào panel chính ─────────────────────
        private void LoadUserControl(UserControl uc)
        {
            panel2.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panel2.Controls.Add(uc);
            uc.BringToFront();
        }

        // ─── Navigation Buttons ───────────────────────────────────────────

        private void btnNhapHang_Click(object sender, EventArgs e)
            => LoadUserControl(new UC_NhapHang());

        private void button5_Click(object sender, EventArgs e)   // Sản phẩm
            => LoadUserControl(new UC_SanPham());

        private void btnKH_Click(object sender, EventArgs e)
            => LoadUserControl(new UC_KhachHang());

        private void btnNCC_Click(object sender, EventArgs e)
            => LoadUserControl(new UC_NhaCungCap());

        private void button1_Click(object sender, EventArgs e)   // Báo cáo
            => LoadUserControl(new UC_BaoCao());

        // ─── NÚT MỚI: Bán Hàng ───────────────────────────────────────────
        /// <summary>
        /// Thêm button btnBanHang vào NhapHang.Designer.cs và gán event này.
        /// </summary>
        private void btnBanHang_Click(object sender, EventArgs e)
            => LoadUserControl(new UC_BanHang());

        // ─── NÚT MỚI: Quản lý Kho ────────────────────────────────────────
        /// <summary>
        /// Thêm button btnKho vào NhapHang.Designer.cs và gán event này.
        /// </summary>
        private void btnKho_Click(object sender, EventArgs e)
            => LoadUserControl(new UC_QuanLyKho());

        // ─── Đăng xuất ───────────────────────────────────────────────────
        private void label1_Click(object sender, EventArgs e)
        {
            var login = new Form1();
            login.Show();
            this.Hide();
        }
    }
}
