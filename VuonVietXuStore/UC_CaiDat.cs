using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VuonVietXuStore
{
    public partial class UC_CaiDat : UserControl
    {
        private string connectionString = @"Data Source=LAPTOP-J142V969\NGUYENTRAM30;Initial Catalog=VuonVietXuStore;Integrated Security=True";

        public UC_CaiDat()
        {
            InitializeComponent();
            ApplyCustomStyles();
            SetupGridColumns();
            LoadRolesToComboBoxColumn();
            LoadUsersAndPermissions();

            // Wire up event handlers for instant checkbox toggling
            dgvUsers.CellValueChanged += dgvUsers_CellValueChanged;
            dgvUsers.CurrentCellDirtyStateChanged += dgvUsers_CurrentCellDirtyStateChanged;
        }

        private void ApplyCustomStyles()
        {
            // Custom styles for DataGridView to match the theme (similar to UC_SanPham)
            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle();
            headerStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            headerStyle.BackColor = Color.FromArgb(18, 78, 44);
            headerStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            headerStyle.ForeColor = Color.White;
            headerStyle.SelectionBackColor = Color.FromArgb(18, 78, 44);
            headerStyle.SelectionForeColor = SystemColors.HighlightText;
            headerStyle.WrapMode = DataGridViewTriState.True;
            dgvUsers.ColumnHeadersDefaultCellStyle = headerStyle;

            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            cellStyle.BackColor = Color.White;
            cellStyle.Font = new Font("Segoe UI", 10F);
            cellStyle.ForeColor = Color.FromArgb(64, 64, 64);
            cellStyle.SelectionBackColor = Color.FromArgb(235, 247, 238);
            cellStyle.SelectionForeColor = Color.FromArgb(18, 78, 44);
            cellStyle.WrapMode = DataGridViewTriState.False;
            dgvUsers.DefaultCellStyle = cellStyle;

            // Flat look for save & add buttons
            btnSave.BackColor = Color.FromArgb(18, 78, 44);
            btnSave.ForeColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;

            btnAddRole.BackColor = Color.FromArgb(18, 78, 44);
            btnAddRole.ForeColor = Color.White;
            btnAddRole.FlatStyle = FlatStyle.Flat;
            btnAddRole.FlatAppearance.BorderSize = 0;

            btnAddUser.BackColor = Color.FromArgb(18, 78, 44);
            btnAddUser.ForeColor = Color.White;
            btnAddUser.FlatStyle = FlatStyle.Flat;
            btnAddUser.FlatAppearance.BorderSize = 0;
        }

        private void SetupGridColumns()
        {
            dgvUsers.AutoGenerateColumns = false;
            dgvUsers.Columns.Clear();

            // Hidden Id
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { Name = "id", DataPropertyName = "id", Visible = false });

            // Text columns (employee fields)
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaNV", HeaderText = "Mã NV", DataPropertyName = "MaNV", Width = 80 });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenNV", HeaderText = "Tên NV", DataPropertyName = "TenNV", Width = 130 });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { Name = "SDT", HeaderText = "SĐT", DataPropertyName = "SDT", Width = 100 });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { Name = "DiaChi", HeaderText = "Địa chỉ", DataPropertyName = "DiaChi", Width = 150 });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { Name = "Email", HeaderText = "Email", DataPropertyName = "Email", Width = 160 });
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn { Name = "username", HeaderText = "Tên Đăng Nhập", DataPropertyName = "username", Width = 120, ReadOnly = true });

            // Role combobox column
            var cboCol = new DataGridViewComboBoxColumn
            {
                Name = "RoleId",
                HeaderText = "Chức vụ",
                DataPropertyName = "RoleId",
                Width = 140,
                DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox
            };
            dgvUsers.Columns.Add(cboCol);

            // Permission checkbox columns
            dgvUsers.Columns.Add(new DataGridViewCheckBoxColumn { Name = "TOAN_QUYEN", HeaderText = "Toàn Quyền", Width = 90 });
            dgvUsers.Columns.Add(new DataGridViewCheckBoxColumn { Name = "SP", HeaderText = "SP", Width = 45 });
            dgvUsers.Columns.Add(new DataGridViewCheckBoxColumn { Name = "NCC", HeaderText = "NCC", Width = 45 });
            dgvUsers.Columns.Add(new DataGridViewCheckBoxColumn { Name = "KHACH", HeaderText = "Khách", Width = 50 });
            dgvUsers.Columns.Add(new DataGridViewCheckBoxColumn { Name = "NHAP", HeaderText = "Nhập", Width = 50 });
            dgvUsers.Columns.Add(new DataGridViewCheckBoxColumn { Name = "BAN", HeaderText = "Bán", Width = 45 });
            dgvUsers.Columns.Add(new DataGridViewCheckBoxColumn { Name = "KHO", HeaderText = "Kho", Width = 45 });
            dgvUsers.Columns.Add(new DataGridViewCheckBoxColumn { Name = "BAO_CAO", HeaderText = "Báo Cáo", Width = 65 });
            dgvUsers.Columns.Add(new DataGridViewCheckBoxColumn { Name = "CAI_DAT", HeaderText = "Cài Đặt", Width = 65 });
        }

        private void LoadRolesToComboBoxColumn()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    string query = "SELECT RoleId, RoleName FROM Roles";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connect))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        var cboCol = (DataGridViewComboBoxColumn)dgvUsers.Columns["RoleId"];
                        cboCol.DataSource = dt;
                        cboCol.DisplayMember = "RoleName";
                        cboCol.ValueMember = "RoleId";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục chức vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadUsersAndPermissions()
        {
            // Temporarily unhook event to prevent firing during initial loading
            dgvUsers.CellValueChanged -= dgvUsers_CellValueChanged;

            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    string query = "SELECT id, MaNV, TenNV, SDT, DiaChi, Email, username, RoleId FROM users";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connect))
                    {
                        adapter.Fill(dt);
                    }
                }

                dgvUsers.DataSource = dt;

                // Load permissions for each user row
                foreach (DataGridViewRow row in dgvUsers.Rows)
                {
                    if (row.IsNewRow) continue;
                    string username = row.Cells["username"].Value?.ToString();
                    object roleVal = row.Cells["RoleId"].Value;
                    int roleId = roleVal != DBNull.Value && roleVal != null ? Convert.ToInt32(roleVal) : 0;

                    List<string> userPerms = GetUserPermissions(username, roleId);

                    row.Cells["SP"].Value = userPerms.Contains("SP");
                    row.Cells["NCC"].Value = userPerms.Contains("NCC");
                    row.Cells["KHACH"].Value = userPerms.Contains("KHACH");
                    row.Cells["NHAP"].Value = userPerms.Contains("NHAP");
                    row.Cells["BAN"].Value = userPerms.Contains("BAN");
                    row.Cells["KHO"].Value = userPerms.Contains("KHO");
                    row.Cells["BAO_CAO"].Value = userPerms.Contains("BAO_CAO");
                    row.Cells["CAI_DAT"].Value = userPerms.Contains("CAI_DAT");

                    CheckAndSetToanQuyen(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dgvUsers.CellValueChanged += dgvUsers_CellValueChanged;
            }
        }

        private List<string> GetUserPermissions(string username, int roleId)
        {
            List<string> list = new List<string>();
            try
            {
                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    connect.Open();
                    // 1. Try UserPermissions table
                    string q1 = "SELECT PermissionCode FROM UserPermissions WHERE Username = @username";
                    using (SqlCommand cmd = new SqlCommand(q1, connect))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(reader.GetString(0));
                            }
                        }
                    }

                    // 2. Fallback to RolePermissions table
                    if (list.Count == 0 && roleId > 0)
                    {
                        string q2 = "SELECT PermissionCode FROM RolePermissions WHERE RoleId = @RoleId";
                        using (SqlCommand cmd = new SqlCommand(q2, connect))
                        {
                            cmd.Parameters.AddWithValue("@RoleId", roleId);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    list.Add(reader.GetString(0));
                                }
                            }
                        }
                    }
                }
            }
            catch { }
            return list;
        }

        private List<string> GetRoleDefaultPermissions(int roleId)
        {
            List<string> list = new List<string>();
            try
            {
                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    string query = "SELECT PermissionCode FROM RolePermissions WHERE RoleId = @RoleId";
                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@RoleId", roleId);
                        connect.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                list.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
            catch { }
            return list;
        }

        private void CheckAndSetToanQuyen(DataGridViewRow row)
        {
            bool allChecked = Convert.ToBoolean(row.Cells["SP"].Value) &&
                              Convert.ToBoolean(row.Cells["NCC"].Value) &&
                              Convert.ToBoolean(row.Cells["KHACH"].Value) &&
                              Convert.ToBoolean(row.Cells["NHAP"].Value) &&
                              Convert.ToBoolean(row.Cells["BAN"].Value) &&
                              Convert.ToBoolean(row.Cells["KHO"].Value) &&
                              Convert.ToBoolean(row.Cells["BAO_CAO"].Value) &&
                              Convert.ToBoolean(row.Cells["CAI_DAT"].Value);

            row.Cells["TOAN_QUYEN"].Value = allChecked;
        }

        private void dgvUsers_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvUsers.IsCurrentCellDirty)
            {
                dgvUsers.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvUsers_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvUsers.Rows[e.RowIndex];
            string colName = dgvUsers.Columns[e.ColumnIndex].Name;

            dgvUsers.CellValueChanged -= dgvUsers_CellValueChanged;

            try
            {
                if (colName == "TOAN_QUYEN")
                {
                    bool isChecked = Convert.ToBoolean(row.Cells["TOAN_QUYEN"].Value);
                    row.Cells["SP"].Value = isChecked;
                    row.Cells["NCC"].Value = isChecked;
                    row.Cells["KHACH"].Value = isChecked;
                    row.Cells["NHAP"].Value = isChecked;
                    row.Cells["BAN"].Value = isChecked;
                    row.Cells["KHO"].Value = isChecked;
                    row.Cells["BAO_CAO"].Value = isChecked;
                    row.Cells["CAI_DAT"].Value = isChecked;
                }
                else if (colName == "RoleId")
                {
                    // If position is changed, apply default permissions of that role as suggestion
                    object roleVal = row.Cells["RoleId"].Value;
                    int roleId = roleVal != DBNull.Value && roleVal != null ? Convert.ToInt32(roleVal) : 0;
                    if (roleId > 0)
                    {
                        List<string> defaultPerms = GetRoleDefaultPermissions(roleId);

                        row.Cells["SP"].Value = defaultPerms.Contains("SP");
                        row.Cells["NCC"].Value = defaultPerms.Contains("NCC");
                        row.Cells["KHACH"].Value = defaultPerms.Contains("KHACH");
                        row.Cells["NHAP"].Value = defaultPerms.Contains("NHAP");
                        row.Cells["BAN"].Value = defaultPerms.Contains("BAN");
                        row.Cells["KHO"].Value = defaultPerms.Contains("KHO");
                        row.Cells["BAO_CAO"].Value = defaultPerms.Contains("BAO_CAO");
                        row.Cells["CAI_DAT"].Value = defaultPerms.Contains("CAI_DAT");

                        CheckAndSetToanQuyen(row);
                    }
                }
                else if (colName == "SP" || colName == "NCC" || colName == "KHACH" || colName == "NHAP" || colName == "BAN" || colName == "KHO" || colName == "BAO_CAO" || colName == "CAI_DAT")
                {
                    CheckAndSetToanQuyen(row);
                }
            }
            catch { }
            finally
            {
                dgvUsers.CellValueChanged += dgvUsers_CellValueChanged;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    connect.Open();

                    foreach (DataGridViewRow row in dgvUsers.Rows)
                    {
                        if (row.IsNewRow) continue;

                        string username = row.Cells["username"].Value?.ToString();
                        string maNV = row.Cells["MaNV"].Value?.ToString();
                        string tennv = row.Cells["TenNV"].Value?.ToString();
                        string sdt = row.Cells["SDT"].Value?.ToString();
                        string diaChi = row.Cells["DiaChi"].Value?.ToString();
                        string email = row.Cells["Email"].Value?.ToString();
                        
                        object roleVal = row.Cells["RoleId"].Value;
                        int roleId = roleVal != DBNull.Value && roleVal != null ? Convert.ToInt32(roleVal) : 0;

                        // 1. Update basic employee info and role in users table
                        string queryUpdateUser = @"
                            UPDATE users 
                            SET MaNV = @MaNV, TenNV = @TenNV, SDT = @SDT, DiaChi = @DiaChi, Email = @Email, RoleId = @RoleId 
                            WHERE username = @username";
                        using (SqlCommand cmd = new SqlCommand(queryUpdateUser, connect))
                        {
                            cmd.Parameters.AddWithValue("@MaNV", (object)maNV ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@TenNV", (object)tennv ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@SDT", (object)sdt ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@DiaChi", (object)diaChi ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@RoleId", roleId);
                            cmd.Parameters.AddWithValue("@username", username);
                            cmd.ExecuteNonQuery();
                        }

                        // 2. Save customized user-specific permissions
                        // First delete old custom permissions
                        string queryDeletePerms = "DELETE FROM UserPermissions WHERE Username = @username";
                        using (SqlCommand cmd = new SqlCommand(queryDeletePerms, connect))
                        {
                            cmd.Parameters.AddWithValue("@username", username);
                            cmd.ExecuteNonQuery();
                        }

                        // Then insert the ones currently selected in the checkboxes
                        string[] permissionCodes = { "SP", "NCC", "KHACH", "NHAP", "BAN", "KHO", "BAO_CAO", "CAI_DAT" };
                        foreach (string code in permissionCodes)
                        {
                            bool hasPerm = Convert.ToBoolean(row.Cells[code].Value);
                            if (hasPerm)
                            {
                                string queryInsertPerm = "INSERT INTO UserPermissions (Username, PermissionCode) VALUES (@username, @code)";
                                using (SqlCommand cmd = new SqlCommand(queryInsertPerm, connect))
                                {
                                    cmd.Parameters.AddWithValue("@username", username);
                                    cmd.Parameters.AddWithValue("@code", code);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }

                MessageBox.Show("Cập nhật thông tin nhân viên và phân quyền thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUsersAndPermissions(); // Reload to refresh grid
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu cấu hình phân quyền: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddRole_Click(object sender, EventArgs e)
        {
            using (var dialog = new FormAddRole())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (SqlConnection connect = new SqlConnection(connectionString))
                        {
                            string query = "INSERT INTO Roles (RoleName) VALUES (@RoleName)";
                            using (SqlCommand cmd = new SqlCommand(query, connect))
                            {
                                cmd.Parameters.AddWithValue("@RoleName", dialog.RoleName);
                                connect.Open();
                                cmd.ExecuteNonQuery();
                            }
                        }
                        MessageBox.Show("Thêm chức vụ mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        // Reload Roles in Grid ComboBox column
                        LoadRolesToComboBoxColumn();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi thêm chức vụ mới: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            DataTable dtRoles = new DataTable();
            try
            {
                using (SqlConnection connect = new SqlConnection(connectionString))
                {
                    string query = "SELECT RoleId, RoleName FROM Roles";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connect))
                    {
                        adapter.Fill(dtRoles);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục chức vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var dialog = new FormAddUser(dtRoles))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (SqlConnection connect = new SqlConnection(connectionString))
                        {
                            connect.Open();
                            
                            // Check if username already exists
                            string checkQuery = "SELECT COUNT(*) FROM users WHERE username = @username";
                            using (SqlCommand checkCmd = new SqlCommand(checkQuery, connect))
                            {
                                checkCmd.Parameters.AddWithValue("@username", dialog.Username);
                                int exists = Convert.ToInt32(checkCmd.ExecuteScalar());
                                if (exists > 0)
                                {
                                    MessageBox.Show("Tên đăng nhập đã tồn tại! Vui lòng chọn tên đăng nhập khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }

                            // Auto-generate employee code (MaNV)
                            string maNV = "NV001";
                            string getMaQuery = "SELECT TOP 1 MaNV FROM users WHERE MaNV LIKE 'NV%' ORDER BY MaNV DESC";
                            using (SqlCommand getMaCmd = new SqlCommand(getMaQuery, connect))
                            {
                                object val = getMaCmd.ExecuteScalar();
                                if (val != null && val != DBNull.Value)
                                {
                                    string lastMa = val.ToString();
                                    if (lastMa.Length > 2 && int.TryParse(lastMa.Substring(2), out int num))
                                    {
                                        maNV = "NV" + (num + 1).ToString("D3");
                                    }
                                }
                            }

                            // Insert into users
                            string query = @"
                                INSERT INTO users (username, password, RoleId, MaNV, TenNV, SDT, DiaChi, Email)
                                VALUES (@username, @password, @RoleId, @MaNV, @TenNV, @SDT, @DiaChi, @Email)";
                            using (SqlCommand cmd = new SqlCommand(query, connect))
                            {
                                cmd.Parameters.AddWithValue("@username", dialog.Username);
                                cmd.Parameters.AddWithValue("@password", dialog.Password);
                                cmd.Parameters.AddWithValue("@RoleId", dialog.RoleId);
                                cmd.Parameters.AddWithValue("@MaNV", maNV);
                                cmd.Parameters.AddWithValue("@TenNV", dialog.TenNV);
                                cmd.Parameters.AddWithValue("@SDT", dialog.SDT);
                                cmd.Parameters.AddWithValue("@DiaChi", dialog.DiaChi);
                                cmd.Parameters.AddWithValue("@Email", dialog.Email);
                                cmd.ExecuteNonQuery();
                            }

                            // Copy default permissions of selected role to UserPermissions
                            string copyPermQuery = @"
                                INSERT INTO UserPermissions (Username, PermissionCode)
                                SELECT @username, PermissionCode 
                                FROM RolePermissions 
                                WHERE RoleId = @RoleId";
                            using (SqlCommand copyCmd = new SqlCommand(copyPermQuery, connect))
                            {
                                copyCmd.Parameters.AddWithValue("@username", dialog.Username);
                                copyCmd.Parameters.AddWithValue("@RoleId", dialog.RoleId);
                                copyCmd.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Thêm nhân viên mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadUsersAndPermissions();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi thêm nhân viên mới: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }

    // Modern green theme custom modal dialog for adding new position
    public class FormAddRole : Form
    {
        public string RoleName { get; private set; }
        private TextBox txtRoleName;
        private Button btnOk;
        private Button btnCancel;

        public FormAddRole()
        {
            this.Text = "Thêm chức vụ mới";
            this.Size = new Size(360, 190);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(242, 247, 244);

            Label lblPrompt = new Label() 
            { 
                Left = 20, 
                Top = 20, 
                Width = 320, 
                Text = "Nhập tên chức vụ mới (Ví dụ: Nhân viên quầy):", 
                Font = new Font("Segoe UI", 10F, FontStyle.Bold), 
                ForeColor = Color.FromArgb(18, 78, 44) 
            };
            
            txtRoleName = new TextBox() 
            { 
                Left = 20, 
                Top = 50, 
                Width = 300, 
                Font = new Font("Segoe UI", 11F) 
            };

            btnOk = new Button() 
            { 
                Text = "Thêm", 
                Left = 120, 
                Top = 95, 
                Width = 95, 
                Height = 35, 
                DialogResult = DialogResult.OK, 
                BackColor = Color.FromArgb(18, 78, 44), 
                ForeColor = Color.White, 
                FlatStyle = FlatStyle.Flat 
            };
            btnOk.FlatAppearance.BorderSize = 0;
            btnOk.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            btnCancel = new Button() 
            { 
                Text = "Hủy", 
                Left = 225, 
                Top = 95, 
                Width = 95, 
                Height = 35, 
                DialogResult = DialogResult.Cancel, 
                BackColor = Color.Gray, 
                ForeColor = Color.White, 
                FlatStyle = FlatStyle.Flat 
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            this.Controls.Add(lblPrompt);
            this.Controls.Add(txtRoleName);
            this.Controls.Add(btnOk);
            this.Controls.Add(btnCancel);
            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;

            this.FormClosing += (s, e) => {
                if (this.DialogResult == DialogResult.OK)
                {
                    if (string.IsNullOrWhiteSpace(txtRoleName.Text))
                    {
                        MessageBox.Show("Vui lòng nhập tên chức vụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        e.Cancel = true;
                    }
                    else
                    {
                        RoleName = txtRoleName.Text.Trim();
                    }
                }
            };
        }
    }

    // Beautiful custom modal dialog for adding a new employee
    public class FormAddUser : Form
    {
        public string TenNV { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public int RoleId { get; private set; }
        public string SDT { get; private set; }
        public string DiaChi { get; private set; }
        public string Email { get; private set; }

        private TextBox txtTenNV;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private ComboBox cboRole;
        private TextBox txtSDT;
        private TextBox txtDiaChi;
        private TextBox txtEmail;

        private Button btnOk;
        private Button btnCancel;

        public FormAddUser(DataTable rolesTable)
        {
            this.Text = "Thêm nhân viên mới";
            this.Size = new Size(460, 460);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(242, 247, 244);

            int startY = 20;
            int gapY = 45;
            int labelX = 20;
            int inputX = 160;
            int inputWidth = 260;

            void AddField(string labelText, Control ctrl, int index)
            {
                Label lbl = new Label()
                {
                    Text = labelText,
                    Left = labelX,
                    Top = startY + index * gapY,
                    Width = 130,
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(18, 78, 44),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Height = 28
                };
                ctrl.Left = inputX;
                ctrl.Top = startY + index * gapY;
                ctrl.Width = inputWidth;
                ctrl.Font = new Font("Segoe UI", 10F);
                if (ctrl is TextBox) ctrl.Height = 28;

                this.Controls.Add(lbl);
                this.Controls.Add(ctrl);
            }

            txtTenNV = new TextBox();
            txtUsername = new TextBox();
            txtPassword = new TextBox() { UseSystemPasswordChar = true };
            
            cboRole = new ComboBox() { DropDownStyle = ComboBoxStyle.DropDownList };
            cboRole.DataSource = rolesTable;
            cboRole.DisplayMember = "RoleName";
            cboRole.ValueMember = "RoleId";

            txtSDT = new TextBox();
            txtDiaChi = new TextBox();
            txtEmail = new TextBox();

            AddField("Tên Nhân Viên *:", txtTenNV, 0);
            AddField("Tên Đăng Nhập *:", txtUsername, 1);
            AddField("Mật Khẩu *:", txtPassword, 2);
            AddField("Chức Vụ:", cboRole, 3);
            AddField("Số Điện Thoại:", txtSDT, 4);
            AddField("Địa Chỉ:", txtDiaChi, 5);
            AddField("Email:", txtEmail, 6);

            btnOk = new Button()
            {
                Text = "Thêm",
                Left = 205,
                Top = 360,
                Width = 100,
                Height = 38,
                DialogResult = DialogResult.OK,
                BackColor = Color.FromArgb(18, 78, 44),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnOk.FlatAppearance.BorderSize = 0;
            btnOk.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            btnCancel = new Button()
            {
                Text = "Hủy",
                Left = 320,
                Top = 360,
                Width = 100,
                Height = 38,
                DialogResult = DialogResult.Cancel,
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            this.Controls.Add(btnOk);
            this.Controls.Add(btnCancel);
            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;

            this.FormClosing += (s, e) =>
            {
                if (this.DialogResult == DialogResult.OK)
                {
                    if (string.IsNullOrWhiteSpace(txtTenNV.Text) ||
                        string.IsNullOrWhiteSpace(txtUsername.Text) ||
                        string.IsNullOrWhiteSpace(txtPassword.Text))
                    {
                        MessageBox.Show("Vui lòng điền đầy đủ các thông tin bắt buộc (*)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        e.Cancel = true;
                        return;
                    }

                    TenNV = txtTenNV.Text.Trim();
                    Username = txtUsername.Text.Trim();
                    Password = txtPassword.Text.Trim();
                    RoleId = Convert.ToInt32(cboRole.SelectedValue);
                    SDT = txtSDT.Text.Trim();
                    DiaChi = txtDiaChi.Text.Trim();
                    Email = txtEmail.Text.Trim();
                }
            };
        }
    }
}
