namespace HRMApp.Forms
{
    partial class ReportForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.ComboBox cbYear;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cbReportType;
        private System.Windows.Forms.ComboBox cbMonth;
        private System.Windows.Forms.ComboBox cbQuarter;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.Label lblQuarter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdPhongBan;
        private System.Windows.Forms.RadioButton rdGioiTinh;
        private System.Windows.Forms.RadioButton rdDoTuoi;
        private System.Windows.Forms.RadioButton rdNamLamViec;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnExportPdf;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartReport;
        private System.Windows.Forms.DataGridView dgvReport;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnViewMonth;
        private System.Windows.Forms.Button btnViewQuarter;
        private System.Windows.Forms.Button btnViewYear;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lblYear = new System.Windows.Forms.Label();
            this.cbYear = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cbReportType = new System.Windows.Forms.ComboBox();
            this.lblMonth = new System.Windows.Forms.Label();
            this.cbMonth = new System.Windows.Forms.ComboBox();
            this.lblQuarter = new System.Windows.Forms.Label();
            this.cbQuarter = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdPhongBan = new System.Windows.Forms.RadioButton();
            this.rdGioiTinh = new System.Windows.Forms.RadioButton();
            this.rdDoTuoi = new System.Windows.Forms.RadioButton();
            this.rdNamLamViec = new System.Windows.Forms.RadioButton();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnExportPdf = new System.Windows.Forms.Button();
            this.chartReport = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnViewMonth = new System.Windows.Forms.Button();
            this.btnViewQuarter = new System.Windows.Forms.Button();
            this.btnViewYear = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(20, 20);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(70, 16);
            this.lblYear.TabIndex = 0;
            this.lblYear.Text = "Chọn năm:";
            // 
            // cbYear
            // 
            this.cbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbYear.Location = new System.Drawing.Point(100, 18);
            this.cbYear.Name = "cbYear";
            this.cbYear.Size = new System.Drawing.Size(80, 24);
            this.cbYear.TabIndex = 1;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(200, 20);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(89, 16);
            this.lblType.TabIndex = 2;
            this.lblType.Text = "Loại báo cáo:";
            // 
            // cbReportType
            // 
            this.cbReportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReportType.Items.AddRange(new object[] {
            "Theo tháng",
            "Theo quý",
            "Theo năm"});
            this.cbReportType.Location = new System.Drawing.Point(300, 18);
            this.cbReportType.Name = "cbReportType";
            this.cbReportType.Size = new System.Drawing.Size(100, 24);
            this.cbReportType.TabIndex = 3;
            this.cbReportType.SelectedIndexChanged += new System.EventHandler(this.cbReportType_SelectedIndexChanged);
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Location = new System.Drawing.Point(420, 20);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(49, 16);
            this.lblMonth.TabIndex = 4;
            this.lblMonth.Text = "Tháng:";
            this.lblMonth.Visible = false;
            // 
            // cbMonth
            // 
            this.cbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMonth.Items.AddRange(new object[] {
            "1","2","3","4","5","6","7","8","9","10","11","12"});
            this.cbMonth.Location = new System.Drawing.Point(470, 18);
            this.cbMonth.Name = "cbMonth";
            this.cbMonth.Size = new System.Drawing.Size(60, 24);
            this.cbMonth.TabIndex = 5;
            this.cbMonth.Visible = false;
            // 
            // lblQuarter
            // 
            this.lblQuarter.AutoSize = true;
            this.lblQuarter.Location = new System.Drawing.Point(420, 20);
            this.lblQuarter.Name = "lblQuarter";
            this.lblQuarter.Size = new System.Drawing.Size(34, 16);
            this.lblQuarter.TabIndex = 6;
            this.lblQuarter.Text = "Quý:";
            this.lblQuarter.Visible = false;
            // 
            // cbQuarter
            // 
            this.cbQuarter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbQuarter.Items.AddRange(new object[] { "1", "2", "3", "4" });
            this.cbQuarter.Location = new System.Drawing.Point(470, 18);
            this.cbQuarter.Name = "cbQuarter";
            this.cbQuarter.Size = new System.Drawing.Size(60, 24);
            this.cbQuarter.TabIndex = 7;
            this.cbQuarter.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdPhongBan);
            this.groupBox1.Controls.Add(this.rdGioiTinh);
            this.groupBox1.Controls.Add(this.rdDoTuoi);
            this.groupBox1.Controls.Add(this.rdNamLamViec);
            this.groupBox1.Location = new System.Drawing.Point(20, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(515, 60);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thống kê theo";
            // 
            // rdPhongBan
            // 
            this.rdPhongBan.Location = new System.Drawing.Point(20, 25);
            this.rdPhongBan.Name = "rdPhongBan";
            this.rdPhongBan.Size = new System.Drawing.Size(104, 24);
            this.rdPhongBan.TabIndex = 0;
            this.rdPhongBan.Text = "Phòng ban";
            // 
            // rdGioiTinh
            // 
            this.rdGioiTinh.Location = new System.Drawing.Point(140, 25);
            this.rdGioiTinh.Name = "rdGioiTinh";
            this.rdGioiTinh.Size = new System.Drawing.Size(104, 24);
            this.rdGioiTinh.TabIndex = 1;
            this.rdGioiTinh.Text = "Giới tính";
            // 
            // rdDoTuoi
            // 
            this.rdDoTuoi.Location = new System.Drawing.Point(260, 25);
            this.rdDoTuoi.Name = "rdDoTuoi";
            this.rdDoTuoi.Size = new System.Drawing.Size(104, 24);
            this.rdDoTuoi.TabIndex = 2;
            this.rdDoTuoi.Text = "Độ tuổi";
            // 
            // rdNamLamViec
            // 
            this.rdNamLamViec.Location = new System.Drawing.Point(370, 25);
            this.rdNamLamViec.Name = "rdNamLamViec";
            this.rdNamLamViec.Size = new System.Drawing.Size(130, 24);
            this.rdNamLamViec.TabIndex = 3;
            this.rdNamLamViec.Text = "Năm làm việc";
            // 
            // btnThongKe
            // 
            this.btnThongKe.Location = new System.Drawing.Point(560, 18);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(75, 23);
            this.btnThongKe.TabIndex = 9;
            this.btnThongKe.Text = "Thống kê";
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Location = new System.Drawing.Point(650, 18);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(85, 23);
            this.btnExportExcel.TabIndex = 10;
            this.btnExportExcel.Text = "Xuất Excel";
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnExportPdf
            // 
            this.btnExportPdf.Location = new System.Drawing.Point(750, 18);
            this.btnExportPdf.Name = "btnExportPdf";
            this.btnExportPdf.Size = new System.Drawing.Size(75, 23);
            this.btnExportPdf.TabIndex = 11;
            this.btnExportPdf.Text = "Xuất PDF";
            this.btnExportPdf.Click += new System.EventHandler(this.btnExportPdf_Click);
            // 
            // chartReport
            // 
            chartArea1.Name = "ChartArea1";
            this.chartReport.ChartAreas.Add(chartArea1);
            this.chartReport.Location = new System.Drawing.Point(20, 180);
            this.chartReport.Name = "chartReport";
            series1.ChartArea = "ChartArea1";
            series1.Name = "Default";
            this.chartReport.Series.Add(series1);
            this.chartReport.Size = new System.Drawing.Size(800, 360);
            this.chartReport.TabIndex = 12;
            this.chartReport.Visible = true;
            // 
            // dgvReport
            // 
            this.dgvReport.Location = new System.Drawing.Point(20, 180);
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.Size = new System.Drawing.Size(800, 360);
            this.dgvReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReport.TabIndex = 13;
            this.dgvReport.Visible = false;
            this.dgvReport.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReport_CellClick);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(20, 150);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(80, 25);
            this.btnBack.TabIndex = 14;
            this.btnBack.Text = "Quay lại";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Visible = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnViewMonth
            // 
            this.btnViewMonth.Location = new System.Drawing.Point(120, 150);
            this.btnViewMonth.Name = "btnViewMonth";
            this.btnViewMonth.Size = new System.Drawing.Size(120, 25);
            this.btnViewMonth.TabIndex = 15;
            this.btnViewMonth.Text = "Xem theo tháng";
            this.btnViewMonth.UseVisualStyleBackColor = true;
            this.btnViewMonth.Visible = false;
            this.btnViewMonth.Click += new System.EventHandler(this.btnViewMonth_Click);
            // 
            // btnViewQuarter
            // 
            this.btnViewQuarter.Location = new System.Drawing.Point(250, 150);
            this.btnViewQuarter.Name = "btnViewQuarter";
            this.btnViewQuarter.Size = new System.Drawing.Size(120, 25);
            this.btnViewQuarter.TabIndex = 16;
            this.btnViewQuarter.Text = "Xem theo quý";
            this.btnViewQuarter.UseVisualStyleBackColor = true;
            this.btnViewQuarter.Visible = false;
            this.btnViewQuarter.Click += new System.EventHandler(this.btnViewQuarter_Click);
            // 
            // btnViewYear
            // 
            this.btnViewYear.Location = new System.Drawing.Point(380, 150);
            this.btnViewYear.Name = "btnViewYear";
            this.btnViewYear.Size = new System.Drawing.Size(120, 25);
            this.btnViewYear.TabIndex = 17;
            this.btnViewYear.Text = "Xem theo năm";
            this.btnViewYear.UseVisualStyleBackColor = true;
            this.btnViewYear.Visible = false;
            this.btnViewYear.Click += new System.EventHandler(this.btnViewYear_Click);
            // 
            // ReportForm
            // 
            this.ClientSize = new System.Drawing.Size(860, 580);
            this.Controls.Add(this.btnViewYear);
            this.Controls.Add(this.btnViewQuarter);
            this.Controls.Add(this.btnViewMonth);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.cbYear);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.cbReportType);
            this.Controls.Add(this.lblMonth);
            this.Controls.Add(this.cbMonth);
            this.Controls.Add(this.lblQuarter);
            this.Controls.Add(this.cbQuarter);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnThongKe);
            this.Controls.Add(this.btnExportExcel);
            this.Controls.Add(this.btnExportPdf);
            this.Controls.Add(this.chartReport);
            this.Controls.Add(this.dgvReport);
            this.Name = "ReportForm";
            this.Text = "Báo cáo nhân sự";
            this.Load += new System.EventHandler(this.ReportForm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
