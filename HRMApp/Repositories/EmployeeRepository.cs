using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMApp.Repositories
{
    public class EmployeeRepository
    {
        private readonly DBConnection _db = new DBConnection();

        public DataTable GetAll()
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                string sql = @"
SELECT nv.NhanVienID, nv.HoTen, nv.NgaySinh, nv.GioiTinh, nv.SoDienThoai, nv.Email, nv.DiaChi,
       nv.NgayVaoLam, nv.PhongBanID, nv.VaiTroID, nv.Anh,
       pb.TenPhongBan,
       vt.TenVaiTro
FROM nhanvien nv
LEFT JOIN phongban pb ON nv.PhongBanID = pb.PhongBanID
LEFT JOIN vaitro  vt ON nv.VaiTroID   = vt.VaiTroID
ORDER BY nv.NhanVienID DESC;
";
                var da = new SqlDataAdapter(sql, conn);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void AddEmployee(string hoTen, DateTime ngaySinh, int gioiTinh,
            string sdt, string email, string diaChi, DateTime ngayVaoLam,
            int phongBanId, int vaiTroId, byte[] anhBytes)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                string sql = @"
INSERT INTO nhanvien
(HoTen, NgaySinh, GioiTinh, SoDienThoai, Email, DiaChi, NgayVaoLam, PhongBanID, VaiTroID, Anh)
VALUES
(@HoTen, @NgaySinh, @GioiTinh, @SoDienThoai, @Email, @DiaChi, @NgayVaoLam, @PhongBanID, @VaiTroID, @Anh);";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@HoTen", hoTen);
                    cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                    cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@SoDienThoai", sdt);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                    cmd.Parameters.AddWithValue("@NgayVaoLam", ngayVaoLam);
                    cmd.Parameters.AddWithValue("@PhongBanID", phongBanId);
                    cmd.Parameters.AddWithValue("@VaiTroID", vaiTroId);

                    // ✅ NULL thì lưu DBNull.Value (tránh byte[] rỗng gây X đỏ)
                    cmd.Parameters.Add("@Anh", SqlDbType.VarBinary, -1).Value =
                        (anhBytes != null && anhBytes.Length > 0) ? (object)anhBytes : DBNull.Value;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateEmployee(int id, string hoTen, DateTime ngaySinh, int gioiTinh,
            string sdt, string email, string diaChi, DateTime ngayVaoLam,
            int phongBanId, int vaiTroId, byte[] anhBytes)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                string sql = @"
UPDATE nhanvien
SET HoTen=@HoTen, NgaySinh=@NgaySinh, GioiTinh=@GioiTinh,
    SoDienThoai=@SoDienThoai, Email=@Email, DiaChi=@DiaChi,
    NgayVaoLam=@NgayVaoLam, PhongBanID=@PhongBanID, VaiTroID=@VaiTroID,
    Anh=@Anh
WHERE NhanVienID=@ID;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
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

                    cmd.Parameters.Add("@Anh", SqlDbType.VarBinary, -1).Value =
                        (anhBytes != null && anhBytes.Length > 0) ? (object)anhBytes : DBNull.Value;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool DeleteEmployee(int id)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM nhanvien WHERE NhanVienID=@ID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public DataTable Search(string keyword, int phongBanId, int vaiTroId)
        {
            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                string sql = @"
SELECT nv.NhanVienID, nv.HoTen, nv.NgaySinh, nv.GioiTinh, nv.SoDienThoai, nv.Email, nv.DiaChi,
       nv.NgayVaoLam, nv.PhongBanID, nv.VaiTroID, nv.Anh,
       pb.TenPhongBan,
       vt.TenVaiTro
FROM nhanvien nv
LEFT JOIN phongban pb ON nv.PhongBanID = pb.PhongBanID
LEFT JOIN vaitro  vt ON nv.VaiTroID   = vt.VaiTroID
WHERE (nv.HoTen LIKE @kw OR nv.SoDienThoai LIKE @kw OR nv.Email LIKE @kw)
  AND (@PhongBanID = 0 OR nv.PhongBanID = @PhongBanID)
  AND (@VaiTroID   = 0 OR nv.VaiTroID   = @VaiTroID)
ORDER BY nv.NhanVienID DESC;";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@kw", "%" + (keyword ?? "") + "%");
                    cmd.Parameters.AddWithValue("@PhongBanID", phongBanId);
                    cmd.Parameters.AddWithValue("@VaiTroID", vaiTroId);

                    var da = new SqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }
    }
}
