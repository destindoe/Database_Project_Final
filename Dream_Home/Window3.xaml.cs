using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace Dream_Home
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        //Grab connection
        SqlConnection connection = ConnectionManager.Open(ConnectionStrings.Destin);

        public Window3()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Login().Show();
            this.Close();
        }

        //CLIENT TAB STARTED


        //VIEWING TAB START
        private void DisplayViewings()
        {
            var command = new SqlCommand("Select * From Viewing", connection);

            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            SVDataGrid.ItemsSource = dt.DefaultView;
        }

        //VIEWING SELECTION CHANGE
        private void SVDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            if (row_selected != null)
            {
                SVClientNum.Text = row_selected["ClientNo"].ToString();
                SVPropNum.Text = row_selected["PropertyNo"].ToString();
                SVViewDate.SelectedDate = DateTime.Parse(row_selected["ViewDate"].ToString());
                SVAgentID.Text = row_selected["AgentID"].ToString();
                SVComments.Text = row_selected["Comments"].ToString();
            }
        }

        //VIEWING SEARCH
        private void SVSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayViewings();
        }

        //VIEWING ADD
        private void SVAddBtn_Click(object sender, RoutedEventArgs e)
        {
            var command = new SqlCommand("INSERT INTO VIEWING(ClientNo,PropertyNo,ViewDate,Comments,AgentID) VALUES (@clientno,@propertyno,@viewdate,@comments,@agentid)", connection);


            MessageBoxResult dr = MessageBox.Show("Would you like to add?", "ADD", MessageBoxButton.YesNoCancel);

            if (dr == MessageBoxResult.Yes)
            {
                command.Parameters.AddWithValue("@clientno", SVClientNum.Text);
                command.Parameters.AddWithValue("@propertyno", SVPropNum.Text);
                command.Parameters.AddWithValue("@viewdate", SVViewDate.Text);
                command.Parameters.AddWithValue("@comments", SVComments.Text);
                command.Parameters.AddWithValue("@agentid", SVAgentID.Text);
                command.ExecuteNonQuery();

                DisplayViewings();
            }
        }

        //VIEWING DELETE
        private void SVDelete_Click(object sender, RoutedEventArgs e)
        {
            DataGrid gd = SVDataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            MessageBoxResult dr = MessageBox.Show("Would you like to delete?", "DELETE", MessageBoxButton.YesNoCancel);

            if (row_selected != null && dr == MessageBoxResult.Yes)
            {
                var command = new SqlCommand("DELETE FROM Viewing WHERE ClientNo = @clientno", connection);
                command.Parameters.AddWithValue("@clientno", row_selected["ClientNo"].ToString());
                command.ExecuteNonQuery();

                DisplayViewings();
            }
        }

        //VIEWING UPDATE
        private void SVUpdate_Click(object sender, RoutedEventArgs e)
        {
            DataGrid gd = SVDataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            MessageBoxResult dr = MessageBox.Show("Would you like to update?", "UPDATE", MessageBoxButton.YesNoCancel);

            if (SVClientNum.Text != "" && dr == MessageBoxResult.Yes)
            {
                var command = new SqlCommand("Update Viewing SET PropertyNo=@propertyno,ViewDate=@viewdate,Comments=@comments,AgentID=@agentid WHERE ClientNo = @clientno", connection);
                command.Parameters.AddWithValue("@propertyno", SVPropNum.Text);
                command.Parameters.AddWithValue("@clientno", row_selected["ClientNo"].ToString());
                command.Parameters.AddWithValue("@viewdate", SVViewDate.Text);
                command.Parameters.AddWithValue("@comments", SVComments.Text);
                command.Parameters.AddWithValue("@agentid", SVAgentID.Text);
                command.ExecuteNonQuery();

                DisplayViewings();
            }
        }
        //VIEWINGS TAB END

        //LEASE TAB START
        private void DisplayLease()
        {
            var command = new SqlCommand("Select * From Lease", connection);

            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            SLDataGrid.ItemsSource = dt.DefaultView;
        }

        //LEASE SELECTION CHANGE
        private void SLDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            if (row_selected != null)
            {
                SLLeaseNo.Text = row_selected["LeaseNo"].ToString();
                SLPropNo.Text = row_selected["PropertyNo"].ToString();
                SLRentStart.SelectedDate = DateTime.Parse(row_selected["RentStart"].ToString());
                SLRentFinish.SelectedDate = DateTime.Parse(row_selected["RentFinish"].ToString());
                SLDepositPaid.Text = row_selected["DepositPaid"].ToString();
                SLPayMethod.Text = row_selected["PaymentMethod"].ToString();
                SLClientNo.Text = row_selected["ClientNo"].ToString();
            }
        }

        //LEASE SEARCH
        private void SLSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayLease();
        }

        //LEASE ADD
        private void SLAddBtn_Click(object sender, RoutedEventArgs e)
        {
            var command = new SqlCommand("INSERT INTO LEASE(LeaseNo,PropertyNo,RentStart,RentFinish,DepositPaid,PaymentMethod,ClientNo) VALUES (@leaseno,@propertyno,@rentstart,@rentfinish,@depositpaid,@paymentmethod,@clientno)", connection);


            MessageBoxResult dr = MessageBox.Show("Would you like to add?", "ADD", MessageBoxButton.YesNoCancel);

            if (dr == MessageBoxResult.Yes)
            {
                command.Parameters.AddWithValue("@leaseno", SLLeaseNo.Text);
                command.Parameters.AddWithValue("@propertyno", SLPropNo.Text);
                command.Parameters.AddWithValue("@rentstart", SLRentStart.Text);
                command.Parameters.AddWithValue("@rentfinish", SLRentFinish.Text);
                command.Parameters.AddWithValue("@depositpaid", SLDepositPaid.Text);
                command.Parameters.AddWithValue("@paymentmethod", SLPayMethod.Text);
                command.Parameters.AddWithValue("@clientno", SLClientNo.Text);
                command.ExecuteNonQuery();

                DisplayLease();
            }
        }

        //LEASE DELETE
        private void SLDelete_Click(object sender, RoutedEventArgs e)
        {
            DataGrid gd = SLDataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            MessageBoxResult dr = MessageBox.Show("Would you like to delete?", "DELETE", MessageBoxButton.YesNoCancel);

            if (row_selected != null && dr == MessageBoxResult.Yes)
            {
                var command = new SqlCommand("DELETE FROM Lease WHERE LeaseNo = @leaseno", connection);
                command.Parameters.AddWithValue("@leaseno", row_selected["LeaseNo"].ToString());
                command.ExecuteNonQuery();

                DisplayLease();
            }
        }

        //LEASE UPDATE
        private void SLUpdate_Click(object sender, RoutedEventArgs e)
        {
            DataGrid gd = SLDataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            MessageBoxResult dr = MessageBox.Show("Would you like to update?", "UPDATE", MessageBoxButton.YesNoCancel);

            if (SLLeaseNo.Text != "" && dr == MessageBoxResult.Yes)
            {
                var command = new SqlCommand("Update Lease SET ClientNo=@clientno,PropertyNo=@propertyno,RentStart=@rentstart,RentFinish=@rentfinish,DepositPaid=@depositpaid,PaymentMethod=@paymentmethod WHERE LeaseNo = @leaseno", connection);
                command.Parameters.AddWithValue("@leaseno", row_selected["LeaseNo"].ToString());
                command.Parameters.AddWithValue("@propertyno", SLPropNo.Text);
                command.Parameters.AddWithValue("@rentstart", SLRentStart.Text);
                command.Parameters.AddWithValue("@rentfinish", SLRentFinish.Text);
                command.Parameters.AddWithValue("@depositpaid", SLDepositPaid.Text);
                command.Parameters.AddWithValue("@paymentmethod", SLPayMethod.Text);
                command.Parameters.AddWithValue("@clientno", SLClientNo.Text);
                command.ExecuteNonQuery();

                DisplayLease();
            }
        }
        //END LEASE TAB

        //MORTGAGE TAB START
        private void DisplayMortgage()
        {
            var command = new SqlCommand("Select * From Mortgage", connection);

            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            SMDataGrid.ItemsSource = dt.DefaultView;
        }

        //MORTGAGE SELECTION CHANGE
        private void SMDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            if (row_selected != null)
            {
                SMPropNo.Text = row_selected["PropertyNo"].ToString();
                SMFirstName.Text = row_selected["FirstName"].ToString();
                SMLastName.Text = row_selected["LastName"].ToString();
                SMInterest.Text = row_selected["Interest"].ToString();
                SMPrincipal.Text = row_selected["Principal"].ToString();
            }
        }

        //MORTGAGE SEARCH
        private void SMSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayMortgage();
        }

        //MORTGAGE ADD
        private void SMAddBtn_Click(object sender, RoutedEventArgs e)
        {
            var command = new SqlCommand("INSERT INTO Mortgage(PropertyNo,FirstName,LastName,Interest,Principal) VALUES (@propertyno,@firstname,@lastname,@interest,@principal)", connection);


            MessageBoxResult dr = MessageBox.Show("Would you like to add?", "ADD", MessageBoxButton.YesNoCancel);

            if (dr == MessageBoxResult.Yes)
            {
                command.Parameters.AddWithValue("@propertyno", SMPropNo.Text);
                command.Parameters.AddWithValue("@firstname", SMFirstName.Text);
                command.Parameters.AddWithValue("@lastname", SMLastName.Text);
                command.Parameters.AddWithValue("@interest", SMInterest.Text);
                command.Parameters.AddWithValue("@principal", SMPrincipal.Text);
                command.ExecuteNonQuery();

                DisplayMortgage();
            }
        }

        //MORTGAGE DELETE
        private void SMDelete_Click(object sender, RoutedEventArgs e)
        {
            DataGrid gd = SMDataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            MessageBoxResult dr = MessageBox.Show("Would you like to delete?", "DELETE", MessageBoxButton.YesNoCancel);

            if (row_selected != null && dr == MessageBoxResult.Yes)
            {
                var command = new SqlCommand("DELETE FROM Mortgage WHERE PropertyNo = @propertyno", connection);
                command.Parameters.AddWithValue("@propertyno", row_selected["PropertyNo"].ToString());
                command.ExecuteNonQuery();

                DisplayMortgage();
            }
        }

        //MORTGAGE UPDATE
        private void SMUpdate_Click(object sender, RoutedEventArgs e)
        {
            DataGrid gd = SMDataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            MessageBoxResult dr = MessageBox.Show("Would you like to update?", "UPDATE", MessageBoxButton.YesNoCancel);

            if (SMPropNo.Text != "" && dr == MessageBoxResult.Yes)
            {
                var command = new SqlCommand("Update Mortgage SET FirstName=@firstname,LastName=@lastname,Interest=@interest,Principal=@principal WHERE PropertyNo = @propno", connection);
                command.Parameters.AddWithValue("@propno", row_selected["PropertyNo"].ToString());
                command.Parameters.AddWithValue("@firstname", SMFirstName.Text);
                command.Parameters.AddWithValue("@lastname", SMLastName.Text);
                command.Parameters.AddWithValue("@interest", SMInterest.Text);
                command.Parameters.AddWithValue("@principal", SMPrincipal.Text);
                command.ExecuteNonQuery();

                DisplayMortgage();
            }
        }
        //MORTGAGE TAB END
    }
}
