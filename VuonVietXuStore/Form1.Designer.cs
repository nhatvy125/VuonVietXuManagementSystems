namespace VuonVietXuStore
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.panelLeft = new System.Windows.Forms.Panel();
            this.lblBrand = new System.Windows.Forms.Label();
            this.lblTagline = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelRight = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.panelPass = new System.Windows.Forms.Panel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.lblFooter = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.panelLeft.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panelPass.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();

            // === panelLeft (Bên trái - Xanh lá đậm + hình nền) ===
            this.panelLeft.BackColor = System.Drawing.Color.FromArgb(13, 74, 46);
            this.panelLeft.Controls.Add(this.picLogo);
            this.panelLeft.Controls.Add(this.lblBrand);
            this.panelLeft.Controls.Add(this.label1);
            this.panelLeft.Controls.Add(this.lblTagline);
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(420, 600);
            this.panelLeft.TabIndex = 0;
            this.panelLeft.Paint += new System.Windows.Forms.PaintEventHandler(this.panelLeft_Paint);

            // === picLogo (Hình ảnh nền trái cây) ===
            this.picLogo.Location = new System.Drawing.Point(60, 140);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(300, 220);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 10;
            this.picLogo.TabStop = false;
            this.picLogo.BackColor = System.Drawing.Color.Transparent;
            try {
                this.picLogo.Image = System.Drawing.Image.FromFile(
                    System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath,
                    "Resources", "login_bg.png"));
            } catch { }

            // === lblBrand ===
            this.lblBrand.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblBrand.ForeColor = System.Drawing.Color.White;
            this.lblBrand.Location = new System.Drawing.Point(20, 30);
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Size = new System.Drawing.Size(380, 65);
            this.lblBrand.TabIndex = 0;
            this.lblBrand.Text = "🌿 Vườn Việt Xứ";
            this.lblBrand.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // === label1 (phụ đề nhỏ bên dưới tên) ===
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Italic);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(180, 255, 210);
            this.label1.Location = new System.Drawing.Point(20, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(380, 30);
            this.label1.TabIndex = 2;
            this.label1.Text = "Cửa hàng nhập khẩu cao cấp";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);

            // === lblTagline (phía dưới ảnh) ===
            this.lblTagline.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTagline.ForeColor = System.Drawing.Color.FromArgb(200, 255, 220);
            this.lblTagline.Location = new System.Drawing.Point(20, 385);
            this.lblTagline.Name = "lblTagline";
            this.lblTagline.Size = new System.Drawing.Size(380, 80);
            this.lblTagline.TabIndex = 1;
            this.lblTagline.Text = "\"Hương vị quốc tế –\r\nChất lượng Việt Nam\"";
            this.lblTagline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTagline.Click += new System.EventHandler(this.lblTagline_Click);

            // === panelRight (Bên phải - Form đăng nhập) ===
            this.panelRight.BackColor = System.Drawing.Color.FromArgb(248, 251, 248);
            this.panelRight.Controls.Add(this.lblTitle);
            this.panelRight.Controls.Add(this.lblSubtitle);
            this.panelRight.Controls.Add(this.lblUser);
            this.panelRight.Controls.Add(this.txtUsername);
            this.panelRight.Controls.Add(this.lblPass);
            this.panelRight.Controls.Add(this.panelPass);
            this.panelRight.Controls.Add(this.btnLogin);
            this.panelRight.Controls.Add(this.lblFooter);
            this.panelRight.Location = new System.Drawing.Point(420, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(560, 600);
            this.panelRight.TabIndex = 1;

            // === lblTitle ===
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(13, 74, 46);
            this.lblTitle.Location = new System.Drawing.Point(60, 85);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(440, 60);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Đăng nhập";

            // === lblSubtitle ===
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(120, 130, 120);
            this.lblSubtitle.Location = new System.Drawing.Point(60, 148);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(440, 30);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Chào mừng trở lại! Vui lòng đăng nhập để tiếp tục.";

            // Dải màu xanh nhỏ dưới tiêu đề - tạo bằng panel
            // === lblUser ===
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblUser.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblUser.Location = new System.Drawing.Point(60, 205);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(440, 26);
            this.lblUser.TabIndex = 2;
            this.lblUser.Text = "👤  Tên tài khoản";

            // === txtUsername ===
            this.txtUsername.BackColor = System.Drawing.Color.White;
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtUsername.Location = new System.Drawing.Point(60, 235);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(440, 34);
            this.txtUsername.TabIndex = 1;
            this.txtUsername.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);

            // === lblPass ===
            this.lblPass.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPass.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblPass.Location = new System.Drawing.Point(60, 295);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(440, 26);
            this.lblPass.TabIndex = 3;
            this.lblPass.Text = "🔒  Mật khẩu";

            // === panelPass ===
            this.panelPass.BackColor = System.Drawing.Color.White;
            this.panelPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPass.Controls.Add(this.txtPassword);
            this.panelPass.Controls.Add(this.pictureBox1);
            this.panelPass.Location = new System.Drawing.Point(60, 325);
            this.panelPass.Name = "panelPass";
            this.panelPass.Size = new System.Drawing.Size(440, 42);
            this.panelPass.TabIndex = 4;

            // === txtPassword ===
            this.txtPassword.BackColor = System.Drawing.Color.White;
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtPassword.Location = new System.Drawing.Point(5, 7);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(395, 26);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.ForeColor = System.Drawing.Color.FromArgb(40, 40, 40);

            // === pictureBox1 (Eye toggle) ===
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::VuonVietXuStore.Properties.Resources.eyeclose;
            this.pictureBox1.Location = new System.Drawing.Point(406, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(28, 28);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);

            // === btnLogin ===
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(13, 74, 46);
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(22, 110, 68);
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(60, 400);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(440, 52);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "🚀  ĐĂNG NHẬP";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

            // === lblFooter ===
            this.lblFooter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFooter.ForeColor = System.Drawing.Color.FromArgb(170, 170, 170);
            this.lblFooter.Location = new System.Drawing.Point(60, 475);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(440, 30);
            this.lblFooter.TabIndex = 9;
            this.lblFooter.Text = "© 2026 Vườn Việt Xứ Store  •  Phiên bản 1.0";
            this.lblFooter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // === Form1 ===
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 600);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.panelRight);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vườn Việt Xứ – Đăng nhập hệ thống";
            this.panelLeft.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            this.panelPass.ResumeLayout(false);
            this.panelPass.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.Label lblTagline;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.Panel panelPass;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFooter;
        private System.Windows.Forms.PictureBox picLogo;
    }
}