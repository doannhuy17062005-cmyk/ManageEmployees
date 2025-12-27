using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMApp.Models
{
    public class Employee
    {
        public int NhanVienID { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public byte GioiTinh { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgayVaoLam { get; set; }
        public int PhongBanID { get; set; }
        public int VaiTroID { get; set; }
    }
}
