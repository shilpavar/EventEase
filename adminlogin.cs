using System;
using System.Windows.Forms;

namespace SteppingIntoHistoryFinal
{
    public partial class adminlogin : Form
    {
        public adminlogin()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_thinButton21_Click(object sender, EventArgs e)
        {
            if (Password_MetroTextbox2.Text == "")
            {
                MessageBox.Show("Enter Admin Password!");
            }
            else
            {
                if (Password_MetroTextbox2.Text == "Password")
                {
                    Users Obj = new Users();
                    Obj.Show();
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("Wrong Password");
                }
            }
        }
    }
}
