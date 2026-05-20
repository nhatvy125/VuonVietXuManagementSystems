namespace VuonVietXuStore
{
    partial class UC_SuaSP
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.panelCard = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSP = new System.Windows.Forms.TextBox();
            this.lblMaVach = new System.Windows.Forms.Label();
            this.txtMaVach = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSoLuong = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtGiaNhap = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtGiaBan = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelCard.SuspendLayout();
            this.SuspendLayout();

            // 
            // UC_SuaSP
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(247)))), ((int)(((byte)(244)))));
            this.Controls.Add(this.panelCard);
            this.Name = "UC_SuaSP";
            this.Size = new System.Drawing.Size(850, 600);

            // 
            // panelCard
            // 
            this.panelCard.BackColor = System.Drawing.Color.White;
            this.panelCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCard.Controls.Add(this.lblTitle);
            this.panelCard.Controls.Add(this.label1);
            this.panelCard.Controls.Add(this.txtSP);
            this.panelCard.Controls.Add(this.lblMaVach);
            this.panelCard.Controls.Add(this.txtMaVach);
            this.panelCard.Controls.Add(this.label2);
            this.panelCard.Controls.Add(this.txtSoLuong);
            this.panelCard.Controls.Add(this.label3);
            this.panelCard.Controls.Add(this.txtGiaNhap);
            this.panelCard.Controls.Add(this.label4);
            this.panelCard.Controls.Add(this.txtGiaBan);
            this.panelCard.Controls.Add(this.btnSave);
            this.panelCard.Controls.Add(this.btnCancel);
            this.panelCard.Location = new System.Drawing.Point(150, 60);
            this.panelCard.Size = new System.Drawing.Size(550, 400);
            this.panelCard.Name = "panelCard";

            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(78)))), ((int)(((byte)(44)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 20);
            this.lblTitle.Size = new System.Drawing.Size(550, 40);
            this.lblTitle.Text = "CẬP NHẬT SẢN PHẨM";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // label1 / txtSP
            // 
            this.label1.Text = "Tên sản phẩm:";
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(50, 85);
            this.label1.Size = new System.Drawing.Size(210, 20);
            this.txtSP.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSP.Location = new System.Drawing.Point(50, 110);
            this.txtSP.Size = new System.Drawing.Size(210, 30);
            this.txtSP.Name = "txtSP";

            // 
            // lblMaVach / txtMaVach
            // 
            this.lblMaVach.Text = "Mã vạch / Mã SP:";
            this.lblMaVach.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMaVach.Location = new System.Drawing.Point(290, 85);
            this.lblMaVach.Size = new System.Drawing.Size(210, 20);
            this.txtMaVach.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtMaVach.Location = new System.Drawing.Point(290, 110);
            this.txtMaVach.Size = new System.Drawing.Size(210, 30);
            this.txtMaVach.Name = "txtMaVach";

            // 
            // label2 / txtSoLuong
            // 
            this.label2.Text = "Số lượng tồn kho:";
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(50, 155);
            this.label2.Size = new System.Drawing.Size(450, 20);
            this.txtSoLuong.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSoLuong.Location = new System.Drawing.Point(50, 180);
            this.txtSoLuong.Size = new System.Drawing.Size(450, 30);
            this.txtSoLuong.Name = "txtSoLuong";

            // 
            // label3 / txtGiaNhap
            // 
            this.label3.Text = "Giá nhập (VNĐ):";
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(50, 225);
            this.label3.Size = new System.Drawing.Size(210, 20);
            this.txtGiaNhap.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtGiaNhap.Location = new System.Drawing.Point(50, 250);
            this.txtGiaNhap.Size = new System.Drawing.Size(210, 30);
            this.txtGiaNhap.Name = "txtGiaNhap";

            // 
            // label4 / txtGiaBan
            // 
            this.label4.Text = "Giá bán (VNĐ):";
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(290, 225);
            this.label4.Size = new System.Drawing.Size(210, 20);
            this.txtGiaBan.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtGiaBan.Location = new System.Drawing.Point(290, 250);
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
            this.btnSave.Location = new System.Drawing.Point(50, 310);
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
            this.btnCancel.Location = new System.Drawing.Point(290, 310);
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
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSP;
        private System.Windows.Forms.Label lblMaVach;
        private System.Windows.Forms.TextBox txtMaVach;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSoLuong;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtGiaNhap;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtGiaBan;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
