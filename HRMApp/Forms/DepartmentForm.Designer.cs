namespace HRMApp.Forms
{
    partial class DepartmentForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvDepartments;
        private System.Windows.Forms.TextBox txtTenPhongBan;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.Button btnThem, btnSua, btnXoa;
        private System.Windows.Forms.Label lblTenPhongBan;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.GroupBox groupNhapLieu;
        private System.Windows.Forms.GroupBox groupChucNang;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelMiddle;
        private System.Windows.Forms.Label lblTitle;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvDepartments = new System.Windows.Forms.DataGridView();
            this.txtTenPhongBan = new System.Windows.Forms.TextBox();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.lblTenPhongBan = new System.Windows.Forms.Label();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.groupNhapLieu = new System.Windows.Forms.GroupBox();
            this.groupChucNang = new System.Windows.Forms.GroupBox();
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelMiddle = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartments)).BeginInit();
            this.groupNhapLieu.SuspendLayout();
            this.groupChucNang.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.panelMiddle.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDepartments
            // 
            this.dgvDepartments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDepartments.ColumnHeadersHeight = 29;
            this.dgvDepartments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDepartments.Location = new System.Drawing.Point(0, 170);
            this.dgvDepartments.Name = "dgvDepartments";
            this.dgvDepartments.RowHeadersVisible = false;
            this.dgvDepartments.RowHeadersWidth = 51;
            this.dgvDepartments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDepartments.Size = new System.Drawing.Size(800, 280);
            this.dgvDepartments.TabIndex = 3;
            this.dgvDepartments.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDepartments_CellClick);
            this.dgvDepartments.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDepartments_CellContentClick);
            // 
            // txtTenPhongBan
            // 
            this.txtTenPhongBan.Location = new System.Drawing.Point(140, 32);
            this.txtTenPhongBan.Name = "txtTenPhongBan";
            this.txtTenPhongBan.Size = new System.Drawing.Size(220, 30);
            this.txtTenPhongBan.TabIndex = 1;
            // 
            // txtMoTa
            // 
            this.txtMoTa.Location = new System.Drawing.Point(140, 72);
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new System.Drawing.Size(220, 30);
            this.txtMoTa.TabIndex = 3;
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(25, 25);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(100, 28);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(25, 55);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(100, 28);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Sửa";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(25, 85);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(100, 28);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // lblTenPhongBan
            // 
            this.lblTenPhongBan.AutoSize = true;
            this.lblTenPhongBan.Location = new System.Drawing.Point(20, 35);
            this.lblTenPhongBan.Name = "lblTenPhongBan";
            this.lblTenPhongBan.Size = new System.Drawing.Size(129, 23);
            this.lblTenPhongBan.TabIndex = 0;
            this.lblTenPhongBan.Text = "Tên phòng ban:";
            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Location = new System.Drawing.Point(20, 75);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(59, 23);
            this.lblMoTa.TabIndex = 2;
            this.lblMoTa.Text = "Mô tả:";
            // 
            // groupNhapLieu
            // 
            this.groupNhapLieu.Controls.Add(this.lblTenPhongBan);
            this.groupNhapLieu.Controls.Add(this.txtTenPhongBan);
            this.groupNhapLieu.Controls.Add(this.lblMoTa);
            this.groupNhapLieu.Controls.Add(this.txtMoTa);
            this.groupNhapLieu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.groupNhapLieu.Location = new System.Drawing.Point(20, 10);
            this.groupNhapLieu.Name = "groupNhapLieu";
            this.groupNhapLieu.Size = new System.Drawing.Size(400, 120);
            this.groupNhapLieu.TabIndex = 1;
            this.groupNhapLieu.TabStop = false;
            this.groupNhapLieu.Text = "Thông tin phòng ban";
            // 
            // groupChucNang
            // 
            this.groupChucNang.Controls.Add(this.btnThem);
            this.groupChucNang.Controls.Add(this.btnSua);
            this.groupChucNang.Controls.Add(this.btnXoa);
            this.groupChucNang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.groupChucNang.Location = new System.Drawing.Point(440, 10);
            this.groupChucNang.Name = "groupChucNang";
            this.groupChucNang.Size = new System.Drawing.Size(150, 120);
            this.groupChucNang.TabIndex = 2;
            this.groupChucNang.TabStop = false;
            this.groupChucNang.Text = "Chức năng";
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.SteelBlue;
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(800, 50);
            this.panelTop.TabIndex = 0;
            // 
            // panelMiddle
            // 
            this.panelMiddle.Controls.Add(this.groupNhapLieu);
            this.panelMiddle.Controls.Add(this.groupChucNang);
            this.panelMiddle.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMiddle.Height = 170;
            this.panelMiddle.Location = new System.Drawing.Point(0, 50);
            this.panelMiddle.Name = "panelMiddle";
            this.panelMiddle.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(15, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(233, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Quản lý Phòng ban";
            // 
            // DepartmentForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvDepartments);
            this.Controls.Add(this.panelMiddle);
            this.Controls.Add(this.panelTop);
            this.Name = "DepartmentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý Phòng ban";
            this.Load += new System.EventHandler(this.DepartmentForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartments)).EndInit();
            this.groupNhapLieu.ResumeLayout(false);
            this.groupNhapLieu.PerformLayout();
            this.groupChucNang.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelMiddle.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
