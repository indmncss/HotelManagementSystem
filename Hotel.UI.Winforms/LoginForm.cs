// LoginForm.cs
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hotel.Business.Services;
using Hotel.Data;
using Hotel.Data.UnitOfWork;
using Hotel.UI.Winforms;

namespace Hotel.UI.WinForms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            // Optional: set an in-app logo if you added one to resources
            // this.picLogo.Image = Properties.Resources.logo_placeholder;
            lblMessage.Text = string.Empty;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            await AttemptLoginAsync();
        }

        private async Task AttemptLoginAsync()
        {
            lblMessage.Text = string.Empty;
            btnLogin.Enabled = false;
            btnCancel.Enabled = false;
            txtUsername.Enabled = false;
            txtPassword.Enabled = false;

            try
            {
                var username = txtUsername.Text.Trim();
                var password = txtPassword.Text;

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    lblMessage.Text = "Please enter username and password.";
                    return;
                }

                // Read connection string from App.config
                var conn = ConfigurationManager.ConnectionStrings["HotelDb"].ConnectionString;
                using (var ctx = new HotelDbContext(conn))
                using (var uow = new UnitOfWork(ctx))
                {
                    var auth = new AuthService(uow,
                        maxFailedAttempts: int.Parse(ConfigurationManager.AppSettings["LockoutFailedAttempts"] ?? "5"),
                        lockoutMinutes: int.Parse(ConfigurationManager.AppSettings["LockoutMinutes"] ?? "15"));

                    // Run authentication in background to avoid UI freeze
                    var user = await Task.Run(() => auth.Authenticate(username, password, out string message));

                    if (user != null)
                    {
                        // Success: open main form
                        this.DialogResult = DialogResult.OK;
                        // Store current user in a static session holder (simple approach)
                        Program.CurrentUser = user;
                        this.Close();
                    }
                    else
                    {
                        lblMessage.Text = "Login failed. Check username/password or account status.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error during login. See Logs.";
                Hotel.Common.Logger.LogException(ex);
            }
            finally
            {
                btnLogin.Enabled = true;
                btnCancel.Enabled = true;
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
