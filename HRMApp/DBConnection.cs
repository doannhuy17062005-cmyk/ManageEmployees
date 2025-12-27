using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace HRMApp
{
    public class DBConnection
    {
        private readonly string _connectionString;

        public DBConnection()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["HRMConnection"].ConnectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }

        // === Thêm vào để ReportRepository gọi ===
        public DataTable ExecuteQuery(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = GetConnection())
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                if (parameters != null && parameters.Length > 0)
                    cmd.Parameters.AddRange(parameters);

                conn.Open();
                return cmd.ExecuteScalar();
            }
        }
    }
}
