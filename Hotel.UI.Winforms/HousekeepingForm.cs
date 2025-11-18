// HousekeepingForm.cs
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Hotel.UI.WinForms
{
    public partial class HousekeepingForm : Form
    {
        public HousekeepingForm()
        {
            InitializeComponent();
            LoadRooms();
            LoadUsers();
            LoadTasks();
        }

        private string Conn => ConfigurationManager.ConnectionStrings["HotelDb"].ConnectionString;

        private void LoadRooms()
        {
            try
            {
                using (var cn = new SqlConnection(Conn))
                using (var cmd = new SqlCommand("SELECT RoomId, RoomNumber FROM Rooms ORDER BY RoomNumber", cn))
                {
                    var dt = new DataTable();
                    var da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    cboRooms.DisplayMember = "RoomNumber";
                    cboRooms.ValueMember = "RoomId";
                    cboRooms.DataSource = dt;
                }
            }
            catch (Exception ex) { Hotel.Common.Logger.LogException(ex); }
        }

        private void LoadUsers()
        {
            try
            {
                using (var cn = new SqlConnection(Conn))
                using (var cmd = new SqlCommand("SELECT UserId, Username FROM Users WHERE IsActive = 1 ORDER BY Username", cn))
                {
                    var dt = new DataTable();
                    var da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    cboUsers.DisplayMember = "Username";
                    cboUsers.ValueMember = "UserId";
                    cboUsers.DataSource = dt;
                }
            }
            catch (Exception ex) { Hotel.Common.Logger.LogException(ex); }
        }

        private void LoadTasks()
        {
            try
            {
                using (var cn = new SqlConnection(Conn))
                using (var cmd = new SqlCommand(@"
                    SELECT ht.TaskId, ht.RoomId, r.RoomNumber, ht.AssignedToUserId, u.Username AS AssignedTo, ht.Status, ht.Notes, ht.ScheduledAt, ht.CompletedAt
                    FROM HousekeepingTasks ht
                    LEFT JOIN Rooms r ON ht.RoomId = r.RoomId
                    LEFT JOIN Users u ON ht.AssignedToUserId = u.UserId
                    ORDER BY ht.Status, ht.ScheduledAt DESC", cn))
                {
                    var dt = new DataTable();
                    var da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    dgvTasks.DataSource = dt;
                }
            }
            catch (Exception ex) { Hotel.Common.Logger.LogException(ex); }
        }

        private void btnNewTask_Click(object sender, EventArgs e)
        {
            if (cboRooms.SelectedValue == null) { MessageBox.Show("Select a room."); return; }
            var roomId = Convert.ToInt32(cboRooms.SelectedValue);
            var notes = txtNotes.Text.Trim();
            var scheduledAt = DateTime.UtcNow;

            try
            {
                using (var cn = new SqlConnection(Conn))
                using (var cmd = new SqlCommand("INSERT INTO HousekeepingTasks (RoomId, AssignedToUserId, Status, Notes, ScheduledAt) VALUES (@rid, NULL, 'Open', @notes, @sched)", cn))
                {
                    cmd.Parameters.AddWithValue("@rid", roomId);
                    cmd.Parameters.AddWithValue("@notes", (object)notes ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@sched", scheduledAt);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
                txtNotes.Text = "";
                LoadTasks();
                MessageBox.Show("Task created.");
            }
            catch (Exception ex) { Hotel.Common.Logger.LogException(ex); MessageBox.Show("Error creating task."); }
        }

        private void btnAssign_Click(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count == 0) { MessageBox.Show("Select a task to assign."); return; }
            if (cboUsers.SelectedValue == null) { MessageBox.Show("Select a user to assign to."); return; }

            var taskId = Convert.ToInt32(dgvTasks.SelectedRows[0].Cells["TaskId"].Value);
            var userId = Convert.ToInt32(cboUsers.SelectedValue);
            try
            {
                using (var cn = new SqlConnection(Conn))
                using (var cmd = new SqlCommand("UPDATE HousekeepingTasks SET AssignedToUserId = @uid, Status = 'Assigned' WHERE TaskId = @tid", cn))
                {
                    cmd.Parameters.AddWithValue("@uid", userId);
                    cmd.Parameters.AddWithValue("@tid", taskId);
                    cn.Open(); cmd.ExecuteNonQuery(); cn.Close();
                }
                LoadTasks();
                MessageBox.Show("Task assigned.");
            }
            catch (Exception ex) { Hotel.Common.Logger.LogException(ex); MessageBox.Show("Error assigning task."); }
        }

        private void btnMarkComplete_Click(object sender, EventArgs e)
        {
            if (dgvTasks.SelectedRows.Count == 0) { MessageBox.Show("Select a task."); return; }
            var taskId = Convert.ToInt32(dgvTasks.SelectedRows[0].Cells["TaskId"].Value);

            try
            {
                using (var cn = new SqlConnection(Conn))
                using (var cmd = new SqlCommand("UPDATE HousekeepingTasks SET Status = 'Completed', CompletedAt = GETDATE() WHERE TaskId = @tid", cn))
                {
                    cmd.Parameters.AddWithValue("@tid", taskId);
                    cn.Open(); cmd.ExecuteNonQuery(); cn.Close();
                }
                LoadTasks();
                MessageBox.Show("Task marked complete.");
            }
            catch (Exception ex) { Hotel.Common.Logger.LogException(ex); MessageBox.Show("Error updating task."); }
        }

        private void btnRefresh_Click(object sender, EventArgs e) => LoadTasks();
    }
}
