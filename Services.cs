using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace SteppingIntoHistoryFinal
{
    public partial class Services : Form
    {
        public Services()
        {
            InitializeComponent();
            GetCategories();
            GetUser();
            ShowServices();
        }
        SqlConnection Con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\jqgas\OneDrive\Documents\SteppingIntoHistoryDataBase.mdf;Integrated Security = True; Connect Timeout = 30");
        private void GetCategories()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select CategoryID from CategoryTable", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            System.Data.DataTable dt = new DataTable();
            dt.Columns.Add("CategoryID", typeof(int));
            dt.Load(Rdr);
            TyService_combobox.ValueMember = "CategoryID";
            TyService_combobox.DataSource = dt;
            Con.Close();
        }
        private void GetUser()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select Distinct UserID from UsersTable", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            System.Data.DataTable dt = new DataTable();
            dt.Columns.Add("Instructor", typeof(int));
            dt.Load(Rdr);
            Instructor_combobox.ValueMember = "UserID";
            Instructor_combobox.DataSource = dt;
            Con.Close();
        }
        private void ShowServices()
        {
            Con.Open();
            string Query = "Select * from ServicesTable";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            Services_gridview.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void ResetData()
        {
            servicename_textBox.Text = "";
            Instructor_combobox.SelectedIndex = -1;
            servicedetails_textBox.Text = "";
            TyService_combobox.SelectedIndex = -1;
            cost_textbox.Text = "";
        }

        // Insert statement to allow user to insert new rows of data onto the ServiceTable
        private void Save_button_Click(object sender, EventArgs e)
        {
            if (servicename_textBox.Text == "" || TyService_combobox.SelectedIndex == -1 || cost_textbox.Text == "" || Instructor_combobox.SelectedIndex == -1 || servicedetails_textBox.Text == "")
            {
                MessageBox.Show("Missing Information!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Insert Into ServicesTable(ServiceName,ServiceType,ServiceCost,Instructor,ServiceDetails) values (@SN,@ST,@SC,@SU,@SADR)", Con);
                    cmd.Parameters.AddWithValue("@SN", servicename_textBox.Text);
                    cmd.Parameters.AddWithValue("@ST", TyService_combobox.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@SC", cost_textbox.Text);
                    cmd.Parameters.AddWithValue("@SU", Instructor_combobox.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@SADR", servicedetails_textBox.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Service Added Successfully!");
                    Con.Close();
                    ResetData();
                    ShowServices();
                }
                catch (Exception Ex)

                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void Delete_button_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Please Select a Service!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete From ServicesTable Where ServiceID = @SKey", Con);
                    cmd.Parameters.AddWithValue("@SKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Service Deleted Successfully!");
                    Con.Close();
                    ResetData();
                    ShowServices();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        int Key = 0;
        private void Services_gridview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            servicename_textBox.Text = Services_gridview.SelectedRows[0].Cells[1].Value.ToString();
            servicedetails_textBox.Text = Services_gridview.SelectedRows[0].Cells[2].Value.ToString();
            TyService_combobox.Text = Services_gridview.SelectedRows[0].Cells[3].Value.ToString();
            cost_textbox.Text = Services_gridview.SelectedRows[0].Cells[4].Value.ToString();
            Instructor_combobox.Text = Services_gridview.SelectedRows[0].Cells[5].Value.ToString();

            if (servicename_textBox.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(Services_gridview.SelectedRows[0].Cells[0].Value.ToString());

            }
        }
        // Update statement to allow users to update the data in the database
        private void Edit_button_Click(object sender, EventArgs e)
        {
            if (servicename_textBox.Text == "" || TyService_combobox.SelectedIndex == -1 || cost_textbox.Text == "" || Instructor_combobox.SelectedIndex == -1 || servicedetails_textBox.Text == "")
            {
                MessageBox.Show("Missing Information!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update ServicesTable set ServiceName =@SN,ServiceType=@ST,ServiceCost=@SC,Instructor=@SU,ServiceAddress=@SADR Where ServiceID =@SKey)", Con);
                    cmd.Parameters.AddWithValue("@SN", servicename_textBox.Text);
                    cmd.Parameters.AddWithValue("@ST", TyService_combobox.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SC", cost_textbox.Text);
                    cmd.Parameters.AddWithValue("@SU", Instructor_combobox.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@SADR", servicedetails_textBox.Text);
                    cmd.Parameters.AddWithValue("@SKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Service Updated Successfully!");
                    Con.Close();
                    ResetData();
                    ShowServices();
                }
                catch (Exception Ex)

                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            Users Obj = new Users();
            Obj.Show();
            this.Hide();

        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            Customer Obj = new Customer();
            Obj.Show();
            this.Hide();

        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            Types Obj = new Types();
            Obj.Show();
            this.Hide();
        }

        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton210_Click(object sender, EventArgs e)
        {
            Bookingscs Obj = new Bookingscs();
            Obj.Show();
            this.Hide();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            Customer Obj = new Customer();
            Obj.Show();
            this.Hide();
        }
    }
}
