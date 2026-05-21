using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace VuonVietXuStore
{
    public partial class UC_BaoCao : UserControl
    {
        SqlConnection connect =new SqlConnection(ConfigurationManager.ConnectionStrings["VuonVietXuStore"].ConnectionString);

        public UC_BaoCao()
        {
            InitializeComponent();
            cbBaoCao.Items.Add("Doanh thu");
            cbBaoCao.Items.Add("Tồn kho");
            cbBaoCao.Items.Add("Nhập hàng");
            cbBaoCao.Items.Add("Bán chạy");
            cbBaoCao.Items.Add("Hết hàng");
            cbBaoCao.DropDownStyle = ComboBoxStyle.DropDownList;
            cbBaoCao.SelectedIndex = 0;
        }
        private void cbBaoCao_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            try
            {
                chart1.Series.Clear();
                chart1.Titles.Clear();

                Series series = new Series();
                series.ChartType = SeriesChartType.Column;

                connect.Open();
                if (cbBaoCao.Text == "Doanh thu")
                {
                    chart1.Titles.Add("Biểu đồ doanh thu");
                    series.Name = "DoanhThu";
                    string query =
                    "SELECT " +
                    "NgayDatHang, " +
                    "SUM(TongTien) AS DoanhThu " +
                    "FROM DonHang " +
                    "GROUP BY NgayDatHang";

                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();

                    decimal tong = 0;
                    while (reader.Read())
                    {
                        series.Points.AddXY(
                            reader["NgayDatHang"],
                            reader["DoanhThu"]);

                        tong += Convert.ToDecimal(reader["DoanhThu"]);
                    }
                    lblTong.Text = tong.ToString("N0") + " VNĐ";
                    reader.Close();
                }
                else if (cbBaoCao.Text == "Tồn kho")
                {
                    chart1.Titles.Add("Biểu đồ tồn kho");
                    series.Name ="TonKho";
                    string query =
                    "SELECT " +
                    "TenSP, " +
                    "SoLuongTon " +
                    "FROM SanPham";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();
                    int tong = 0;
                    while (reader.Read())
                    {
                        series.Points.AddXY(
                            reader["TenSP"],
                            reader["SoLuongTon"]);
                        tong += Convert.ToInt32( reader["SoLuongTon"]);
                    }
                    lblTong.Text = tong.ToString() +" sản phẩm";
                    reader.Close();
                }
                else if (cbBaoCao.Text == "Nhập hàng")
                {
                    chart1.Titles.Add( "Biểu đồ nhập hàng");
                    series.Name =     "NhapHang";
                    string query =
                    "SELECT " +
                    "NgayNhap, " +
                    "SUM(TongTien) AS TongNhap " +
                    "FROM PhieuNhap " +
                    "GROUP BY NgayNhap";
                    SqlCommand cmd =new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();
                    decimal tong = 0;
                    while (reader.Read())
                    {
                        series.Points.AddXY(
                            reader["NgayNhap"],
                            reader["TongNhap"]);
                        tong += Convert.ToDecimal( reader["TongNhap"]);
                    }
                    lblTong.Text =tong.ToString("N0") +" VNĐ";
                    reader.Close();
                }
                else if (cbBaoCao.Text == "Bán chạy")
                {
                    chart1.Titles.Add( "Biểu đồ bán chạy");
                    series.Name = "BanChay";
                    string query =
                    "SELECT " +
                    "SanPham.TenSP, " +
                    "SUM(ChiTietDH.SoLuong) AS DaBan " +
                    "FROM ChiTietDH " +
                    "INNER JOIN SanPham " +
                    "ON ChiTietDH.MaSP = SanPham.MaSP " +
                    "GROUP BY SanPham.TenSP " +
                    "ORDER BY DaBan DESC";
                    SqlCommand cmd =new SqlCommand(query, connect);
                    SqlDataReader reader =  cmd.ExecuteReader();
                    int tong = 0;
                    while (reader.Read())
                    {
                        series.Points.AddXY(
                            reader["TenSP"],
                            reader["DaBan"]);
                        tong += Convert.ToInt32( reader["DaBan"]);
                    }
                    lblTong.Text = tong.ToString() + " sản phẩm đã bán";
                    reader.Close();
                }
                else if (cbBaoCao.Text == "Hết hàng")
                {
                    chart1.Titles.Add( "Sản phẩm sắp hết");
                    series.Name = "HetHang";
                    string query =
                    "SELECT " +
                    "TenSP, " +
                    "SoLuongTon " +
                    "FROM SanPham " +
                    "WHERE SoLuongTon < 10";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();
                    int tong = 0;
                    while (reader.Read())
                    {
                        series.Points.AddXY(
                            reader["TenSP"],
                            reader["SoLuongTon"]);
                        tong++;
                    }
                    lblTong.Text = tong.ToString() +" sản phẩm sắp hết";
                    reader.Close();
                }
                chart1.Series.Add(series);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }
    }
}