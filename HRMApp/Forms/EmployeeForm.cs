using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using HRMApp.Repositories;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ClosedXML.Excel;

// tránh xung đột iTextSharp.text.Image
using DrawingImage = System.Drawing.Image;

namespace HRMApp.Forms
{
    public partial class EmployeeForm : Form
    {
        private readonly EmployeeRepository _repo = new EmployeeRepository();
        private readonly DBConnection _db = new DBConnection();

        private byte[] _anhBytes = null;

        // Nếu bạn CHƯA tạo trong Designer thì để 3 control này
        private PictureBox picAnh;
        private Button btnChonAnh;
        private Button btnXoaAnh;

        public EmployeeForm()
        {
            InitializeComponent();
            EnsurePhotoControls();

            // Nếu Designer chưa gán event thì gán ở đây (có rồi thì vẫn ok)
            dgvEmployees.CellClick += dgvEmployees_CellClick;

            // ✅ 2 event quan trọng để HIỂN THỊ ẢNH + NULL thì TRỐNG
            dgvEmployees.CellFormatting += dgvEmployees_CellFormatting;
            dgvEmployees.DataError += dgvEmployees_DataError;
        }

        // =========================
        // TẠO UI ẢNH (nếu thiếu)
        // =========================
        private void EnsurePhotoControls()
        {
            if (panelTop == null) return;

            if (picAnh == null)
            {
                picAnh = new PictureBox
                {
                    Name = "picAnh",
                    BorderStyle = BorderStyle.FixedSingle,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Location = new Point(340, 80),
                    Size = new Size(180, 150),
                    Anchor = AnchorStyles.Top | AnchorStyles.Right
                };
                panelTop.Controls.Add(picAnh);
                picAnh.BringToFront();
            }

            if (btnChonAnh == null)
            {
                btnChonAnh = new Button
                {
                    Name = "btnChonAnh",
                    Text = "Chọn ảnh",
                    Location = new Point(340, 235),
                    Size = new Size(85, 28),
                    Anchor = AnchorStyles.Top | AnchorStyles.Right
                };
                btnChonAnh.Click += btnChonAnh_Click;
                panelTop.Controls.Add(btnChonAnh);
                btnChonAnh.BringToFront();
            }

            if (btnXoaAnh == null)
            {
                btnXoaAnh = new Button
                {
                    Name = "btnXoaAnh",
                    Text = "Xóa ảnh",
                    Location = new Point(435, 235),
                    Size = new Size(85, 28),
                    Anchor = AnchorStyles.Top | AnchorStyles.Right
                };
                btnXoaAnh.Click += (s, e) =>
                {
                    _anhBytes = null;
                    if (picAnh != null) picAnh.Image = null;
                };
                panelTop.Controls.Add(btnXoaAnh);
                btnXoaAnh.BringToFront();
            }
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _anhBytes = File.ReadAllBytes(ofd.FileName);
                    if (picAnh != null && _anhBytes != null)
                        picAnh.Image = BytesToImageClone(_anhBytes);
                }
            }
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            LoadEmployees();
            LoadCombos();
        }

        // =========================
        // LOAD GRID + HIỂN THỊ ẢNH TRONG GRID
        // =========================
        private void LoadEmployees()
        {
            dgvEmployees.AutoGenerateColumns = true;
            dgvEmployees.DataSource = _repo.GetAll();

            SetupAnhColumn(); // ✅ cấu hình cột ảnh (quan trọng)

            // Ẩn ID nếu muốn
            if (dgvEmployees.Columns.Contains("PhongBanID")) dgvEmployees.Columns["PhongBanID"].Visible = false;
            if (dgvEmployees.Columns.Contains("VaiTroID")) dgvEmployees.Columns["VaiTroID"].Visible = false;

            // format chung
            foreach (DataGridViewColumn col in dgvEmployees.Columns)
            {
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dgvEmployees.Columns["HoTen"] != null)
            {
                dgvEmployees.Columns["HoTen"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgvEmployees.Columns["HoTen"].Width = 200;
            }
            dgvEmployees.AllowUserToAddRows = false;

        }

        // ✅ cấu hình cột Anh để không hiện X đỏ và hiển thị zoom
        private void SetupAnhColumn()
        {
            if (!dgvEmployees.Columns.Contains("Anh")) return;

            // ✅ tắt icon lỗi (dấu X đỏ)
            dgvEmployees.ShowCellErrors = false;
            dgvEmployees.ShowRowErrors = false;
            dgvEmployees.ShowEditingIcon = false;

            if (dgvEmployees.Columns["Anh"] is DataGridViewImageColumn imgCol)
            {
                imgCol.HeaderText = "Ảnh";
                imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;

                // NULL -> trống
                imgCol.DefaultCellStyle.NullValue = null;

                // ✅ ErrorImage nằm ở DataGridViewImageCell, không nằm ở ImageColumn
                if (imgCol.CellTemplate is DataGridViewImageCell cell)
                {
                    
                    cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                }

                dgvEmployees.RowTemplate.Height = 70;
                imgCol.Width = 90;
            }
        }


        // ✅ convert byte[] -> Image để hiển thị trong grid
        private void dgvEmployees_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvEmployees.Columns[e.ColumnIndex].Name != "Anh") return;

            // NULL => trống
            if (e.Value == null || e.Value == DBNull.Value)
            {
                e.Value = null;
                e.FormattingApplied = true;
                return;
            }

            // byte[] => Image
            if (e.Value is byte[] bytes)
            {
                if (bytes.Length == 0)
                {
                    e.Value = null;
                    e.FormattingApplied = true;
                    return;
                }

                try
                {
                    e.Value = BytesToImageClone(bytes);
                }
                catch
                {
                    e.Value = null;
                }

                e.FormattingApplied = true;
            }
        }

        // ✅ chặn DataError để không hiện icon X đỏ
        private void dgvEmployees_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }


        // byte[] -> Image (clone để không bị lỗi dispose stream)
        private DrawingImage BytesToImageClone(byte[] bytes)
        {
            using (var ms = new MemoryStream(bytes))
            using (var img = DrawingImage.FromStream(ms))
            {
                return (DrawingImage)img.Clone();
            }
        }

        // =========================
        // LOAD COMBO
        // =========================
        private void LoadCombos()
        {
            cboGioiTinh.Items.Clear();
            cboGioiTinh.Items.Add("Nam");
            cboGioiTinh.Items.Add("Nữ");
            cboGioiTinh.SelectedIndex = -1;

            using (SqlConnection conn = _db.GetConnection())
            {
                conn.Open();

                // PHÒNG BAN
                SqlDataAdapter daPB = new SqlDataAdapter(
                    "SELECT PhongBanID, TenPhongBan FROM phongban ORDER BY TenPhongBan", conn);
                DataTable dtPB = new DataTable();
                daPB.Fill(dtPB);

                cboPhongBan.DataSource = dtPB;
                cboPhongBan.DisplayMember = "TenPhongBan";
                cboPhongBan.ValueMember = "PhongBanID";
                cboPhongBan.SelectedIndex = -1;

                DataTable dtPBSearch = dtPB.Copy();
                DataRow allPB = dtPBSearch.NewRow();
                allPB["PhongBanID"] = 0;
                allPB["TenPhongBan"] = "-- Tất cả phòng ban --";
                dtPBSearch.Rows.InsertAt(allPB, 0);

                cboTimPhongBan.DataSource = dtPBSearch;
                cboTimPhongBan.DisplayMember = "TenPhongBan";
                cboTimPhongBan.ValueMember = "PhongBanID";
                cboTimPhongBan.SelectedValue = 0;

                // VAI TRÒ
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
        // THÊM
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
                    Convert.ToInt32(cboVaiTro.SelectedValue),
                    _anhBytes
                );

                MessageBox.Show("✅ Đã thêm nhân viên!");
                LoadEmployees();
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi thêm nhân viên: " + ex.Message);
            }
        }

        // =========================
        // SỬA
        // =========================
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
                    Convert.ToInt32(cboVaiTro.SelectedValue),
                    _anhBytes
                );

                MessageBox.Show("✅ Đã cập nhật!");
                LoadEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi sửa nhân viên: " + ex.Message);
            }
        }

        private void ResetForm()
        {
            txtHoTen.Clear();
            txtSoDienThoai.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();

            cboGioiTinh.SelectedIndex = -1;

            if (cboPhongBan.Items.Count > 0) cboPhongBan.SelectedIndex = 0;
            if (cboVaiTro.Items.Count > 0) cboVaiTro.SelectedIndex = 0;

            dtpNgaySinh.Value = DateTime.Now;
            dtpNgayVaoLam.Value = DateTime.Now;

            dgvEmployees.ClearSelection();

            _anhBytes = null;
            if (picAnh != null) picAnh.Image = null;
        }

        // =========================
        // XÓA
        // =========================
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
                    ResetForm();
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

        // =========================
        // CLICK GRID: đổ dữ liệu + đổ ảnh lên picturebox
        // =========================
        private void dgvEmployees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvEmployees.CurrentRow;
            if (row == null) return;

            txtHoTen.Text = row.Cells["HoTen"].Value?.ToString();

            dtpNgaySinh.Value = row.Cells["NgaySinh"].Value != DBNull.Value
                ? Convert.ToDateTime(row.Cells["NgaySinh"].Value)
                : DateTime.Now;

            cboGioiTinh.Text = ConvertGenderToText(row.Cells["GioiTinh"].Value);
            txtSoDienThoai.Text = row.Cells["SoDienThoai"].Value?.ToString();
            txtEmail.Text = row.Cells["Email"].Value?.ToString();
            txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString();

            dtpNgayVaoLam.Value = row.Cells["NgayVaoLam"].Value != DBNull.Value
                ? Convert.ToDateTime(row.Cells["NgayVaoLam"].Value)
                : DateTime.Now;

            if (row.Cells["PhongBanID"].Value != DBNull.Value)
                cboPhongBan.SelectedValue = Convert.ToInt32(row.Cells["PhongBanID"].Value);

            if (row.Cells["VaiTroID"].Value != DBNull.Value)
                cboVaiTro.SelectedValue = Convert.ToInt32(row.Cells["VaiTroID"].Value);

            // ✅ ảnh từ DB -> PictureBox
            if (dgvEmployees.Columns.Contains("Anh") &&
                row.Cells["Anh"].Value != null &&
                row.Cells["Anh"].Value != DBNull.Value &&
                row.Cells["Anh"].Value is byte[] bytes &&
                bytes.Length > 0)
            {
                _anhBytes = bytes;
                if (picAnh != null) picAnh.Image = BytesToImageClone(_anhBytes);
            }
            else
            {
                _anhBytes = null;
                if (picAnh != null) picAnh.Image = null;
            }
        }

        // =========================
        // TÌM KIẾM
        // =========================
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            int phongBan = cboTimPhongBan.SelectedValue != null ? Convert.ToInt32(cboTimPhongBan.SelectedValue) : 0;
            int vaiTro = cboTimVaiTro.SelectedValue != null ? Convert.ToInt32(cboTimVaiTro.SelectedValue) : 0;

            dgvEmployees.DataSource = _repo.Search(keyword, phongBan, vaiTro);
            SetupAnhColumn(); // ✅ cực quan trọng sau khi đổi DataSource
        }

        // =========================
        // IMPORT EXCEL (không có ảnh -> truyền null)
        // =========================
        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Excel files (*.xlsx)|*.xlsx";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Mở file với quyền share để tránh bị Excel/preview khóa
                        using (var stream = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        using (var workbook = new XLWorkbook(stream))
                        {
                            var ws = workbook.Worksheet(1);
                            var rows = ws.RangeUsed().RowsUsed();

                            foreach (var r in rows.Skip(1))
                            {
                                string hoTen = r.Cell(1).GetString();

                                DateTime ngaySinh = DateTime.Now;
                                if (!r.Cell(2).IsEmpty())
                                    DateTime.TryParse(r.Cell(2).GetString(), out ngaySinh);

                                string gt = r.Cell(3).GetString();
                                int gioiTinh = (gt == "Nam" || gt == "1") ? 1 : 0;

                                string sdt = r.Cell(4).GetString();
                                string email = r.Cell(5).GetString();
                                string diaChi = r.Cell(6).GetString();

                                DateTime ngayVaoLam = DateTime.Now;
                                if (!r.Cell(7).IsEmpty())
                                    DateTime.TryParse(r.Cell(7).GetString(), out ngayVaoLam);

                                int phongBanId = 0;
                                int.TryParse(r.Cell(8).GetString(), out phongBanId);

                                int vaiTroId = 0;
                                int.TryParse(r.Cell(9).GetString(), out vaiTroId);

                                _repo.AddEmployee(hoTen, ngaySinh, gioiTinh, sdt, email, diaChi, ngayVaoLam,
                                    phongBanId, vaiTroId, null);
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

        // =========================
        // EXPORT PDF (bỏ cột ảnh)
        // =========================
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
                        var headerFont = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.BOLD);
                        var cellFont = new iTextSharp.text.Font(bf, 10);

                        Document doc = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);
                        PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
                        doc.Open();

                        Paragraph title = new Paragraph("DANH SÁCH NHÂN VIÊN\n\n", new iTextSharp.text.Font(bf, 16, iTextSharp.text.Font.BOLD));
                        title.Alignment = Element.ALIGN_CENTER;
                        doc.Add(title);

                        // bỏ cột Anh khi export
                        var cols = dgvEmployees.Columns
                            .Cast<DataGridViewColumn>()
                            .Where(c => c.Name != "Anh")
                            .ToList();

                        PdfPTable table = new PdfPTable(cols.Count);
                        table.WidthPercentage = 100;

                        foreach (var col in cols)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(col.HeaderText, headerFont));
                            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            table.AddCell(cell);
                        }

                        foreach (DataGridViewRow row in dgvEmployees.Rows)
                        {
                            if (row.IsNewRow) continue;

                            foreach (var col in cols)
                            {
                                var val = row.Cells[col.Name].Value;
                                string text = (val == null || val == DBNull.Value) ? "" : val.ToString();

                                PdfPCell pdfCell = new PdfPCell(new Phrase(text, cellFont));
                                pdfCell.VerticalAlignment = Element.ALIGN_MIDDLE;

                                if (col.Name == "HoTen") pdfCell.HorizontalAlignment = Element.ALIGN_LEFT;
                                else pdfCell.HorizontalAlignment = Element.ALIGN_CENTER;

                                table.AddCell(pdfCell);
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
    }
}
