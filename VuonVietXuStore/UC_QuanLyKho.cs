using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using VuonVietXuStore.Helpers;

namespace VuonVietXuStore
{
    /// <summary>
    /// UC_QuanLyKho: Module quản lý kho hàng toàn diện.
    ///   Tab 1 - Tồn kho       : Danh sách tất cả sản phẩm, số lượng, highlight hết/gần hết
    ///   Tab 2 - Cảnh báo       : Sản phẩm sắp hết (dưới ngưỡng)
    ///   Tab 3 - Hạn sử dụng    : Sản phẩm sắp hết hạn hoặc đã hết hạn
    ///   Tab 4 - Điều chỉnh kho : Nhập / xuất kho thủ công — SplitContainer: trái = form, phải = lịch sử
    /// </summary>
    public partial class UC_QuanLyKho : UserControl
    {
        private const int CANH_BAO_TON_KHO = 10;   // ngưỡng cảnh báo hết hàng

        public UC_QuanLyKho()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                try
                {
                    SetupAllGrids();
                    LoadTabTonKho();
                    LoadTabCanhBao();
                    LoadTabHanSuDung();
                    LoadTabLichSu();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải UC_QuanLyKho:\n\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  Tab 1 – Tồn kho
        // ═══════════════════════════════════════════════════════════════════════
        private void SetupAllGrids()
        {
            GridHelper.ApplyStandardStyle(dgvTonKho);
            GridHelper.ApplyStandardStyle(dgvCanhBao);
            GridHelper.ApplyStandardStyle(dgvHanSD);

            // Tô màu dòng tồn kho theo trạng thái
            dgvTonKho.CellFormatting += DgvTonKho_CellFormatting;
        }

        private void LoadTabTonKho(string keyword = "")
        {
            string sql = @"
                SELECT
                    sp.MaSP,
                    sp.TenSP            AS [Sản phẩm],
                    dm.TenDanhMuc       AS [Danh mục],
                    sp.SoLuongTon       AS [Tồn kho],
                    sp.GiaNhap          AS [Giá nhập],
                    sp.GiaBan           AS [Giá bán],
                    CASE
                        WHEN sp.SoLuongTon = 0          THEN N'Hết hàng'
                        WHEN sp.SoLuongTon <= @nguong   THEN N'Sắp hết'
                        ELSE N'Còn hàng'
                    END                 AS [Trạng thái]
                FROM SanPham sp
                JOIN DanhMuc dm ON sp.MaDanhMuc = dm.MaDanhMuc
                WHERE @kw = '' OR sp.TenSP LIKE @kw OR dm.TenDanhMuc LIKE @kw
                ORDER BY sp.SoLuongTon ASC";

            var dt = DbHelper.ExecuteQuery(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("@nguong", CANH_BAO_TON_KHO);
                cmd.Parameters.AddWithValue("@kw", string.IsNullOrWhiteSpace(keyword) ? "" : "%" + keyword + "%");
            });

            dgvTonKho.DataSource = dt;

            if (dgvTonKho.Columns["MaSP"]      != null) dgvTonKho.Columns["MaSP"].Visible = false;
            if (dgvTonKho.Columns["Giá nhập"]  != null) dgvTonKho.Columns["Giá nhập"].DefaultCellStyle.Format = "N0";
            if (dgvTonKho.Columns["Giá bán"]   != null) dgvTonKho.Columns["Giá bán"].DefaultCellStyle.Format  = "N0";
            if (dgvTonKho.Columns["Tồn kho"]   != null) dgvTonKho.Columns["Tồn kho"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Thêm nút điều chỉnh
            if (dgvTonKho.Columns["BtnDC"] == null)
            {
                var btn = new DataGridViewButtonColumn
                {
                    Name = "BtnDC", HeaderText = "", Text = "⚙ Điều chỉnh",
                    UseColumnTextForButtonValue = true, FlatStyle = FlatStyle.Flat,
                    Width = 100, AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };
                dgvTonKho.Columns.Add(btn);
            }

            UpdateSummary(dt);
        }

        // Tô màu theo trạng thái tồn kho
        private void DgvTonKho_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || dgvTonKho.Columns[e.ColumnIndex].Name != "Trạng thái") return;
            if (e.Value == null) return;

            switch (e.Value.ToString())
            {
                case "Hết hàng":
                    e.CellStyle.ForeColor  = Color.White;
                    e.CellStyle.BackColor  = Color.FromArgb(231, 76, 60);
                    e.CellStyle.Font       = new Font("Segoe UI", 8.5f, FontStyle.Bold);
                    dgvTonKho.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 235);
                    break;
                case "Sắp hết":
                    e.CellStyle.ForeColor  = Color.White;
                    e.CellStyle.BackColor  = Color.FromArgb(230, 126, 34);
                    e.CellStyle.Font       = new Font("Segoe UI", 8.5f, FontStyle.Bold);
                    dgvTonKho.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(254, 243, 199);
                    break;
                case "Còn hàng":
                    e.CellStyle.ForeColor  = Color.White;
                    e.CellStyle.BackColor  = Color.FromArgb(39, 174, 96);
                    break;
            }
        }

        private void UpdateSummary(DataTable dt)
        {
            int total = dt.Rows.Count;
            int het = 0, sapHet = 0, conHang = 0;
            foreach (DataRow r in dt.Rows)
            {
                switch (r["Trạng thái"].ToString())
                {
                    case "Hết hàng": het++; break;
                    case "Sắp hết":  sapHet++; break;
                    default:         conHang++; break;
                }
            }
            lblSummary.Text = $"Tổng: {total} SP   |   🟢 Còn hàng: {conHang}   |   🟠 Sắp hết: {sapHet}   |   🔴 Hết hàng: {het}";
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  Tab 2 – Cảnh báo hết hàng
        // ═══════════════════════════════════════════════════════════════════════
        private void LoadTabCanhBao()
        {
            string sql = @"
                SELECT
                    sp.TenSP        AS [Sản phẩm],
                    dm.TenDanhMuc   AS [Danh mục],
                    sp.SoLuongTon   AS [Tồn kho],
                    CASE WHEN sp.SoLuongTon = 0 THEN N'⛔ Hết hàng' ELSE N'⚠ Sắp hết' END AS [Mức độ]
                FROM SanPham sp
                JOIN DanhMuc dm ON sp.MaDanhMuc = dm.MaDanhMuc
                WHERE sp.SoLuongTon <= @nguong
                ORDER BY sp.SoLuongTon ASC";

            var dt = DbHelper.ExecuteQuery(sql, cmd => cmd.Parameters.AddWithValue("@nguong", CANH_BAO_TON_KHO));
            dgvCanhBao.DataSource = dt;

            lblCanhBao.Text = dt.Rows.Count > 0
                ? $"⚠ Có {dt.Rows.Count} sản phẩm cần nhập thêm hàng!"
                : "✅ Tất cả sản phẩm đều còn đủ hàng.";
            lblCanhBao.ForeColor = dt.Rows.Count > 0 ? Color.DarkRed : Color.DarkGreen;

            if (dgvCanhBao.Columns["Tồn kho"] != null)
                dgvCanhBao.Columns["Tồn kho"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  Tab 3 – Hạn sử dụng
        // ═══════════════════════════════════════════════════════════════════════
        private void LoadTabHanSuDung()
        {
            string sql = @"
                IF COL_LENGTH('SanPham','HanSuDung') IS NOT NULL
                BEGIN
                    SELECT
                        sp.TenSP            AS [Sản phẩm],
                        dm.TenDanhMuc       AS [Danh mục],
                        sp.SoLuongTon       AS [Tồn kho],
                        sp.HanSuDung        AS [Hạn sử dụng],
                        DATEDIFF(DAY, GETDATE(), sp.HanSuDung) AS [Còn lại (ngày)],
                        CASE
                            WHEN sp.HanSuDung < GETDATE()               THEN N'⛔ Đã hết hạn'
                            WHEN DATEDIFF(DAY, GETDATE(), sp.HanSuDung) <= 30 THEN N'⚠ Sắp hết hạn'
                            ELSE N'✅ Còn hạn'
                        END AS [Trạng thái]
                    FROM SanPham sp
                    JOIN DanhMuc dm ON sp.MaDanhMuc = dm.MaDanhMuc
                    WHERE sp.HanSuDung IS NOT NULL
                    ORDER BY sp.HanSuDung ASC
                END";

            try
            {
                var dt = DbHelper.ExecuteQuery(sql);
                dgvHanSD.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    lblHanSD.Text      = "Chưa có dữ liệu hạn sử dụng.";
                    lblHanSD.ForeColor = Color.Gray;
                }
                else
                {
                    int expired = 0, near = 0;
                    foreach (DataRow r in dt.Rows)
                    {
                        if (r["Trạng thái"].ToString().Contains("Đã hết hạn")) expired++;
                        else if (r["Trạng thái"].ToString().Contains("Sắp hết hạn")) near++;
                    }
                    lblHanSD.Text = $"⛔ Hết hạn: {expired}   ⚠ Sắp hết hạn (≤30 ngày): {near}";
                    lblHanSD.ForeColor = (expired + near) > 0 ? Color.DarkRed : Color.DarkGreen;
                }
            }
            catch { /* cột chưa tồn tại */ }
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  Tab 4 – Điều chỉnh kho (nhập / xuất thủ công)
        // ═══════════════════════════════════════════════════════════════════════
        private void btnDieuChinh_Click(object sender, EventArgs e)
        {
            if (cboDCSP.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txtDCSoLuong.Text, out int sl) || sl <= 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên dương.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool isNhap = rbNhap.Checked;
            int  maSP   = (int)cboDCSP.SelectedValue;
            string lyDo = txtLyDo.Text.Trim();

            if (!isNhap)
            {
                var check = DbHelper.ExecuteScalar("SELECT SoLuongTon FROM SanPham WHERE MaSP = @id",
                    cmd => cmd.Parameters.AddWithValue("@id", maSP));
                if (check != null && Convert.ToInt32(check) < sl)
                {
                    MessageBox.Show($"Không đủ hàng để xuất! Tồn kho hiện tại: {check}", "Cảnh báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            string sqlUpdate = isNhap
                ? "UPDATE SanPham SET SoLuongTon = SoLuongTon + @sl WHERE MaSP = @id"
                : "UPDATE SanPham SET SoLuongTon = SoLuongTon - @sl WHERE MaSP = @id";

            string sqlLog = @"
                IF OBJECT_ID('LichSuKho','U') IS NOT NULL
                    INSERT INTO LichSuKho (MaSP, LoaiDieuChinh, SoLuong, LyDo, NgayDieuChinh)
                    VALUES (@id, @loai, @sl, @lyDo, GETDATE())";

            DbHelper.ExecuteTransaction((conn, tran) =>
            {
                using (var cmd = new SqlCommand(sqlUpdate, conn, tran))
                {
                    cmd.Parameters.AddWithValue("@sl", sl);
                    cmd.Parameters.AddWithValue("@id", maSP);
                    cmd.ExecuteNonQuery();
                }
                using (var cmd2 = new SqlCommand(sqlLog, conn, tran))
                {
                    cmd2.Parameters.AddWithValue("@id",   maSP);
                    cmd2.Parameters.AddWithValue("@loai", isNhap ? "Nhập" : "Xuất");
                    cmd2.Parameters.AddWithValue("@sl",   sl);
                    cmd2.Parameters.AddWithValue("@lyDo", string.IsNullOrEmpty(lyDo) ? (object)DBNull.Value : lyDo);
                    cmd2.ExecuteNonQuery();
                }
            });

            MessageBox.Show($"✅ {(isNhap ? "Nhập" : "Xuất")} kho thành công!\nSố lượng: {sl}",
                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtDCSoLuong.Clear(); txtLyDo.Clear();
            LoadTabTonKho();
            LoadTabCanhBao();
            LoadTabLichSu();
        }

        // ─── Lịch Sử Kho ──────────────────────────────────────────────────────
        private void LoadTabLichSu()
        {
            string sql = @"
                SELECT
                    lsk.NgayDieuChinh   AS [Thời gian],
                    sp.TenSP            AS [Sản phẩm],
                    CASE lsk.LoaiDieuChinh
                        WHEN N'Nhập' THEN N'⬆ Nhập kho'
                        WHEN N'Xuất' THEN N'⬇ Xuất kho'
                        ELSE lsk.LoaiDieuChinh
                    END                 AS [Loại],
                    lsk.SoLuong         AS [Số lượng],
                    ISNULL(lsk.LyDo, N'—') AS [Lý do]
                FROM LichSuKho lsk
                JOIN SanPham sp ON lsk.MaSP = sp.MaSP
                ORDER BY lsk.NgayDieuChinh DESC";
            try
            {
                var dt = DbHelper.ExecuteQuery(sql);
                dgvLichSu.DataSource = dt;
                GridHelper.ApplyStandardStyle(dgvLichSu);

                if (dgvLichSu.Columns["Thời gian"] != null)
                    dgvLichSu.Columns["Thời gian"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                if (dgvLichSu.Columns["Loại"] != null)
                    dgvLichSu.Columns["Loại"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (dgvLichSu.Columns["Số lượng"] != null)
                    dgvLichSu.Columns["Số lượng"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvLichSu.CellFormatting -= DgvLichSu_CellFormatting;
                dgvLichSu.CellFormatting += DgvLichSu_CellFormatting;

                lblLichSu.Text = $"Tổng: {dt.Rows.Count} lần điều chỉnh";
            }
            catch (Exception ex)
            {
                lblLichSu.Text = "Chưa có dữ liệu lịch sử: " + ex.Message;
            }
        }

        private void DgvLichSu_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || dgvLichSu.Columns[e.ColumnIndex].Name != "Loại") return;
            if (e.Value == null) return;
            string val = e.Value.ToString();
            if (val.Contains("Nhập"))
            {
                e.CellStyle.ForeColor = Color.White;
                e.CellStyle.BackColor = Color.FromArgb(39, 174, 96);
                e.CellStyle.Font      = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            }
            else if (val.Contains("Xuất"))
            {
                e.CellStyle.ForeColor = Color.White;
                e.CellStyle.BackColor = Color.FromArgb(231, 76, 60);
                e.CellStyle.Font      = new Font("Segoe UI", 8.5f, FontStyle.Bold);
            }
        }

        // ─── Events chung ─────────────────────────────────────────────────────
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadTabTonKho(txtSearch.Text.Trim());
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadTabTonKho();
            LoadTabCanhBao();
            LoadTabHanSuDung();
            LoadTabLichSu();
        }

        private void dgvTonKho_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvTonKho.Columns[e.ColumnIndex].Name == "BtnDC")
            {
                int maSP = Convert.ToInt32(dgvTonKho.Rows[e.RowIndex].Cells["MaSP"].Value);
                tabControl.SelectedTab = tabDieuChinh;
                cboDCSP.SelectedValue  = maSP;
            }
        }

        // ─── Designer stub ────────────────────────────────────────────────────
        private void InitializeComponent()
        {
            this.tabControl   = new TabControl();
            this.tabTonKho    = new TabPage("📦 Tồn Kho");
            this.tabCanhBao   = new TabPage("⚠ Cảnh Báo");
            this.tabHanSD     = new TabPage("📅 Hạn Sử Dụng");
            this.tabDieuChinh = new TabPage("⚙ Điều Chỉnh Kho");

            this.dgvTonKho    = new DataGridView();
            this.dgvCanhBao   = new DataGridView();
            this.dgvHanSD     = new DataGridView();
            this.dgvLichSu    = new DataGridView();
            this.lblSummary   = new Label();
            this.lblCanhBao   = new Label();
            this.lblHanSD     = new Label();
            this.lblLichSu    = new Label();
            this.txtSearch    = new TextBox();
            this.btnRefresh   = new Button();
            this.cboDCSP      = new ComboBox();
            this.txtDCSoLuong = new TextBox();
            this.txtLyDo      = new TextBox();
            this.rbNhap       = new RadioButton();
            this.rbXuat       = new RadioButton();
            this.btnDieuChinh = new Button();

            // ── Tab Tồn Kho ──
            var pnlSearch = new Panel { Height = 44, Dock = DockStyle.Top, Padding = new Padding(8, 8, 8, 4), BackColor = Color.FromArgb(236, 240, 241) };
            var lblS = new Label { Text = "🔍", AutoSize = true, Location = new Point(8, 12) };
            txtSearch.Location = new Point(30, 9); txtSearch.Width = 280;
            txtSearch.TextChanged += txtSearch_TextChanged;
            btnRefresh.Text = "🔄 Làm mới"; btnRefresh.Location = new Point(330, 8); btnRefresh.Size = new Size(90, 26);
            btnRefresh.BackColor = Color.FromArgb(52, 73, 94); btnRefresh.ForeColor = Color.White; btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Click += btnRefresh_Click;
            pnlSearch.Controls.AddRange(new Control[] { lblS, txtSearch, btnRefresh });
            lblSummary.Dock = DockStyle.Bottom; lblSummary.Height = 24; lblSummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lblSummary.Font = new Font("Segoe UI", 8.5f); lblSummary.BackColor = Color.FromArgb(236, 240, 241);
            dgvTonKho.Dock = DockStyle.Fill;
            dgvTonKho.CellContentClick += dgvTonKho_CellContentClick;
            tabTonKho.Controls.AddRange(new Control[] { dgvTonKho, lblSummary, pnlSearch });

            // ── Tab Cảnh Báo ──
            lblCanhBao.Dock = DockStyle.Top; lblCanhBao.Height = 30; lblCanhBao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lblCanhBao.Font = new Font("Segoe UI", 10f, FontStyle.Bold); lblCanhBao.Padding = new Padding(8, 0, 0, 0);
            dgvCanhBao.Dock = DockStyle.Fill;
            tabCanhBao.Controls.AddRange(new Control[] { dgvCanhBao, lblCanhBao });

            // ── Tab Hạn Sử Dụng ──
            lblHanSD.Dock = DockStyle.Top; lblHanSD.Height = 30; lblHanSD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lblHanSD.Font = new Font("Segoe UI", 10f, FontStyle.Bold); lblHanSD.Padding = new Padding(8, 0, 0, 0);
            dgvHanSD.Dock = DockStyle.Fill;
            tabHanSD.Controls.AddRange(new Control[] { dgvHanSD, lblHanSD });

            // ── Tab Điều Chỉnh Kho — SplitContainer ──
            var splitDC = new SplitContainer
            {
                Dock        = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                BorderStyle = BorderStyle.None
            };
            // SplitterDistance phải set sau khi control có width thực tế
            splitDC.HandleCreated += (s, ev) =>
            {
                try { splitDC.SplitterDistance = Math.Max(300, splitDC.Width * 55 / 100); }
                catch { /* ignore */ }
            };

            // Panel TRÁI: form điều chỉnh
            var pnlDC = new Panel { Padding = new Padding(20, 16, 12, 10), Dock = DockStyle.Fill };
            int y = 16;
            void AddRow(Control lbl, Control ctrl)
            {
                lbl.Location = new Point(20, y); ((Label)lbl).AutoSize = true;
                ctrl.Location = new Point(160, y - 2); ctrl.Width = 240;
                pnlDC.Controls.AddRange(new Control[] { lbl, ctrl });
                y += 36;
            }

            AddRow(new Label { Text = "Sản phẩm:" }, cboDCSP);
            cboDCSP.DropDownStyle      = ComboBoxStyle.DropDown;
            cboDCSP.AutoCompleteMode   = AutoCompleteMode.SuggestAppend;
            cboDCSP.AutoCompleteSource = AutoCompleteSource.ListItems;
            var dcDt = DbHelper.ExecuteQuery("SELECT MaSP, TenSP FROM SanPham ORDER BY TenSP");
            cboDCSP.DataSource = dcDt; cboDCSP.DisplayMember = "TenSP"; cboDCSP.ValueMember = "MaSP"; cboDCSP.SelectedIndex = -1;

            var lblLoai = new Label { Text = "Loại:", Location = new Point(20, y), AutoSize = true };
            rbNhap.Text = "⬆ Nhập kho"; rbNhap.Location = new Point(160, y); rbNhap.Checked = true;
            rbXuat.Text = "⬇ Xuất kho"; rbXuat.Location = new Point(275, y);
            pnlDC.Controls.AddRange(new Control[] { lblLoai, rbNhap, rbXuat });
            y += 36;

            AddRow(new Label { Text = "Số lượng:" }, txtDCSoLuong);
            AddRow(new Label { Text = "Lý do:" }, txtLyDo);

            var lblGy = new Label { Text = "Gợi ý:", AutoSize = true, Location = new Point(20, y), ForeColor = Color.Gray, Font = new Font("Segoe UI", 8.5f) };
            var flowGy = new FlowLayoutPanel { Location = new Point(160, y - 2), Width = 240, Height = 26 };
            foreach (var hint in new[] { "Kiểm kê", "Hàng hỏng", "Điều chuyển", "Bổ sung" })
            {
                var bh = new Button { Text = hint, AutoSize = true, Height = 24, FlatStyle = FlatStyle.Flat, BackColor = Color.FromArgb(236, 240, 241), ForeColor = Color.FromArgb(44, 62, 80), Font = new Font("Segoe UI", 8f), Padding = new Padding(4, 0, 4, 0), Margin = new Padding(0, 0, 4, 0) };
                bh.FlatAppearance.BorderColor = Color.FromArgb(189, 195, 199);
                string cap = hint;
                bh.Click += (s, ev) => txtLyDo.Text = cap;
                flowGy.Controls.Add(bh);
            }
            pnlDC.Controls.AddRange(new Control[] { lblGy, flowGy });
            y += 36;

            btnDieuChinh.Text = "✅ Xác nhận điều chỉnh"; btnDieuChinh.Location = new Point(160, y);
            btnDieuChinh.Size = new Size(200, 36); btnDieuChinh.BackColor = Color.FromArgb(39, 174, 96);
            btnDieuChinh.ForeColor = Color.White; btnDieuChinh.FlatStyle = FlatStyle.Flat;
            btnDieuChinh.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnDieuChinh.Click += btnDieuChinh_Click;
            pnlDC.Controls.Add(btnDieuChinh);
            splitDC.Panel1.Controls.Add(pnlDC);

            // Panel PHẢI: lịch sử kho
            var pnlLSRight = new Panel { Dock = DockStyle.Fill, BackColor = Color.FromArgb(248, 249, 250) };
            var pnlLSHead = new Panel { Height = 40, Dock = DockStyle.Top, BackColor = Color.FromArgb(52, 73, 94) };
            var lblLSTitle = new Label { Text = "📋 Lịch Sử Điều Chỉnh", ForeColor = Color.White, Font = new Font("Segoe UI", 9.5f, FontStyle.Bold), AutoSize = true, Location = new Point(10, 10) };
            var btnRefreshLS = new Button { Text = "🔄", Location = new Point(210, 6), Size = new Size(34, 26) };
            btnRefreshLS.BackColor = Color.FromArgb(44, 62, 80); btnRefreshLS.ForeColor = Color.White; btnRefreshLS.FlatStyle = FlatStyle.Flat;
            btnRefreshLS.Click += (s, ev) => LoadTabLichSu();
            pnlLSHead.Controls.AddRange(new Control[] { lblLSTitle, btnRefreshLS });
            lblLichSu.Dock = DockStyle.Bottom; lblLichSu.Height = 22;
            lblLichSu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lblLichSu.Font = new Font("Segoe UI", 8f); lblLichSu.BackColor = Color.FromArgb(236, 240, 241);
            lblLichSu.ForeColor = Color.FromArgb(52, 73, 94); lblLichSu.Padding = new Padding(8, 0, 0, 0);
            dgvLichSu.Dock = DockStyle.Fill;
            pnlLSRight.Controls.AddRange(new Control[] { dgvLichSu, lblLichSu, pnlLSHead });
            splitDC.Panel2.Controls.Add(pnlLSRight);
            tabDieuChinh.Controls.Add(splitDC);

            // ── Tab Control (header) ──
            var pnlTop = new Panel { Height = 60, Dock = DockStyle.Top, BackColor = Color.FromArgb(44, 62, 80) };
            var lblTitle = new Label { Text = "QUẢN LÝ KHO HÀNG", ForeColor = Color.White, Font = new Font("Segoe UI", 14f, FontStyle.Bold), AutoSize = true, Location = new Point(12, 16) };
            pnlTop.Controls.Add(lblTitle);

            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("Segoe UI", 9.5f);
            tabControl.TabPages.AddRange(new TabPage[] { tabTonKho, tabCanhBao, tabHanSD, tabDieuChinh });

            this.Controls.AddRange(new Control[] { tabControl, pnlTop });
            this.Dock = DockStyle.Fill;
        }

        private TabControl   tabControl;
        private TabPage      tabTonKho, tabCanhBao, tabHanSD, tabDieuChinh;
        private DataGridView dgvTonKho, dgvCanhBao, dgvHanSD, dgvLichSu;
        private Label        lblSummary, lblCanhBao, lblHanSD, lblLichSu;
        private TextBox      txtSearch, txtDCSoLuong, txtLyDo;
        private Button       btnRefresh, btnDieuChinh;
        private ComboBox     cboDCSP;
        private RadioButton  rbNhap, rbXuat;
    }
}
