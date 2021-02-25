using Dashboard.Instances;
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
    public partial class SubRevisionMan : Form
    {

        private List<RevisionMan> revisionMen;
        private List<String> intoListBox;

        public SubRevisionMan()
        {
            InitializeComponent();

            intoListBox = new List<String>();
            revisionMen = Program.GetMySQL().GetRevisionMen();

            foreach (RevisionMan rm in revisionMen)
            {
                listBox1.Items.Add(rm.GetFullname());
                intoListBox.Add(rm.GetFullname());
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            // check if email text box is email
            if(!textBox3.Text.Contains("@") && !textBox3.Text.Contains("."))
            {
                Program.GetUI().setUnsuccessTimer();
                MessageBox.Show("Email musí obsahovat zavináč(@) a tečku(.)");
                return;
            }
            // check if man exists in database
            if (Program.GetMySQL().CheckRevisionManExists(textBox2.Text))
            {
                Program.GetUI().setUnsuccessTimer();
                MessageBox.Show("Smíš vytvořit pouze jednoho revizáka tohoto jména\n\nTip: Smaž starý a následně jej vytvoř znovu");
                return;
            }

            if (textBox2.Text != "" && textBox4.Text != "")
            {

                if (DialogResult.Yes == MessageBox.Show("Opravdu chcete pridat uzivatele: " + textBox2.Text + "?", "Potvrzeni interakce s databazi", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    RevisionMan rm = new RevisionMan(textBox2.Text, textBox4.Text, textBox3.Text, textBox1.Text, richTextBox1.Text);

                    // add to the list
                    revisionMen.Add(rm);
                    // add to the listbox
                    listBox1.Items.Add(rm.GetFullname());
                    listBox1.SetSelected(listBox1.Items.Count - 1, true);
                    // add to the database
                    Program.GetMySQL().AddRevisionMan(rm);

                    Program.GetUI().setSuccessTimer();
                }

            }
            else
            {
                Program.GetUI().setUnsuccessTimer();
                MessageBox.Show("Pro vytvoření musíš zadat potřebné informace");
                return;
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if(listBox1.SelectedItem != null)
            {

                foreach(RevisionMan rm in revisionMen)
                {

                    if (rm.GetFullname().Equals(listBox1.SelectedItem))
                    {

                        textBox2.Text = rm.GetFullname();
                        textBox4.Text = rm.GetCompany();
                        textBox3.Text = rm.GetEmail();
                        textBox1.Text = rm.GetPhone();
                        richTextBox1.Text = rm.GetInfo();

                        return;

                    }

                }

            }

        }

        

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

            listBox1.Items.Clear();

            if(textBox5.Text == "")
            {
                foreach(String s in intoListBox)
                {
                    listBox1.Items.Add(s);
                }
            }
            else
            {
                foreach (String s in intoListBox)
                {
                    if (s.Contains(textBox5.Text))
                    {
                        listBox1.Items.Add(s);
                    }
                }
            }

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

            if (listBox1.SelectedItem != null)
            {

                //check if user change fullname
                if (!Program.GetMySQL().CheckRevisionManExists(textBox2.Text))
                {
                    Program.GetUI().setUnsuccessTimer();
                    MessageBox.Show("Zdá se že se snažíš editovat neexistujícího uživatele!\n\nTip: Pokud chceš vytvořit uživatele: " + textBox2.Text + " klikni na 'Vytvořit'");
                    return;
                }

                if (DialogResult.Yes == MessageBox.Show("Opravdu chcete editovat uzivatele: " + listBox1.Text + "?", "Potvrzeni interakce s databazi", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {

                    foreach (RevisionMan rm in revisionMen)
                    {
                        if (rm.GetFullname().Equals(textBox2.Text))
                        {
                            rm.SetCompany(textBox4.Text);
                            rm.SetEmail(textBox3.Text);
                            rm.SetPhone(textBox1.Text);
                            rm.SetInfo(richTextBox1.Text);
                            Program.GetMySQL().UpdateRevisionMan(rm);
                            break;
                        }
                    }

                    Program.GetUI().setSuccessTimer();
                }

            }
            else
            {
                Program.GetUI().setUnsuccessTimer();
                MessageBox.Show("Musíš mít vybraného revizáka pro editaci");
                return;
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

            if(listBox1.SelectedItem != null)
            {

                if(DialogResult.Yes == MessageBox.Show("Opravdu chcete smazat uzivatele: " + listBox1.Text + "?", "Potvrzeni interakce s databazi", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    foreach (RevisionMan rm in revisionMen)
                    {
                        if (rm.GetFullname().Equals(listBox1.SelectedItem.ToString()))
                        {
                            revisionMen.Remove(rm);
                            Program.GetMySQL().DeleteRevisionMan(listBox1.SelectedItem.ToString());
                            listBox1.Items.Remove(listBox1.SelectedItem);
                            break;
                        }
                    }

                    Program.GetUI().setSuccessTimer();
                }

            }
            else
            {
                Program.GetUI().setUnsuccessTimer();
                MessageBox.Show("Musíš mít vybraného revizáka pro smazání");
                return;
            }

        }

        private void textBox5_MouseClick(object sender, MouseEventArgs e)
        {
            textBox5.Text = "";
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {

                System.Diagnostics.Process.Start("mailto:" + textBox3.Text);

            }
            else
            {
                Program.GetUI().setUnsuccessTimer();
                MessageBox.Show("Musíš mít vybraného revizáka pro editaci");
                return;
            }
        }
    }
}
