using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HRMApp.Repositories;
using ClosedXML.Excel;

namespace HRMApp.Forms
{
    public partial class SalaryForm : Form
    {
        private readonly SalaryRepository repo = new SalaryRepository();
        private readonly DBConnection db = new DBConnection();

        private enum ViewMode { AllYear, AllMonth, EmpYear, EmpMonth }
        private ViewMode _viewMode = ViewMode.AllYear;

        // ===== Panel nhập lương trực tiếp =====
        private Panel panelNhap;
        private NumericUpDown nLuongCoBan, nPhuCap, nThuong, nKhauTru, nNghiPhep, nNghiKhong;
        private Button btnLuuNhap;
        private TextBox txtSoNgayDiLam;

        // đang chọn dòng nào để sửa
        private int _selectedLuongId = 0;
        private int _selectedNhanVienId = 0;
        private int _selectedThang = 0;
        private int _selectedNam = 0;

        private bool _suppressChange = false;

        public SalaryForm()
        {
            InitializeComponent();
        }

        private void SalaryForm_Load(object sender, EventArgs e)
        {
            LoadNhanVien();
            InitNhapPanel();

            dgvLuong.CellClick += dgvLuong_CellClick;

            // đổi NV/tháng/năm thì bỏ “dính” dữ liệu cũ
            cboNhanVien.SelectionChangeCommitted += (s, ev) =>
            {
                ClearSelectionAndInputs();
                UpdateChamCongCountsUI();
            };

            numThang.ValueChanged += (s, ev) =>
            {
                if (_suppressChange) return;
                ClearSelectionAndInputs();
                UpdateChamCongCountsUI();
            };

            numNam.ValueChanged += (s, ev) =>
            {
                if (_suppressChange) return;
                ClearSelectionAndInputs();
                UpdateChamCongCountsUI();
            };


            LoadAllSalaryByYear();
            UpdateChamCongCountsUI();
        }

        private void ClearSelectionAndInputs()
        {
            _selectedLuongId = 0;
            _selectedNhanVienId = 0;
            _selectedThang = 0;
            _selectedNam = 0;

            dgvLuong.ClearSelection();
            ResetInputFields();
        }

        private void ResetInputFields()
        {
            if (nLuongCoBan != null) nLuongCoBan.Value = 0;
            if (nPhuCap != null) nPhuCap.Value = 0;
            if (nThuong != null) nThuong.Value = 0;
            if (nKhauTru != null) nKhauTru.Value = 0;
            if (nNghiPhep != null) nNghiPhep.Value = 0;
            if (nNghiKhong != null) nNghiKhong.Value = 0;
            if (txtSoNgayDiLam != null) txtSoNgayDiLam.Text = "0";
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
                cboNhanVien.SelectedIndex = dt.Rows.Count > 0 ? 0 : -1;
            }
        }

        // =========================
        // UI: Panel nhập trực tiếp
        // =========================
        private void InitNhapPanel()
        {
            panelNhap = new Panel
            {
                Height = 90,
                BackColor = Color.WhiteSmoke,
                Padding = new Padding(10)
            };

            var table = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 8,
                RowCount = 2
            };

            for (int i = 0; i < 8; i++)
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5f));

            table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));

            NumericUpDown Money()
            {
                return new NumericUpDown
                {
                    Maximum = 1000000000,
                    DecimalPlaces = 0,
                    ThousandsSeparator = true,
                    Dock = DockStyle.Fill
                };
            }

            NumericUpDown Int31()
            {
                return new NumericUpDown
                {
                    Maximum = 31,
                    DecimalPlaces = 0,
                    Dock = DockStyle.Fill
                };
            }

            nLuongCoBan = Money();
            nPhuCap = Money();
            nThuong = Money();
            nKhauTru = Money();
            nNghiPhep = Int31();
            nNghiKhong = Int31();

            btnLuuNhap = new Button
            {
                Text = "Lưu nhập",
                Dock = DockStyle.Fill
            };
            btnLuuNhap.Click += btnLuuNhap_Click;

            // Row 0
            table.Controls.Add(new Label { Text = "Lương cơ bản", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, 0, 0);
            table.Controls.Add(nLuongCoBan, 1, 0);

            table.Controls.Add(new Label { Text = "Phụ cấp", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, 2, 0);
            table.Controls.Add(nPhuCap, 3, 0);

            table.Controls.Add(new Label { Text = "Thưởng", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, 4, 0);
            table.Controls.Add(nThuong, 5, 0);

            table.Controls.Add(new Label { Text = "Khấu trừ", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, 6, 0);
            table.Controls.Add(nKhauTru, 7, 0);

            // Row 1
            table.Controls.Add(new Label { Text = "Nghỉ phép", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, 0, 1);
            table.Controls.Add(nNghiPhep, 1, 1);

            table.Controls.Add(new Label { Text = "Nghỉ không phép", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, 2, 1);
            table.Controls.Add(nNghiKhong, 3, 1);

            txtSoNgayDiLam = new TextBox
            {
                ReadOnly = true,
                Dock = DockStyle.Fill,
                Text = "0",
                TextAlign = HorizontalAlignment.Right
            };
            table.Controls.Add(new Label { Text = "Đi làm", TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill }, 4, 1);
            table.Controls.Add(txtSoNgayDiLam, 5, 1);

            table.Controls.Add(btnLuuNhap, 6, 1);
            table.SetColumnSpan(btnLuuNhap, 2);

            panelNhap.Controls.Add(table);

            // tìm GroupBox chứa phần thông tin lương (kể cả nằm sâu)
            var gb = FindFirstControl<GroupBox>(this, g =>
                (g.Text ?? "").ToLower().Contains("lương") || (g.Text ?? "").ToLower().Contains("thông tin"));

            Control container = gb != null ? (Control)gb : (Control)this;


            // đặt panel dưới các control hiện có (tránh dock bị che)
            panelNhap.Dock = DockStyle.None;
            panelNhap.Width = container.ClientSize.Width - 20;
            panelNhap.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

            int y = 10;
            var bottoms = container.Controls.Cast<Control>()
                .Where(c => c.Visible && c != panelNhap)
                .Select(c => c.Bottom)
                .ToList();
            if (bottoms.Count > 0) y = bottoms.Max() + 10;

            panelNhap.Location = new Point(10, y);

            container.Controls.Add(panelNhap);
            panelNhap.BringToFront();

            // nếu bị cắt (container thấp quá) thì tăng chiều cao container + parent
            int needH = panelNhap.Bottom + 15;
            if (container.Height < needH) container.Height = needH;
            if (container.Parent != null && container.Parent.Height < container.Bottom + 10)
                container.Parent.Height = container.Bottom + 10;
        }

        private T FindFirstControl<T>(Control root, Func<T, bool> predicate) where T : Control
        {
            foreach (Control c in root.Controls)
            {
                if (c is T t && predicate(t)) return t;

                var child = FindFirstControl<T>(c, predicate);
                if (child != null) return child;
            }
            return null;
        }
        private void UpdateChamCongCountsUI()
        {
            if (cboNhanVien.SelectedValue == null)
            {
                if (nNghiPhep != null) nNghiPhep.Value = 0;
                if (nNghiKhong != null) nNghiKhong.Value = 0;
                if (txtSoNgayDiLam != null) txtSoNgayDiLam.Text = "0";
                return;
            }

            int nvId = Convert.ToInt32(cboNhanVien.SelectedValue);
            int thang = (int)numThang.Value;
            int nam = (int)numNam.Value;

            var s = repo.GetChamCongSummary(nvId, thang, nam);

            nNghiPhep.Value = ClampInt31(s.SoNgayNghiPhep);
            nNghiKhong.Value = ClampInt31(s.SoNgayNghiKhongPhep);
            txtSoNgayDiLam.Text = s.SoNgayDiLam.ToString();
        }

        private void dgvLuong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            FillInputsFromCurrentRow();
            UpdateChamCongCountsUI();
        }

        private void FillInputsFromCurrentRow()
        {
            if (dgvLuong.CurrentRow == null) return;

            var row = dgvLuong.CurrentRow;

            _selectedLuongId = SafeToInt(row.Cells["LuongID"]?.Value, 0);
            _selectedNhanVienId = dgvLuong.Columns.Contains("NhanVienID") ? SafeToInt(row.Cells["NhanVienID"]?.Value, 0) : 0;
            _selectedThang = dgvLuong.Columns.Contains("Thang") ? SafeToInt(row.Cells["Thang"]?.Value, 0) : 0;
            _selectedNam = dgvLuong.Columns.Contains("Nam") ? SafeToInt(row.Cells["Nam"]?.Value, 0) : 0;

            // nạp giá trị vào ô nhập
            if (dgvLuong.Columns.Contains("LuongCoBan"))
                nLuongCoBan.Value = ClampMoney(SafeToDecimal(row.Cells["LuongCoBan"]?.Value, 0));

            if (dgvLuong.Columns.Contains("PhuCap"))
                nPhuCap.Value = ClampMoney(SafeToDecimal(row.Cells["PhuCap"]?.Value, 0));

            if (dgvLuong.Columns.Contains("Thuong"))
                nThuong.Value = ClampMoney(SafeToDecimal(row.Cells["Thuong"]?.Value, 0));

            if (dgvLuong.Columns.Contains("KhauTru"))
                nKhauTru.Value = ClampMoney(SafeToDecimal(row.Cells["KhauTru"]?.Value, 0));

            if (dgvLuong.Columns.Contains("SoNgayNghiPhep"))
                nNghiPhep.Value = ClampInt31(SafeToInt(row.Cells["SoNgayNghiPhep"]?.Value, 0));

            if (dgvLuong.Columns.Contains("SoNgayNghiKhongPhep"))
                nNghiKhong.Value = ClampInt31(SafeToInt(row.Cells["SoNgayNghiKhongPhep"]?.Value, 0));

            // đồng bộ combobox + num (để bạn nhìn đúng NV/tháng/năm đang chọn)
            if (_selectedNhanVienId > 0 && _selectedThang > 0 && _selectedNam > 0)
            {
                _suppressChange = true;
                try
                {
                    cboNhanVien.SelectedValue = _selectedNhanVienId;
                    numThang.Value = _selectedThang;
                    numNam.Value = _selectedNam;
                }
                finally
                {
                    _suppressChange = false;
                }
            }
        }

        private decimal ClampMoney(decimal v)
        {
            if (v < 0) return 0;
            if (v > 1000000000) return 1000000000;
            return v;
        }

        private int ClampInt31(int v)
        {
            if (v < 0) return 0;
            if (v > 31) return 31;
            return v;
        }

        // =========================
        // GRID
        // =========================
        private void FormatGrid()
        {
            dgvLuong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvLuong.Columns["LuongID"] != null) dgvLuong.Columns["LuongID"].Visible = false;
            if (dgvLuong.Columns["NhanVienID"] != null) dgvLuong.Columns["NhanVienID"].Visible = false;

            string[] moneyCols = { "LuongCoBan", "PhuCap", "Thuong", "KhauTru", "LuongMoiNgay", "ThucLinh" };
            foreach (var c in moneyCols)
                if (dgvLuong.Columns[c] != null)
                    dgvLuong.Columns[c].DefaultCellStyle.Format = "N0";
        }


        private void ReloadCurrentView()
        {
            switch (_viewMode)
            {
                case ViewMode.AllYear:
                    LoadAllSalaryByYear();
                    break;

                case ViewMode.AllMonth:
                    dgvLuong.DataSource = repo.GetAllSalaryReport((int)numThang.Value, (int)numNam.Value);
                    btnQuayLai.Visible = false;
                    FormatGrid();
                    break;

                case ViewMode.EmpYear:
                    if (cboNhanVien.SelectedValue == null) return;
                    dgvLuong.DataSource = repo.GetAllSalaryByEmployee(Convert.ToInt32(cboNhanVien.SelectedValue), (int)numNam.Value);
                    btnQuayLai.Visible = true;
                    FormatGrid();
                    break;

                case ViewMode.EmpMonth:
                    if (cboNhanVien.SelectedValue == null) return;
                    dgvLuong.DataSource = repo.GetSalaryByEmployeeMonth(Convert.ToInt32(cboNhanVien.SelectedValue), (int)numThang.Value, (int)numNam.Value);
                    btnQuayLai.Visible = true;
                    FormatGrid();
                    break;
            }
        }

        private void LoadAllSalaryByYear()
        {
            int nam = (int)numNam.Value;
            dgvLuong.DataSource = repo.GetAllSalaryReport(null, nam);

            _viewMode = ViewMode.AllYear;
            btnQuayLai.Visible = false;

            FormatGrid();
        }

        private void btnXemThang_Click(object sender, EventArgs e)
        {
            int thang = (int)numThang.Value;
            int nam = (int)numNam.Value;

            dgvLuong.DataSource = repo.GetAllSalaryReport(thang, nam);
            _viewMode = ViewMode.AllMonth;
            btnQuayLai.Visible = false;

            FormatGrid();
        }

        private void btnXemNam_Click(object sender, EventArgs e)
        {
            if (cboNhanVien.SelectedValue == null) return;

            int nhanVienId = Convert.ToInt32(cboNhanVien.SelectedValue);
            int nam = (int)numNam.Value;

            dgvLuong.DataSource = repo.GetAllSalaryByEmployee(nhanVienId, nam);

            _viewMode = ViewMode.EmpYear;
            btnQuayLai.Visible = true;

            FormatGrid();
        }

        private void btnXemThangNhanVien_Click(object sender, EventArgs e)
        {
            if (cboNhanVien.SelectedValue == null) return;

            int nhanVienId = Convert.ToInt32(cboNhanVien.SelectedValue);
            int thang = (int)numThang.Value;
            int nam = (int)numNam.Value;

            dgvLuong.DataSource = repo.GetSalaryByEmployeeMonth(nhanVienId, thang, nam);

            _viewMode = ViewMode.EmpMonth;
            btnQuayLai.Visible = true;

            FormatGrid();
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            LoadAllSalaryByYear();
        }

        // =========================
        // LƯU NHẬP (thêm/cập nhật theo NV + tháng + năm đang chọn)
        // =========================
        private void btnLuuNhap_Click(object sender, EventArgs e)
        {
            if (cboNhanVien.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên.");
                return;
            }

            int nhanVienId = Convert.ToInt32(cboNhanVien.SelectedValue);
            int thang = (int)numThang.Value;
            int nam = (int)numNam.Value;

            try
            {
                // thêm/cập nhật lương theo key NV+Tháng+Năm
                repo.UpsertSalary(nhanVienId, thang, nam,
                    nLuongCoBan.Value, nPhuCap.Value, nThuong.Value, nKhauTru.Value);

                // cập nhật số ngày nghỉ (tạo record chamcong)
               

                MessageBox.Show("Lưu nhập thành công!");
                ReloadCurrentView();
                ClearSelectionAndInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu: " + ex.Message);
            }
        }

        // =========================
        // SỬA (update đúng dòng đang chọn bằng LuongID)
        // =========================
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvLuong.CurrentRow == null)
            {
                MessageBox.Show("Hãy chọn 1 dòng lương trong bảng để sửa.");
                return;
            }

            int luongId = SafeToInt(dgvLuong.CurrentRow.Cells["LuongID"]?.Value, 0);
            int nvId = dgvLuong.Columns.Contains("NhanVienID") ? SafeToInt(dgvLuong.CurrentRow.Cells["NhanVienID"]?.Value, 0) : 0;
            int thang = SafeToInt(dgvLuong.CurrentRow.Cells["Thang"]?.Value, (int)numThang.Value);
            int nam = SafeToInt(dgvLuong.CurrentRow.Cells["Nam"]?.Value, (int)numNam.Value);

            if (luongId <= 0)
            {
                MessageBox.Show("Không lấy được LuongID để sửa.");
                return;
            }
            if (nvId <= 0 && cboNhanVien.SelectedValue != null)
                nvId = Convert.ToInt32(cboNhanVien.SelectedValue);

            try
            {
                repo.UpdateSalaryByLuongId(luongId,
                    nLuongCoBan.Value, nPhuCap.Value, nThuong.Value, nKhauTru.Value);

          

                MessageBox.Show("Sửa thành công!");
                ReloadCurrentView();
                ClearSelectionAndInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvLuong.CurrentRow == null) return;

            if (!dgvLuong.Columns.Contains("LuongID"))
            {
                MessageBox.Show("Không thấy cột LuongID trong bảng.");
                return;
            }

            int luongId = Convert.ToInt32(dgvLuong.CurrentRow.Cells["LuongID"].Value);
            if (MessageBox.Show("Bạn có chắc muốn xoá lương này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                repo.DeleteSalary(luongId);
                MessageBox.Show("Xoá lương thành công!");
                ReloadCurrentView();
                ClearSelectionAndInputs();
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

        // =========================
        // Helpers
        // =========================
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
    }
}
