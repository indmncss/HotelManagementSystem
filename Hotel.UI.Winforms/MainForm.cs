using System;
using System.Windows.Forms;
using Hotel.Data.Entities;

namespace Hotel.UI.WinForms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            ApplyUserContext();
        }

        private void ApplyUserContext()
        {
            if (Program.CurrentUser == null)
            {
                // no user -> disable everything
                btnReservations.Enabled = false;
                btnCheckIn.Enabled = false;
                btnCheckOut.Enabled = false;
                btnReports.Enabled = false;
                btnBackup.Enabled = false;
                btnUsers.Enabled = false;
                btnReports.Enabled = false;
                return;
            }

            // show username, and attempt to read role name (safe null checks)
            var roleName = Program.CurrentUser?.Role?.RoleName ?? "";

            this.Text = $"Demo Hotel - Logged in as {Program.CurrentUser.Username} ({roleName})";

            // Simple RBAC rules:
            // Admin -> full access
            // Manager -> reports + rates, audit logs (we'll treat as Reports + Reservations)
            // FrontDesk/Receptionist -> reservations, check-in, check-out
            // Housekeeping -> view housekeeping tasks (not implemented UI yet) - limited
            // Default: minimal

            var isAdmin = string.Equals(roleName, "Admin", StringComparison.OrdinalIgnoreCase);
            var isManager = string.Equals(roleName, "Manager", StringComparison.OrdinalIgnoreCase);
            var isFront = string.Equals(roleName, "FrontDesk", StringComparison.OrdinalIgnoreCase) ||
                          string.Equals(roleName, "Receptionist", StringComparison.OrdinalIgnoreCase);
            var isHousekeeping = string.Equals(roleName, "Housekeeping", StringComparison.OrdinalIgnoreCase);

            // Enable/disable controls
            btnUsers.Enabled = isAdmin;            // only admin
            btnBackup.Enabled = isAdmin;           // only admin
            btnReports.Enabled = isAdmin || isManager;
            btnReservations.Enabled = isAdmin || isManager || isFront;
            btnCheckIn.Enabled = isAdmin || isFront;
            btnCheckOut.Enabled = isAdmin || isFront;
            // housekeeping button not yet added - if added later set enabled = isAdmin || isHousekeeping

            // for better UX you can also set Visible = ... instead of Enabled if you prefer hiding
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            dashboardControl1?.RefreshData();
        }
        private void btnReservations_Click(object sender, EventArgs e)
        {
            using (var frm = new ReservationForm())
            {
                frm.ShowDialog(this);
                // After the dialog closes, refresh dashboard counts in case reservation changed data
                dashboardControl1?.RefreshData();
            }
        }
        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            using (var frm = new CheckInForm())
            {
                frm.ShowDialog(this);
                dashboardControl1?.RefreshData();
            }
        }
        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            using (var frm = new CheckOutForm())
            {
                frm.ShowDialog(this);
                dashboardControl1?.RefreshData();
            }
        }
        private void btnReports_Click(object sender, EventArgs e)
        {
            using (var frm = new ReportsForm())
            {
                frm.ShowDialog(this);
            }
        }
        private void btnBackup_Click(object sender, EventArgs e)
        {
            using (var frm = new BackupForm())
            {
                frm.ShowDialog(this);
            }
        }
        private void btnUsers_Click(object sender, EventArgs e)
        {
            using (var frm = new UserManagementForm())
            {
                frm.ShowDialog(this);
            }
        }
        private void btnHousekeeping_Click(object sender, EventArgs e)
        {
            using (var frm = new HousekeepingForm())
            {
                frm.ShowDialog(this);
                dashboardControl1?.RefreshData();
            }
        }



    }
}
