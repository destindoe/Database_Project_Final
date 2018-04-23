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

        private void StaffUpdate_Click(object sender, RoutedEventArgs e)
        {
            DataGrid gd = StaffDataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
       
            MessageBoxResult dr = MessageBox.Show("Would you like to update?", "UPDATE", MessageBoxButton.YesNoCancel);

            if (StaffNo.Text != "" && dr == MessageBoxResult.Yes )
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

        private void StaffSearchbtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayStaff();
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
                command.Parameters.AddWithValue("@staffno",row_selected["StaffNo"].ToString());
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
            DisplayAgents();
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
            DisplayClients();
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
            DisplayOwners();
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

        //Branch Methods

        private void DisplayBranch()
        {
            var command2 = new SqlCommand("Select * From Branch", connection);

            SqlDataAdapter da2 = new SqlDataAdapter(command2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            BDataGrid.ItemsSource = dt2.DefaultView;
        }

        private void BranchSearchbtn_Click(object sender, RoutedEventArgs e)
        {
            DisplayBranch();
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
                command.Parameters.AddWithValue("@post",BPost.Text);
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

       
    }
}
