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
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.lblGiaNhap = new System.Windows.Forms.Label();
            this.txtGiaNhap = new System.Windows.Forms.TextBox();
            this.lblGiaBan = new System.Windows.Forms.Label();
            this.txtGiaBan = new System.Windows.Forms.TextBox();
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
            this.Size = new System.Drawing.Size(850, 600);

            // 
            // panelCard
            // 
            this.panelCard.BackColor = System.Drawing.Color.White;
            this.panelCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCard.Controls.Add(this.lblHeader);
            this.panelCard.Controls.Add(this.lblSP);
            this.panelCard.Controls.Add(this.txtSP);
            this.panelCard.Controls.Add(this.lblDanhMuc);
            this.panelCard.Controls.Add(this.cboDanhMuc);
            this.panelCard.Controls.Add(this.lblSoLuong);
            this.panelCard.Controls.Add(this.txtSoLuong);
            this.panelCard.Controls.Add(this.lblGiaNhap);
            this.panelCard.Controls.Add(this.txtGiaNhap);
            this.panelCard.Controls.Add(this.lblGiaBan);
            this.panelCard.Controls.Add(this.txtGiaBan);
            this.panelCard.Controls.Add(this.btnSave);
            this.panelCard.Controls.Add(this.btnCancel);
            this.panelCard.Location = new System.Drawing.Point(150, 40);
            this.panelCard.Size = new System.Drawing.Size(550, 500);
            this.panelCard.Name = "panelCard";

            // 
            // lblHeader
            // 
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(78)))), ((int)(((byte)(44)))));
            this.lblHeader.Location = new System.Drawing.Point(0, 20);
            this.lblHeader.Size = new System.Drawing.Size(550, 40);
            this.lblHeader.Text = "THÊM SẢN PHẨM MỚI";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // lblSP / txtSP
            // 
            this.lblSP.Text = "Tên sản phẩm:";
            this.lblSP.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSP.Location = new System.Drawing.Point(50, 85);
            this.lblSP.Size = new System.Drawing.Size(450, 20);
            this.txtSP.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSP.Location = new System.Drawing.Point(50, 110);
            this.txtSP.Size = new System.Drawing.Size(450, 30);
            this.txtSP.Name = "txtSP";

            // 
            // lblDanhMuc / cboDanhMuc
            // 
            this.lblDanhMuc.Text = "Danh mục:";
            this.lblDanhMuc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDanhMuc.Location = new System.Drawing.Point(50, 155);
            this.lblDanhMuc.Size = new System.Drawing.Size(450, 20);
            this.cboDanhMuc.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboDanhMuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDanhMuc.Location = new System.Drawing.Point(50, 180);
            this.cboDanhMuc.Size = new System.Drawing.Size(450, 30);
            this.cboDanhMuc.Name = "cboDanhMuc";

            // 
            // lblSoLuong / txtSoLuong
            // 
            this.lblSoLuong.Text = "Số lượng tồn đầu kỳ:";
            this.lblSoLuong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSoLuong.Location = new System.Drawing.Point(50, 225);
            this.lblSoLuong.Size = new System.Drawing.Size(450, 20);
            this.txtSoLuong.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSoLuong.Location = new System.Drawing.Point(50, 250);
            this.txtSoLuong.Size = new System.Drawing.Size(450, 30);
            this.txtSoLuong.Name = "txtSoLuong";

            // 
            // lblGiaNhap / txtGiaNhap
            // 
            this.lblGiaNhap.Text = "Giá nhập (VNĐ):";
            this.lblGiaNhap.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblGiaNhap.Location = new System.Drawing.Point(50, 295);
            this.lblGiaNhap.Size = new System.Drawing.Size(210, 20);
            this.txtGiaNhap.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtGiaNhap.Location = new System.Drawing.Point(50, 320);
            this.txtGiaNhap.Size = new System.Drawing.Size(210, 30);
            this.txtGiaNhap.Name = "txtGiaNhap";

            // 
            // lblGiaBan / txtGiaBan
            // 
            this.lblGiaBan.Text = "Giá bán (VNĐ):";
            this.lblGiaBan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblGiaBan.Location = new System.Drawing.Point(290, 295);
            this.lblGiaBan.Size = new System.Drawing.Size(210, 20);
            this.txtGiaBan.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtGiaBan.Location = new System.Drawing.Point(290, 320);
            this.txtGiaBan.Size = new System.Drawing.Size(210, 30);
            this.txtGiaBan.Name = "txtGiaBan";

            // 
            // btnSave
            // 
            this.btnSave.Text = "✔  Lưu sản phẩm";
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(78)))), ((int)(((byte)(44)))));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Location = new System.Drawing.Point(50, 400);
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
            this.btnCancel.Location = new System.Drawing.Point(290, 400);
            this.btnCancel.Size = new System.Drawing.Size(210, 45);
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.Name = "btnCancel";

           // this.panelCard.自由Layout = false; // Đã loại bỏ
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
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.Label lblGiaNhap;
        private System.Windows.Forms.TextBox txtGiaNhap;
        private System.Windows.Forms.Label lblGiaBan;
        private System.Windows.Forms.TextBox txtGiaBan;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}