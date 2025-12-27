using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using HRMApp.Repositories;
using iTextSharp.text;       // iTextSharp
using iTextSharp.text.pdf;   // iTextSharp
using ClosedXML.Excel;
using System.Linq;

namespace HRMApp.Forms
{
    public partial class EmployeeForm : Form
    {
        private readonly EmployeeRepository _repo = new EmployeeRepository();
        private readonly DBConnection _db = new DBConnection();
        public EmployeeForm()
        {
            InitializeComponent();
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            LoadEmployees();
            LoadCombos();
        }

        private void LoadEmployees()
        {
            dgvEmployees.DataSource = _repo.GetAll();

            // 📌 Căn giữa toàn bộ cột
            foreach (DataGridViewColumn col in dgvEmployees.Columns)
            {
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // 📌 Riêng cột Họ tên căn trái và rộng hơn
            if (dgvEmployees.Columns["HoTen"] != null)
            {
                dgvEmployees.Columns["HoTen"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvEmployees.Columns["HoTen"].Width = 200; // tăng độ rộng
            }
        }

        private void LoadCombos()
        {
            // Giới tính
            cboGioiTinh.Items.Clear();
            cboGioiTinh.Items.Add("Nam");
            cboGioiTinh.Items.Add("Nữ");
            cboGioiTinh.SelectedIndex = -1;

            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                // ======================
                // PHÒNG BAN
                // ======================
                SqlDataAdapter daPB = new SqlDataAdapter(
                    "SELECT PhongBanID, TenPhongBan FROM phongban ORDER BY TenPhongBan", conn);
                DataTable dtPB = new DataTable();
                daPB.Fill(dtPB);

                // Combo chọn để Thêm/Sửa (không cần "Tất cả")
                cboPhongBan.DataSource = dtPB;
                cboPhongBan.DisplayMember = "TenPhongBan";
                cboPhongBan.ValueMember = "PhongBanID";
                cboPhongBan.SelectedIndex = -1;

                // Combo tìm kiếm (có "Tất cả" = 0)
                DataTable dtPBSearch = dtPB.Copy();
                DataRow allPB = dtPBSearch.NewRow();
                allPB["PhongBanID"] = 0;
                allPB["TenPhongBan"] = "-- Tất cả phòng ban --";
                dtPBSearch.Rows.InsertAt(allPB, 0);

                cboTimPhongBan.DataSource = dtPBSearch;
                cboTimPhongBan.DisplayMember = "TenPhongBan";
                cboTimPhongBan.ValueMember = "PhongBanID";
                cboTimPhongBan.SelectedValue = 0;

                // ======================
                // VAI TRÒ
                // ======================
                SqlDataAdapter daVT = new SqlDataAdapter(
                    "SELECT VaiTroID, TenVaiTro FROM vaitro ORDER BY TenVaiTro", conn);
                DataTable dtVT = new DataTable();
                daVT.Fill(dtVT);

                cboVaiTro.DataSource = dtVT;
                cboVaiTro.DisplayMember = "TenVaiTro";
                cboVaiTro.ValueMember = "VaiTroID";
                cboVaiTro.SelectedIndex = -1;

                DataTable dtVTSearch = dtVT.Copy();
                DataRow allVT = dtVTSearch.NewRow();
                allVT["VaiTroID"] = 0;
                allVT["TenVaiTro"] = "-- Tất cả vai trò --";
                dtVTSearch.Rows.InsertAt(allVT, 0);

                cboTimVaiTro.DataSource = dtVTSearch;
                cboTimVaiTro.DisplayMember = "TenVaiTro";
                cboTimVaiTro.ValueMember = "VaiTroID";
                cboTimVaiTro.SelectedValue = 0;
            }

            // (Tuỳ chọn) Không cho gõ tự do
            cboPhongBan.DropDownStyle = ComboBoxStyle.DropDownList;
            cboVaiTro.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTimPhongBan.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTimVaiTro.DropDownStyle = ComboBoxStyle.DropDownList;
        }


        private int ConvertGenderToInt(string gender) =>
            string.IsNullOrEmpty(gender) ? 0 : (gender == "Nam" ? 1 : 0);

        private string ConvertGenderToText(object val) =>
            val == DBNull.Value ? "" : (Convert.ToInt32(val) == 1 ? "Nam" : "Nữ");

        // =========================
        // 📌 THÊM NHÂN VIÊN
        // =========================
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                _repo.AddEmployee(
                    txtHoTen.Text,
                    dtpNgaySinh.Value,
                    ConvertGenderToInt(cboGioiTinh.Text),
                    txtSoDienThoai.Text,
                    txtEmail.Text,
                    txtDiaChi.Text,
                    dtpNgayVaoLam.Value,
                    Convert.ToInt32(cboPhongBan.SelectedValue),
                    Convert.ToInt32(cboVaiTro.SelectedValue)
                );
                MessageBox.Show("✅ Đã thêm nhân viên!");
                LoadEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi thêm nhân viên: " + ex.Message);
            }
        }

        // 📌 SỬA
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.CurrentRow == null) return;
            int id = Convert.ToInt32(dgvEmployees.CurrentRow.Cells["NhanVienID"].Value);

            try
            {
                _repo.UpdateEmployee(
                    id,
                    txtHoTen.Text,
                    dtpNgaySinh.Value,
                    ConvertGenderToInt(cboGioiTinh.Text),
                    txtSoDienThoai.Text,
                    txtEmail.Text,
                    txtDiaChi.Text,
                    dtpNgayVaoLam.Value,
                    Convert.ToInt32(cboPhongBan.SelectedValue),
                    Convert.ToInt32(cboVaiTro.SelectedValue)
                );
                MessageBox.Show("✅ Đã cập nhật!");
                LoadEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi sửa nhân viên: " + ex.Message);
            }
        }

        // 📌 XÓA
        //private void btnXoa_Click(object sender, EventArgs e)
        //{
        //    if (dgvEmployees.CurrentRow == null) return;
        //    int id = Convert.ToInt32(dgvEmployees.CurrentRow.Cells["NhanVienID"].Value);
        //    _repo.DeleteEmployee(id);
        //    MessageBox.Show("✅ Đã xóa!");
        //    LoadEmployees();
        //}
        private void ResetForm()
        {
            txtHoTen.Clear();
            txtSoDienThoai.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();

            cboGioiTinh.SelectedIndex = -1;

            // ✅ reset combobox có DataSource
            if (cboPhongBan.Items.Count > 0) cboPhongBan.SelectedIndex = 0;
            if (cboVaiTro.Items.Count > 0) cboVaiTro.SelectedIndex = 0;

            dtpNgaySinh.Value = DateTime.Now;
            dtpNgayVaoLam.Value = DateTime.Now;

            dgvEmployees.ClearSelection();
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.CurrentRow == null) return;

            int id = Convert.ToInt32(dgvEmployees.CurrentRow.Cells["NhanVienID"].Value);

            try
            {
                bool ok = _repo.DeleteEmployee(id);
                if (ok)
                {
                    MessageBox.Show("✅ Đã xóa!");
                    LoadEmployees();
                    ResetForm(); // ✅ reset sau khi xóa
                }
                else
                {
                    MessageBox.Show("❌ Không tìm thấy nhân viên để xóa!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi xóa: " + ex.Message);
            }
        }



        // 📌 CLICK GRIDVIEW
        private void dgvEmployees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            txtHoTen.Text = dgvEmployees.CurrentRow.Cells["HoTen"].Value?.ToString();
            dtpNgaySinh.Value = dgvEmployees.CurrentRow.Cells["NgaySinh"].Value != DBNull.Value
                ? Convert.ToDateTime(dgvEmployees.CurrentRow.Cells["NgaySinh"].Value)
                : DateTime.Now;

            cboGioiTinh.Text = ConvertGenderToText(dgvEmployees.CurrentRow.Cells["GioiTinh"].Value);
            txtSoDienThoai.Text = dgvEmployees.CurrentRow.Cells["SoDienThoai"].Value?.ToString();
            txtEmail.Text = dgvEmployees.CurrentRow.Cells["Email"].Value?.ToString();
            txtDiaChi.Text = dgvEmployees.CurrentRow.Cells["DiaChi"].Value?.ToString();
            dtpNgayVaoLam.Value = dgvEmployees.CurrentRow.Cells["NgayVaoLam"].Value != DBNull.Value
                ? Convert.ToDateTime(dgvEmployees.CurrentRow.Cells["NgayVaoLam"].Value)
                : DateTime.Now;

            if (dgvEmployees.CurrentRow.Cells["PhongBanID"].Value != DBNull.Value)
                cboPhongBan.SelectedValue = Convert.ToInt32(dgvEmployees.CurrentRow.Cells["PhongBanID"].Value);

            if (dgvEmployees.CurrentRow.Cells["VaiTroID"].Value != DBNull.Value)
                cboVaiTro.SelectedValue = Convert.ToInt32(dgvEmployees.CurrentRow.Cells["VaiTroID"].Value);

        }

        // 📌 TÌM KIẾM
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            int phongBan = cboTimPhongBan.SelectedValue != null ? Convert.ToInt32(cboTimPhongBan.SelectedValue) : 0;
            int vaiTro = cboTimVaiTro.SelectedValue != null ? Convert.ToInt32(cboTimVaiTro.SelectedValue) : 0;

            dgvEmployees.DataSource = _repo.Search(keyword, phongBan, vaiTro);
        }

        // 📌 IMPORT EXCEL
        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Excel files (*.xlsx)|*.xlsx";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var workbook = new XLWorkbook(ofd.FileName))
                        {
                            var ws = workbook.Worksheet(1); // sheet đầu tiên
                            var rows = ws.RangeUsed().RowsUsed();

                            foreach (var row in rows.Skip(1)) // bỏ header
                            {
                                string hoTen = row.Cell(1).GetString();

                                // Ngày sinh
                                DateTime ngaySinh = DateTime.Now;
                                if (!row.Cell(2).IsEmpty())
                                    DateTime.TryParse(row.Cell(2).GetString(), out ngaySinh);

                                // Giới tính
                                string gt = row.Cell(3).GetString();
                                int gioiTinh = (gt == "Nam" || gt == "1") ? 1 : 0;

                                string sdt = row.Cell(4).GetString();
                                string email = row.Cell(5).GetString();
                                string diaChi = row.Cell(6).GetString();

                                // Ngày vào làm
                                DateTime ngayVaoLam = DateTime.Now;
                                if (!row.Cell(7).IsEmpty())
                                    DateTime.TryParse(row.Cell(7).GetString(), out ngayVaoLam);

                                // Phòng ban ID
                                int phongBanId = 0;
                                int.TryParse(row.Cell(8).GetString(), out phongBanId);

                                // Vai trò ID
                                int vaiTroId = 0;
                                int.TryParse(row.Cell(9).GetString(), out vaiTroId);

                                

                                // Lưu xuống DB
                                _repo.AddEmployee(hoTen, ngaySinh, gioiTinh, sdt, email, diaChi,
                                    ngayVaoLam, phongBanId, vaiTroId);
                            }
                        }

                        MessageBox.Show("✅ Import Excel thành công!");
                        LoadEmployees();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("❌ Lỗi import: " + ex.Message);
                    }
                }
            }
        }

        // 📌 EXPORT PDF
        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PDF file (*.pdf)|*.pdf";
                sfd.FileName = "Employees.pdf";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                        BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                        iTextSharp.text.Font headerFont = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD);
                        iTextSharp.text.Font cellFont = new iTextSharp.text.Font(bf, 10);

                        Document doc = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);
                        PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
                        doc.Open();

                        Paragraph title = new Paragraph("DANH SÁCH NHÂN VIÊN\n\n", new iTextSharp.text.Font(bf, 16, iTextSharp.text.Font.BOLD));
                        title.Alignment = Element.ALIGN_CENTER;
                        doc.Add(title);

                        PdfPTable table = new PdfPTable(dgvEmployees.Columns.Count);
                        table.WidthPercentage = 100;

                        // Set widths (mặc định từ DataGridView, riêng HoTen rộng hơn)
                        float[] widths = new float[dgvEmployees.Columns.Count];
                        for (int i = 0; i < dgvEmployees.Columns.Count; i++)
                        {
                            if (dgvEmployees.Columns[i].Name == "HoTen")
                                widths[i] = 200; // rộng hơn
                            else
                                widths[i] = dgvEmployees.Columns[i].Width;
                        }
                        table.SetWidths(widths);

                        // Header
                        foreach (DataGridViewColumn col in dgvEmployees.Columns)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(col.HeaderText, headerFont));
                            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            table.AddCell(cell);
                        }

                        // Data
                        foreach (DataGridViewRow row in dgvEmployees.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    PdfPCell pdfCell = new PdfPCell(new Phrase(cell.Value?.ToString() ?? "", cellFont));
                                    pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;

                                    // 📌 Căn giữa mặc định, riêng HoTen căn trái
                                    if (dgvEmployees.Columns[cell.ColumnIndex].Name == "HoTen")
                                        pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
                                    else
                                        pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;

                                    table.AddCell(pdfCell);
                                }
                            }
                        }

                        doc.Add(table);
                        doc.Close();

                        MessageBox.Show("✅ Xuất PDF thành công!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("❌ Lỗi xuất PDF: " + ex.Message);
                    }
                }
            }
        }

        private void panelTop_Paint(object sender, PaintEventArgs e) { }

        private void dgvEmployees_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}
