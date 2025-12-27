namespace HRMApp.Forms
{
    partial class AccountForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtTenDangNhap;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Button btnSave;

        private void InitializeComponent()
        {
            this.txtTenDangNhap = new System.Windows.Forms.TextBox();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtTenDangNhap
            // 
            this.txtTenDangNhap.Location = new System.Drawing.Point(30, 30);
            this.txtTenDangNhap.Size = new System.Drawing.Size(200, 22);
            // 
            // txtHoTen
            // 
            this.txtHoTen.Location = new System.Drawing.Point(30, 70);
            this.txtHoTen.Size = new System.Drawing.Size(200, 22);
            // 
            // btnSave
            // 
            this.btnSave.Text = "Lưu";
            this.btnSave.Location = new System.Drawing.Point(30, 110);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // AccountForm
            // 
            this.ClientSize = new System.Drawing.Size(300, 180);
            this.Controls.Add(this.txtTenDangNhap);
            this.Controls.Add(this.txtHoTen);
            this.Controls.Add(this.btnSave);
            this.Text = "Thông tin tài khoản";
            this.Load += new System.EventHandler(this.AccountForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
