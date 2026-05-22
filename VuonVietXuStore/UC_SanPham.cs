using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace VuonVietXuStore
{
    public partial class UC_SanPham : UserControl
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["VuonVietXuStore"].ConnectionString;

        public UC_SanPham()
        {
            InitializeComponent();
<<<<<<< HEAD
            
            
            this.txtSearch.TextChanged += txtSearch_TextChanged;
            this.dgvSanPham.CellClick += dgvSanPham_CellClick;

            btnSearch.BackColor = Color.FromArgb(18, 78, 44);
            btnSearch.ForeColor = Color.White;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSearch.Cursor = Cursors.Hand;

            cboSearchBy.Items.Clear();
            cboSearchBy.Items.AddRange(new object[] {
                "Mã sản phẩm",
                "Tên sản phẩm",
                "Mã vạch",
                "Danh mục"
            });
            cboSearchBy.SelectedIndex = 0; 

            cboSearchBy.SelectedIndexChanged += (s, e) => UpdateSearchPlaceholder();

            this.Load += (s, e) => UpdateSearchPlaceholder();

            cboSort.Items.Clear();
            cboSort.Items.AddRange(new object[] {
                "Mặc định (Mã mới nhất)",
                "Tên A → Z",
                "Tên Z → A",
                "Giá bán tăng dần",
                "Giá bán giảm dần",
                "Tồn kho tăng dần",
                "Tồn kho giảm dần"
            });
            cboSort.SelectedIndex = 0;
            cboSort.SelectedIndexChanged += (s, e) => LoadDataSP();

            btnSearch.Click += (s, e) => LoadDataSP();
            txtSearch.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    LoadDataSP();
                }
            };

            dgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSanPham.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnThem.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            if (FormHome.CurrentRoleId == 3)
            {
                btnThem.Visible = false;
            }

=======
>>>>>>> d3a71189be55522ceaaaddd58735ff0fdad75e66
            LoadDataSP();
            // === TỰ ĐỘNG GIÃN BẢNG VỪA KHUNG MÀN HÌNH ===
            // 1. Ép tất cả các cột tự chia tỷ lệ và giãn đều ra hết chiều rộng của bảng
            dgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 2. Tự động co giãn bảng theo chiều ngang và chiều dọc khi UserControl thay đổi kích thước
            dgvSanPham.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // 3. Tự động bo giãn nút "Thêm sản phẩm" luôn nằm góc bên phải màn hình
            btnThem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        }

        private void LoadDataSP()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    // Truy vấn lấy danh sách sản phẩm hiển thị lên bảng
                    string query = "SELECT MaSP, TenSP, SoLuongTon, GiaNhap, GiaBan FROM SanPham";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connect))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvSanPham.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách sản phẩm: " + ex.Message);
            }
        }

        // === NÚT THÊM SẢN PHẨM HOẠT ĐỘNG CHUẨN UX ===
        private void btnThem_Click(object sender, EventArgs e)
        {
            // Tạo đối tượng màn hình Thêm sản phẩm mới
            UC_ThemSP uc = new UC_ThemSP();
            uc.Dock = DockStyle.Fill;

            // Tìm Panel cha đang chứa UC_SanPham hiện tại (chính là panel2 của FormHome)
            Panel parentPanel = this.Parent as Panel;
            if (parentPanel != null)
            {
<<<<<<< HEAD
                LoadDataSP();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadDataSP();
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvSanPham.Rows[e.RowIndex];

            int id = Convert.ToInt32(row.Cells["MaSP"].Value);
            string maVach = Convert.ToString(row.Cells["MaVach"].Value ?? "");
            string tenSP = Convert.ToString(row.Cells["TenSP"].Value ?? "");
            int soLuong = Convert.ToInt32(row.Cells["SoLuongTon"].Value ?? 0);
            decimal giaNhap = Convert.ToDecimal(row.Cells["GiaNhap"].Value ?? 0);
            decimal giaBan = Convert.ToDecimal(row.Cells["GiaBan"].Value ?? 0);

            UC_SuaSP uc = new UC_SuaSP(id, maVach, tenSP, soLuong, giaNhap, giaBan);
            string title = (FormHome.CurrentRoleId == 3) ? "Chi Tiết Thông Tin Sản Phẩm" : "Sửa Thông Tin Sản Phẩm";
            FormPopup popup = new FormPopup(uc, title);
            if (popup.ShowDialog() == DialogResult.OK)
            {
                LoadDataSP();
=======
                parentPanel.Controls.Clear();     // Xóa màn hình danh sách cũ
                parentPanel.Controls.Add(uc);     // Đẩy giao diện Thêm sản phẩm mới vào
                uc.BringToFront();
>>>>>>> d3a71189be55522ceaaaddd58735ff0fdad75e66
            }
        }
    }
}