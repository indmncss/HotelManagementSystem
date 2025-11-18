// HousekeepingForm.Designer.cs
namespace Hotel.UI.WinForms
{
    partial class HousekeepingForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvTasks;
        private System.Windows.Forms.Button btnNewTask;
        private System.Windows.Forms.Button btnAssign;
        private System.Windows.Forms.Button btnMarkComplete;
        private System.Windows.Forms.ComboBox cboRooms;
        private System.Windows.Forms.ComboBox cboUsers;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label lblRoom;
        private System.Windows.Forms.Label lblAssignee;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.Button btnRefresh;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvTasks = new System.Windows.Forms.DataGridView();
            this.btnNewTask = new System.Windows.Forms.Button();
            this.btnAssign = new System.Windows.Forms.Button();
            this.btnMarkComplete = new System.Windows.Forms.Button();
            this.cboRooms = new System.Windows.Forms.ComboBox();
            this.cboUsers = new System.Windows.Forms.ComboBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.lblRoom = new System.Windows.Forms.Label();
            this.lblAssignee = new System.Windows.Forms.Label();
            this.lblNotes = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvTasks)).BeginInit();
            this.SuspendLayout();

            // dgvTasks
            this.dgvTasks.Location = new System.Drawing.Point(16, 16);
            this.dgvTasks.Size = new System.Drawing.Size(760, 300);
            this.dgvTasks.ReadOnly = true;
            this.dgvTasks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTasks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTasks.MultiSelect = false;

            // controls for creating/assigning tasks
            this.lblRoom.AutoSize = true; this.lblRoom.Location = new System.Drawing.Point(16, 330); this.lblRoom.Text = "Room";
            this.cboRooms.Location = new System.Drawing.Point(16, 350); this.cboRooms.Width = 160; this.cboRooms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblAssignee.AutoSize = true; this.lblAssignee.Location = new System.Drawing.Point(200, 330); this.lblAssignee.Text = "Assign to";
            this.cboUsers.Location = new System.Drawing.Point(200, 350); this.cboUsers.Width = 180; this.cboUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblNotes.AutoSize = true; this.lblNotes.Location = new System.Drawing.Point(400, 330); this.lblNotes.Text = "Notes";
            this.txtNotes.Location = new System.Drawing.Point(400, 350); this.txtNotes.Width = 260; this.txtNotes.Height = 22;

            this.btnNewTask.Location = new System.Drawing.Point(16, 386); this.btnNewTask.Size = new System.Drawing.Size(120, 30); this.btnNewTask.Text = "New Task"; this.btnNewTask.Click += new System.EventHandler(this.btnNewTask_Click);
            this.btnAssign.Location = new System.Drawing.Point(152, 386); this.btnAssign.Size = new System.Drawing.Size(120, 30); this.btnAssign.Text = "Assign"; this.btnAssign.Click += new System.EventHandler(this.btnAssign_Click);
            this.btnMarkComplete.Location = new System.Drawing.Point(288, 386); this.btnMarkComplete.Size = new System.Drawing.Size(140, 30); this.btnMarkComplete.Text = "Mark Complete"; this.btnMarkComplete.Click += new System.EventHandler(this.btnMarkComplete_Click);
            this.btnRefresh.Location = new System.Drawing.Point(440, 386); this.btnRefresh.Size = new System.Drawing.Size(120, 30); this.btnRefresh.Text = "Refresh"; this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            this.ClientSize = new System.Drawing.Size(800, 430);
            this.Controls.Add(this.dgvTasks);
            this.Controls.Add(this.lblRoom);
            this.Controls.Add(this.cboRooms);
            this.Controls.Add(this.lblAssignee);
            this.Controls.Add(this.cboUsers);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.btnNewTask);
            this.Controls.Add(this.btnAssign);
            this.Controls.Add(this.btnMarkComplete);
            this.Controls.Add(this.btnRefresh);
            this.Text = "Housekeeping Tasks";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTasks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
