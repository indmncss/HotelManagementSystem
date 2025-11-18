// UserManagementForm.Designer.cs
namespace Hotel.UI.WinForms
{
    partial class UserManagementForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.Button btnCreateUser;
        private System.Windows.Forms.Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.btnCreateUser = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.SuspendLayout();

            this.dgvUsers.Location = new System.Drawing.Point(12, 12);
            this.dgvUsers.Size = new System.Drawing.Size(760, 360);
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.btnCreateUser.Location = new System.Drawing.Point(12, 384);
            this.btnCreateUser.Size = new System.Drawing.Size(120, 30);
            this.btnCreateUser.Text = "Create User";
            this.btnCreateUser.Click += new System.EventHandler(this.btnCreateUser_Click);

            this.btnClose.Location = new System.Drawing.Point(652, 384);
            this.btnClose.Size = new System.Drawing.Size(120, 30);
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler((s, e) => this.Close());

            this.ClientSize = new System.Drawing.Size(784, 426);
            this.Controls.Add(this.dgvUsers);
            this.Controls.Add(this.btnCreateUser);
            this.Controls.Add(this.btnClose);
            this.Text = "User Management";
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
