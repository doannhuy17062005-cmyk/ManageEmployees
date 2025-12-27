namespace HRMApp.Models
{
    public class User
    {
        public int TaiKhoanID { get; set; }
        public int NhanVienID { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public bool LaNhanSu { get; set; }   // true = nhân sự (quản trị), false = nhân viên thường
        public string HoTen { get; set; }
        public string RoleName { get; set; } // nếu có text
        public int VaiTroID { get; set; }     // để phân quyền (1 = Admin, 2 = NV, 3 = Intern)
    }
}
