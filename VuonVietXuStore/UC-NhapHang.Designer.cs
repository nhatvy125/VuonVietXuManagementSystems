namespace VuonVietXuStore
{
    partial class UC_NhapHang
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelAction = new System.Windows.Forms.Panel();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearchIcon = new System.Windows.Forms.Label();
            this.cboSearchBy = new System.Windows.Forms.ComboBox();
            this.cboSort = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panelHeader.SuspendLayout();
            this.panelAction.SuspendLayout();
            this.panelSearch.SuspendLayout();
            this.panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();

            // 
            // === panelHeader (Thanh tiêu đề phía trên) ===
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.label1);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(997, 70);

            // 
            // === label1 ===
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(18, 78, 44);
            this.label1.Location = new System.Drawing.Point(30, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(300, 40);
            this.label1.Text = "📥 QUẢN LÝ NHẬP HÀNG";

            // 
            // === panelAction (Thanh chứa ô tìm kiếm và nút bấm) ===
            // 
            this.panelAction.BackColor = System.Drawing.Color.Transparent;
            this.panelAction.Controls.Add(this.panelSearch);
            this.panelAction.Controls.Add(this.cboSearchBy);
            this.panelAction.Controls.Add(this.cboSort);
            this.panelAction.Controls.Add(this.btnAdd);
            this.panelAction.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelAction.Location = new System.Drawing.Point(0, 70);
            this.panelAction.Name = "panelAction";
            this.panelAction.Size = new System.Drawing.Size(997, 60);

            // 
            // === panelSearch (Khung tìm kiếm bo viền) ===
            // 
            this.panelSearch.BackColor = System.Drawing.Color.White;
            this.panelSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSearch.Controls.Add(this.txtSearch);
            this.panelSearch.Controls.Add(this.lblSearchIcon);
            this.panelSearch.Location = new System.Drawing.Point(36, 12);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(400, 38);

            // 
            // === txtSearch (Ô nhập từ khóa tìm kiếm) ===
            // 
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtSearch.ForeColor = System.Drawing.Color.Gray;
            this.txtSearch.Location = new System.Drawing.Point(40, 8);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(350, 20);
            this.txtSearch.Text = "Tìm kiếm số phiếu nhập hoặc nhà cung cấp...";

            // 
            // === lblSearchIcon ===
            // 
            this.lblSearchIcon.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblSearchIcon.Location = new System.Drawing.Point(8, 7);
            this.lblSearchIcon.Name = "lblSearchIcon";
            this.lblSearchIcon.Size = new System.Drawing.Size(26, 23);
            this.lblSearchIcon.Text = "🔍";
            // 
            // === cboSearchBy ===
            // 
            this.cboSearchBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSearchBy.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.cboSearchBy.Location = new System.Drawing.Point(442, 12);
            this.cboSearchBy.Name = "cboSearchBy";
            this.cboSearchBy.Size = new System.Drawing.Size(160, 38);
            this.cboSearchBy.TabIndex = 11;
            // 
            // === cboSort ===
            // 
            this.cboSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSort.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.cboSort.Location = new System.Drawing.Point(610, 12);
            this.cboSort.Name = "cboSort";
            this.cboSort.Size = new System.Drawing.Size(180, 38);
            this.cboSort.TabIndex = 10;

            // 
            // === btnAdd (Nút thêm mới thiết kế phẳng) ===
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(20, 90, 50);
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Size = new System.Drawing.Size(130, 40);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "➕ Thêm mới";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.Name = "btnAdd";
            // Đưa các lệnh cấu hình vị trí thông minh của nút Thêm mới về đúng nhóm
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(831, 11); // Tọa độ tương đối chuẩn với chiều rộng 997
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // 
            // === panelContent (Vùng chứa bảng dữ liệu) ===
            // 
            this.panelContent.BackColor = System.Drawing.Color.Transparent;
            this.panelContent.Controls.Add(this.dataGridView1);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 130);
            this.panelContent.Name = "panelContent";
            // Chỉnh lại phần Padding phía dưới (Bottom từ 30 xuống 20) để ép khung nội dung không đẩy bảng xuống quá sát đất
            this.panelContent.Padding = new System.Windows.Forms.Padding(36, 10, 36, 20);
            this.panelContent.Size = new System.Drawing.Size(997, 477);

            // 
            // === dataGridView1 (Bảng phẳng hiện đại) ===
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;

            // Định dạng thanh tiêu đề bảng (Header)
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(20, 90, 50);
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(20, 90, 50);
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 40;
            this.dataGridView1.EnableHeadersVisualStyles = false;

            // Định dạng dữ liệu các dòng trong bảng
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(235, 247, 238);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(20, 90, 50);
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;

            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(230, 235, 232);
            this.dataGridView1.Location = new System.Drawing.Point(36, 10);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 35;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(925, 447);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);

            // 
            // === UC_NhapHang ===
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(244, 248, 245);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelAction);
            this.Controls.Add(this.panelHeader);
            this.Name = "UC_NhapHang";
            this.Size = new System.Drawing.Size(997, 607);
            this.Load += new System.EventHandler(this.UC_NhapHang_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelAction.ResumeLayout(false);
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.panelContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panelAction;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearchIcon;
        private System.Windows.Forms.ComboBox cboSearchBy;
        private System.Windows.Forms.ComboBox cboSort;
        private System.Windows.Forms.Panel panelContent;
    }
}