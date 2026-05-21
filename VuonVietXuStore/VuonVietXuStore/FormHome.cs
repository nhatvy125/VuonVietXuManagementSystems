using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace VuonVietXuStore
{
    public partial class FormHome : Form
    {
        public FormHome()
        {
            InitializeComponent();

            // Mặc định vừa mở phần mềm lên là nạp ngay trang Welcome và active nút Trang Chủ
            LoadUserControl(new UC_Welcomeback());
            SetActiveButton(btnTrangChu);
        }

        // Hàm lõi để xóa và nạp UserControl mới vào vùng trống bên phải (panel2)
        private void LoadUserControl(UserControl uc)
        {
            panel2.Controls.Clear();
            uc.Dock = DockStyle.Fill;

            // Xử lý để giữ background của panel2 không bị đè mất (nếu UserControl có màu Transparent)
            uc.BackColor = Color.Transparent;

            panel2.Controls.Add(uc);
            uc.BringToFront();
        }

        // Hàm đổi hiệu ứng màu sắc để nhận biết nút đang được chọn
        private void SetActiveButton(Guna2Button activeButton)
        {
            // Reset toàn bộ nút về trong suốt
            foreach (Control ctrl in panel1.Controls)
            {
                if (ctrl is Guna2Button btn)
                {
                    btn.FillColor = Color.Transparent;
                    btn.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                }
            }
            // Tô màu nền highlight cho nút đang chọn
            activeButton.FillColor = Color.FromArgb(40, 255, 255, 255);
            activeButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_Welcomeback());
            SetActiveButton(btnTrangChu);
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_SanPham());
            SetActiveButton(btnSanPham);
        }

        private void btnKH_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_KhachHang());
            SetActiveButton(btnKH);
        }

        private void btnNCC_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_NhaCungCap());
            SetActiveButton(btnNCC);
        }

        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_NhapHang());
            SetActiveButton(btnNhapHang);
        }

        private void btnBanHang_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_BanHang());
            SetActiveButton(btnBanHang);
        }

        private void btnKho_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_QuanLyKho());
            SetActiveButton(btnKho);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_BaoCao());
            SetActiveButton(button1);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn đăng xuất không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Form1 login = new Form1();
                login.Show();
                this.Hide();
            }
        }
    }
}