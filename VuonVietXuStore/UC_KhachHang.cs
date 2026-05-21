using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace VuonVietXuStore
{
    public partial class UC_KhachHang : UserControl
    {
        SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["VuonVietXuStore"].ConnectionString);

        public UC_KhachHang()
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
                string query ="SELECT MaKH, TenKH, SDT, DiaChiKH " + "FROM KhachHang";

                SqlDataAdapter adapter =new SqlDataAdapter(query, connect);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                dgvKH.DataSource = dt;

                SetupGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load data: " + ex.Message);
            }
        }
        private void SetupGrid()
        {
            dgvKH.AllowUserToAddRows = false;
            dgvKH.RowHeadersVisible = false;

            dgvKH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn col in dgvKH.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            AddDeleteButtonColumn();
        }

        private void AddDeleteButtonColumn()
        {
            if (dgvKH.Columns["Delete"] == null)
            {
                DataGridViewButtonColumn btnDelete =new DataGridViewButtonColumn();

                btnDelete.Name = "Delete";
                btnDelete.HeaderText = "";
                btnDelete.Text = "🗑";
                btnDelete.UseColumnTextForButtonValue = true;
                btnDelete.FlatStyle = FlatStyle.Standard;

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
            using (FormPopupContainer popup = new FormPopupContainer(uc, "Thêm khách hàng mới"))
            {
                if (popup.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string query =
                    "SELECT MaKH, TenKH, SDT, DiaChiKH " +
                    "FROM KhachHang " +
                    "WHERE TenKH LIKE @ten";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connect);

                adapter.SelectCommand.Parameters.AddWithValue(
                    "@ten",
                    "%" + txtSearch.Text.Trim() + "%");

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                dgvKH.DataSource = dt;

                if (dgvKH.Columns["Delete"] == null)
                {
                    AddDeleteButtonColumn();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi search: " + ex.Message);
            }
        }

        private void dgvKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvKH.Columns[e.ColumnIndex].Name == "Delete")
                return;

            DataGridViewRow row = dgvKH.Rows[e.RowIndex];

            UC_SuaKhachHang uc = new UC_SuaKhachHang(
                Convert.ToInt32(row.Cells["MaKH"].Value),
                row.Cells["TenKH"].Value.ToString(),
                row.Cells["SDT"].Value.ToString(),
                row.Cells["DiaChiKH"].Value.ToString()
            );

            uc.Dock = DockStyle.Fill;

            Panel panel = this.Parent as Panel;

            if (panel != null)
            {
                panel.Controls.Clear();
                panel.Controls.Add(uc);
            }
        }

        private void dgvKH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvKH.Columns[e.ColumnIndex].Name == "Delete")
            {
                int maKH =
                    Convert.ToInt32(dgvKH.Rows[e.RowIndex].Cells["MaKH"].Value);

                DialogResult result = MessageBox.Show("Xóa khách hàng?","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        connect.Open();

                        SqlCommand cmd = new SqlCommand( "DELETE FROM KhachHang WHERE MaKH = @id", connect);

                        cmd.Parameters.AddWithValue("@id", maKH);
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