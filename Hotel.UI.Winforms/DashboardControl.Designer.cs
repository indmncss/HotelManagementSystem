namespace Hotel.UI.WinForms
{
    partial class DashboardControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblOccupancy;
        private System.Windows.Forms.Label lblCheckedIn;
        private System.Windows.Forms.Label lblUpcoming;
        private System.Windows.Forms.Label lblRevenueToday;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRevenue;
        private System.Windows.Forms.Label lblHousekeepingPending;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblOccupancy = new System.Windows.Forms.Label();
            this.lblCheckedIn = new System.Windows.Forms.Label();
            this.lblUpcoming = new System.Windows.Forms.Label();
            this.lblRevenueToday = new System.Windows.Forms.Label();
            this.chartRevenue = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenue)).BeginInit();
            this.SuspendLayout();
            // 
            // lblOccupancy
            // 
            this.lblOccupancy.AutoSize = true;
            this.lblOccupancy.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblOccupancy.Location = new System.Drawing.Point(16, 16);
            this.lblOccupancy.Name = "lblOccupancy";
            this.lblOccupancy.Size = new System.Drawing.Size(150, 21);
            this.lblOccupancy.Text = "Occupancy: 0%";
            // 
            // lblCheckedIn
            // 
            this.lblCheckedIn.AutoSize = true;
            this.lblCheckedIn.Location = new System.Drawing.Point(16, 46);
            this.lblCheckedIn.Name = "lblCheckedIn";
            this.lblCheckedIn.Size = new System.Drawing.Size(140, 15);
            this.lblCheckedIn.Text = "Checked-in: 0";
            // 
            // lblUpcoming
            // 
            this.lblUpcoming.AutoSize = true;
            this.lblUpcoming.Location = new System.Drawing.Point(200, 46);
            this.lblUpcoming.Name = "lblUpcoming";
            this.lblUpcoming.Size = new System.Drawing.Size(200, 15);
            this.lblUpcoming.Text = "Upcoming check-ins (24h): 0";
            // 
            // lblRevenueToday
            // 
            this.lblRevenueToday.AutoSize = true;
            this.lblRevenueToday.Location = new System.Drawing.Point(16, 72);
            this.lblRevenueToday.Name = "lblRevenueToday";
            this.lblRevenueToday.Size = new System.Drawing.Size(140, 15);
            this.lblRevenueToday.Text = "Revenue today: 0.00";
            // 
            // lblHousekeepingPending
            // 
            this.lblHousekeepingPending = new System.Windows.Forms.Label();
            this.lblHousekeepingPending.AutoSize = true;
            this.lblHousekeepingPending.Location = new System.Drawing.Point(200, 72); // adjust if necessary
            this.lblHousekeepingPending.Name = "lblHousekeepingPending";
            this.lblHousekeepingPending.Size = new System.Drawing.Size(220, 15);
            this.lblHousekeepingPending.Text = "Housekeeping pending: 0";
            this.Controls.Add(this.lblHousekeepingPending);

            // 
            // chartRevenue
            // 
            this.chartRevenue.Location = new System.Drawing.Point(16, 100);
            this.chartRevenue.Name = "chartRevenue";
            this.chartRevenue.Size = new System.Drawing.Size(700, 300);
            this.chartRevenue.TabIndex = 0;
            this.chartRevenue.Text = "chartRevenue";
            // 
            // DashboardControl
            // 
            this.Controls.Add(this.lblOccupancy);
            this.Controls.Add(this.lblCheckedIn);
            this.Controls.Add(this.lblUpcoming);
            this.Controls.Add(this.lblRevenueToday);
            this.Controls.Add(this.chartRevenue);
            this.Name = "DashboardControl";
            this.Size = new System.Drawing.Size(740, 420);
            ((System.ComponentModel.ISupportInitialize)(this.chartRevenue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
