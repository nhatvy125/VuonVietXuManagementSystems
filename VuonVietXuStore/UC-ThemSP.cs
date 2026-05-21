using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VuonVietXuStore
{
    public partial class UC_ThemSP : UserControl
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["VuonVietXuStore"].ConnectionString;

        public UC_ThemSP()
        {
            InitializeComponent();
            LoadDanhMuc();
            SetupHoverEffects();

            // === ĐĂNG KÝ SỰ KIỆN TỰ ĐỘNG PHẨY PHÂN CÁCH HÀNG NGHÌN ===
            txtGiaNhap.TextChanged += txtGiaTien_TextChanged;
            txtGiaBan.TextChanged += txtGiaTien_TextChanged;
        }

        // Tự động căn giữa khung Card nhập liệu khi UserControl co giãn theo màn hình chính
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (panelCard != null)
            {
                panelCard.Left = (this.Width - panelCard.Width) / 2;
                panelCard.Top = (this.Height - panelCard.Height) / 2;
            }
        }

        // Thêm hiệu ứng đổi màu khi rê chuột vào các nút cho sinh động
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

        // === HÀM TỰ ĐỘNG PHẨY NGĂN CÁCH KHI ĐANG GÕ PHÍM ===
        private void txtGiaTien_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt == null) return;

            // Tạm thời hủy đăng ký sự kiện để tránh vòng lặp vô hạn khi gán lại Text
            txt.TextChanged -= txtGiaTien_TextChanged;

            try
            {
                // Xóa bỏ dấu phẩy cũ nếu có trước khi định dạng lại
                string value = txt.Text.Replace(",", "");

                if (!string.IsNullOrEmpty(value))
                {
                    // Chuyển chuỗi thành số thuần túy và định dạng lại thành chuỗi có dấu phẩy (N0)
                    decimal number = decimal.Parse(value);
                    txt.Text = string.Format("{0:N0}", number);

                    // Đẩy con trỏ chuột về cuối văn bản để người dùng gõ tiếp không bị ngược
                    txt.SelectionStart = txt.Text.Length;
                }
            }
            catch
            {
                // Bỏ qua nếu có lỗi ký tự lạ
            }

            // Đăng ký lại sự kiện sau khi xử lý xong văn bản
            txt.TextChanged += txtGiaTien_TextChanged;
        }

        private void LoadDanhMuc()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    string query = "SELECT MaDanhMuc, TenDanhMuc FROM DanhMuc";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connect))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        cboDanhMuc.DataSource = dt;
                        cboDanhMuc.DisplayMember = "TenDanhMuc";
                        cboDanhMuc.ValueMember = "MaDanhMuc";
                        cboDanhMuc.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // === VALIDATION: Kiểm tra xem người dùng đã nhập đủ thông tin chưa ===
            if (string.IsNullOrWhiteSpace(txtSP.Text) || cboDanhMuc.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txtSoLuong.Text) || string.IsNullOrWhiteSpace(txtGiaNhap.Text) || string.IsNullOrWhiteSpace(txtGiaBan.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tất cả các thông tin sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // LOẠI BỎ DẤU PHẨY ĐỂ ĐƯA VỀ ĐỊNH DẠNG SỐ CHUẨN TRƯỚC KHI LƯU VÀO DATABASE
            string giaNhapChuan = txtGiaNhap.Text.Replace(",", "");
            string giaBanChuan = txtGiaBan.Text.Replace(",", "");

            // Kiểm tra định dạng số dựa trên chuỗi đã được làm sạch dấu phẩy
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
                    string query = "INSERT INTO SanPham (TenSP, MaDanhMuc, SoLuongTon, GiaNhap, GiaBan) " +
                                   "VALUES (@tensp, @madm, @soluong, @gianhap, @giaban)";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@tensp", txtSP.Text.Trim());
                        cmd.Parameters.AddWithValue("@madm", cboDanhMuc.SelectedValue);
                        cmd.Parameters.AddWithValue("@soluong", soLuong);
                        cmd.Parameters.AddWithValue("@gianhap", giaNhap); // Số thuần túy hợp lệ để lưu vào SQL
                        cmd.Parameters.AddWithValue("@giaban", giaBan);   // Số thuần túy hợp lệ để lưu vào SQL

                        connect.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Thêm sản phẩm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                QuayVeTrangSanPham();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            QuayVeTrangSanPham();
        }

        private void QuayVeTrangSanPham()
        {
            Form parentForm = this.FindForm();
            if (parentForm != null && parentForm.GetType().Name == "FormPopupContainer")
            {
                parentForm.DialogResult = DialogResult.OK;
                parentForm.Close();
                return;
            }

            UC_SanPham uc = new UC_SanPham();
            uc.Dock = DockStyle.Fill;

            Panel parentPanel = this.Parent as Panel;
            if (parentPanel != null)
            {
                parentPanel.Controls.Clear();
                parentPanel.Controls.Add(uc);
                uc.BringToFront();
            }
        }
    }
}