using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace VuonVietXuStore
{
    public partial class UC_SuaNCC : UserControl
    {
        SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["VuonVietXuStore"].ConnectionString);

        int maNCC = 0;

        public UC_SuaNCC(
            int id,
            string tenNCC,
            string sdt,
            string diaChi,
            string quocGia)
        {
            InitializeComponent();

            maNCC = id;

            txtNCC.Text = tenNCC;
            txtSDT.Text = sdt;
            txtDiaChi.Text = diaChi;
            txtQuocGia.Text = quocGia;
        }
        private Panel GetMainPanel()
        {
            Control parent = this;

            while (parent != null && !(parent is Panel))
            {
                parent = parent.Parent;
            }

            return parent as Panel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                connect.Open();

                string query =
                    "UPDATE NhaCungCap SET " +
                    "TenNCC = @ten, " +
                    "SDT = @sdt, " +
                    "DiaChiNCC = @dc, " +
                    "QuocGia = @qg " +
                    "WHERE MaNCC = @id";

                SqlCommand cmd = new SqlCommand(query, connect);

                cmd.Parameters.AddWithValue("@ten", txtNCC.Text);
                cmd.Parameters.AddWithValue("@sdt", txtSDT.Text);
                cmd.Parameters.AddWithValue("@dc", txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@qg", txtQuocGia.Text);
                cmd.Parameters.AddWithValue("@id", maNCC);

                cmd.ExecuteNonQuery();

                connect.Close();

                LoadDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            LoadDanhSach();
        }

        private void LoadDanhSach()
        {
            UC_NhaCungCap uc = new UC_NhaCungCap();
            uc.Dock = DockStyle.Fill;

            Panel panel = GetMainPanel();

            if (panel != null)
            {
                panel.Controls.Clear();
                panel.Controls.Add(uc);
                uc.BringToFront();
            }
        }
    }
}