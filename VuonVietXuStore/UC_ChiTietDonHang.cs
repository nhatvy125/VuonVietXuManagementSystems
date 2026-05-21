using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using VuonVietXuStore.Helpers;

namespace VuonVietXuStore
{
    /// <summary>
    /// UC_ChiTietDonHang: Xem chi tiết đơn hàng + xuất hóa đơn in / lưu file.
    /// </summary>
    public partial class UC_ChiTietDonHang : UserControl
    {
        private readonly int _maDH;
        private DataTable _chiTiet;
        private string    _tenKH, _sdt, _diaChi, _ghiChu, _trangThai;
        private DateTime  _ngay;
        private decimal   _tongTien;

        public UC_ChiTietDonHang(int maDonHang)
        {
            _maDH = maDonHang;
            InitializeComponent();
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                LoadDonHang();
                LoadChiTiet();
            }
        }

        // ─── Load dữ liệu ────────────────────────────────────────────────────
        private void LoadDonHang()
        {
            string sql = @"
                SELECT dh.MaDH AS MaDonHang, kh.TenKH, kh.SDT, kh.DiaChiKH,
                       dh.NgayDatHang, dh.TongTien, dh.TrangThai, dh.GhiChu
                FROM DonHang dh
                JOIN KhachHang kh ON dh.MaKH = kh.MaKH
                WHERE dh.MaDH = @id";

            var dt = DbHelper.ExecuteQuery(sql, cmd => cmd.Parameters.AddWithValue("@id", _maDH));
            if (dt.Rows.Count == 0) return;

            DataRow r = dt.Rows[0];
            _tenKH    = r["TenKH"].ToString();
            _sdt      = r["SDT"].ToString();
            _diaChi   = r["DiaChiKH"].ToString();
            _ngay     = Convert.ToDateTime(r["NgayDatHang"]);
            _tongTien = Convert.ToDecimal(r["TongTien"]);
            _trangThai= r["TrangThai"].ToString();
            _ghiChu   = r["GhiChu"].ToString();

            lblMaDH.Text     = $"📄 Đơn hàng #{_maDH}";
            lblKH.Text       = $"👤 Khách hàng:  {_tenKH}";
            lblSDT.Text      = $"📱 SĐT:         {_sdt}";
            lblDiaChi.Text   = $"📍 Địa chỉ:     {_diaChi}";
            lblNgay.Text     = $"📅 Ngày đặt:    {_ngay:dd/MM/yyyy HH:mm}";

            // Trang thai label
            lblTrangThai.Text = _trangThai;
            UpdateTrangThaiColor();

            lblTongTien.Text = $"TỔNG TIỀN:  {_tongTien:N0} ₫";
            if (!string.IsNullOrEmpty(_ghiChu))
                lblGhiChu.Text = $"📝 Ghi chú:     {_ghiChu}";

            // Chon trang thai hien tai trong combo
            if (cboTrangThai != null)
                cboTrangThai.Text = _trangThai;
        }

        private void LoadChiTiet()
        {
            string sql = @"
                SELECT sp.TenSP AS [Sản phẩm],
                       ct.SoLuong AS [Số lượng],
                       ct.Gia AS [Đơn giá],
                       ct.SoLuong * ct.Gia AS [Thành tiền]
                FROM ChiTietDH ct
                JOIN SanPham sp ON ct.MaSP = sp.MaSP
                WHERE ct.MaDH = @id";

            _chiTiet = DbHelper.ExecuteQuery(sql, cmd => cmd.Parameters.AddWithValue("@id", _maDH));
            dgvChiTiet.DataSource = _chiTiet;

            GridHelper.ApplyStandardStyle(dgvChiTiet);
            if (dgvChiTiet.Columns["Đơn giá"]   != null) dgvChiTiet.Columns["Đơn giá"].DefaultCellStyle.Format   = "N0";
            if (dgvChiTiet.Columns["Thành tiền"] != null) dgvChiTiet.Columns["Thành tiền"].DefaultCellStyle.Format = "N0";
        }

        // ─── Xuất hóa đơn ─────────────────────────────────────────────────────────────────
        private void btnXuatHoaDon_Click(object sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog())
            {
                dlg.Filter   = "XML Hóa đơn điện tử|*.xml|HTML file|*.html|Text file|*.txt";
                dlg.FileName = $"HoaDon_{_maDH}_{DateTime.Now:yyyyMMdd}";
                if (dlg.ShowDialog() != DialogResult.OK) return;

                string content;
                switch (dlg.FilterIndex)
                {
                    case 1:  content = BuildXmlInvoice();  break;
                    case 2:  content = BuildHtmlInvoice(); break;
                    default: content = BuildTextInvoice(); break;
                }

                System.IO.File.WriteAllText(dlg.FileName, content, Encoding.UTF8);
                MessageBox.Show($"✅ Xuất hóa đơn thành công!\n{dlg.FileName}",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start(dlg.FileName);
            }
        }

        // ─── XML Invoice (chuẩn hóa đơn điện tử Việt Nam) ───────────────────────
        private string BuildXmlInvoice()
        {
            var settings = new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 };
            var sb = new StringBuilder();
            using (var w = XmlWriter.Create(sb, settings))
            {
                w.WriteStartDocument();
                w.WriteStartElement("HDon");  // Hóa Đơn
                w.WriteAttributeString("xmlns", "http://lading.mof.gov.vn/XML/QD15");

                // ─ Thông tin chung
                w.WriteStartElement("TTChung");
                w.WriteElementString("SHDon",    _maDH.ToString());
                w.WriteElementString("NLap",     _ngay.ToString("yyyy-MM-dd"));
                w.WriteElementString("SLap",     "1");
                w.WriteElementString("DVTTe",    "VND");
                w.WriteElementString("TyGia",    "1");
                w.WriteElementString("HTToan",   "TM/CK");
                w.WriteElementString("TrangThai",_trangThai);
                w.WriteEndElement(); // TTChung

                // ─ Người bán
                w.WriteStartElement("NBan");
                w.WriteElementString("Ten",      "Vuơn Việt Xưa Store");
                w.WriteElementString("MST",      "0000000000");
                w.WriteElementString("DChi",     "Việt Nam");
                w.WriteEndElement(); // NBan

                // ─ Người mua
                w.WriteStartElement("NMua");
                w.WriteElementString("Ten",   _tenKH);
                w.WriteElementString("SDT",   _sdt);
                w.WriteElementString("DChi",  _diaChi);
                if (!string.IsNullOrEmpty(_ghiChu))
                    w.WriteElementString("GhiChu", _ghiChu);
                w.WriteEndElement(); // NMua

                // ─ Danh sách hàng hóa
                w.WriteStartElement("DSHHDVu");
                if (_chiTiet != null)
                {
                    int stt = 1;
                    foreach (DataRow r in _chiTiet.Rows)
                    {
                        w.WriteStartElement("HHDVu");
                        w.WriteAttributeString("STT", stt++.ToString());
                        w.WriteElementString("Ten",     r["Sản phẩm"].ToString());
                        w.WriteElementString("DVTinh",  "Chiếc");
                        w.WriteElementString("SLuong",  r["Số lượng"].ToString());
                        w.WriteElementString("DGia",    Convert.ToDecimal(r["Đơn giá"]).ToString("F0"));
                        w.WriteElementString("TLCKhau", "0");
                        w.WriteElementString("STCKhau", "0");
                        w.WriteElementString("ThTien",  Convert.ToDecimal(r["Thành tiền"]).ToString("F0"));
                        w.WriteElementString("TThue",   "KHAC");
                        w.WriteEndElement(); // HHDVu
                    }
                }
                w.WriteEndElement(); // DSHHDVu

                // ─ Tổng cộng
                w.WriteStartElement("TToan");
                w.WriteElementString("TgTCThue",  _tongTien.ToString("F0"));
                w.WriteElementString("TgTThue",   "0");
                w.WriteElementString("TgTCKhau",  "0");
                w.WriteElementString("TongTToan", _tongTien.ToString("F0"));
                w.WriteEndElement(); // TToan

                w.WriteEndElement(); // HDon
                w.WriteEndDocument();
            }
            return sb.ToString();
        }

        // ─── Đổi trạng thái đơn hàng ──────────────────────────────────────────
        private void UpdateTrangThaiColor()
        {
            switch (_trangThai)
            {
                case "Hoàn thành":
                    lblTrangThai.ForeColor = Color.FromArgb(39, 174, 96);
                    break;
                case "Đang xử lý":
                case "Đang đóng gói":
                case "Đang giao":
                    lblTrangThai.ForeColor = Color.FromArgb(230, 126, 34);
                    break;
                case "Đã hủy":
                    lblTrangThai.ForeColor = Color.FromArgb(231, 76, 60);
                    break;
                default:
                    lblTrangThai.ForeColor = Color.FromArgb(52, 73, 94);
                    break;
            }
            lblTrangThai.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
        }

        private void btnDoiTrangThai_Click(object sender, EventArgs e)
        {
            string newStatus = cboTrangThai.Text.Trim();
            if (string.IsNullOrEmpty(newStatus))
            {
                MessageBox.Show("Vui lòng chọn trạng thái.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DbHelper.ExecuteNonQuery(
                "UPDATE DonHang SET TrangThai = @tt WHERE MaDH = @id",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@tt",  newStatus);
                    cmd.Parameters.AddWithValue("@id", _maDH);
                });

            _trangThai = newStatus;
            lblTrangThai.Text = newStatus;
            UpdateTrangThaiColor();
            MessageBox.Show($"✅ Đã cập nhật trạng thái: {newStatus}",
                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ─── In trực tiếp ────────────────────────────────────────────────────
        private void btnIn_Click(object sender, EventArgs e)
        {
            var pd = new PrintDocument();
            pd.PrintPage += PrintInvoicePage;

            using (var preview = new PrintPreviewDialog())
            {
                preview.Document = pd;
                preview.WindowState = FormWindowState.Maximized;
                preview.ShowDialog();
            }
        }

        private void PrintInvoicePage(object sender, PrintPageEventArgs e)
        {
            var g      = e.Graphics;
            var y      = 30;
            var left   = 40;
            var width  = e.PageBounds.Width - 80;

            var fontTitle  = new Font("Arial", 14f, FontStyle.Bold);
            var fontHeader = new Font("Arial", 10f, FontStyle.Bold);
            var fontBody   = new Font("Arial", 9f);
            var fontBold   = new Font("Arial", 9f, FontStyle.Bold);

            // Tiêu đề
            g.DrawString("VƯỜN VIỆT XƯ STORE", fontTitle, Brushes.DarkSlateBlue, left, y); y += 28;
            g.DrawString("─────────────────────────────────────────", fontBody, Brushes.Gray, left, y); y += 16;
            g.DrawString("HÓA ĐƠN BÁN HÀNG", new Font("Arial", 12f, FontStyle.Bold), Brushes.Black, left + width/2 - 70, y); y += 24;

            g.DrawString($"Mã đơn hàng : #{_maDH}", fontBody, Brushes.Black, left, y); y += 16;
            g.DrawString($"Khách hàng  : {_tenKH}", fontBody, Brushes.Black, left, y); y += 16;
            g.DrawString($"Ngày đặt    : {_ngay:dd/MM/yyyy HH:mm}", fontBody, Brushes.Black, left, y); y += 16;
            if (_ghiChu.Length > 0)
            {
                g.DrawString($"Ghi chú     : {_ghiChu}", fontBody, Brushes.Black, left, y); y += 16;
            }
            y += 8;
            g.DrawString("─────────────────────────────────────────", fontBody, Brushes.Gray, left, y); y += 16;

            // Header bảng
            g.DrawString("Sản phẩm",   fontHeader, Brushes.Black, left,         y);
            g.DrawString("SL",         fontHeader, Brushes.Black, left + 240,   y);
            g.DrawString("Đơn giá",    fontHeader, Brushes.Black, left + 280,   y);
            g.DrawString("Thành tiền", fontHeader, Brushes.Black, left + 380,   y);
            y += 18;
            g.DrawString("─────────────────────────────────────────", fontBody, Brushes.Gray, left, y); y += 14;

            // Rows
            if (_chiTiet != null)
            {
                foreach (DataRow r in _chiTiet.Rows)
                {
                    g.DrawString(r["Sản phẩm"].ToString(), fontBody, Brushes.Black, left,       y);
                    g.DrawString(r["Số lượng"].ToString(), fontBody, Brushes.Black, left + 240, y);
                    g.DrawString(Convert.ToDecimal(r["Đơn giá"]).ToString("N0"),   fontBody, Brushes.Black, left + 280, y);
                    g.DrawString(Convert.ToDecimal(r["Thành tiền"]).ToString("N0"),fontBody, Brushes.Black, left + 380, y);
                    y += 16;
                }
            }

            y += 8;
            g.DrawString("─────────────────────────────────────────", fontBody, Brushes.Gray, left, y); y += 14;
            g.DrawString($"TỔNG TIỀN: {_tongTien:N0} ₫", fontBold, Brushes.DarkSlateBlue, left + 260, y); y += 30;
            g.DrawString("Cảm ơn quý khách! Hẹn gặp lại.", fontBody, Brushes.Gray, left + 90, y);
        }

        // ─── Build nội dung hóa đơn ─────────────────────────────────────────
        private string BuildTextInvoice()
        {
            var sb = new StringBuilder();
            sb.AppendLine("===== VƯỜN VIỆT XƯ STORE =====");
            sb.AppendLine("HÓA ĐƠN BÁN HÀNG");
            sb.AppendLine(new string('-', 45));
            sb.AppendLine($"Mã đơn hàng : #{_maDH}");
            sb.AppendLine($"Khách hàng  : {_tenKH}");
            sb.AppendLine($"Ngày đặt    : {_ngay:dd/MM/yyyy HH:mm}");
            sb.AppendLine($"Trạng thái  : {_trangThai}");
            if (_ghiChu.Length > 0) sb.AppendLine($"Ghi chú     : {_ghiChu}");
            sb.AppendLine(new string('-', 45));
            sb.AppendLine($"{"Sản phẩm",-28} {"SL",4} {"Đơn giá",12} {"Thành tiền",14}");
            sb.AppendLine(new string('-', 45));

            if (_chiTiet != null)
                foreach (DataRow r in _chiTiet.Rows)
                    sb.AppendLine($"{r["Sản phẩm"],-28} {r["Số lượng"],4} {Convert.ToDecimal(r["Đơn giá"]):N0,12} {Convert.ToDecimal(r["Thành tiền"]):N0,14}");

            sb.AppendLine(new string('=', 45));
            sb.AppendLine($"TỔNG TIỀN: {_tongTien:N0} ₫");
            sb.AppendLine(new string('=', 45));
            sb.AppendLine("Cảm ơn quý khách! Hẹn gặp lại.");
            return sb.ToString();
        }

        private string BuildHtmlInvoice()
        {
            var rows = new StringBuilder();
            if (_chiTiet != null)
                foreach (DataRow r in _chiTiet.Rows)
                    rows.AppendLine($@"<tr>
                        <td>{r["Sản phẩm"]}</td>
                        <td style='text-align:center'>{r["Số lượng"]}</td>
                        <td style='text-align:right'>{Convert.ToDecimal(r["Đơn giá"]):N0} ₫</td>
                        <td style='text-align:right'>{Convert.ToDecimal(r["Thành tiền"]):N0} ₫</td>
                    </tr>");

            return $@"<!DOCTYPE html>
<html lang='vi'>
<head><meta charset='UTF-8'><title>Hóa đơn #{_maDH}</title>
<style>
  body {{ font-family: 'Segoe UI', sans-serif; max-width: 700px; margin: 40px auto; color: #333; }}
  h1 {{ color: #2c3e50; }} h2 {{ color: #7f8c8d; font-weight: normal; }}
  table {{ width:100%; border-collapse: collapse; margin-top: 20px; }}
  th {{ background: #2c3e50; color: white; padding: 10px; text-align: left; }}
  td {{ padding: 8px 10px; border-bottom: 1px solid #eee; }}
  tr:hover td {{ background: #f5f7fa; }}
  .total {{ font-size: 1.2em; font-weight: bold; color: #27ae60; text-align: right; margin-top: 16px; }}
  .info p {{ margin: 4px 0; }}
</style>
</head>
<body>
  <h1>🏪 Vườn Việt Xưa Store</h1>
  <h2>Hóa Đơn Bán Hàng</h2>
  <hr>
  <div class='info'>
    <p><b>Mã đơn hàng:</b> #{_maDH}</p>
    <p><b>Khách hàng:</b> {_tenKH}</p>
    <p><b>Ngày đặt:</b> {_ngay:dd/MM/yyyy HH:mm}</p>
    <p><b>Trạng thái:</b> {_trangThai}</p>
    {(_ghiChu.Length > 0 ? $"<p><b>Ghi chú:</b> {_ghiChu}</p>" : "")}
  </div>
  <table>
    <thead><tr><th>Sản phẩm</th><th>Số lượng</th><th>Đơn giá</th><th>Thành tiền</th></tr></thead>
    <tbody>{rows}</tbody>
  </table>
  <p class='total'>TỔNG TIỀN: {_tongTien:N0} ₫</p>
  <hr>
  <p style='color:#999; text-align:center'>Cảm ơn quý khách! Hẹn gặp lại.</p>
</body></html>";
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            Control p = this;
            while (p != null && !(p is Panel)) p = p.Parent;
            if (p is Panel panel)
            {
                var uc = new UC_BanHang();
                panel.Controls.Clear();
                uc.Dock = DockStyle.Fill;
                panel.Controls.Add(uc);
            }
        }

        // ─── Designer stub ────────────────────────────────────────────────────
        private void InitializeComponent()
        {
            this.dgvChiTiet      = new DataGridView();
            this.lblMaDH         = new Label();
            this.lblKH           = new Label();
            this.lblSDT          = new Label();
            this.lblDiaChi       = new Label();
            this.lblNgay         = new Label();
            this.lblTrangThai    = new Label();
            this.lblTongTien     = new Label();
            this.lblGhiChu       = new Label();
            this.btnXuatHoaDon   = new Button();
            this.btnIn           = new Button();
            this.btnQuayLai      = new Button();
            this.cboTrangThai    = new ComboBox();
            this.btnDoiTrangThai = new Button();

            var pnlTop   = new Panel { Height = 60, Dock = DockStyle.Top, BackColor = Color.FromArgb(44, 62, 80), Padding = new Padding(12, 0, 12, 0) };
            var lblTitle = new Label { Text = "CHI TIẾT ĐƠN HÀNG", ForeColor = Color.White, Font = new Font("Segoe UI", 14f, FontStyle.Bold), AutoSize = true, Location = new Point(12, 16) };

            btnQuayLai.Text = "← Quản lý bán hàng";
            btnQuayLai.Size = new Size(160, 34);
            btnQuayLai.Location = new Point(12, 14);
            btnQuayLai.BackColor = Color.FromArgb(127, 140, 141);
            btnQuayLai.ForeColor = Color.White;
            btnQuayLai.FlatStyle = FlatStyle.Flat;
            btnQuayLai.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            btnQuayLai.Click += btnQuayLai_Click;
            pnlTop.Controls.AddRange(new Control[] { lblTitle, btnQuayLai });

            // Info panel
            var pnlInfo = new Panel { Height = 160, Dock = DockStyle.Top, Padding = new Padding(16, 8, 16, 8), BackColor = Color.FromArgb(245, 247, 250) };

            int ly = 10;
            lblMaDH.AutoSize      = true; lblMaDH.Location      = new Point(16, ly); lblMaDH.Font = new Font("Segoe UI", 10f, FontStyle.Bold); ly += 22;
            lblKH.AutoSize        = true; lblKH.Location        = new Point(16, ly); ly += 20;
            lblSDT.AutoSize       = true; lblSDT.Location       = new Point(16, ly); ly += 20;
            lblDiaChi.AutoSize    = true; lblDiaChi.Location    = new Point(16, ly); ly += 20;
            lblNgay.AutoSize      = true; lblNgay.Location      = new Point(16, ly); ly += 20;

            // Trang thai
            var lblTTLabel = new Label { Text = "Trạng thái:", AutoSize = true, Location = new Point(16, ly), Font = new Font("Segoe UI", 9f) };
            lblTrangThai.AutoSize = true; lblTrangThai.Location = new Point(100, ly); lblTrangThai.Font = new Font("Segoe UI", 9f, FontStyle.Bold);

            // Ben phai: tong tien + xuat hoa don + in + doi trang thai
            lblGhiChu.AutoSize    = true; lblGhiChu.Location    = new Point(460, 10);

            lblTongTien.AutoSize  = true; lblTongTien.Location  = new Point(460, 34);
            lblTongTien.Font      = new Font("Segoe UI", 12f, FontStyle.Bold);
            lblTongTien.ForeColor = Color.FromArgb(39, 174, 96);

            btnXuatHoaDon.Text = "📄 Xuất Hóa Đơn"; btnXuatHoaDon.Location = new Point(460, 62); btnXuatHoaDon.Size = new Size(150, 30);
            btnXuatHoaDon.BackColor = Color.FromArgb(52, 152, 219); btnXuatHoaDon.ForeColor = Color.White; btnXuatHoaDon.FlatStyle = FlatStyle.Flat;
            btnXuatHoaDon.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            btnXuatHoaDon.Click += btnXuatHoaDon_Click;

            btnIn.Text = "🖨 In"; btnIn.Location = new Point(620, 62); btnIn.Size = new Size(70, 30);
            btnIn.BackColor = Color.FromArgb(142, 68, 173); btnIn.ForeColor = Color.White; btnIn.FlatStyle = FlatStyle.Flat;
            btnIn.Click += btnIn_Click;

            // Doi trang thai
            var lblDoiTT = new Label { Text = "Đổi trạng thái:", AutoSize = true, Location = new Point(460, 102), Font = new Font("Segoe UI", 9f) };
            cboTrangThai.Location = new Point(565, 99); cboTrangThai.Width = 130;
            cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangThai.Font = new Font("Segoe UI", 9f);
            cboTrangThai.Items.AddRange(new object[] { "Đang xử lý", "Đang đóng gói", "Đang giao", "Hoàn thành", "Đã hủy" });

            btnDoiTrangThai.Text = "✅ Lưu"; btnDoiTrangThai.Location = new Point(705, 98); btnDoiTrangThai.Size = new Size(60, 30);
            btnDoiTrangThai.BackColor = Color.FromArgb(39, 174, 96); btnDoiTrangThai.ForeColor = Color.White; btnDoiTrangThai.FlatStyle = FlatStyle.Flat;
            btnDoiTrangThai.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            btnDoiTrangThai.Click += btnDoiTrangThai_Click;

            pnlInfo.Controls.AddRange(new Control[] {
                lblMaDH, lblKH, lblSDT, lblDiaChi, lblNgay,
                lblTTLabel, lblTrangThai,
                lblGhiChu, lblTongTien,
                btnXuatHoaDon, btnIn,
                lblDoiTT, cboTrangThai, btnDoiTrangThai
            });

            dgvChiTiet.Dock = DockStyle.Fill;

            this.Controls.AddRange(new Control[] { dgvChiTiet, pnlInfo, pnlTop });
            this.Dock = DockStyle.Fill;
        }

        private DataGridView dgvChiTiet;
        private Label lblMaDH, lblKH, lblSDT, lblDiaChi, lblNgay, lblTrangThai, lblTongTien, lblGhiChu;
        private Button btnXuatHoaDon, btnIn, btnQuayLai, btnDoiTrangThai;
        private ComboBox cboTrangThai;
    }
}
