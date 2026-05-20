namespace VuonVietXuStore
{
    partial class UC_ThemKhachHang
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
            this.lblKH = new System.Windows.Forms.Label();
            this.txtKH = new System.Windows.Forms.TextBox();
            this.lblSDT = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.lblDiem = new System.Windows.Forms.Label();
            this.numDiemTichLuy = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiemTichLuy)).BeginInit();
            this.SuspendLayout();

            // 
            // UC_ThemKhachHang
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(247)))), ((int)(((byte)(244)))));
            this.Controls.Add(this.panelCard);
            this.Name = "UC_ThemKhachHang";
            this.Size = new System.Drawing.Size(950, 650);

            // 
            // panelCard
            // 
            this.panelCard.BackColor = System.Drawing.Color.White;
            this.panelCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCard.Controls.Add(this.lblHeader);
            this.panelCard.Controls.Add(this.lblKH);
            this.panelCard.Controls.Add(this.txtKH);
            this.panelCard.Controls.Add(this.lblSDT);
            this.panelCard.Controls.Add(this.txtSDT);
            this.panelCard.Controls.Add(this.lblDiaChi);
            this.panelCard.Controls.Add(this.txtDiaChi);
            this.panelCard.Controls.Add(this.lblDiem);
            this.panelCard.Controls.Add(this.numDiemTichLuy);
            this.panelCard.Controls.Add(this.btnSave);
            this.panelCard.Controls.Add(this.btnCancel);
            this.panelCard.Location = new System.Drawing.Point(200, 80);
            this.panelCard.Size = new System.Drawing.Size(550, 480);
            this.panelCard.Name = "panelCard";

            // 
            // lblHeader
            // 
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(78)))), ((int)(((byte)(44)))));
            this.lblHeader.Location = new System.Drawing.Point(0, 25);
            this.lblHeader.Size = new System.Drawing.Size(550, 40);
            this.lblHeader.Text = "THÊM KHÁCH HÀNG MỚI";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // lblKH / txtKH
            // 
            this.lblKH.Text = "Họ tên khách hàng:";
            this.lblKH.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblKH.Location = new System.Drawing.Point(50, 95);
            this.lblKH.Size = new System.Drawing.Size(210, 20);
            this.txtKH.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtKH.Location = new System.Drawing.Point(50, 120);
            this.txtKH.Size = new System.Drawing.Size(450, 32);
            this.txtKH.Name = "txtKH";

            // 
            // lblSDT / txtSDT
            // 
            this.lblSDT.Text = "Số điện thoại:";
            this.lblSDT.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSDT.Location = new System.Drawing.Point(50, 165);
            this.lblSDT.Size = new System.Drawing.Size(210, 20);
            this.txtSDT.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSDT.Location = new System.Drawing.Point(50, 190);
            this.txtSDT.Size = new System.Drawing.Size(210, 32);
            this.txtSDT.Name = "txtSDT";

            // 
            // lblDiem / numDiemTichLuy
            // 
            this.lblDiem.Text = "Điểm tích lũy ban đầu:";
            this.lblDiem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDiem.Location = new System.Drawing.Point(290, 165);
            this.lblDiem.Size = new System.Drawing.Size(210, 20);
            this.numDiemTichLuy.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.numDiemTichLuy.Location = new System.Drawing.Point(290, 190);
            this.numDiemTichLuy.Size = new System.Drawing.Size(210, 32);
            this.numDiemTichLuy.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
            this.numDiemTichLuy.Name = "numDiemTichLuy";

            // 
            // lblDiaChi / txtDiaChi
            // 
            this.lblDiaChi.Text = "Địa chỉ:";
            this.lblDiaChi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDiaChi.Location = new System.Drawing.Point(50, 235);
            this.lblDiaChi.Size = new System.Drawing.Size(450, 20);
            this.txtDiaChi.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtDiaChi.Location = new System.Drawing.Point(50, 260);
            this.txtDiaChi.Size = new System.Drawing.Size(450, 32);
            this.txtDiaChi.Name = "txtDiaChi";

            // 
            // btnSave
            // 
            this.btnSave.Text = "✔  Lưu khách hàng";
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(78)))), ((int)(((byte)(44)))));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Location = new System.Drawing.Point(50, 340);
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
            this.btnCancel.Location = new System.Drawing.Point(290, 340);
            this.btnCancel.Size = new System.Drawing.Size(210, 45);
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.Name = "btnCancel";

            this.panelCard.ResumeLayout(false);
            this.panelCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiemTichLuy)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Panel panelCard;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblKH;
        private System.Windows.Forms.TextBox txtKH;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label lblDiem;
        private System.Windows.Forms.NumericUpDown numDiemTichLuy;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
