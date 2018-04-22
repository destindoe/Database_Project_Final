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
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Dream_Home
{
    /// <summary>
    /// Interaction logic for DirectorWindow.xaml
    /// </summary>
    public partial class DirectorWindow : Window
    {
        //Grab connection
         SqlConnection connection = ConnectionManager.Open(ConnectionStrings.Destin);

        public DirectorWindow()
        {
            InitializeComponent();
          
            


        }

        private void Director_SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            var command = new SqlCommand("Select * From " + Director_CB.Text, connection);

            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable(Director_CB.Text);
            da.Fill(dt);
            DData.ItemsSource = dt.DefaultView;

        }

        
    }
}
