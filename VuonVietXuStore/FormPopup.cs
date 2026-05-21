using System;
using System.Windows.Forms;
using System.Drawing;

namespace VuonVietXuStore
{
    public class FormPopup : Form
    {
        private Panel containerPanel;

        public FormPopup(UserControl userControl, string title = "Thông tin chi tiết")
        {
            this.Text = title;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(242, 247, 244);

            this.ClientSize = new Size(userControl.Width, userControl.Height);

            containerPanel = new Panel();
            containerPanel.Dock = DockStyle.Fill;
            containerPanel.BackColor = Color.Transparent;
            this.Controls.Add(containerPanel);

            containerPanel.ControlAdded += ContainerPanel_ControlAdded;

            userControl.Dock = DockStyle.Fill;
            containerPanel.Controls.Add(userControl);
        }

        private void ContainerPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            string controlName = e.Control.GetType().Name;
            if (controlName == "UC_SanPham" || 
                controlName == "UC_KhachHang" || 
                controlName == "UC_NhaCungCap" || 
                controlName == "UC_DonHang" || 
                controlName == "UC_NhapHang")
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
