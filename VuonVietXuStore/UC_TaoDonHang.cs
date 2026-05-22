using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using VuonVietXuStore.Helpers;

namespace VuonVietXuStore
{
    public partial class UC_TaoDonHang : UserControl
    {
        private readonly Action _onSaved;
        private DataTable _cart;
        private DataTable _dtKH;
        private DataTable _dtSP;

        public UC_TaoDonHang(Action onSaved = null)
        {
            _onSaved = onSaved;
            InitializeComponent();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                try
                {
                    InitCart();
                    LoadKhachHang();
                    LoadSanPham();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khởi tạo: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ─── Khởi tạo giỏ hàng ───────────────────────────────────────────────
        private void InitCart()
        {
            _cart = new DataTable();
            _cart.Columns.Add("MaSP",      typeof(int));
            _cart.Columns.Add("TenSP",     typeof(string));
            _cart.Columns.Add("GiaBan",    typeof(decimal));
            _cart.Columns.Add("SoLuong",   typeof(int));
            _cart.Columns.Add("ThanhTien", typeof(decimal));

            // Gán DataSource TRƯỚC khi ApplyStandardStyle để columns được tạo
            dgvCart.DataSource = _cart;
            GridHelper.ApplyStandardStyle(dgvCart);
            dgvCart.ReadOnly = false;
            SetupCartGrid();
        }

        private void SetupCartGrid()
        {
            if (dgvCart.Columns.Count == 0) return;

            if (dgvCart.Columns["MaSP"]      != null) dgvCart.Columns["MaSP"].Visible          = false;
            if (dgvCart.Columns["TenSP"]     != null) { dgvCart.Columns["TenSP"].HeaderText     = "Sản phẩm";   dgvCart.Columns["TenSP"].ReadOnly     = true;  dgvCart.Columns["TenSP"].FillWeight    = 30; }
            if (dgvCart.Columns["GiaBan"]    != null) { dgvCart.Columns["GiaBan"].HeaderText    = "Đơn giá";    dgvCart.Columns["GiaBan"].DefaultCellStyle.Format = "N0"; dgvCart.Columns["GiaBan"].ReadOnly = true; dgvCart.Columns["GiaBan"].FillWeight = 20; }
            if (dgvCart.Columns["SoLuong"]   != null) { dgvCart.Columns["SoLuong"].HeaderText   = "Số lượng";   dgvCart.Columns["SoLuong"].FillWeight  = 15; }
            if (dgvCart.Columns["ThanhTien"] != null) { dgvCart.Columns["ThanhTien"].HeaderText = "Thành tiền"; dgvCart.Columns["ThanhTien"].DefaultCellStyle.Format = "N0"; dgvCart.Columns["ThanhTien"].ReadOnly = true; dgvCart.Columns["ThanhTien"].FillWeight = 20; }

            GridHelper.AddDeleteButton(dgvCart, "BtnXoa");
            dgvCart.CellEndEdit      += DgvCart_CellEndEdit;
            dgvCart.CellContentClick += DgvCart_CellContentClick;
        }

        // ─── Load dữ liệu ────────────────────────────────────────────────────
        private void LoadKhachHang()
        {
            _dtKH = DbHelper.ExecuteQuery("SELECT MaKH, TenKH FROM KhachHang ORDER BY TenKH");
            cboKH.Items.Clear();
            cboKH.Items.Add(new ComboItem(0, "-- Chọn khách hàng --"));
            foreach (DataRow r in _dtKH.Rows)
                cboKH.Items.Add(new ComboItem(Convert.ToInt32(r["MaKH"]), r["TenKH"].ToString()));
            cboKH.SelectedIndex = 0;
        }

        private void LoadSanPham()
        {
            _dtSP = DbHelper.ExecuteQuery(
                "SELECT MaSP, TenSP, GiaBan, SoLuongTon FROM SanPham WHERE SoLuongTon > 0 ORDER BY TenSP");
            cboSP.Items.Clear();
            cboSP.Items.Add(new ComboItem(0, "-- Chọn sản phẩm --"));
            foreach (DataRow r in _dtSP.Rows)
                cboSP.Items.Add(new ComboItem(Convert.ToInt32(r["MaSP"]), r["TenSP"].ToString()));
            cboSP.SelectedIndex = 0;
        }

        // ─── Thêm sản phẩm vào giỏ ───────────────────────────────────────────
        private void btnThemSP_Click(object sender, EventArgs e)
        {
            // Hỗ trợ cả chọn từ list và gõ tay
            ComboItem spItem = cboSP.SelectedItem as ComboItem;
            if (spItem == null || spItem.Id <= 0)
            {
                // Thử tìm theo tên gõ vào
                string typed = cboSP.Text.Trim().ToLower();
                foreach (var item in cboSP.Items)
                {
                    if (item is ComboItem ci && ci.Name.ToLower().Contains(typed) && ci.Id > 0)
                    { spItem = ci; break; }
                }
            }
            if (spItem == null || spItem.Id <= 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm từ danh sách.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtSoLuong.Text.Trim(), out int sl) || sl <= 0)
            {
                MessageBox.Show("Số lượng phải là số nguyên dương.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRow spRow = null;
            foreach (DataRow r in _dtSP.Rows)
                if (Convert.ToInt32(r["MaSP"]) == spItem.Id) { spRow = r; break; }

            if (spRow == null) { MessageBox.Show("Không tìm thấy sản phẩm."); return; }

            string  tenSP = spRow["TenSP"].ToString();
            decimal gia   = Convert.ToDecimal(spRow["GiaBan"]);
            int     ton   = Convert.ToInt32(spRow["SoLuongTon"]);

            if (sl > ton)
            {
                MessageBox.Show($"Không đủ hàng! Tồn kho: {ton}", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Nếu đã có trong giỏ → cộng thêm
            foreach (DataRow r in _cart.Rows)
            {
                if ((int)r["MaSP"] == spItem.Id)
                {
                    int newSl = (int)r["SoLuong"] + sl;
                    if (newSl > ton)
                    {
                        MessageBox.Show($"Tổng số lượng vượt tồn kho ({ton}).", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    r["SoLuong"]   = newSl;
                    r["ThanhTien"] = (decimal)r["GiaBan"] * newSl;
                    UpdateTotal();
                    return;
                }
            }

            _cart.Rows.Add(spItem.Id, tenSP, gia, sl, gia * sl);
            UpdateTotal();

            // Reset
            cboSP.SelectedIndex  = 0;
            txtSoLuong.Text      = "1";
        }

        // ─── Sửa / Xóa trong giỏ ─────────────────────────────────────────────
        private void DgvCart_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCart.Columns[e.ColumnIndex].Name != "SoLuong") return;
            var cell = dgvCart.Rows[e.RowIndex].Cells["SoLuong"];
            if (!int.TryParse(cell.Value?.ToString(), out int sl) || sl <= 0)
            {
                MessageBox.Show("Số lượng không hợp lệ."); return;
            }
            decimal gia = Convert.ToDecimal(dgvCart.Rows[e.RowIndex].Cells["GiaBan"].Value);
            dgvCart.Rows[e.RowIndex].Cells["ThanhTien"].Value = gia * sl;
            _cart.Rows[e.RowIndex]["ThanhTien"] = gia * sl;
            UpdateTotal();
        }

        private void DgvCart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvCart.Columns[e.ColumnIndex].Name == "BtnXoa")
            {
                _cart.Rows[e.RowIndex].Delete();
                UpdateTotal();
            }
        }

        private void UpdateTotal()
        {
            decimal total = 0;
            foreach (DataRow r in _cart.Rows)
                if (r.RowState != DataRowState.Deleted)
                    total += Convert.ToDecimal(r["ThanhTien"]);
            lblTotal.Text = "Tổng tiền: " + total.ToString("N0") + " ₫";
        }

        // ─── Lưu đơn hàng ────────────────────────────────────────────────────
        private void btnLuu_Click(object sender, EventArgs e)
        {
            ComboItem khItem = cboKH.SelectedItem as ComboItem;
            if (khItem == null || khItem.Id <= 0)
            {
                string typed = cboKH.Text.Trim().ToLower();
                foreach (var item in cboKH.Items)
                {
                    if (item is ComboItem ci && ci.Name.ToLower().Contains(typed) && ci.Id > 0)
                    { khItem = ci; break; }
                }
            }
            if (khItem == null || khItem.Id <= 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng từ danh sách.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_cart.Rows.Count == 0)
            {
                MessageBox.Show("Giỏ hàng trống.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal tongTien = 0;
            foreach (DataRow r in _cart.Rows)
                if (r.RowState != DataRowState.Deleted)
                    tongTien += Convert.ToDecimal(r["ThanhTien"]);

            try
            {
                int maDH = 0;

                DbHelper.ExecuteTransaction((conn, tran) =>
                {
                    using (var cmd = new SqlCommand(@"
                        INSERT INTO DonHang (MaKH, NgayDatHang, TongTien, TrangThai, GhiChu)
                        OUTPUT INSERTED.MaDH
                        VALUES (@maKH, @ngay, @tong, N'Hoàn thành', @ghiChu)", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@maKH",   khItem.Id);
                        cmd.Parameters.AddWithValue("@ngay",   DateTime.Now);
                        cmd.Parameters.AddWithValue("@tong",   tongTien);
                        cmd.Parameters.AddWithValue("@ghiChu", txtGhiChu.Text.Trim());
                        maDH = (int)cmd.ExecuteScalar();
                    }

                    foreach (DataRow r in _cart.Rows)
                    {
                        if (r.RowState == DataRowState.Deleted) continue;
                        int     maSP = Convert.ToInt32(r["MaSP"]);
                        int     sl   = Convert.ToInt32(r["SoLuong"]);
                        decimal gia  = Convert.ToDecimal(r["GiaBan"]);

                        using (var c = new SqlCommand("SELECT SoLuongTon FROM SanPham WHERE MaSP=@id", conn, tran))
                        {
                            c.Parameters.AddWithValue("@id", maSP);
                            int ton = (int)c.ExecuteScalar();
                            if (sl > ton) throw new Exception($"'{r["TenSP"]}' không đủ hàng (còn {ton}).");
                        }

                        using (var c = new SqlCommand(
                            "INSERT INTO ChiTietDH(MaDH,MaSP,SoLuong,Gia) VALUES(@dh,@sp,@sl,@gia)", conn, tran))
                        {
                            c.Parameters.AddWithValue("@dh",  maDH);
                            c.Parameters.AddWithValue("@sp",  maSP);
                            c.Parameters.AddWithValue("@sl",  sl);
                            c.Parameters.AddWithValue("@gia", gia);
                            c.ExecuteNonQuery();
                        }

                        using (var c = new SqlCommand(
                            "UPDATE SanPham SET SoLuongTon=SoLuongTon-@sl WHERE MaSP=@id", conn, tran))
                        {
                            c.Parameters.AddWithValue("@sl", sl);
                            c.Parameters.AddWithValue("@id", maSP);
                            c.ExecuteNonQuery();
                        }
                    }
                });

                MessageBox.Show($"✅ Tạo đơn hàng #{maDH} thành công!\nTổng tiền: {tongTien:N0} ₫",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _onSaved?.Invoke();
                NavigateTo(new UC_ChiTietDonHang(maDH));
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e) => NavigateBack();

        private void NavigateTo(UserControl uc)
        {
            Control p = this;
            while (p != null && !(p is Panel)) p = p.Parent;
            if (p is Panel panel)
            {
                panel.Controls.Clear();
                uc.Dock = DockStyle.Fill;
                panel.Controls.Add(uc);
                uc.BringToFront();
            }
        }

        private void NavigateBack() => NavigateTo(new UC_BanHang());

        // ─── InitializeComponent ──────────────────────────────────────────────
        private void InitializeComponent()
        {
            this.dgvCart    = new DataGridView();
            this.cboKH      = new ComboBox();
            this.cboSP      = new ComboBox();
            this.txtSoLuong = new TextBox();
            this.txtGhiChu  = new TextBox();
            this.btnThemSP  = new Button();
            this.btnLuu     = new Button();
            this.btnHuy     = new Button();
            this.lblTotal   = new Label();

            // ── Header ──────────────────────────────────────────────────────────
            var pnlTop   = new Panel
            {
                Height    = 56,
                Dock      = DockStyle.Top,
                BackColor = Color.FromArgb(44, 62, 80),
                Padding   = new Padding(16, 0, 16, 0)
            };
            var lblTitle = new Label
            {
                Text      = "TẠO ĐƠN HÀNG MỚI",
                ForeColor = Color.White,
                Font      = new Font("Segoe UI", 14f, FontStyle.Bold),
                AutoSize  = true,
                Location  = new Point(16, 14)
            };
            pnlTop.Controls.Add(lblTitle);

            // ── Form panel ──────────────────────────────────────────────────────
            var pnlForm = new Panel
            {
                Height    = 170,
                Dock      = DockStyle.Top,
                Padding   = new Padding(16, 12, 16, 8),
                BackColor = Color.FromArgb(245, 247, 250)
            };

            int labelX = 16, controlX = 120, rowH = 38;

            // Row 1 – Khách hàng
            var lblKH = new Label { Text = "Khách hàng:", Location = new Point(labelX, 16), AutoSize = true, Font = new Font("Segoe UI", 9.5f) };
            cboKH.Location         = new Point(controlX, 12);
            cboKH.Width            = 280;
            cboKH.Font             = new Font("Segoe UI", 9.5f);
            cboKH.DropDownStyle    = ComboBoxStyle.DropDown;
            cboKH.AutoCompleteMode   = AutoCompleteMode.SuggestAppend;
            cboKH.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboKH.FlatStyle        = FlatStyle.Flat;
            cboKH.SelectedIndexChanged += (s, ev) =>
            {
                if (_cart != null && _cart.Rows.Count > 0)
                {
                    var res = MessageBox.Show(
                        "Đổi khách hàng sẽ xóa giỏ hàng hiện tại. Tiếp tục?",
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        _cart.Rows.Clear();
                        UpdateTotal();
                    }
                }
            };

            // Row 2 – Sản phẩm + Số lượng + Thêm
            var lblSP = new Label { Text = "Sản phẩm:", Location = new Point(labelX, 16 + rowH), AutoSize = true, Font = new Font("Segoe UI", 9.5f) };
            cboSP.Location         = new Point(controlX, 12 + rowH);
            cboSP.Width            = 260;
            cboSP.Font             = new Font("Segoe UI", 9.5f);
            cboSP.DropDownStyle    = ComboBoxStyle.DropDown;
            cboSP.AutoCompleteMode   = AutoCompleteMode.SuggestAppend;
            cboSP.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboSP.FlatStyle        = FlatStyle.Flat;

            var lblSL = new Label { Text = "Số lượng:", Location = new Point(controlX + 270, 16 + rowH), AutoSize = true, Font = new Font("Segoe UI", 9.5f) };
            txtSoLuong.Location    = new Point(controlX + 350, 12 + rowH);
            txtSoLuong.Width       = 60;
            txtSoLuong.Text        = "1";
            txtSoLuong.Font        = new Font("Segoe UI", 9.5f);
            txtSoLuong.TextAlign   = HorizontalAlignment.Center;

            btnThemSP.Text         = "+ Thêm";
            btnThemSP.Location     = new Point(controlX + 420, 10 + rowH);
            btnThemSP.Size         = new Size(80, 28);
            btnThemSP.BackColor    = Color.FromArgb(52, 152, 219);
            btnThemSP.ForeColor    = Color.White;
            btnThemSP.FlatStyle    = FlatStyle.Flat;
            btnThemSP.Font         = new Font("Segoe UI", 9f, FontStyle.Bold);
            btnThemSP.Cursor       = Cursors.Hand;
            btnThemSP.Click       += btnThemSP_Click;

            // Row 3 – Ghi chú
            var lblGC = new Label { Text = "Ghi chú:", Location = new Point(labelX, 16 + rowH * 2), AutoSize = true, Font = new Font("Segoe UI", 9.5f) };
            txtGhiChu.Location     = new Point(controlX, 12 + rowH * 2);
            txtGhiChu.Width        = 420;
            txtGhiChu.Font         = new Font("Segoe UI", 9.5f);

            // Row 4 – Buttons
            btnLuu.Text            = "💾 Lưu Đơn Hàng";
            btnLuu.Location        = new Point(controlX, 12 + rowH * 3);
            btnLuu.Size            = new Size(150, 34);
            btnLuu.BackColor       = Color.FromArgb(39, 174, 96);
            btnLuu.ForeColor       = Color.White;
            btnLuu.FlatStyle       = FlatStyle.Flat;
            btnLuu.Font            = new Font("Segoe UI", 9f, FontStyle.Bold);
            btnLuu.Cursor          = Cursors.Hand;
            btnLuu.Click          += btnLuu_Click;

            btnHuy.Text            = "✕ Hủy";
            btnHuy.Location        = new Point(controlX + 160, 12 + rowH * 3);
            btnHuy.Size            = new Size(80, 34);
            btnHuy.BackColor       = Color.FromArgb(189, 195, 199);
            btnHuy.ForeColor       = Color.White;
            btnHuy.FlatStyle       = FlatStyle.Flat;
            btnHuy.Font            = new Font("Segoe UI", 9f);
            btnHuy.Cursor          = Cursors.Hand;
            btnHuy.Click          += btnHuy_Click;

            pnlForm.Controls.AddRange(new Control[]
            {
                lblKH, cboKH,
                lblSP, cboSP, lblSL, txtSoLuong, btnThemSP,
                lblGC, txtGhiChu,
                btnLuu, btnHuy
            });

            // ── Total bar ───────────────────────────────────────────────────────
            var pnlTotal = new Panel
            {
                Height    = 40,
                Dock      = DockStyle.Bottom,
                BackColor = Color.FromArgb(44, 62, 80),
                Padding   = new Padding(16, 8, 16, 0)
            };
            lblTotal.Text      = "Tổng tiền: 0 ₫";
            lblTotal.ForeColor = Color.White;
            lblTotal.Font      = new Font("Segoe UI", 11f, FontStyle.Bold);
            lblTotal.AutoSize  = true;
            lblTotal.Location  = new Point(16, 8);
            pnlTotal.Controls.Add(lblTotal);

            // ── DataGridView ────────────────────────────────────────────────────
            dgvCart.Dock                     = DockStyle.Fill;
            dgvCart.BackgroundColor          = Color.White;
            dgvCart.BorderStyle              = BorderStyle.None;
            dgvCart.RowHeadersVisible        = false;
            dgvCart.AllowUserToAddRows       = false;
            dgvCart.SelectionMode            = DataGridViewSelectionMode.FullRowSelect;
            dgvCart.AutoSizeColumnsMode      = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCart.ColumnHeadersDefaultCellStyle.BackColor  = Color.FromArgb(44, 62, 80);
            dgvCart.ColumnHeadersDefaultCellStyle.ForeColor  = Color.White;
            dgvCart.ColumnHeadersDefaultCellStyle.Font       = new Font("Segoe UI", 10f, FontStyle.Bold);
            dgvCart.EnableHeadersVisualStyles                 = false;
            dgvCart.RowTemplate.Height                        = 32;

            this.Controls.AddRange(new Control[] { dgvCart, pnlTotal, pnlForm, pnlTop });
            this.Dock = DockStyle.Fill;
        }

        // ─── Fields ───────────────────────────────────────────────────────────
        private DataGridView dgvCart;
        private ComboBox     cboKH, cboSP;
        private TextBox      txtSoLuong, txtGhiChu;
        private Button       btnThemSP, btnLuu, btnHuy;
        private Label        lblTotal;

        // Helper class cho ComboBox item
        private class ComboItem
        {
            public int    Id   { get; }
            public string Name { get; }
            public ComboItem(int id, string name) { Id = id; Name = name; }
            public override string ToString() => Name;
        }
    }
}
