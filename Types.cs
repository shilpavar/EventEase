using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SteppingIntoHistoryFinal
{
    public partial class Types : Form
    {
        public Types()
        {
            InitializeComponent();
            ShowCategories();
            ResetData();
        }
        // Connection String
        SqlConnection Con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\jqgas\OneDrive\Documents\SteppingIntoHistoryDataBase.mdf;Integrated Security = True; Connect Timeout = 30");
        private void ShowCategories()
        {
            Con.Open();
            string Query = "Select * from CategoryTable";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            categories_DataGrid.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void ResetData()
        {
            Categoryname_textBox.Text = "";
            categorydetails_textBox.Text = "";
        }
        // Insert statement to allow user to insert new rows of data
        private void Save_Button_Click(object sender, System.EventArgs e)
        {
            if (Categoryname_textBox.Text == "" || categorydetails_textBox.Text == "")
            {
                MessageBox.Show("Missing Information!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Insert Into CategoryTable(CategoryName, CategoryCost) values (@TN,@TD)", Con);
                    cmd.Parameters.AddWithValue("@TN", Categoryname_textBox.Text);
                    cmd.Parameters.AddWithValue("@TD", categorydetails_textBox.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("New Gategory Added Successfully!");
                    Con.Close();
                    ShowCategories();
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

        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {
            Services Obj = new Services();
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
            LoginPage Obj = new LoginPage();
            Obj.Show();
            this.Hide();
        }
        int Key = 0;

        private void delete_button_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Please Select a Category!");

            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete From CategoryTable Where CategoryID = @CTKey", Con);
                    cmd.Parameters.AddWithValue("@CTKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category Deleted Successfully!");
                    Con.Close();

                    ShowCategories();

                }
                catch (Exception Ex)

                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void categories_DataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Categoryname_textBox.Text = categories_DataGrid.SelectedRows[0].Cells[1].Value.ToString();
            categorydetails_textBox.Text = categories_DataGrid.SelectedRows[0].Cells[2].Value.ToString();

            if (Categoryname_textBox.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(categories_DataGrid.SelectedRows[0].Cells[0].Value.ToString());

            }
        }
    }
}
