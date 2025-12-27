using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HRMApp.Forms
{
    public partial class DepartmentForm : Form
    {
        private DBConnection db = new DBConnection();

        public DepartmentForm()
        {
            InitializeComponent();
        }

        private void DepartmentForm_Load(object sender, EventArgs e)
        {
            LoadDepartments();
        }

        private void LoadDepartments()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM phongban", db.GetConnection());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvDepartments.DataSource = dt;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO phongban (TenPhongBan, MoTa) VALUES (@Ten, @MoTa)", conn);
                cmd.Parameters.AddWithValue("@Ten", txtTenPhongBan.Text);
                cmd.Parameters.AddWithValue("@MoTa", txtMoTa.Text);
                cmd.ExecuteNonQuery();
            }
            LoadDepartments();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvDepartments.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvDepartments.CurrentRow.Cells["PhongBanID"].Value);

            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE phongban SET TenPhongBan=@Ten, MoTa=@MoTa WHERE PhongBanID=@ID", conn);
                cmd.Parameters.AddWithValue("@Ten", txtTenPhongBan.Text);
                cmd.Parameters.AddWithValue("@MoTa", txtMoTa.Text);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.ExecuteNonQuery();
            }
            LoadDepartments();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDepartments.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvDepartments.CurrentRow.Cells["PhongBanID"].Value);

            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();

                // Kiểm tra xem còn nhân viên nào thuộc phòng ban này không
                SqlCommand check = new SqlCommand("SELECT COUNT(*) FROM nhanvien WHERE PhongBanID=@ID", conn);
                check.Parameters.AddWithValue("@ID", id);
                int count = (int)check.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Không thể xóa! Phòng ban này vẫn còn nhân viên.",
                                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Nếu không còn nhân viên thì cho phép xóa
                SqlCommand cmd = new SqlCommand("DELETE FROM phongban WHERE PhongBanID=@ID", conn);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Đã xóa phòng ban thành công!");
            }
            LoadDepartments();
        }


        private void dgvDepartments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtTenPhongBan.Text = dgvDepartments.Rows[e.RowIndex].Cells["TenPhongBan"].Value.ToString();
                txtMoTa.Text = dgvDepartments.Rows[e.RowIndex].Cells["MoTa"].Value.ToString();
            }
        }

        private void dgvDepartments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
