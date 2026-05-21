using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace VuonVietXuStore
{
    public partial class UC_SuaSP : UserControl
    {
        SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["VuonVietXuStore"].ConnectionString);

        int maSP = 0;
        public UC_SuaSP(
            int id,
            string tenSP,
            int soLuong,
            decimal giaNhap,
            decimal giaBan)
        {
            InitializeComponent();

            maSP = id;

            txtSP.Text = tenSP;

            txtSoLuong.Text = soLuong.ToString();

            txtGiaNhap.Text = giaNhap.ToString();

            txtGiaBan.Text = giaBan.ToString();
        }

        private void btnSave_Click( object sender,EventArgs e)
        {
            try
            {
                connect.Open();

                string query =
                "UPDATE SanPham " +
                "SET " +
                "TenSP = @ten, " +
                "SoLuongTon = @sl, " +
                "GiaNhap = @gn, " +
                "GiaBan = @gb " +
                "WHERE MaSP = @id";

                SqlCommand cmd =new SqlCommand(query, connect);

                cmd.Parameters.AddWithValue("@ten",txtSP.Text);

                cmd.Parameters.AddWithValue("@sl",int.Parse(txtSoLuong.Text));

                cmd.Parameters.AddWithValue("@gn", decimal.Parse(txtGiaNhap.Text));

                cmd.Parameters.AddWithValue("@gb", decimal.Parse(txtGiaBan.Text));

                cmd.Parameters.AddWithValue("@id",maSP);

                cmd.ExecuteNonQuery();

                connect.Close();

                UC_SanPham uc = new UC_SanPham();

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
            UC_SanPham uc = new UC_SanPham();

            uc.Dock = DockStyle.Fill;

            Panel panel = this.Parent as Panel;

            panel.Controls.Clear();

            panel.Controls.Add(uc);
        }
    }
}