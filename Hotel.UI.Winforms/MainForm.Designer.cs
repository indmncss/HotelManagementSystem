namespace Hotel.UI.WinForms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Demo Hotel - Main";

            this.dashboardControl1 = new Hotel.UI.WinForms.DashboardControl();
            this.Controls.Add(this.dashboardControl1);
            this.dashboardControl1.Location = new System.Drawing.Point(16, 16);
            this.dashboardControl1.Name = "dashboardControl1";

            this.btnReservations = new System.Windows.Forms.Button();
            this.btnReservations.Location = new System.Drawing.Point(16, 460);
            this.btnReservations.Name = "btnReservations";
            this.btnReservations.Size = new System.Drawing.Size(120, 30);
            this.btnReservations.Text = "Reservations";
            this.btnReservations.UseVisualStyleBackColor = true;
            this.btnReservations.Click += new System.EventHandler(this.btnReservations_Click);
            this.Controls.Add(this.btnReservations);

            this.btnCheckIn = new System.Windows.Forms.Button();
            this.btnCheckIn.Location = new System.Drawing.Point(152, 460);
            this.btnCheckIn.Size = new System.Drawing.Size(120, 30);
            this.btnCheckIn.Text = "Check-In";
            this.btnCheckIn.Click += new System.EventHandler(this.btnCheckIn_Click);
            this.Controls.Add(this.btnCheckIn);

            this.btnCheckOut = new System.Windows.Forms.Button();
            this.btnCheckOut.Location = new System.Drawing.Point(288, 460);
            this.btnCheckOut.Size = new System.Drawing.Size(120, 30);
            this.btnCheckOut.Text = "Check-Out";
            this.btnCheckOut.Click += new System.EventHandler(this.btnCheckOut_Click);
            this.Controls.Add(this.btnCheckOut);

            this.btnReports = new System.Windows.Forms.Button();
            this.btnReports.Location = new System.Drawing.Point(424, 460);
            this.btnReports.Size = new System.Drawing.Size(120, 30);
            this.btnReports.Text = "Reports";
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);
            this.Controls.Add(this.btnReports);

            this.btnBackup = new System.Windows.Forms.Button();
            this.btnBackup.Location = new System.Drawing.Point(560, 460);
            this.btnBackup.Size = new System.Drawing.Size(120, 30);
            this.btnBackup.Text = "Backup/Restore";
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            this.Controls.Add(this.btnBackup);

            this.btnUsers = new System.Windows.Forms.Button();
            this.btnUsers.Location = new System.Drawing.Point(696, 460);
            this.btnUsers.Size = new System.Drawing.Size(120, 30);
            this.btnUsers.Text = "Users";
            this.btnUsers.Click += new System.EventHandler(this.btnUsers_Click);
            this.Controls.Add(this.btnUsers);

            this.btnHousekeeping = new System.Windows.Forms.Button();
            this.btnHousekeeping.Location = new System.Drawing.Point(832, 460); // adjust so it fits, or move others left
            this.btnHousekeeping.Size = new System.Drawing.Size(120, 30);
            this.btnHousekeeping.Text = "Housekeeping";
            this.btnHousekeeping.Click += new System.EventHandler(this.btnHousekeeping_Click);
            this.Controls.Add(this.btnHousekeeping);

            this.ResumeLayout(false);
        }
        private System.Windows.Forms.Button btnReservations;
        private System.Windows.Forms.Button btnCheckIn;
        private System.Windows.Forms.Button btnCheckOut;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Button btnUsers;
        private System.Windows.Forms.Button btnHousekeeping;



        private DashboardControl dashboardControl1;
    }
}
