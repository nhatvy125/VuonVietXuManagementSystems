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
        SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["VuonVietXuStore"].ConnectionString);

        private Timer slideTimer = new Timer();
        private int targetTop;

        public UC_ThemPhieuNhap()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.UpdateStyles();

            SetupDataGridView();
            SetupModernEffects();
        }

        private void SetupDataGridView()
        {
            if (dgvChiTiet.Columns.Count == 0)
            {
                dgvChiTiet.Columns.Add("colTenSP", "Tên sản phẩm");
                dgvChiTiet.Columns.Add("colSoLuong", "Số lượng");
                dgvChiTiet.Columns.Add("colGiaNhap", "Giá nhập");
            }

            dgvChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvChiTiet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvChiTiet.ReadOnly = true;

            dgvChiTiet.Columns[2].DefaultCellStyle.Format = "N0";
        }

        private void SetupModernEffects()
        {
            btnThem.MouseEnter += (s, e) => btnThem.BackColor = Color.FromArgb(27, 120, 67);
            btnThem.MouseLeave += (s, e) => btnThem.BackColor = Color.FromArgb(20, 90, 50);

            btnLuu.MouseEnter += (s, e) => btnLuu.BackColor = Color.FromArgb(27, 120, 67);
            btnLuu.MouseLeave += (s, e) => btnLuu.BackColor = Color.FromArgb(20, 90, 50);

            btnHuy.MouseEnter += (s, e) => {
                btnHuy.BackColor = Color.FromArgb(255, 235, 235);
                btnHuy.ForeColor = Color.FromArgb(220, 53, 69);
            };
            btnHuy.MouseLeave += (s, e) => {
                btnHuy.BackColor = Color.Transparent;
                btnHuy.ForeColor = Color.FromArgb(20, 90, 50);
            };

            this.Load += (s, e) =>
            {
                targetTop = panelGrid.Top;
                panelGrid.Top += 40; 

                slideTimer.Interval = 15;
                slideTimer.Tick += (ss, ee) =>
                {
                    if (panelGrid.Top > targetTop)
                        panelGrid.Top -= 4; 
                    else
                        slideTimer.Stop();
                };
                slideTimer.Start();
            };

            btnThem.Click += btnAddSP_Click;
            btnLuu.Click += btnSave_Click;
            btnHuy.Click += btnCancel_Click;
        }


        // Thêm sản phẩm 
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

        // Quay lại trang Quản lý Nhập Hàng
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