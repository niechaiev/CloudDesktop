using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace formsDiplom
{
    public partial class SignUp : Form
    {
        bool isSignIn = true;
        public SignUp()
        {
            InitializeComponent();
            textBox2.PasswordChar = '*';
            textBox3.PasswordChar = '*';
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(isSignIn)
            {
                label2.Text = "Sign Up";
                label4.Visible = true;
                textBox3.Visible = true;
                button1.Text = "Register";
                button2.Text = "Have account?";
                isSignIn = false;
            }
            else
            {
                label2.Text = "Sign In";
                label4.Visible = false;
                textBox3.Visible = false;
                button1.Text = "Login";
                button2.Text = "New User";
                isSignIn = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainMenu f;
            if (isSignIn && textBox1.Text == "maksym.niechaiev@nure.ua")
                f = new MainMenu(textBox1.Text, false);
            else
            {
                f = new MainMenu(textBox1.Text, true);
            }
            Hide();
            f.ShowDialog();

           textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            Show();
        }
    }
}
