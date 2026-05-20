using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void LoadUserControl(UserControl uc)
        {
            panel2.Controls.Clear();

            uc.Dock = DockStyle.Fill;

            panel2.Controls.Add(uc);

            uc.BringToFront();
        }

        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            UC_NhapHang uc = new UC_NhapHang();

            LoadUserControl(uc);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();

            login.Show();

            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UC_SanPham uc = new UC_SanPham();

            LoadUserControl(uc);
        }

        private void btnKH_Click(object sender, EventArgs e)
        {
            UC_KhachHang uc = new UC_KhachHang();

            LoadUserControl(uc);
        }

        private void btnNCC_Click(object sender, EventArgs e)
        {
            UC_NhaCungCap uc = new UC_NhaCungCap();

            LoadUserControl(uc);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UC_BaoCao uc = new UC_BaoCao();

            LoadUserControl(uc);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
