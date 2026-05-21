using Guna.UI2.WinForms;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace VuonVietXuStore
{
    public partial class FormHome : Form
    {
        private Form1 loginForm;
        private bool isLoggingOut = false;

        int roleId;
        string loginUsername;
        string displayName;
        string roleName;
        System.Collections.Generic.List<string> permissions = new System.Collections.Generic.List<string>();
        System.Data.SqlClient.SqlConnection connect = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["VuonVietXuStore"].ConnectionString);

        public FormHome(Form1 loginForm, int roleId, string loginUsername, string displayName, string roleName)
        {
            InitializeComponent();

            this.loginForm = loginForm;
            this.roleId = roleId;
            this.loginUsername = loginUsername;
            this.displayName = displayName;
            this.roleName = roleName;

            lblUsername.Text = displayName;
            lblRole.Text = roleName;
            LoadAvatar();

            LoadUserControl(new UC_Welcomeback());
            SetActiveButton(btnTrangChu);
            this.FormClosed += FormHome_FormClosed;
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
                connect.Open();

                // Ưu tiên quyền
                SqlCommand cmd = new SqlCommand(
                    "SELECT PermissionCode FROM UserPermissions WHERE Username=@username",
                    connect);
                cmd.Parameters.AddWithValue("@username", loginUsername);

                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        permissions.Add(rd.GetString(0).Trim());
                    }
                }

                // Không có quyền thì lấy theo chức vụ
                if (permissions.Count == 0)
                {
                    SqlCommand cmdRole = new SqlCommand(
                        "SELECT PermissionCode FROM RolePermissions WHERE RoleId=@id",
                        connect);
                    cmdRole.Parameters.AddWithValue("@id", roleId);

                    using (SqlDataReader rd = cmdRole.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            permissions.Add(rd.GetString(0).Trim());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connect.State == System.Data.ConnectionState.Open)
                    connect.Close();
            }
        }

        void ApplyPermission()
        {
            btnSanPham.Visible = permissions.Contains("SP");
            btnDonHang.Visible = permissions.Contains("DON_HANG");
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
                btnDonHang,
                btnNCC,
                btnNhapHang,
                btnBanHang,
                btnKho,
                button1,
                btnSettings
            };

            int top = btnTrangChu.Top;
            int spacing = 4;

            Guna2Button lastVisibleButton = null;

            foreach (Guna2Button btn in buttons)
            {
                if (btn.Visible)
                {
                    btn.Top = top;

                    top += btn.Height + spacing;

                    lastVisibleButton = btn;
                }
            }

            if (lastVisibleButton != null)
            {
                panelDivider2.Top = lastVisibleButton.Bottom + 10;
                label1.Top = panelDivider2.Bottom + 3;
            }
        }

        private void LoadAvatar()
        {
            try
            {
                connect.Open();

                string query = "SELECT AvatarPath FROM users WHERE username = @username";

                SqlCommand cmd = new SqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@username", loginUsername);

                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    string imagePath = result.ToString();

                    if (File.Exists(imagePath))
                    {
                        pictureBox1.Image = Image.FromFile(imagePath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load avatar: " + ex.Message);
            }
            finally
            {
                if (connect.State == System.Data.ConnectionState.Open)
                {
                    connect.Close();
                }
            }
        }

        private void LoadUserControl(UserControl uc)
        {
            panel2.Controls.Clear();
            uc.Dock = DockStyle.Fill;

            uc.BackColor = Color.Transparent;

            panel2.Controls.Add(uc);
            uc.BringToFront();
        }

        private void SetActiveButton(Guna2Button activeButton)
        {
            foreach (Control ctrl in panel1.Controls)
            {
                if (ctrl is Guna2Button btn)
                {
                    btn.FillColor = Color.Transparent;
                    btn.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                }
            }
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

        private void btnDonHang_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_DonHang());
            SetActiveButton(btnDonHang);
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

        private void btnSettings_Click(object sender, EventArgs e)
        {
            LoadUserControl(new UC_CaiDat());
            SetActiveButton(btnSettings);
        }

        private void label1_Click(object sender, EventArgs e)
        {
                isLoggingOut = true;
                if (loginForm != null)
                {
                    loginForm.Show();
                }
                else
                {
                    Form1 login = new Form1();
                    login.Show();
                }
                this.Close();
        }

        private void FormHome_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!isLoggingOut)
            {
                Application.Exit();
            }
        }

        private void lblRole_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png";

            ofd.InitialDirectory = @"C:\Users\Tram Nguyen\Desktop\VuonVietXuManagementSystems\VuonVietXuStore\Resources";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string selectedImage = ofd.FileName;

                    if (connect.State == System.Data.ConnectionState.Open)
                    {
                        connect.Close();
                    }

                    connect.Open();

                    string query = "UPDATE users SET AvatarPath = @path WHERE username = @username";

                    SqlCommand cmd = new SqlCommand(query, connect);

                    cmd.Parameters.AddWithValue("@path", selectedImage);
                    cmd.Parameters.AddWithValue("@username", loginUsername);

                    cmd.ExecuteNonQuery();

                    connect.Close();

                    pictureBox1.Image = Image.FromFile(selectedImage);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi đổi avatar: " + ex.Message);
                }
                finally
                {
                    if (connect.State == System.Data.ConnectionState.Open)
                    {
                        connect.Close();
                    }
                }
            }
        }
    }
}