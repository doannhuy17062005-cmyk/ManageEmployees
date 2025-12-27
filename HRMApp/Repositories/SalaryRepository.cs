using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMApp.Repositories
{
    public class SalaryRepository
    {
        private readonly DBConnection db = new DBConnection();

        /// <summary>
        /// Báo cáo toàn bộ nhân viên (lọc theo tháng nếu có)
        /// </summary>
        public DataTable GetAllSalaryReport(int? thang, int nam)
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT l.LuongID, nv.HoTen, l.Thang, l.Nam,
                           l.LuongCoBan, l.PhuCap, l.Thuong, l.KhauTru,
                           COUNT(CASE WHEN cc.TrangThai = N'Đi làm' THEN 1 END) AS SoNgayDiLam,
                           COUNT(CASE WHEN cc.TrangThai = N'Nghỉ phép' THEN 1 END) AS SoNgayNghiPhep,
                           COUNT(CASE WHEN cc.TrangThai = N'Nghỉ không phép' THEN 1 END) AS SoNgayNghiKhongPhep,
                           (l.LuongCoBan/22.0) AS LuongMoiNgay,
                           (
                             (COUNT(CASE WHEN cc.TrangThai = N'Đi làm' THEN 1 END) * (l.LuongCoBan/22.0))
                             + (COUNT(CASE WHEN cc.TrangThai = N'Nghỉ phép' THEN 1 END) * (l.LuongCoBan/22.0) * 0.5)
                           ) + l.PhuCap + l.Thuong - l.KhauTru AS ThucLinh
                    FROM nhanvien nv
                    INNER JOIN luong l ON nv.NhanVienID = l.NhanVienID
                    LEFT JOIN chamcong cc 
                           ON nv.NhanVienID = cc.NhanVienID
                           AND MONTH(cc.Ngay) = l.Thang 
                           AND YEAR(cc.Ngay) = l.Nam
                    WHERE l.Nam = @Nam";

                if (thang.HasValue)
                {
                    sql += " AND l.Thang = @Thang";
                }

                sql += @"
                    GROUP BY l.LuongID, nv.HoTen, l.Thang, l.Nam,
                             l.LuongCoBan, l.PhuCap, l.Thuong, l.KhauTru
                    ORDER BY nv.HoTen;";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Nam", nam);
                    if (thang.HasValue)
                        cmd.Parameters.AddWithValue("@Thang", thang.Value);

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        /// <summary>
        /// Báo cáo chi tiết 12 tháng theo nhân viên
        /// </summary>
        public DataTable GetAllSalaryByEmployee(int nhanVienId, int nam)
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT l.LuongID, l.Thang, l.Nam,
                           l.LuongCoBan, l.PhuCap, l.Thuong, l.KhauTru,
                           COUNT(CASE WHEN cc.TrangThai = N'Đi làm' THEN 1 END) AS SoNgayDiLam,
                           COUNT(CASE WHEN cc.TrangThai = N'Nghỉ phép' THEN 1 END) AS SoNgayNghiPhep,
                           COUNT(CASE WHEN cc.TrangThai = N'Nghỉ không phép' THEN 1 END) AS SoNgayNghiKhongPhep,
                           (l.LuongCoBan/22.0) AS LuongMoiNgay,
                           (
                             (COUNT(CASE WHEN cc.TrangThai = N'Đi làm' THEN 1 END) * (l.LuongCoBan/22.0))
                             + (COUNT(CASE WHEN cc.TrangThai = N'Nghỉ phép' THEN 1 END) * (l.LuongCoBan/22.0) * 0.5)
                           ) + l.PhuCap + l.Thuong - l.KhauTru AS ThucLinh
                    FROM luong l
                    INNER JOIN nhanvien nv ON nv.NhanVienID = l.NhanVienID
                    LEFT JOIN chamcong cc 
                           ON nv.NhanVienID = cc.NhanVienID
                           AND MONTH(cc.Ngay) = l.Thang 
                           AND YEAR(cc.Ngay) = l.Nam
                    WHERE nv.NhanVienID = @NhanVienID AND l.Nam = @Nam
                    GROUP BY l.LuongID, l.Thang, l.Nam,
                             l.LuongCoBan, l.PhuCap, l.Thuong, l.KhauTru
                    ORDER BY l.Thang;";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@NhanVienID", nhanVienId);
                    cmd.Parameters.AddWithValue("@Nam", nam);

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        /// <summary>
        /// Báo cáo chi tiết theo nhân viên + tháng cụ thể
        /// </summary>
        public DataTable GetSalaryByEmployeeMonth(int nhanVienId, int thang, int nam)
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();

                string sql = @"
                    SELECT l.LuongID, l.Thang, l.Nam,
                           l.LuongCoBan, l.PhuCap, l.Thuong, l.KhauTru,
                           COUNT(CASE WHEN cc.TrangThai = N'Đi làm' THEN 1 END) AS SoNgayDiLam,
                           COUNT(CASE WHEN cc.TrangThai = N'Nghỉ phép' THEN 1 END) AS SoNgayNghiPhep,
                           COUNT(CASE WHEN cc.TrangThai = N'Nghỉ không phép' THEN 1 END) AS SoNgayNghiKhongPhep,
                           (l.LuongCoBan/22.0) AS LuongMoiNgay,
                           (
                             (COUNT(CASE WHEN cc.TrangThai = N'Đi làm' THEN 1 END) * (l.LuongCoBan/22.0))
                             + (COUNT(CASE WHEN cc.TrangThai = N'Nghỉ phép' THEN 1 END) * (l.LuongCoBan/22.0) * 0.5)
                           ) + l.PhuCap + l.Thuong - l.KhauTru AS ThucLinh
                    FROM luong l
                    INNER JOIN nhanvien nv ON nv.NhanVienID = l.NhanVienID
                    LEFT JOIN chamcong cc 
                           ON nv.NhanVienID = cc.NhanVienID
                           AND MONTH(cc.Ngay) = @Thang 
                           AND YEAR(cc.Ngay) = @Nam
                    WHERE nv.NhanVienID = @NhanVienID AND l.Thang=@Thang AND l.Nam=@Nam
                    GROUP BY l.LuongID, l.Thang, l.Nam,
                             l.LuongCoBan, l.PhuCap, l.Thuong, l.KhauTru;";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@NhanVienID", nhanVienId);
                    cmd.Parameters.AddWithValue("@Thang", thang);
                    cmd.Parameters.AddWithValue("@Nam", nam);

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        /// <summary>
        /// Cập nhật lương (sửa dữ liệu lương cơ bản, phụ cấp, thưởng, khấu trừ)
        /// </summary>
        public void UpdateSalary(int luongId, int thang, int nam,
                                 decimal luongCoBan, decimal phuCap,
                                 decimal thuong, decimal khauTru)
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();
                string sql = @"
                    UPDATE luong
                    SET LuongCoBan = @LuongCoBan,
                        PhuCap = @PhuCap,
                        Thuong = @Thuong,
                        KhauTru = @KhauTru
                    WHERE LuongID = @LuongID AND Thang=@Thang AND Nam=@Nam";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@LuongCoBan", luongCoBan);
                    cmd.Parameters.AddWithValue("@PhuCap", phuCap);
                    cmd.Parameters.AddWithValue("@Thuong", thuong);
                    cmd.Parameters.AddWithValue("@KhauTru", khauTru);
                    cmd.Parameters.AddWithValue("@LuongID", luongId);
                    cmd.Parameters.AddWithValue("@Thang", thang);
                    cmd.Parameters.AddWithValue("@Nam", nam);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Xóa bản ghi lương
        /// </summary>
        public void DeleteSalary(int luongId)
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM luong WHERE LuongID = @LuongID";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@LuongID", luongId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
