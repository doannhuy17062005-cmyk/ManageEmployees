using System;
using System.Data;
using System.Data.SqlClient;
using HRMApp.Models;

namespace HRMApp.Repositories
{
    public class EmployeeRepository
    {
        private readonly DBConnection _db;

        public EmployeeRepository()
        {
            _db = new DBConnection();
        }

        public DataTable GetAll()
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                string sql = @"
            SELECT nv.*,
                   pb.TenPhongBan,
                   vt.TenVaiTro
            FROM nhanvien nv
            LEFT JOIN phongban pb ON nv.PhongBanID = pb.PhongBanID
            LEFT JOIN vaitro  vt ON nv.VaiTroID   = vt.VaiTroID
        ";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // 📌 Thêm nhân viên
        public void AddEmployee(string hoTen, DateTime ngaySinh, int gioiTinh,
            string sdt, string email, string diaChi, DateTime ngayVaoLam,
            int phongBanId, int vaiTroId)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                string sql = @"INSERT INTO nhanvien
                                (HoTen, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi, NgayVaoLam, PhongBanID, VaiTroID)
                               VALUES (@HoTen, @NgaySinh, @GioiTinh, @SoDienThoai, @Email, @DiaChi, @NgayVaoLam, @PhongBanID, @VaiTroID)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@HoTen", hoTen);
                cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                cmd.Parameters.AddWithValue("@SoDienThoai", sdt);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                cmd.Parameters.AddWithValue("@NgayVaoLam", ngayVaoLam);
                cmd.Parameters.AddWithValue("@PhongBanID", phongBanId);
                cmd.Parameters.AddWithValue("@VaiTroID", vaiTroId);
                cmd.ExecuteNonQuery();
            }
        }

        // 📌 Cập nhật nhân viên
        public void UpdateEmployee(int id, string hoTen, DateTime ngaySinh, int gioiTinh,
            string sdt, string email, string diaChi, DateTime ngayVaoLam,
            int phongBanId, int vaiTroId)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                string sql = @"UPDATE nhanvien
                               SET HoTen=@HoTen, NgaySinh=@NgaySinh, GioiTinh=@GioiTinh, 
                                   SoDienThoai=@SoDienThoai, Email=@Email, DiaChi=@DiaChi, 
                                   NgayVaoLam=@NgayVaoLam, PhongBanID=@PhongBanID, VaiTroID=@VaiTroID
                               WHERE NhanVienID=@ID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@HoTen", hoTen);
                cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                cmd.Parameters.AddWithValue("@SoDienThoai", sdt);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                cmd.Parameters.AddWithValue("@NgayVaoLam", ngayVaoLam);
                cmd.Parameters.AddWithValue("@PhongBanID", phongBanId);
                cmd.Parameters.AddWithValue("@VaiTroID", vaiTroId);
                cmd.ExecuteNonQuery();
            }
        }
       

        // 📌 Xóa nhân viên
        public bool DeleteEmployee(int id)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM nhanvien WHERE NhanVienID=@ID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ID", id);
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }

        // 📌 Tìm kiếm
        public DataTable Search(string keyword, int phongBanId, int vaiTroId)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT nv.*,
                   pb.TenPhongBan,
                   vt.TenVaiTro
            FROM nhanvien nv
            LEFT JOIN phongban pb ON nv.PhongBanID = pb.PhongBanID
            LEFT JOIN vaitro  vt ON nv.VaiTroID   = vt.VaiTroID
            WHERE (nv.HoTen LIKE @kw OR nv.SoDienThoai LIKE @kw OR nv.Email LIKE @kw)
              AND (@PhongBanID = 0 OR nv.PhongBanID = @PhongBanID)
              AND (@VaiTroID = 0 OR nv.VaiTroID = @VaiTroID)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@kw", "%" + (keyword ?? "") + "%");
                cmd.Parameters.AddWithValue("@PhongBanID", phongBanId);
                cmd.Parameters.AddWithValue("@VaiTroID", vaiTroId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

    }
}
