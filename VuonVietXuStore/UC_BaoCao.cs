using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
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

            // Premium card styling
            panel1.BackColor = Color.White;
            panel1.Paint += panel1_Paint;
            
            label1.ForeColor = Color.Gray;
            label1.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            label1.Text = "TỔNG DOANH THU";
            
            lblTong.ForeColor = Color.FromArgb(18, 78, 44);
            lblTong.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Left forest green stripe
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(18, 78, 44)))
            {
                e.Graphics.FillRectangle(brush, 0, 0, 6, panel1.Height);
            }
            // Light grey border
            using (Pen pen = new Pen(Color.FromArgb(224, 232, 228), 1))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, panel1.Width - 1, panel1.Height - 1);
            }
        }

        private void cbBaoCao_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            try
            {
                chart1.Series.Clear();
                chart1.Titles.Clear();

                // Style the chart area
                chart1.BackColor = Color.White;
                if (chart1.ChartAreas.Count > 0)
                {
                    var area = chart1.ChartAreas[0];
                    area.BackColor = Color.White;
                    area.AxisX.MajorGrid.LineColor = Color.FromArgb(240, 240, 240);
                    area.AxisY.MajorGrid.LineColor = Color.FromArgb(240, 240, 240);
                    area.AxisX.LabelStyle.Font = new Font("Segoe UI", 9F);
                    area.AxisY.LabelStyle.Font = new Font("Segoe UI", 9F);
                    area.AxisX.LineColor = Color.FromArgb(220, 220, 220);
                    area.AxisY.LineColor = Color.FromArgb(220, 220, 220);
                }

                if (chart1.Legends.Count > 0)
                {
                    chart1.Legends[0].Font = new Font("Segoe UI", 9F);
                }

                Series series = new Series();
                series.Font = new Font("Segoe UI", 9F);

                Title title = new Title();
                title.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
                title.ForeColor = Color.FromArgb(18, 78, 44);

                connect.Open();
                if (cbBaoCao.Text == "Doanh thu")
                {
                    label1.Text = "TỔNG DOANH THU";
                    title.Text = "BIỂU ĐỒ DOANH THU CỬA HÀNG";
                    chart1.Titles.Add(title);

                    series.Name = "DoanhThu";
                    series.ChartType = SeriesChartType.SplineArea;
                    series.Color = Color.FromArgb(120, 46, 204, 113); // Transparent green
                    series.BorderColor = Color.FromArgb(46, 204, 113); // Solid green
                    series.BorderWidth = 3;
                    series.MarkerStyle = MarkerStyle.Circle;
                    series.MarkerSize = 8;
                    series.MarkerColor = Color.FromArgb(46, 204, 113);

                    string query = @"
                        SELECT 
                            NgayDatHang, 
                            SUM(TongTien) AS DoanhThu 
                        FROM DonHang 
                        GROUP BY NgayDatHang
                        ORDER BY NgayDatHang ASC";

                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();

                    decimal tong = 0;
                    while (reader.Read())
                    {
                        DateTime date = Convert.ToDateTime(reader["NgayDatHang"]);
                        series.Points.AddXY(date.ToString("dd/MM/yyyy"), reader["DoanhThu"]);
                        tong += Convert.ToDecimal(reader["DoanhThu"]);
                    }
                    lblTong.Text = tong.ToString("N0") + " VNĐ";
                    reader.Close();
                }
                else if (cbBaoCao.Text == "Tồn kho")
                {
                    label1.Text = "TỔNG SỐ LƯỢNG TỒN";
                    title.Text = "BIỂU ĐỒ SỐ LƯỢNG TỒN KHO SẢN PHẨM";
                    chart1.Titles.Add(title);

                    series.Name = "TonKho";
                    series.ChartType = SeriesChartType.Column;
                    series.Color = Color.FromArgb(230, 126, 34);
                    series.BackSecondaryColor = Color.FromArgb(241, 196, 15);
                    series.BackGradientStyle = GradientStyle.TopBottom;
                    series.BorderColor = Color.FromArgb(211, 84, 0);

                    string query = "SELECT TenSP, SoLuongTon FROM SanPham";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();
                    int tong = 0;
                    while (reader.Read())
                    {
                        series.Points.AddXY(reader["TenSP"], reader["SoLuongTon"]);
                        tong += Convert.ToInt32(reader["SoLuongTon"]);
                    }
                    lblTong.Text = tong.ToString("N0") + " sản phẩm";
                    reader.Close();
                }
                else if (cbBaoCao.Text == "Nhập hàng")
                {
                    label1.Text = "TỔNG CHI PHÍ NHẬP";
                    title.Text = "BIỂU ĐỒ CHI PHÍ NHẬP HÀNG VÀO KHO";
                    chart1.Titles.Add(title);

                    series.Name = "NhapHang";
                    series.ChartType = SeriesChartType.SplineArea;
                    series.Color = Color.FromArgb(120, 52, 152, 219); // Transparent blue
                    series.BorderColor = Color.FromArgb(52, 152, 219); // Solid blue
                    series.BorderWidth = 3;
                    series.MarkerStyle = MarkerStyle.Circle;
                    series.MarkerSize = 8;
                    series.MarkerColor = Color.FromArgb(52, 152, 219);

                    string query = @"
                        SELECT 
                            NgayNhap, 
                            SUM(TongTien) AS TongNhap 
                        FROM PhieuNhap 
                        GROUP BY NgayNhap
                        ORDER BY NgayNhap ASC";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();
                    decimal tong = 0;
                    while (reader.Read())
                    {
                        DateTime date = Convert.ToDateTime(reader["NgayNhap"]);
                        series.Points.AddXY(date.ToString("dd/MM/yyyy"), reader["TongNhap"]);
                        tong += Convert.ToDecimal(reader["TongNhap"]);
                    }
                    lblTong.Text = tong.ToString("N0") + " VNĐ";
                    reader.Close();
                }
                else if (cbBaoCao.Text == "Bán chạy")
                {
                    label1.Text = "TỔNG SẢN PHẨM ĐÃ BÁN";
                    title.Text = "TOP SẢN PHẨM BÁN CHẠY NHẤT";
                    chart1.Titles.Add(title);

                    series.Name = "BanChay";
                    series.ChartType = SeriesChartType.Doughnut;
                    series["PieLabelStyle"] = "Outside";
                    series["DoughnutRadius"] = "60";

                    string query = @"
                        SELECT TOP 5
                            SanPham.TenSP, 
                            SUM(ChiTietDH.SoLuong) AS DaBan 
                        FROM ChiTietDH 
                        INNER JOIN SanPham ON ChiTietDH.MaSP = SanPham.MaSP 
                        GROUP BY SanPham.TenSP 
                        ORDER BY DaBan DESC";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();
                    int tong = 0;

                    Color[] doughnutColors = {
                        Color.FromArgb(46, 204, 113),
                        Color.FromArgb(52, 152, 219),
                        Color.FromArgb(155, 89, 182),
                        Color.FromArgb(230, 126, 34),
                        Color.FromArgb(241, 196, 15)
                    };

                    int colorIdx = 0;
                    while (reader.Read())
                    {
                        int val = Convert.ToInt32(reader["DaBan"]);
                        int ptIdx = series.Points.AddXY(reader["TenSP"], val);
                        series.Points[ptIdx].Color = doughnutColors[colorIdx % doughnutColors.Length];
                        colorIdx++;
                        tong += val;
                    }
                    lblTong.Text = tong.ToString("N0") + " sản phẩm";
                    reader.Close();
                }
                else if (cbBaoCao.Text == "Hết hàng")
                {
                    label1.Text = "SỐ SẢN PHẨM SẮP HẾT";
                    title.Text = "CẢNH BÁO SẢN PHẨM SẮP HẾT HÀNG (< 10)";
                    chart1.Titles.Add(title);

                    series.Name = "HetHang";
                    series.ChartType = SeriesChartType.Bar; // Horizontal bars look awesome
                    series.Color = Color.FromArgb(231, 76, 60);
                    series.BackSecondaryColor = Color.FromArgb(192, 57, 43);
                    series.BackGradientStyle = GradientStyle.LeftRight;
                    series.BorderColor = Color.FromArgb(192, 57, 43);

                    string query = "SELECT TenSP, SoLuongTon FROM SanPham WHERE SoLuongTon < 10";
                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlDataReader reader = cmd.ExecuteReader();
                    int tong = 0;
                    while (reader.Read())
                    {
                        series.Points.AddXY(reader["TenSP"], reader["SoLuongTon"]);
                        tong++;
                    }
                    lblTong.Text = tong.ToString() + " sản phẩm";
                    reader.Close();
                }
                chart1.Series.Add(series);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connect.Close();
            }
        }
    }
}