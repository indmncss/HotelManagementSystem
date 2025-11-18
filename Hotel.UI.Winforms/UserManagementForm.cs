// UserManagementForm.cs
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Hotel.Common.Security;

namespace Hotel.UI.WinForms
{
    public partial class UserManagementForm : Form
    {
        public UserManagementForm()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            try
            {
                var conn = ConfigurationManager.ConnectionStrings["HotelDb"].ConnectionString;
                using (var cn = new SqlConnection(conn))
                using (var cmd = new SqlCommand("SELECT UserId, Username, FullName, Email, RoleId, IsActive, CreatedAt, LastLogin FROM Users", cn))
                {
                    var dt = new DataTable();
                    var da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    dgvUsers.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                Hotel.Common.Logger.LogException(ex);
                MessageBox.Show("Error loading users. See logs.");
            }
        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            // small create dialog
            using (var frm = new Form())
            {
                frm.Text = "Create User";
                frm.Width = 420; frm.Height = 300;

                var lblUser = new Label() { Left = 12, Top = 12, Text = "Username" };
                var txtUser = new TextBox() { Left = 12, Top = 32, Width = 360 };

                var lblPass = new Label() { Left = 12, Top = 64, Text = "Password" };
                var txtPass = new TextBox() { Left = 12, Top = 84, Width = 360, UseSystemPasswordChar = true };

                var lblFull = new Label() { Left = 12, Top = 116, Text = "Full name" };
                var txtFull = new TextBox() { Left = 12, Top = 136, Width = 360 };

                var lblEmail = new Label() { Left = 12, Top = 168, Text = "Email" };
                var txtEmail = new TextBox() { Left = 12, Top = 188, Width = 360 };

                var lblRole = new Label() { Left = 12, Top = 220, Text = "RoleId" };
                var cboRole = new ComboBox() { Left = 80, Top = 216, Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };

                var btnOk = new Button() { Left = 250, Top = 216, Text = "Save", DialogResult = DialogResult.OK };

                // populate roles
                var conn = ConfigurationManager.ConnectionStrings["HotelDb"].ConnectionString;
                using (var cn = new SqlConnection(conn))
                using (var cmd = new SqlCommand("SELECT RoleId, RoleName FROM Roles ORDER BY RoleName", cn))
                {
                    var dt = new DataTable();
                    var da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    cboRole.DisplayMember = "RoleName";
                    cboRole.ValueMember = "RoleId";
                    cboRole.DataSource = dt;
                }

                frm.Controls.Add(lblUser); frm.Controls.Add(txtUser);
                frm.Controls.Add(lblPass); frm.Controls.Add(txtPass);
                frm.Controls.Add(lblFull); frm.Controls.Add(txtFull);
                frm.Controls.Add(lblEmail); frm.Controls.Add(txtEmail);
                frm.Controls.Add(lblRole); frm.Controls.Add(cboRole);
                frm.Controls.Add(btnOk);

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var username = txtUser.Text.Trim();
                    var password = txtPass.Text;
                    var fullname = txtFull.Text.Trim();
                    var email = txtEmail.Text.Trim();
                    var roleId = cboRole.SelectedValue;

                    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    {
                        MessageBox.Show("Username and password required.");
                        return;
                    }

                    // hash password
                    var hash = PasswordHelper.HashPassword(password, out string salt);
                    int iterations = 100000;

                    try
                    {
                        using (var cn = new SqlConnection(conn))
                        using (var cmd = new SqlCommand(@"INSERT INTO Users (Username, PasswordHash, PasswordSalt, PasswordIterations, FullName, Email, RoleId, IsActive, CreatedAt)
                                                         VALUES (@u,@h,@s,@it,@fn,@em,@rid,1,GETDATE())", cn))
                        {
                            cmd.Parameters.AddWithValue("@u", username);
                            cmd.Parameters.AddWithValue("@h", hash);
                            cmd.Parameters.AddWithValue("@s", salt);
                            cmd.Parameters.AddWithValue("@it", iterations);
                            cmd.Parameters.AddWithValue("@fn", fullname);
                            cmd.Parameters.AddWithValue("@em", email);
                            cmd.Parameters.AddWithValue("@rid", roleId);

                            cn.Open();
                            cmd.ExecuteNonQuery();
                            cn.Close();
                        }

                        MessageBox.Show("User created.");
                        LoadUsers();
                    }
                    catch (SqlException sqlEx) when (sqlEx.Number == 2601 || sqlEx.Number == 2627)
                    {
                        MessageBox.Show("Username already exists. Choose another username.");
                    }
                    catch (Exception ex)
                    {
                        Hotel.Common.Logger.LogException(ex);
                        MessageBox.Show("Error creating user. See logs.");
                    }
                }
            }
        }
    }
}
