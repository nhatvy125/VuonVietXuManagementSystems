using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VuonVietXuStore
{
    public partial class UC_NhapHang : UserControl
    {
        // Sử dụng chuỗi kết nối an toàn từ App.config
        private string connectionString = ConfigurationManager.ConnectionStrings["VuonVietXuStore"].ConnectionString;

        private bool isLoaded = false;

        // Các biến phục vụ hiệu ứng lướt mượt mà (Animation)
        private Timer slideTimer = new Timer();
        private int targetTop;

        public UC_NhapHang()
        {
            InitializeComponent();

            // Kích hoạt bộ đệm kép chống nhấp nháy màn hình khi chạy hiệu ứng lướt Slide
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.UpdateStyles();

            // Đăng ký sự kiện Tải dữ liệu và hiệu ứng giao diện
            this.Load += UC_NhapHang_Load;
            SetupModernEffects();
        }

        private void UC_NhapHang_Load(object sender, EventArgs e)
        {
            dataGridView1.MultiSelect = false;
            LoadData();
        }

        // ======================================================================
        // 1. TÍCH HỢP HIỆU ỨNG GIAO DIỆN HIỆN ĐẠI (ANIMATION & PLACEHOLDER)
        // ======================================================================
        private void SetupModernEffects()
        {
            // A. Hiệu ứng đổi màu xanh sáng hơn khi di chuột vào nút Thêm mới
            btnAdd.MouseEnter += (s, e) => btnAdd.BackColor = Color.FromArgb(27, 120, 67);
            btnAdd.MouseLeave += (s, e) => btnAdd.BackColor = Color.FromArgb(20, 90, 50);

            // B. Hiệu ứng lướt nhẹ từ dưới lên (Slide Up Animation) khi mở trang
            this.Load += (s, e) =>
            {
                targetTop = panelContent.Top;
                panelContent.Top += 25; // Đẩy nhẹ khung bảng xuống một chút để lấy đà lướt

                slideTimer.Interval = 15;
                slideTimer.Tick += (ss, ee) =>
                {
                    if (panelContent.Top > targetTop)
                    {
                        panelContent.Top -= 3; // Lướt dần lên vị trí chuẩn
                    }
                    else
                    {
                        panelContent.Top = targetTop;
                        slideTimer.Stop();
                    }
                };
                slideTimer.Start();
            };

            // C. Quản lý văn bản gợi ý (Placeholder) cho ô Tìm kiếm
            txtSearch.Enter += TxtSearch_Enter;
            txtSearch.Leave += TxtSearch_Leave;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            // D. Thiết lập combobox tìm kiếm theo loại và sắp xếp
            lblSearchIcon.Cursor = Cursors.Hand;
            lblSearchIcon.Click += (s, e) => LoadData();

            cboSearchBy.Items.Clear();
            cboSearchBy.Items.AddRange(new object[] {
                "Mã phiếu nhập",
                "Tên nhà cung cấp",
                "Ngày nhập"
            });
            cboSearchBy.SelectedIndex = 0;
            cboSearchBy.SelectedIndexChanged += (s, e) => LoadData();

            cboSort.Items.Clear();
            cboSort.Items.AddRange(new object[] {
                "Ngày nhập mới nhất",
                "Ngày nhập cũ nhất",
                "Tổng tiền giảm dần",
                "Tổng tiền tăng dần"
            });
            cboSort.SelectedIndex = 0;
            cboSort.SelectedIndexChanged += (s, e) => LoadData();

            txtSearch.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    LoadData();
                }
            };
        }

        private void TxtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Tìm kiếm số phiếu nhập hoặc nhà cung cấp...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.FromArgb(50, 50, 50); // Đổi chữ sang màu đậm khi gõ
            }
        }

        private void TxtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Tìm kiếm số phiếu nhập hoặc nhà cung cấp...";
                txtSearch.ForeColor = Color.Gray; // Làm mờ chữ đi khi để trống
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        // ======================================================================
        // 3. LOGIC TẢI DỮ LIỆU & CO GIÃN KHUNG HÌNH TỰ ĐỘNG
        // ======================================================================
        private void LoadData()
        {
            try
            {
                string searchVal = txtSearch.Text.Trim();
                if (searchVal == "Tìm kiếm số phiếu nhập hoặc nhà cung cấp...")
                    searchVal = "";

                string sortOrder = "NgayNhap DESC, MaPhieuNhap DESC";
                if (cboSort.SelectedIndex == 1) sortOrder = "NgayNhap ASC, MaPhieuNhap ASC";
                else if (cboSort.SelectedIndex == 2) sortOrder = "TongTien DESC";
                else if (cboSort.SelectedIndex == 3) sortOrder = "TongTien ASC";

                // Xác định cột tìm kiếm
                string query;
                switch (cboSearchBy.SelectedItem?.ToString())
                {
                    case "Tên nhà cung cấp":
                        query = $@"SELECT pn.MaPhieuNhap, ncc.TenNCC AS [Nhà Cung Cấp], pn.NgayNhap, pn.TongTien 
                                   FROM PhieuNhap pn
                                   LEFT JOIN NhaCungCap ncc ON pn.MaNCC = ncc.MaNCC
                                   WHERE ISNULL(ncc.TenNCC,'') LIKE @search
                                   ORDER BY {sortOrder}";
                        break;
                    case "Ngày nhập":
                        query = $@"SELECT pn.MaPhieuNhap, ncc.TenNCC AS [Nhà Cung Cấp], pn.NgayNhap, pn.TongTien 
                                   FROM PhieuNhap pn
                                   LEFT JOIN NhaCungCap ncc ON pn.MaNCC = ncc.MaNCC
                                   WHERE CAST(pn.NgayNhap AS VARCHAR) LIKE @search
                                   ORDER BY {sortOrder}";
                        break;
                    default: // Mã phiếu nhập
                        query = $@"SELECT pn.MaPhieuNhap, ncc.TenNCC AS [Nhà Cung Cấp], pn.NgayNhap, pn.TongTien 
                                   FROM PhieuNhap pn
                                   LEFT JOIN NhaCungCap ncc ON pn.MaNCC = ncc.MaNCC
                                   WHERE CAST(pn.MaPhieuNhap AS VARCHAR) LIKE @search
                                   ORDER BY {sortOrder}";
                        break;
                }

                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@search", "%" + searchVal + "%");
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            dataGridView1.DataSource = dt;
                            isLoaded = true;

                            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                            if (dataGridView1.Columns["MaPhieuNhap"] != null) dataGridView1.Columns["MaPhieuNhap"].HeaderText = "Mã Phiếu Nhập";
                            if (dataGridView1.Columns["NgayNhap"] != null) dataGridView1.Columns["NgayNhap"].HeaderText = "Ngày Nhập";
                            if (dataGridView1.Columns["TongTien"] != null)
                            {
                                dataGridView1.Columns["TongTien"].HeaderText = "Tổng Tiền";
                                dataGridView1.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                                dataGridView1.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu phiếu nhập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Hàm lõi tìm kiếm Panel cha trên Form chính để chuyển đổi màn hình mượt mà
        private void LoadUC(UserControl uc)
        {
            Control parent = this;

            while (parent != null && !(parent is Panel))
            {
                parent = parent.Parent;
            }

            Panel panel = parent as Panel;

            if (panel != null)
            {
                panel.Controls.Clear();
                uc.Dock = DockStyle.Fill;
                panel.Controls.Add(uc);
                uc.BringToFront();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            UC_ThemPhieuNhap uc = new UC_ThemPhieuNhap();
            FormPopup popup = new FormPopup(uc, "Thêm Phiếu Nhập Hàng Mới");
            if (popup.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!isLoaded) return;
            if (e.RowIndex < 0) return;

            if (dataGridView1.Rows[e.RowIndex].Cells["MaPhieuNhap"].Value == null)
                return;

            int maPN = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["MaPhieuNhap"].Value);

            // Chuyển hướng sang màn hình Chi tiết phiếu nhập dạng popup
            UC_CTPN uc = new UC_CTPN(maPN);
            FormPopup popup = new FormPopup(uc, $"Chi Tiết Phiếu Nhập #{maPN}");
            if (popup.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }
    }
}