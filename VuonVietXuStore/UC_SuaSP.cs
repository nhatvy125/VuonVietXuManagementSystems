using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VuonVietXuStore
{
    public partial class UC_SuaSP : UserControl
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["VuonVietXuStore"].ConnectionString;
        private readonly int maSP;

        public UC_SuaSP(
            int id,
            string maVach,
            string tenSP,
            int soLuong,
            decimal giaNhap,
            decimal giaBan)
        {
            InitializeComponent();
            this.maSP = id;

            txtSP.Text = tenSP;
            txtMaVach.Text = maVach;
            txtSoLuong.Text = soLuong.ToString();
            txtGiaNhap.Text = giaNhap.ToString("N0");
            txtGiaBan.Text = giaBan.ToString("N0");

            SetupStyles();
            
            // Format input keys as they type
            txtGiaNhap.TextChanged += txtGiaTien_TextChanged;
            txtGiaBan.TextChanged += txtGiaTien_TextChanged;
        }

        private void SetupStyles()
        {
            this.BackColor = Color.FromArgb(242, 247, 244);

            // Title
            lblTitle.Text = "CẬP NHẬT THÔNG TIN SẢN PHẨM";
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(18, 78, 44);

            // Labels
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

            // TextBoxes
            txtSP.Font = new Font("Segoe UI", 11F);
            txtMaVach.Font = new Font("Segoe UI", 11F);
            txtSoLuong.Font = new Font("Segoe UI", 11F);
            txtGiaNhap.Font = new Font("Segoe UI", 11F);
            txtGiaBan.Font = new Font("Segoe UI", 11F);

            // Buttons
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
        }

        private void txtGiaTien_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt == null) return;

            txt.TextChanged -= txtGiaTien_TextChanged;
            try
            {
                string value = txt.Text.Replace(",", "");
                if (!string.IsNullOrEmpty(value))
                {
                    decimal number = decimal.Parse(value);
                    txt.Text = string.Format("{0:N0}", number);
                    txt.SelectionStart = txt.Text.Length;
                }
            }
            catch { }
            txt.TextChanged += txtGiaTien_TextChanged;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSP.Text) || string.IsNullOrWhiteSpace(txtSoLuong.Text) ||
                string.IsNullOrWhiteSpace(txtGiaNhap.Text) || string.IsNullOrWhiteSpace(txtGiaBan.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string giaNhapChuan = txtGiaNhap.Text.Replace(",", "");
            string giaBanChuan = txtGiaBan.Text.Replace(",", "");

            if (!int.TryParse(txtSoLuong.Text, out int soLuong) ||
                !decimal.TryParse(giaNhapChuan, out decimal giaNhap) ||
                !decimal.TryParse(giaBanChuan, out decimal giaBan))
            {
                MessageBox.Show("Số lượng và đơn giá phải là số hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    string query = @"
                        UPDATE SanPham 
                        SET 
                            TenSP = @ten, 
                            SoLuongTon = @sl, 
                            GiaNhap = @gn, 
                            GiaBan = @gb,
                            MaVach = @mavach
                        WHERE MaSP = @id";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@ten", txtSP.Text.Trim());
                        cmd.Parameters.AddWithValue("@sl", soLuong);
                        cmd.Parameters.AddWithValue("@gn", giaNhap);
                        cmd.Parameters.AddWithValue("@gb", giaBan);
                        cmd.Parameters.AddWithValue("@mavach", txtMaVach.Text.Trim());
                        cmd.Parameters.AddWithValue("@id", maSP);

                        connect.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        private void GoBack()
        {
            UC_SanPham uc = new UC_SanPham();
            uc.Dock = DockStyle.Fill;
            Panel panel = this.Parent as Panel;
            if (panel != null)
            {
                panel.Controls.Clear();
                panel.Controls.Add(uc);
            }
        }
    }
}