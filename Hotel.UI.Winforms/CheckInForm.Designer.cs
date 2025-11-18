// CheckInForm.Designer.cs
namespace Hotel.UI.WinForms
{
    partial class CheckInForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblReservation;
        private System.Windows.Forms.ComboBox cboReservations;
        private System.Windows.Forms.Button btnLoadReservation;
        private System.Windows.Forms.Label lblGuest;
        private System.Windows.Forms.TextBox txtGuest;
        private System.Windows.Forms.Label lblRoom;
        private System.Windows.Forms.ComboBox cboRoom;
        private System.Windows.Forms.Label lblDeposit;
        private System.Windows.Forms.NumericUpDown nudDeposit;
        private System.Windows.Forms.ComboBox cboPaymentMethod;
        private System.Windows.Forms.Label lblPaymentMethod;
        private System.Windows.Forms.Button btnCheckIn;
        private System.Windows.Forms.Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblReservation = new System.Windows.Forms.Label();
            this.cboReservations = new System.Windows.Forms.ComboBox();
            this.btnLoadReservation = new System.Windows.Forms.Button();
            this.lblGuest = new System.Windows.Forms.Label();
            this.txtGuest = new System.Windows.Forms.TextBox();
            this.lblRoom = new System.Windows.Forms.Label();
            this.cboRoom = new System.Windows.Forms.ComboBox();
            this.lblDeposit = new System.Windows.Forms.Label();
            this.nudDeposit = new System.Windows.Forms.NumericUpDown();
            this.lblPaymentMethod = new System.Windows.Forms.Label();
            this.cboPaymentMethod = new System.Windows.Forms.ComboBox();
            this.btnCheckIn = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudDeposit)).BeginInit();
            this.SuspendLayout();
            // 
            // lblReservation
            // 
            this.lblReservation.AutoSize = true;
            this.lblReservation.Location = new System.Drawing.Point(16, 16);
            this.lblReservation.Name = "lblReservation";
            this.lblReservation.Size = new System.Drawing.Size(110, 13);
            this.lblReservation.TabIndex = 0;
            this.lblReservation.Text = "Reservation (optional)";
            // 
            // cboReservations
            // 
            this.cboReservations.Location = new System.Drawing.Point(16, 36);
            this.cboReservations.Name = "cboReservations";
            this.cboReservations.Size = new System.Drawing.Size(480, 21);
            this.cboReservations.TabIndex = 1;
            // 
            // btnLoadReservation
            // 
            this.btnLoadReservation.Location = new System.Drawing.Point(504, 34);
            this.btnLoadReservation.Name = "btnLoadReservation";
            this.btnLoadReservation.Size = new System.Drawing.Size(75, 23);
            this.btnLoadReservation.TabIndex = 2;
            this.btnLoadReservation.Text = "Load";
            this.btnLoadReservation.Click += new System.EventHandler(this.btnLoadReservation_Click);
            // 
            // lblGuest
            // 
            this.lblGuest.AutoSize = true;
            this.lblGuest.Location = new System.Drawing.Point(16, 72);
            this.lblGuest.Name = "lblGuest";
            this.lblGuest.Size = new System.Drawing.Size(35, 13);
            this.lblGuest.TabIndex = 3;
            this.lblGuest.Text = "Guest";
            // 
            // txtGuest
            // 
            this.txtGuest.Location = new System.Drawing.Point(16, 92);
            this.txtGuest.Name = "txtGuest";
            this.txtGuest.ReadOnly = true;
            this.txtGuest.Size = new System.Drawing.Size(480, 20);
            this.txtGuest.TabIndex = 4;
            // 
            // lblRoom
            // 
            this.lblRoom.AutoSize = true;
            this.lblRoom.Location = new System.Drawing.Point(16, 128);
            this.lblRoom.Name = "lblRoom";
            this.lblRoom.Size = new System.Drawing.Size(74, 13);
            this.lblRoom.TabIndex = 5;
            this.lblRoom.Text = "Room (assign)";
            // 
            // cboRoom
            // 
            this.cboRoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRoom.Location = new System.Drawing.Point(16, 148);
            this.cboRoom.Name = "cboRoom";
            this.cboRoom.Size = new System.Drawing.Size(200, 21);
            this.cboRoom.TabIndex = 6;
            // 
            // lblDeposit
            // 
            this.lblDeposit.AutoSize = true;
            this.lblDeposit.Location = new System.Drawing.Point(240, 128);
            this.lblDeposit.Name = "lblDeposit";
            this.lblDeposit.Size = new System.Drawing.Size(43, 13);
            this.lblDeposit.TabIndex = 7;
            this.lblDeposit.Text = "Deposit";
            // 
            // nudDeposit
            // 
            this.nudDeposit.DecimalPlaces = 2;
            this.nudDeposit.Location = new System.Drawing.Point(240, 148);
            this.nudDeposit.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudDeposit.Name = "nudDeposit";
            this.nudDeposit.Size = new System.Drawing.Size(120, 20);
            this.nudDeposit.TabIndex = 8;
            // 
            // lblPaymentMethod
            // 
            this.lblPaymentMethod.AutoSize = true;
            this.lblPaymentMethod.Location = new System.Drawing.Point(16, 184);
            this.lblPaymentMethod.Name = "lblPaymentMethod";
            this.lblPaymentMethod.Size = new System.Drawing.Size(87, 13);
            this.lblPaymentMethod.TabIndex = 9;
            this.lblPaymentMethod.Text = "Payment Method";
            // 
            // cboPaymentMethod
            // 
            this.cboPaymentMethod.Location = new System.Drawing.Point(16, 204);
            this.cboPaymentMethod.Name = "cboPaymentMethod";
            this.cboPaymentMethod.Size = new System.Drawing.Size(200, 21);
            this.cboPaymentMethod.TabIndex = 10;
            // 
            // btnCheckIn
            // 
            this.btnCheckIn.Location = new System.Drawing.Point(16, 240);
            this.btnCheckIn.Name = "btnCheckIn";
            this.btnCheckIn.Size = new System.Drawing.Size(75, 23);
            this.btnCheckIn.TabIndex = 11;
            this.btnCheckIn.Text = "Check In";
            this.btnCheckIn.Click += new System.EventHandler(this.btnCheckIn_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(141, 240);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "Close";
            // 
            // CheckInForm
            // 
            this.ClientSize = new System.Drawing.Size(640, 300);
            this.Controls.Add(this.lblReservation);
            this.Controls.Add(this.cboReservations);
            this.Controls.Add(this.btnLoadReservation);
            this.Controls.Add(this.lblGuest);
            this.Controls.Add(this.txtGuest);
            this.Controls.Add(this.lblRoom);
            this.Controls.Add(this.cboRoom);
            this.Controls.Add(this.lblDeposit);
            this.Controls.Add(this.nudDeposit);
            this.Controls.Add(this.lblPaymentMethod);
            this.Controls.Add(this.cboPaymentMethod);
            this.Controls.Add(this.btnCheckIn);
            this.Controls.Add(this.btnClose);
            this.Name = "CheckInForm";
            this.Text = "Check-In";
            ((System.ComponentModel.ISupportInitialize)(this.nudDeposit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
