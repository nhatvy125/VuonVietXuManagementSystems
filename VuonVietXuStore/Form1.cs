using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using System.Configuration;

namespace VuonVietXuStore
{
    public partial class Form1 : Form
    {
        SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["VuonVietXuStore"].ConnectionString);

        public Form1()
        {
            InitializeComponent();
            txtPassword.UseSystemPasswordChar = true;
            btnLogin.MouseEnter += (s, e) => btnLogin.BackColor = Color.FromArgb(22, 110, 68);
            btnLogin.MouseLeave += (s, e) => btnLogin.BackColor = Color.FromArgb(13, 74, 46);

            // Enter để đăng nhập
            txtPassword.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) btnLogin_Click(s, e); };
            txtUsername.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) btnLogin_Click(s, e); };

            LoadLoginBackground();
        }

        private void LoadLoginBackground()
        {
            string[] paths = new[]
            {
                Path.Combine(Application.StartupPath, "Resources", "login_bg.png"),
                Path.Combine(Application.StartupPath, "login_bg.png"),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "login_bg.png"),
            };
            foreach (var p in paths)
            {
                if (File.Exists(p))
                {
                    try { picLogo.Image = Image.FromFile(p); break; } catch { }
                }
            }
        }

        public bool checkConnection()
        {
            return connect.State == ConnectionState.Closed;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên tài khoản và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (checkConnection())
            {
                try
                {
                    connect.Open();
                    string selectData = @"
                        SELECT u.username, u.TenNV, u.RoleId, r.RoleName
                        FROM Users u
                        JOIN Roles r ON u.RoleId = r.RoleId
                        WHERE u.username = @usern AND u.password = @pass";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        cmd.Parameters.AddWithValue("@usern", txtUsername.Text.Trim());
                        cmd.Parameters.AddWithValue("@pass", txtPassword.Text.Trim());

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        if (table.Rows.Count > 0)
                        {
                            int roleId = Convert.ToInt32(table.Rows[0]["RoleId"]);
                            string loginUsername = table.Rows[0]["username"].ToString();
                            string displayName = table.Rows[0]["TenNV"].ToString();
                            string roleName = table.Rows[0]["RoleName"].ToString();

                            FormHome home = new FormHome(this, roleId, loginUsername, displayName, roleName);
                            home.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Sai tên tài khoản hoặc mật khẩu!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtPassword.Clear();
                            txtPassword.Focus();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kết nối thất bại: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        private bool isShowPassword = false;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (isShowPassword)
            {
                txtPassword.UseSystemPasswordChar = true;
                pictureBox1.Image = Properties.Resources.eyeclose;
                isShowPassword = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = false;
                pictureBox1.Image = Properties.Resources.Eye;
                isShowPassword = true;
            }
        }

        private void panelLeft_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                panelLeft.ClientRectangle,
                Color.FromArgb(8, 50, 30),
                Color.FromArgb(30, 100, 60),
                LinearGradientMode.ForwardDiagonal))
            {
                e.Graphics.FillRectangle(brush, panelLeft.ClientRectangle);
            }

            using (SolidBrush circleBrush = new SolidBrush(Color.FromArgb(30, 255, 255, 255)))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.FillEllipse(circleBrush, 250, 420, 280, 280);
                e.Graphics.FillEllipse(circleBrush, -80, 440, 220, 220);
            }
        }

        private void lblTagline_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
    }
}
