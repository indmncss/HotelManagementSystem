using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Hotel.UI.WinForms
{
    public partial class CheckOutForm : Form
    {
        public CheckOutForm()
        {
            InitializeComponent();
            LoadActiveCheckIns();
        }

        private void LoadActiveCheckIns()
        {
            var conn = ConfigurationManager.ConnectionStrings["HotelDb"].ConnectionString;
            using (var cn = new SqlConnection(conn))
            using (var cmd = new SqlCommand(@"SELECT c.CheckInId, c.ReservationId, c.RoomId, r.RoomNumber, c.ActualCheckInDateTime, c.ExpectedCheckOutDateTime
                                             FROM CheckIns c JOIN Rooms r ON c.RoomId = r.RoomId
                                             WHERE c.ActualCheckOutDateTime IS NULL", cn))
            {
                var dt = new DataTable();
                var da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dgvCheckIns.DataSource = dt;
            }
            ClearInvoice();
        }

        private void ClearInvoice()
        {
            txtFolio.Text = string.Empty;
            txtTotal.Text = "0.00";
            dgvPayments.DataSource = null;
        }

        private void btnAddPayment_Click(object sender, EventArgs e)
        {
            if (dgvCheckIns.SelectedRows.Count == 0) { MessageBox.Show("Select a check-in first."); return; }
            var checkInId = Convert.ToInt32(dgvCheckIns.SelectedRows[0].Cells["CheckInId"].Value);

            // simple add payment dialog
            using (var frm = new Form())
            {
                frm.Text = "Add Payment";
                var txtAmt = new TextBox() { Left = 20, Top = 20, Width = 200, Text = "0.00" };
                var cboMethod = new ComboBox() { Left = 20, Top = 60, Width = 200 };
                cboMethod.Items.AddRange(new[] { "Cash", "Card", "Bank Transfer" }); cboMethod.SelectedIndex = 0;
                var btnOk = new Button() { Left = 20, Top = 100, Text = "Save", DialogResult = DialogResult.OK };
                frm.Controls.Add(txtAmt); frm.Controls.Add(cboMethod); frm.Controls.Add(btnOk);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    if (!decimal.TryParse(txtAmt.Text, out var amt) || amt <= 0) { MessageBox.Show("Invalid amount."); return; }
                    var conn = ConfigurationManager.ConnectionStrings["HotelDb"].ConnectionString;
                    using (var cn = new SqlConnection(conn))
                    using (var cmd = new SqlCommand("INSERT INTO Payments (CheckInId, Amount, PaymentDate, PaymentMethod) VALUES (@cid,@amt,GETDATE(),@pm)", cn))
                    {
                        cmd.Parameters.AddWithValue("@cid", checkInId);
                        cmd.Parameters.AddWithValue("@amt", amt);
                        cmd.Parameters.AddWithValue("@pm", cboMethod.SelectedItem.ToString());
                        cn.Open(); cmd.ExecuteNonQuery(); cn.Close();
                    }
                    // refresh payments list
                    ShowInvoiceForCheckIn(checkInId);
                }
            }
        }

        private void ShowInvoiceForCheckIn(int checkInId)
        {
            // compute invoice: nights * rate + extras (simple: use current rate.PricePerNight)
            var conn = ConfigurationManager.ConnectionStrings["HotelDb"].ConnectionString;
            using (var cn = new SqlConnection(conn))
            {
                // fetch checkin, room, rate
                var cmd = new SqlCommand(@"SELECT ci.CheckInId, ci.ReservationId, ci.ActualCheckInDateTime, ci.ExpectedCheckOutDateTime, r.RoomNumber, rt.MaxOccupancy, ISNULL(rate.PricePerNight,0) AS PricePerNight
                                          FROM CheckIns ci
                                          JOIN Rooms r ON ci.RoomId = r.RoomId
                                          JOIN RoomTypes rt ON r.RoomTypeId = rt.RoomTypeId
                                          LEFT JOIN Rates rate ON r.RateId = rate.RateId
                                          WHERE ci.CheckInId = @cid", cn);
                cmd.Parameters.AddWithValue("@cid", checkInId);
                var dt = new DataTable();
                var da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count == 0) return;

                var row = dt.Rows[0];
                var checkIn = Convert.ToDateTime(row["ActualCheckInDateTime"]);
                var expected = Convert.ToDateTime(row["ExpectedCheckOutDateTime"]);
                var nights = (expected.Date - checkIn.Date).Days;
                if (nights <= 0) nights = 1;
                var rate = Convert.ToDecimal(row["PricePerNight"]);
                var subtotal = nights * rate;
                var taxRate = 0.0m;
                // read tax rate from Settings if present
                // read tax rate from Settings if present
                // NOTE: ensure the connection is open because ExecuteScalar requires it
                if (cn.State != System.Data.ConnectionState.Open) cn.Open();
                using (var sCmd = new SqlCommand("SELECT [Value] FROM Settings WHERE [Key] = 'TaxRate'", cn))
                {
                    var val = sCmd.ExecuteScalar();
                    if (val != null && decimal.TryParse(val.ToString(), out var tr)) taxRate = tr;
                }
                // If you opened the connection here, it's fine to leave it open until the method ends
                // or explicitly close it: cn.Close();
                var tax = subtotal * taxRate;
                var total = subtotal + tax;

                txtFolio.Text = $"FOLIO-{checkInId}-{DateTime.UtcNow:yyyyMMddHHmm}";
                txtTotal.Text = total.ToString("0.00");

                // payments
                var pCmd = new SqlCommand("SELECT PaymentId, Amount, PaymentDate, PaymentMethod FROM Payments WHERE CheckInId = @cid ORDER BY PaymentDate", cn);
                pCmd.Parameters.AddWithValue("@cid", checkInId);
                var pdt = new DataTable();
                var pda = new SqlDataAdapter(pCmd);
                pda.Fill(pdt);
                dgvPayments.DataSource = pdt;
            }
        }

        private void btnFinalize_Click(object sender, EventArgs e)
        {
            if (dgvCheckIns.SelectedRows.Count == 0) { MessageBox.Show("Select a check-in."); return; }
            var checkInId = Convert.ToInt32(dgvCheckIns.SelectedRows[0].Cells["CheckInId"].Value);
            var conn = ConfigurationManager.ConnectionStrings["HotelDb"].ConnectionString;
            using (var cn = new SqlConnection(conn))
            {
                cn.Open();
                using (var tran = cn.BeginTransaction())
                {
                    try
                    {
                        // recompute total
                        var cmd = new SqlCommand(@"SELECT ci.CheckInId, ci.RoomId, ci.ActualCheckInDateTime, ci.ExpectedCheckOutDateTime, ISNULL(rate.PricePerNight,0) AS PricePerNight
                                                   FROM CheckIns ci
                                                   JOIN Rooms r ON ci.RoomId = r.RoomId
                                                   LEFT JOIN Rates rate ON r.RateId = rate.RateId
                                                   WHERE ci.CheckInId = @cid", cn, tran);
                        cmd.Parameters.AddWithValue("@cid", checkInId);
                        var dt = new DataTable();
                        var da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        if (dt.Rows.Count == 0) { MessageBox.Show("Check-in not found."); tran.Rollback(); return; }

                        var row = dt.Rows[0];
                        var checkIn = Convert.ToDateTime(row["ActualCheckInDateTime"]);
                        var expected = Convert.ToDateTime(row["ExpectedCheckOutDateTime"]);
                        var nights = (expected.Date - checkIn.Date).Days;
                        if (nights <= 0) nights = 1;
                        var rate = Convert.ToDecimal(row["PricePerNight"]);
                        var subtotal = nights * rate;
                        decimal taxRate = 0m;
                        using (var sCmd = new SqlCommand("SELECT [Value] FROM Settings WHERE [Key] = 'TaxRate'", cn, tran))
                        {
                            var val = sCmd.ExecuteScalar();
                            if (val != null && decimal.TryParse(val.ToString(), out var tr)) taxRate = tr;
                        }
                        var tax = subtotal * taxRate;
                        var total = subtotal + tax;

                        // sum payments
                        var payCmd = new SqlCommand("SELECT ISNULL(SUM(Amount),0) FROM Payments WHERE CheckInId = @cid", cn, tran);
                        payCmd.Parameters.AddWithValue("@cid", checkInId);
                        var paid = Convert.ToDecimal(payCmd.ExecuteScalar());

                        if (paid < total)
                        {
                            MessageBox.Show($"Total due is {total:0.00}. Payments received {paid:0.00}. Please collect remaining amount first.", "Payment Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            tran.Rollback();
                            return;
                        }

                        // Create Invoice record
                        var invCmd = new SqlCommand("INSERT INTO Invoices (CheckInId, TotalAmount, Tax, Discounts, Status, GeneratedAt) VALUES (@cid,@total,@tax,0,'Paid',GETDATE()); SELECT SCOPE_IDENTITY()", cn, tran);
                        invCmd.Parameters.AddWithValue("@cid", checkInId);
                        invCmd.Parameters.AddWithValue("@total", total);
                        invCmd.Parameters.AddWithValue("@tax", tax);
                        var invoiceId = Convert.ToInt32(invCmd.ExecuteScalar());

                        // Update CheckIn actual checkout time
                        var updCheck = new SqlCommand("UPDATE CheckIns SET ActualCheckOutDateTime = GETDATE() WHERE CheckInId = @cid", cn, tran);
                        updCheck.Parameters.AddWithValue("@cid", checkInId);
                        updCheck.ExecuteNonQuery();

                        // Update Room status to Available
                        var roomId = Convert.ToInt32(row["RoomId"]);
                        var upRoom = new SqlCommand("UPDATE Rooms SET Status = 'Available' WHERE RoomId = @rid", cn, tran);
                        upRoom.Parameters.AddWithValue("@rid", roomId);
                        upRoom.ExecuteNonQuery();

                        // Update reservation if any
                        if (row.Table.Columns.Contains("ReservationId") && row["ReservationId"] != DBNull.Value)
                        {
                            var resId = row["ReservationId"];
                            var ur = new SqlCommand("UPDATE Reservations SET Status = 'Completed' WHERE ReservationId = @id", cn, tran);
                            ur.Parameters.AddWithValue("@id", resId);
                            ur.ExecuteNonQuery();
                        }

                        // room status history
                        var hs = new SqlCommand("INSERT INTO RoomStatusesHistory (RoomId, OldStatus, NewStatus, ChangedAt, ChangedByUserId) VALUES (@rid,'Occupied','Available',GETDATE(),@user)", cn, tran);
                        hs.Parameters.AddWithValue("@rid", roomId);
                        hs.Parameters.AddWithValue("@user", Program.CurrentUser?.UserId ?? (object)DBNull.Value);
                        hs.ExecuteNonQuery();

                        tran.Commit();

                        MessageBox.Show($"Checked out and invoice generated (InvoiceId: {invoiceId}).", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // simulate invoice print to Receipts folder
                        var receipt = $"Invoice ID: {invoiceId}\r\nCheckInID: {checkInId}\r\nTotal: {total:0.00}\r\nPaid: {paid:0.00}\r\nDate: {DateTime.UtcNow}";
                        var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Receipts");
                        System.IO.Directory.CreateDirectory(path);
                        System.IO.File.WriteAllText(System.IO.Path.Combine(path, $"invoice_{invoiceId}.txt"), receipt);

                        LoadActiveCheckIns();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        Hotel.Common.Logger.LogException(ex);
                        MessageBox.Show("Error during finalize. See logs.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dgvCheckIns_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCheckIns.SelectedRows.Count == 0) { ClearInvoice(); return; }
            var checkInId = Convert.ToInt32(dgvCheckIns.SelectedRows[0].Cells["CheckInId"].Value);
            ShowInvoiceForCheckIn(checkInId);
        }
    }
}
