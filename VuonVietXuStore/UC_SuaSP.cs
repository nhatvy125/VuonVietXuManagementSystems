using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace VuonVietXuStore
{
    public partial class UC_SuaSP : UserControl
    {
        SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["VuonVietXuStore"].ConnectionString);

        int maSP = 0;
        public UC_SuaSP(
            int id,
            string tenSP,
            int soLuong,
            decimal giaNhap,
            decimal giaBan)
        {
            InitializeComponent();

            maSP = id;

            txtSP.Text = tenSP;

            txtSoLuong.Text = soLuong.ToString();

            txtGiaNhap.Text = giaNhap.ToString();

            txtGiaBan.Text = giaBan.ToString();
        }

        private void btnSave_Click( object sender,EventArgs e)
        {
<<<<<<< HEAD
            this.BackColor = Color.FromArgb(242, 247, 244);

            lblTitle.Text = "CẬP NHẬT THÔNG TIN SẢN PHẨM";
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(18, 78, 44);

            label1.Text = "Tên sản phẩm:";
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(64, 64, 64);

            lblMaVach.Text = "Mã vạch / Mã SP:";
            lblMaVach.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblMaVach.ForeColor = Color.FromArgb(64, 64, 64);

            label2.Text = "Số lượng tồn kho:";
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(64, 64, 64);

            label3.Text = "Giá nhập (VNĐ):";
            label3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(64, 64, 64);

            label4.Text = "Giá bán (VNĐ):";
            label4.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(64, 64, 64);

            txtSP.Font = new Font("Segoe UI", 11F);
            txtMaVach.Font = new Font("Segoe UI", 11F);
            txtSoLuong.Font = new Font("Segoe UI", 11F);
            txtGiaNhap.Font = new Font("Segoe UI", 11F);
            txtGiaBan.Font = new Font("Segoe UI", 11F);

            btnSave.Text = "💾 Lưu thay đổi";
            btnSave.BackColor = Color.FromArgb(18, 78, 44);
            btnSave.ForeColor = Color.White;
            btnSave.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Cursor = Cursors.Hand;

            btnCancel.Text = "❌ Hủy bỏ";
            btnCancel.BackColor = Color.FromArgb(127, 140, 141);
            btnCancel.ForeColor = Color.White;
            btnCancel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Cursor = Cursors.Hand;

            if (FormHome.CurrentRoleId == 3) // Nhân viên bán hàng
            {
                lblTitle.Text = "CHI TIẾT THÔNG TIN SẢN PHẨM";
                txtSP.ReadOnly = true;
                txtMaVach.ReadOnly = true;
                txtSoLuong.ReadOnly = true;
                txtGiaNhap.ReadOnly = true;
                txtGiaBan.ReadOnly = true;
                btnSave.Visible = false;
                btnCancel.Text = "🚪 Đóng";
                btnCancel.Location = new Point(170, 310);
            }
        }

        private void txtGiaTien_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt == null) return;

            txt.TextChanged -= txtGiaTien_TextChanged;
=======
>>>>>>> d3a71189be55522ceaaaddd58735ff0fdad75e66
            try
            {
                connect.Open();

                string query =
                "UPDATE SanPham " +
                "SET " +
                "TenSP = @ten, " +
                "SoLuongTon = @sl, " +
                "GiaNhap = @gn, " +
                "GiaBan = @gb " +
                "WHERE MaSP = @id";

                SqlCommand cmd =new SqlCommand(query, connect);

                cmd.Parameters.AddWithValue("@ten",txtSP.Text);

                cmd.Parameters.AddWithValue("@sl",int.Parse(txtSoLuong.Text));

                cmd.Parameters.AddWithValue("@gn", decimal.Parse(txtGiaNhap.Text));

                cmd.Parameters.AddWithValue("@gb", decimal.Parse(txtGiaBan.Text));

                cmd.Parameters.AddWithValue("@id",maSP);

                cmd.ExecuteNonQuery();

                connect.Close();

                UC_SanPham uc = new UC_SanPham();

                uc.Dock = DockStyle.Fill;

                Panel panel = this.Parent as Panel;

                panel.Controls.Clear();

                panel.Controls.Add(uc);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click( object sender, EventArgs e)
        {
            UC_SanPham uc = new UC_SanPham();

            uc.Dock = DockStyle.Fill;

            Panel panel = this.Parent as Panel;

            panel.Controls.Clear();

            panel.Controls.Add(uc);
        }
    }
}