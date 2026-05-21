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
                parentPanel.Controls.Clear();     // Xóa màn hình danh sách cũ
                parentPanel.Controls.Add(uc);     // Đẩy giao diện Thêm sản phẩm mới vào
                uc.BringToFront();
            }
        }
    }
}