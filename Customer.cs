using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SteppingIntoHistoryFinal
{
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
            ShowTenants();
        }
        // Connection String
        SqlConnection Con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\jqgas\OneDrive\Documents\SteppingIntoHistoryDataBase.mdf;Integrated Security = True; Connect Timeout = 30");
        private void ShowTenants()
        {
            Con.Open();
            string Query = "Select * from CustomerTable";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CUstomer_DataGrid.DataSource = ds.Tables[0];
            Con.Close();
        }
        // This code is used to insert customers into the database
        private void save_button_Click(object sender, System.EventArgs e)
        {
            if (Customername_textBox.Text == "" || customergender_comboBox.SelectedIndex == -1 || CustomerPhone_textBox.Text == "")
            {
                MessageBox.Show("Missing Information!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Insert Into CustomerTable(CustomerName, CustomerPhone,CustomerGender,CustomerPassword) values (@CN,@CP,@CG,@CPASS)", Con);
                    cmd.Parameters.AddWithValue("@CN", Customername_textBox.Text);
                    cmd.Parameters.AddWithValue("@CP", CustomerPhone_textBox.Text);
                    cmd.Parameters.AddWithValue("@CG", customergender_comboBox.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@CPASS", Password_textBox1.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Added Successfully!");
                    Con.Close();
                    ShowTenants();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        // This code will display the data from the datagridview in the textboxes
        int Key = 0;
        private void CUstomer_DataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Customername_textBox.Text = CUstomer_DataGrid.SelectedRows[0].Cells[1].Value.ToString();
            CustomerPhone_textBox.Text = CUstomer_DataGrid.SelectedRows[0].Cells[2].Value.ToString();
            customergender_comboBox.Text = CUstomer_DataGrid.SelectedRows[0].Cells[3].Value.ToString();
            if (Customername_textBox.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(CUstomer_DataGrid.SelectedRows[0].Cells[0].Value.ToString());

            }

        }
        //This code will delete customer data from the database
        private void delete_button_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Please Select a Customer!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete From  CustomerTable Where CustomerID = @CKey", Con);
                    cmd.Parameters.AddWithValue("@CKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Deleted Successfully!");
                    Con.Close();
                    // INSERT RESETDATA
                    ShowTenants();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        // This code will Update the customer details in the database
        private void edit_button_Click(object sender, EventArgs e)
        {
            if (Customername_textBox.Text == "" || customergender_comboBox.SelectedIndex == -1 || CustomerPhone_textBox.Text == "")
            {
                MessageBox.Show("Missing Information!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update CustomerTable set CustomerName=@CN, CustomerPhone=@CP,CustomerGender=@CG, CustomerPassword=@CPASS Where CustomerID=@Ckey", Con);
                    cmd.Parameters.AddWithValue("@CN", Customername_textBox.Text);
                    cmd.Parameters.AddWithValue("@CP", CustomerPhone_textBox.Text);
                    cmd.Parameters.AddWithValue("@CG", customergender_comboBox.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@CPASS", Password_textBox1.Text);
                    cmd.Parameters.AddWithValue("@CKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Updated Successfully!");
                    Con.Close();
                    ShowTenants();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void Customer_ThinButton23_Click(object sender, EventArgs e)
        {
            Customer Obj = new Customer();
            Obj.Show();
            this.Hide();

        }

        private void User_ThinButton21_Click(object sender, EventArgs e)
        {
            Users Obj = new Users();
            Obj.Show();
            this.Hide();

        }

        private void Services_ThinButton26_Click(object sender, EventArgs e)
        {
            Services Obj = new Services();
            Obj.Show();
            this.Hide();

        }

        private void Categories_ThinButton24_Click(object sender, EventArgs e)
        {
            Types Obj = new Types();
            Obj.Show();
            this.Hide();

        }

        private void Bookings_ThinButton210_Click(object sender, EventArgs e)
        {
            Bookingscs Obj = new Bookingscs();
            Obj.Show();
            this.Hide();

        }

        private void LogOut_ThinButton22_Click(object sender, EventArgs e)
        {
            LoginPage Obj = new LoginPage();
            Obj.Show();
            this.Hide();

        }
    }
}
