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
        private void DisplayClients()
        {
            var command2 = new SqlCommand("Select * From Client", connection);

            SqlDataAdapter da2 = new SqlDataAdapter(command2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            SCDataGrid.ItemsSource = dt2.DefaultView;
        }

        private void SCShowBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayClients();
        }
        //CLIENT SELECTION CHANGE
        private void SCDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            if (row_selected != null)
            {
                SCClientNo.Text = row_selected["ClientNo"].ToString();
                SCFirstName.Text = row_selected["FirstName"].ToString();
                SCLastName.Text = row_selected["LastName"].ToString();
                SCPhone.Text = row_selected["TelNo"].ToString();
                SCMaxRent.Text = row_selected["MaxRent"].ToString();

            }
        }

        //CLIENT SEARCH
        private void SCSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SCSearchBox.Text != "")
            {
                var command2 = new SqlCommand("Select * From Client Where " + SCCombo.Text + " = '" + SCSearchBox.Text + "'", connection);
                SqlDataAdapter da2 = new SqlDataAdapter(command2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                SCDataGrid.ItemsSource = dt2.DefaultView;
            }
            else
            {
                MessageBox.Show("Please choose a column and enter a search.", "MESSAGE");
            }
        }

        //CLIENT ADD
        private void SCAddBtn_Click(object sender, RoutedEventArgs e)
        {

            var command = new SqlCommand("INSERT INTO Client(ClientNo,FirstName,LastName,TelNo,MaxRent) VALUES (@clientno,@name,@lname,@phone,@rent)", connection);


            MessageBoxResult dr = MessageBox.Show("Would you like to add?", "ADD", MessageBoxButton.YesNoCancel);

            if (dr == MessageBoxResult.Yes)
            {
                command.Parameters.AddWithValue("@clientno", SCClientNo.Text);
                command.Parameters.AddWithValue("@name", SCFirstName.Text);
                command.Parameters.AddWithValue("@lname", SCLastName.Text);
                command.Parameters.AddWithValue("@phone", SCPhone.Text);
                command.Parameters.AddWithValue("@rent", SCMaxRent.Text);
                command.ExecuteNonQuery();

                DisplayClients();
            }
        }

        //CLIENT DELETE
        private void SCDelete_Click(object sender, RoutedEventArgs e)
        {
            DataGrid gd = SCDataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            MessageBoxResult dr = MessageBox.Show("Would you like to delete?", "DELETE", MessageBoxButton.YesNoCancel);

            if (row_selected != null && dr == MessageBoxResult.Yes)
            {
                var command = new SqlCommand("DELETE FROM Client WHERE ClientNo = @clientno", connection);
                command.Parameters.AddWithValue("@clientno", row_selected["ClientNo"].ToString());
                command.ExecuteNonQuery();

                DisplayClients();
            }
        }

        //CLIENT UPDATE
        private void SCUpdate_Click(object sender, RoutedEventArgs e)
        {
            DataGrid gd = SCDataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            MessageBoxResult dr = MessageBox.Show("Would you like to update?", "UPDATE", MessageBoxButton.YesNoCancel);

            if (SCClientNo.Text != "" && dr == MessageBoxResult.Yes)
            {
                var command = new SqlCommand("Update Client SET FirstName=@name,LastName=@lname,TelNo=@phone,MaxRent=@rent WHERE ClientNo = @clientno", connection);
                command.Parameters.AddWithValue("@clientno", row_selected["ClientNo"].ToString());
                command.Parameters.AddWithValue("@name", SCFirstName.Text);
                command.Parameters.AddWithValue("@lname", SCLastName.Text);
                command.Parameters.AddWithValue("@phone", SCPhone.Text);
                command.Parameters.AddWithValue("@rent", SCMaxRent.Text);
                command.ExecuteNonQuery();

                DisplayClients();
            }
        }
        //CLIENT TAB END

        //OWNER TAB START
        private void DisplayOwners()
        {
            var command2 = new SqlCommand("Select * From Owner", connection);

            SqlDataAdapter da2 = new SqlDataAdapter(command2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            SODataGrid.ItemsSource = dt2.DefaultView;
        }

        //RESET DATA GRID TO SELECT *
        private void SOShowBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayOwners();
        }

        //OWNER SELECTION CHANGED
        private void SODataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            if (row_selected != null)
            {
                SOBranchNum.Text = row_selected["BranchNo"].ToString();
                SOOwnerNum.Text = row_selected["OwnerNo"].ToString();
                SOFirstName.Text = row_selected["FName"].ToString();
                SOLastName.Text = row_selected["LName"].ToString();
                SOAddress.Text = row_selected["Address"].ToString();
                SOPhone.Text = row_selected["TeloNo"].ToString();
            }
        }

        //OWNER SEARCH
        private void SOSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SOSearchBox.Text != "")
            {
                var command2 = new SqlCommand("Select * From Owner Where " + SOCombo.Text + " = '" + SOSearchBox.Text + "'", connection);
                SqlDataAdapter da2 = new SqlDataAdapter(command2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                SODataGrid.ItemsSource = dt2.DefaultView;
            }
            else
            {
                MessageBox.Show("Please choose a column and enter a search.", "MESSAGE");
            }
        }

        //OWNER ADD
        private void SOAddBtn_Click(object sender, RoutedEventArgs e)
        {
            var command = new SqlCommand("INSERT INTO Owner(BranchNo,OwnerNo,FName,LName,Address,TeloNo) VALUES (@branchno,@ownerno,@fname,@lname,@address,@phone)", connection);


            MessageBoxResult dr = MessageBox.Show("Would you like to add?", "ADD", MessageBoxButton.YesNoCancel);

            if (dr == MessageBoxResult.Yes)
            {

                command.Parameters.AddWithValue("@branchno", SOBranchNum.Text);
                command.Parameters.AddWithValue("@ownerno", SOOwnerNum.Text);
                command.Parameters.AddWithValue("@fname", SOFirstName.Text);
                command.Parameters.AddWithValue("@lname", SOLastName.Text);
                command.Parameters.AddWithValue("@address", SOAddress.Text);
                command.Parameters.AddWithValue("@phone", SOPhone.Text);
                command.ExecuteNonQuery();

                DisplayOwners();
            }
        }

        //OWNER DELETE
        private void SODelete_Click(object sender, RoutedEventArgs e)
        {
            DataGrid gd = SODataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            MessageBoxResult dr = MessageBox.Show("Would you like to delete?", "DELETE", MessageBoxButton.YesNoCancel);

            if (row_selected != null && dr == MessageBoxResult.Yes)
            {
                var command = new SqlCommand("DELETE FROM Owner WHERE OwnerNo = @ownerno", connection);
                command.Parameters.AddWithValue("@ownerno", row_selected["OwnerNo"].ToString());
                command.ExecuteNonQuery();

                DisplayOwners();
            }
        }

        //OWNER UPDATE
        private void SOUpdate_Click(object sender, RoutedEventArgs e)
        {
            DataGrid gd = SODataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            MessageBoxResult dr = MessageBox.Show("Would you like to update?", "UPDATE", MessageBoxButton.YesNoCancel);

            if (SOOwnerNum.Text != "" && dr == MessageBoxResult.Yes)
            {
                var command = new SqlCommand("Update Owner SET BranchNo=@branchno,FName=@fname,LName=@lname,Address=@address,TeloNo=@phone WHERE OwnerNo = @ownerno", connection);

                command.Parameters.AddWithValue("@branchno", SOBranchNum.Text);
                command.Parameters.AddWithValue("@fname", SOFirstName.Text);
                command.Parameters.AddWithValue("@lname", SOLastName.Text);
                command.Parameters.AddWithValue("@address", SOAddress.Text);
                command.Parameters.AddWithValue("@phone", SOPhone.Text);
                command.Parameters.AddWithValue("@ownerno", row_selected["OwnerNo"].ToString());
                command.ExecuteNonQuery();

                DisplayOwners();
            }
        }
        //OWNER TAB END

        //PROP FOR RENT TAB BEGIN
        //DISPLAY
        private void DisplayPFS()
        {
            var command2 = new SqlCommand("Select * From PropertyForSale", connection);

            SqlDataAdapter da2 = new SqlDataAdapter(command2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            SSDataGrid.ItemsSource = dt2.DefaultView;
        }

        private void SSShowBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayPFS();
        }

        //Displays in textboxes
        private void SSDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            if (row_selected != null)
            {
                SSPropNum.Text = row_selected["PropertyNo"].ToString();
                SSStreet.Text = row_selected["Street"].ToString();
                SSCity.Text = row_selected["City"].ToString();
                SSPostCode.Text = row_selected["PostCode"].ToString();
                SSTypeID.Text = row_selected["TypeID"].ToString();
                SSRooms.Text = row_selected["Rooms"].ToString();
                SSOwnerNum.Text = row_selected["OwnerNo"].ToString();
                SSState.Text = row_selected["State"].ToString();
                SSCloseID.Text = row_selected["ClosingAgentID"].ToString();
                SSViewID.Text = row_selected["ViewAgentID"].ToString();
                SSListID.Text = row_selected["ListingAgentID"].ToString();

            }
        }

        //Click For sale Search
        private void SSSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SSSearchBox.Text != "")
            {
                var command2 = new SqlCommand("Select * From PropertyForSale Where " + SSCombo.Text + " = '" + SSSearchBox.Text + "'", connection);
                SqlDataAdapter da2 = new SqlDataAdapter(command2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                SSDataGrid.ItemsSource = dt2.DefaultView;
            }
            else
            {
                MessageBox.Show("Please choose a column and enter a search.", "MESSAGE");
            }
        }

        //Add row to For Sale Table
        private void SSAddBtn_Click(object sender, RoutedEventArgs e)
        {
            var command = new SqlCommand("INSERT INTO PropertyForSale(PropertyNo,Street,City,PostCode,TypeID,Rooms,OwnerNo,State,ClosingAgentID,ViewAgentID,ListingAgentID) VALUES (@propertyno,@street,@city,@postcode,@type,@rooms,@ownerno,@state,@closeagentid,@viewagentid,@listagentid)", connection);


            MessageBoxResult dr = MessageBox.Show("Would you like to add?", "ADD", MessageBoxButton.YesNoCancel);

            if (dr == MessageBoxResult.Yes)
            {
                command.Parameters.AddWithValue("@propertyno", SSPropNum.Text);
                command.Parameters.AddWithValue("@street", SSStreet.Text);
                command.Parameters.AddWithValue("@city", SSCity.Text);
                command.Parameters.AddWithValue("@postcode", SSPostCode.Text);
                command.Parameters.AddWithValue("@type", SSTypeID.Text);
                command.Parameters.AddWithValue("@rooms", SSRooms.Text);
                command.Parameters.AddWithValue("@ownerno", SSOwnerNum.Text);
                command.Parameters.AddWithValue("@state", SSState.Text);
                command.Parameters.AddWithValue("@closeagentid", SSCloseID.Text);
                command.Parameters.AddWithValue("@viewagentid", SSViewID.Text);
                command.Parameters.AddWithValue("@listagentid", SSListID.Text);
                command.ExecuteNonQuery();

                DisplayPFS();
            }
        }


        //Delete Row from For Sale table

        private void SSDelete_Click(object sender, RoutedEventArgs e)
        {
            DataGrid gd = SSDataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            MessageBoxResult dr = MessageBox.Show("Would you like to delete?", "DELETE", MessageBoxButton.YesNoCancel);

            if (row_selected != null && dr == MessageBoxResult.Yes)
            {
                var command = new SqlCommand("DELETE FROM PropertyForSale WHERE PropertyNo = @propertyno", connection);
                command.Parameters.AddWithValue("@propertyno", row_selected["PropertyNo"].ToString());
                command.ExecuteNonQuery();

                DisplayPFS();
            }
        }

        //Update Row for Sale table

        private void SSUpdate_Click(object sender, RoutedEventArgs e)
        {

            DataGrid gd = SSDataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            MessageBoxResult dr = MessageBox.Show("Would you like to update?", "UPDATE", MessageBoxButton.YesNoCancel);

            if (SSPropNum.Text != "" && dr == MessageBoxResult.Yes)
            {
                var command = new SqlCommand("Update PropertyForSale SET Street=@street,City=@city,PostCode=@postcode,TypeID=@type,Rooms=@rooms,OwnerNo=@ownerno,State=@state,ClosingAgentID=@closeagentid,ViewAgentID=@viewagentid,ListingAgentID=@listagentid WHERE PropertyNo = @propertyno", connection);

                command.Parameters.AddWithValue("@propertyno", row_selected["PropertyNo"].ToString());
                command.Parameters.AddWithValue("@street", SSStreet.Text);
                command.Parameters.AddWithValue("@city", SSCity.Text);
                command.Parameters.AddWithValue("@postcode", SSPostCode.Text);
                command.Parameters.AddWithValue("@type", SSTypeID.Text);
                command.Parameters.AddWithValue("@rooms", SSRooms.Text);
                command.Parameters.AddWithValue("@ownerno", SSOwnerNum.Text);
                command.Parameters.AddWithValue("@state", SSState.Text);
                command.Parameters.AddWithValue("@closeagentid", SSCloseID.Text);
                command.Parameters.AddWithValue("@viewagentid", SSViewID.Text);
                command.Parameters.AddWithValue("@listagentid", SSListID.Text);
                command.ExecuteNonQuery();

                DisplayPFS();
            }
        }
        //Properties for Sale Tab Ended

        //For Rent Properties tab started
        //Update for Rent table
        private void DisplayPFR()
        {
            var command2 = new SqlCommand("Select * From PropertyForRent", connection);

            SqlDataAdapter da2 = new SqlDataAdapter(command2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            SRDataGrid.ItemsSource = dt2.DefaultView;
        }

        //Display to textboxes
        private void SRDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            if (row_selected != null)
            {
                SRPropNum.Text = row_selected["PropertyNo"].ToString();
                SRStreet.Text = row_selected["Street"].ToString();
                SRCity.Text = row_selected["City"].ToString();
                SRPostCode.Text = row_selected["PostCode"].ToString();
                SRTypeID.Text = row_selected["TypeID"].ToString();
                SRRooms.Text = row_selected["Rooms"].ToString();
                SROwnerNum.Text = row_selected["OwnerNo"].ToString();
                SRState.Text = row_selected["State"].ToString();
                SRRent.Text = row_selected["Rent"].ToString();


            }
        }

        private void SRShowBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayPFR();
        }

        //click Search
        private void SRSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SRSearchBox.Text != "")
            {
                var command2 = new SqlCommand("Select * From PropertyForRent Where " + SRCombo.Text + " = '" + SRSearchBox.Text + "'", connection);
                SqlDataAdapter da2 = new SqlDataAdapter(command2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                SRDataGrid.ItemsSource = dt2.DefaultView;
            }
            else
            {
                MessageBox.Show("Please choose a column and enter a search.", "MESSAGE");
            }
        }

        //Add row to for rent
        private void SRAddBtn_Click(object sender, RoutedEventArgs e)
        {
            var command = new SqlCommand("INSERT INTO PropertyForRent(PropertyNo,Street,City,PostCode,TypeID,Rooms,OwnerNo,State,Rent) VALUES (@propertyno,@street,@city,@postcode,@type,@rooms,@ownerno,@state,@rent)", connection);


            MessageBoxResult dr = MessageBox.Show("Would you like to add?", "ADD", MessageBoxButton.YesNoCancel);

            if (dr == MessageBoxResult.Yes)
            {
                command.Parameters.AddWithValue("@propertyno", SRPropNum.Text);
                command.Parameters.AddWithValue("@street", SRStreet.Text);
                command.Parameters.AddWithValue("@city", SRCity.Text);
                command.Parameters.AddWithValue("@postcode", SRPostCode.Text);
                command.Parameters.AddWithValue("@type", SRTypeID.Text);
                command.Parameters.AddWithValue("@rooms", SRRooms.Text);
                command.Parameters.AddWithValue("@ownerno", SROwnerNum.Text);
                command.Parameters.AddWithValue("@state", SRState.Text);
                command.Parameters.AddWithValue("@rent", SRRent.Text);
                command.ExecuteNonQuery();

                DisplayPFR();
            }
        }

        //Delete Row from for rent
        private void SRDelete_Click(object sender, RoutedEventArgs e)
        {

            DataGrid gd = SRDataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            MessageBoxResult dr = MessageBox.Show("Would you like to delete?", "DELETE", MessageBoxButton.YesNoCancel);

            if (row_selected != null && dr == MessageBoxResult.Yes)
            {
                var command = new SqlCommand("DELETE FROM PropertyForRent WHERE PropertyNo = @propertyno", connection);
                command.Parameters.AddWithValue("@propertyno", row_selected["PropertyNo"].ToString());
                command.ExecuteNonQuery();

                DisplayPFR();
            }
        }

        //Update For Rent Properties
        private void SRUpdate_Click(object sender, RoutedEventArgs e)
        {

            DataGrid gd = SRDataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            MessageBoxResult dr = MessageBox.Show("Would you like to update?", "UPDATE", MessageBoxButton.YesNoCancel);

            if (SRPropNum.Text != "" && dr == MessageBoxResult.Yes)
            {
                var command = new SqlCommand("Update PropertyForRent SET Street=@street,City=@city,PostCode=@postcode,TypeID=@type,Rooms=@rooms,OwnerNo=@ownerno,State=@state,Rent=@rent WHERE PropertyNo = @propertyno", connection);

                command.Parameters.AddWithValue("@propertyno", row_selected["PropertyNo"].ToString());
                command.Parameters.AddWithValue("@street", SRStreet.Text);
                command.Parameters.AddWithValue("@city", SRCity.Text);
                command.Parameters.AddWithValue("@postcode", SRPostCode.Text);
                command.Parameters.AddWithValue("@type", SRTypeID.Text);
                command.Parameters.AddWithValue("@rooms", SRRooms.Text);
                command.Parameters.AddWithValue("@ownerno", SROwnerNum.Text);
                command.Parameters.AddWithValue("@state", SRState.Text);
                command.Parameters.AddWithValue("@rent", SRRent.Text);
                command.ExecuteNonQuery();

                DisplayPFR();
            }
        }

        //For rent tab ENDED


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

        private void SVShowBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayViewings();
        }

        //VIEWING SEARCH
        private void SVSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SVSearchBox.Text != "")
            {
                var command2 = new SqlCommand("Select * From Viewing Where " + SVCombo.Text + " = '" + SVSearchBox.Text + "'", connection);
                SqlDataAdapter da2 = new SqlDataAdapter(command2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                SVDataGrid.ItemsSource = dt2.DefaultView;
            }
            else
            {
                MessageBox.Show("Please choose a column and enter a search.", "MESSAGE");
            }
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

        private void SLShowBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayLease();
        }

        //LEASE SEARCH
        private void SLSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SLSearchBox.Text != "")
            {
                var command2 = new SqlCommand("Select * From Lease Where " + SLCombo.Text + " = '" + SLSearchBox.Text + "'", connection);
                SqlDataAdapter da2 = new SqlDataAdapter(command2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                SLDataGrid.ItemsSource = dt2.DefaultView;
            }
            else
            {
                MessageBox.Show("Please choose a column and enter a search.", "MESSAGE");
            }
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

        private void SMShowBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayMortgage();
        }

        //MORTGAGE SEARCH
        private void SMSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SMSearchBox.Text != "")
            {
                var command2 = new SqlCommand("Select * From Mortgage Where " + SMCombo.Text + " = '" + SMSearchBox.Text + "'", connection);
                SqlDataAdapter da2 = new SqlDataAdapter(command2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                SMDataGrid.ItemsSource = dt2.DefaultView;
            }
            else
            {
                MessageBox.Show("Please choose a column and enter a search.", "MESSAGE");
            }
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
