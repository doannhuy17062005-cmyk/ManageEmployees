using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HRMApp.Models;
using HRMApp.Repositories;
using System.Drawing;

namespace HRMApp.Forms
{
    public partial class UserForm : Form
    {
        private readonly UserRepository userRepo;
        private int selectedId = -1;
        private int _selectedNhanVienId = 0;
        private readonly DBConnection db = new DBConnection();
        public UserForm()
        {
            InitializeComponent();
            userRepo = new UserRepository();
        }
        private void FixLayout()
        {
            // form basic
            this.Font = new Font("Segoe UI", 10F);
            this.StartPosition = FormStartPosition.CenterScreen;

            // combobox & password
            cboNhanVien.DropDownStyle = ComboBoxStyle.DropDownList;
            txtPassword.UseSystemPasswordChar = true;

            int leftX = 20;
            int labelW = 120;
            int gapX = 12;
            int inputX = leftX + labelW + gapX;
            int inputW = 260;

            int topY = 18;
            int rowH = 32;
            int gapY = 10;

            // Row 1: Nhân viên
            cboNhanVien.Location = new Point(leftX, topY + 6);
            cboNhanVien.Location = new Point(inputX, topY);
            cboNhanVien.Size = new Size(inputW, 28);

            // Row 2: Username
            int y2 = topY + (rowH + gapY) * 1;
            lblUsername.Location = new Point(leftX, y2 + 6);
            txtUsername.Location = new Point(inputX, y2);
            txtUsername.Size = new Size(inputW, 28);

            // Row 3: Password
            int y3 = topY + (rowH + gapY) * 2;
            lblPassword.Location = new Point(leftX, y3 + 6);
            txtPassword.Location = new Point(inputX, y3);
            txtPassword.Size = new Size(inputW, 28);

            // Row 4: Checkbox
            int y4 = topY + (rowH + gapY) * 3;
            chkNhanSu.Location = new Point(inputX, y4 + 4);

            // Buttons column (right)
            int btnX = inputX + inputW + 40;
            int btnW = 110;
            int btnH = 32;

            btnAdd.Location = new Point(btnX, topY);
            btnEdit.Size = new Size(btnW, btnH);

            btnEdit.Location = new Point(btnX, topY + (btnH + 10) * 1);
            btnEdit.Size = new Size(btnW, btnH);

            btnDelete.Location = new Point(btnX, topY + (btnH + 10) * 2);
            btnDelete.Size = new Size(btnW, btnH);

            btnReset.Location = new Point(btnX, topY + (btnH + 10) * 3);
            btnReset.Size = new Size(btnW, btnH);

            // Bring to front to avoid being hidden
            cboNhanVien.BringToFront();
        }

        private void LoadNhanVien()
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT NhanVienID, HoTen FROM nhanvien ORDER BY HoTen", conn);
                da.Fill(dt);

                // reset binding trước khi bind lại
                cboNhanVien.DataSource = null;

                cboNhanVien.DisplayMember = "HoTen";
                cboNhanVien.ValueMember = "NhanVienID";
                cboNhanVien.DataSource = dt;

                // ✅ an toàn
                cboNhanVien.SelectedIndex = -1; // hoặc auto chọn dòng đầu: xem dưới
                if (cboNhanVien.Items.Count > 0)
                    cboNhanVien.SelectedIndex = 0;
            }
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            LoadUsers();
            LoadNhanVien();
            FixLayout();
        }

        private void LoadUsers()
        {
            dgvUsers.DataSource = userRepo.GetAll();
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvUsers.Rows[e.RowIndex];

            selectedId = Convert.ToInt32(row.Cells["TaiKhoanID"].Value);
            txtUsername.Text = row.Cells["TenDangNhap"].Value?.ToString();
            txtPassword.Text = row.Cells["MatKhau"].Value?.ToString();
            chkNhanSu.Checked = Convert.ToBoolean(row.Cells["LaNhanSu"].Value);

            // ✅ ComboBox nhảy theo nhân viên của tài khoản
            if (row.Cells["NhanVienID"] != null && row.Cells["NhanVienID"].Value != DBNull.Value)
            {
                int nvId = Convert.ToInt32(row.Cells["NhanVienID"].Value);
                _selectedNhanVienId = nvId;

                // phải có dữ liệu trong cboNhanVien trước (nên LoadNhanVien trước LoadUsers)
                cboNhanVien.SelectedValue = nvId;
            }
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cboNhanVien.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên!");
                return;
            }

            int nhanVienId = Convert.ToInt32(cboNhanVien.SelectedValue);

            // ✅ chặn 1 NV tạo 2 tài khoản
            if (userRepo.ExistsByNhanVien(nhanVienId))
            {
                MessageBox.Show("Nhân viên này đã có tài khoản rồi!");
                return;
            }

            var u = new User
            {
                NhanVienID = nhanVienId,
                TenDangNhap = txtUsername.Text.Trim(),
                MatKhau = txtPassword.Text.Trim(),
                LaNhanSu = chkNhanSu.Checked
            };

            userRepo.Add(u);
            LoadUsers();
        }



        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedId <= 0) return;

            if (cboNhanVien.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên!");
                return;
            }

            int nhanVienId = Convert.ToInt32(cboNhanVien.SelectedValue);

            // ✅ chặn đổi sang NV đã có tài khoản khác
            if (userRepo.ExistsByNhanVien(nhanVienId, excludeTaiKhoanId: selectedId))
            {
                MessageBox.Show("Nhân viên này đã có tài khoản khác rồi!");
                return;
            }

            var u = new User
            {
                TaiKhoanID = selectedId,
                NhanVienID = nhanVienId, // ✅ nhớ update cả NhanVienID
                TenDangNhap = txtUsername.Text.Trim(),
                MatKhau = txtPassword.Text.Trim(),
                LaNhanSu = chkNhanSu.Checked
            };

            userRepo.Update(u);
            LoadUsers();
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedId > 0)
            {
                userRepo.Delete(selectedId);
                LoadUsers();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (selectedId > 0)
            {
                userRepo.ResetPassword(selectedId);
                LoadUsers();
                MessageBox.Show("Mật khẩu đã được reset về 123456");
            }
        }
    }
}
