namespace HRMApp.Forms
{
    partial class AttendanceForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.GroupBox groupBoxInfo;
        private System.Windows.Forms.ComboBox cboNhanVien;
        private System.Windows.Forms.Label lblNhanVien;
        private System.Windows.Forms.DateTimePicker dtpNgay;
        private System.Windows.Forms.Label lblNgay;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnTaiLai;
        private System.Windows.Forms.DataGridView dgvChamCong;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.groupBoxInfo = new System.Windows.Forms.GroupBox();
            this.lblNhanVien = new System.Windows.Forms.Label();
            this.cboNhanVien = new System.Windows.Forms.ComboBox();
            this.lblNgay = new System.Windows.Forms.Label();
            this.dtpNgay = new System.Windows.Forms.DateTimePicker();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnTaiLai = new System.Windows.Forms.Button();
            this.dgvChamCong = new System.Windows.Forms.DataGridView();
            this.panelTop.SuspendLayout();
            this.groupBoxInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChamCong)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.SteelBlue;
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(820, 57);
            this.panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(10, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(196, 28);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Quản lý Chấm công";
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblWelcome.Location = new System.Drawing.Point(12, 60);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(151, 20);
            this.lblWelcome.TabIndex = 1;
            this.lblWelcome.Text = "Chấm công nhân viên";
            // 
            // groupBoxInfo
            // 
            this.groupBoxInfo.Controls.Add(this.lblNhanVien);
            this.groupBoxInfo.Controls.Add(this.cboNhanVien);
            this.groupBoxInfo.Controls.Add(this.lblNgay);
            this.groupBoxInfo.Controls.Add(this.dtpNgay);
            this.groupBoxInfo.Controls.Add(this.lblTrangThai);
            this.groupBoxInfo.Controls.Add(this.cboTrangThai);
            this.groupBoxInfo.Controls.Add(this.btnThem);
            this.groupBoxInfo.Controls.Add(this.btnSua);
            this.groupBoxInfo.Controls.Add(this.btnXoa);
            this.groupBoxInfo.Controls.Add(this.btnTaiLai);
            this.groupBoxInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.groupBoxInfo.Location = new System.Drawing.Point(8, 83);
            this.groupBoxInfo.Name = "groupBoxInfo";
            this.groupBoxInfo.Size = new System.Drawing.Size(800, 116);
            this.groupBoxInfo.TabIndex = 2;
            this.groupBoxInfo.TabStop = false;
            this.groupBoxInfo.Text = "Thông tin chấm công";
            // 
            // lblNhanVien
            // 
            this.lblNhanVien.AutoSize = true;
            this.lblNhanVien.Location = new System.Drawing.Point(15, 28);
            this.lblNhanVien.Name = "lblNhanVien";
            this.lblNhanVien.Size = new System.Drawing.Size(78, 20);
            this.lblNhanVien.TabIndex = 0;
            this.lblNhanVien.Text = "Nhân viên:";
            // 
            // cboNhanVien
            // 
            this.cboNhanVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhanVien.Location = new System.Drawing.Point(90, 25);
            this.cboNhanVien.Name = "cboNhanVien";
            this.cboNhanVien.Size = new System.Drawing.Size(160, 28);
            this.cboNhanVien.TabIndex = 1;
            // 
            // lblNgay
            // 
            this.lblNgay.AutoSize = true;
            this.lblNgay.Location = new System.Drawing.Point(15, 60);
            this.lblNgay.Name = "lblNgay";
            this.lblNgay.Size = new System.Drawing.Size(47, 20);
            this.lblNgay.TabIndex = 2;
            this.lblNgay.Text = "Ngày:";
            // 
            // dtpNgay
            // 
            this.dtpNgay.Location = new System.Drawing.Point(90, 55);
            this.dtpNgay.Name = "dtpNgay";
            this.dtpNgay.Size = new System.Drawing.Size(200, 27);
            this.dtpNgay.TabIndex = 3;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Location = new System.Drawing.Point(280, 28);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(78, 20);
            this.lblTrangThai.TabIndex = 4;
            this.lblTrangThai.Text = "Trạng thái:";
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.Items.AddRange(new object[] {
            "Đi làm",
            "Nghỉ phép",
            "Nghỉ không phép"});
            this.cboTrangThai.Location = new System.Drawing.Point(360, 25);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(130, 28);
            this.cboTrangThai.TabIndex = 5;
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(520, 20);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(80, 25);
            this.btnThem.TabIndex = 6;
            this.btnThem.Text = "Thêm";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(610, 20);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(80, 25);
            this.btnSua.TabIndex = 7;
            this.btnSua.Text = "Sửa";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(700, 20);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(80, 25);
            this.btnXoa.TabIndex = 8;
            this.btnXoa.Text = "Xoá";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnTaiLai
            // 
            this.btnTaiLai.Location = new System.Drawing.Point(520, 55);
            this.btnTaiLai.Name = "btnTaiLai";
            this.btnTaiLai.Size = new System.Drawing.Size(100, 25);
            this.btnTaiLai.TabIndex = 9;
            this.btnTaiLai.Text = "Tải lại";
            this.btnTaiLai.Click += new System.EventHandler(this.btnTaiLai_Click);
            // 
            // dgvChamCong
            // 
            this.dgvChamCong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChamCong.ColumnHeadersHeight = 29;
            this.dgvChamCong.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvChamCong.Location = new System.Drawing.Point(0, 218);
            this.dgvChamCong.Name = "dgvChamCong";
            this.dgvChamCong.RowHeadersVisible = false;
            this.dgvChamCong.RowHeadersWidth = 51;
            this.dgvChamCong.Size = new System.Drawing.Size(820, 232);
            this.dgvChamCong.TabIndex = 3;
            this.dgvChamCong.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChamCong_CellClick);
            // 
            // AttendanceForm
            // 
            this.ClientSize = new System.Drawing.Size(820, 450);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.groupBoxInfo);
            this.Controls.Add(this.dgvChamCong);
            this.Name = "AttendanceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chấm công";
            this.Load += new System.EventHandler(this.AttendanceForm_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.groupBoxInfo.ResumeLayout(false);
            this.groupBoxInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChamCong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
