using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SteppingIntoHistoryFinal
{
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
            ShowUsers();
        }
        SqlConnection Con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\jqgas\OneDrive\Documents\SteppingIntoHistoryDataBase.mdf;Integrated Security = True; Connect Timeout = 30");
        private void ShowUsers()
        {
            Con.Open();
            string Query = "Select * from UsersTable";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            user_datagrid.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void ResetData()
        {
            userphone_textBox.Text = "";
            usergender_combobox.SelectedIndex = -1;
            username_textBox.Text = "";
        }
        // This code is used to insert Users into the database
        private void Save_button_Click(object sender, EventArgs e)
        {
            if (username_textBox.Text == "" || usergender_combobox.SelectedIndex == -1 || userphone_textBox.Text == "")
            {
                MessageBox.Show("Missing Information!");

            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Insert Into UsersTable(UserName, UserPhone,UserGender,UserPassword) values (@UN,@UP,@UG,@UPASS)", Con);
                    cmd.Parameters.AddWithValue("@UN", username_textBox.Text);
                    cmd.Parameters.AddWithValue("@UP", userphone_textBox.Text);
                    cmd.Parameters.AddWithValue("@UG", usergender_combobox.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@UPASS", Password_textBox1.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Added Successfully!");
                    Con.Close();
                    ShowUsers();
                }
                catch (Exception Ex)

                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }
        // This code will display the data from the datagridview in the textboxes when user clicks a row
        int Key = 0;
        private void user_datagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            username_textBox.Text = user_datagrid.SelectedRows[0].Cells[1].Value.ToString();
            userphone_textBox.Text = user_datagrid.SelectedRows[0].Cells[2].Value.ToString();
            usergender_combobox.Text = user_datagrid.SelectedRows[0].Cells[3].Value.ToString();
            if (username_textBox.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(user_datagrid.SelectedRows[0].Cells[0].Value.ToString());

            }
        }
        //This code will delete User data from the database
        private void Delete_Button_Click(object sender, EventArgs e)
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
                    SqlCommand cmd = new SqlCommand("Delete From  UsersTable Where UserID = @UKey", Con);
                    cmd.Parameters.AddWithValue("@UKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Deleted Successfully!");
                    Con.Close();
                    // INSERT RESETDATA

                    ShowUsers();

                }
                catch (Exception Ex)

                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        // Update statement to allow users to update the data in the database
        private void Edit_button_Click(object sender, EventArgs e)
        {
            if (username_textBox.Text == "" || usergender_combobox.SelectedIndex == -1 || userphone_textBox.Text == "")
            {
                MessageBox.Show("Missing Information!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update UsersTable set UserName=@UN, UserPhone=@UP, UserGender=@UG, UserPassword=@UPASS Where UserID=@Ukey", Con);
                    cmd.Parameters.AddWithValue("@UN", username_textBox.Text);
                    cmd.Parameters.AddWithValue("@UP", userphone_textBox.Text);
                    cmd.Parameters.AddWithValue("@UG", usergender_combobox.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@UPASS", Password_textBox1.Text);
                    cmd.Parameters.AddWithValue("@UKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Updated Successfully!");
                    Con.Close();


                    ShowUsers();

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

        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {
            Services Obj = new Services();
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
    }
}
