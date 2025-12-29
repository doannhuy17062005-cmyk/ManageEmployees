namespace HRMApp.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuNhanVien;
        private System.Windows.Forms.ToolStripMenuItem menuPhongBan;
        private System.Windows.Forms.ToolStripMenuItem menuVaiTro;
        private System.Windows.Forms.ToolStripMenuItem menuChamCong;
        private System.Windows.Forms.ToolStripMenuItem menuLuong;
        private System.Windows.Forms.ToolStripMenuItem menuBaoCao;
        private System.Windows.Forms.ToolStripMenuItem menuTaiKhoan;
        private System.Windows.Forms.ToolStripMenuItem menuThoat;
        private System.Windows.Forms.Panel pnlContent;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuNhanVien = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPhongBan = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVaiTro = new System.Windows.Forms.ToolStripMenuItem();
            this.menuChamCong = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLuong = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBaoCao = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTaiKhoan = new System.Windows.Forms.ToolStripMenuItem();
            this.menuThoat = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNhanVien,
            this.menuPhongBan,
            this.menuVaiTro,
            this.menuChamCong,
            this.menuLuong,
            this.menuBaoCao,
            this.menuTaiKhoan,
            this.menuThoat});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1333, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuNhanVien
            // 
            this.menuNhanVien.Name = "menuNhanVien";
            this.menuNhanVien.Size = new System.Drawing.Size(89, 24);
            this.menuNhanVien.Text = "Nhân viên";
            this.menuNhanVien.Click += new System.EventHandler(this.menuNhanVien_Click);
            // 
            // menuPhongBan
            // 
            this.menuPhongBan.Name = "menuPhongBan";
            this.menuPhongBan.Size = new System.Drawing.Size(94, 24);
            this.menuPhongBan.Text = "Phòng ban";
            this.menuPhongBan.Click += new System.EventHandler(this.menuPhongBan_Click);
            // 
            // menuVaiTro
            // 
            this.menuVaiTro.Name = "menuVaiTro";
            this.menuVaiTro.Size = new System.Drawing.Size(66, 24);
            this.menuVaiTro.Text = "Vai trò";
            this.menuVaiTro.Click += new System.EventHandler(this.menuVaiTro_Click);
            // 
            // menuChamCong
            // 
            this.menuChamCong.Name = "menuChamCong";
            this.menuChamCong.Size = new System.Drawing.Size(98, 24);
            this.menuChamCong.Text = "Chấm công";
            this.menuChamCong.Click += new System.EventHandler(this.menuChamCong_Click);
            // 
            // menuLuong
            // 
            this.menuLuong.Name = "menuLuong";
            this.menuLuong.Size = new System.Drawing.Size(65, 24);
            this.menuLuong.Text = "Lương";
            this.menuLuong.Click += new System.EventHandler(this.menuLuong_Click);
            // 
            
            // 
            // menuTaiKhoan
            // 
            this.menuTaiKhoan.Name = "menuTaiKhoan";
            this.menuTaiKhoan.Size = new System.Drawing.Size(85, 24);
            this.menuTaiKhoan.Text = "Tài khoản";
            this.menuTaiKhoan.Click += new System.EventHandler(this.menuTaiKhoan_Click);
            // 
            // menuThoat
            // 
            this.menuThoat.Name = "menuThoat";
            this.menuThoat.Size = new System.Drawing.Size(61, 24);
            this.menuThoat.Text = "Thoát";
            this.menuThoat.Click += new System.EventHandler(this.menuThoat_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 28);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1333, 710);
            this.pnlContent.TabIndex = 1;
            this.pnlContent.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlContent_Paint);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1333, 738);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.Text = "Hệ thống quản lý nhân sự";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
