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

            // Set size based on the user control
            this.ClientSize = new Size(userControl.Width, userControl.Height);

            containerPanel = new Panel();
            containerPanel.Dock = DockStyle.Fill;
            containerPanel.BackColor = Color.Transparent;
            this.Controls.Add(containerPanel);

            // Hook up ControlAdded event to detect when the user control tries to transition back
            containerPanel.ControlAdded += ContainerPanel_ControlAdded;

            userControl.Dock = DockStyle.Fill;
            containerPanel.Controls.Add(userControl);
        }

        private void ContainerPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            // If the control being added is a list control (which means they clicked Save/Cancel/Quay lai)
            // we close the popup instead of showing the list control inside the popup.
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
