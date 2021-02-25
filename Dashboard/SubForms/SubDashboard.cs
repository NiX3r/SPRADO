using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dashboard.SubForms
{
    public partial class SubDashboard : Form
    {
        public SubDashboard()
        {
            InitializeComponent();

            if (Program.GetMySQL().GetCountHouses() != null)
                label5.Text = Program.GetMySQL().GetCountHouses();
            else
                label5.Text = "0";
            if (Program.GetMySQL().GetCountUsers() != null)
                label7.Text = Program.GetMySQL().GetCountUsers();
            else
                label7.Text = "0";

            List<DateTime> dates = Program.GetMySQL().GetRevisionDates();
            DateTime dtNow = DateTime.Now;

            int done = 0, undone = 0, total = 0;

            foreach(DateTime dt in dates)
            {

                Debug.WriteLine("Checking >>>> " + dt.ToString());

                if (dt < dtNow) // is done
                {

                    undone++;

                }
                else // is undone
                {

                    done++;

                }

                total++;

            }
            double ptg;
            if (Convert.ToDouble(total.ToString()) == 0)
            {
                ptg = 100.00;
            }
            else
            {
                ptg = Convert.ToDouble(done.ToString()) / Convert.ToDouble(total.ToString()) * 100;
            }
            int percentage = Convert.ToInt32(ptg);
            circularProgressBar1.Value = percentage;
            circularProgressBar1.Text = percentage + "%";
            Debug.WriteLine("(" + done + "/" + total + ") * 100 = " + ptg);
            label17.Text = total.ToString();
            label18.Text = done.ToString();
            label19.Text = undone.ToString();

        }

        private void panel8_MouseClick(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:iliev@iliev.dev");
        }

        private void label15_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:iliev@iliev.dev");
        }

        private void label13_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:iliev@iliev.dev");
        }
    }
}
