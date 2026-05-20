namespace VuonVietXuStore
{
    partial class UC_DonHang
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cboSort = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cboSearchBy = new System.Windows.Forms.ComboBox();
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvDonHang = new System.Windows.Forms.DataGridView();
            this.panelDetail = new System.Windows.Forms.Panel();
            this.dgvChiTietDH = new System.Windows.Forms.DataGridView();
            this.panelDetailFooter = new System.Windows.Forms.Panel();
            this.lblDetailCustomer = new System.Windows.Forms.Label();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.cbTrangThai = new System.Windows.Forms.ComboBox();
            this.btnCapNhatTrangThai = new System.Windows.Forms.Button();
            this.lblDetailTotal = new System.Windows.Forms.Label();
            this.panelDetailHeader = new System.Windows.Forms.Panel();
            this.lblDetailTitle = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonHang)).BeginInit();
            this.panelDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietDH)).BeginInit();
            this.panelDetailFooter.SuspendLayout();
            this.panelDetailHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(247)))), ((int)(((byte)(244)))));
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Controls.Add(this.lblSearch);
            this.panelTop.Controls.Add(this.cboSearchBy);
            this.panelTop.Controls.Add(this.txtSearch);
            this.panelTop.Controls.Add(this.btnSearch);
            this.panelTop.Controls.Add(this.cboSort);
            this.panelTop.Controls.Add(this.btnAdd);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(950, 110);
            this.panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(78)))), ((int)(((byte)(44)))));
            this.lblTitle.Location = new System.Drawing.Point(25, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(325, 41);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "ĐƠN HÀNG KHÁCH HÀNG";
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSearch.ForeColor = System.Drawing.Color.Gray;
            this.lblSearch.Location = new System.Drawing.Point(25, 75);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(86, 23);
            this.lblSearch.TabIndex = 1;
            this.lblSearch.Text = "Tìm kiếm:";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtSearch.Location = new System.Drawing.Point(280, 71);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(210, 31);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // cboSearchBy
            // 
            this.cboSearchBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSearchBy.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboSearchBy.Location = new System.Drawing.Point(117, 71);
            this.cboSearchBy.Name = "cboSearchBy";
            this.cboSearchBy.Size = new System.Drawing.Size(158, 31);
            this.cboSearchBy.TabIndex = 10;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(495, 71);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(40, 31);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "🔍";
            // 
            // cboSort
            // 
            this.cboSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSort.Location = new System.Drawing.Point(542, 71);
            this.cboSort.Name = "cboSort";
            this.cboSort.Size = new System.Drawing.Size(180, 31);
            this.cboSort.TabIndex = 4;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(78)))), ((int)(((byte)(44)))));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(740, 71);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(170, 31);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Text = "➕ Thêm Đơn Hàng";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 110);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvDonHang);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(25, 0, 10, 20);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelDetail);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(10, 0, 25, 20);
            this.splitContainer1.Size = new System.Drawing.Size(950, 540);
            this.splitContainer1.SplitterDistance = 510;
            this.splitContainer1.TabIndex = 1;
            // 
            // dgvDonHang
            // 
            this.dgvDonHang.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDonHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDonHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDonHang.Location = new System.Drawing.Point(25, 0);
            this.dgvDonHang.Name = "dgvDonHang";
            this.dgvDonHang.RowHeadersWidth = 51;
            this.dgvDonHang.Size = new System.Drawing.Size(475, 520);
            this.dgvDonHang.TabIndex = 0;
            this.dgvDonHang.SelectionChanged += new System.EventHandler(this.dgvDonHang_SelectionChanged);
            // 
            // panelDetail
            // 
            this.panelDetail.BackColor = System.Drawing.Color.White;
            this.panelDetail.Controls.Add(this.dgvChiTietDH);
            this.panelDetail.Controls.Add(this.panelDetailFooter);
            this.panelDetail.Controls.Add(this.panelDetailHeader);
            this.panelDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetail.Location = new System.Drawing.Point(10, 0);
            this.panelDetail.Name = "panelDetail";
            this.panelDetail.Size = new System.Drawing.Size(401, 520);
            this.panelDetail.TabIndex = 0;
            // 
            // dgvChiTietDH
            // 
            this.dgvChiTietDH.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTietDH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTietDH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTietDH.Location = new System.Drawing.Point(0, 50);
            this.dgvChiTietDH.Name = "dgvChiTietDH";
            this.dgvChiTietDH.RowHeadersWidth = 51;
            this.dgvChiTietDH.Size = new System.Drawing.Size(401, 330);
            this.dgvChiTietDH.TabIndex = 1;
            // 
            // panelDetailFooter
            // 
            this.panelDetailFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.panelDetailFooter.Controls.Add(this.lblDetailCustomer);
            this.panelDetailFooter.Controls.Add(this.lblTrangThai);
            this.panelDetailFooter.Controls.Add(this.cbTrangThai);
            this.panelDetailFooter.Controls.Add(this.btnCapNhatTrangThai);
            this.panelDetailFooter.Controls.Add(this.lblDetailTotal);
            this.panelDetailFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelDetailFooter.Location = new System.Drawing.Point(0, 380);
            this.panelDetailFooter.Name = "panelDetailFooter";
            this.panelDetailFooter.Size = new System.Drawing.Size(401, 140);
            this.panelDetailFooter.TabIndex = 2;
            // 
            // lblDetailCustomer
            // 
            this.lblDetailCustomer.AutoSize = true;
            this.lblDetailCustomer.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDetailCustomer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblDetailCustomer.Location = new System.Drawing.Point(15, 12);
            this.lblDetailCustomer.Name = "lblDetailCustomer";
            this.lblDetailCustomer.Size = new System.Drawing.Size(124, 23);
            this.lblDetailCustomer.TabIndex = 0;
            this.lblDetailCustomer.Text = "Khách hàng: --";
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTrangThai.Location = new System.Drawing.Point(15, 52);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(95, 23);
            this.lblTrangThai.TabIndex = 3;
            this.lblTrangThai.Text = "Trạng thái:";
            // 
            // cbTrangThai
            // 
            this.cbTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTrangThai.Location = new System.Drawing.Point(120, 48);
            this.cbTrangThai.Name = "cbTrangThai";
            this.cbTrangThai.Size = new System.Drawing.Size(145, 24);
            this.cbTrangThai.TabIndex = 4;
            // 
            // btnCapNhatTrangThai
            // 
            this.btnCapNhatTrangThai.Location = new System.Drawing.Point(275, 45);
            this.btnCapNhatTrangThai.Name = "btnCapNhatTrangThai";
            this.btnCapNhatTrangThai.Size = new System.Drawing.Size(110, 30);
            this.btnCapNhatTrangThai.TabIndex = 5;
            this.btnCapNhatTrangThai.Text = "Cập nhật";
            this.btnCapNhatTrangThai.UseVisualStyleBackColor = true;
            this.btnCapNhatTrangThai.Click += new System.EventHandler(this.btnCapNhatTrangThai_Click);
            // 
            // lblDetailTotal
            // 
            this.lblDetailTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDetailTotal.Font = new System.Drawing.Font("Segoe UI", 13.5F, System.Drawing.FontStyle.Bold);
            this.lblDetailTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.lblDetailTotal.Location = new System.Drawing.Point(15, 95);
            this.lblDetailTotal.Name = "lblDetailTotal";
            this.lblDetailTotal.Size = new System.Drawing.Size(371, 35);
            this.lblDetailTotal.TabIndex = 1;
            this.lblDetailTotal.Text = "Tổng thanh toán: 0 đ";
            this.lblDetailTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelDetailHeader
            // 
            this.panelDetailHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.panelDetailHeader.Controls.Add(this.lblDetailTitle);
            this.panelDetailHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDetailHeader.Location = new System.Drawing.Point(0, 0);
            this.panelDetailHeader.Name = "panelDetailHeader";
            this.panelDetailHeader.Size = new System.Drawing.Size(401, 50);
            this.panelDetailHeader.TabIndex = 0;
            // 
            // lblDetailTitle
            // 
            this.lblDetailTitle.AutoSize = true;
            this.lblDetailTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDetailTitle.ForeColor = System.Drawing.Color.White;
            this.lblDetailTitle.Location = new System.Drawing.Point(15, 13);
            this.lblDetailTitle.Name = "lblDetailTitle";
            this.lblDetailTitle.Size = new System.Drawing.Size(201, 25);
            this.lblDetailTitle.TabIndex = 0;
            this.lblDetailTitle.Text = "CHI TIẾT ĐƠN HÀNG";
            // 
            // UC_DonHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panelTop);
            this.Name = "UC_DonHang";
            this.Size = new System.Drawing.Size(950, 650);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonHang)).EndInit();
            this.panelDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietDH)).EndInit();
            this.panelDetailFooter.ResumeLayout(false);
            this.panelDetailFooter.PerformLayout();
            this.panelDetailHeader.ResumeLayout(false);
            this.panelDetailHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cboSort;
        private System.Windows.Forms.ComboBox cboSearchBy;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvDonHang;
        private System.Windows.Forms.Panel panelDetail;
        private System.Windows.Forms.DataGridView dgvChiTietDH;
        private System.Windows.Forms.Panel panelDetailHeader;
        private System.Windows.Forms.Label lblDetailTitle;
        private System.Windows.Forms.Panel panelDetailFooter;
        private System.Windows.Forms.Label lblDetailCustomer;
        private System.Windows.Forms.Label lblDetailTotal;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.ComboBox cbTrangThai;
        private System.Windows.Forms.Button btnCapNhatTrangThai;
        private System.Windows.Forms.Button btnAdd;
    }
}
