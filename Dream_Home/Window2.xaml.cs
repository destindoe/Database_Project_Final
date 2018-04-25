using System;
using System.Collections.Generic;
using System.Data;
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
using System.Data.SqlClient;


namespace Dream_Home
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        //Grab connection
        SqlConnection connection = ConnectionManager.Open(ConnectionStrings.Destin);
        public Window2()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Login().Show();
            this.Close();

        }


        //Refresh/Display Staff Table
        private void DisplayStaff()
        {
            var command2 = new SqlCommand("Select * From Staff", connection);

            SqlDataAdapter da2 = new SqlDataAdapter(command2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            StaffDataGrid.ItemsSource = dt2.DefaultView;
        }

        private void SShowBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayStaff();
        }

        private void StaffSearchbtn_Click(object sender, RoutedEventArgs e)
        {
            if (StaffSearchText.Text != "")
            {
                var command2 = new SqlCommand("Select * From Staff Where " + SCombo.Text + " = '" + StaffSearchText.Text + "'", connection);
                SqlDataAdapter da2 = new SqlDataAdapter(command2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                StaffDataGrid.ItemsSource = dt2.DefaultView;
            }
            else
            {
                MessageBox.Show("Please choose a column and enter a search.", "MESSAGE");
            }
        }

        private void StaffUpdate_Click(object sender, RoutedEventArgs e)
        {
            DataGrid gd = StaffDataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            MessageBoxResult dr = MessageBox.Show("Would you like to update?", "UPDATE", MessageBoxButton.YesNoCancel);

            if (StaffNo.Text != "" && dr == MessageBoxResult.Yes)
            {
                var command = new SqlCommand("Update Staff SET FirstName=@name,LastName=@lname,Position=@pos,DateOfBirth=@dob,Salary=@sal,Sex=@sex,BranchNo=@branchno WHERE StaffNo = @staffno", connection);
                command.Parameters.AddWithValue("@name", StaffName.Text);
                command.Parameters.AddWithValue("@staffno", row_selected["StaffNo"].ToString());
                command.Parameters.AddWithValue("@lname", StaffLName.Text);
                command.Parameters.AddWithValue("@pos", StaffPosition.Text);
                command.Parameters.AddWithValue("@dob", StaffDOB.Text);
                command.Parameters.AddWithValue("@sal", StaffSalary.Text);
                command.Parameters.AddWithValue("@sex", StaffSex.Text);
                command.Parameters.AddWithValue("@branchno", StaffBranch.Text);
                command.ExecuteNonQuery();

                DisplayStaff();
            }
        }

        private void Staffaddbtn_Click(object sender, RoutedEventArgs e)
        {
            var command = new SqlCommand("INSERT INTO STAFF(StaffNo,FirstName,LastName,Position,Sex,DateOfBirth,Salary,BranchNo) VALUES (@staffno,@name,@lname,@pos,@sex,@dob,@sal,@branchno)", connection);


            MessageBoxResult dr = MessageBox.Show("Would you like to add?", "ADD", MessageBoxButton.YesNoCancel);

            if (dr == MessageBoxResult.Yes)
            {
                command.Parameters.AddWithValue("@staffno", StaffNo.Text);
                command.Parameters.AddWithValue("@name", StaffName.Text);
                command.Parameters.AddWithValue("@lname", StaffLName.Text);
                command.Parameters.AddWithValue("@pos", StaffPosition.Text);
                command.Parameters.AddWithValue("@dob", StaffDOB.Text);
                command.Parameters.AddWithValue("@sal", StaffSalary.Text);
                command.Parameters.AddWithValue("@sex", StaffSex.Text);
                command.Parameters.AddWithValue("@branchno", StaffBranch.Text);
                command.ExecuteNonQuery();

                DisplayStaff();
            }


        }

        private void StaffDelete_Click(object sender, RoutedEventArgs e)
        {
            DataGrid gd = StaffDataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            MessageBoxResult dr = MessageBox.Show("Would you like to delete?", "DELETE", MessageBoxButton.YesNoCancel);

            if (row_selected != null && dr == MessageBoxResult.Yes)
            {
                var command = new SqlCommand("DELETE FROM STAFF WHERE StaffNo = @staffno", connection);
                command.Parameters.AddWithValue("@staffno", row_selected["StaffNo"].ToString());
                command.ExecuteNonQuery();

                DisplayStaff();
            }


        }

        private void StaffDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            if (row_selected != null)
            {
                StaffBranch.Text = row_selected["BranchNo"].ToString();
                StaffDOB.SelectedDate = DateTime.Parse(row_selected["DateOfBirth"].ToString());
                StaffName.Text = row_selected["FirstName"].ToString();
                StaffLName.Text = row_selected["LastName"].ToString();
                StaffPosition.Text = row_selected["Position"].ToString();
                StaffSalary.Text = row_selected["Salary"].ToString();
                StaffSex.Text = row_selected["Sex"].ToString();
                StaffNo.Text = row_selected["StaffNo"].ToString();


            }
        }

        //Agent Methods
        private void DisplayAgents()
        {
            var command2 = new SqlCommand("Select * From Agents", connection);

            SqlDataAdapter da2 = new SqlDataAdapter(command2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            AgentsDataGrid.ItemsSource = dt2.DefaultView;
        }

        private void AShowBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayAgents();
        }

        private void AgentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            if (row_selected != null)
            {
                AgentBranch.Text = row_selected["BranchNo"].ToString();
                AgentID.Text = row_selected["AgentID"].ToString();
                AgentName.Text = row_selected["FirstName"].ToString();
                AgentLName.Text = row_selected["LastName"].ToString();
                AgentStaffNo.Text = row_selected["StaffNo"].ToString();
                AgentPhone.Text = row_selected["TelephoneNumber"].ToString();
                AgentCompany.Text = row_selected["Company"].ToString();


            }
        }


        private void AgentSearchbtn_Click(object sender, RoutedEventArgs e)
        {
            if (AgentSearch.Text != "")
            {
                var command2 = new SqlCommand("Select * From Agents Where " + ACombo.Text + " = '" + AgentSearch.Text + "'", connection);
                SqlDataAdapter da2 = new SqlDataAdapter(command2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                AgentsDataGrid.ItemsSource = dt2.DefaultView;
            }
            else
            {
                MessageBox.Show("Please choose a column and enter a search.", "MESSAGE");
            }
        }

        private void AgentDelete_Click(object sender, RoutedEventArgs e)
        {
            DataGrid gd = AgentsDataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            MessageBoxResult dr = MessageBox.Show("Would you like to delete?", "DELETE", MessageBoxButton.YesNoCancel);

            if (row_selected != null && dr == MessageBoxResult.Yes)
            {
                var command = new SqlCommand("DELETE FROM Agents WHERE AgentID = @agentid", connection);
                command.Parameters.AddWithValue("@agentid", row_selected["AgentID"].ToString());
                command.ExecuteNonQuery();

                DisplayAgents();
            }
        }

        private void AgentUpdate_Click(object sender, RoutedEventArgs e)
        {
            DataGrid gd = AgentsDataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            MessageBoxResult dr = MessageBox.Show("Would you like to update?", "UPDATE", MessageBoxButton.YesNoCancel);

            if (AgentID.Text != "" && dr == MessageBoxResult.Yes)
            {
                var command = new SqlCommand("Update Agents SET FirstName=@name,LastName=@lname,Company=@company,BranchNo=@branchno,TelephoneNumber=@phone WHERE AgentID = @agentid", connection);
                command.Parameters.AddWithValue("@name", AgentName.Text);
                command.Parameters.AddWithValue("@lname", AgentLName.Text);
                command.Parameters.AddWithValue("@company", AgentCompany.Text);
                command.Parameters.AddWithValue("@branchno", AgentBranch.Text);
                command.Parameters.AddWithValue("@agentid", row_selected["AgentID"].ToString());
                command.Parameters.AddWithValue("@phone", AgentPhone.Text);
                command.ExecuteNonQuery();

                DisplayAgents();
            }
        }

        private void AgentAddbtn_Click(object sender, RoutedEventArgs e)
        {
            var command = new SqlCommand("INSERT INTO Agents(AgentID,StaffNo,FirstName,LastName,Company,BranchNo,TelephoneNumber) VALUES (@agentid,@staffno,@name,@lname,@company,@branchno,@phone)", connection);


            MessageBoxResult dr = MessageBox.Show("Would you like to add?", "ADD", MessageBoxButton.YesNoCancel);

            if (dr == MessageBoxResult.Yes)
            {
                command.Parameters.AddWithValue("@staffno", AgentStaffNo.Text);
                command.Parameters.AddWithValue("@name", AgentName.Text);
                command.Parameters.AddWithValue("@lname", AgentLName.Text);
                command.Parameters.AddWithValue("@company", AgentCompany.Text);
                command.Parameters.AddWithValue("@branchno", AgentBranch.Text);
                command.Parameters.AddWithValue("@agentid", AgentID.Text);
                command.Parameters.AddWithValue("@phone", AgentPhone.Text);
                command.ExecuteNonQuery();

                DisplayAgents();
            }

        }

        //Client Methods
        private void DisplayClients()
        {
            var command2 = new SqlCommand("Select * From Client", connection);

            SqlDataAdapter da2 = new SqlDataAdapter(command2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            ClientDataGrid.ItemsSource = dt2.DefaultView;
        }

        private void CShowBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayClients();
        }

        private void ClientDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            if (row_selected != null)
            {
                ClientNo.Text = row_selected["ClientNo"].ToString();
                ClientName.Text = row_selected["FirstName"].ToString();
                ClientLName.Text = row_selected["LastName"].ToString();
                ClientPhone.Text = row_selected["TelNo"].ToString();
                ClientMaxRent.Text = row_selected["MaxRent"].ToString();

            }
        }

        private void ClientSearchbtn_Click(object sender, RoutedEventArgs e)
        {
            if (ClientSearch.Text != "")
            {
                var command2 = new SqlCommand("Select * From Client Where " + CCombo.Text + " = '" + ClientSearch.Text + "'", connection);
                SqlDataAdapter da2 = new SqlDataAdapter(command2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                ClientDataGrid.ItemsSource = dt2.DefaultView;
            }
            else
            {
                MessageBox.Show("Please choose a column and enter a search.", "MESSAGE");
            }
        }

        private void ClientAddbtn_Click(object sender, RoutedEventArgs e)
        {

            var command = new SqlCommand("INSERT INTO Client(ClientNo,FirstName,LastName,TelNo,MaxRent) VALUES (@clientno,@name,@lname,@phone,@rent)", connection);


            MessageBoxResult dr = MessageBox.Show("Would you like to add?", "ADD", MessageBoxButton.YesNoCancel);

            if (dr == MessageBoxResult.Yes)
            {
                command.Parameters.AddWithValue("@clientno", ClientNo.Text);
                command.Parameters.AddWithValue("@name", ClientName.Text);
                command.Parameters.AddWithValue("@lname", ClientLName.Text);
                command.Parameters.AddWithValue("@phone", ClientPhone.Text);
                command.Parameters.AddWithValue("@rent", ClientMaxRent.Text);
                command.ExecuteNonQuery();

                DisplayClients();
            }
        }

        private void ClienttDelete_Click(object sender, RoutedEventArgs e)
        {
            DataGrid gd = ClientDataGrid;
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

        private void ClientUpdate_Click(object sender, RoutedEventArgs e)
        {
            DataGrid gd = ClientDataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            MessageBoxResult dr = MessageBox.Show("Would you like to update?", "UPDATE", MessageBoxButton.YesNoCancel);

            if (ClientNo.Text != "" && dr == MessageBoxResult.Yes)
            {
                var command = new SqlCommand("Update Client SET FirstName=@name,LastName=@lname,TelNo=@phone,MaxRent=@rent WHERE ClientNo = @clientno", connection);
                command.Parameters.AddWithValue("@clientno", row_selected["ClientNo"].ToString());
                command.Parameters.AddWithValue("@name", ClientName.Text);
                command.Parameters.AddWithValue("@lname", ClientLName.Text);
                command.Parameters.AddWithValue("@phone", ClientPhone.Text);
                command.Parameters.AddWithValue("@rent", ClientMaxRent.Text);
                command.ExecuteNonQuery();

                DisplayClients();
            }
        }

        //Owner Methods
        private void DisplayOwners()
        {
            var command2 = new SqlCommand("Select * From Owner", connection);

            SqlDataAdapter da2 = new SqlDataAdapter(command2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            OwnerDataGrid.ItemsSource = dt2.DefaultView;
        }

        private void OShowBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayOwners();
        }


        private void OwnerDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            if (row_selected != null)
            {
                OwnerBranch.Text = row_selected["BranchNo"].ToString();
                OwnerNo.Text = row_selected["OwnerNo"].ToString();
                OwnerName.Text = row_selected["FName"].ToString();
                OwnerLName.Text = row_selected["LName"].ToString();
                OwnerAddress.Text = row_selected["Address"].ToString();
                OwnerPhone.Text = row_selected["TeloNo"].ToString();
            }
        }

        private void OwnerSearchbtn_Click(object sender, RoutedEventArgs e)
        {
            if (OwnerSearch.Text != "")
            {
                var command2 = new SqlCommand("Select * From Owner Where " + OCombo.Text + " = '" + OwnerSearch.Text + "'", connection);
                SqlDataAdapter da2 = new SqlDataAdapter(command2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                OwnerDataGrid.ItemsSource = dt2.DefaultView;
            }
            else
            {
                MessageBox.Show("Please choose a column and enter a search.", "MESSAGE");
            }
        }

      

        private void OwnerAddbtn_Click(object sender, RoutedEventArgs e)
        {
            var command = new SqlCommand("INSERT INTO Owner(BranchNo,OwnerNo,FName,LName,Address,TeloNo) VALUES (@branchno,@ownerno,@fname,@lname,@address,@phone)", connection);


            MessageBoxResult dr = MessageBox.Show("Would you like to add?", "ADD", MessageBoxButton.YesNoCancel);

            if (dr == MessageBoxResult.Yes)
            {

                command.Parameters.AddWithValue("@branchno", OwnerBranch.Text);
                command.Parameters.AddWithValue("@ownerno", OwnerNo.Text);
                command.Parameters.AddWithValue("@fname", OwnerName.Text);
                command.Parameters.AddWithValue("@lname", OwnerLName.Text);
                command.Parameters.AddWithValue("@address", OwnerAddress.Text);
                command.Parameters.AddWithValue("@phone", OwnerPhone.Text);
                command.ExecuteNonQuery();

                DisplayOwners();
            }
        }

        private void OwnertDelete_Click(object sender, RoutedEventArgs e)
        {
            DataGrid gd = OwnerDataGrid;
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

        private void OwnerUpdate_Click(object sender, RoutedEventArgs e)
        {
            DataGrid gd = OwnerDataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            MessageBoxResult dr = MessageBox.Show("Would you like to update?", "UPDATE", MessageBoxButton.YesNoCancel);

            if (OwnerNo.Text != "" && dr == MessageBoxResult.Yes)
            {
                var command = new SqlCommand("Update Owner SET BranchNo=@branchno,FName=@fname,LName=@lname,Address=@address,TeloNo=@phone WHERE OwnerNo = @ownerno", connection);

                command.Parameters.AddWithValue("@branchno", OwnerBranch.Text);
                command.Parameters.AddWithValue("@fname", OwnerName.Text);
                command.Parameters.AddWithValue("@lname", OwnerLName.Text);
                command.Parameters.AddWithValue("@address", OwnerAddress.Text);
                command.Parameters.AddWithValue("@phone", OwnerPhone.Text);
                command.Parameters.AddWithValue("@ownerno", row_selected["OwnerNo"].ToString());
                command.ExecuteNonQuery();

                DisplayOwners();
            }
        }


        //Properties for Sale Tab Started

        //Display for Sale table
        private void DisplayPFS()
        {
            var command2 = new SqlCommand("Select * From PropertyForSale", connection);

            SqlDataAdapter da2 = new SqlDataAdapter(command2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            ForSaleDataGrid.ItemsSource = dt2.DefaultView;
        }

        private void SaleShowBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayPFS();
        }

        //Displays in textboxes
        private void ForSaleDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            if (row_selected != null)
            {
                FSPropertyNo.Text = row_selected["PropertyNo"].ToString();
                FSStreet.Text = row_selected["Street"].ToString();
                FSCity.Text = row_selected["City"].ToString();
                FSPost.Text = row_selected["PostCode"].ToString();
                FSType.Text = row_selected["TypeID"].ToString();
                FSRooms.Text = row_selected["Rooms"].ToString();
                FSOwnerNo.Text = row_selected["OwnerNo"].ToString();
                FSState.Text = row_selected["State"].ToString();
                FSCAgent.Text = row_selected["ClosingAgentID"].ToString();
                FSVAgent.Text = row_selected["ViewAgentID"].ToString();
                FSLAgent.Text = row_selected["ListingAgentID"].ToString();

            }
        }

        //Click For sale Search
        private void FSSearchbtn_Click(object sender, RoutedEventArgs e)
        {
            if (FSSearch.Text != "")
            {
                var command2 = new SqlCommand("Select * From PropertyForSale Where " + SaleCombo.Text + " = '" + FSSearch.Text + "'", connection);
                SqlDataAdapter da2 = new SqlDataAdapter(command2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                ForSaleDataGrid.ItemsSource = dt2.DefaultView;
            }
            else
            {
                MessageBox.Show("Please choose a column and enter a search.", "MESSAGE");
            }
        }

        //Add row to For Sale Table
        private void FSAddbtn_Click(object sender, RoutedEventArgs e)
        {
            var command = new SqlCommand("INSERT INTO PropertyForSale(PropertyNo,Street,City,PostCode,TypeID,Rooms,OwnerNo,State,ClosingAgentID,ViewAgentID,ListingAgentID) VALUES (@propertyno,@street,@city,@postcode,@type,@rooms,@ownerno,@state,@closeagentid,@viewagentid,@listagentid)", connection);


            MessageBoxResult dr = MessageBox.Show("Would you like to add?", "ADD", MessageBoxButton.YesNoCancel);

            if (dr == MessageBoxResult.Yes)
            {
                command.Parameters.AddWithValue("@propertyno", FSPropertyNo.Text);
                command.Parameters.AddWithValue("@street", FSStreet.Text);
                command.Parameters.AddWithValue("@city", FSCity.Text);
                command.Parameters.AddWithValue("@postcode", FSPost.Text);
                command.Parameters.AddWithValue("@type", FSType.Text);
                command.Parameters.AddWithValue("@rooms", FSRooms.Text);
                command.Parameters.AddWithValue("@ownerno", FSOwnerNo.Text);
                command.Parameters.AddWithValue("@state", FSState.Text);
                command.Parameters.AddWithValue("@closeagentid", FSCAgent.Text);
                command.Parameters.AddWithValue("@viewagentid", FSVAgent.Text);
                command.Parameters.AddWithValue("@listagentid", FSLAgent.Text);
                command.ExecuteNonQuery();

                DisplayPFS();
            }
        }


        //Delete Row from For Sale table

        private void FSDelete_Click(object sender, RoutedEventArgs e)
        {
            DataGrid gd = ForSaleDataGrid;
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

        private void FSUpdate_Click(object sender, RoutedEventArgs e)
        {

            DataGrid gd = ForSaleDataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            MessageBoxResult dr = MessageBox.Show("Would you like to update?", "UPDATE", MessageBoxButton.YesNoCancel);

            if (FSPropertyNo.Text != "" && dr == MessageBoxResult.Yes)
            {
                var command = new SqlCommand("Update PropertyForSale SET Street=@street,City=@city,PostCode=@postcode,TypeID=@type,Rooms=@rooms,OwnerNo=@ownerno,State=@state,ClosingAgentID=@closeagentid,ViewAgentID=@viewagentid,ListingAgentID=@listagentid WHERE PropertyNo = @propertyno", connection);

                command.Parameters.AddWithValue("@propertyno", row_selected["PropertyNo"].ToString());
                command.Parameters.AddWithValue("@street", FSStreet.Text);
                command.Parameters.AddWithValue("@city", FSCity.Text);
                command.Parameters.AddWithValue("@postcode", FSPost.Text);
                command.Parameters.AddWithValue("@type", FSType.Text);
                command.Parameters.AddWithValue("@rooms", FSRooms.Text);
                command.Parameters.AddWithValue("@ownerno", FSOwnerNo.Text);
                command.Parameters.AddWithValue("@state", FSState.Text);
                command.Parameters.AddWithValue("@closeagentid", FSCAgent.Text);
                command.Parameters.AddWithValue("@viewagentid", FSVAgent.Text);
                command.Parameters.AddWithValue("@listagentid", FSLAgent.Text);
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
            ForRentDataGrid.ItemsSource = dt2.DefaultView;
        }

        private void RShowBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayPFR();
        }


        //Display to textboxes
        private void ForRentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            if (row_selected != null)
            {
                FRProperty.Text = row_selected["PropertyNo"].ToString();
                FRStreet.Text = row_selected["Street"].ToString();
                FRCity.Text = row_selected["City"].ToString();
                FRPost.Text = row_selected["PostCode"].ToString();
                FRType.Text = row_selected["TypeID"].ToString();
                FRRooms.Text = row_selected["Rooms"].ToString();
                FROwner.Text = row_selected["OwnerNo"].ToString();
                FRState.Text = row_selected["State"].ToString();
                FRRent.Text = row_selected["Rent"].ToString();


            }
        }

        //click Search
        private void FRSearchbtn_Click(object sender, RoutedEventArgs e)
        {
            if (FRSearch.Text != "")
            {
                var command2 = new SqlCommand("Select * From PropertyForRent Where " + RCombo.Text + " = '" + FRSearch.Text + "'", connection);
                SqlDataAdapter da2 = new SqlDataAdapter(command2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                ForRentDataGrid.ItemsSource = dt2.DefaultView;
            }
            else
            {
                MessageBox.Show("Please choose a column and enter a search.", "MESSAGE");
            }
        }

        //Add row to for rent
        private void FRAddbtn_Click(object sender, RoutedEventArgs e)
        {
            var command = new SqlCommand("INSERT INTO PropertyForRent(PropertyNo,Street,City,PostCode,TypeID,Rooms,OwnerNo,State,Rent) VALUES (@propertyno,@street,@city,@postcode,@type,@rooms,@ownerno,@state,@rent)", connection);


            MessageBoxResult dr = MessageBox.Show("Would you like to add?", "ADD", MessageBoxButton.YesNoCancel);

            if (dr == MessageBoxResult.Yes)
            {
                command.Parameters.AddWithValue("@propertyno", FRProperty.Text);
                command.Parameters.AddWithValue("@street", FRStreet.Text);
                command.Parameters.AddWithValue("@city", FRCity.Text);
                command.Parameters.AddWithValue("@postcode", FRPost.Text);
                command.Parameters.AddWithValue("@type", FRType.Text);
                command.Parameters.AddWithValue("@rooms", FRRooms.Text);
                command.Parameters.AddWithValue("@ownerno", FROwner.Text);
                command.Parameters.AddWithValue("@state", FRState.Text);
                command.Parameters.AddWithValue("@rent", FRRent.Text);
                command.ExecuteNonQuery();

                DisplayPFR();
            }
        }

        //Delete Row from for rent
        private void FRDelete_Click(object sender, RoutedEventArgs e)
        {

            DataGrid gd = ForRentDataGrid;
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
        private void FRUpdate_Click(object sender, RoutedEventArgs e)
        {

            DataGrid gd = ForRentDataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            MessageBoxResult dr = MessageBox.Show("Would you like to update?", "UPDATE", MessageBoxButton.YesNoCancel);

            if (FRProperty.Text != "" && dr == MessageBoxResult.Yes)
            {
                var command = new SqlCommand("Update PropertyForRent SET Street=@street,City=@city,PostCode=@postcode,TypeID=@type,Rooms=@rooms,OwnerNo=@ownerno,State=@state,Rent=@rent WHERE PropertyNo = @propertyno", connection);

                command.Parameters.AddWithValue("@propertyno", row_selected["PropertyNo"].ToString());
                command.Parameters.AddWithValue("@street", FRStreet.Text);
                command.Parameters.AddWithValue("@city", FRCity.Text);
                command.Parameters.AddWithValue("@postcode", FRPost.Text);
                command.Parameters.AddWithValue("@type", FRType.Text);
                command.Parameters.AddWithValue("@rooms", FRRooms.Text);
                command.Parameters.AddWithValue("@ownerno", FROwner.Text);
                command.Parameters.AddWithValue("@state", FRState.Text);
                command.Parameters.AddWithValue("@rent", FRRent.Text);
                command.ExecuteNonQuery();

                DisplayPFR();
            }
        }

        //For rent tab ENDED

        //Advertisement Tab Started

        //Newspaper tab started

        private void DisplayNewspaper()
        {
            var command2 = new SqlCommand("Select * From Newspaper", connection);

            SqlDataAdapter da2 = new SqlDataAdapter(command2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            NewspaperDataGrid.ItemsSource = dt2.DefaultView;
        }

        private void NewsShowBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayNewspaper();
        }

        //Display to Textboxes
        private void NewspaperDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            if (row_selected != null)
            {
                NProperty.Text = row_selected["PropertyNo"].ToString();
                NAdID.Text = row_selected["AdvertisementID"].ToString();
                NPhone.Text = row_selected["TelNo"].ToString();
                NName.Text = row_selected["NewspaperName"].ToString();
                NContact.Text = row_selected["ContactName"].ToString();

            }
        }

        //Show table
        private void NSearch1_Click(object sender, RoutedEventArgs e)
        {
            if (NSearch.Text != "")
            {
                var command2 = new SqlCommand("Select * From Newspaper Where " + NewsCombo.Text + " = '" + NSearch.Text + "'", connection);
                SqlDataAdapter da2 = new SqlDataAdapter(command2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                NewspaperDataGrid.ItemsSource = dt2.DefaultView;
            }
            else
            {
                MessageBox.Show("Please choose a column and enter a search.", "MESSAGE");
            }
        }

        //Add Newspaper Row
        private void NAdd_Click(object sender, RoutedEventArgs e)
        {
            var command = new SqlCommand("INSERT INTO Newspaper(NewspaperName,TelNo,ContactName,AdvertisementID,PropertyNo) VALUES (@name,@phone,@cname,@adid,@propertyno)", connection);


            MessageBoxResult dr = MessageBox.Show("Would you like to add?", "ADD", MessageBoxButton.YesNoCancel);

            if (dr == MessageBoxResult.Yes)
            {
                command.Parameters.AddWithValue("@name", NName.Text);
                command.Parameters.AddWithValue("@cname", NContact.Text);
                command.Parameters.AddWithValue("@phone", NPhone.Text);
                command.Parameters.AddWithValue("@adid", NAdID.Text);
                command.Parameters.AddWithValue("@propertyno", NProperty.Text);

                command.ExecuteNonQuery();

                DisplayNewspaper();
            }

        }

        //Delete Newspaper
        private void NDelete_Click(object sender, RoutedEventArgs e)
        {
            DataGrid gd = NewspaperDataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            MessageBoxResult dr = MessageBox.Show("Would you like to delete?", "DELETE", MessageBoxButton.YesNoCancel);

            if (row_selected != null && dr == MessageBoxResult.Yes)
            {
                var command = new SqlCommand("DELETE FROM Newspaper WHERE AdvertisementID = @nadid", connection);
                command.Parameters.AddWithValue("@nadid", row_selected["AdvertisementID"].ToString());
                command.ExecuteNonQuery();

                DisplayNewspaper();
            }
        }

        //Update Newspaper

        private void NUpdate_Click(object sender, RoutedEventArgs e)
        {

            DataGrid gd = NewspaperDataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            MessageBoxResult dr = MessageBox.Show("Would you like to update?", "UPDATE", MessageBoxButton.YesNoCancel);

            if (NAdID.Text != "" && dr == MessageBoxResult.Yes)
            {
                var command = new SqlCommand("Update Newspaper SET NewspaperName=@name,TelNo=@phone,ContactName=@cname,PropertyNo=@propertyno WHERE AdvertisementID = @adid", connection);
                command.Parameters.AddWithValue("@name", NName.Text);
                command.Parameters.AddWithValue("@phone", NPhone.Text);
                command.Parameters.AddWithValue("@cname", NContact.Text);
                command.Parameters.AddWithValue("@propertyno", NProperty.Text);
                command.Parameters.AddWithValue("@adid", row_selected["AdvertisementID"].ToString());
                command.ExecuteNonQuery();

                DisplayNewspaper();
            }
        }

        //End of Newspaper tab

        //TV tab started

        //Display TV table
        private void DisplayTV()
        {
            var command2 = new SqlCommand("Select * From TVAdvertisement", connection);

            SqlDataAdapter da2 = new SqlDataAdapter(command2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            TVDataGrid.ItemsSource = dt2.DefaultView;
        }

        private void TVShowBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayTV();
        }

        //Display to Textboxes
        private void TVDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            if (row_selected != null)
            {
                TVProperty.Text = row_selected["PropertyNo"].ToString();
                TVAdID.Text = row_selected["AdvertisementID"].ToString();
                TVContactNo.Text = row_selected["ContactNumber"].ToString();
                TVContactName.Text = row_selected["ContactName"].ToString();
                TVStation.Text = row_selected["TVStation"].ToString();

            }
        }

        //Display table
        private void TVSearchbtn_Click(object sender, RoutedEventArgs e)
        {

            if (TVSearch.Text != "")
            {
                var command2 = new SqlCommand("Select * From TVAdvertisement Where " + TVCombo.Text + " = '" + TVSearch.Text + "'", connection);
                SqlDataAdapter da2 = new SqlDataAdapter(command2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                TVDataGrid.ItemsSource = dt2.DefaultView;
            }
            else
            {
                MessageBox.Show("Please choose a column and enter a search.", "MESSAGE");
            }
        }

        //Add TV Ad
        private void TVAddbtn_Click(object sender, RoutedEventArgs e)
        {

            var command = new SqlCommand("INSERT INTO TVAdvertisement(TVStation,AdvertisementID,ContactName,ContactNumber,PropertyNo) VALUES (@station,@adid,@cname,@phone,@propertyno)", connection);


            MessageBoxResult dr = MessageBox.Show("Would you like to add?", "ADD", MessageBoxButton.YesNoCancel);

            if (dr == MessageBoxResult.Yes)
            {
                command.Parameters.AddWithValue("@station", TVStation.Text);
                command.Parameters.AddWithValue("@cname", TVContactName.Text);
                command.Parameters.AddWithValue("@phone", TVContactNo.Text);
                command.Parameters.AddWithValue("@adid", TVAdID.Text);
                command.Parameters.AddWithValue("@propertyno", TVProperty.Text);

                command.ExecuteNonQuery();

                DisplayTV();
            }

        }

        //TV Update
        private void TVUpdate_Click(object sender, RoutedEventArgs e)
        {
            DataGrid gd = TVDataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            MessageBoxResult dr = MessageBox.Show("Would you like to update?", "UPDATE", MessageBoxButton.YesNoCancel);

            if (TVAdID.Text != "" && dr == MessageBoxResult.Yes)
            {
                var command = new SqlCommand("Update TVAdvertisement SET TVStation=@station,ContactNumber=@phone,ContactName=@cname,PropertyNo=@propertyno WHERE AdvertisementID = @adid", connection);
                command.Parameters.AddWithValue("@station", TVStation.Text);
                command.Parameters.AddWithValue("@phone", TVContactNo.Text);
                command.Parameters.AddWithValue("@cname", TVContactName.Text);
                command.Parameters.AddWithValue("@propertyno", TVProperty.Text);
                command.Parameters.AddWithValue("@adid", row_selected["AdvertisementID"].ToString());
                command.ExecuteNonQuery();

                DisplayTV();
            }
        }

        //TV Delete

        private void TVDelete_Click(object sender, RoutedEventArgs e)
        {
            DataGrid gd = TVDataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            MessageBoxResult dr = MessageBox.Show("Would you like to delete?", "DELETE", MessageBoxButton.YesNoCancel);

            if (row_selected != null && dr == MessageBoxResult.Yes)
            {
                var command = new SqlCommand("DELETE FROM TVAdvertisement WHERE AdvertisementID = @nadid", connection);
                command.Parameters.AddWithValue("@nadid", row_selected["AdvertisementID"].ToString());
                command.ExecuteNonQuery();

                DisplayTV();
            }
        }

        //End of TV tab

        //Start of Internet Tab

        //Display Internet ads
        private void DisplayInternet()
        {
            var command2 = new SqlCommand("Select * From InternetAdvertisements", connection);

            SqlDataAdapter da2 = new SqlDataAdapter(command2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            IDataGrid.ItemsSource = dt2.DefaultView;
        }

        private void IShowBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayInternet();
        }

        //Display to textboxes
        private void IDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            if (row_selected != null)
            {
                IProperty.Text = row_selected["PropertyNo"].ToString();
                IAdID.Text = row_selected["AdvertisementID"].ToString();
                IContactNo.Text = row_selected["ContactNumber"].ToString();
                IContactName.Text = row_selected["ContactName"].ToString();
                IWebsite.Text = row_selected["WebsiteName"].ToString();

            }
        }

        //Search click
        private void ISearchbtn_Click(object sender, RoutedEventArgs e)
        {
            if (ISearch.Text != "")
            {
                var command2 = new SqlCommand("Select * From InternetAdvertisements Where " + ICombo.Text + " = '" + ISearch.Text + "'", connection);
                SqlDataAdapter da2 = new SqlDataAdapter(command2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                IDataGrid.ItemsSource = dt2.DefaultView;
            }
            else
            {
                MessageBox.Show("Please choose a column and enter a search.", "MESSAGE");
            }
        }

        //Add Internet Ad
        private void IAddbtn_Click(object sender, RoutedEventArgs e)
        {
            var command = new SqlCommand("INSERT INTO InternetAdvertisements(WebsiteName,AdvertisementID,ContactName,ContactNumber,PropertyNo) VALUES (@url,@adid,@cname,@phone,@propertyno)", connection);


            MessageBoxResult dr = MessageBox.Show("Would you like to add?", "ADD", MessageBoxButton.YesNoCancel);

            if (dr == MessageBoxResult.Yes)
            {
                command.Parameters.AddWithValue("@url", IWebsite.Text);
                command.Parameters.AddWithValue("@cname", IContactName.Text);
                command.Parameters.AddWithValue("@phone", IContactNo.Text);
                command.Parameters.AddWithValue("@adid", IAdID.Text);
                command.Parameters.AddWithValue("@propertyno", IProperty.Text);

                command.ExecuteNonQuery();

                DisplayInternet();
            }
        }

        //Delete Internet Add

        private void IDelete_Click(object sender, RoutedEventArgs e)
        {
            DataGrid gd = IDataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            MessageBoxResult dr = MessageBox.Show("Would you like to delete?", "DELETE", MessageBoxButton.YesNoCancel);

            if (row_selected != null && dr == MessageBoxResult.Yes)
            {
                var command = new SqlCommand("DELETE FROM InternetAdvertisements WHERE AdvertisementID = @nadid", connection);
                command.Parameters.AddWithValue("@nadid", row_selected["AdvertisementID"].ToString());
                command.ExecuteNonQuery();

                DisplayInternet();
            }
        }

        //Update Internet Ad
        private void IUpdate_Click(object sender, RoutedEventArgs e)
        {
            DataGrid gd = IDataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            MessageBoxResult dr = MessageBox.Show("Would you like to update?", "UPDATE", MessageBoxButton.YesNoCancel);

            if (IAdID.Text != "" && dr == MessageBoxResult.Yes)
            {
                var command = new SqlCommand("Update InternetAdvertisements SET WebsiteName=@url,ContactNumber=@phone,ContactName=@cname,PropertyNo=@propertyno WHERE AdvertisementID = @adid", connection);
                command.Parameters.AddWithValue("@url", IWebsite.Text);
                command.Parameters.AddWithValue("@phone", IContactNo.Text);
                command.Parameters.AddWithValue("@cname", IContactName.Text);
                command.Parameters.AddWithValue("@propertyno", IProperty.Text);
                command.Parameters.AddWithValue("@adid", row_selected["AdvertisementID"].ToString());
                command.ExecuteNonQuery();

                DisplayInternet();
            }
        }

        //End of Internet Tab

        //End of Advertisement Tab

        //Branch Methods

        private void DisplayBranch()
        {
            var command2 = new SqlCommand("Select * From Branch", connection);

            SqlDataAdapter da2 = new SqlDataAdapter(command2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            BDataGrid.ItemsSource = dt2.DefaultView;
        }

        private void BShowBtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayBranch();
        }

        private void BDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            if (row_selected != null)
            {
                BBranch.Text = row_selected["BranchNo"].ToString();
                BManager.Text = row_selected["MgrStaffNo"].ToString();
                BState.Text = row_selected["State"].ToString();
                BCity.Text = row_selected["City"].ToString();
                BStreet.Text = row_selected["StreetNo"].ToString();
                BPost.Text = row_selected["PostCode"].ToString();
            }
        }

        private void BranchSearchbtn_Click(object sender, RoutedEventArgs e)
        {
            if (BSearch.Text != "")
            {
                var command2 = new SqlCommand("Select * From Branch Where " + BCombo.Text + " = '" + BSearch.Text + "'", connection);
                SqlDataAdapter da2 = new SqlDataAdapter(command2);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                BDataGrid.ItemsSource = dt2.DefaultView;
            }
            else
            {
                MessageBox.Show("Please choose a column and enter a search.", "MESSAGE");
            }
        }


        private void BAddbtn_Click(object sender, RoutedEventArgs e)
        {
            var command = new SqlCommand("INSERT INTO Branch(BranchNo,StreetNo,City,PostCode,MgrStaffNo,State) VALUES (@branchno,@streetno,@city,@post,@staffno,@state)", connection);


            MessageBoxResult dr = MessageBox.Show("Would you like to add?", "ADD", MessageBoxButton.YesNoCancel);

            if (dr == MessageBoxResult.Yes)
            {
                command.Parameters.AddWithValue("@staffno", BManager.Text);
                command.Parameters.AddWithValue("@streetno", BStreet.Text);
                command.Parameters.AddWithValue("@city", BCity.Text);
                command.Parameters.AddWithValue("@post", BPost.Text);
                command.Parameters.AddWithValue("@state", BState.Text);
                command.Parameters.AddWithValue("@branchno", BBranch.Text);
                command.ExecuteNonQuery();

                DisplayBranch();
            }


        }

        private void BranchUpdate_Click(object sender, RoutedEventArgs e)
        {
            DataGrid gd = BDataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            MessageBoxResult dr = MessageBox.Show("Would you like to update?", "UPDATE", MessageBoxButton.YesNoCancel);

            if (BBranch.Text != "" && dr == MessageBoxResult.Yes)
            {
                var command = new SqlCommand("Update Branch SET StreetNo=@streetno,City=@city,PostCode=@post,MgrStaffNo=@staffno,State=@state WHERE BranchNo = @branchno", connection);
                command.Parameters.AddWithValue("@staffno", BManager.Text);
                command.Parameters.AddWithValue("@streetno", BStreet.Text);
                command.Parameters.AddWithValue("@city", BCity.Text);
                command.Parameters.AddWithValue("@post", BPost.Text);
                command.Parameters.AddWithValue("@state", BState.Text);
                command.Parameters.AddWithValue("@branchno", row_selected["BranchNo"].ToString());
                command.ExecuteNonQuery();

                DisplayBranch();
            }
        }



        private void BranchDelete_Click(object sender, RoutedEventArgs e)
        {
            DataGrid gd = BDataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            MessageBoxResult dr = MessageBox.Show("Would you like to delete?", "DELETE", MessageBoxButton.YesNoCancel);

            if (row_selected != null && dr == MessageBoxResult.Yes)
            {
                var command = new SqlCommand("DELETE FROM Branch WHERE BranchNo = @branchno", connection);
                command.Parameters.AddWithValue("@branchno", row_selected["BranchNo"].ToString());
                command.ExecuteNonQuery();

                DisplayBranch();
            }
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes(StaffGrid);
        }

        private void NewsClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes(NewsGrid);
        }

        private void AgentClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes(AgentGrid);
        }

        private void ClientClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes(ClientGrid);
        }

        private void OwnerClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes(OwnerGrid);
        }

        private void FSClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes(SaleGrid);
        }

        private void FRClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes(RGrid);
        }

        private void TVClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes(TVGrid);
        }

        private void InternetClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes(InternetGrid);
        }

        private void BranchClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes(BGrid);
        }

        private void ClearTextBoxes(Grid g)
        {
            foreach (Control ctl in g.Children)
            {
                if (ctl.GetType() == typeof(CheckBox))
                    ((CheckBox)ctl).IsChecked = false;
                if (ctl.GetType() == typeof(TextBox))
                    ((TextBox)ctl).Text = String.Empty;
            }

        }

     
    }
}
