// ReservationForm.Designer.cs
namespace Hotel.UI.WinForms
{
    partial class ReservationForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblCheckIn;
        private System.Windows.Forms.DateTimePicker dtpCheckIn;
        private System.Windows.Forms.Label lblCheckOut;
        private System.Windows.Forms.DateTimePicker dtpCheckOut;
        private System.Windows.Forms.ComboBox cboRoomType;
        private System.Windows.Forms.Label lblRoomType;
        private System.Windows.Forms.NumericUpDown nudGuests;
        private System.Windows.Forms.Label lblGuests;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvAvailableRooms;
        private System.Windows.Forms.Button btnReserve;
        private System.Windows.Forms.Button btnNewGuest;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblCheckIn = new System.Windows.Forms.Label();
            this.dtpCheckIn = new System.Windows.Forms.DateTimePicker();
            this.lblCheckOut = new System.Windows.Forms.Label();
            this.dtpCheckOut = new System.Windows.Forms.DateTimePicker();
            this.lblRoomType = new System.Windows.Forms.Label();
            this.cboRoomType = new System.Windows.Forms.ComboBox();
            this.lblGuests = new System.Windows.Forms.Label();
            this.nudGuests = new System.Windows.Forms.NumericUpDown();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgvAvailableRooms = new System.Windows.Forms.DataGridView();
            this.btnReserve = new System.Windows.Forms.Button();
            this.btnNewGuest = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudGuests)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableRooms)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCheckIn
            // 
            this.lblCheckIn.AutoSize = true;
            this.lblCheckIn.Location = new System.Drawing.Point(13, 16);
            this.lblCheckIn.Name = "lblCheckIn";
            this.lblCheckIn.Size = new System.Drawing.Size(49, 13);
            this.lblCheckIn.TabIndex = 0;
            this.lblCheckIn.Text = "Check-in";
            // 
            // dtpCheckIn
            // 
            this.dtpCheckIn.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCheckIn.Location = new System.Drawing.Point(16, 36);
            this.dtpCheckIn.Name = "dtpCheckIn";
            this.dtpCheckIn.Size = new System.Drawing.Size(200, 20);
            this.dtpCheckIn.TabIndex = 1;
            this.dtpCheckIn.ValueChanged += new System.EventHandler(this.dtpCheckIn_ValueChanged);
            // 
            // lblCheckOut
            // 
            this.lblCheckOut.AutoSize = true;
            this.lblCheckOut.Location = new System.Drawing.Point(232, 16);
            this.lblCheckOut.Name = "lblCheckOut";
            this.lblCheckOut.Size = new System.Drawing.Size(56, 13);
            this.lblCheckOut.TabIndex = 2;
            this.lblCheckOut.Text = "Check-out";
            // 
            // dtpCheckOut
            // 
            this.dtpCheckOut.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpCheckOut.Location = new System.Drawing.Point(235, 36);
            this.dtpCheckOut.Name = "dtpCheckOut";
            this.dtpCheckOut.Size = new System.Drawing.Size(200, 20);
            this.dtpCheckOut.TabIndex = 3;
            this.dtpCheckOut.ValueChanged += new System.EventHandler(this.dtpCheckOut_ValueChanged);
            // 
            // lblRoomType
            // 
            this.lblRoomType.AutoSize = true;
            this.lblRoomType.Location = new System.Drawing.Point(451, 16);
            this.lblRoomType.Name = "lblRoomType";
            this.lblRoomType.Size = new System.Drawing.Size(62, 13);
            this.lblRoomType.TabIndex = 4;
            this.lblRoomType.Text = "Room Type";
            // 
            // cboRoomType
            // 
            this.cboRoomType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRoomType.Location = new System.Drawing.Point(454, 36);
            this.cboRoomType.Name = "cboRoomType";
            this.cboRoomType.Size = new System.Drawing.Size(160, 21);
            this.cboRoomType.TabIndex = 5;
            this.cboRoomType.SelectedIndexChanged += new System.EventHandler(this.cboRoomType_SelectedIndexChanged);
            // 
            // lblGuests
            // 
            this.lblGuests.AutoSize = true;
            this.lblGuests.Location = new System.Drawing.Point(630, 16);
            this.lblGuests.Name = "lblGuests";
            this.lblGuests.Size = new System.Drawing.Size(40, 13);
            this.lblGuests.TabIndex = 6;
            this.lblGuests.Text = "Guests";
            // 
            // nudGuests
            // 
            this.nudGuests.Location = new System.Drawing.Point(633, 36);
            this.nudGuests.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudGuests.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudGuests.Name = "nudGuests";
            this.nudGuests.Size = new System.Drawing.Size(120, 20);
            this.nudGuests.TabIndex = 7;
            this.nudGuests.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(772, 35);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dgvAvailableRooms
            // 
            this.dgvAvailableRooms.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAvailableRooms.Location = new System.Drawing.Point(16, 80);
            this.dgvAvailableRooms.MultiSelect = false;
            this.dgvAvailableRooms.Name = "dgvAvailableRooms";
            this.dgvAvailableRooms.ReadOnly = true;
            this.dgvAvailableRooms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAvailableRooms.Size = new System.Drawing.Size(833, 300);
            this.dgvAvailableRooms.TabIndex = 9;
            // 
            // btnReserve
            // 
            this.btnReserve.Location = new System.Drawing.Point(772, 405);
            this.btnReserve.Name = "btnReserve";
            this.btnReserve.Size = new System.Drawing.Size(75, 23);
            this.btnReserve.TabIndex = 10;
            this.btnReserve.Text = "Reserve";
            this.btnReserve.Click += new System.EventHandler(this.btnReserve_Click);
            // 
            // btnNewGuest
            // 
            this.btnNewGuest.Location = new System.Drawing.Point(678, 405);
            this.btnNewGuest.Name = "btnNewGuest";
            this.btnNewGuest.Size = new System.Drawing.Size(75, 23);
            this.btnNewGuest.TabIndex = 11;
            this.btnNewGuest.Text = "New Guest";
            this.btnNewGuest.Click += new System.EventHandler(this.btnNewGuest_Click);
            // 
            // ReservationForm
            // 
            this.ClientSize = new System.Drawing.Size(869, 440);
            this.Controls.Add(this.lblCheckIn);
            this.Controls.Add(this.dtpCheckIn);
            this.Controls.Add(this.lblCheckOut);
            this.Controls.Add(this.dtpCheckOut);
            this.Controls.Add(this.lblRoomType);
            this.Controls.Add(this.cboRoomType);
            this.Controls.Add(this.lblGuests);
            this.Controls.Add(this.nudGuests);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dgvAvailableRooms);
            this.Controls.Add(this.btnReserve);
            this.Controls.Add(this.btnNewGuest);
            this.Name = "ReservationForm";
            this.Text = "Reservations";
            ((System.ComponentModel.ISupportInitialize)(this.nudGuests)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableRooms)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
