// ReportsForm.cs
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Hotel.UI.WinForms
{
    public partial class ReportsForm : Form
    {
        public ReportsForm()
        {
            InitializeComponent();
            dtpDate.Value = DateTime.Today;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            RunDailyOccupancyReport(dtpDate.Value.Date);
        }

        private void RunDailyOccupancyReport(DateTime date)
        {
            try
            {
                var conn = ConfigurationManager.ConnectionStrings["HotelDb"].ConnectionString;
                using (var cn = new SqlConnection(conn))
                using (var cmd = new SqlCommand(@"
                    -- Daily Occupancy: room list with status for the selected date
                    SELECT
                        r.RoomId,
                        r.RoomNumber,
                        rt.Name AS RoomType,
                        r.Status,
                        CASE WHEN EXISTS (
                            SELECT 1 FROM CheckIns c
                            WHERE c.RoomId = r.RoomId
                              AND c.ActualCheckInDateTime <= @dateEnd
                              AND (c.ActualCheckOutDateTime IS NULL OR c.ActualCheckOutDateTime > @dateStart)
                        ) THEN 1 ELSE 0 END AS IsCheckedIn,
                        CASE WHEN EXISTS (
                            SELECT 1 FROM Reservations res
                            WHERE res.RoomId = r.RoomId
                              AND res.Status = 'Reserved'
                              AND res.CheckInDate < @dateEnd
                              AND res.CheckOutDate > @dateStart
                        ) THEN 1 ELSE 0 END AS IsReserved
                    FROM Rooms r
                    JOIN RoomTypes rt ON r.RoomTypeId = rt.RoomTypeId
                    ORDER BY r.RoomNumber
                ", cn))
                {
                    var dateStart = date.Date;
                    var dateEnd = date.Date.AddDays(1);
                    cmd.Parameters.AddWithValue("@dateStart", dateStart);
                    cmd.Parameters.AddWithValue("@dateEnd", dateEnd);

                    var dt = new DataTable();
                    var da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    dgvReport.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                Hotel.Common.Logger.LogException(ex);
                MessageBox.Show("Error generating report. See logs.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            if (dgvReport.DataSource == null) { MessageBox.Show("Run the report first."); return; }

            var sfd = new SaveFileDialog()
            {
                Filter = "CSV files (*.csv)|*.csv",
                FileName = $"DailyOccupancy_{dtpDate.Value:yyyyMMdd}.csv"
            };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            var dt = (DataTable)dgvReport.DataSource;
            using (var sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
            {
                // header
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    if (c > 0) sw.Write(",");
                    sw.Write($"\"{dt.Columns[c].ColumnName}\"");
                }
                sw.WriteLine();

                // rows
                foreach (DataRow r in dt.Rows)
                {
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        if (c > 0) sw.Write(",");
                        var val = r[c]?.ToString().Replace("\"", "\"\"") ?? "";
                        sw.Write($"\"{val}\"");
                    }
                    sw.WriteLine();
                }
            }

            MessageBox.Show("CSV exported.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
