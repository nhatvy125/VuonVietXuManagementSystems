using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace VuonVietXuStore
{
    public partial class UC_CTPN : UserControl
    {
        SqlConnection connect = new SqlConnection( ConfigurationManager.ConnectionStrings["VuonVietXuStore"].ConnectionString);

        private int maPN;

        public UC_CTPN(int maPhieuNhap)
        {
            InitializeComponent();

            maPN = maPhieuNhap;

            this.Load += UC_CTPN_Load;
        }

        private void UC_CTPN_Load(object sender, EventArgs e)
        {
            LoadThongTinPhieu();
            LoadChiTiet();
        }
        private void LoadThongTinPhieu()
        {
            try
            {
                string query =
                    "SELECT MaPhieuNhap, NgayNhap, TongTien " +
                    "FROM PhieuNhap WHERE MaPhieuNhap = @mapn";

                SqlCommand cmd = new SqlCommand(query, connect);
                cmd.Parameters.AddWithValue("@mapn", maPN);

                connect.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lblMaPhieu.Text = "Mã phiếu: " + reader["MaPhieuNhap"].ToString();

                    lblNgayNhap.Text =
                        "Ngày nhập: " +
                        Convert.ToDateTime(reader["NgayNhap"]).ToString("dd/MM/yyyy");

                    lblTongTien.Text =
                        "Tổng tiền: " +
                        Convert.ToDecimal(reader["TongTien"]).ToString("N0");
                }

                reader.Close();
                connect.Close();
            }
            catch
            {
                if (connect.State == ConnectionState.Open)
                    connect.Close();
            }
        }

        private void LoadChiTiet()
        {
            try
            {
                string query =
                    "SELECT ct.MaSP, sp.TenSP, ct.SoLuong, ct.GiaNhap " +
                    "FROM ChiTietPhieuNhap ct " +
                    "JOIN SanPham sp ON ct.MaSP = sp.MaSP " +
                    "WHERE ct.MaPhieuNhap = @mapn";

                SqlDataAdapter da = new SqlDataAdapter(query, connect);
                da.SelectCommand.Parameters.AddWithValue("@mapn", maPN);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvChiTiet.DataSource = dt;

                dgvChiTiet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvChiTiet.AllowUserToAddRows = false;
                dgvChiTiet.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            UC_NhapHang uc = new UC_NhapHang();
            uc.Dock = DockStyle.Fill;

            Control parent = this;

            while (parent != null && !(parent is Panel))
            {
                parent = parent.Parent;
            }

            Panel panel = parent as Panel;

            if (panel != null)
            {
                panel.Controls.Clear();
                panel.Controls.Add(uc);
                uc.BringToFront();
            }
        }
    }
}