using System;
using System.Data;
using System.Data.SqlClient;

namespace HRMApp.Repositories
{
    public class AttendanceRepository
    {
        private readonly DBConnection db = new DBConnection();

        // ================== LẤY DỮ LIỆU ==================

        // Lấy tất cả chấm công (cho Admin)
        public DataTable GetAll()
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                string sql = @"SELECT cc.ChamCongID, nv.HoTen, cc.Ngay, cc.TrangThai
                               FROM chamcong cc
                               JOIN nhanvien nv ON cc.NhanVienID = nv.NhanVienID
                               ORDER BY cc.Ngay DESC";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Lấy tất cả chấm công của một nhân viên (cho Nhân viên / Thực tập)
        public DataTable GetByNhanVien(int nhanVienId)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                string sql = @"SELECT cc.ChamCongID, nv.HoTen, cc.Ngay, cc.TrangThai
                               FROM chamcong cc
                               JOIN nhanvien nv ON cc.NhanVienID = nv.NhanVienID
                               WHERE cc.NhanVienID = @NhanVienID
                               ORDER BY cc.Ngay DESC";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@NhanVienID", nhanVienId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Lấy chấm công theo tháng/năm (cho báo cáo cá nhân)
        public DataTable GetAttendanceByMonth(int nhanVienId, int thang, int nam)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                string sql = @"SELECT cc.ChamCongID, cc.Ngay, cc.TrangThai
                               FROM chamcong cc
                               WHERE cc.NhanVienID=@NhanVienID 
                               AND MONTH(cc.Ngay)=@Thang 
                               AND YEAR(cc.Ngay)=@Nam
                               ORDER BY cc.Ngay DESC";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@NhanVienID", nhanVienId);
                cmd.Parameters.AddWithValue("@Thang", thang);
                cmd.Parameters.AddWithValue("@Nam", nam);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Lấy chấm công theo ngày cụ thể
        public DataTable GetByDate(DateTime ngay)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                string sql = @"SELECT cc.ChamCongID, nv.HoTen, cc.Ngay, cc.TrangThai
                               FROM chamcong cc
                               JOIN nhanvien nv ON cc.NhanVienID = nv.NhanVienID
                               WHERE CAST(cc.Ngay AS DATE)=@Ngay";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ngay", ngay.Date);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // ================== THÊM / SỬA / XÓA ==================

        public void AddAttendance(int nhanVienId, DateTime ngay, string trangThai)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                // Check trùng ngày trước khi insert
                string checkSql = @"SELECT COUNT(*) FROM chamcong 
                                    WHERE NhanVienID=@NhanVienID AND CAST(Ngay AS DATE)=@Ngay";
                SqlCommand checkCmd = new SqlCommand(checkSql, conn);
                checkCmd.Parameters.AddWithValue("@NhanVienID", nhanVienId);
                checkCmd.Parameters.AddWithValue("@Ngay", ngay.Date);

                int count = (int)checkCmd.ExecuteScalar();
                if (count > 0)
                {
                    throw new Exception("Nhân viên này đã chấm công ngày hôm nay!");
                }

                string sql = "INSERT INTO chamcong (NhanVienID, Ngay, TrangThai) VALUES (@NhanVienID, @Ngay, @TrangThai)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@NhanVienID", nhanVienId);
                cmd.Parameters.AddWithValue("@Ngay", ngay);
                cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateAttendance(int chamCongId, int nhanVienId, DateTime ngay, string trangThai)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                string sql = @"UPDATE chamcong 
                               SET NhanVienID=@NhanVienID, Ngay=@Ngay, TrangThai=@TrangThai 
                               WHERE ChamCongID=@ChamCongID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ChamCongID", chamCongId);
                cmd.Parameters.AddWithValue("@NhanVienID", nhanVienId);
                cmd.Parameters.AddWithValue("@Ngay", ngay);
                cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteAttendance(int chamCongId)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM chamcong WHERE ChamCongID=@ChamCongID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ChamCongID", chamCongId);
                cmd.ExecuteNonQuery();
            }
        }

        // ================== THỐNG KÊ ==================

        public DataTable GetSummaryByMonth(int nhanVienId, int nam)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                string sql = @"SELECT MONTH(Ngay) AS Thang,
                                      SUM(CASE WHEN TrangThai='Đi làm' THEN 1 ELSE 0 END) AS SoNgayDiLam,
                                      SUM(CASE WHEN TrangThai='Nghỉ phép' THEN 1 ELSE 0 END) AS SoNgayNghiPhep
                               FROM chamcong
                               WHERE NhanVienID=@NhanVienID AND YEAR(Ngay)=@Nam
                               GROUP BY MONTH(Ngay)
                               ORDER BY Thang";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@NhanVienID", nhanVienId);
                cmd.Parameters.AddWithValue("@Nam", nam);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}
