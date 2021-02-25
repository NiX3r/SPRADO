using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Dashboard.SubForms;
using System.Diagnostics;

namespace Dashboard
{
    public partial class Form1 : Form
    {

        private Form currentChildForm;
        private Timer clock;
        private Timer t;
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
         (
               int nLeftRect,
               int nTopRect,
               int nRightRect,
               int nBottomRect,
               int nWidthEllipse,
               int nHeightEllipse

         );

        public Form1()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            pnlNav.Height = btnDashbord.Height;
            pnlNav.Top = btnDashbord.Top;
            pnlNav.Left = btnDashbord.Left;
            pnlNav.Hide();
            //btnDashbord.BackColor = Color.FromArgb(46, 51, 73);
            if(Program.IsLogin())
                OpenChildForm(new SubDashboard());
            else
                OpenChildForm(new SubLogin());
            // timer setup
            clock = new Timer();
            clock.Tick += new EventHandler(clockEvent);
            clock.Interval = 1000;
            clock.Start();
            t = new Timer();
            t.Interval = 3000;
            t.Tick += new EventHandler(clock2Event);
            label9.Text = DateTime.Now.ToString("HH:mm");
            seconds.Value = DateTime.Now.Second;

        }

        private void clockEvent(object sender, EventArgs e)
        {
            label9.Text = DateTime.Now.ToString("HH:mm");
            seconds.Value = DateTime.Now.Second;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnDashbord_Click(object sender, EventArgs e)
        {
            if (Program.IsLogin())
            {
                pnlNav.Height = btnDashbord.Height;
                pnlNav.Top = btnDashbord.Top;
                pnlNav.Left = btnDashbord.Left;
                btnDashbord.BackColor = Color.FromArgb(46, 51, 73);
                OpenChildForm(new SubDashboard());
            }
        }

        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            if (Program.IsLogin())
            {
                pnlNav.Height = btnAnalytics.Height;
                pnlNav.Top = btnAnalytics.Top;
                btnAnalytics.BackColor = Color.FromArgb(46, 51, 73);
                OpenChildForm(new SubRevision());
            }
        }

        private void btnCalender_Click(object sender, EventArgs e)
        {
            if (Program.IsLogin())
            {
                pnlNav.Height = btnCalender.Height;
                pnlNav.Top = btnCalender.Top;
                btnCalender.BackColor = Color.FromArgb(46, 51, 73);
                OpenChildForm(new SubHouses());
            }
        }

        private void btnContactUs_Click(object sender, EventArgs e)
        {
            if (Program.IsLogin())
            {
                pnlNav.Height = btnContactUs.Height;
                pnlNav.Top = btnContactUs.Top;
                btnContactUs.BackColor = Color.FromArgb(46, 51, 73);
                OpenChildForm(new SubListBy());
            }
        }

        private void btnsettings_Click(object sender, EventArgs e)
        {
            Program.SetLogin(false);
            OpenChildForm(new SubLogin());
        }

        private void btnDashbord_Leave(object sender, EventArgs e)
        {
            btnDashbord.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnAnalytics_Leave(object sender, EventArgs e)
        {
            btnAnalytics.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnCalender_Leave(object sender, EventArgs e)
        {
            btnCalender.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnContactUs_Leave(object sender, EventArgs e)
        {
            btnContactUs.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnsettings_Leave(object sender, EventArgs e)
        {
            btnsettings.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Program.IsLogin())
            {
                pnlNav.Height = button2.Height;
                pnlNav.Top = button2.Top;
                button2.BackColor = Color.FromArgb(46, 51, 73);
                OpenChildForm(new SubRevisionType());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Program.IsLogin())
            {
                pnlNav.Height = button3.Height;
                pnlNav.Top = button3.Top;
                button3.BackColor = Color.FromArgb(46, 51, 73);
                OpenChildForm(new SubRevisionMan());
            }
        }

        // Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panel10_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void button2_Leave(object sender, EventArgs e)
        {
            button2.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void button3_Leave(object sender, EventArgs e)
        {
            button3.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void OpenChildForm(Form childForm)
        {
            //open only form
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            //End
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel4.Controls.Add(childForm);
            panel4.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        public void LoggedIn()
        {
            OpenChildForm(new SubDashboard());
        }

        public void SetFullname(String fullname)
        {
            label1.Text = fullname;
        }

        public void setSuccessTimer()
        {
            panel10.BackColor = Color.FromArgb(5, 193, 1);
            t.Start();
        }

        public void setUnsuccessTimer()
        {
            panel10.BackColor = Color.FromArgb(193, 5, 1);
            t.Start();
        }

        private void clock2Event(object sender, EventArgs e)
        {
            panel10.BackColor = Color.FromArgb(24, 30, 54);
            t.Stop();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://icons8.com");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.GetMySQL().DestroyConnection();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
