using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VuonVietXuStore
{
    public partial class UC_ThemDonHang : UserControl
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["VuonVietXuStore"].ConnectionString;
        private DataTable cartTable;
        private decimal tongTien = 0;

        public UC_ThemDonHang()
        {
            InitializeComponent();
            SetupHoverEffects();
            InitCartTable();
            LoadKhachHang();
            LoadSanPham();

            btnThemVaoGio.Click += btnThemVaoGio_Click;
            btnLuu.Click += btnLuu_Click;
            btnHuy.Click += btnHuy_Click;
        }

        private void SetupHoverEffects()
        {
            btnLuu.MouseEnter += (s, e) => btnLuu.BackColor = Color.FromArgb(25, 110, 62);
            btnLuu.MouseLeave += (s, e) => btnLuu.BackColor = Color.FromArgb(18, 78, 44);

            btnHuy.MouseEnter += (s, e) => {
                btnHuy.BackColor = Color.FromArgb(231, 76, 60);
                btnHuy.ForeColor = Color.White;
            };
            btnHuy.MouseLeave += (s, e) => {
                btnHuy.BackColor = Color.FromArgb(220, 220, 220);
                btnHuy.ForeColor = Color.FromArgb(60, 60, 60);
            };
        }

        private void InitCartTable()
        {
            cartTable = new DataTable();
            cartTable.Columns.Add("MaSP", typeof(int));
            cartTable.Columns.Add("Tên Sản Phẩm", typeof(string));
            cartTable.Columns.Add("Đơn Giá", typeof(decimal));
            cartTable.Columns.Add("Số Lượng", typeof(int));
            cartTable.Columns.Add("Thành Tiền", typeof(decimal));

            dgvChiTiet.DataSource = cartTable;
            dgvChiTiet.Columns["MaSP"].Visible = false; 
            
            dgvChiTiet.Columns["Đơn Giá"].DefaultCellStyle.Format = "N0";
            dgvChiTiet.Columns["Thành Tiền"].DefaultCellStyle.Format = "N0";
        }

        private void LoadKhachHang()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    string query = "SELECT MaKH, TenKH FROM KhachHang";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connect))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        cboKhachHang.DataSource = dt;
                        cboKhachHang.DisplayMember = "TenKH";
                        cboKhachHang.ValueMember = "MaKH";
                        cboKhachHang.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải khách hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSanPham()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    string query = "SELECT MaSP, TenSP, GiaBan, SoLuongTon FROM SanPham WHERE SoLuongTon > 0";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connect))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        cboSanPham.DataSource = dt;
                        cboSanPham.DisplayMember = "TenSP";
                        cboSanPham.ValueMember = "MaSP";
                        cboSanPham.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThemVaoGio_Click(object sender, EventArgs e)
        {
            if (cboSanPham.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maSP = Convert.ToInt32(cboSanPham.SelectedValue);
            string tenSP = cboSanPham.Text;
            int soLuongThem = (int)numSoLuong.Value;

            // Kiểm tra số lượng tồn và giá bán
            decimal giaBan = 0;
            int soLuongTon = 0;

            try
            {
                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    connect.Open();
                    string query = "SELECT GiaBan, SoLuongTon FROM SanPham WHERE MaSP = @masp";
                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@masp", maSP);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                giaBan = Convert.ToDecimal(reader["GiaBan"]);
                                soLuongTon = Convert.ToInt32(reader["SoLuongTon"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy thông tin sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra trong giỏ xem đã có món này chưa
            DataRow existingRow = null;
            foreach (DataRow row in cartTable.Rows)
            {
                if ((int)row["MaSP"] == maSP)
                {
                    existingRow = row;
                    break;
                }
            }

            int soLuongHienTaiTrongGio = existingRow != null ? (int)existingRow["Số Lượng"] : 0;
            if (soLuongHienTaiTrongGio + soLuongThem > soLuongTon)
            {
                MessageBox.Show($"Kho không đủ hàng! Chỉ còn {soLuongTon} sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (existingRow != null)
            {
                existingRow["Số Lượng"] = soLuongHienTaiTrongGio + soLuongThem;
                existingRow["Thành Tiền"] = (soLuongHienTaiTrongGio + soLuongThem) * giaBan;
            }
            else
            {
                DataRow newRow = cartTable.NewRow();
                newRow["MaSP"] = maSP;
                newRow["Tên Sản Phẩm"] = tenSP;
                newRow["Đơn Giá"] = giaBan;
                newRow["Số Lượng"] = soLuongThem;
                newRow["Thành Tiền"] = soLuongThem * giaBan;
                cartTable.Rows.Add(newRow);
            }

            CapNhatTongTien();
            numSoLuong.Value = 1; // Reset số lượng
        }

        private void CapNhatTongTien()
        {
            tongTien = 0;
            foreach (DataRow row in cartTable.Rows)
            {
                tongTien += Convert.ToDecimal(row["Thành Tiền"]);
            }
            lblTongTien.Text = $"Tổng thanh toán: {tongTien:N0} VNĐ";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboKhachHang.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cartTable.Rows.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống! Vui lòng thêm sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    connect.Open();
                    SqlTransaction transaction = connect.BeginTransaction();

                    try
                    {
                        // 1. Insert Đơn Hàng
                        string insertDH = "INSERT INTO DonHang (MaKH, NgayDat, TrangThai) OUTPUT INSERTED.MaDonHang VALUES (@makh, GETDATE(), N'Chờ xử lý')";
                        int maDonHang = 0;
                        using (SqlCommand cmdDH = new SqlCommand(insertDH, connect, transaction))
                        {
                            cmdDH.Parameters.AddWithValue("@makh", cboKhachHang.SelectedValue);
                            maDonHang = (int)cmdDH.ExecuteScalar();
                        }

                        // 2. Insert Chi Tiết Đơn Hàng & Cập nhật số lượng tồn
                        string insertCT = "INSERT INTO ChiTietDH (MaDonHang, MaSP, SoLuong, DonGia) VALUES (@madh, @masp, @soluong, @dongia)";
                        string updateSP = "UPDATE SanPham SET SoLuongTon = SoLuongTon - @soluong WHERE MaSP = @masp";

                        foreach (DataRow row in cartTable.Rows)
                        {
                            using (SqlCommand cmdCT = new SqlCommand(insertCT, connect, transaction))
                            {
                                cmdCT.Parameters.AddWithValue("@madh", maDonHang);
                                cmdCT.Parameters.AddWithValue("@masp", row["MaSP"]);
                                cmdCT.Parameters.AddWithValue("@soluong", row["Số Lượng"]);
                                cmdCT.Parameters.AddWithValue("@dongia", row["Đơn Giá"]);
                                cmdCT.ExecuteNonQuery();
                            }

                            using (SqlCommand cmdUP = new SqlCommand(updateSP, connect, transaction))
                            {
                                cmdUP.Parameters.AddWithValue("@masp", row["MaSP"]);
                                cmdUP.Parameters.AddWithValue("@soluong", row["Số Lượng"]);
                                cmdUP.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                        
                        QuayVeTrangDonHang();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Lỗi khi lưu đơn hàng (Rollback): " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            QuayVeTrangDonHang();
        }

        private void QuayVeTrangDonHang()
        {
            UC_DonHang uc = new UC_DonHang();
            uc.Dock = DockStyle.Fill;
            Panel parentPanel = this.Parent as Panel;
            if (parentPanel != null)
            {
                parentPanel.Controls.Clear();
                parentPanel.Controls.Add(uc);
                uc.BringToFront();
            }
        }
    }
}
