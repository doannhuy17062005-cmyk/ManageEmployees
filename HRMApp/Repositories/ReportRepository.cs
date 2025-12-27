using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMApp.Repositories
{
    public class ReportRepository
    {
        private readonly DBConnection db = new DBConnection();

        // --- Báo cáo chi tiết theo phòng ban ---
        public DataTable GetReportByDepartment(int? year, int? month, int? quarter)
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();
                string sql = @"
                    SELECT nv.NhanVienID, nv.HoTen, nv.NgaySinh,
                           CASE nv.GioiTinh WHEN 1 THEN N'Nam' ELSE N'Nữ' END AS GioiTinh,
                           nv.SoDienThoai, nv.Email, nv.DiaChi, nv.NgayVaoLam,
                           pb.TenPhongBan, vt.TenVaiTro,
                           l.LuongCoBan, l.PhuCap, l.Thuong, l.KhauTru,
                           (l.LuongCoBan + l.PhuCap + l.Thuong - l.KhauTru) AS ThucLinh
                    FROM nhanvien nv
                    INNER JOIN phongban pb ON nv.PhongBanID = pb.PhongBanID
                    INNER JOIN vaitro vt ON nv.VaiTroID = vt.VaiTroID
                    INNER JOIN luong l ON nv.NhanVienID = l.NhanVienID
                    WHERE (@year IS NULL OR YEAR(l.NamThang) = @year)
                      AND (@month IS NULL OR MONTH(l.NamThang) = @month)
                      AND (@quarter IS NULL OR DATEPART(QUARTER, l.NamThang) = @quarter)";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@year", (object)year ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@month", (object)month ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@quarter", (object)quarter ?? DBNull.Value);

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        // --- Lấy theo tháng / quý / năm (cho toàn bộ) ---
        public DataTable GetReportByMonth(int year, int month) =>
            GetReportByDepartment(year, month, null);

        public DataTable GetReportByQuarter(int year, int quarter) =>
            GetReportByDepartment(year, null, quarter);

        public DataTable GetReportByYear(int year) =>
            GetReportByDepartment(year, null, null);

        // --- Theo giới tính ---
        public DataTable GetReportByGender(int? year, int? month, int? quarter)
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();
                string sql = @"
                    SELECT CASE nv.GioiTinh WHEN 1 THEN N'Nam' ELSE N'Nữ' END AS GioiTinh,
                           COUNT(*) AS SoLuong
                    FROM nhanvien nv
                    INNER JOIN luong l ON nv.NhanVienID = l.NhanVienID
                    WHERE (@year IS NULL OR YEAR(l.NamThang) = @year)
                      AND (@month IS NULL OR MONTH(l.NamThang) = @month)
                      AND (@quarter IS NULL OR DATEPART(QUARTER, l.NamThang) = @quarter)
                    GROUP BY nv.GioiTinh";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@year", (object)year ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@month", (object)month ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@quarter", (object)quarter ?? DBNull.Value);

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        // --- Theo độ tuổi ---
        public DataTable GetReportByAgeGroup(int? year, int? month, int? quarter)
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();
                string sql = @"
                    SELECT 
                        CASE 
                            WHEN DATEDIFF(YEAR, nv.NgaySinh, GETDATE()) < 30 THEN N'Dưới 30'
                            WHEN DATEDIFF(YEAR, nv.NgaySinh, GETDATE()) BETWEEN 30 AND 50 THEN N'30-50'
                            ELSE N'Trên 50'
                        END AS DoTuoi,
                        COUNT(*) AS SoLuong
                    FROM nhanvien nv
                    INNER JOIN luong l ON nv.NhanVienID = l.NhanVienID
                    WHERE (@year IS NULL OR YEAR(l.NamThang) = @year)
                      AND (@month IS NULL OR MONTH(l.NamThang) = @month)
                      AND (@quarter IS NULL OR DATEPART(QUARTER, l.NamThang) = @quarter)
                    GROUP BY 
                        CASE 
                            WHEN DATEDIFF(YEAR, nv.NgaySinh, GETDATE()) < 30 THEN N'Dưới 30'
                            WHEN DATEDIFF(YEAR, nv.NgaySinh, GETDATE()) BETWEEN 30 AND 50 THEN N'30-50'
                            ELSE N'Trên 50'
                        END";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@year", (object)year ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@month", (object)month ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@quarter", (object)quarter ?? DBNull.Value);

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        // --- Theo số năm làm việc ---
        public DataTable GetReportByWorkingYears(int? year, int? month, int? quarter)
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();
                string sql = @"
                    SELECT 
                        CASE 
                            WHEN DATEDIFF(YEAR, nv.NgayVaoLam, GETDATE()) < 5 THEN N'Dưới 5 năm'
                            WHEN DATEDIFF(YEAR, nv.NgayVaoLam, GETDATE()) BETWEEN 5 AND 10 THEN N'5-10 năm'
                            ELSE N'Trên 10 năm'
                        END AS ThamNien,
                        COUNT(*) AS SoLuong
                    FROM nhanvien nv
                    INNER JOIN luong l ON nv.NhanVienID = l.NhanVienID
                    WHERE (@year IS NULL OR YEAR(l.NamThang) = @year)
                      AND (@month IS NULL OR MONTH(l.NamThang) = @month)
                      AND (@quarter IS NULL OR DATEPART(QUARTER, l.NamThang) = @quarter)
                    GROUP BY 
                        CASE 
                            WHEN DATEDIFF(YEAR, nv.NgayVaoLam, GETDATE()) < 5 THEN N'Dưới 5 năm'
                            WHEN DATEDIFF(YEAR, nv.NgayVaoLam, GETDATE()) BETWEEN 5 AND 10 THEN N'5-10 năm'
                            ELSE N'Trên 10 năm'
                        END";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@year", (object)year ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@month", (object)month ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@quarter", (object)quarter ?? DBNull.Value);

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        // --- Lương theo tháng của nhân viên ---
        public DataTable GetEmployeeMonthlySalary(int nhanVienId, int year)
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();
                string sql = @"
                    SELECT MONTH(l.NamThang) AS Thang,
                           SUM(l.LuongCoBan + l.PhuCap + l.Thuong - l.KhauTru) AS ThucLinh
                    FROM luong l
                    WHERE l.NhanVienID = @NhanVienID AND YEAR(l.NamThang) = @Year
                    GROUP BY MONTH(l.NamThang)
                    ORDER BY Thang";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@NhanVienID", nhanVienId);
                    cmd.Parameters.AddWithValue("@Year", year);

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        // --- Lương theo quý ---
        public DataTable GetEmployeeQuarterlySalary(int nhanVienId, int year, int quarter)
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();
                string sql = @"
                    SELECT DATEPART(QUARTER, l.NamThang) AS Quy,
                           SUM(l.LuongCoBan + l.PhuCap + l.Thuong - l.KhauTru) AS TongThucLinh
                    FROM luong l
                    WHERE l.NhanVienID = @NhanVienID AND YEAR(l.NamThang) = @Year
                          AND DATEPART(QUARTER, l.NamThang) = @Quarter
                    GROUP BY DATEPART(QUARTER, l.NamThang)";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@NhanVienID", nhanVienId);
                    cmd.Parameters.AddWithValue("@Year", year);
                    cmd.Parameters.AddWithValue("@Quarter", quarter);

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        // --- Lương cả năm ---
        public DataTable GetEmployeeYearlySalary(int nhanVienId, int year)
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();
                string sql = @"
                    SELECT YEAR(l.NamThang) AS Nam,
                           SUM(l.LuongCoBan + l.PhuCap + l.Thuong - l.KhauTru) AS TongThucLinh
                    FROM luong l
                    WHERE l.NhanVienID = @NhanVienID AND YEAR(l.NamThang) = @Year
                    GROUP BY YEAR(l.NamThang)";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@NhanVienID", nhanVienId);
                    cmd.Parameters.AddWithValue("@Year", year);

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }
}
