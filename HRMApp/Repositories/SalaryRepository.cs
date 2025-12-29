using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace HRMApp.Repositories
{
    public class SalaryRepository
    {
        private readonly DBConnection db = new DBConnection();

        // =========================
        // 1) THÊM / CẬP NHẬT LƯƠNG theo (NhanVienID, Thang, Nam)
        // =========================
        public void UpsertSalary(int nhanVienId, int thang, int nam,
                                 decimal luongCoBan, decimal phuCap, decimal thuong, decimal khauTru)
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();

                string sql = @"
IF EXISTS (SELECT 1 FROM luong WHERE NhanVienID=@NhanVienID AND Thang=@Thang AND Nam=@Nam)
BEGIN
    UPDATE luong
    SET LuongCoBan=@LuongCoBan, PhuCap=@PhuCap, Thuong=@Thuong, KhauTru=@KhauTru
    WHERE NhanVienID=@NhanVienID AND Thang=@Thang AND Nam=@Nam
END
ELSE
BEGIN
    INSERT INTO luong(NhanVienID, Thang, Nam, LuongCoBan, PhuCap, Thuong, KhauTru)
    VALUES(@NhanVienID, @Thang, @Nam, @LuongCoBan, @PhuCap, @Thuong, @KhauTru)
END";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@NhanVienID", nhanVienId);
                    cmd.Parameters.AddWithValue("@Thang", thang);
                    cmd.Parameters.AddWithValue("@Nam", nam);

                    cmd.Parameters.AddWithValue("@LuongCoBan", luongCoBan);
                    cmd.Parameters.AddWithValue("@PhuCap", phuCap);
                    cmd.Parameters.AddWithValue("@Thuong", thuong);
                    cmd.Parameters.AddWithValue("@KhauTru", khauTru);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // =========================
        // 2) SỬA LƯƠNG theo LuongID (để “Sửa” đúng dòng đang chọn)
        // =========================
        public void UpdateSalaryByLuongId(int luongId,
                                          decimal luongCoBan, decimal phuCap, decimal thuong, decimal khauTru)
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();
                string sql = @"
UPDATE luong
SET LuongCoBan=@LuongCoBan,
    PhuCap=@PhuCap,
    Thuong=@Thuong,
    KhauTru=@KhauTru
WHERE LuongID=@LuongID;";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@LuongID", luongId);
                    cmd.Parameters.AddWithValue("@LuongCoBan", luongCoBan);
                    cmd.Parameters.AddWithValue("@PhuCap", phuCap);
                    cmd.Parameters.AddWithValue("@Thuong", thuong);
                    cmd.Parameters.AddWithValue("@KhauTru", khauTru);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // =========================
        // 3) NHẬP NGÀY NGHỈ THEO THÁNG (không sửa schema DB)
        // -> tự tạo record chamcong theo ngày (chỉ tạo trạng thái nghỉ)
        // =========================
        public class ChamCongSummary
        {
            public int SoNgayDiLam { get; set; }
            public int SoNgayNghiPhep { get; set; }
            public int SoNgayNghiKhongPhep { get; set; }
        }

        public ChamCongSummary GetChamCongSummary(int nhanVienId, int thang, int nam)
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();
                string sql = @"
            SELECT
                ISNULL(SUM(CASE WHEN TrangThai = N'Đi làm' THEN 1 ELSE 0 END), 0) AS SoNgayDiLam,
                ISNULL(SUM(CASE WHEN TrangThai = N'Nghỉ phép' THEN 1 ELSE 0 END), 0) AS SoNgayNghiPhep,
                ISNULL(SUM(CASE WHEN TrangThai = N'Nghỉ không phép' THEN 1 ELSE 0 END), 0) AS SoNgayNghiKhongPhep
            FROM chamcong
            WHERE NhanVienID = @NhanVienID
              AND MONTH(Ngay) = @Thang
              AND YEAR(Ngay) = @Nam;
        ";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@NhanVienID", nhanVienId);
                    cmd.Parameters.AddWithValue("@Thang", thang);
                    cmd.Parameters.AddWithValue("@Nam", nam);

                    using (var r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            return new ChamCongSummary
                            {
                                SoNgayDiLam = Convert.ToInt32(r["SoNgayDiLam"]),
                                SoNgayNghiPhep = Convert.ToInt32(r["SoNgayNghiPhep"]),
                                SoNgayNghiKhongPhep = Convert.ToInt32(r["SoNgayNghiKhongPhep"])
                            };
                        }
                    }
                }
            }

            return new ChamCongSummary(); // mặc định 0
        }

        public void SetMonthlyLeaveCounts(int nhanVienId, int thang, int nam, int nghiPhep, int nghiKhongPhep)
        {
            var workingDates = GetWorkingDates(nam, thang); // bỏ T7/CN
            int totalWorking = workingDates.Count;

            if (nghiPhep < 0) nghiPhep = 0;
            if (nghiKhongPhep < 0) nghiKhongPhep = 0;

            int totalLeave = nghiPhep + nghiKhongPhep;
            if (totalLeave > totalWorking) totalLeave = totalWorking;

            if (nghiPhep > totalLeave) nghiPhep = totalLeave;
            if (nghiKhongPhep > totalLeave - nghiPhep) nghiKhongPhep = totalLeave - nghiPhep;

            using (var conn = db.GetConnection())
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        // Xóa record nghỉ cũ trong tháng (chỉ 2 trạng thái nghỉ)
                        string delSql = @"
DELETE FROM chamcong
WHERE NhanVienID=@NhanVienID
  AND MONTH(Ngay)=@Thang AND YEAR(Ngay)=@Nam
  AND TrangThai IN (N'Nghỉ phép', N'Nghỉ không phép');";

                        using (var delCmd = new SqlCommand(delSql, conn, tran))
                        {
                            delCmd.Parameters.AddWithValue("@NhanVienID", nhanVienId);
                            delCmd.Parameters.AddWithValue("@Thang", thang);
                            delCmd.Parameters.AddWithValue("@Nam", nam);
                            delCmd.ExecuteNonQuery();
                        }

                        // Insert nghỉ phép
                        for (int i = 0; i < nghiPhep; i++)
                            UpsertChamCong(conn, tran, nhanVienId, workingDates[i], "Nghỉ phép");

                        // Insert nghỉ không phép
                        for (int i = 0; i < nghiKhongPhep; i++)
                            UpsertChamCong(conn, tran, nhanVienId, workingDates[nghiPhep + i], "Nghỉ không phép");

                        tran.Commit();
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        private void UpsertChamCong(SqlConnection conn, SqlTransaction tran, int nhanVienId, DateTime ngay, string trangThai)
        {
            string sql = @"
IF EXISTS (SELECT 1 FROM chamcong WHERE NhanVienID = @NhanVienID AND Ngay = @Ngay)
BEGIN
    UPDATE chamcong
    SET TrangThai = @TrangThai
    WHERE NhanVienID = @NhanVienID AND Ngay = @Ngay
END
ELSE
BEGIN
    INSERT INTO chamcong(NhanVienID, Ngay, TrangThai)
    VALUES(@NhanVienID, @Ngay, @TrangThai)
END";

            using (var cmd = new SqlCommand(sql, conn, tran))
            {
                cmd.Parameters.Add("@NhanVienID", SqlDbType.Int).Value = nhanVienId;
                cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = ngay.Date;
                cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = trangThai;

                cmd.ExecuteNonQuery();
            }
        }


        private List<DateTime> GetWorkingDates(int year, int month)
        {
            var list = new List<DateTime>();
            int days = DateTime.DaysInMonth(year, month);
            for (int d = 1; d <= days; d++)
            {
                var dt = new DateTime(year, month, d);
                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday) continue;
                list.Add(dt);
            }
            return list;
        }

        private int GetWorkingDays(int year, int month) => GetWorkingDates(year, month).Count;

        // =========================
        // 4) REPORT: lấy dữ liệu + tính CongChuan / DiLam / LuongMoiNgay / ThucLinh bằng C#
        // (không fix cứng 22 nữa)
        // =========================
        public DataTable GetAllSalaryReport(int? thang, int nam)
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();

                string sql = @"
            SELECT
                l.LuongID,
                l.NhanVienID,
                nv.HoTen,
                l.Thang,
                l.Nam,
                l.LuongCoBan, l.PhuCap, l.Thuong, l.KhauTru, l.ThucLinh,

                ISNULL(a.SoNgayDiLam, 0)         AS SoNgayDiLam,
                ISNULL(a.SoNgayNghiPhep, 0)      AS SoNgayNghiPhep,
                ISNULL(a.SoNgayNghiKhongPhep, 0) AS SoNgayNghiKhongPhep

            FROM luong l
            INNER JOIN nhanvien nv ON nv.NhanVienID = l.NhanVienID

            LEFT JOIN (
                SELECT
                    cc.NhanVienID,
                    MONTH(cc.Ngay) AS Thang,
                    YEAR(cc.Ngay)  AS Nam,
                    SUM(CASE WHEN cc.TrangThai = N'Đi làm' THEN 1 ELSE 0 END) AS SoNgayDiLam,
                    SUM(CASE WHEN cc.TrangThai = N'Nghỉ phép' THEN 1 ELSE 0 END) AS SoNgayNghiPhep,
                    SUM(CASE WHEN cc.TrangThai = N'Nghỉ không phép' THEN 1 ELSE 0 END) AS SoNgayNghiKhongPhep
                FROM chamcong cc
                GROUP BY cc.NhanVienID, MONTH(cc.Ngay), YEAR(cc.Ngay)
            ) a
            ON a.NhanVienID = l.NhanVienID
            AND a.Thang = l.Thang
            AND a.Nam = l.Nam

            WHERE l.Nam = @Nam
            AND (@Thang IS NULL OR l.Thang = @Thang)

            ORDER BY l.Nam, l.Thang, nv.HoTen;
        ";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@Nam", SqlDbType.Int).Value = nam;
                    cmd.Parameters.Add("@Thang", SqlDbType.Int).Value = (object)thang ?? DBNull.Value;

                    var dt = new DataTable();
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    return dt;
                }
            }
        }


        public DataTable GetAllSalaryByEmployee(int nhanVienId, int nam)
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();

                string sql = @"
SELECT  l.LuongID, l.NhanVienID, nv.HoTen, l.Thang, l.Nam,
        l.LuongCoBan, l.PhuCap, l.Thuong, l.KhauTru,
        ISNULL(cc.SoNgayNghiPhep, 0)       AS SoNgayNghiPhep,
        ISNULL(cc.SoNgayNghiKhongPhep, 0) AS SoNgayNghiKhongPhep
FROM luong l
JOIN nhanvien nv ON nv.NhanVienID = l.NhanVienID
LEFT JOIN (
    SELECT  NhanVienID, MONTH(Ngay) AS Thang, YEAR(Ngay) AS Nam,
            SUM(CASE WHEN TrangThai = N'Nghỉ phép' THEN 1 ELSE 0 END)       AS SoNgayNghiPhep,
            SUM(CASE WHEN TrangThai = N'Nghỉ không phép' THEN 1 ELSE 0 END) AS SoNgayNghiKhongPhep
    FROM chamcong
    GROUP BY NhanVienID, MONTH(Ngay), YEAR(Ngay)
) cc ON cc.NhanVienID = l.NhanVienID AND cc.Thang = l.Thang AND cc.Nam = l.Nam
WHERE l.NhanVienID=@NhanVienID AND l.Nam=@Nam
ORDER BY l.Thang;";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@NhanVienID", nhanVienId);
                    cmd.Parameters.AddWithValue("@Nam", nam);

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        da.Fill(dt);
                        EnrichSalaryTable(dt);
                        return dt;
                    }
                }
            }
        }

        public DataTable GetSalaryByEmployeeMonth(int nhanVienId, int thang, int nam)
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();

                string sql = @"
SELECT  l.LuongID, l.NhanVienID, nv.HoTen, l.Thang, l.Nam,
        l.LuongCoBan, l.PhuCap, l.Thuong, l.KhauTru,
        ISNULL(cc.SoNgayNghiPhep, 0)       AS SoNgayNghiPhep,
        ISNULL(cc.SoNgayNghiKhongPhep, 0) AS SoNgayNghiKhongPhep
FROM luong l
JOIN nhanvien nv ON nv.NhanVienID = l.NhanVienID
LEFT JOIN (
    SELECT  NhanVienID, MONTH(Ngay) AS Thang, YEAR(Ngay) AS Nam,
            SUM(CASE WHEN TrangThai = N'Nghỉ phép' THEN 1 ELSE 0 END)       AS SoNgayNghiPhep,
            SUM(CASE WHEN TrangThai = N'Nghỉ không phép' THEN 1 ELSE 0 END) AS SoNgayNghiKhongPhep
    FROM chamcong
    GROUP BY NhanVienID, MONTH(Ngay), YEAR(Ngay)
) cc ON cc.NhanVienID = l.NhanVienID AND cc.Thang = l.Thang AND cc.Nam = l.Nam
WHERE l.NhanVienID=@NhanVienID AND l.Thang=@Thang AND l.Nam=@Nam;";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@NhanVienID", nhanVienId);
                    cmd.Parameters.AddWithValue("@Thang", thang);
                    cmd.Parameters.AddWithValue("@Nam", nam);

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        da.Fill(dt);
                        EnrichSalaryTable(dt);
                        return dt;
                    }
                }
            }
        }

        private void EnrichSalaryTable(DataTable dt)
        {
            // thêm cột tính toán nếu chưa có
            if (!dt.Columns.Contains("SoNgayCongChuan")) dt.Columns.Add("SoNgayCongChuan", typeof(int));
            if (!dt.Columns.Contains("SoNgayDiLam")) dt.Columns.Add("SoNgayDiLam", typeof(int));
            if (!dt.Columns.Contains("LuongMoiNgay")) dt.Columns.Add("LuongMoiNgay", typeof(decimal));
            if (!dt.Columns.Contains("ThucLinh")) dt.Columns.Add("ThucLinh", typeof(decimal));

            foreach (DataRow r in dt.Rows)
            {
                int thang = SafeToInt(r["Thang"], 1);
                int nam = SafeToInt(r["Nam"], 0);

                int congChuan = (nam > 0) ? GetWorkingDays(nam, thang) : 0;
                int nghiPhep = dt.Columns.Contains("SoNgayNghiPhep") ? SafeToInt(r["SoNgayNghiPhep"], 0) : 0;
                int nghiKhong = dt.Columns.Contains("SoNgayNghiKhongPhep") ? SafeToInt(r["SoNgayNghiKhongPhep"], 0) : 0;

                int diLam = Math.Max(0, congChuan - (nghiPhep + nghiKhong));

                decimal luongCoBan = SafeToDecimal(r["LuongCoBan"], 0);
                decimal phuCap = SafeToDecimal(r["PhuCap"], 0);
                decimal thuong = SafeToDecimal(r["Thuong"], 0);
                decimal khauTru = SafeToDecimal(r["KhauTru"], 0);

                decimal luongMoiNgay = (congChuan > 0) ? (luongCoBan / congChuan) : 0m;
                decimal thucLinh = (diLam * luongMoiNgay) + phuCap + thuong - khauTru;

                r["SoNgayCongChuan"] = congChuan;
                r["SoNgayDiLam"] = diLam;
                r["LuongMoiNgay"] = luongMoiNgay;
                r["ThucLinh"] = thucLinh;
            }
        }

        private int SafeToInt(object v, int def)
        {
            if (v == null || v == DBNull.Value) return def;
            if (int.TryParse(v.ToString(), out int x)) return x;
            return def;
        }

        private decimal SafeToDecimal(object v, decimal def)
        {
            if (v == null || v == DBNull.Value) return def;
            if (decimal.TryParse(v.ToString(), out decimal x)) return x;
            return def;
        }

        // =========================
        // 5) XÓA
        // =========================
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
