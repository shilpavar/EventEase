using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SteppingIntoHistoryFinal
{
    public partial class LoginPage : Form
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=C:\Users\jqgas\OneDrive\Documents\SteppingIntoHistoryDataBase.mdf;Integrated Security = True; Connect Timeout = 30");

        private void Reset()
        {
            Username_MetroTextbox1.Text = "";
            Password_MetroTextbox2.Text = "";
        }
        private void reset_ThinButton27_Click(object sender, System.EventArgs e)
        {
            Reset();
        }

        private void Login_thinButton21_Click(object sender, System.EventArgs e)
        {
            if (Username_MetroTextbox1.Text == "" || Password_MetroTextbox2.Text == "")
            {
                MessageBox.Show("Enter Details!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from UsersTable where UserName = '" + Username_MetroTextbox1.Text + "'", Con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        Bookingscs Obj = new Bookingscs();
                        Obj.Show();
                        this.Hide();
                        Con.Close();
                    }

                    else
                    {
                        MessageBox.Show("Wrong Username or Password!");
                    }
                    Con.Close();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }


            }

        }

        private void label5_Click(object sender, System.EventArgs e)
        {
            adminlogin Obj = new adminlogin();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

    }
}

