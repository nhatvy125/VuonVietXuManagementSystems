using System;
using System.Data;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using VuonVietXuStore.Helpers;

namespace VuonVietXuStore
{
    /// <summary>
    /// UC_BanHang: Quản lý toàn bộ luồng bán hàng.
    ///   - Hiển thị lịch sử đơn hàng đã tạo
    ///   - Nút "Tạo Đơn Hàng Mới" → UC_TaoDonHang
    ///   - Click "Xem chi tiết" → UC_ChiTietDonHang
    /// </summary>
    public partial class UC_BanHang : UserControl
    {
        public UC_BanHang()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                try
                {
                    SetupGrid();
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải UC_BanHang:\n\n" + ex.Message,
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ─── Setup ───────────────────────────────────────────────────────────
        private void SetupGrid()
        {
            GridHelper.ApplyStandardStyle(dgvDonHang);
            dgvDonHang.CellFormatting   += DgvDonHang_CellFormatting;
            dgvDonHang.CellContentClick += dgvDonHang_CellContentClick;
        }

        private void LoadData(string keyword = "")
        {
            string sql = @"
                SELECT
                    dh.MaDH         AS MaDonHang,
                    kh.TenKH        AS KhachHang,
                    dh.NgayDatHang,
                    dh.TongTien,
                    dh.TrangThai,
                    dh.GhiChu
                FROM DonHang dh
                JOIN KhachHang kh ON dh.MaKH = kh.MaKH
                WHERE (@kw = '' OR kh.TenKH LIKE @kw OR CAST(dh.MaDH AS NVARCHAR) LIKE @kw)
                ORDER BY dh.NgayDatHang DESC";

            var dt = DbHelper.ExecuteQuery(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("@kw",
                    string.IsNullOrWhiteSpace(keyword) ? "" : "%" + keyword + "%");
            });

            dgvDonHang.DataSource = dt;

            if (dgvDonHang.Columns["MaDonHang"]  != null) dgvDonHang.Columns["MaDonHang"].HeaderText  = "Mã ĐH";
            if (dgvDonHang.Columns["KhachHang"]   != null) dgvDonHang.Columns["KhachHang"].HeaderText   = "Khách hàng";
            if (dgvDonHang.Columns["NgayDatHang"] != null)
            {
                dgvDonHang.Columns["NgayDatHang"].HeaderText = "Ngày đặt";
                dgvDonHang.Columns["NgayDatHang"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
            if (dgvDonHang.Columns["TongTien"] != null)
            {
                dgvDonHang.Columns["TongTien"].HeaderText = "Tổng tiền";
                dgvDonHang.Columns["TongTien"].DefaultCellStyle.Format    = "N0";
                dgvDonHang.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvDonHang.Columns["TrangThai"] != null) dgvDonHang.Columns["TrangThai"].HeaderText = "Trạng thái";
            if (dgvDonHang.Columns["GhiChu"]    != null) dgvDonHang.Columns["GhiChu"].HeaderText    = "Ghi chú";

            // Chỉ thêm nút Xem (không có nút Xóa)
            if (dgvDonHang.Columns["BtnXem"] == null)
            {
                var btnXem = new DataGridViewButtonColumn
                {
                    Name = "BtnXem", HeaderText = "", Text = "👁 Xem chi tiết",
                    UseColumnTextForButtonValue = true,
                    FlatStyle = FlatStyle.Flat, Width = 120
                };
                dgvDonHang.Columns.Add(btnXem);
            }

            lblCount.Text = $"Tổng: {dt.Rows.Count} đơn hàng";
        }

        // Màu trạng thái
        private void DgvDonHang_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvDonHang.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                switch (e.Value.ToString())
                {
                    case "Hoàn thành":
                        e.CellStyle.ForeColor = Color.FromArgb(39, 174, 96);
                        e.CellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                        break;
                    case "Đang xử lý":
                    case "Đang đóng gói":
                    case "Đang giao":
                        e.CellStyle.ForeColor = Color.DarkOrange;
                        break;
                    case "Đã hủy":
                        e.CellStyle.ForeColor = Color.Red;
                        break;
                }
            }
        }

        // ─── Events ──────────────────────────────────────────────────────────
        private void btnTaoDon_Click(object sender, EventArgs e)
        {
            NavigateTo(new UC_TaoDonHang(OnDonHangCreated));
        }

        private void OnDonHangCreated() => LoadData();

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text.Trim());
        }

        private void dgvDonHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvDonHang.Columns[e.ColumnIndex].Name == "BtnXem")
            {
                int maDH = Convert.ToInt32(dgvDonHang.Rows[e.RowIndex].Cells["MaDonHang"].Value);
                NavigateTo(new UC_ChiTietDonHang(maDH));
            }
        }

        private void NavigateTo(UserControl uc)
        {
            Control parent = this;
            while (parent != null && !(parent is Panel))
                parent = parent.Parent;

            if (parent is Panel panel)
            {
                panel.Controls.Clear();
                uc.Dock = DockStyle.Fill;
                panel.Controls.Add(uc);
                uc.BringToFront();
            }
        }

        // ─── Designer stub ────────────────────────────────────────────────────
        private void InitializeComponent()
        {
            this.dgvDonHang = new DataGridView();
            this.btnTaoDon  = new Button();
            this.txtSearch  = new TextBox();
            this.lblCount   = new Label();

            // ── Header panel ──
            var pnlTop = new Panel
            {
                Height    = 60,
                Dock      = DockStyle.Top,
                BackColor = Color.FromArgb(44, 62, 80),
                Padding   = new Padding(12, 0, 12, 0)
            };

            var lblTitle = new Label
            {
                Text      = "LỊCH SỬ ĐƠN HÀNG",
                ForeColor = Color.White,
                Font      = new Font("Segoe UI", 14f, FontStyle.Bold),
                AutoSize  = true,
                Location  = new Point(12, 16)
            };

            btnTaoDon.Text      = "+ Tạo Đơn Hàng";
            btnTaoDon.Size      = new Size(150, 36);
            btnTaoDon.Anchor    = AnchorStyles.Right | AnchorStyles.Top;
            btnTaoDon.BackColor = Color.FromArgb(39, 174, 96);
            btnTaoDon.ForeColor = Color.White;
            btnTaoDon.FlatStyle = FlatStyle.Flat;
            btnTaoDon.Font      = new Font("Segoe UI", 9f, FontStyle.Bold);
            btnTaoDon.Click    += btnTaoDon_Click;

            pnlTop.Controls.AddRange(new Control[] { lblTitle, btnTaoDon });

            // Đặt vị trí nút sau khi panel có kích thước thực
            pnlTop.Layout += (s, ev) =>
            {
                btnTaoDon.Location = new Point(pnlTop.ClientSize.Width - btnTaoDon.Width - 12, 12);
            };

            // ── Search bar ──
            var pnlSearch = new Panel
            {
                Height    = 44,
                Dock      = DockStyle.Top,
                BackColor = Color.FromArgb(236, 240, 241),
                Padding   = new Padding(12, 8, 12, 4)
            };

            var lblSearch  = new Label { Text = "🔍", AutoSize = true, Location = new Point(12, 12) };
            txtSearch.Location    = new Point(36, 10);
            txtSearch.Width       = 300;
            txtSearch.Font        = new Font("Segoe UI", 9.5f);
            txtSearch.TextChanged += txtSearch_TextChanged;

            lblCount.AutoSize  = true;
            lblCount.Location  = new Point(360, 13);
            lblCount.Font      = new Font("Segoe UI", 9f);
            lblCount.ForeColor = Color.FromArgb(52, 73, 94);

            pnlSearch.Controls.AddRange(new Control[] { lblSearch, txtSearch, lblCount });

            // ── Grid ──
            dgvDonHang.Dock = DockStyle.Fill;

            this.Controls.AddRange(new Control[] { dgvDonHang, pnlSearch, pnlTop });
            this.Dock = DockStyle.Fill;
        }

        private DataGridView dgvDonHang;
        private Button       btnTaoDon;
        private TextBox      txtSearch;
        private Label        lblCount;
    }
}
