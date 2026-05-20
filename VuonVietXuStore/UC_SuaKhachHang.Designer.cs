namespace VuonVietXuStore
{
    partial class UC_SuaKhachHang
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtKH = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.lblDiem = new System.Windows.Forms.Label();
            this.txtDiemTichLuy = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelPointsCard = new System.Windows.Forms.Panel();
            this.lblCardDesc = new System.Windows.Forms.Label();
            this.lblCardPoints = new System.Windows.Forms.Label();
            this.lblCardTitle = new System.Windows.Forms.Label();
            this.lblHistory = new System.Windows.Forms.Label();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.panelPointsCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(30, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(439, 41);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "HỒ SƠ KHÁCH HÀNG & TÍCH ĐIỂM";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Họ và tên:";
            // 
            // txtKH
            // 
            this.txtKH.Location = new System.Drawing.Point(30, 115);
            this.txtKH.Name = "txtKH";
            this.txtKH.Size = new System.Drawing.Size(350, 22);
            this.txtKH.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Số điện thoại:";
            // 
            // txtSDT
            // 
            this.txtSDT.Location = new System.Drawing.Point(30, 185);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(350, 22);
            this.txtSDT.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 230);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Địa chỉ:";
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Location = new System.Drawing.Point(30, 255);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(350, 22);
            this.txtDiaChi.TabIndex = 6;
            // 
            // lblDiem
            // 
            this.lblDiem.AutoSize = true;
            this.lblDiem.Location = new System.Drawing.Point(30, 300);
            this.lblDiem.Name = "lblDiem";
            this.lblDiem.Size = new System.Drawing.Size(94, 17);
            this.lblDiem.TabIndex = 7;
            this.lblDiem.Text = "Điểm tích lũy:";
            // 
            // txtDiemTichLuy
            // 
            this.txtDiemTichLuy.Location = new System.Drawing.Point(30, 325);
            this.txtDiemTichLuy.Name = "txtDiemTichLuy";
            this.txtDiemTichLuy.Size = new System.Drawing.Size(350, 22);
            this.txtDiemTichLuy.TabIndex = 8;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(30, 380);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(160, 38);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "💾 Lưu thay đổi";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(210, 380);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(160, 38);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "↩️ Quay lại";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelPointsCard
            // 
            this.panelPointsCard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPointsCard.Controls.Add(this.lblCardDesc);
            this.panelPointsCard.Controls.Add(this.lblCardPoints);
            this.panelPointsCard.Controls.Add(this.lblCardTitle);
            this.panelPointsCard.Location = new System.Drawing.Point(430, 90);
            this.panelPointsCard.Name = "panelPointsCard";
            this.panelPointsCard.Size = new System.Drawing.Size(540, 115);
            this.panelPointsCard.TabIndex = 11;
            // 
            // lblCardDesc
            // 
            this.lblCardDesc.AutoSize = true;
            this.lblCardDesc.Location = new System.Drawing.Point(15, 85);
            this.lblCardDesc.Name = "lblCardDesc";
            this.lblCardDesc.Size = new System.Drawing.Size(437, 17);
            this.lblCardDesc.TabIndex = 2;
            this.lblCardDesc.Text = "Hệ thống tự động tích lũy: 1 điểm cho mỗi 10.000đ thanh toán đơn hàng.";
            // 
            // lblCardPoints
            // 
            this.lblCardPoints.AutoSize = true;
            this.lblCardPoints.Location = new System.Drawing.Point(15, 38);
            this.lblCardPoints.Name = "lblCardPoints";
            this.lblCardPoints.Size = new System.Drawing.Size(59, 17);
            this.lblCardPoints.TabIndex = 1;
            this.lblCardPoints.Text = "0 ĐIỂM";
            // 
            // lblCardTitle
            // 
            this.lblCardTitle.AutoSize = true;
            this.lblCardTitle.Location = new System.Drawing.Point(15, 12);
            this.lblCardTitle.Name = "lblCardTitle";
            this.lblCardTitle.Size = new System.Drawing.Size(185, 17);
            this.lblCardTitle.TabIndex = 0;
            this.lblCardTitle.Text = "TÍCH ĐIỂM THÀNH VIÊN KH";
            // 
            // lblHistory
            // 
            this.lblHistory.AutoSize = true;
            this.lblHistory.Location = new System.Drawing.Point(430, 225);
            this.lblHistory.Name = "lblHistory";
            this.lblHistory.Size = new System.Drawing.Size(163, 17);
            this.lblHistory.TabIndex = 12;
            this.lblHistory.Text = "📜 LỊCH SỬ GIAO DỊCH";
            // 
            // dgvHistory
            // 
            this.dgvHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistory.Location = new System.Drawing.Point(430, 255);
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.RowHeadersWidth = 51;
            this.dgvHistory.RowTemplate.Height = 32;
            this.dgvHistory.Size = new System.Drawing.Size(540, 360);
            this.dgvHistory.TabIndex = 13;
            // 
            // UC_SuaKhachHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvHistory);
            this.Controls.Add(this.lblHistory);
            this.Controls.Add(this.panelPointsCard);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtDiemTichLuy);
            this.Controls.Add(this.lblDiem);
            this.Controls.Add(this.txtDiaChi);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSDT);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtKH);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTitle);
            this.Name = "UC_SuaKhachHang";
            this.Size = new System.Drawing.Size(1007, 650);
            this.panelPointsCard.ResumeLayout(false);
            this.panelPointsCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKH;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label lblDiem;
        private System.Windows.Forms.TextBox txtDiemTichLuy;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panelPointsCard;
        private System.Windows.Forms.Label lblCardDesc;
        private System.Windows.Forms.Label lblCardPoints;
        private System.Windows.Forms.Label lblCardTitle;
        private System.Windows.Forms.Label lblHistory;
        private System.Windows.Forms.DataGridView dgvHistory;
    }
}
