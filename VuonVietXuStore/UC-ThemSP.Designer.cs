namespace VuonVietXuStore
{
    partial class UC_ThemSP
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        private void InitializeComponent()
        {
            this.panelCard = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblSP = new System.Windows.Forms.Label();
            this.txtSP = new System.Windows.Forms.TextBox();
            this.lblDanhMuc = new System.Windows.Forms.Label();
            this.cboDanhMuc = new System.Windows.Forms.ComboBox();
            this.lblNCC = new System.Windows.Forms.Label();
            this.cboNCC = new System.Windows.Forms.ComboBox();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.lblGiaNhap = new System.Windows.Forms.Label();
            this.txtGiaNhap = new System.Windows.Forms.TextBox();
            this.lblGiaBan = new System.Windows.Forms.Label();
            this.txtGiaBan = new System.Windows.Forms.TextBox();
            this.lblXuatXu = new System.Windows.Forms.Label();
            this.txtXuatXu = new System.Windows.Forms.TextBox();
            this.lblHSD = new System.Windows.Forms.Label();
            this.dtpHSD = new System.Windows.Forms.DateTimePicker();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelCard.SuspendLayout();
            this.SuspendLayout();

            // 
            // UC_ThemSP
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(247)))), ((int)(((byte)(244)))));
            this.Controls.Add(this.panelCard);
            this.Name = "UC_ThemSP";
            this.Size = new System.Drawing.Size(950, 700);

            // 
            // panelCard
            // 
            this.panelCard.BackColor = System.Drawing.Color.White;
            this.panelCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCard.Controls.Add(this.lblHeader);
            this.panelCard.Controls.Add(this.lblSP);
            this.panelCard.Controls.Add(this.txtSP);
            this.panelCard.Controls.Add(this.lblSoLuong);
            this.panelCard.Controls.Add(this.txtSoLuong);
            this.panelCard.Controls.Add(this.lblDanhMuc);
            this.panelCard.Controls.Add(this.cboDanhMuc);
            this.panelCard.Controls.Add(this.lblNCC);
            this.panelCard.Controls.Add(this.cboNCC);
            this.panelCard.Controls.Add(this.lblGiaNhap);
            this.panelCard.Controls.Add(this.txtGiaNhap);
            this.panelCard.Controls.Add(this.lblGiaBan);
            this.panelCard.Controls.Add(this.txtGiaBan);
            this.panelCard.Controls.Add(this.lblXuatXu);
            this.panelCard.Controls.Add(this.txtXuatXu);
            this.panelCard.Controls.Add(this.lblHSD);
            this.panelCard.Controls.Add(this.dtpHSD);
            this.panelCard.Controls.Add(this.btnSave);
            this.panelCard.Controls.Add(this.btnCancel);
            this.panelCard.Location = new System.Drawing.Point(150, 40);
            this.panelCard.Size = new System.Drawing.Size(550, 580);
            this.panelCard.Name = "panelCard";

            // 
            // lblHeader
            // 
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(78)))), ((int)(((byte)(44)))));
            this.lblHeader.Location = new System.Drawing.Point(0, 15);
            this.lblHeader.Size = new System.Drawing.Size(550, 40);
            this.lblHeader.Text = "THÊM SẢN PHẨM MỚI";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // lblSP / txtSP
            // 
            this.lblSP.Text = "Tên sản phẩm:";
            this.lblSP.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSP.Location = new System.Drawing.Point(50, 75);
            this.lblSP.Size = new System.Drawing.Size(210, 20);
            this.txtSP.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSP.Location = new System.Drawing.Point(50, 100);
            this.txtSP.Size = new System.Drawing.Size(210, 30);
            this.txtSP.Name = "txtSP";

            // 
            // lblSoLuong / txtSoLuong
            // 
            this.lblSoLuong.Text = "Số lượng tồn:";
            this.lblSoLuong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSoLuong.Location = new System.Drawing.Point(290, 75);
            this.lblSoLuong.Size = new System.Drawing.Size(210, 20);
            this.txtSoLuong.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSoLuong.Location = new System.Drawing.Point(290, 100);
            this.txtSoLuong.Size = new System.Drawing.Size(210, 30);
            this.txtSoLuong.Name = "txtSoLuong";

            // 
            // lblDanhMuc / cboDanhMuc
            // 
            this.lblDanhMuc.Text = "Danh mục:";
            this.lblDanhMuc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDanhMuc.Location = new System.Drawing.Point(50, 145);
            this.lblDanhMuc.Size = new System.Drawing.Size(210, 20);
            this.cboDanhMuc.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboDanhMuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDanhMuc.Location = new System.Drawing.Point(50, 170);
            this.cboDanhMuc.Size = new System.Drawing.Size(210, 30);
            this.cboDanhMuc.Name = "cboDanhMuc";

            // 
            // lblNCC / cboNCC
            // 
            this.lblNCC.Text = "Nhà cung cấp:";
            this.lblNCC.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNCC.Location = new System.Drawing.Point(290, 145);
            this.lblNCC.Size = new System.Drawing.Size(210, 20);
            this.cboNCC.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboNCC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNCC.Location = new System.Drawing.Point(290, 170);
            this.cboNCC.Size = new System.Drawing.Size(210, 30);
            this.cboNCC.Name = "cboNCC";

            // 
            // lblGiaNhap / txtGiaNhap
            // 
            this.lblGiaNhap.Text = "Giá nhập (VNĐ):";
            this.lblGiaNhap.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblGiaNhap.Location = new System.Drawing.Point(50, 215);
            this.lblGiaNhap.Size = new System.Drawing.Size(210, 20);
            this.txtGiaNhap.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtGiaNhap.Location = new System.Drawing.Point(50, 240);
            this.txtGiaNhap.Size = new System.Drawing.Size(210, 30);
            this.txtGiaNhap.Name = "txtGiaNhap";

            // 
            // lblGiaBan / txtGiaBan
            // 
            this.lblGiaBan.Text = "Giá bán (VNĐ):";
            this.lblGiaBan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblGiaBan.Location = new System.Drawing.Point(290, 215);
            this.lblGiaBan.Size = new System.Drawing.Size(210, 20);
            this.txtGiaBan.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtGiaBan.Location = new System.Drawing.Point(290, 240);
            this.txtGiaBan.Size = new System.Drawing.Size(210, 30);
            this.txtGiaBan.Name = "txtGiaBan";

            // 
            // lblXuatXu / txtXuatXu
            // 
            this.lblXuatXu.Text = "Xuất xứ:";
            this.lblXuatXu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblXuatXu.Location = new System.Drawing.Point(50, 285);
            this.lblXuatXu.Size = new System.Drawing.Size(210, 20);
            this.txtXuatXu.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtXuatXu.Location = new System.Drawing.Point(50, 310);
            this.txtXuatXu.Size = new System.Drawing.Size(210, 30);
            this.txtXuatXu.Name = "txtXuatXu";

            // 
            // lblHSD / dtpHSD
            // 
            this.lblHSD.Text = "Hạn sử dụng:";
            this.lblHSD.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblHSD.Location = new System.Drawing.Point(290, 285);
            this.lblHSD.Size = new System.Drawing.Size(210, 20);
            this.dtpHSD.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpHSD.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHSD.Location = new System.Drawing.Point(290, 310);
            this.dtpHSD.Size = new System.Drawing.Size(210, 30);
            this.dtpHSD.Name = "dtpHSD";

            // 
            // btnSave
            // 
            this.btnSave.Text = "✔  Lưu sản phẩm";
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(78)))), ((int)(((byte)(44)))));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Location = new System.Drawing.Point(50, 430);
            this.btnSave.Size = new System.Drawing.Size(210, 45);
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnSave.Name = "btnSave";

            // 
            // btnCancel
            // 
            this.btnCancel.Text = "❌  Hủy bỏ";
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.Location = new System.Drawing.Point(290, 430);
            this.btnCancel.Size = new System.Drawing.Size(210, 45);
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.Name = "btnCancel";

            this.panelCard.ResumeLayout(false);
            this.panelCard.PerformLayout();
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Panel panelCard;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblSP;
        private System.Windows.Forms.TextBox txtSP;
        private System.Windows.Forms.Label lblDanhMuc;
        private System.Windows.Forms.ComboBox cboDanhMuc;
        private System.Windows.Forms.Label lblNCC;
        private System.Windows.Forms.ComboBox cboNCC;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.Label lblGiaNhap;
        private System.Windows.Forms.TextBox txtGiaNhap;
        private System.Windows.Forms.Label lblGiaBan;
        private System.Windows.Forms.TextBox txtGiaBan;
        private System.Windows.Forms.Label lblXuatXu;
        private System.Windows.Forms.TextBox txtXuatXu;
        private System.Windows.Forms.Label lblHSD;
        private System.Windows.Forms.DateTimePicker dtpHSD;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}