using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace VuonVietXuStore
{
    public partial class UC_Welcomeback : UserControl
    {
        // Khai báo các màu sắc chủ đạo theo tone Thực phẩm sạch
        private Color colorDefaultBg = Color.White;
        private Color colorHoverBg = Color.FromArgb(235, 247, 238); // Xanh lá cực nhẹ khi hover card
        private Color colorBorderDefault = Color.FromArgb(220, 230, 222);

        // Quản lý trạng thái Animation phóng to thu nhỏ của Card
        private Timer animationTimer = new Timer();
        private Panel activeCard = null;
        private bool isExpanding = true;
        private int targetWidth, targetHeight, targetX, targetY;

        public UC_Welcomeback()
        {
            InitializeComponent();

            // Bật DoubleBuffered để tránh hiện tượng màn hình bị giật/nháy
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.UpdateStyles();

            // Thiết lập ngày tháng hiện tại hiển thị ở Top Panel
            lblDate.Text = DateTime.Now.ToString("dddd, dd/MM/yyyy", new System.Globalization.CultureInfo("vi-VN"));

            SetupCardEffects();
            InitAnimation();
            ConfigureDataGridViews();
            LoadDashboardData();
        }

        // 1. CẤU HÌNH DATAGRIDVIEW ĐẸP CHUYÊN NGHIỆP
        private void ConfigureDataGridViews()
        {
            // Đơn hàng chờ xác nhận
            dgvChoXacNhan.AutoGenerateColumns = false;
            dgvChoXacNhan.Columns.Clear();
            dgvChoXacNhan.CellContentClick += dgvChoXacNhan_CellContentClick;

            dgvChoXacNhan.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Mã Đơn", HeaderText = "MÃ ĐƠN", Width = 90 });
            dgvChoXacNhan.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Khách Hàng", HeaderText = "KHÁCH HÀNG" });
            
            var colDate = new DataGridViewTextBoxColumn { DataPropertyName = "Ngày Đặt", HeaderText = "NGÀY ĐẶT", Width = 150 };
            colDate.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvChoXacNhan.Columns.Add(colDate);

            var colPrice = new DataGridViewTextBoxColumn { DataPropertyName = "Số Tiền", HeaderText = "TỔNG TIỀN", Width = 150 };
            colPrice.DefaultCellStyle.Format = "N0";
            colPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvChoXacNhan.Columns.Add(colPrice);

            DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
            btnCol.Name = "Duyet";
            btnCol.HeaderText = "HÀNH ĐỘNG";
            btnCol.Text = "Duyệt";
            btnCol.UseColumnTextForButtonValue = true;
            btnCol.FlatStyle = FlatStyle.Flat;
            btnCol.DefaultCellStyle.BackColor = Color.FromArgb(241, 196, 15); // Màu vàng của TwelveFit
            btnCol.DefaultCellStyle.ForeColor = Color.Black;
            btnCol.DefaultCellStyle.SelectionBackColor = Color.FromArgb(241, 196, 15);
            btnCol.DefaultCellStyle.SelectionForeColor = Color.Black;
            btnCol.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCol.Width = 100;
            dgvChoXacNhan.Columns.Add(btnCol);

            // Lịch sử đơn hàng
            dgvLichSuDonHang.AutoGenerateColumns = false;
            dgvLichSuDonHang.Columns.Clear();
            dgvLichSuDonHang.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Mã Đơn", HeaderText = "MÃ ĐƠN", Width = 90 });
            dgvLichSuDonHang.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Khách Hàng", HeaderText = "KHÁCH HÀNG" });

            var colHistoryPrice = new DataGridViewTextBoxColumn { DataPropertyName = "Tổng Tiền", HeaderText = "TỔNG TIỀN", Width = 130 };
            colHistoryPrice.DefaultCellStyle.Format = "N0";
            colHistoryPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLichSuDonHang.Columns.Add(colHistoryPrice);

            var colStatus = new DataGridViewTextBoxColumn { DataPropertyName = "Trạng Thái", HeaderText = "TRẠNG THÁI", Width = 130 };
            colStatus.DefaultCellStyle.ForeColor = Color.FromArgb(46, 204, 113); // Màu xanh lá "Đã thanh toán"
            colStatus.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvLichSuDonHang.Columns.Add(colStatus);
        }

        // 2. NẠP DỮ LIỆU TỪ CƠ SỞ DỮ LIỆU SQL SERVER
        private void LoadDashboardData()
        {
            string connStr = ConfigurationManager.ConnectionStrings["VuonVietXuStore"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    // 1. Thống kê sản phẩm
                    SqlCommand cmdProducts = new SqlCommand("SELECT COUNT(*) FROM SanPham", conn);
                    int totalProducts = (int)cmdProducts.ExecuteScalar();
                    lblCard1Val.Text = totalProducts.ToString();

                    // 2. Thống kê Doanh thu
                    SqlCommand cmdRevenue = new SqlCommand("SELECT SUM(TongTien) FROM DonHang", conn);
                    object revenueObj = cmdRevenue.ExecuteScalar();
                    decimal totalRevenue = revenueObj != DBNull.Value ? Convert.ToDecimal(revenueObj) : 0;
                    lblCard2Val.Text = totalRevenue.ToString("N0") + " đ";

                    // 3. Thống kê Sản phẩm sắp hết hàng (tồn < 15)
                    SqlCommand cmdLowStock = new SqlCommand("SELECT COUNT(*) FROM SanPham WHERE SoLuongTon < 15", conn);
                    int lowStockCount = (int)cmdLowStock.ExecuteScalar();
                    lblCard3Val.Text = lowStockCount.ToString();

                    // 4. Thống kê Khách hàng
                    SqlCommand cmdCustomers = new SqlCommand("SELECT COUNT(*) FROM KhachHang", conn);
                    int totalCustomers = (int)cmdCustomers.ExecuteScalar();
                    lblCard4Val.Text = totalCustomers.ToString();

                    // 5. Nạp Đơn hàng chờ xác nhận (3 đơn hàng mới nhất)
                    string queryPending = @"
                        SELECT TOP 3 
                            dh.MaDH AS [Mã Đơn], 
                            COALESCE(kh.TenKH, N'Khách vãng lai') AS [Khách Hàng], 
                            dh.NgayDatHang AS [Ngày Đặt], 
                            dh.TongTien AS [Số Tiền]
                        FROM DonHang dh
                        LEFT JOIN KhachHang kh ON dh.MaKH = kh.MaKH
                        ORDER BY dh.MaDH DESC";

                    SqlDataAdapter daPending = new SqlDataAdapter(queryPending, conn);
                    DataTable dtPending = new DataTable();
                    daPending.Fill(dtPending);
                    dgvChoXacNhan.DataSource = dtPending;

                    // 6. Nạp Lịch sử đơn hàng (5 đơn hàng gần đây)
                    string queryHistory = @"
                        SELECT TOP 5 
                            dh.MaDH AS [Mã Đơn], 
                            COALESCE(kh.TenKH, N'Khách vãng lai') AS [Khách Hàng], 
                            dh.TongTien AS [Tổng Tiền],
                            dh.TrangThai AS [Trạng Thái]
                        FROM DonHang dh
                        LEFT JOIN KhachHang kh ON dh.MaKH = kh.MaKH
                        ORDER BY dh.NgayDatHang DESC, dh.MaDH DESC";

                    SqlDataAdapter daHistory = new SqlDataAdapter(queryHistory, conn);
                    DataTable dtHistory = new DataTable();
                    daHistory.Fill(dtHistory);
                    dgvLichSuDonHang.DataSource = dtHistory;

                    // 7. Nạp danh sách sản phẩm sắp hết hàng (FlowLayoutPanel)
                    flpSapHetHang.Controls.Clear();
                    string queryLowStockList = "SELECT TOP 5 TenSP, SoLuongTon, GiaBan FROM SanPham WHERE SoLuongTon < 15 ORDER BY SoLuongTon ASC";
                    SqlCommand cmdLowStockList = new SqlCommand(queryLowStockList, conn);
                    using (SqlDataReader reader = cmdLowStockList.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string tenSp = reader.GetString(0);
                            int tonKho = reader.GetInt32(1);
                            decimal giaBan = reader.GetDecimal(2);

                            Panel itemPanel = CreateLowStockItem(tenSp, tonKho, giaBan);
                            flpSapHetHang.Controls.Add(itemPanel);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi nạp dữ liệu Dashboard: " + ex.Message);
                }
            }
        }

        // 3. TẠO ITEM SẢN PHẨM SẮP HẾT HÀNG TRỰC QUAN (GIỐNG LỊCH HỌC SẮP TỚI)
        private Panel CreateLowStockItem(string tenSp, int tonKho, decimal giaBan)
        {
            Panel panel = new Panel();
            panel.Size = new Size(flpSapHetHang.Width - 25, 60);
            panel.BackColor = Color.FromArgb(248, 249, 250); // Màu nền xám nhẹ
            panel.Margin = new Padding(0, 0, 0, 8);
            panel.Padding = new Padding(5);

            panel.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(Color.FromArgb(230, 235, 232), 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
                }
            };

            // Badge Tồn kho màu đỏ nổi bật
            Label lblBadge = new Label();
            lblBadge.Text = "Tồn: " + tonKho;
            lblBadge.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblBadge.ForeColor = Color.White;
            lblBadge.BackColor = Color.FromArgb(231, 76, 60); // Màu đỏ
            lblBadge.TextAlign = ContentAlignment.MiddleCenter;
            lblBadge.Size = new Size(58, 26);
            lblBadge.Location = new Point(10, 17);

            // Bo tròn nhẹ cho badge bằng Region
            GraphicsPath path = new GraphicsPath();
            int r = 6;
            path.AddArc(0, 0, r, r, 180, 90);
            path.AddArc(lblBadge.Width - r, 0, r, r, 270, 90);
            path.AddArc(lblBadge.Width - r, lblBadge.Height - r, r, r, 0, 90);
            path.AddArc(0, lblBadge.Height - r, r, r, 90, 90);
            lblBadge.Region = new Region(path);

            // Tên sản phẩm
            Label lblName = new Label();
            lblName.Text = tenSp;
            lblName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblName.ForeColor = Color.FromArgb(44, 62, 80);
            lblName.Location = new Point(80, 10);
            lblName.Size = new Size(panel.Width - 90, 20);

            // Giá bán
            Label lblPrice = new Label();
            lblPrice.Text = "Giá bán: " + giaBan.ToString("N0") + " đ";
            lblPrice.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            lblPrice.ForeColor = Color.Gray;
            lblPrice.Location = new Point(80, 32);
            lblPrice.Size = new Size(panel.Width - 90, 18);

            panel.Controls.Add(lblBadge);
            panel.Controls.Add(lblName);
            panel.Controls.Add(lblPrice);

            return panel;
        }

        // 4. XỬ LÝ SỰ KIỆN DUYỆT ĐƠN HÀNG THỰC TẾ
        private void dgvChoXacNhan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvChoXacNhan.Columns[e.ColumnIndex].Name == "Duyet")
            {
                var maDH = dgvChoXacNhan.Rows[e.RowIndex].Cells[0].Value;
                var khachHang = dgvChoXacNhan.Rows[e.RowIndex].Cells[1].Value;

                try
                {
                    string connStr = ConfigurationManager.ConnectionStrings["VuonVietXuStore"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        string query = "UPDATE DonHang SET TrangThai = N'Hoàn thành' WHERE MaDH = @maDH";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@maDH", maDH);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show($"Đã phê duyệt đơn hàng #{maDH} của khách hàng [{khachHang}] thành công!", 
                                    "Duyệt Đơn Hàng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDashboardData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi phê duyệt đơn hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // 5. CO GIÃN TỰ ĐỘNG THEO MÀN HÌNH (RESPONSIVE)
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (panelScrollContainer == null || panelCards == null || card1 == null) return;

            // Đặt kích thước container cuộn khớp vùng dưới panelTop
            panelScrollContainer.Width = this.Width;
            panelScrollContainer.Height = this.Height - panelTop.Height;

            int contentWidth = (int)(panelScrollContainer.Width * 0.94);
            int contentLeft = (panelScrollContainer.Width - contentWidth) / 2;
            if (contentLeft < 15) contentLeft = 15;

            // Cập nhật kích thước panelCards
            panelCards.Left = contentLeft;
            panelCards.Width = contentWidth;

            int totalSpacing = 60;
            int cardWidth = (contentWidth - totalSpacing) / 4;
            if (cardWidth < 120) cardWidth = 120;
            int cardHeight = 140;

            card1.Size = new Size(cardWidth, cardHeight);
            card2.Size = new Size(cardWidth, cardHeight);
            card3.Size = new Size(cardWidth, cardHeight);
            card4.Size = new Size(cardWidth, cardHeight);

            card1.Left = 0;
            card2.Left = cardWidth + 20;
            card3.Left = (cardWidth * 2) + 40;
            card4.Left = (cardWidth * 3) + 60;

            // Sắp xếp các control bên trong Card
            LayoutControlsInCard(card1, lblCard1Icon, lblCard1Badge, lblCard1Title, lblCard1Val);
            LayoutControlsInCard(card2, lblCard2Icon, lblCard2Badge, lblCard2Title, lblCard2Val);
            LayoutControlsInCard(card3, lblCard3Icon, lblCard3Badge, lblCard3Title, lblCard3Val);
            LayoutControlsInCard(card4, lblCard4Icon, lblCard4Badge, lblCard4Title, lblCard4Val);

            // Cập nhật kích thước panelMiddle (Đơn hàng chờ xác nhận)
            panelMiddle.Left = contentLeft;
            panelMiddle.Width = contentWidth;
            dgvChoXacNhan.Width = contentWidth;

            // Cập nhật kích thước panelBottom
            panelBottom.Left = contentLeft;
            panelBottom.Width = contentWidth;

            int spacing = 20;
            int leftColWidth = (int)(contentWidth * 0.6) - (spacing / 2);
            int rightColWidth = contentWidth - leftColWidth - spacing;

            panelBottomLeft.Width = leftColWidth;
            dgvLichSuDonHang.Width = leftColWidth;

            panelBottomRight.Left = leftColWidth + spacing;
            panelBottomRight.Width = rightColWidth;
            flpSapHetHang.Width = rightColWidth;

            // Resize lại các panel sản phẩm sắp hết hàng theo chiều rộng mới
            foreach (Control ctrl in flpSapHetHang.Controls)
            {
                if (ctrl is Panel p)
                {
                    p.Width = flpSapHetHang.Width - 25;
                }
            }
        }

        private void LayoutControlsInCard(Panel card, Label icon, Label badge, Label title, Label val)
        {
            if (icon == null || title == null || val == null) return;

            icon.Location = new Point(15, 10);
            icon.AutoSize = true;

            if (badge != null)
            {
                badge.AutoSize = true;
                badge.Location = new Point(card.Width - badge.Width - 15, 15);
            }

            title.Location = new Point(15, 65);
            title.Width = card.Width - 30;

            val.Location = new Point(15, 90);
            val.Width = card.Width - 30;
        }

        // 6. TẠO HIỆU ỨNG DI CHUỘT (HOVER EFFECT)
        private void SetupCardEffects()
        {
            Panel[] cards = { card1, card2, card3, card4 };
            foreach (var card in cards)
            {
                card.Cursor = Cursors.Hand;
                card.MouseEnter += Card_MouseEnter;
                card.MouseLeave += Card_MouseLeave;

                foreach (Control child in card.Controls)
                {
                    child.Cursor = Cursors.Hand;
                    child.MouseEnter += (s, e) => Card_MouseEnter(card, e);
                    child.MouseLeave += (s, e) => Card_MouseLeave(card, e);
                }
            }
        }

        private void Card_MouseEnter(object sender, EventArgs e)
        {
            Panel card = sender as Panel;
            if (card != null)
            {
                card.BackColor = colorHoverBg;
                StartCardAnimation(card, true);
            }
        }

        private void Card_MouseLeave(object sender, EventArgs e)
        {
            Panel card = sender as Panel;
            if (card != null)
            {
                card.BackColor = colorDefaultBg;
                StartCardAnimation(card, false);
            }
        }

        // 7. ANIMATION PHÓNG TO CARD NHẸ NHÀNG
        private void InitAnimation()
        {
            animationTimer.Interval = 10;
            animationTimer.Tick += AnimationTimer_Tick;
        }

        private void StartCardAnimation(Panel card, bool expand)
        {
            animationTimer.Stop();
            activeCard = card;
            isExpanding = expand;

            int baseWidth = (panelCards.Width - 60) / 4;
            int baseLeft = 0;
            if (card == card2) baseLeft = baseWidth + 20;
            if (card == card3) baseLeft = (baseWidth * 2) + 40;
            if (card == card4) baseLeft = (baseWidth * 3) + 60;

            if (expand)
            {
                targetWidth = baseWidth + 8;
                targetHeight = 146;
                targetX = baseLeft - 4;
                targetY = -3;
            }
            else
            {
                targetWidth = baseWidth;
                targetHeight = 140;
                targetX = baseLeft;
                targetY = 0;
            }

            animationTimer.Start();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (activeCard == null) return;
            int stepSize = 2;

            if (activeCard.Width != targetWidth)
                activeCard.Width += (activeCard.Width < targetWidth) ? stepSize : -stepSize;

            if (activeCard.Height != targetHeight)
                activeCard.Height += (activeCard.Height < targetHeight) ? stepSize : -stepSize;

            if (activeCard.Left != targetX)
                activeCard.Left += (activeCard.Left < targetX) ? 1 : -1;

            if (activeCard.Top != targetY)
                activeCard.Top += (activeCard.Top < targetY) ? 1 : -1;

            if (activeCard.Width == targetWidth && activeCard.Height == targetHeight && activeCard.Top == targetY)
            {
                animationTimer.Stop();
            }
        }
    }
}