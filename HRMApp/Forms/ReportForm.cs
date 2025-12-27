using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using OfficeOpenXml; // EPPlus
using OfficeOpenXml.Table;
using iTextSharp.text; // iTextSharp
using iTextSharp.text.pdf;
using HRMApp.Repositories;

namespace HRMApp.Forms
{
    public partial class ReportForm : Form
    {
        private DataTable lastReportData;
        private readonly ReportRepository reportRepo = new ReportRepository();
        private int selectedNhanVienId = -1;

        public ReportForm()
        {
            InitializeComponent();
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            cbReportType.SelectedIndex = 2; // mặc định "Theo năm"
            rdPhongBan.Checked = true;

            for (int y = DateTime.Now.Year; y >= 2000; y--)
                cbYear.Items.Add(y.ToString());
            cbYear.SelectedIndex = 0;

            ToggleDetailButtons(false);
        }

        private void cbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMonth.Visible = cbMonth.Visible = (cbReportType.SelectedIndex == 0);
            lblQuarter.Visible = cbQuarter.Visible = (cbReportType.SelectedIndex == 1);
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            int year = int.Parse(cbYear.Text);
            DataTable dt = null;

            if (rdPhongBan.Checked)
            {
                if (cbReportType.SelectedIndex == 0 && cbMonth.SelectedIndex >= 0)
                    dt = reportRepo.GetReportByMonth(year, cbMonth.SelectedIndex + 1);
                else if (cbReportType.SelectedIndex == 1 && cbQuarter.SelectedIndex >= 0)
                    dt = reportRepo.GetReportByQuarter(year, cbQuarter.SelectedIndex + 1);
                else
                    dt = reportRepo.GetReportByYear(year);
            }
            else if (rdGioiTinh.Checked)
                dt = reportRepo.GetReportByGender(year, GetMonth(), GetQuarter());
            else if (rdDoTuoi.Checked)
                dt = reportRepo.GetReportByAgeGroup(year, GetMonth(), GetQuarter());
            else if (rdNamLamViec.Checked)
                dt = reportRepo.GetReportByWorkingYears(year, GetMonth(), GetQuarter());

            if (dt == null || dt.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu thống kê.");
                return;
            }

            lastReportData = dt;

            if (rdPhongBan.Checked)
                ShowTable(dt);
            else
                DrawChart(dt);
        }

        private int? GetMonth()
            => cbReportType.SelectedIndex == 0 && cbMonth.SelectedIndex >= 0 ? cbMonth.SelectedIndex + 1 : (int?)null;

        private int? GetQuarter()
            => cbReportType.SelectedIndex == 1 && cbQuarter.SelectedIndex >= 0 ? cbQuarter.SelectedIndex + 1 : (int?)null;

        private void ShowTable(DataTable dt)
        {
            chartReport.Visible = false;
            dgvReport.Visible = true;
            dgvReport.DataSource = dt;

            if (dgvReport.Columns.Contains("NhanVienID"))
                dgvReport.Columns["NhanVienID"].Visible = false;

            string[] moneyCols = { "LuongCoBan", "PhuCap", "Thuong", "KhauTru", "ThucLinh" };
            foreach (var col in moneyCols)
                if (dgvReport.Columns.Contains(col))
                {
                    dgvReport.Columns[col].DefaultCellStyle.Format = "N0";
                    dgvReport.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

            dgvReport.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReport.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            if (dgvReport.Columns.Contains("TenPhongBan"))
                dgvReport.Columns["TenPhongBan"].DisplayIndex = 2;
            if (dgvReport.Columns.Contains("TenVaiTro"))
                dgvReport.Columns["TenVaiTro"].DisplayIndex = 3;

            dgvReport.CellClick -= dgvReport_CellClick;
            dgvReport.CellClick += dgvReport_CellClick;
        }

        private void DrawChart(DataTable dt)
        {
            dgvReport.Visible = false;
            chartReport.Visible = true;

            chartReport.Series.Clear();
            chartReport.Legends.Clear();
            EnsureChartArea();

            Series series = new Series("Thống kê")
            {
                IsValueShownAsLabel = true,
                ChartType = (rdGioiTinh.Checked || rdDoTuoi.Checked || rdNamLamViec.Checked)
                    ? SeriesChartType.Column
                    : SeriesChartType.Pie
            };

            foreach (DataRow row in dt.Rows)
                series.Points.AddXY(row[0].ToString(), Convert.ToInt32(row[1]));

            chartReport.Series.Add(series);
            chartReport.Legends.Add(new Legend { Docking = Docking.Right });
        }

        private void EnsureChartArea()
        {
            if (chartReport.ChartAreas.Count == 0)
                chartReport.ChartAreas.Add(new ChartArea("Default"));
        }

        // --- Click vào nhân viên
        private void dgvReport_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvReport.Columns.Contains("NhanVienID"))
            {
                selectedNhanVienId = Convert.ToInt32(dgvReport.Rows[e.RowIndex].Cells["NhanVienID"].Value);
                ToggleDetailButtons(true);
            }
        }

        private void btnViewMonth_Click(object sender, EventArgs e)
        {
            if (selectedNhanVienId < 0) return;
            int year = int.Parse(cbYear.Text);

            DataTable salaryData = reportRepo.GetEmployeeMonthlySalary(selectedNhanVienId, year);

            chartReport.Visible = true;
            dgvReport.Visible = false;
            chartReport.Series.Clear();
            chartReport.Titles.Clear();
            EnsureChartArea();
            chartReport.ChartAreas[0].AxisX.Title = "Tháng";
            chartReport.ChartAreas[0].AxisY.Title = "Thu nhập (VNĐ)";

            Series series = new Series("Lương theo tháng")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true
            };

            for (int thang = 1; thang <= 12; thang++)
            {
                var rows = salaryData.Select("Thang = " + thang);
                decimal thucLinh = rows.Length > 0 ? Convert.ToDecimal(rows[0]["ThucLinh"]) : 0;
                series.Points.AddXY("Tháng " + thang, thucLinh);
            }

            chartReport.Series.Add(series);
            chartReport.Titles.Add("Biểu đồ lương theo tháng - " + year);
        }

        private void btnViewQuarter_Click(object sender, EventArgs e)
        {
            if (selectedNhanVienId < 0) return;
            int year = int.Parse(cbYear.Text);

            chartReport.Visible = true;
            dgvReport.Visible = false;
            chartReport.Series.Clear();
            chartReport.Titles.Clear();
            EnsureChartArea();
            chartReport.ChartAreas[0].AxisX.Title = "Quý";
            chartReport.ChartAreas[0].AxisY.Title = "Thu nhập (VNĐ)";

            Series series = new Series("Lương theo quý")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true
            };

            for (int quy = 1; quy <= 4; quy++)
            {
                DataTable dt = reportRepo.GetEmployeeQuarterlySalary(selectedNhanVienId, year, quy);
                decimal thucLinh = dt.Rows.Count > 0 ? Convert.ToDecimal(dt.Rows[0]["TongThucLinh"]) : 0;
                series.Points.AddXY("Quý " + quy, thucLinh);
            }

            chartReport.Series.Add(series);
            chartReport.Titles.Add("Biểu đồ lương theo quý - " + year);
        }

        private void btnViewYear_Click(object sender, EventArgs e)
        {
            if (selectedNhanVienId < 0) return;
            int year = int.Parse(cbYear.Text);

            DataTable dt = reportRepo.GetEmployeeYearlySalary(selectedNhanVienId, year);

            chartReport.Visible = true;
            dgvReport.Visible = false;
            chartReport.Series.Clear();
            chartReport.Titles.Clear();
            EnsureChartArea();
            chartReport.ChartAreas[0].AxisX.Title = "Năm";
            chartReport.ChartAreas[0].AxisY.Title = "Thu nhập (VNĐ)";

            Series series = new Series("Lương cả năm")
            {
                ChartType = SeriesChartType.Column,
                IsValueShownAsLabel = true
            };

            decimal thucLinh = dt.Rows.Count > 0 ? Convert.ToDecimal(dt.Rows[0]["TongThucLinh"]) : 0;
            series.Points.AddXY(year.ToString(), thucLinh);

            chartReport.Series.Add(series);
            chartReport.Titles.Add("Tổng thu nhập năm " + year);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            chartReport.Visible = false;
            dgvReport.Visible = true;
            ToggleDetailButtons(false);
            selectedNhanVienId = -1;
        }

        private void ToggleDetailButtons(bool visible)
        {
            btnViewMonth.Visible = visible;
            btnViewQuarter.Visible = visible;
            btnViewYear.Visible = visible;
            btnBack.Visible = visible;
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (lastReportData == null)
            {
                MessageBox.Show("Chưa có dữ liệu để xuất.");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx",
                FileName = "Report.xlsx"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (ExcelPackage package = new ExcelPackage())
                    {
                        var ws = package.Workbook.Worksheets.Add("Report");
                        ws.Cells["A1"].LoadFromDataTable(lastReportData, true, TableStyles.Light9);
                        ws.Cells[ws.Dimension.Address].AutoFitColumns();
                        package.SaveAs(new FileInfo(sfd.FileName));
                    }
                    MessageBox.Show("Xuất Excel thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xuất Excel: " + ex.Message);
                }
            }
        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            if (lastReportData == null)
            {
                MessageBox.Show("Chưa có dữ liệu để xuất.");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                FileName = "Report.pdf"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Document doc = new Document(PageSize.A4);
                    PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
                    doc.Open();

                    // Hỗ trợ Unicode tiếng Việt
                    string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                    BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 12);
                    iTextSharp.text.Font fontTitle = new iTextSharp.text.Font(bf, 16, iTextSharp.text.Font.BOLD);

                    Paragraph title = new Paragraph("Báo cáo nhân sự\n\n", fontTitle);
                    title.Alignment = Element.ALIGN_CENTER;
                    doc.Add(title);

                    PdfPTable table = new PdfPTable(lastReportData.Columns.Count);
                    table.WidthPercentage = 100;

                    foreach (DataColumn col in lastReportData.Columns)
                    {
                        table.AddCell(new Phrase(col.ColumnName, fontTitle));
                    }

                    foreach (DataRow row in lastReportData.Rows)
                    {
                        foreach (var item in row.ItemArray)
                        {
                            table.AddCell(new Phrase(item.ToString(), font));
                        }
                    }

                    doc.Add(table);
                    doc.Close();

                    MessageBox.Show("Xuất PDF thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xuất PDF: " + ex.Message);
                }
            }
        }
    }
}
