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

                QuayVeTrangKH(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            QuayVeTrangKH(false);
        }

        private void QuayVeTrangKH(bool isSaved)
        {
            Form parentForm = this.FindForm();
            if (parentForm != null && parentForm.GetType().Name == "FormPopupContainer")
            {
                parentForm.DialogResult = isSaved ? DialogResult.OK : DialogResult.Cancel;
                parentForm.Close();
                return;
            }

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
