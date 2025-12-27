using System;

namespace HRMApp.Forms
{
    partial class SalaryForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox groupSalaryInfo;
        private System.Windows.Forms.ComboBox cboNhanVien;
        private System.Windows.Forms.Label lblNhanVien;
        private System.Windows.Forms.NumericUpDown numThang;
        private System.Windows.Forms.NumericUpDown numNam;
        private System.Windows.Forms.Label lblThang;
        private System.Windows.Forms.Label lblNam;

        private System.Windows.Forms.Button btnXemThang;
        private System.Windows.Forms.Button btnXemNam;
        private System.Windows.Forms.Button btnXemThangNhanVien;
        private System.Windows.Forms.Button btnExportAll;
        private System.Windows.Forms.Button btnExportByEmployee;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnQuayLai;

        private System.Windows.Forms.DataGridView dgvLuong;

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
            this.groupSalaryInfo = new System.Windows.Forms.GroupBox();
            this.lblNhanVien = new System.Windows.Forms.Label();
            this.cboNhanVien = new System.Windows.Forms.ComboBox();
            this.lblThang = new System.Windows.Forms.Label();
            this.numThang = new System.Windows.Forms.NumericUpDown();
            this.lblNam = new System.Windows.Forms.Label();
            this.numNam = new System.Windows.Forms.NumericUpDown();
            this.btnXemThang = new System.Windows.Forms.Button();
            this.btnXemNam = new System.Windows.Forms.Button();
            this.btnXemThangNhanVien = new System.Windows.Forms.Button();
            this.btnExportAll = new System.Windows.Forms.Button();
            this.btnExportByEmployee = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnQuayLai = new System.Windows.Forms.Button();
            this.dgvLuong = new System.Windows.Forms.DataGridView();
            this.panelTop.SuspendLayout();
            this.groupSalaryInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLuong)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.SteelBlue;
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(900, 50);
            this.panelTop.TabIndex = 2;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(15, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(182, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Quản lý Lương";
            // 
            // groupSalaryInfo
            // 
            this.groupSalaryInfo.Controls.Add(this.lblNhanVien);
            this.groupSalaryInfo.Controls.Add(this.cboNhanVien);
            this.groupSalaryInfo.Controls.Add(this.lblThang);
            this.groupSalaryInfo.Controls.Add(this.numThang);
            this.groupSalaryInfo.Controls.Add(this.lblNam);
            this.groupSalaryInfo.Controls.Add(this.numNam);
            this.groupSalaryInfo.Controls.Add(this.btnXemThang);
            this.groupSalaryInfo.Controls.Add(this.btnXemNam);
            this.groupSalaryInfo.Controls.Add(this.btnXemThangNhanVien);
            this.groupSalaryInfo.Controls.Add(this.btnExportAll);
            this.groupSalaryInfo.Controls.Add(this.btnExportByEmployee);
            this.groupSalaryInfo.Controls.Add(this.btnSua);
            this.groupSalaryInfo.Controls.Add(this.btnXoa);
            this.groupSalaryInfo.Controls.Add(this.btnQuayLai);
            this.groupSalaryInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupSalaryInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.groupSalaryInfo.Location = new System.Drawing.Point(0, 50);
            this.groupSalaryInfo.Name = "groupSalaryInfo";
            this.groupSalaryInfo.Size = new System.Drawing.Size(900, 160);
            this.groupSalaryInfo.TabIndex = 1;
            this.groupSalaryInfo.TabStop = false;
            this.groupSalaryInfo.Text = "Thông tin lương";
            // 
            // lblNhanVien
            // 
            this.lblNhanVien.AutoSize = true;
            this.lblNhanVien.Location = new System.Drawing.Point(20, 30);
            this.lblNhanVien.Name = "lblNhanVien";
            this.lblNhanVien.Size = new System.Drawing.Size(92, 23);
            this.lblNhanVien.TabIndex = 0;
            this.lblNhanVien.Text = "Nhân viên:";
            // 
            // cboNhanVien
            // 
            this.cboNhanVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhanVien.Location = new System.Drawing.Point(100, 28);
            this.cboNhanVien.Name = "cboNhanVien";
            this.cboNhanVien.Size = new System.Drawing.Size(180, 31);
            this.cboNhanVien.TabIndex = 1;
            // 
            // lblThang
            // 
            this.lblThang.AutoSize = true;
            this.lblThang.Location = new System.Drawing.Point(20, 70);
            this.lblThang.Name = "lblThang";
            this.lblThang.Size = new System.Drawing.Size(62, 23);
            this.lblThang.TabIndex = 2;
            this.lblThang.Text = "Tháng:";
            // 
            // numThang
            // 
            this.numThang.Location = new System.Drawing.Point(100, 68);
            this.numThang.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numThang.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numThang.Name = "numThang";
            this.numThang.Size = new System.Drawing.Size(80, 30);
            this.numThang.TabIndex = 3;
            this.numThang.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblNam
            // 
            this.lblNam.AutoSize = true;
            this.lblNam.Location = new System.Drawing.Point(200, 70);
            this.lblNam.Name = "lblNam";
            this.lblNam.Size = new System.Drawing.Size(51, 23);
            this.lblNam.TabIndex = 4;
            this.lblNam.Text = "Năm:";
            // 
            // numNam
            // 
            this.numNam.Location = new System.Drawing.Point(250, 68);
            this.numNam.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.numNam.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numNam.Name = "numNam";
            this.numNam.Size = new System.Drawing.Size(100, 30);
            this.numNam.TabIndex = 5;
            this.numNam.Value = new decimal(new int[] {
            2025,
            0,
            0,
            0});
            // 
            // btnXemThang
            // 
            this.btnXemThang.Location = new System.Drawing.Point(380, 25);
            this.btnXemThang.Name = "btnXemThang";
            this.btnXemThang.Size = new System.Drawing.Size(150, 28);
            this.btnXemThang.TabIndex = 6;
            this.btnXemThang.Text = "Xem theo tháng (All)";
            this.btnXemThang.Click += new System.EventHandler(this.btnXemThang_Click);
            // 
            // btnXemNam
            // 
            this.btnXemNam.Location = new System.Drawing.Point(536, 27);
            this.btnXemNam.Name = "btnXemNam";
            this.btnXemNam.Size = new System.Drawing.Size(150, 28);
            this.btnXemNam.TabIndex = 7;
            this.btnXemNam.Text = "Xem theo năm (NV)";
            this.btnXemNam.Click += new System.EventHandler(this.btnXemNam_Click);
            // 
            // btnXemThangNhanVien
            // 
            this.btnXemThangNhanVien.Location = new System.Drawing.Point(380, 65);
            this.btnXemThangNhanVien.Name = "btnXemThangNhanVien";
            this.btnXemThangNhanVien.Size = new System.Drawing.Size(150, 28);
            this.btnXemThangNhanVien.TabIndex = 8;
            this.btnXemThangNhanVien.Text = "Xem theo tháng (NV)";
            this.btnXemThangNhanVien.Click += new System.EventHandler(this.btnXemThangNhanVien_Click);
            // 
            // btnExportAll
            // 
            this.btnExportAll.Location = new System.Drawing.Point(700, 27);
            this.btnExportAll.Name = "btnExportAll";
            this.btnExportAll.Size = new System.Drawing.Size(150, 28);
            this.btnExportAll.TabIndex = 9;
            this.btnExportAll.Text = "Xuất Excel (All)";
            this.btnExportAll.Click += new System.EventHandler(this.btnExportAll_Click);
            // 
            // btnExportByEmployee
            // 
            this.btnExportByEmployee.Location = new System.Drawing.Point(700, 65);
            this.btnExportByEmployee.Name = "btnExportByEmployee";
            this.btnExportByEmployee.Size = new System.Drawing.Size(150, 28);
            this.btnExportByEmployee.TabIndex = 10;
            this.btnExportByEmployee.Text = "Xuất Excel (NV)";
            this.btnExportByEmployee.Click += new System.EventHandler(this.btnExportByEmployee_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(380, 105);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(80, 28);
            this.btnSua.TabIndex = 11;
            this.btnSua.Text = "Sửa";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(470, 105);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(80, 28);
            this.btnXoa.TabIndex = 12;
            this.btnXoa.Text = "Xoá";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnQuayLai
            // 
            this.btnQuayLai.Location = new System.Drawing.Point(560, 105);
            this.btnQuayLai.Name = "btnQuayLai";
            this.btnQuayLai.Size = new System.Drawing.Size(100, 28);
            this.btnQuayLai.TabIndex = 13;
            this.btnQuayLai.Text = "Quay lại";
            this.btnQuayLai.Visible = false;
            this.btnQuayLai.Click += new System.EventHandler(this.btnQuayLai_Click);
            // 
            // dgvLuong
            // 
            this.dgvLuong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLuong.ColumnHeadersHeight = 29;
            this.dgvLuong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLuong.Location = new System.Drawing.Point(0, 210);
            this.dgvLuong.Name = "dgvLuong";
            this.dgvLuong.RowHeadersVisible = false;
            this.dgvLuong.RowHeadersWidth = 51;
            this.dgvLuong.Size = new System.Drawing.Size(900, 390);
            this.dgvLuong.TabIndex = 0;
            // 
            // SalaryForm
            // 
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.dgvLuong);
            this.Controls.Add(this.groupSalaryInfo);
            this.Controls.Add(this.panelTop);
            this.Name = "SalaryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Lương";
            this.Load += new System.EventHandler(this.SalaryForm_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.groupSalaryInfo.ResumeLayout(false);
            this.groupSalaryInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLuong)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
