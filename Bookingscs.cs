using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SteppingIntoHistoryFinal
{
    public partial class Bookingscs : Form
    {
        public Bookingscs()
        {
            InitializeComponent();
            GetService();
            GetCustomer();
            ResetData();
            ShowBookings();

        }

        SqlConnection Con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\jqgas\OneDrive\Documents\SteppingIntoHistoryDataBase.mdf;Integrated Security = True; Connect Timeout = 30");
        private void GetService()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select CategoryID from CategoryTable", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            System.Data.DataTable dt = new DataTable();
            dt.Columns.Add("CategoryID", typeof(int));
            dt.Load(Rdr);
            typeservice_comboBox1.DataSource = dt;
            typeservice_comboBox1.ValueMember = "CategoryID";
            typeservice_comboBox1.DisplayMember = "CategoryID";
            Con.Close();
        }
        private void ShowBookings()
        {
            Con.Open();
            string Query = "Select * from BookingsTable";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new System.Data.DataSet();
            sda.Fill(ds);
            booking_datagrid.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void GetCost()
        {
            Con.Open();
            String Query = "Select * from CategoryTable where CategoryID =" + typeservice_comboBox1.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Cost_textBox.Text = dr["CategoryCost"].ToString();
            }
            Con.Close();

        }
        private void GetCustomer()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select CustomerID from CustomerTable", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            System.Data.DataTable dt = new DataTable();
            dt.Columns.Add("CustomerID", typeof(int));
            dt.Load(Rdr);
            customer_comboBox2.DataSource = dt;
            customer_comboBox2.ValueMember = "CustomerID";
            customer_comboBox2.DisplayMember = "CustomerID";
            Con.Close();
        }
        private void ResetData()
        {
            Cost_textBox.Text = "";
        }
        // Insert statement to allow user to insert new rows of data
        private void book_bunifuThinButton27_Click(object sender, System.EventArgs e)
        {
            if (Cost_textBox.Text == "" || typeservice_comboBox1.SelectedIndex == -1 || customer_comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information!");
            }
            else
            {
                try
                {
                    string Period = bookingdate_picker.Value.Date.Month + "-" + bookingdate_picker.Value.Date.Year;
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Insert Into BookingsTable(ServiceType,Customer,BookingDate,Cost) values (@BS,@BC,@BD,@BCST)", Con);
                    cmd.Parameters.AddWithValue("@BS", typeservice_comboBox1.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@BC", customer_comboBox2.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@BD", Period);
                    cmd.Parameters.AddWithValue("@BCST", Cost_textBox.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Booking Added Successfully!");
                    Con.Close();
                    ResetData();
                    ShowBookings();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void typeservice_comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCost();
        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            Types Obj = new Types();
            Obj.Show();
            this.Hide();

        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            Customer Obj = new Customer();
            Obj.Show();
            this.Hide();

        }

        private void Users_hinButton21_Click(object sender, EventArgs e)
        {
            Users Obj = new Users();
            Obj.Show();
            this.Hide();

        }

        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {
            Services Obj = new Services();
            Obj.Show();
            this.Hide();

        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            LoginPage Obj = new LoginPage();
            Obj.Show();
            this.Hide();

        }
    }

    internal class DataTable : System.Data.DataTable
    {
    }
}
