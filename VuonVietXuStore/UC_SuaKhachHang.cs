using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VuonVietXuStore
{
    public partial class UC_SuaKhachHang : UserControl
    {
        private readonly string connString = ConfigurationManager.ConnectionStrings["VuonVietXuStore"].ConnectionString;
        private readonly int maKH;

        public UC_SuaKhachHang(int id, string tenKH, string sdt, string diaChi)
        {
            InitializeComponent();
            this.maKH = id;
            
            txtKH.Text = tenKH;
            txtSDT.Text = sdt;
            txtDiaChi.Text = diaChi;

            LoadCustomerDetails();
            SetupStyles();
        }

        private void SetupStyles()
        {
            this.BackColor = Color.FromArgb(242, 247, 244);

            lblTitle.Text = "HỒ SƠ KHÁCH HÀNG & TÍCH ĐIỂM";
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(18, 78, 44);

            label1.Text = "Họ và tên:";
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(64, 64, 64);

            label2.Text = "Số điện thoại:";
            label2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(64, 64, 64);

            label3.Text = "Địa chỉ:";
            label3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(64, 64, 64);

            lblDiem.Text = "Điểm tích lũy:";
            lblDiem.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDiem.ForeColor = Color.FromArgb(64, 64, 64);

            txtKH.Font = new Font("Segoe UI", 11F);
            txtSDT.Font = new Font("Segoe UI", 11F);
            txtDiaChi.Font = new Font("Segoe UI", 11F);
            txtDiemTichLuy.Font = new Font("Segoe UI", 11F);

            btnSave.Text = "💾 Lưu thay đổi";
            btnSave.BackColor = Color.FromArgb(18, 78, 44);
            btnSave.ForeColor = Color.White;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Cursor = Cursors.Hand;

            btnCancel.Text = "↩️ Quay lại";
            btnCancel.BackColor = Color.FromArgb(127, 140, 141);
            btnCancel.ForeColor = Color.White;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Cursor = Cursors.Hand;

            panelPointsCard.BackColor = Color.FromArgb(18, 78, 44);
            lblCardTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCardTitle.ForeColor = Color.FromArgb(200, 255, 210);
            
            lblCardPoints.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            lblCardPoints.ForeColor = Color.White;

            lblCardDesc.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            lblCardDesc.ForeColor = Color.FromArgb(220, 255, 220);

            lblHistory.Text = "📜 LỊCH SỬ GIAO DỊCH";
            lblHistory.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblHistory.ForeColor = Color.FromArgb(18, 78, 44);

            dgvHistory.BackgroundColor = Color.White;
            dgvHistory.BorderStyle = BorderStyle.None;
            dgvHistory.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvHistory.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvHistory.EnableHeadersVisualStyles = false;
            dgvHistory.GridColor = Color.FromArgb(224, 224, 224);
            dgvHistory.RowHeadersVisible = false;
            dgvHistory.RowTemplate.Height = 32;
            dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(18, 78, 44);
            dgvHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            dgvHistory.ColumnHeadersHeight = 35;

            dgvHistory.DefaultCellStyle.BackColor = Color.White;
            dgvHistory.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            dgvHistory.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F);
            dgvHistory.DefaultCellStyle.SelectionBackColor = Color.FromArgb(235, 247, 238);
            dgvHistory.DefaultCellStyle.SelectionForeColor = Color.FromArgb(18, 78, 44);
        }

        private void LoadCustomerDetails()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(connString))
                {
                    connect.Open();

                    // Điểm tích lũy
                    string queryPoints = "SELECT DiemTichLuy FROM KhachHang WHERE MaKH = @id";
                    using (SqlCommand cmd = new SqlCommand(queryPoints, connect))
                    {
                        cmd.Parameters.AddWithValue("@id", maKH);
                        object val = cmd.ExecuteScalar();
                        int points = val != DBNull.Value ? Convert.ToInt32(val) : 0;
                        
                        txtDiemTichLuy.Text = points.ToString();
                        lblCardPoints.Text = points.ToString("N0") + " ĐIỂM";
                    }

                    // Lịch sử mua hàng
                    string queryHistory = @"
                        SELECT 
                            MaDH, 
                            NgayDatHang, 
                            TongTien, 
                            TrangThai 
                        FROM DonHang 
                        WHERE MaKH = @id 
                        ORDER BY NgayDatHang DESC, MaDH DESC";
                    
                    using (SqlCommand cmd = new SqlCommand(queryHistory, connect))
                    {
                        cmd.Parameters.AddWithValue("@id", maKH);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dgvHistory.DataSource = dt;
                        }
                    }
                }

                dgvHistory.AllowUserToAddRows = false;
                dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                if (dgvHistory.Columns["MaDH"] != null) dgvHistory.Columns["MaDH"].HeaderText = "Mã Đơn";
                if (dgvHistory.Columns["NgayDatHang"] != null)
                {
                    dgvHistory.Columns["NgayDatHang"].HeaderText = "Ngày Mua";
                    dgvHistory.Columns["NgayDatHang"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
                if (dgvHistory.Columns["TongTien"] != null)
                {
                    dgvHistory.Columns["TongTien"].HeaderText = "Tổng Tiền";
                    dgvHistory.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                    dgvHistory.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (dgvHistory.Columns["TrangThai"] != null)
                {
                    dgvHistory.Columns["TrangThai"].HeaderText = "Trạng Thái";
                    dgvHistory.Columns["TrangThai"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp dữ liệu khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKH.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connect = new SqlConnection(connString))
                {
                    connect.Open();
                    string query = @"
                        UPDATE KhachHang 
                        SET 
                            TenKH = @ten, 
                            SDT = @sdt, 
                            DiaChiKH = @dc,
                            DiemTichLuy = @diem
                        WHERE MaKH = @id";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@ten", txtKH.Text.Trim());
                        cmd.Parameters.AddWithValue("@sdt", txtSDT.Text.Trim());
                        cmd.Parameters.AddWithValue("@dc", txtDiaChi.Text.Trim());
                        
                        int points = 0;
                        int.TryParse(txtDiemTichLuy.Text, out points);
                        cmd.Parameters.AddWithValue("@diem", points);
                        
                        cmd.Parameters.AddWithValue("@id", maKH);

                        cmd.ExecuteNonQuery();
                    }
                }

                
                GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu thông tin: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        private void GoBack()
        {
            UC_KhachHang uc = new UC_KhachHang();
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