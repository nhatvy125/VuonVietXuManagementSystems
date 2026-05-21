using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VuonVietXuStore
{
    public partial class UC_ThemKhachHang : UserControl
    {
        SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["VuonVietXuStore"].ConnectionString);
        public UC_ThemKhachHang()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                connect.Open();

                string query =
                "INSERT INTO KhachHang " +
                "(TenKH, SDT, DiaChiKH) " +
                "VALUES " +
                "(@tenkh, @sdt, @diachi)";

                SqlCommand cmd = new SqlCommand(query, connect);

                cmd.Parameters.AddWithValue( "@tenkh", txtKH.Text);

                cmd.Parameters.AddWithValue( "@sdt", txtSDT.Text);

                cmd.Parameters.AddWithValue( "@diachi", txtDiaChi.Text);

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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            UC_KhachHang uc = new UC_KhachHang();

            uc.Dock = DockStyle.Fill;

            Panel panel = this.Parent as Panel;

            panel.Controls.Clear();

            panel.Controls.Add(uc);
        }
    }
}
