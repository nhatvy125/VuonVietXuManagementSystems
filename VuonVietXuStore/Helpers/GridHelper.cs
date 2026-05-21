using System.Drawing;
using System.Windows.Forms;

namespace VuonVietXuStore.Helpers
{
    /// <summary>
    /// GridHelper: Tiêu chuẩn hoá thiết lập DataGridView toàn hệ thống.
    /// </summary>
    public static class GridHelper
    {
        /// <summary>Áp dụng style chuẩn cho mọi DataGridView.</summary>
        public static void ApplyStandardStyle(DataGridView dgv)
        {
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.BorderStyle = BorderStyle.None;
            dgv.BackgroundColor = Color.White;

            // Header style
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.EnableHeadersVisualStyles = false;

            // Row style
            dgv.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9f);
            dgv.RowTemplate.Height = 32;

            // Alternating row color
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 252);
        }

        /// <summary>Thêm cột nút Xóa vào DataGridView.</summary>
        public static void AddDeleteButton(DataGridView dgv, string colName = "BtnDelete")
        {
            if (dgv.Columns[colName] != null) return;

            var btn = new DataGridViewButtonColumn
            {
                Name = colName,
                HeaderText = "",
                Text = "🗑 Xóa",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat,
                Width = 80,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };
            dgv.Columns.Add(btn);
        }

        /// <summary>Thêm cột nút Sửa vào DataGridView.</summary>
        public static void AddEditButton(DataGridView dgv, string colName = "BtnEdit")
        {
            if (dgv.Columns[colName] != null) return;

            var btn = new DataGridViewButtonColumn
            {
                Name = colName,
                HeaderText = "",
                Text = "✏ Sửa",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat,
                Width = 80,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };
            dgv.Columns.Add(btn);
        }
    }
}
