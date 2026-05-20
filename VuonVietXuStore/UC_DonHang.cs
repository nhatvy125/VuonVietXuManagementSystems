using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VuonVietXuStore
{
    public partial class UC_DonHang : UserControl
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["VuonVietXuStore"].ConnectionString;

        public UC_DonHang()
        {
            InitializeComponent();
            SetupStyles();
            LoadDataDonHang();
        }

        private void SetupStyles()
        {
            this.BackColor = Color.FromArgb(242, 247, 244);

            // Style dgvDonHang
            dgvDonHang.AllowUserToAddRows = false;
            dgvDonHang.BackgroundColor = Color.White;
            dgvDonHang.BorderStyle = BorderStyle.None;
            dgvDonHang.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDonHang.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvDonHang.EnableHeadersVisualStyles = false;
            dgvDonHang.GridColor = Color.FromArgb(224, 224, 224);
            dgvDonHang.RowHeadersVisible = false;
            dgvDonHang.RowTemplate.Height = 35;
            dgvDonHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvDonHang.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(18, 78, 44);
            dgvDonHang.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvDonHang.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvDonHang.ColumnHeadersHeight = 40;

            dgvDonHang.DefaultCellStyle.BackColor = Color.White;
            dgvDonHang.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            dgvDonHang.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvDonHang.DefaultCellStyle.SelectionBackColor = Color.FromArgb(235, 247, 238);
            dgvDonHang.DefaultCellStyle.SelectionForeColor = Color.FromArgb(18, 78, 44);

            // Style dgvChiTietDH
            dgvChiTietDH.AllowUserToAddRows = false;
            dgvChiTietDH.BackgroundColor = Color.White;
            dgvChiTietDH.BorderStyle = BorderStyle.None;
            dgvChiTietDH.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvChiTietDH.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvChiTietDH.EnableHeadersVisualStyles = false;
            dgvChiTietDH.GridColor = Color.FromArgb(224, 224, 224);
            dgvChiTietDH.RowHeadersVisible = false;
            dgvChiTietDH.RowTemplate.Height = 35;
            dgvChiTietDH.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvChiTietDH.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgvChiTietDH.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvChiTietDH.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvChiTietDH.ColumnHeadersHeight = 40;

            dgvChiTietDH.DefaultCellStyle.BackColor = Color.White;
            dgvChiTietDH.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            dgvChiTietDH.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvChiTietDH.DefaultCellStyle.SelectionBackColor = Color.FromArgb(235, 243, 250);
            dgvChiTietDH.DefaultCellStyle.SelectionForeColor = Color.FromArgb(44, 62, 80);

            // Combobox items
            cbTrangThai.Items.Clear();
            cbTrangThai.Items.AddRange(new object[] { "Chờ xử lý", "Đang giao", "Hoàn thành", "Đã hủy" });
            cbTrangThai.Font = new Font("Segoe UI", 9.5F);

            // Update status button
            btnCapNhatTrangThai.BackColor = Color.FromArgb(18, 78, 44);
            btnCapNhatTrangThai.ForeColor = Color.White;
            btnCapNhatTrangThai.FlatStyle = FlatStyle.Flat;
            btnCapNhatTrangThai.FlatAppearance.BorderSize = 0;
            btnCapNhatTrangThai.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCapNhatTrangThai.Cursor = Cursors.Hand;

            // Search button styling
            btnSearch.BackColor = Color.FromArgb(18, 78, 44);
            btnSearch.ForeColor = Color.White;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSearch.Cursor = Cursors.Hand;

            // Search-by dropdown
            cboSearchBy.Items.Clear();
            cboSearchBy.Items.AddRange(new object[] {
                "Mã đơn hàng",
                "Tên khách hàng",
                "Trạng thái"
            });
            cboSearchBy.SelectedIndex = 0;
            cboSearchBy.SelectedIndexChanged += (s, e) => LoadDataDonHang();

            // Sort dropdown items
            cboSort.Items.Clear();
            cboSort.Items.AddRange(new object[] { 
                "Ngày đặt mới nhất", 
                "Ngày đặt cũ nhất", 
                "Tổng tiền giảm dần", 
                "Tổng tiền tăng dần" 
            });
            cboSort.SelectedIndex = 0;

            // Register events
            cboSort.SelectedIndexChanged += (s, e) => LoadDataDonHang();
            btnSearch.Click += (s, e) => LoadDataDonHang();
            txtSearch.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true; // Prevents beep
                    LoadDataDonHang();
                }
            };
        }

        private void LoadDataDonHang()
        {
            try
            {
                string searchVal = txtSearch.Text.Trim();
                string sortOrder = "dh.NgayDatHang DESC, dh.MaDH DESC"; // Default
                
                if (cboSort.SelectedIndex == 1) // NgayDatHang ASC
                    sortOrder = "dh.NgayDatHang ASC, dh.MaDH ASC";
                else if (cboSort.SelectedIndex == 2) // TongTien DESC
                    sortOrder = "dh.TongTien DESC";
                else if (cboSort.SelectedIndex == 3) // TongTien ASC
                    sortOrder = "dh.TongTien ASC";

                string searchCol;
                switch (cboSearchBy.SelectedItem?.ToString())
                {
                    case "Tên khách hàng": searchCol = "COALESCE(kh.TenKH, '')"; break;
                    case "Trạng thái":     searchCol = "COALESCE(dh.TrangThai, '')"; break;
                    default:               searchCol = "CAST(dh.MaDH AS VARCHAR)"; break;
                }

                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    string query = $@"
                        SELECT 
                            dh.MaDH AS [Mã Đơn], 
                            COALESCE(kh.TenKH, N'Khách vãng lai') AS [Khách Hàng], 
                            dh.NgayDatHang AS [Ngày Đặt], 
                            dh.TongTien AS [Tổng Tiền],
                            dh.TrangThai AS [Trạng Thái]
                        FROM DonHang dh
                        LEFT JOIN KhachHang kh ON dh.MaKH = kh.MaKH
                        WHERE {searchCol} LIKE @search
                        ORDER BY {sortOrder}";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + searchVal + "%");
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dgvDonHang.DataSource = dt;
                        }
                    }
                }

                // Format columns
                if (dgvDonHang.Columns["Tổng Tiền"] != null)
                {
                    dgvDonHang.Columns["Tổng Tiền"].DefaultCellStyle.Format = "N0";
                    dgvDonHang.Columns["Tổng Tiền"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (dgvDonHang.Columns["Ngày Đặt"] != null)
                {
                    dgvDonHang.Columns["Ngày Đặt"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
                if (dgvDonHang.Columns["Trạng Thái"] != null)
                {
                    dgvDonHang.Columns["Trạng Thái"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách đơn hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Keeping real-time filter capability
            LoadDataDonHang();
        }

        private void dgvDonHang_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDonHang.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvDonHang.SelectedRows[0];
                int maDH = Convert.ToInt32(row.Cells["Mã Đơn"].Value);
                string customerName = Convert.ToString(row.Cells["Khách Hàng"].Value);
                decimal tongTien = Convert.ToDecimal(row.Cells["Tổng Tiền"].Value);
                string trangThai = Convert.ToString(row.Cells["Trạng Thái"].Value);

                lblDetailTitle.Text = $"CHI TIẾT ĐƠN HÀNG #{maDH}";
                lblDetailCustomer.Text = $"Khách hàng: {customerName}";
                lblDetailTotal.Text = $"Tổng thanh toán: {tongTien.ToString("N0")} đ";
                cbTrangThai.Text = trangThai;

                LoadDetailData(maDH);
            }
            else
            {
                lblDetailTitle.Text = "CHI TIẾT ĐƠN HÀNG";
                lblDetailCustomer.Text = "Khách hàng: --";
                lblDetailTotal.Text = "Tổng thanh toán: 0 đ";
                cbTrangThai.SelectedIndex = -1;
                dgvChiTietDH.DataSource = null;
            }
        }

        private void LoadDetailData(int maDH)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT 
                            sp.TenSP AS [Sản Phẩm],
                            ct.SoLuong AS [Số Lượng],
                            ct.Gia AS [Đơn Giá],
                            (ct.SoLuong * ct.Gia) AS [Thành Tiền]
                        FROM ChiTietDH ct
                        INNER JOIN SanPham sp ON ct.MaSP = sp.MaSP
                        WHERE ct.MaDH = @maDH";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connect))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@maDH", maDH);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvChiTietDH.DataSource = dt;
                    }
                }

                // Format columns
                if (dgvChiTietDH.Columns["Đơn Giá"] != null)
                {
                    dgvChiTietDH.Columns["Đơn Giá"].DefaultCellStyle.Format = "N0";
                    dgvChiTietDH.Columns["Đơn Giá"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (dgvChiTietDH.Columns["Thành Tiền"] != null)
                {
                    dgvChiTietDH.Columns["Thành Tiền"].DefaultCellStyle.Format = "N0";
                    dgvChiTietDH.Columns["Thành Tiền"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy chi tiết đơn hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCapNhatTrangThai_Click(object sender, EventArgs e)
        {
            if (dgvDonHang.SelectedRows.Count == 0) return;

            if (cbTrangThai.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn trạng thái mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dgvDonHang.SelectedRows[0];
            int maDH = Convert.ToInt32(row.Cells["Mã Đơn"].Value);
            string trangThaiMoi = cbTrangThai.SelectedItem.ToString();

            try
            {
                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    connect.Open();
                    string query = "UPDATE DonHang SET TrangThai = @tt WHERE MaDH = @maDH";
                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@tt", trangThaiMoi);
                        cmd.Parameters.AddWithValue("@maDH", maDH);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Cập nhật trạng thái thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataDonHang();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật trạng thái: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            UC_ThemDonHang uc = new UC_ThemDonHang();
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

