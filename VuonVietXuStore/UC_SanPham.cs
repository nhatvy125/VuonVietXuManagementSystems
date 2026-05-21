using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VuonVietXuStore
{
    public partial class UC_SanPham : UserControl
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["VuonVietXuStore"].ConnectionString;

        public UC_SanPham()
        {
            InitializeComponent();
            
            
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

            LoadDataSP();
        }

        // Cập nhật gợi ý trong ô tìm kiếm theo lựa chọn
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        private void UpdateSearchPlaceholder()
        {
            string hint;
            switch (cboSearchBy.SelectedItem?.ToString())
            {
                case "Mã sản phẩm":  hint = "Nhập mã sản phẩm..."; break;
                case "Tên sản phẩm": hint = "Nhập tên sản phẩm..."; break;
                case "Mã vạch":      hint = "Nhập mã vạch (barcode)..."; break;
                case "Danh mục":     hint = "Nhập tên danh mục..."; break;
                default:             hint = "Nhập từ khóa..."; break;
            }
            SendMessage(txtSearch.Handle, EM_SETCUEBANNER, IntPtr.Zero, hint);
        }

        private void LoadDataSP()
        {
            try
            {
                string searchVal = txtSearch.Text.Trim();
                string sortOrder = "sp.MaSP DESC";

                switch (cboSort.SelectedIndex)
                {
                    case 1: sortOrder = "sp.TenSP ASC"; break;
                    case 2: sortOrder = "sp.TenSP DESC"; break;
                    case 3: sortOrder = "sp.GiaBan ASC"; break;
                    case 4: sortOrder = "sp.GiaBan DESC"; break;
                    case 5: sortOrder = "sp.SoLuongTon ASC"; break;
                    case 6: sortOrder = "sp.SoLuongTon DESC"; break;
                }

                string selectedSearch = cboSearchBy.SelectedItem?.ToString() ?? "Mã sản phẩm";
                string query;

                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    if (selectedSearch == "Danh mục")
                    {
                        query = $@"SELECT sp.MaSP, sp.MaVach, sp.TenSP, dm.TenDanhMuc, sp.SoLuongTon, sp.GiaNhap, sp.GiaBan
                                   FROM SanPham sp
                                   LEFT JOIN DanhMuc dm ON sp.MaDanhMuc = dm.MaDanhMuc
                                   WHERE dm.TenDanhMuc LIKE @search
                                   ORDER BY {sortOrder}";
                    }
                    else
                    {
                        string column = selectedSearch == "Mã sản phẩm" ? "CAST(sp.MaSP AS VARCHAR)"
                                      : selectedSearch == "Tên sản phẩm" ? "sp.TenSP"
                                      : "sp.MaVach";

                        query = $@"SELECT sp.MaSP, sp.MaVach, sp.TenSP, dm.TenDanhMuc, sp.SoLuongTon, sp.GiaNhap, sp.GiaBan
                                   FROM SanPham sp
                                   LEFT JOIN DanhMuc dm ON sp.MaDanhMuc = dm.MaDanhMuc
                                   WHERE {column} LIKE @search
                                   ORDER BY {sortOrder}";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + searchVal + "%");
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dgvSanPham.DataSource = dt;
                        }
                    }
                }
                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatGrid()
        {
            if (dgvSanPham.Columns["MaSP"] != null)     { dgvSanPham.Columns["MaSP"].HeaderText = "Mã SP"; dgvSanPham.Columns["MaSP"].Width = 70; }
            if (dgvSanPham.Columns["MaVach"] != null)   { dgvSanPham.Columns["MaVach"].HeaderText = "Mã Vạch"; }
            if (dgvSanPham.Columns["TenSP"] != null)    { dgvSanPham.Columns["TenSP"].HeaderText = "Tên Sản Phẩm"; }
            if (dgvSanPham.Columns["TenDanhMuc"] != null) { dgvSanPham.Columns["TenDanhMuc"].HeaderText = "Danh Mục"; }

            if (dgvSanPham.Columns["SoLuongTon"] != null)
            {
                dgvSanPham.Columns["SoLuongTon"].HeaderText = "Tồn Kho";
                dgvSanPham.Columns["SoLuongTon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvSanPham.Columns["SoLuongTon"].Width = 90;
            }

            if (dgvSanPham.Columns["GiaNhap"] != null)
            {
                dgvSanPham.Columns["GiaNhap"].HeaderText = "Giá Nhập";
                dgvSanPham.Columns["GiaNhap"].DefaultCellStyle.Format = "N0";
                dgvSanPham.Columns["GiaNhap"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dgvSanPham.Columns["GiaBan"] != null)
            {
                dgvSanPham.Columns["GiaBan"].HeaderText = "Giá Bán";
                dgvSanPham.Columns["GiaBan"].DefaultCellStyle.Format = "N0";
                dgvSanPham.Columns["GiaBan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        // Cập nhật tên sản phẩm thật từ SQL backup
        private void UpdateProductNamesFromBackup()
        {
            using (SqlConnection connect = new SqlConnection(connectionString))
            {
                connect.Open();
                using (SqlTransaction tx = connect.BeginTransaction())
                {
                    try
                    {
                        // Bước 1: Tạo bảng tạm chứa tên thật
                        string createTemp = @"
IF OBJECT_ID('tempdb..#RealNamesSP') IS NOT NULL DROP TABLE #RealNamesSP;
CREATE TABLE #RealNamesSP (
    ID INT IDENTITY(1,1),
    TenSP NVARCHAR(200),
    CategoryName NVARCHAR(100),
    XuatXu NVARCHAR(100)
);

INSERT INTO #RealNamesSP (TenSP, CategoryName, XuatXu) VALUES
-- Trái cây
(N'Cherry Bing Đỏ Mỹ Premium (Hộp 1kg)', N'Trái cây', N'Mỹ'),
(N'Lê Hàn Quốc Evergood Organic (Hộp 500g)', N'Trái cây', N'Hàn Quốc'),
(N'Việt Quất Mỹ Premium (Khay 300g)', N'Trái cây', N'Mỹ'),
(N'Nho Đen Úc Organic (Hộp 500g)', N'Trái cây', N'Úc'),
(N'Dâu Tây Hàn Quốc Premium (Hộp 500g)', N'Trái cây', N'Hàn Quốc'),
-- Thịt bò
(N'Thăn Lưng Bò Black Angus Mỹ (Khay 500g)', N'Thịt bò', N'Mỹ'),
(N'Bắp Hoa Bò Úc Kilcoy Premium (Kg)', N'Thịt bò', N'Úc'),
(N'Ribeye Bò Nhật A5 Wagyu (100g)', N'Thịt bò', N'Nhật Bản'),
(N'Thăn Nội Bò Mỹ USDA Choice (500g)', N'Thịt bò', N'Mỹ'),
(N'Sườn Bò Úc Premium BBQ (1kg)', N'Thịt bò', N'Úc'),
-- Hải sản
(N'Sò Điệp Hokkaido L-Size Premium (300g)', N'Hải sản', N'Nhật Bản'),
(N'Cá Hồi Atlantic Fillet Na Uy (500g)', N'Hải sản', N'Na Uy'),
(N'Tôm Càng Xanh Úc Đông Lạnh (500g)', N'Hải sản', N'Úc'),
(N'Cua Tuyết Canada Premium (1 con)', N'Hải sản', N'Canada'),
(N'Bạch Tuộc Nhật Bản Đã Làm Sạch (500g)', N'Hải sản', N'Nhật Bản'),
-- Sữa tươi
(N'Sữa Hạnh Nhân Blue Diamond Unsweetened (946ml)', N'Sữa tươi', N'Mỹ'),
(N'Sữa Yến Mạch Oatly Barista (1L)', N'Sữa tươi', N'Thụy Điển'),
(N'Sữa Tươi Nguyên Kem Organic Valley (1.89L)', N'Sữa tươi', N'Mỹ'),
-- Rượu vang
(N'Rượu Vang Cabernet Sauvignon Chile Premium (750ml)', N'Rượu vang', N'Chile'),
(N'Rượu Vang Trắng Chardonnay Pháp (750ml)', N'Rượu vang', N'Pháp'),
(N'Rượu Vang Đỏ Barossa Úc Shiraz (750ml)', N'Rượu vang', N'Úc'),
-- Bánh kẹo
(N'Chocolate Lindt Excellence 70% Dark (100g)', N'Bánh kẹo', N'Thụy Sĩ'),
(N'Bánh Macaron Ladurée Mixed Flavors (12 cái)', N'Bánh kẹo', N'Pháp'),
(N'Kẹo Haribo Goldbears Gummy (200g)', N'Bánh kẹo', N'Đức'),
-- Đồ khô
(N'Mì Ý Barilla Linguine #13 (500g)', N'Đồ khô', N'Ý'),
(N'Phô Mai Brie President Pháp (200g)', N'Đồ khô', N'Pháp'),
(N'Nước Tương Kikkoman Nhật (500ml)', N'Đồ khô', N'Nhật Bản'),
(N'Dầu Olive Extra Virgin Borges (750ml)', N'Đồ khô', N'Tây Ban Nha'),
-- Hạt
(N'Hạt Dẻ Cười Wonderful No Shell (340g)', N'Hạt', N'Mỹ'),
(N'Hạt Macadamia Úc Rang Muối (200g)', N'Hạt', N'Úc'),
(N'Hạt Hạnh Nhân California Organic (300g)', N'Hạt', N'Mỹ'),
(N'Óc Chó Mỹ Premium Nhân (250g)', N'Hạt', N'Mỹ'),
-- Đồ hộp
(N'Cá Ngừ Đóng Hộp Rio Mare (160g)', N'Đồ hộp', N'Ý'),
(N'Cà Chua Nghiền Mutti Hộp (400g)', N'Đồ hộp', N'Ý'),
(N'Đậu Đen Hữu Cơ EDEN (425g)', N'Đồ hộp', N'Mỹ'),
-- Gia vị
(N'Muối Himalaya Hồng Morton (453g)', N'Gia vị', N'Pakistan'),
(N'Hạt Tiêu Đen Kampot Premium (50g)', N'Gia vị', N'Campuchia'),
(N'Bơ Pháp Président Unsalted (200g)', N'Gia vị', N'Pháp'),
(N'Mật Ong Manuka UMF 15+ New Zealand (250g)', N'Gia vị', N'New Zealand'),
(N'Nước Mắm Phú Quốc 40 Độ Đạm (500ml)', N'Gia vị', N'Việt Nam');
";
                        using (SqlCommand cmd = new SqlCommand(createTemp, connect, tx))
                        {
                            cmd.CommandTimeout = 60;
                            cmd.ExecuteNonQuery();
                        }

                        // Bước 2: Cập nhật tên sản phẩm hiện có
                        string updateExisting = @"
;WITH CTE_SP AS (
    SELECT MaSP, TenSP, MaDanhMuc, XuatXu,
           ROW_NUMBER() OVER (ORDER BY MaSP) AS Rn
    FROM SanPham
),
CTE_Names AS (
    SELECT r.ID, r.TenSP, r.CategoryName, r.XuatXu,
           d.MaDanhMuc
    FROM #RealNamesSP r
    LEFT JOIN DanhMuc d ON d.TenDanhMuc = r.CategoryName
)
UPDATE sp
SET 
    sp.TenSP = n.TenSP,
    sp.XuatXu = n.XuatXu,
    sp.MaDanhMuc = ISNULL(n.MaDanhMuc, sp.MaDanhMuc),
    sp.GiaBan = sp.GiaNhap * 1.35,
    sp.MaVach = '8936' + RIGHT('000000' + CAST(sp.MaSP * 7 + 10001 AS VARCHAR), 6) + CAST((sp.MaSP * 3) % 10 AS VARCHAR)
FROM SanPham sp
JOIN CTE_SP c ON sp.MaSP = c.MaSP
JOIN CTE_Names n ON c.Rn = n.ID;
";
                        using (SqlCommand cmd2 = new SqlCommand(updateExisting, connect, tx))
                        {
                            cmd2.CommandTimeout = 60;
                            cmd2.ExecuteNonQuery();
                        }

                        // Bước 3: Thêm sản phẩm mới nếu cần (nếu bảng có ít hơn 40 sản phẩm)
                        string insertNew = @"
DECLARE @CurrentCount INT = (SELECT COUNT(*) FROM SanPham);
DECLARE @MaDM_TraiCay INT = (SELECT TOP 1 MaDanhMuc FROM DanhMuc WHERE TenDanhMuc = N'Trái cây');
DECLARE @MaDM_HaiSan INT = (SELECT TOP 1 MaDanhMuc FROM DanhMuc WHERE TenDanhMuc = N'Hải sản');
DECLARE @MaDM_HatKho INT = (SELECT TOP 1 MaDanhMuc FROM DanhMuc WHERE TenDanhMuc = N'Hạt');
DECLARE @MaNCC_Default INT = (SELECT TOP 1 MaNCC FROM NhaCungCap ORDER BY MaNCC);

IF @CurrentCount < 30
BEGIN
    INSERT INTO SanPham (TenSP, MaDanhMuc, MaNCC, SoLuongTon, GiaNhap, GiaBan, XuatXu, HSD, MaVach)
    SELECT r.TenSP, d.MaDanhMuc, @MaNCC_Default,
           ABS(CHECKSUM(NEWID())) % 100 + 50,
           (ABS(CHECKSUM(NEWID())) % 500 + 100) * 1000,
           (ABS(CHECKSUM(NEWID())) % 500 + 100) * 1000 * 1.35,
           r.XuatXu,
           DATEADD(DAY, 300, GETDATE()),
           '8936' + RIGHT('000000' + CAST(ABS(CHECKSUM(NEWID())) % 999999 AS VARCHAR), 6) + '0'
    FROM #RealNamesSP r
    LEFT JOIN DanhMuc d ON d.TenDanhMuc = r.CategoryName
    WHERE NOT EXISTS (SELECT 1 FROM SanPham sp2 WHERE sp2.TenSP = r.TenSP)
    AND @CurrentCount < 30;
END

DROP TABLE #RealNamesSP;
";
                        using (SqlCommand cmd3 = new SqlCommand(insertNew, connect, tx))
                        {
                            cmd3.CommandTimeout = 60;
                            cmd3.ExecuteNonQuery();
                        }

                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            UC_ThemSP uc = new UC_ThemSP();
            FormPopup popup = new FormPopup(uc, "Thêm Sản Phẩm Mới");
            if (popup.ShowDialog() == DialogResult.OK)
            {
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
            FormPopup popup = new FormPopup(uc, "Sửa Thông Tin Sản Phẩm");
            if (popup.ShowDialog() == DialogResult.OK)
            {
                LoadDataSP();
            }
        }
    }
}