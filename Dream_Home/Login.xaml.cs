using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace Dream_Home
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        //DEBUG
        private Dictionary<string, string> logins = new Dictionary<string, string>();
       

        public Login()
        {
            InitializeComponent();
            //Set username/password for debugging
            logins.Add("admin", "admin");
            logins.Add("Director", "Director");
            logins.Add("Staff", "Staff");
        

            LoginButtonGo.Click += (sender, obj) =>
            {
                var username = LoginTextUsername.Text;
                var pass = LoginTextPassword.Password;

                if (logins.ContainsKey(username))
                {
                    if (logins[username] == pass)
                    {
                        if (username.Equals("Director"))
                        {
                            new Window2().Show();
                            this.Close();
                        }

                        if (username.Equals("Staff"))
                        {
                            new Window3().Show();
                            this.Close();
                        }

                    }

                    else
                    {
                        MessageBox.Show("Invalid username/password!");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid username/password!");
                }







            };
            }
    }
}
