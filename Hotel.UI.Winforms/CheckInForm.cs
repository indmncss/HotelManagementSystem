// CheckInForm.cs
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Hotel.UI.WinForms
{
    public partial class CheckInForm : Form
    {
        public CheckInForm()
        {
            InitializeComponent();
            LoadPaymentMethods();
            LoadOpenReservations();
            LoadAvailableRooms(); // for walk-ins
        }

        private void LoadPaymentMethods()
        {
            cboPaymentMethod.Items.Clear();
            cboPaymentMethod.Items.Add("Cash");
            cboPaymentMethod.Items.Add("Card");
            cboPaymentMethod.Items.Add("Bank Transfer");
            cboPaymentMethod.SelectedIndex = 0;
        }

        private void LoadOpenReservations()
        {
            // load reservations with status 'Reserved'
            var conn = ConfigurationManager.ConnectionStrings["HotelDb"].ConnectionString;
            using (var cn = new SqlConnection(conn))
            using (var cmd = new SqlCommand("SELECT ReservationId, GuestId, RoomId, CheckInDate, CheckOutDate FROM Reservations WHERE Status = 'Reserved' ORDER BY CheckInDate", cn))
            {
                var dt = new DataTable();
                var da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cboReservations.DisplayMember = "ReservationId";
                cboReservations.ValueMember = "ReservationId";
                cboReservations.DataSource = dt;
            }
        }

        private void LoadAvailableRooms()
        {
            // list available rooms (status = Available)
            var conn = ConfigurationManager.ConnectionStrings["HotelDb"].ConnectionString;
            using (var cn = new SqlConnection(conn))
            using (var cmd = new SqlCommand("SELECT RoomId, RoomNumber FROM Rooms WHERE Status = 'Available'", cn))
            {
                var dt = new DataTable();
                var da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cboRoom.DisplayMember = "RoomNumber";
                cboRoom.ValueMember = "RoomId";
                cboRoom.DataSource = dt;
            }
        }

        private void btnLoadReservation_Click(object sender, EventArgs e)
        {
            if (cboReservations.SelectedValue == null) return;
            var resId = Convert.ToInt32(cboReservations.SelectedValue);
            var conn = ConfigurationManager.ConnectionStrings["HotelDb"].ConnectionString;
            using (var cn = new SqlConnection(conn))
            using (var cmd = new SqlCommand("SELECT r.ReservationId, g.FirstName, g.LastName, r.RoomId FROM Reservations r JOIN Guests g ON r.GuestId = g.GuestId WHERE r.ReservationId = @id", cn))
            {
                cmd.Parameters.AddWithValue("@id", resId);
                var dt = new DataTable();
                var da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    var row = dt.Rows[0];
                    txtGuest.Text = $"{row["FirstName"]} {row["LastName"]}";
                    if (row["RoomId"] != DBNull.Value)
                    {
                        // select assigned room if any
                        cboRoom.SelectedValue = Convert.ToInt32(row["RoomId"]);
                    }
                }
            }
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            // Validation
            int? reservationId = null;
            if (cboReservations.SelectedValue != null) reservationId = Convert.ToInt32(cboReservations.SelectedValue);
            if (cboRoom.SelectedValue == null) { MessageBox.Show("Select a room"); return; }

            var roomId = Convert.ToInt32(cboRoom.SelectedValue);
            var deposit = nudDeposit.Value;
            var paymentMethod = cboPaymentMethod.SelectedItem?.ToString();

            var conn = ConfigurationManager.ConnectionStrings["HotelDb"].ConnectionString;
            using (var cn = new SqlConnection(conn))
            {
                cn.Open();
                using (var tran = cn.BeginTransaction())
                {
                    try
                    {
                        // Before committing, double-check room is still available (prevent race)
                        using (var chkCmd = new SqlCommand("SELECT Status FROM Rooms WHERE RoomId = @rid", cn, tran))
                        {
                            chkCmd.Parameters.AddWithValue("@rid", roomId);
                            var status = chkCmd.ExecuteScalar()?.ToString();
                            if (status != "Available")
                            {
                                MessageBox.Show("Room is no longer available. Please choose another room.", "Conflict", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                tran.Rollback();
                                return;
                            }
                        }

                        // Insert CheckIn
                        using (var cmd = new SqlCommand(@"INSERT INTO CheckIns (ReservationId, ActualCheckInDateTime, ExpectedCheckOutDateTime, DepositAmount, PaymentMethod, HandledByUserId, RoomId)
                                                         VALUES (@res,@now,@expected,@deposit,@pm,@user,@room); SELECT SCOPE_IDENTITY()", cn, tran))
                        {
                            cmd.Parameters.AddWithValue("@res", (object)reservationId ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@now", DateTime.UtcNow);
                            // if reservation exists, load reservation's CheckOutDate; else default +1 day
                            DateTime expected = DateTime.UtcNow.AddDays(1);
                            if (reservationId.HasValue)
                            {
                                using (var rcmd = new SqlCommand("SELECT CheckOutDate FROM Reservations WHERE ReservationId = @id", cn, tran))
                                {
                                    rcmd.Parameters.AddWithValue("@id", reservationId.Value);
                                    var val = rcmd.ExecuteScalar();
                                    if (val != null && val != DBNull.Value) expected = Convert.ToDateTime(val);
                                }
                            }
                            cmd.Parameters.AddWithValue("@expected", expected);
                            cmd.Parameters.AddWithValue("@deposit", deposit);
                            cmd.Parameters.AddWithValue("@pm", paymentMethod ?? "");
                            cmd.Parameters.AddWithValue("@user", Program.CurrentUser?.UserId ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@room", roomId);

                            var checkInIdObj = cmd.ExecuteScalar();
                            var checkInId = Convert.ToInt32(checkInIdObj);

                            // Insert Payment record for deposit if > 0
                            if (deposit > 0)
                            {
                                using (var payCmd = new SqlCommand("INSERT INTO Payments (CheckInId, Amount, PaymentDate, PaymentMethod) VALUES (@cid,@amt,@dt,@pm)", cn, tran))
                                {
                                    payCmd.Parameters.AddWithValue("@cid", checkInId);
                                    payCmd.Parameters.AddWithValue("@amt", deposit);
                                    payCmd.Parameters.AddWithValue("@dt", DateTime.UtcNow);
                                    payCmd.Parameters.AddWithValue("@pm", paymentMethod ?? "Cash");
                                    payCmd.ExecuteNonQuery();
                                }
                            }

                            // Update Room status to Occupied
                            using (var upd = new SqlCommand("UPDATE Rooms SET Status = 'Occupied' WHERE RoomId = @rid", cn, tran))
                            {
                                upd.Parameters.AddWithValue("@rid", roomId);
                                upd.ExecuteNonQuery();
                            }

                            // If reservation exists, set its status to CheckedIn
                            if (reservationId.HasValue)
                            {
                                using (var ur = new SqlCommand("UPDATE Reservations SET Status = 'CheckedIn' WHERE ReservationId = @id", cn, tran))
                                {
                                    ur.Parameters.AddWithValue("@id", reservationId.Value);
                                    ur.ExecuteNonQuery();
                                }
                            }

                            // Add room status history
                            using (var hs = new SqlCommand("INSERT INTO RoomStatusesHistory (RoomId, OldStatus, NewStatus, ChangedAt, ChangedByUserId) VALUES (@rid,@old,'Occupied',GETDATE(),@user)", cn, tran))
                            {
                                hs.Parameters.AddWithValue("@rid", roomId);
                                hs.Parameters.AddWithValue("@old", "Available");
                                hs.Parameters.AddWithValue("@user", Program.CurrentUser?.UserId ?? (object)DBNull.Value);
                                hs.ExecuteNonQuery();
                            }

                            tran.Commit();

                            MessageBox.Show($"Checked in successfully (CheckIn ID: {checkInId}).", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Optionally: generate a small receipt text file (simulate printing)
                            var receipt = $"Check-In Receipt\r\nCheckInId: {checkInId}\r\nRoomId: {roomId}\r\nDeposit: {deposit:0.00}\r\nDate: {DateTime.UtcNow}\r\nHandled by: {Program.CurrentUser?.Username}";
                            var logPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Receipts");
                            System.IO.Directory.CreateDirectory(logPath);
                            System.IO.File.WriteAllText(System.IO.Path.Combine(logPath, $"receipt_{checkInId}.txt"), receipt);

                            // refresh lists
                            LoadOpenReservations();
                            LoadAvailableRooms();
                        }
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        Hotel.Common.Logger.LogException(ex);
                        MessageBox.Show("Error during check-in. See logs.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
