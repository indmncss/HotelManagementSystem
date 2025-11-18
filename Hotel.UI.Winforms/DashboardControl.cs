using System;
using System.Linq;
using System.Windows.Forms;
using Hotel.Data;
using Hotel.Data.UnitOfWork;
using System.Configuration;
using System.Data.Entity;
using System.Windows.Forms.DataVisualization.Charting;


namespace Hotel.UI.WinForms
{
    public partial class DashboardControl : UserControl
    {
        public DashboardControl()
        {
            InitializeComponent();
            InitializeChart();
        }

        private void InitializeChart()
        {
            var chart = chartRevenue;
            chart.Series.Clear();
            var series = chart.Series.Add("Revenue");
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
        }

        public void RefreshData()
        {
            try
            {
                var conn = ConfigurationManager.ConnectionStrings["HotelDb"].ConnectionString;
                using (var ctx = new HotelDbContext(conn))
                using (var uow = new UnitOfWork(ctx))
                {
                    // Occupancy rate today = occupied rooms / total rooms
                    var totalRooms = uow.Rooms.GetAll().Count();
                    // occupied = rooms with CheckIns where ActualCheckInDateTime <= now and (ActualCheckOutDateTime is null or > now)
                    var now = DateTime.UtcNow;
                    var checkedInCount = uow.CheckIns.Find(c =>
                        DbFunctions.TruncateTime(c.ActualCheckInDateTime) <= DbFunctions.TruncateTime(now) &&
                        (c.ActualCheckOutDateTime == null || DbFunctions.TruncateTime(c.ActualCheckOutDateTime) > DbFunctions.TruncateTime(now))
                    ).Count();

                    var occupancyRate = totalRooms == 0 ? 0m : (decimal)checkedInCount / totalRooms * 100m;

                    lblOccupancy.Text = $"Occupancy: {occupancyRate:0.##}%";
                    lblCheckedIn.Text = $"Checked-in: {checkedInCount}";

                    // Upcoming check-ins next 24 hours
                    var next24 = now.AddHours(24);
                    var upcoming = uow.Reservations.Find(r => r.CheckInDate >= now && r.CheckInDate <= next24 && r.Status == "Reserved").Count();
                    lblUpcoming.Text = $"Upcoming check-ins (24h): {upcoming}";

                    // Revenue today: sum of Invoices GeneratedAt today
                    var todayStart = now.Date;
                    var rev = uow.Invoices.Find(i => DbFunctions.TruncateTime(i.GeneratedAt) == DbFunctions.TruncateTime(now))
                                 .Sum(i => (decimal?)i.TotalAmount) ?? 0m;
                    lblRevenueToday.Text = $"Revenue today: {rev:0.00}";

                    // Pending housekeeping tasks (not completed)
                    var pendingHousekeeping = uow.HousekeepingTasks
                        .Find(h => h.Status != "Completed").Count();
                    lblHousekeepingPending.Text = $"Housekeeping pending: {pendingHousekeeping}";

                    // Monthly revenue chart (last 6 months)
                    var sixMonthsAgo = now.AddMonths(-5);
                    var monthly = uow.Invoices.GetAll()
                        .Where(i => i.GeneratedAt >= sixMonthsAgo)
                        .GroupBy(i => new { Year = i.GeneratedAt.Year, Month = i.GeneratedAt.Month })
                        .Select(g => new { Year = g.Key.Year, Month = g.Key.Month, Revenue = g.Sum(x => x.TotalAmount) })
                        .OrderBy(x => x.Year).ThenBy(x => x.Month)
                        .ToList();

                    var chart = chartRevenue;
                    chart.Series["Revenue"].Points.Clear();
                    foreach (var m in monthly)
                    {
                        var label = new DateTime(m.Year, m.Month, 1).ToString("MMM yyyy");
                        chart.Series["Revenue"].Points.AddXY(label, (double)m.Revenue);
                    }
                }
            }
            catch (Exception ex)
            {
                Hotel.Common.Logger.LogException(ex);
                // show friendly error
                lblRevenueToday.Text = "Revenue today: error";
            }
        }
    }
}
