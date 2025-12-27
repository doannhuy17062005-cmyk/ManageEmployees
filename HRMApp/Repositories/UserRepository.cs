using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HRMApp.Models;

namespace HRMApp.Repositories
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository()
        {
            _connectionString = new DBConnection().GetConnection().ConnectionString;
        }

        // ✅ Đăng nhập
        public User Login(string username, string password)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string sql = @"
                        SELECT tk.TaiKhoanID, tk.NhanVienID, tk.TenDangNhap, tk.MatKhau, tk.LaNhanSu,
                               nv.HoTen, nv.VaiTroID
                        FROM taikhoan tk
                        INNER JOIN nhanvien nv ON tk.NhanVienID = nv.NhanVienID
                        WHERE tk.TenDangNhap = @username AND tk.MatKhau = @password";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new User
                                {
                                    TaiKhoanID = reader.GetInt32(reader.GetOrdinal("TaiKhoanID")),
                                    NhanVienID = reader.GetInt32(reader.GetOrdinal("NhanVienID")),
                                    TenDangNhap = reader.GetString(reader.GetOrdinal("TenDangNhap")),
                                    MatKhau = reader.GetString(reader.GetOrdinal("MatKhau")),
                                    LaNhanSu = reader.GetByte(reader.GetOrdinal("LaNhanSu")) == 1,
                                    HoTen = reader["HoTen"].ToString(),
                                    VaiTroID = Convert.ToInt32(reader["VaiTroID"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi đăng nhập: " + ex.Message);
            }

            return null;
        }

        // ✅ Lấy danh sách tài khoản
        public List<User> GetAll()
        {
            var users = new List<User>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = @"
                    SELECT tk.TaiKhoanID, tk.NhanVienID, tk.TenDangNhap, tk.MatKhau, tk.LaNhanSu,
                           nv.HoTen, nv.VaiTroID
                    FROM taikhoan tk
                    INNER JOIN nhanvien nv ON tk.NhanVienID = nv.NhanVienID";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            TaiKhoanID = reader.GetInt32(0),
                            NhanVienID = reader.GetInt32(1),
                            TenDangNhap = reader.GetString(2),
                            MatKhau = reader.GetString(3),
                            LaNhanSu = reader.GetByte(4) == 1,
                            HoTen = reader.GetString(5),
                            VaiTroID = reader.GetInt32(6)
                        });
                    }
                }
            }
            return users;
        }

        // ✅ Thêm tài khoản
        public void Add(User user)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO taikhoan (NhanVienID, TenDangNhap, MatKhau, LaNhanSu) VALUES (@nvID, @username, @password, @laNS)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nvID", user.NhanVienID);
                    cmd.Parameters.AddWithValue("@username", user.TenDangNhap);
                    cmd.Parameters.AddWithValue("@password", user.MatKhau);
                    cmd.Parameters.AddWithValue("@laNS", user.LaNhanSu ? 1 : 0);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ✅ Sửa tài khoản
        public void Update(User user)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "UPDATE taikhoan SET TenDangNhap=@username, MatKhau=@password, LaNhanSu=@laNS WHERE TaiKhoanID=@id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", user.TaiKhoanID);
                    cmd.Parameters.AddWithValue("@username", user.TenDangNhap);
                    cmd.Parameters.AddWithValue("@password", user.MatKhau);
                    cmd.Parameters.AddWithValue("@laNS", user.LaNhanSu ? 1 : 0);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ✅ Xóa tài khoản
        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM taikhoan WHERE TaiKhoanID=@id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ✅ Reset mật khẩu về mặc định (123456)
        public void ResetPassword(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "UPDATE taikhoan SET MatKhau='123456' WHERE TaiKhoanID=@id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
