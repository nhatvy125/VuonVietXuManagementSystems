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

                QuayVeTrangNCC(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            QuayVeTrangNCC(false);
        }

        private void QuayVeTrangNCC(bool isSaved)
        {
            Form parentForm = this.FindForm();
            if (parentForm != null && parentForm.GetType().Name == "FormPopupContainer")
            {
                parentForm.DialogResult = isSaved ? DialogResult.OK : DialogResult.Cancel;
                parentForm.Close();
                return;
            }

            UC_NhaCungCap uc = new UC_NhaCungCap();
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