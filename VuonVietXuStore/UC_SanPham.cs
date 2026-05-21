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
            LoadDataSP();
            // === TỰ ĐỘNG GIÃN BẢNG VỪA KHUNG MÀN HÌNH ===
            // 1. Ép tất cả các cột tự chia tỷ lệ và giãn đều ra hết chiều rộng của bảng
            dgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 2. Tự động co giãn bảng theo chiều ngang và chiều dọc khi UserControl thay đổi kích thước
            dgvSanPham.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // 3. Tự động bo giãn nút "Thêm sản phẩm" luôn nằm góc bên phải màn hình
            btnThem.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // 4. Đăng ký sự kiện Enter trong ô tìm kiếm
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
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
            UC_ThemSP uc = new UC_ThemSP();
            using (FormPopupContainer popup = new FormPopupContainer(uc, "Thêm sản phẩm mới"))
            {
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    LoadDataSP();
                }
            }
        }

        private void PerformSearch()
        {
            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadDataSP();
            }
            else
            {
                SearchDataSP(keyword);
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Chặn tiếng bíp của Windows khi nhấn Enter
                PerformSearch();
            }
        }

        private void SearchDataSP(string keyword)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    string query = "SELECT MaSP, TenSP, SoLuongTon, GiaNhap, GiaBan FROM SanPham WHERE TenSP LIKE @keyword OR MaSP LIKE @keyword";
                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dgvSanPham.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}