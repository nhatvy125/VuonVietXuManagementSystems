using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;

namespace VuonVietXuStore
{
    public partial class Form1 : Form
    {
        // Khởi tạo kết nối từ App.config
        SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["VuonVietXuStore"].ConnectionString);

        public Form1()
        {
            InitializeComponent();
            // Đảm bảo mật khẩu bị ẩn khi vừa mở form lên
            txtPassword.UseSystemPasswordChar = true;
        }

        public bool checkConnection()
        {
            if (connect.State == ConnectionState.Closed)
            {
                return true;
            }
            return false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Bổ sung: Kiểm tra nhập trống để tránh gọi SQL Server không cần thiết
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
                        SELECT u.username, u.RoleId, r.RoleName
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
                            string username = table.Rows[0]["username"].ToString();
                            string roleName = table.Rows[0]["RoleName"].ToString();

                            FormHome home = new FormHome(roleId, username, roleName);
                            home.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Sai tên tài khoản hoặc mật khẩu!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Lấy ex.Message để thông báo gọn gàng hơn
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
                pictureBox1.Image = Properties.Resources.Eye; // File ảnh mắt mở của bạn
                isShowPassword = true;
            }
        }

        private void panelLeft_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}