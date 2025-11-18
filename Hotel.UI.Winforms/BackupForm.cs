// BackupForm.cs
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace Hotel.UI.WinForms
{
    public partial class BackupForm : Form
    {
        public BackupForm()
        {
            InitializeComponent();
            // default folder under user's Documents
            var docs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var defaultFolder = Path.Combine(docs, "HotelDBBackups");
            txtFolder.Text = defaultFolder;
            Directory.CreateDirectory(defaultFolder);
            RefreshBackupList();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.SelectedPath = txtFolder.Text;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtFolder.Text = dlg.SelectedPath;
                    Directory.CreateDirectory(txtFolder.Text);
                    RefreshBackupList();
                }
            }
        }

        private void RefreshBackupList()
        {
            lstBackups.Items.Clear();
            try
            {
                if (!Directory.Exists(txtFolder.Text)) return;
                var files = Directory.GetFiles(txtFolder.Text, "*.bak");
                Array.Sort(files);
                foreach (var f in files) lstBackups.Items.Add(f);
            }
            catch (Exception ex)
            {
                Hotel.Common.Logger.LogException(ex);
            }
        }

        private void btnRefreshList_Click(object sender, EventArgs e) => RefreshBackupList();

        private void btnBackupNow_Click(object sender, EventArgs e)
        {
            try
            {
                var folder = txtFolder.Text;
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
                var filename = Path.Combine(folder, $"HotelDB_backup_{DateTime.Now:yyyyMMdd_HHmmss}.bak");

                // Use master connection to run backup
                var connStr = ConfigurationManager.ConnectionStrings["HotelDb"].ConnectionString;
                // replace Initial Catalog to master so we can run BACKUP
                var builder = new SqlConnectionStringBuilder(connStr) { InitialCatalog = "master" };
                using (var cn = new SqlConnection(builder.ConnectionString))
                using (var cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.CommandText = $"BACKUP DATABASE [HotelDB] TO DISK = @file WITH INIT, FORMAT";
                    cmd.Parameters.AddWithValue("@file", filename);
                    cmd.CommandTimeout = 600; // long timeout
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }

                MessageBox.Show($"Backup created:\r\n{filename}", "Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshBackupList();
            }
            catch (Exception ex)
            {
                Hotel.Common.Logger.LogException(ex);
                MessageBox.Show($"Backup failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (lstBackups.SelectedItem == null) { MessageBox.Show("Select a backup file first."); return; }
            var file = lstBackups.SelectedItem.ToString();
            var confirm = MessageBox.Show($"Restore database from:\r\n{file}\r\n\nThis will overwrite the current HotelDB. Continue?", "Confirm Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            try
            {
                var connStr = ConfigurationManager.ConnectionStrings["HotelDb"].ConnectionString;
                var builder = new SqlConnectionStringBuilder(connStr) { InitialCatalog = "master" };

                using (var cn = new SqlConnection(builder.ConnectionString))
                using (var cmd = cn.CreateCommand())
                {
                    cn.Open();
                    // set DB to single_user to force disconnects, then restore with REPLACE
                    cmd.CommandText = $@"
ALTER DATABASE [HotelDB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
RESTORE DATABASE [HotelDB] FROM DISK = @file WITH REPLACE;
ALTER DATABASE [HotelDB] SET MULTI_USER;
";
                    cmd.Parameters.AddWithValue("@file", file);
                    cmd.CommandTimeout = 600;
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }

                MessageBox.Show("Restore completed. Please restart the application to refresh connections.", "Restore", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshBackupList();
            }
            catch (Exception ex)
            {
                Hotel.Common.Logger.LogException(ex);
                MessageBox.Show($"Restore failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
