using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace VuonVietXuStore
{
    public partial class UC_SuaKhachHang : UserControl
    {
        SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["VuonVietXuStore"].ConnectionString);

        int maKH = 0;
        public UC_SuaKhachHang(
            int id,
            string tenKH,
            string sdt,
            string diaChi)
        {
            InitializeComponent();

            maKH = id;

            txtKH.Text = tenKH;

            txtSDT.Text = sdt;

            txtDiaChi.Text = diaChi;
        }

        private void btnSave_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                connect.Open();

                string query =
                "UPDATE KhachHang " +
                "SET " +
                "TenKH = @ten, " +
                "SDT = @sdt, " +
                "DiaChiKH = @dc " +
                "WHERE MaKH = @id";

                SqlCommand cmd = new SqlCommand(query, connect);

                cmd.Parameters.AddWithValue( "@ten", txtKH.Text);

                cmd.Parameters.AddWithValue("@sdt", txtSDT.Text);

                cmd.Parameters.AddWithValue( "@dc", txtDiaChi.Text);

                cmd.Parameters.AddWithValue( "@id", maKH);

                cmd.ExecuteNonQuery();

                connect.Close();

                UC_KhachHang uc = new UC_KhachHang();

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

        private void btnCancel_Click( object sender, EventArgs e)
        {
            UC_KhachHang uc = new UC_KhachHang();

            uc.Dock = DockStyle.Fill;

            Panel panel =this.Parent as Panel;

            panel.Controls.Clear();

            panel.Controls.Add(uc);
        }
    }
}