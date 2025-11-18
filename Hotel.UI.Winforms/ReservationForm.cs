// ReservationForm.cs
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Hotel.UI.WinForms
{
    public partial class ReservationForm : Form
    {
        public ReservationForm()
        {
            InitializeComponent();
            LoadRoomTypes();
            dtpCheckIn.Value = DateTime.Today.AddDays(1);
            dtpCheckOut.Value = DateTime.Today.AddDays(2);
        }

        private void LoadRoomTypes()
        {
            // load room types into cboRoomType
            var conn = ConfigurationManager.ConnectionStrings["HotelDb"].ConnectionString;
            using (var cn = new SqlConnection(conn))
            using (var cmd = new SqlCommand("SELECT RoomTypeId, Name, MaxOccupancy FROM RoomTypes", cn))
            {
                var dt = new DataTable();
                var da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cboRoomType.DisplayMember = "Name";
                cboRoomType.ValueMember = "RoomTypeId";
                cboRoomType.DataSource = dt;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchAvailableRooms();
        }

        private void SearchAvailableRooms()
        {
            // validate dates
            if (dtpCheckOut.Value.Date <= dtpCheckIn.Value.Date)
            {
                MessageBox.Show("Check-out must be after check-in.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var conn = ConfigurationManager.ConnectionStrings["HotelDb"].ConnectionString;
            using (var cn = new SqlConnection(conn))
            using (var cmd = new SqlCommand("sp_SearchAvailableRooms", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CheckInDate", dtpCheckIn.Value.Date);
                cmd.Parameters.AddWithValue("@CheckOutDate", dtpCheckOut.Value.Date);
                if (cboRoomType.SelectedValue != null) cmd.Parameters.AddWithValue("@RoomTypeId", cboRoomType.SelectedValue);
                else cmd.Parameters.AddWithValue("@RoomTypeId", DBNull.Value);
                cmd.Parameters.AddWithValue("@MinOccupancy", (int)nudGuests.Value);

                var da = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);
                dgvAvailableRooms.DataSource = dt;

                // show helpful columns
                if (dgvAvailableRooms.Columns.Contains("PricePerNight"))
                {
                    dgvAvailableRooms.Columns["PricePerNight"].DefaultCellStyle.Format = "N2";
                }
            }
        }

        private void btnNewGuest_Click(object sender, EventArgs e)
        {
            // simple new guest dialog
            using (var frm = new Form())
            {
                frm.Text = "New Guest";
                var txtFirst = new TextBox() { Left = 20, Top = 20, Width = 300, Text = "" };
                var lblFirst = new Label() { Left = 20, Top = 0, Text = "First name" };
                var txtLast = new TextBox() { Left = 20, Top = 60, Width = 300, Text = "" };
                var lblLast = new Label() { Left = 20, Top = 40, Text = "Last name" };
                var btnOk = new Button() { Left = 20, Top = 100, Text = "Save", DialogResult = DialogResult.OK };
                frm.Controls.Add(lblFirst);
                frm.Controls.Add(txtFirst);
                frm.Controls.Add(lblLast);
                frm.Controls.Add(txtLast);
                frm.Controls.Add(btnOk);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // insert guest into DB
                    var conn = ConfigurationManager.ConnectionStrings["HotelDb"].ConnectionString;
                    using (var cn = new SqlConnection(conn))
                    using (var cmd = new SqlCommand("INSERT INTO Guests (FirstName, LastName, CreatedAt) VALUES (@f,@l,GETDATE()); SELECT SCOPE_IDENTITY()", cn))
                    {
                        cmd.Parameters.AddWithValue("@f", txtFirst.Text.Trim());
                        cmd.Parameters.AddWithValue("@l", txtLast.Text.Trim());
                        cn.Open();
                        var id = cmd.ExecuteScalar();
                        cn.Close();
                        MessageBox.Show("Guest created (ID: " + id + ")", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnReserve_Click(object sender, EventArgs e)
        {
            if (dgvAvailableRooms.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a room from the list first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var roomId = Convert.ToInt32(dgvAvailableRooms.SelectedRows[0].Cells["RoomId"].Value);

            // Ask for guest id (simple)
            var guestIdStr = Microsoft.VisualBasic.Interaction.InputBox("Enter guest ID (or leave blank to create new)", "Guest ID", "");
            int guestId = 0;
            if (string.IsNullOrWhiteSpace(guestIdStr))
            {
                MessageBox.Show("Please use the New Guest button to create a guest and then provide its ID.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!int.TryParse(guestIdStr, out guestId))
            {
                MessageBox.Show("Invalid guest ID.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // create reservation
            var conn = ConfigurationManager.ConnectionStrings["HotelDb"].ConnectionString;
            using (var cn = new SqlConnection(conn))
            using (var cmd = new SqlCommand("INSERT INTO Reservations (GuestId, RoomId, CheckInDate, CheckOutDate, NumGuests, Status, CreatedByUserId, CreatedAt, Source) VALUES (@g,@r,@ci,@co,@ng,'Reserved',@u,GETDATE(),'FrontDesk')", cn))
            {
                cmd.Parameters.AddWithValue("@g", guestId);
                cmd.Parameters.AddWithValue("@r", roomId);
                cmd.Parameters.AddWithValue("@ci", dtpCheckIn.Value.Date);
                cmd.Parameters.AddWithValue("@co", dtpCheckOut.Value.Date);
                cmd.Parameters.AddWithValue("@ng", (int)nudGuests.Value);
                cmd.Parameters.AddWithValue("@u", Program.CurrentUser?.UserId ?? (object)DBNull.Value);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Reservation created.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // refresh results
                SearchAvailableRooms();
            }
        }

        private void dtpCheckOut_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpCheckIn_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cboRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
