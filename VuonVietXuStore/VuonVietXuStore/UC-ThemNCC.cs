using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace VuonVietXuStore
{
    public partial class UC_ThemNCC : UserControl
    {
        SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["VuonVietXuStore"].ConnectionString);

        public UC_ThemNCC()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                connect.Open();

                string query =
                "INSERT INTO NhaCungCap " +
                "(TenNCC, SDT, DiaChiNCC, QuocGia) " +
                "VALUES " +
                "(@tenncc, @sdt, @diachi, @quocgia)";

                SqlCommand cmd = new SqlCommand(query, connect);

                cmd.Parameters.AddWithValue( "@tenncc", txtNCC.Text);

                cmd.Parameters.AddWithValue( "@sdt", txtSDT.Text);

                cmd.Parameters.AddWithValue( "@diachi", txtDiaChi.Text);

                cmd.Parameters.AddWithValue("@quocgia", txtQuocGia.Text);

                cmd.ExecuteNonQuery();

                connect.Close();

                UC_NhaCungCap uc = new UC_NhaCungCap();

                uc.Dock = DockStyle.Fill;

                Panel panel = this.Parent as Panel;

                panel.Controls.Clear();

                panel.Controls.Add(uc);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            UC_NhaCungCap uc = new UC_NhaCungCap();

            uc.Dock = DockStyle.Fill;

            Panel panel = this.Parent as Panel;

            panel.Controls.Clear();

            panel.Controls.Add(uc);
        }
    }
}