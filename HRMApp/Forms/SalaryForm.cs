using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HRMApp.Repositories;
using ClosedXML.Excel;

namespace HRMApp.Forms
{
    public partial class SalaryForm : Form
    {
        private readonly SalaryRepository repo = new SalaryRepository();
        private readonly DBConnection db = new DBConnection();

        private bool isDetailMode = false;   // trạng thái xem chi tiết

        public SalaryForm()
        {
            InitializeComponent();
        }

        private void SalaryForm_Load(object sender, EventArgs e)
        {
            LoadNhanVien();
            LoadAllSalaryByYear(); // mặc định: hiển thị tất cả nhân viên theo năm
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

        // --- Hiển thị tất cả lương theo năm ---
        private void LoadAllSalaryByYear()
        {
            int nam = (int)numNam.Value;
            DataTable dt = repo.GetAllSalaryReport(null, nam);
            dgvLuong.DataSource = dt;
            isDetailMode = false;
            btnQuayLai.Visible = false;
        }

        // --- Xem báo cáo theo tháng (toàn bộ nhân viên) ---
        private void btnXemThang_Click(object sender, EventArgs e)
        {
            int thang = (int)numThang.Value;
            int nam = (int)numNam.Value;

            DataTable dt = repo.GetAllSalaryReport(thang, nam);
            dgvLuong.DataSource = dt;

            isDetailMode = false;
            btnQuayLai.Visible = false;
        }

        // --- Xem báo cáo chi tiết theo năm (1 nhân viên) ---
        private void btnXemNam_Click(object sender, EventArgs e)
        {
            if (cboNhanVien.SelectedValue == null) return;

            int nhanVienId = Convert.ToInt32(cboNhanVien.SelectedValue);
            int nam = (int)numNam.Value;

            DataTable dt = repo.GetAllSalaryByEmployee(nhanVienId, nam);
            dgvLuong.DataSource = dt;

            isDetailMode = true;
            btnQuayLai.Visible = true;
        }

        // --- Xem báo cáo theo tháng (1 nhân viên) ---
        private void btnXemThangNhanVien_Click(object sender, EventArgs e)
        {
            if (cboNhanVien.SelectedValue == null) return;

            int nhanVienId = Convert.ToInt32(cboNhanVien.SelectedValue);
            int thang = (int)numThang.Value;
            int nam = (int)numNam.Value;

            DataTable dt = repo.GetSalaryByEmployeeMonth(nhanVienId, thang, nam);
            dgvLuong.DataSource = dt;

            isDetailMode = true;
            btnQuayLai.Visible = true;
        }

        // --- Nút Quay lại ---
        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            if (isDetailMode)
            {
                LoadAllSalaryByYear(); // trở về danh sách tổng hợp theo năm
            }
        }

        // --- Xuất Excel toàn bộ nhân viên ---
        private void btnExportAll_Click(object sender, EventArgs e)
        {
            int thang = (int)numThang.Value;
            int nam = (int)numNam.Value;

            DataTable dt = repo.GetAllSalaryReport(thang, nam);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                FileName = $"SalaryReport_{thang}_{nam}.xlsx"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add(dt, "AllEmployees");
                    ws.Columns().AdjustToContents();
                    wb.SaveAs(sfd.FileName);
                }
                MessageBox.Show("Xuất file Excel thành công!");
            }
        }

        // --- Xuất Excel cho 1 nhân viên ---
        private void btnExportByEmployee_Click(object sender, EventArgs e)
        {
            if (cboNhanVien.SelectedValue == null) return;

            int nhanVienId = Convert.ToInt32(cboNhanVien.SelectedValue);
            string tenNv = cboNhanVien.Text;
            int nam = (int)numNam.Value;

            DataTable dt = repo.GetAllSalaryByEmployee(nhanVienId, nam);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                FileName = $"Salary_{tenNv}_{nam}.xlsx"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var ws = wb.Worksheets.Add(dt, tenNv);
                    ws.Columns().AdjustToContents();
                    wb.SaveAs(sfd.FileName);
                }
                MessageBox.Show("Xuất file Excel thành công!");
            }
        }

        // --- Sửa lương ---
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvLuong.CurrentRow == null) return;

            try
            {
                int luongId = Convert.ToInt32(dgvLuong.CurrentRow.Cells["LuongID"].Value);
                int thang = Convert.ToInt32(dgvLuong.CurrentRow.Cells["Thang"].Value);
                int nam = Convert.ToInt32(dgvLuong.CurrentRow.Cells["Nam"].Value);
                decimal luongCoBan = Convert.ToDecimal(dgvLuong.CurrentRow.Cells["LuongCoBan"].Value);
                decimal phuCap = Convert.ToDecimal(dgvLuong.CurrentRow.Cells["PhuCap"].Value);
                decimal thuong = Convert.ToDecimal(dgvLuong.CurrentRow.Cells["Thuong"].Value);
                decimal khauTru = Convert.ToDecimal(dgvLuong.CurrentRow.Cells["KhauTru"].Value);

                repo.UpdateSalary(luongId, thang, nam, luongCoBan, phuCap, thuong, khauTru);
                MessageBox.Show("Cập nhật lương thành công!");
                LoadAllSalaryByYear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message);
            }
        }

        // --- Xóa lương ---
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvLuong.CurrentRow == null) return;

            int luongId = Convert.ToInt32(dgvLuong.CurrentRow.Cells["LuongID"].Value);
            if (MessageBox.Show("Bạn có chắc muốn xoá lương này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                repo.DeleteSalary(luongId);
                MessageBox.Show("Xoá lương thành công!");
                LoadAllSalaryByYear();
            }
        }
    }
}
