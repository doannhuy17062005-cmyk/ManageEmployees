using System.Data;
using System.Data.SqlClient;

namespace HRMApp.Repositories
{
    public class RoleRepository
    {
        private DBConnection db = new DBConnection();

        public DataTable GetAllRoles()
        {
            using (SqlConnection conn = db.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM vaitro", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void AddRole(string ten, string mota)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO vaitro (TenVaiTro, MoTa) VALUES (@ten, @mota)", conn);
                cmd.Parameters.AddWithValue("@ten", ten);
                cmd.Parameters.AddWithValue("@mota", mota);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateRole(int id, string ten, string mota)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE vaitro SET TenVaiTro=@ten, MoTa=@mota WHERE VaiTroID=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@ten", ten);
                cmd.Parameters.AddWithValue("@mota", mota);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteRole(int id)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM vaitro WHERE VaiTroID=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
