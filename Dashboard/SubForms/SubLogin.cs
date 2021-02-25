using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dashboard.SubForms
{
    public partial class SubLogin : Form
    {
        public SubLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != "" && textBox2.Text != "" && textBox1.Text.Contains("@") && textBox1.Text.Contains("."))
            {

                if (Program.DoesPasswordCheck(textBox1.Text, textBox2.Text))
                {
                    Program.SetLogin(true);
                    Program.GetUI().LoggedIn();
                }

            }

        }
    }
}
