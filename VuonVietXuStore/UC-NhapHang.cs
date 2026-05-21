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

        // ======================================================================
        // 2. LOGIC TÌM KIẾM THEO THỜI GIAN THỰC (REAL-TIME FILTER)
        // ======================================================================
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Tìm kiếm số phiếu nhập hoặc nhà cung cấp...") return;

            string filterText = txtSearch.Text.Trim().Replace("'", "''");

            if (dataGridView1.DataSource is DataTable dt)
            {
                // Tự động lọc danh sách phiếu nhập dựa theo Mã phiếu nhập đang gõ trên TextBox
                dt.DefaultView.RowFilter = string.Format("Convert(MaPhieuNhap, 'System.String') LIKE '%{0}%'", filterText);
            }
        }

        // ======================================================================
        // 3. LOGIC TẢI DỮ LIỆU & CO GIÃN KHUNG HÌNH TỰ ĐỘNG
        // ======================================================================
        private void LoadData()
        {
            try
            {
                string query = "SELECT MaPhieuNhap, NgayNhap, TongTien FROM PhieuNhap";

                // Sử dụng khối using giải phóng kết nối SQL ngay sau khi nạp xong, tránh rò rỉ bộ nhớ
                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connect))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dataGridView1.DataSource = dt;
                        isLoaded = true;

                        // === THÀNH PHẦN QUAN TRỌNG: ÉP CÁC CỘT GIÃN FULL MÀN HÌNH ===
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        // Định dạng hiển thị tiền tệ VNĐ có phẩy phân cách hàng nghìn (Ví dụ: 1,500,000)
                        if (dataGridView1.Columns["TongTien"] != null)
                        {
                            dataGridView1.Columns["TongTien"].DefaultCellStyle.Format = "N0";
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
            using (FormPopupContainer popup = new FormPopupContainer(uc, "Thêm phiếu nhập mới"))
            {
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!isLoaded) return;
            if (e.RowIndex < 0) return;

            if (dataGridView1.Rows[e.RowIndex].Cells["MaPhieuNhap"].Value == null)
                return;

            int maPN = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["MaPhieuNhap"].Value);

            // Chuyển hướng sang màn hình Chi tiết phiếu nhập
            LoadUC(new UC_CTPN(maPN));
        }
    }
}