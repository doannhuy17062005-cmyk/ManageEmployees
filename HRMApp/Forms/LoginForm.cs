using System;
using System.Windows.Forms;
using HRMApp.Repositories;
using HRMApp.Models;

namespace HRMApp.Forms
{
    public partial class LoginForm : Form
    {
        private readonly UserRepository userRepo;

        public LoginForm()
        {
            InitializeComponent();
            userRepo = new UserRepository();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu!",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            try
            {
                User user = userRepo.Login(username, password);
                if (user != null)
                {
                    this.Hide();

                    // Phân quyền
                    if (user.VaiTroID == 1|| user.VaiTroID == 5 || user.VaiTroID == 6) // Quản lý / Admin
                    {
                        MainForm main = new MainForm(user);
                        main.ShowDialog();
                    }
                    else if (user.VaiTroID == 2 || user.VaiTroID == 3 || user.VaiTroID == 7) // Nhân viên hoặc Thực tập
                    {
                        AttendanceForm att = new AttendanceForm(user);
                        att.ShowDialog();
                    }

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!",
                                    "Đăng nhập thất bại",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi đăng nhập: " + ex.Message,
                                "Lỗi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Có thể để trống hoặc preload dữ liệu
        }
    }
}
