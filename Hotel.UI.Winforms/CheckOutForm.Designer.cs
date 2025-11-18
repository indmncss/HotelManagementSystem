// CheckOutForm.Designer.cs
namespace Hotel.UI.WinForms
{
    partial class CheckOutForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblActiveCheckIns;
        private System.Windows.Forms.DataGridView dgvCheckIns;
        private System.Windows.Forms.Label lblFolio;
        private System.Windows.Forms.TextBox txtFolio;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label lblPayments;
        private System.Windows.Forms.DataGridView dgvPayments;
        private System.Windows.Forms.Button btnAddPayment;
        private System.Windows.Forms.Button btnFinalize;
        private System.Windows.Forms.Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblActiveCheckIns = new System.Windows.Forms.Label();
            this.dgvCheckIns = new System.Windows.Forms.DataGridView();
            this.lblFolio = new System.Windows.Forms.Label();
            this.txtFolio = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.lblPayments = new System.Windows.Forms.Label();
            this.dgvPayments = new System.Windows.Forms.DataGridView();
            this.btnAddPayment = new System.Windows.Forms.Button();
            this.btnFinalize = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCheckIns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayments)).BeginInit();
            this.SuspendLayout();
            // 
            // lblActiveCheckIns
            // 
            this.lblActiveCheckIns.AutoSize = true;
            this.lblActiveCheckIns.Location = new System.Drawing.Point(16, 16);
            this.lblActiveCheckIns.Name = "lblActiveCheckIns";
            this.lblActiveCheckIns.Size = new System.Drawing.Size(88, 13);
            this.lblActiveCheckIns.TabIndex = 0;
            this.lblActiveCheckIns.Text = "Active Check-Ins";
            // 
            // dgvCheckIns
            // 
            this.dgvCheckIns.Location = new System.Drawing.Point(16, 36);
            this.dgvCheckIns.Name = "dgvCheckIns";
            this.dgvCheckIns.ReadOnly = true;
            this.dgvCheckIns.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCheckIns.Size = new System.Drawing.Size(760, 160);
            this.dgvCheckIns.TabIndex = 1;
            // 
            // lblFolio
            // 
            this.lblFolio.AutoSize = true;
            this.lblFolio.Location = new System.Drawing.Point(16, 210);
            this.lblFolio.Name = "lblFolio";
            this.lblFolio.Size = new System.Drawing.Size(69, 13);
            this.lblFolio.TabIndex = 2;
            this.lblFolio.Text = "Invoice/Folio";
            // 
            // txtFolio
            // 
            this.txtFolio.Location = new System.Drawing.Point(16, 230);
            this.txtFolio.Name = "txtFolio";
            this.txtFolio.ReadOnly = true;
            this.txtFolio.Size = new System.Drawing.Size(400, 20);
            this.txtFolio.TabIndex = 3;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(430, 210);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(54, 13);
            this.lblTotal.TabIndex = 4;
            this.lblTotal.Text = "Total Due";
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(430, 230);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(160, 20);
            this.txtTotal.TabIndex = 5;
            // 
            // lblPayments
            // 
            this.lblPayments.AutoSize = true;
            this.lblPayments.Location = new System.Drawing.Point(16, 270);
            this.lblPayments.Name = "lblPayments";
            this.lblPayments.Size = new System.Drawing.Size(53, 13);
            this.lblPayments.TabIndex = 6;
            this.lblPayments.Text = "Payments";
            // 
            // dgvPayments
            // 
            this.dgvPayments.Location = new System.Drawing.Point(16, 290);
            this.dgvPayments.Name = "dgvPayments";
            this.dgvPayments.ReadOnly = true;
            this.dgvPayments.Size = new System.Drawing.Size(760, 120);
            this.dgvPayments.TabIndex = 7;
            // 
            // btnAddPayment
            // 
            this.btnAddPayment.Location = new System.Drawing.Point(16, 420);
            this.btnAddPayment.Name = "btnAddPayment";
            this.btnAddPayment.Size = new System.Drawing.Size(75, 23);
            this.btnAddPayment.TabIndex = 8;
            this.btnAddPayment.Text = "Add Payment";
            this.btnAddPayment.Click += new System.EventHandler(this.btnAddPayment_Click);
            // 
            // btnFinalize
            // 
            this.btnFinalize.Location = new System.Drawing.Point(140, 420);
            this.btnFinalize.Name = "btnFinalize";
            this.btnFinalize.Size = new System.Drawing.Size(75, 23);
            this.btnFinalize.TabIndex = 9;
            this.btnFinalize.Text = "Finalize Check-Out";
            this.btnFinalize.Click += new System.EventHandler(this.btnFinalize_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(262, 420);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Close";
            // 
            // CheckOutForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 470);
            this.Controls.Add(this.lblActiveCheckIns);
            this.Controls.Add(this.dgvCheckIns);
            this.Controls.Add(this.lblFolio);
            this.Controls.Add(this.txtFolio);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.lblPayments);
            this.Controls.Add(this.dgvPayments);
            this.Controls.Add(this.btnAddPayment);
            this.Controls.Add(this.btnFinalize);
            this.Controls.Add(this.btnClose);
            this.Name = "CheckOutForm";
            this.Text = "Check-Out / Invoice";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCheckIns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
