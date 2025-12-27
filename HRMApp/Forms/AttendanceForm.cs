using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HRMApp.Repositories;
using HRMApp.Models;

namespace HRMApp.Forms
{
    public partial class AttendanceForm : Form
    {
        private readonly AttendanceRepository repo = new AttendanceRepository();
        private readonly DBConnection db = new DBConnection();
        private readonly User currentUser;   // người đăng nhập

        // Constructor mặc định (Admin dùng)
        public AttendanceForm()
        {
            InitializeComponent();
        }

        // Constructor cho nhân viên / thực tập
        public AttendanceForm(User user) : this()
        {
            currentUser = user;
        }

        private void AttendanceForm_Load(object sender, EventArgs e)
        {
            LoadNhanVien();
            LoadChamCong();

            if (currentUser != null && currentUser.VaiTroID != 1 && currentUser.VaiTroID != 5 && currentUser.VaiTroID != 6)
            {
                // Nhân viên / thực tập
                cboNhanVien.SelectedValue = currentUser.NhanVienID;
                cboNhanVien.Enabled = false;
                lblWelcome.Text = $"Xin chào {currentUser.HoTen}, vui lòng chấm công!";
            }
            else
            {
                // Admin
                lblWelcome.Text = "Quản lý chấm công";
            }
        }

        private void LoadNhanVien()
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT NhanVienID, HoTen FROM nhanvien", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cboNhanVien.DataSource = dt;
                cboNhanVien.DisplayMember = "HoTen";
                cboNhanVien.ValueMember = "NhanVienID";
            }
        }

        private void LoadChamCong()
        {
            DataTable dt;

            if (currentUser != null && currentUser.VaiTroID != 1 && currentUser.VaiTroID != 5 && currentUser.VaiTroID != 6)
            {
                // Nhân viên → chỉ xem chấm công của mình
                dt = repo.GetByNhanVien(currentUser.NhanVienID);
            }
            else
            {
                // Admin / Quản lý / Giám đốc → xem tất cả
                dt = repo.GetAll();
            }

            dgvChamCong.DataSource = dt;

            // Đặt tên cột hiển thị
            if (dgvChamCong.Columns.Contains("ChamCongID"))
                dgvChamCong.Columns["ChamCongID"].HeaderText = "Mã CC";
            if (dgvChamCong.Columns.Contains("HoTen"))
                dgvChamCong.Columns["HoTen"].HeaderText = "Họ tên";
            if (dgvChamCong.Columns.Contains("Ngay"))
                dgvChamCong.Columns["Ngay"].HeaderText = "Ngày";
            if (dgvChamCong.Columns.Contains("TrangThai"))
                dgvChamCong.Columns["TrangThai"].HeaderText = "Trạng thái";

            // Căn giữa tất cả cột
            foreach (DataGridViewColumn col in dgvChamCong.Columns)
            {
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                repo.AddAttendance(
                    Convert.ToInt32(cboNhanVien.SelectedValue),
                    dtpNgay.Value,
                    cboTrangThai.Text
                );

                LoadChamCong();
                MessageBox.Show("✅ Đã thêm chấm công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Unique constraint (trùng ngày)
                {
                    MessageBox.Show("⚠ Nhân viên này đã chấm công hôm nay!",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("❌ Lỗi SQL: " + ex.Message,
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvChamCong.CurrentRow == null) return;

            int id = Convert.ToInt32(dgvChamCong.CurrentRow.Cells["ChamCongID"].Value);

            try
            {
                repo.UpdateAttendance(
                    id,
                    Convert.ToInt32(cboNhanVien.SelectedValue),
                    dtpNgay.Value,
                    cboTrangThai.Text
                );

                LoadChamCong();
                MessageBox.Show("✅ Đã cập nhật chấm công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvChamCong.CurrentRow == null) return;

            int id = Convert.ToInt32(dgvChamCong.CurrentRow.Cells["ChamCongID"].Value);
            repo.DeleteAttendance(id);

            LoadChamCong();
            MessageBox.Show("✅ Đã xóa chấm công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            LoadChamCong();
        }

        private void dgvChamCong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvChamCong.CurrentRow != null)
            {
                cboNhanVien.Text = dgvChamCong.CurrentRow.Cells["HoTen"].Value.ToString();
                cboTrangThai.Text = dgvChamCong.CurrentRow.Cells["TrangThai"].Value.ToString();
                dtpNgay.Value = Convert.ToDateTime(dgvChamCong.CurrentRow.Cells["Ngay"].Value);
            }
        }
    }
}
