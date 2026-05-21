using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VuonVietXuStore
{
    public partial class UC_KhachHang : UserControl
    {
        SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["VuonVietXuStore"].ConnectionString);

        public UC_KhachHang()
        {
            InitializeComponent();
            SetupStyles();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                var connStr = ConfigurationManager.ConnectionStrings["VuonVietXuStore"];
                if (connStr != null)
                {
                    connect = new SqlConnection(connStr.ConnectionString);
                    LoadData();
                }
            }
        }

        private void SetupStyles()
        {
            // Background color matching the organic theme
            this.BackColor = Color.FromArgb(242, 247, 244);

            // Title styling
            label1.Text = "QUẢN LÝ KHÁCH HÀNG";
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(18, 78, 44);
            label1.Location = new Point(30, 25);

            // Button styling
            btnAdd.Text = "➕ Thêm khách hàng";
            btnAdd.BackColor = Color.FromArgb(18, 78, 44);
            btnAdd.ForeColor = Color.White;
            btnAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.Cursor = Cursors.Hand;

            // Textbox search
            txtSearch.Font = new Font("Segoe UI", 10.5F);

            // DataGridView styling
            dgvKH.BackgroundColor = Color.White;
            dgvKH.BorderStyle = BorderStyle.None;
            dgvKH.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvKH.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvKH.EnableHeadersVisualStyles = false;
            dgvKH.GridColor = Color.FromArgb(224, 224, 224);
            dgvKH.RowHeadersVisible = false;
            dgvKH.RowTemplate.Height = 35;
            dgvKH.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvKH.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(18, 78, 44);
            dgvKH.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvKH.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvKH.ColumnHeadersHeight = 40;

            dgvKH.DefaultCellStyle.BackColor = Color.White;
            dgvKH.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            dgvKH.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvKH.DefaultCellStyle.SelectionBackColor = Color.FromArgb(235, 247, 238);
            dgvKH.DefaultCellStyle.SelectionForeColor = Color.FromArgb(18, 78, 44);

            // Setup sorting and searching controls
            btnSearch.BackColor = Color.FromArgb(18, 78, 44);
            btnSearch.ForeColor = Color.White;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSearch.Cursor = Cursors.Hand;

            // Search-by dropdown
            cboSearchBy.Items.Clear();
            cboSearchBy.Items.AddRange(new object[] {
                "Mã khách hàng",
                "Họ tên",
                "Số điện thoại",
                "Địa chỉ"
            });
            cboSearchBy.SelectedIndex = 0;
            cboSearchBy.SelectedIndexChanged += (s, e) => LoadData();

            cboSort.Items.Clear();
            cboSort.Items.AddRange(new object[] {
                "Mặc định",
                "Tên khách hàng A-Z",
                "Tên khách hàng Z-A",
                "Tổng tích lũy giảm dần",
                "Tổng tích lũy tăng dần"
            });
            cboSort.SelectedIndex = 0;

            cboSort.SelectedIndexChanged += (s, e) => LoadData();
            btnSearch.Click += (s, e) => LoadData();
            txtSearch.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true; // Prevents beep
                    LoadData();
                }
            };
        }

        private void LoadData()
        {
            try
            {
                string searchVal = txtSearch.Text.Trim();
                string sortOrder = "kh.MaKH DESC";

                if (cboSort.SelectedIndex == 1) sortOrder = "kh.TenKH ASC";
                else if (cboSort.SelectedIndex == 2) sortOrder = "kh.TenKH DESC";
                else if (cboSort.SelectedIndex == 3) sortOrder = "[Tổng Tích Lũy] DESC";
                else if (cboSort.SelectedIndex == 4) sortOrder = "[Tổng Tích Lũy] ASC";

                string searchCol;
                switch (cboSearchBy.SelectedItem?.ToString())
                {
                    case "Họ tên":          searchCol = "kh.TenKH"; break;
                    case "Số điện thoại":   searchCol = "kh.SDT"; break;
                    case "Địa chỉ":         searchCol = "kh.DiaChiKH"; break;
                    default:                searchCol = "CAST(kh.MaKH AS VARCHAR)"; break;
                }

                string query = $@"
                    SELECT
                        kh.MaKH,
                        kh.TenKH,
                        kh.SDT,
                        kh.DiaChiKH,
                        COUNT(dh.MaDH) AS [Số Đơn Hàng],
                        COALESCE(SUM(dh.TongTien), 0) AS [Tổng Tích Lũy]
                    FROM KhachHang kh
                    LEFT JOIN DonHang dh ON kh.MaKH = dh.MaKH
                    WHERE {searchCol} LIKE @search
                    GROUP BY kh.MaKH, kh.TenKH, kh.SDT, kh.DiaChiKH
                    ORDER BY {sortOrder}";

                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + searchVal + "%");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvKH.DataSource = dt;
                }

                SetupGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load data: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void SetupGrid()
        {
            dgvKH.AllowUserToAddRows = false;
            dgvKH.RowHeadersVisible = false;
            dgvKH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvKH.Columns["MaKH"] != null) dgvKH.Columns["MaKH"].HeaderText = "Mã KH";
            if (dgvKH.Columns["TenKH"] != null) dgvKH.Columns["TenKH"].HeaderText = "Họ Tên";
            if (dgvKH.Columns["SDT"] != null) dgvKH.Columns["SDT"].HeaderText = "Số Điện Thoại";
            if (dgvKH.Columns["DiaChiKH"] != null) dgvKH.Columns["DiaChiKH"].HeaderText = "Địa Chi";
            
            if (dgvKH.Columns["Số Đơn Hàng"] != null)
            {
                dgvKH.Columns["Số Đơn Hàng"].HeaderText = "Số Đơn";
                dgvKH.Columns["Số Đơn Hàng"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvKH.Columns["Tổng Tích Lũy"] != null)
            {
                dgvKH.Columns["Tổng Tích Lũy"].HeaderText = "Tích Lũy";
                dgvKH.Columns["Tổng Tích Lũy"].DefaultCellStyle.Format = "N0";
                dgvKH.Columns["Tổng Tích Lũy"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            AddDeleteButtonColumn();
        }

        private void AddDeleteButtonColumn()
        {
            if (dgvKH.Columns["Delete"] == null)
            {
                DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                btnDelete.Name = "Delete";
                btnDelete.HeaderText = "";
                btnDelete.Text = "🗑";
                btnDelete.UseColumnTextForButtonValue = true;
                btnDelete.FlatStyle = FlatStyle.Flat;
                btnDelete.DefaultCellStyle.BackColor = Color.FromArgb(231, 76, 60);
                btnDelete.DefaultCellStyle.ForeColor = Color.White;
                btnDelete.DefaultCellStyle.SelectionBackColor = Color.FromArgb(192, 57, 43);
                btnDelete.DefaultCellStyle.SelectionForeColor = Color.White;
                btnDelete.Width = 40;

                dgvKH.Columns.Add(btnDelete);
            }
        }

        private void UC_KhachHang_Load(object sender, EventArgs e)
        {
            dgvKH.RowHeadersVisible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            UC_ThemKhachHang uc = new UC_ThemKhachHang();
            FormPopup popup = new FormPopup(uc, "Thêm Khách Hàng Mới");
            if (popup.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvKH.Columns[e.ColumnIndex].Name == "Delete") return;

            DataGridViewRow row = dgvKH.Rows[e.RowIndex];

            UC_SuaKhachHang uc = new UC_SuaKhachHang(
                Convert.ToInt32(row.Cells["MaKH"].Value),
                row.Cells["TenKH"].Value.ToString(),
                row.Cells["SDT"].Value.ToString(),
                row.Cells["DiaChiKH"].Value.ToString()
            );

            FormPopup popup = new FormPopup(uc, "Sửa Thông Tin Khách Hàng");
            if (popup.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void dgvKH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvKH.Columns[e.ColumnIndex].Name == "Delete")
            {
                int maKH = Convert.ToInt32(dgvKH.Rows[e.RowIndex].Cells["MaKH"].Value);
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        connect.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM KhachHang WHERE MaKH = @id", connect);
                        cmd.Parameters.AddWithValue("@id", maKH);
                        cmd.ExecuteNonQuery();
                        connect.Close();

                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        connect.Close();
                        MessageBox.Show("Lỗi xóa khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}