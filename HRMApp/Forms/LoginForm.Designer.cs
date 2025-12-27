namespace HRMApp.Forms
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(30, 144, 255); // xanh dương
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Height = 60;
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = false;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Text = "HỆ THỐNG QUẢN LÝ NHÂN SỰ";
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            // 
            // lblUsername
            // 
            this.lblUsername.Text = "Tài khoản:";
            this.lblUsername.Location = new System.Drawing.Point(50, 90);
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 10F);
            // 
            // lblPassword
            // 
            this.lblPassword.Text = "Mật khẩu:";
            this.lblPassword.Location = new System.Drawing.Point(50, 140);
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(150, 85);
            this.txtUsername.Size = new System.Drawing.Size(220, 25);
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 10F);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(150, 135);
            this.txtPassword.Size = new System.Drawing.Size(220, 25);
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.Location = new System.Drawing.Point(150, 190);
            this.btnLogin.Size = new System.Drawing.Size(100, 35);
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(30, 144, 255);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Location = new System.Drawing.Point(270, 190);
            this.btnThoat.Size = new System.Drawing.Size(100, 35);
            this.btnThoat.BackColor = System.Drawing.Color.LightGray;
            this.btnThoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // LoginForm
            // 
            this.AcceptButton = this.btnLogin;
            this.ClientSize = new System.Drawing.Size(450, 270);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnThoat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập hệ thống";
            this.panelHeader.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
