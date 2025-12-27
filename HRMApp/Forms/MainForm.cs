using System;
using System.Windows.Forms;
using HRMApp.Models;

namespace HRMApp.Forms
{
    public partial class MainForm : Form
    {
        private readonly User currentUser;

        public MainForm(User user)
        {
            InitializeComponent();
            currentUser = user;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Hiển thị tên đăng nhập ở tiêu đề
            this.Text = $"Hệ thống quản lý nhân sự - Xin chào {currentUser.HoTen ?? currentUser.TenDangNhap}";

            // ✅ mở EmployeeForm mặc định khi đăng nhập
            LoadForm(new EmployeeForm());

            // VaiTroID: 1 = Quản lý, 5 = Giám đốc VP, 6 = Giám đốc
            if (currentUser.VaiTroID == 1 || currentUser.VaiTroID == 5 || currentUser.VaiTroID == 6)
            {
                // Admin / Quản lý cấp cao → full quyền
                menuNhanVien.Visible = true;
                menuPhongBan.Visible = true;
                menuVaiTro.Visible = true;
                menuChamCong.Visible = true;
                menuLuong.Visible = true;
                menuBaoCao.Visible = true;
                menuTaiKhoan.Visible = true;
            }
            else
            {
                // Nhân viên, thực tập → chỉ được chấm công & tài khoản cá nhân
                menuNhanVien.Visible = false;
                menuPhongBan.Visible = false;
                menuVaiTro.Visible = false;
                menuChamCong.Visible = true;
                menuLuong.Visible = false;
                menuBaoCao.Visible = false;
                menuTaiKhoan.Visible = true;
            }
        }

        // ✅ Hàm load form con vào panel chính
        private void LoadForm(Form frm)
        {
            pnlContent.Controls.Clear();   // xoá form cũ
            frm.TopLevel = false;          // form con, không phải top-level
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;     // full panel
            pnlContent.Controls.Add(frm);
            frm.Show();
        }

        // ========== MENU SỰ KIỆN ==========
        private void menuThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuNhanVien_Click(object sender, EventArgs e)
        {
            LoadForm(new EmployeeForm());
        }

        private void menuPhongBan_Click(object sender, EventArgs e)
        {
            LoadForm(new DepartmentForm());
        }

        private void menuVaiTro_Click(object sender, EventArgs e)
        {
            LoadForm(new RoleForm());
        }

        private void menuChamCong_Click(object sender, EventArgs e)
        {
            if (currentUser.VaiTroID == 1 || currentUser.VaiTroID == 5 || currentUser.VaiTroID == 6)
            {
                LoadForm(new AttendanceForm());
            }
            else
            {
                LoadForm(new AttendanceForm(currentUser));
            }
        }

        private void menuLuong_Click(object sender, EventArgs e)
        {
            LoadForm(new SalaryForm());
        }

        private void menuBaoCao_Click(object sender, EventArgs e)
        {
            LoadForm(new ReportForm());
        }

        private void menuTaiKhoan_Click(object sender, EventArgs e)
        {
            // Clear nội dung panel cũ
            pnlContent.Controls.Clear();

            // Tạo form con
            UserForm frm = new UserForm();
            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            // Thêm vào panel
            pnlContent.Controls.Add(frm);
            frm.Show();
        }



        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
