using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
                    LoadData();
                }
            }
        }

        private void LoadData()
        {
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT MaNCC, TenNCC, SDT, DiaChiNCC, QuocGia FROM NhaCungCap",connect);

                DataTable dt = new DataTable();
                dt.Clear();
                adapter.Fill(dt);

                dgvNCC.DataSource = null;
                dgvNCC.Columns.Clear();

                dgvNCC.AutoGenerateColumns = true;
                dgvNCC.DataSource = dt;

                SetupGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load data: " + ex.Message);
            }
        }

 
        private void SetupGrid()
        {
            dgvNCC.AllowUserToAddRows = false;
            dgvNCC.RowHeadersVisible = false;

            dgvNCC.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn col in dgvNCC.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

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
                btnDelete.FlatStyle = FlatStyle.Standard;

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
            try
            {
                string query = "SELECT MaNCC, TenNCC, SDT, DiaChiNCC, QuocGia FROM NhaCungCap WHERE TenNCC LIKE @ten";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connect);

                adapter.SelectCommand.Parameters.AddWithValue("@ten", "%" + txtSearch.Text + "%");

                DataTable dt = new DataTable();
                dt.Clear();
                adapter.Fill(dt);

                dgvNCC.DataSource = null;
                dgvNCC.Columns.Clear();

                dgvNCC.AutoGenerateColumns = true;
                dgvNCC.DataSource = dt;

                SetupGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi search: " + ex.Message);
            }
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