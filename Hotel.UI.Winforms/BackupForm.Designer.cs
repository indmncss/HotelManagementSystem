// BackupForm.Designer.cs
namespace Hotel.UI.WinForms
{
    partial class BackupForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnBackupNow;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.ListBox lstBackups;
        private System.Windows.Forms.Button btnRefreshList;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblFolder = new System.Windows.Forms.Label();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnBackupNow = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.lstBackups = new System.Windows.Forms.ListBox();
            this.btnRefreshList = new System.Windows.Forms.Button();

            this.SuspendLayout();
            // 
            this.lblFolder.AutoSize = true; this.lblFolder.Location = new System.Drawing.Point(12, 12); this.lblFolder.Text = "Backup Folder";
            this.txtFolder.Location = new System.Drawing.Point(12, 32); this.txtFolder.Width = 480;
            this.btnBrowse.Location = new System.Drawing.Point(500, 30); this.btnBrowse.Text = "Browse"; this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            this.btnBackupNow.Location = new System.Drawing.Point(12, 68); this.btnBackupNow.Text = "Backup Now"; this.btnBackupNow.Click += new System.EventHandler(this.btnBackupNow_Click);
            this.btnRefreshList.Location = new System.Drawing.Point(120, 68); this.btnRefreshList.Text = "Refresh List"; this.btnRefreshList.Click += new System.EventHandler(this.btnRefreshList_Click);
            this.lstBackups.Location = new System.Drawing.Point(12, 108); this.lstBackups.Size = new System.Drawing.Size(560, 240);
            this.btnRestore.Location = new System.Drawing.Point(12, 358); this.btnRestore.Text = "Restore Selected"; this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);

            this.ClientSize = new System.Drawing.Size(590, 400);
            this.Controls.Add(this.lblFolder);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.btnBackupNow);
            this.Controls.Add(this.btnRefreshList);
            this.Controls.Add(this.lstBackups);
            this.Controls.Add(this.btnRestore);
            this.Text = "Database Backup & Restore";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
