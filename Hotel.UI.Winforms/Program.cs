using System;
using System.Configuration;
using System.Windows.Forms;
using Hotel.Data;
using Hotel.Data.UnitOfWork;
using Hotel.Data.Entities;

namespace Hotel.UI.WinForms
{
    static class Program
    {
        // Simple session holder
        public static User CurrentUser;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Set DataDirectory to the executable folder so |DataDirectory| works
            AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);

            using (var login = new LoginForm())
            {
                var res = login.ShowDialog();
                if (res == DialogResult.OK && CurrentUser != null)
                {
                    Application.Run(new MainForm()); // MainForm you will create later
                }
                else
                {
                    // user cancelled or login failed
                    Application.Exit();
                }
            }
        }
    }
}
