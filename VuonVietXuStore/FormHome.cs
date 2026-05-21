using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace VuonVietXuStore
{
    public partial class FormHome : Form
    {
        int roleId;
        string username;
        string roleName;
        System.Collections.Generic.List<string> permissions = new System.Collections.Generic.List<string>();
        System.Data.SqlClient.SqlConnection connect = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["VuonVietXuStore"].ConnectionString);

        public FormHome(int roleId, string username, string roleName)
        {
            InitializeComponent();

            this.roleId = roleId;
            this.username = username;
            this.roleName = roleName;

            lblUsername.Text = username;
            lblRole.Text = roleName;

            this.FormClosed += FormHome_FormClosed;

            // Mặc định vừa mở phần mềm lên là nạp ngay trang Welcome và active nút Trang Chủ
            LoadUserControl(new UC_Welcomeback());
            SetActiveButton(btnTrangChu);
        }

        private void FormHome_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            LoadPermissions();
            ApplyPermission();
        }

        void LoadPermissions()
        {
            permissions.Clear();
            try
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT PermissionCode FROM UserPermissions WHERE Username=@username",
                    connect);
                cmd.Parameters.AddWithValue("@username", username);

                connect.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        permissions.Add(rd.GetString(0));
                    }
                }
                connect.Close();
            }
            catch
            {
                if (connect.State == System.Data.ConnectionState.Open) connect.Close();
            }

            if (permissions.Count == 0)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(
                        "SELECT PermissionCode FROM RolePermissions WHERE RoleId=@id",
                        connect);
                    cmd.Parameters.AddWithValue("@id", roleId);

                    connect.Open();
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            permissions.Add(rd.GetString(0));
                        }
                    }
                    connect.Close();
                }
                catch
                {
                    if (connect.State == System.Data.ConnectionState.Open) connect.Close();
                }
            }
        }

        void ApplyPermission()
        {
            btnSanPham.Visible = permissions.Contains("SP");
            btnNCC.Visible = permissions.Contains("NCC");
            btnKH.Visible = permissions.Contains("KHACH");
            btnNhapHang.Visible = permissions.Contains("NHAP");
            btnBanHang.Visible = permissions.Contains("BAN");
            btnKho.Visible = permissions.Contains("KHO");
            button1.Visible = permissions.Contains("BAO_CAO");
            btnSettings.Visible = permissions.Contains("CAI_DAT") || roleId == 1;

            RearrangeButtons();
        }

        private void RearrangeButtons()
        {
            Guna2Button[] buttons =
            {
                btnTrangChu,
                btnSanPham,
                btnKH,
                btnNCC,
                btnNhapHang,
                btnBanHang,
                btnKho,
                button1,
                btnSettings
            };

            // Lấy vị trí bắt đầu từ nút đầu tiên
            int top = btnTrangChu.Top;

            // Tự tính khoảng cách thật giữa 2 nút
            int spacing = btnSanPham.Top - (btnTrangChu.Top + btnTrangChu.Height);

            foreach (Guna2Button btn in buttons)
            {
                if (btn.Visible)
                {
                    btn.Top = top;
                    top += btn.Height + spacing;
                }
            }
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
            // Nếu sau này có UC_BanHang thì mở comment ra nhé:
            // LoadUserControl(new UC_BanHang());
            // SetActiveButton(btnBanHang);
        }

        private void btnKho_Click(object sender, EventArgs e)
        {
            // Nếu sau này có UC_KhoHang thì mở comment ra nhé:
            // LoadUserControl(new UC_KhoHang());
            // SetActiveButton(btnKho);
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

        private void btnSettings_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_CaiDat());
            SetActiveButton(btnSettings);
        }
    }

    public class FormPopupContainer : Form
    {
        public FormPopupContainer(UserControl uc, string title)
        {
            this.Text = title;
            this.Size = new System.Drawing.Size(uc.Width + 24, uc.Height + 48);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = System.Drawing.Color.FromArgb(242, 247, 244);

            uc.Dock = DockStyle.Fill;
            this.Controls.Add(uc);
        }
    }
}