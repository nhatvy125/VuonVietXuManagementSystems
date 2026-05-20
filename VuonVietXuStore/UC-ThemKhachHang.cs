using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VuonVietXuStore
{
    public partial class UC_ThemKhachHang : UserControl
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["VuonVietXuStore"].ConnectionString;

        public UC_ThemKhachHang()
        {
            InitializeComponent();
            SetupHoverEffects();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (panelCard != null)
            {
                panelCard.Left = (this.Width - panelCard.Width) / 2;
                panelCard.Top = (this.Height - panelCard.Height) / 2;
            }
        }

        private void SetupHoverEffects()
        {
            btnSave.MouseEnter += (s, e) => btnSave.BackColor = Color.FromArgb(25, 110, 62);
            btnSave.MouseLeave += (s, e) => btnSave.BackColor = Color.FromArgb(18, 78, 44);

            btnCancel.MouseEnter += (s, e) => {
                btnCancel.BackColor = Color.FromArgb(231, 76, 60);
                btnCancel.ForeColor = Color.White;
            };
            btnCancel.MouseLeave += (s, e) => {
                btnCancel.BackColor = Color.FromArgb(220, 220, 220);
                btnCancel.ForeColor = Color.FromArgb(60, 60, 60);
            };
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKH.Text) || string.IsNullOrWhiteSpace(txtSDT.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên và số điện thoại khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    connect.Open();
                    string query = "INSERT INTO KhachHang (TenKH, SDT, DiaChiKH, DiemTichLuy) VALUES (@tenkh, @sdt, @diachi, @diem)";
                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@tenkh", txtKH.Text.Trim());
                        cmd.Parameters.AddWithValue("@sdt", txtSDT.Text.Trim());
                        cmd.Parameters.AddWithValue("@diachi", txtDiaChi.Text.Trim());
                        cmd.Parameters.AddWithValue("@diem", (int)numDiemTichLuy.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Thêm khách hàng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                QuayVeTrangKhachHang();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            QuayVeTrangKhachHang();
        }

        private void QuayVeTrangKhachHang()
        {
            UC_KhachHang uc = new UC_KhachHang();
            uc.Dock = DockStyle.Fill;
            Panel panel = this.Parent as Panel;
            if (panel != null)
            {
                panel.Controls.Clear();
                panel.Controls.Add(uc);
                uc.BringToFront();
            }
        }
    }
}
