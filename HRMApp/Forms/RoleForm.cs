using System;
using System.Data;
using System.Windows.Forms;
using HRMApp.Repositories;

namespace HRMApp.Forms
{
    public partial class RoleForm : Form
    {
        private RoleRepository repo = new RoleRepository();
        private int selectedId = -1;

        public RoleForm()
        {
            InitializeComponent();
        }

        private void RoleForm_Load(object sender, EventArgs e)
        {
            LoadRoles();
        }

        private void LoadRoles()
        {
            DataTable dt = repo.GetAllRoles();
            dgvRoles.DataSource = dt;
        }

        private void dgvRoles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedId = Convert.ToInt32(dgvRoles.Rows[e.RowIndex].Cells["VaiTroID"].Value);
                txtTenVaiTro.Text = dgvRoles.Rows[e.RowIndex].Cells["TenVaiTro"].Value.ToString();
                txtMoTa.Text = dgvRoles.Rows[e.RowIndex].Cells["MoTa"].Value.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            repo.AddRole(txtTenVaiTro.Text, txtMoTa.Text);
            LoadRoles();
            ClearInput();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (selectedId > 0)
            {
                repo.UpdateRole(selectedId, txtTenVaiTro.Text, txtMoTa.Text);
                LoadRoles();
                ClearInput();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn vai trò để sửa");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (selectedId > 0)
            {
                repo.DeleteRole(selectedId);
                LoadRoles();
                ClearInput();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn vai trò để xoá");
            }
        }

        private void ClearInput()
        {
            txtTenVaiTro.Text = "";
            txtMoTa.Text = "";
            selectedId = -1;
        }

        private void dgvRoles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
