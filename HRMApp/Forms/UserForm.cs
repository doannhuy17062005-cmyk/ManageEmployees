using System;
using System.Windows.Forms;
using HRMApp.Repositories;
using HRMApp.Models;

namespace HRMApp.Forms
{
    public partial class UserForm : Form
    {
        private readonly UserRepository userRepo;
        private int selectedId = -1;

        public UserForm()
        {
            InitializeComponent();
            userRepo = new UserRepository();
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void LoadUsers()
        {
            dgvUsers.DataSource = userRepo.GetAll();
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvUsers.Rows[e.RowIndex];
                selectedId = Convert.ToInt32(row.Cells["TaiKhoanID"].Value);
                txtUsername.Text = row.Cells["TenDangNhap"].Value.ToString();
                txtPassword.Text = row.Cells["MatKhau"].Value.ToString();
                chkNhanSu.Checked = Convert.ToBoolean(row.Cells["LaNhanSu"].Value);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            var u = new User
            {
                NhanVienID = 0, // nếu muốn gắn NV cụ thể thì thêm combobox chọn nhân viên
                TenDangNhap = txtUsername.Text.Trim(),
                MatKhau = txtPassword.Text.Trim(),
                LaNhanSu = chkNhanSu.Checked
            };
            userRepo.Add(u);
            LoadUsers();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedId > 0)
            {
                var u = new User
                {
                    TaiKhoanID = selectedId,
                    TenDangNhap = txtUsername.Text.Trim(),
                    MatKhau = txtPassword.Text.Trim(),
                    LaNhanSu = chkNhanSu.Checked
                };
                userRepo.Update(u);
                LoadUsers();
            }
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
