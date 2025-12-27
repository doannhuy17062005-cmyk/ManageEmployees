using System;

namespace HRMApp.Forms
{
    partial class RoleForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox groupRoleInfo;
        private System.Windows.Forms.Label lblTenVaiTro;
        private System.Windows.Forms.TextBox txtTenVaiTro;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.DataGridView dgvRoles;

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
            this.groupRoleInfo = new System.Windows.Forms.GroupBox();
            this.lblTenVaiTro = new System.Windows.Forms.Label();
            this.txtTenVaiTro = new System.Windows.Forms.TextBox();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.dgvRoles = new System.Windows.Forms.DataGridView();

            this.panelTop.SuspendLayout();
            this.groupRoleInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoles)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.SteelBlue;
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(600, 50);
            this.panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(15, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(184, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Quản lý Vai trò";
            // 
            // groupRoleInfo
            // 
            this.groupRoleInfo.Controls.Add(this.lblTenVaiTro);
            this.groupRoleInfo.Controls.Add(this.txtTenVaiTro);
            this.groupRoleInfo.Controls.Add(this.lblMoTa);
            this.groupRoleInfo.Controls.Add(this.txtMoTa);
            this.groupRoleInfo.Controls.Add(this.btnThem);
            this.groupRoleInfo.Controls.Add(this.btnSua);
            this.groupRoleInfo.Controls.Add(this.btnXoa);
            this.groupRoleInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupRoleInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.groupRoleInfo.Location = new System.Drawing.Point(0, 50);
            this.groupRoleInfo.Name = "groupRoleInfo";
            this.groupRoleInfo.Size = new System.Drawing.Size(600, 110);
            this.groupRoleInfo.TabIndex = 1;
            this.groupRoleInfo.TabStop = false;
            this.groupRoleInfo.Text = "Thông tin vai trò";
            // 
            // lblTenVaiTro
            // 
            this.lblTenVaiTro.AutoSize = true;
            this.lblTenVaiTro.Location = new System.Drawing.Point(20, 35);
            this.lblTenVaiTro.Name = "lblTenVaiTro";
            this.lblTenVaiTro.Size = new System.Drawing.Size(93, 23);
            this.lblTenVaiTro.TabIndex = 0;
            this.lblTenVaiTro.Text = "Tên vai trò:";
            // 
            // txtTenVaiTro
            // 
            this.txtTenVaiTro.Location = new System.Drawing.Point(120, 32);
            this.txtTenVaiTro.Name = "txtTenVaiTro";
            this.txtTenVaiTro.Size = new System.Drawing.Size(200, 30);
            this.txtTenVaiTro.TabIndex = 1;
            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Location = new System.Drawing.Point(20, 70);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(59, 23);
            this.lblMoTa.TabIndex = 2;
            this.lblMoTa.Text = "Mô tả:";
            // 
            // txtMoTa
            // 
            this.txtMoTa.Location = new System.Drawing.Point(120, 67);
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new System.Drawing.Size(200, 30);
            this.txtMoTa.TabIndex = 3;
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(350, 28);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(80, 28);
            this.btnThem.TabIndex = 4;
            this.btnThem.Text = "Thêm";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(450, 28);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(80, 28);
            this.btnSua.TabIndex = 5;
            this.btnSua.Text = "Sửa";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(400, 67);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(80, 28);
            this.btnXoa.TabIndex = 6;
            this.btnXoa.Text = "Xoá";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // dgvRoles
            // 
            this.dgvRoles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRoles.ColumnHeadersHeight = 29;
            this.dgvRoles.Dock = System.Windows.Forms.DockStyle.Fill;   // ⬅ Fill toàn bộ khoảng trống còn lại
            this.dgvRoles.Location = new System.Drawing.Point(0, 160);
            this.dgvRoles.Name = "dgvRoles";
            this.dgvRoles.RowHeadersVisible = false;
            this.dgvRoles.RowHeadersWidth = 51;
            this.dgvRoles.Size = new System.Drawing.Size(600, 240);
            this.dgvRoles.TabIndex = 2;
            this.dgvRoles.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRoles_CellClick);
            this.dgvRoles.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRoles_CellContentClick);
            // 
            // RoleForm
            // 
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.dgvRoles);
            this.Controls.Add(this.groupRoleInfo);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = true;
            this.Name = "RoleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Vai trò";
            this.Load += new System.EventHandler(this.RoleForm_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.groupRoleInfo.ResumeLayout(false);
            this.groupRoleInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoles)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
