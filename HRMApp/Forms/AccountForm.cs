using System;
using System.Windows.Forms;
using HRMApp.Models;

namespace HRMApp.Forms
{
    public partial class AccountForm : Form
    {
        private User currentUser;

        public AccountForm(User user)
        {
            InitializeComponent();
            currentUser = user;
        }

        private void AccountForm_Load(object sender, EventArgs e)
        {
            txtTenDangNhap.Text = currentUser.TenDangNhap;
            txtHoTen.Text = currentUser.HoTen;

            // Nếu là nhân viên thường thì disable edit
            if (!currentUser.LaNhanSu)
            {
                txtTenDangNhap.ReadOnly = true;
                txtHoTen.ReadOnly = true;
                btnSave.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // TODO: cập nhật vào database
            MessageBox.Show("Cập nhật thông tin thành công!");
        }
    }
}
