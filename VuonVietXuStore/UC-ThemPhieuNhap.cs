using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VuonVietXuStore
{
    public partial class UC_ThemPhieuNhap : UserControl
    {
        // Khởi tạo kết nối CSDL
        SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["VuonVietXuStore"].ConnectionString);

        // Biến phục vụ Animation
        private Timer slideTimer = new Timer();
        private int targetTop;

        public UC_ThemPhieuNhap()
        {
            InitializeComponent();

            // Kích hoạt DoubleBuffer để chống nháy màn hình khi chạy hiệu ứng lướt
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.UpdateStyles();

            // Cấu hình giao diện và cột dữ liệu
            SetupDataGridView();
            SetupModernEffects();
        }

        // ======================================================================
        // 1. CẤU HÌNH BẢNG CHI TIẾT (ĐỊNH DẠNG ĐẸP)
        // ======================================================================
        private void SetupDataGridView()
        {
            // Tự động tạo 3 cột cho bảng chi tiết nếu chưa có
            if (dgvChiTiet.Columns.Count == 0)
            {
                dgvChiTiet.Columns.Add("colTenSP", "Tên sản phẩm");
                dgvChiTiet.Columns.Add("colSoLuong", "Số lượng");
                dgvChiTiet.Columns.Add("colGiaNhap", "Giá nhập");
            }

            dgvChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvChiTiet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvChiTiet.ReadOnly = true;

            // Định dạng cột Giá nhập thành tiền tệ (VD: 50,000)
            dgvChiTiet.Columns[2].DefaultCellStyle.Format = "N0";
        }

        // ======================================================================
        // 2. HIỆU ỨNG GIAO DIỆN & HOVER CHUỘT
        // ======================================================================
        private void SetupModernEffects()
        {
            // Hover: Nút Thêm vào danh sách (Xanh lá)
            btnThem.MouseEnter += (s, e) => btnThem.BackColor = Color.FromArgb(27, 120, 67);
            btnThem.MouseLeave += (s, e) => btnThem.BackColor = Color.FromArgb(20, 90, 50);

            // Hover: Nút Lưu lại (Xanh lá)
            btnLuu.MouseEnter += (s, e) => btnLuu.BackColor = Color.FromArgb(27, 120, 67);
            btnLuu.MouseLeave += (s, e) => btnLuu.BackColor = Color.FromArgb(20, 90, 50);

            // Hover: Nút Hủy bỏ (Đổi sang nền đỏ nhạt cực xịn)
            btnHuy.MouseEnter += (s, e) => {
                btnHuy.BackColor = Color.FromArgb(255, 235, 235);
                btnHuy.ForeColor = Color.FromArgb(220, 53, 69);
            };
            btnHuy.MouseLeave += (s, e) => {
                btnHuy.BackColor = Color.Transparent;
                btnHuy.ForeColor = Color.FromArgb(20, 90, 50);
            };

            // Animation: Bảng lưới lướt từ dưới lên khi mở trang
            this.Load += (s, e) =>
            {
                targetTop = panelGrid.Top;
                panelGrid.Top += 40; // Kéo bảng xuống 40px

                slideTimer.Interval = 15;
                slideTimer.Tick += (ss, ee) =>
                {
                    if (panelGrid.Top > targetTop)
                        panelGrid.Top -= 4; // Trượt lên mượt mà
                    else
                        slideTimer.Stop();
                };
                slideTimer.Start();
            };

            // Liên kết sự kiện nút bấm vào logic
            btnThem.Click += btnAddSP_Click;
            btnLuu.Click += btnSave_Click;
            btnHuy.Click += btnCancel_Click;
        }

        // ======================================================================
        // 3. LOGIC XỬ LÝ DỮ LIỆU
        // ======================================================================

        // Thêm sản phẩm vào danh sách tạm (Grid)
        private void btnAddSP_Click(object sender, EventArgs e)
        {
            // Kiểm tra nhập liệu
            if (string.IsNullOrWhiteSpace(txtTenSP.Text) ||
                string.IsNullOrWhiteSpace(txtSoLuong.Text) ||
                string.IsNullOrWhiteSpace(txtGiaNhap.Text) ||
                string.IsNullOrWhiteSpace(txtNCC.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Đẩy dữ liệu vào bảng
            dgvChiTiet.Rows.Add(
                txtTenSP.Text.Trim(),
                txtSoLuong.Text.Trim(),
                txtGiaNhap.Text.Trim()
            );

            // Tự động cuộn xuống dòng cuối cùng để nhìn cho rõ
            dgvChiTiet.FirstDisplayedScrollingRowIndex = dgvChiTiet.RowCount - 1;

            // Dọn dẹp ô nhập liệu để nhập món mới, giữ lại Nhà cung cấp & Ngày
            txtTenSP.Clear();
            txtSoLuong.Clear();
            txtGiaNhap.Clear();
            txtTenSP.Focus();
        }

        // Lưu toàn bộ phiếu nhập vào SQL Server
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có sản phẩm nào trong danh sách nhập!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                connect.Open();
                decimal tongTien = 0;

                // Tính tổng tiền
                foreach (DataGridViewRow row in dgvChiTiet.Rows)
                {
                    if (row.Cells[1].Value == null) continue;
                    int sl = Convert.ToInt32(row.Cells[1].Value);
                    decimal gia = Convert.ToDecimal(row.Cells[2].Value);
                    tongTien += sl * gia;
                }

                // 1. Tạo Phiếu Nhập
                string query = "INSERT INTO PhieuNhap (MaNCC, NgayNhap, TongTien) OUTPUT INSERTED.MaPhieuNhap VALUES (@mancc, @ngay, @tongtien)";
                SqlCommand cmd = new SqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@mancc", txtNCC.Text);
                cmd.Parameters.AddWithValue("@ngay", dtpNgayNhap.Value);
                cmd.Parameters.AddWithValue("@tongtien", tongTien);

                int maPN = (int)cmd.ExecuteScalar();

                // 2. Lưu Chi Tiết & Cập nhật kho
                foreach (DataGridViewRow row in dgvChiTiet.Rows)
                {
                    if (row.Cells[0].Value == null) continue;

                    // Lấy mã sản phẩm
                    string sqlSP = "SELECT MaSP FROM SanPham WHERE TenSP = @tensp";
                    SqlCommand cmdSP = new SqlCommand(sqlSP, connect);
                    cmdSP.Parameters.AddWithValue("@tensp", row.Cells[0].Value.ToString());
                    object result = cmdSP.ExecuteScalar();

                    if (result == null) continue;
                    int maSP = Convert.ToInt32(result);

                    // Thêm Chi tiết Phiếu nhập
                    string sqlCT = "INSERT INTO ChiTietPhieuNhap (MaPhieuNhap, MaSP, SoLuong, GiaNhap) VALUES (@mapn, @masp, @sl, @gia)";
                    SqlCommand cmdCT = new SqlCommand(sqlCT, connect);
                    cmdCT.Parameters.AddWithValue("@mapn", maPN);
                    cmdCT.Parameters.AddWithValue("@masp", maSP);
                    cmdCT.Parameters.AddWithValue("@sl", row.Cells[1].Value);
                    cmdCT.Parameters.AddWithValue("@gia", row.Cells[2].Value);
                    cmdCT.ExecuteNonQuery();

                    // Cập nhật số lượng tồn kho
                    string updateKho = "UPDATE SanPham SET SoLuongTon = SoLuongTon + @sl WHERE MaSP = @masp";
                    SqlCommand cmdKho = new SqlCommand(updateKho, connect);
                    cmdKho.Parameters.AddWithValue("@sl", row.Cells[1].Value);
                    cmdKho.Parameters.AddWithValue("@masp", maSP);
                    cmdKho.ExecuteNonQuery();
                }

                MessageBox.Show("Đã lưu Phiếu Nhập thành công rực rỡ! 🎉", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                NavigateBack(); // Trở về trang danh sách
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối hoặc dữ liệu: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connect.State == ConnectionState.Open)
                    connect.Close();
            }
        }

        // Hủy bỏ
        private void btnCancel_Click(object sender, EventArgs e)
        {
            NavigateBack();
        }

        // Hàm hỗ trợ quay lại trang Quản lý Nhập Hàng
        private void NavigateBack()
        {
            UC_NhapHang uc = new UC_NhapHang();
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