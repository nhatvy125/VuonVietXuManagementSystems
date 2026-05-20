using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VuonVietXuStore
{
    public partial class UC_NhaCungCap : UserControl
    {
        SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["VuonVietXuStore"].ConnectionString);

        public UC_NhaCungCap()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                var connStr = ConfigurationManager.ConnectionStrings["VuonVietXuStore"];

                if (connStr != null)
                {
                    connect = new SqlConnection(connStr.ConnectionString);
                    SetupStyles();
                    LoadData();
                }
            }
        }

        private void SetupStyles()
        {
            this.BackColor = Color.FromArgb(242, 247, 244);

            label1.Text = "QUẢN LÝ NHÀ CUNG CẤP";
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(18, 78, 44);
            label1.Location = new Point(30, 25);

            btnAdd.Text = "➕ Thêm nhà cung cấp";
            btnAdd.BackColor = Color.FromArgb(18, 78, 44);
            btnAdd.ForeColor = Color.White;
            btnAdd.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            txtSearch.Font = new Font("Segoe UI", 10.5F);

            dgvNCC.BackgroundColor = Color.White;
            dgvNCC.BorderStyle = BorderStyle.None;
            dgvNCC.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvNCC.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvNCC.EnableHeadersVisualStyles = false;
            dgvNCC.GridColor = Color.FromArgb(224, 224, 224);
            dgvNCC.RowHeadersVisible = false;
            dgvNCC.RowTemplate.Height = 35;
            dgvNCC.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvNCC.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(18, 78, 44);
            dgvNCC.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvNCC.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvNCC.ColumnHeadersHeight = 40;

            dgvNCC.DefaultCellStyle.BackColor = Color.White;
            dgvNCC.DefaultCellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            dgvNCC.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
            dgvNCC.DefaultCellStyle.SelectionBackColor = Color.FromArgb(235, 247, 238);
            dgvNCC.DefaultCellStyle.SelectionForeColor = Color.FromArgb(18, 78, 44);

            btnSearch.BackColor = Color.FromArgb(18, 78, 44);
            btnSearch.ForeColor = Color.White;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSearch.Cursor = Cursors.Hand;

            cboSearchBy.Items.Clear();
            cboSearchBy.Items.AddRange(new object[] {
                "Mã nhà cung cấp",
                "Tên nhà cung cấp",
                "Số điện thoại",
                "Địa chỉ",
                "Quốc gia"
            });
            cboSearchBy.SelectedIndex = 0;
            cboSearchBy.SelectedIndexChanged += (s, e) => LoadData();

            cboSort.Items.Clear();
            cboSort.Items.AddRange(new object[] {
                "Mặc định",
                "Tên nhà cung cấp A-Z",
                "Tên nhà cung cấp Z-A",
                "Quốc gia A-Z"
            });
            cboSort.SelectedIndex = 0;

            cboSort.SelectedIndexChanged += (s, e) => LoadData();
            btnSearch.Click += (s, e) => LoadData();
            txtSearch.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    LoadData();
                }
            };
        }

        private void LoadData()
        {
            try
            {
                string searchVal = txtSearch.Text.Trim();
                string sortOrder = "MaNCC DESC";

                if (cboSort.SelectedIndex == 1) sortOrder = "TenNCC ASC";
                else if (cboSort.SelectedIndex == 2) sortOrder = "TenNCC DESC";
                else if (cboSort.SelectedIndex == 3) sortOrder = "QuocGia ASC";

                string searchCol;
                switch (cboSearchBy.SelectedItem?.ToString())
                {
                    case "Tên nhà cung cấp": searchCol = "TenNCC"; break;
                    case "Số điện thoại":  searchCol = "SDT"; break;
                    case "Địa chỉ":          searchCol = "DiaChiNCC"; break;
                    case "Quốc gia":          searchCol = "QuocGia"; break;
                    default:                   searchCol = "CAST(MaNCC AS VARCHAR)"; break;
                }

                string query = $@"
                    SELECT MaNCC, TenNCC, SDT, DiaChiNCC, QuocGia 
                    FROM NhaCungCap 
                    WHERE {searchCol} LIKE @search
                    ORDER BY {sortOrder}";

                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    cmd.Parameters.AddWithValue("@search", "%" + searchVal + "%");
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvNCC.DataSource = null;
                    dgvNCC.Columns.Clear();

                    dgvNCC.AutoGenerateColumns = true;
                    dgvNCC.DataSource = dt;

                    SetupGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load data: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupGrid()
        {
            dgvNCC.AllowUserToAddRows = false;
            dgvNCC.RowHeadersVisible = false;
            dgvNCC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvNCC.Columns["MaNCC"] != null) dgvNCC.Columns["MaNCC"].HeaderText = "Mã NCC";
            if (dgvNCC.Columns["TenNCC"] != null) dgvNCC.Columns["TenNCC"].HeaderText = "Nhà Cung Cấp";
            if (dgvNCC.Columns["SDT"] != null) dgvNCC.Columns["SDT"].HeaderText = "Số Điện Thoại";
            if (dgvNCC.Columns["DiaChiNCC"] != null) dgvNCC.Columns["DiaChiNCC"].HeaderText = "Địa Chỉ";
            if (dgvNCC.Columns["QuocGia"] != null) dgvNCC.Columns["QuocGia"].HeaderText = "Quốc Gia";

            AddDeleteButtonColumn();
        }

        private void AddDeleteButtonColumn()
        {
            if (dgvNCC.Columns["Delete"] == null)
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

                dgvNCC.Columns.Add(btnDelete);
            }
        }
        private void UC_NhaCungCap_Load(object sender, EventArgs e)
        {
            dgvNCC.RowHeadersVisible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            UC_ThemNCC uc = new UC_ThemNCC();
            uc.Dock = DockStyle.Fill;

            Panel panel = this.Parent as Panel;

            if (panel != null)
            {
                panel.Controls.Clear();
                panel.Controls.Add(uc);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvNCC.Columns[e.ColumnIndex].Name == "Delete")
                return;

            DataGridViewRow row = dgvNCC.Rows[e.RowIndex];

            UC_SuaNCC uc = new UC_SuaNCC(
                Convert.ToInt32(row.Cells["MaNCC"].Value),
                row.Cells["TenNCC"].Value.ToString(),
                row.Cells["SDT"].Value.ToString(),
                row.Cells["DiaChiNCC"].Value.ToString(),
                row.Cells["QuocGia"].Value.ToString()
            );

            uc.Dock = DockStyle.Fill;

            Panel panel = this.Parent as Panel;

            if (panel != null)
            {
                panel.Controls.Clear();
                panel.Controls.Add(uc);
            }
        }

        private void dgvNCC_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvNCC.Columns[e.ColumnIndex].Name == "Delete")
            {
                int maNCC =  Convert.ToInt32(dgvNCC.Rows[e.RowIndex].Cells["MaNCC"].Value);

                DialogResult result =
                    MessageBox.Show(
                        "Xóa nhà cung cấp?",
                        "Thông báo",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        connect.Open();

                        SqlCommand cmd = new SqlCommand( "DELETE FROM NhaCungCap WHERE MaNCC = @id", connect);

                        cmd.Parameters.AddWithValue("@id", maNCC);
                        cmd.ExecuteNonQuery();

                        connect.Close();

                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        connect.Close();
                        MessageBox.Show("Lỗi xóa: " + ex.Message);
                    }
                }
            }
        }
    }
}