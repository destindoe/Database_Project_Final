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

        private void StaffSearchbtn_Click(object sender, RoutedEventArgs e)
        {
            var command = new SqlCommand("Select * From Staff", connection);

            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);

            StaffDataGrid.ItemsSource = dt.DefaultView;
        }

        private void Staffaddbtn_Click(object sender, RoutedEventArgs e)
        {
            var command = new SqlCommand("Select * From Staff", connection);

            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);

            var newRow = dt.NewRow();
            newRow["FirstName"] = StaffName.Text;
            newRow["LastName"] = StaffLName.Text;
            newRow["Position"] = StaffPosition.Text;
            newRow["Sex"] = StaffSex.Text;
            newRow["DateOfBirth"] = StaffDOB.SelectedDate;
            newRow["Salary"] = decimal.Parse(StaffSalary.Text);
            newRow["BranchNo"] = StaffBranch.Text;
            newRow["StaffNo"] = StaffNo.Text;
            dt.Rows.Add(newRow);

            new SqlCommandBuilder(da);
            da.Update(dt);
            StaffDataGrid.ItemsSource = dt.DefaultView;
        }

        private void StaffDelete_Click(object sender, RoutedEventArgs e)
        {
            DataGrid gd = StaffDataGrid;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            if (row_selected != null)
            {
                var command = new SqlCommand("DELETE FROM STAFF WHERE StaffNo = " + row_selected["StaffNo"].ToString(), connection);
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);



            }

        }

        private void BAddbtn_Click(object sender, RoutedEventArgs e)
        {
            var command = new SqlCommand("Select * From Branch", connection);

            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);

            var newRow = dt.NewRow();
            newRow["BranchNo"] = BBranch.Text;
            newRow["ManagerNo"] = BManager.Text;
            newRow["State"] = BState.Text;
            newRow["City"] = BCity.Text;
            newRow["Street"] = BStreet.Text;
            dt.Rows.Add(newRow);

            new SqlCommandBuilder(da);
            da.Update(dt);
            BDataGrid.ItemsSource = dt.DefaultView;

        }

        private void BranchSearchbtn_Click(object sender, RoutedEventArgs e)
        {
            var command = new SqlCommand("Select * From Branch", connection);

            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);

            BDataGrid.ItemsSource = dt.DefaultView;
        }

        private void BDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

            if (row_selected != null)
            {
                BBranch.Text = row_selected["BranchNo"].ToString();
                BManager.Text = row_selected["ManagerNo"].ToString();
                BState.Text = row_selected["State"].ToString();
                BCity.Text = row_selected["City"].ToString();
                BStreet.Text = row_selected["Street"].ToString();
            }
        }

       
    }
}
