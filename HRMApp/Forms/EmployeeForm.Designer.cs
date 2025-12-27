namespace HRMApp.Forms
{
    partial class EmployeeForm
    {
        private System.ComponentModel.IContainer components = null;

        // Khai báo control
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblHeader;

        private System.Windows.Forms.DataGridView dgvEmployees;
        private System.Windows.Forms.TextBox txtHoTen, txtSoDienThoai, txtEmail, txtDiaChi, txtTimKiem;
        private System.Windows.Forms.DateTimePicker dtpNgaySinh, dtpNgayVaoLam;
        private System.Windows.Forms.ComboBox cboGioiTinh, cboPhongBan, cboVaiTro;
        private System.Windows.Forms.ComboBox cboTimPhongBan, cboTimVaiTro;
        private System.Windows.Forms.Button btnThem, btnSua, btnXoa, btnTimKiem, btnImportExcel, btnExportPdf;

        private System.Windows.Forms.Label lblHoTen, lblNgaySinh, lblGioiTinh, lblSoDienThoai, lblEmail,
            lblDiaChi, lblNgayVaoLam, lblPhongBan, lblVaiTro;

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelSearch;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            // ===== panelHeader =====
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.panelHeader.BackColor = System.Drawing.Color.RoyalBlue;
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Height = 50;
            this.panelHeader.Controls.Add(this.lblHeader);

            this.lblHeader.Text = "Quản lý Nhân viên";
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeader.AutoSize = true;
            this.lblHeader.Location = new System.Drawing.Point(20, 10);

            // ===== panelTop =====
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height = 250;
            this.panelTop.Padding = new System.Windows.Forms.Padding(10);

            // Các label + input
            this.lblHoTen = new System.Windows.Forms.Label();
            this.lblHoTen.Text = "Họ tên:";
            this.lblHoTen.Location = new System.Drawing.Point(20, 20);

            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.txtHoTen.Location = new System.Drawing.Point(120, 20);
            this.txtHoTen.Size = new System.Drawing.Size(200, 22);

            this.lblNgaySinh = new System.Windows.Forms.Label();
            this.lblNgaySinh.Text = "Ngày sinh:";
            this.lblNgaySinh.Location = new System.Drawing.Point(20, 50);

            this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.dtpNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgaySinh.Location = new System.Drawing.Point(120, 50);

            this.lblGioiTinh = new System.Windows.Forms.Label();
            this.lblGioiTinh.Text = "Giới tính:";
            this.lblGioiTinh.Location = new System.Drawing.Point(20, 80);

            this.cboGioiTinh = new System.Windows.Forms.ComboBox();
            this.cboGioiTinh.Location = new System.Drawing.Point(120, 80);
            this.cboGioiTinh.Size = new System.Drawing.Size(121, 24);

            this.lblSoDienThoai = new System.Windows.Forms.Label();
            this.lblSoDienThoai.Text = "SĐT:";
            this.lblSoDienThoai.Location = new System.Drawing.Point(20, 110);

            this.txtSoDienThoai = new System.Windows.Forms.TextBox();
            this.txtSoDienThoai.Location = new System.Drawing.Point(120, 110);
            this.txtSoDienThoai.Size = new System.Drawing.Size(200, 22);

            this.lblEmail = new System.Windows.Forms.Label();
            this.lblEmail.Text = "Email:";
            this.lblEmail.Location = new System.Drawing.Point(20, 140);

            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtEmail.Location = new System.Drawing.Point(120, 140);
            this.txtEmail.Size = new System.Drawing.Size(200, 22);

            this.lblDiaChi = new System.Windows.Forms.Label();
            this.lblDiaChi.Text = "Địa chỉ:";
            this.lblDiaChi.Location = new System.Drawing.Point(20, 170);

            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.txtDiaChi.Location = new System.Drawing.Point(120, 170);
            this.txtDiaChi.Size = new System.Drawing.Size(200, 22);

            this.lblNgayVaoLam = new System.Windows.Forms.Label();
            this.lblNgayVaoLam.Text = "Ngày vào làm:";
            this.lblNgayVaoLam.Location = new System.Drawing.Point(20, 200);

            this.dtpNgayVaoLam = new System.Windows.Forms.DateTimePicker();
            this.dtpNgayVaoLam.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayVaoLam.Location = new System.Drawing.Point(120, 200);

            this.lblPhongBan = new System.Windows.Forms.Label();
            this.lblPhongBan.Text = "Phòng ban:";
            this.lblPhongBan.Location = new System.Drawing.Point(340, 20);

            this.cboPhongBan = new System.Windows.Forms.ComboBox();
            this.cboPhongBan.Location = new System.Drawing.Point(440, 20);

            this.lblVaiTro = new System.Windows.Forms.Label();
            this.lblVaiTro.Text = "Vai trò:";
            this.lblVaiTro.Location = new System.Drawing.Point(340, 50);

            this.cboVaiTro = new System.Windows.Forms.ComboBox();
            this.cboVaiTro.Location = new System.Drawing.Point(440, 50);

            this.btnThem = new System.Windows.Forms.Button();
            this.btnThem.Text = "Thêm";
            this.btnThem.Location = new System.Drawing.Point(650, 20);
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);

            this.btnSua = new System.Windows.Forms.Button();
            this.btnSua.Text = "Sửa";
            this.btnSua.Location = new System.Drawing.Point(650, 50);
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);

            this.btnXoa = new System.Windows.Forms.Button();
            this.btnXoa.Text = "Xoá";
            this.btnXoa.Location = new System.Drawing.Point(650, 80);
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);

            this.btnImportExcel = new System.Windows.Forms.Button();
            this.btnImportExcel.Text = "Import Excel";
            this.btnImportExcel.Location = new System.Drawing.Point(650, 120);
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);

            this.btnExportPdf = new System.Windows.Forms.Button();
            this.btnExportPdf.Text = "Xuất PDF";
            this.btnExportPdf.Location = new System.Drawing.Point(650, 150);
            this.btnExportPdf.Click += new System.EventHandler(this.btnExportPdf_Click);

            this.panelTop.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblHoTen, this.txtHoTen, this.lblNgaySinh, this.dtpNgaySinh,
                this.lblGioiTinh, this.cboGioiTinh, this.lblSoDienThoai, this.txtSoDienThoai,
                this.lblEmail, this.txtEmail, this.lblDiaChi, this.txtDiaChi,
                this.lblNgayVaoLam, this.dtpNgayVaoLam, this.lblPhongBan, this.cboPhongBan,
                this.lblVaiTro, this.cboVaiTro, 
                this.btnThem, this.btnSua, this.btnXoa, this.btnImportExcel, this.btnExportPdf
            });

            // ===== panelSearch =====
            this.panelSearch = new System.Windows.Forms.Panel();
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Height = 50;
            this.panelSearch.Padding = new System.Windows.Forms.Padding(10);

            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.txtTimKiem.Location = new System.Drawing.Point(20, 15);

            this.cboTimPhongBan = new System.Windows.Forms.ComboBox();
            this.cboTimPhongBan.Location = new System.Drawing.Point(240, 15);

            this.cboTimVaiTro = new System.Windows.Forms.ComboBox();
            this.cboTimVaiTro.Location = new System.Drawing.Point(400, 15);

            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.Location = new System.Drawing.Point(560, 15);
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);

            this.panelSearch.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.txtTimKiem, this.cboTimPhongBan, this.cboTimVaiTro, this.btnTimKiem
            });

            // ===== dgvEmployees =====
            this.dgvEmployees = new System.Windows.Forms.DataGridView();
            this.dgvEmployees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEmployees.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEmployees.RowHeadersVisible = false;
            this.dgvEmployees.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmployees_CellClick);

            // ===== Form =====
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.dgvEmployees);
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelHeader);
            this.Name = "EmployeeForm";
            this.Text = "Quản lý Nhân viên";
            this.Load += new System.EventHandler(this.EmployeeForm_Load);
        }
    }
}
