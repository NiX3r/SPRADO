using Dashboard.Classes;
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
    public partial class SubHouses : Form
    {

        private List<House> houses;
        private List<String> intoListBox1 = new List<String>();

        private List<Contact> contacts;
        private List<String> intoListBox2 = new List<String>();

        private int selectedHouse;

        public SubHouses()
        {
            InitializeComponent();

            houses = Program.GetMySQL().GetHouses();

            foreach (House h in houses)
            {
                listBox1.Items.Add(h.GetName());
                intoListBox1.Add(h.GetName());
            }

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            if (textBox5.Text == "")
            {
                foreach (String s in intoListBox1)
                {
                    listBox1.Items.Add(s);
                }
            }
            else
            {
                foreach (String s in intoListBox1)
                {
                    if (s.Contains(textBox5.Text))
                    {
                        listBox1.Items.Add(s);
                    }
                }
            }
        }

        private void textBox5_MouseClick(object sender, MouseEventArgs e)
        {
            textBox5.Text = "";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listBox1.SelectedItem != null)
            {

                listBox2.Items.Clear();
                intoListBox2.Clear();

                foreach (House h in houses)
                {

                    if (h.GetName().Equals(listBox1.SelectedItem))
                    {

                        contacts = Program.GetMySQL().GetContactsByHouseID(h.GetKey());

                        foreach (Contact c in contacts)
                        {
                            listBox2.Items.Add(c.GetName());
                            intoListBox2.Add(c.GetName());
                        }

                        selectedHouse = h.GetKey();

                        return;

                    }

                }
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            if (textBox3.Text != "" && textBox4.Text != "" && textBox6.Text != "" && listBox1.SelectedItem != null)
            {

                // check if email text box is email
                if (!textBox3.Text.Contains("@") && !textBox3.Text.Contains("."))
                {
                    Program.GetUI().setUnsuccessTimer();
                    MessageBox.Show("Email musí obsahovat zavináč(@) a tečku(.)");
                    return;
                }
                // check if man exists in database
                if (Program.GetMySQL().CheckContactExists(textBox6.Text))
                {
                    Program.GetUI().setUnsuccessTimer();
                    MessageBox.Show("Smíš vytvořit pouze jeden kontakt tohoto jména\n\nTip: Smaž starý a následně jej vytvoř znovu");
                    return;
                }

                if (DialogResult.Yes == MessageBox.Show("Opravdu chcete pridat kontakt: " + textBox6.Text + " s p./pi: " + textBox4.Text + "?", "Potvrzeni interakce s databazi", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {

                    Contact c = new Contact(textBox6.Text, textBox4.Text, textBox3.Text, richTextBox1.Text, textBox2.Text, checkBox1.Checked);

                    // add to the list
                    contacts.Add(c);
                    // add to the listbox
                    listBox2.Items.Add(c.GetName());
                    listBox2.SetSelected(listBox2.Items.Count - 1, true);
                    // add to the database
                    Program.GetMySQL().AddContact(c, selectedHouse);

                    Program.GetUI().setSuccessTimer();

                }
                else
                    Program.GetUI().setUnsuccessTimer();

            }

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            listBox2.Items.Clear();

            if (textBox1.Text == "")
            {
                foreach (String s in intoListBox2)
                {
                    listBox2.Items.Add(s);
                }
            }
            else
            {
                foreach (String s in intoListBox2)
                {
                    if (s.Contains(textBox1.Text))
                    {
                        listBox2.Items.Add(s);
                    }
                }
            }
        }

        private void textBox1_MouseClick_1(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listBox2.SelectedItem != null && listBox1.SelectedItem != null)
            {

                foreach(Contact c in contacts)
                {

                    if (c.GetName().Equals(listBox2.SelectedItem))
                    {

                        textBox6.Text = c.GetName();
                        textBox4.Text = c.GetFullname();
                        textBox3.Text = c.GetEmail();
                        textBox2.Text = c.GetPhone();
                        richTextBox1.Text = c.GetInfo();
                        checkBox1.Checked = c.GetIsOwner();

                        return;

                    }

                }

            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null && listBox1.SelectedItem != null)
            {

                //check if user change fullname
                if (!Program.GetMySQL().CheckContactExists(textBox6.Text))
                {
                    Program.GetUI().setUnsuccessTimer();
                    MessageBox.Show("Zdá se že se snažíš editovat neexistujícího uživatele!\n\nTip: Pokud chceš vytvořit uživatele: " + textBox2.Text + " klikni na 'Vytvořit'");
                    return;
                }

                if (DialogResult.Yes == MessageBox.Show("Opravdu chcete editovat kontakt: " + textBox6.Text + " s p./pi: " + textBox4.Text + "?", "Potvrzeni interakce s databazi", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {

                    foreach (Contact c in contacts)
                    {
                        
                        if (c.GetName().Equals(listBox2.SelectedItem))
                        {
                            
                            c.SetFullname(textBox4.Text);
                            c.SetEmail(textBox3.Text);
                            c.SetInfo(richTextBox1.Text);
                            c.SetPhone(textBox2.Text);
                            c.SetIsOwner(checkBox1.Checked);

                            Program.GetMySQL().UpdateContact(c);

                            Program.GetUI().setSuccessTimer();

                            return;
                        }

                    }

                }

            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

            System.Diagnostics.Process.Start("mailto:?&bcc=" + Program.GetMySQL().GetEmailListByHouseID(selectedHouse));

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:?&bcc=" + Program.GetMySQL().GetOwnerEmailListByHouseID(selectedHouse));
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null && listBox2.SelectedItem != null)
            {

                if (DialogResult.Yes == MessageBox.Show("Opravdu chcete smazat kontakt: " + textBox6.Text + " s p./pi " + textBox4.Text + "?", "Potvrzeni interakce s databazi", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    foreach (Contact c in contacts)
                    {
                        if (c.GetName().Equals(listBox2.SelectedItem))
                        {
                            contacts.Remove(c);
                            Program.GetMySQL().DeleteContact(textBox6.Text, selectedHouse);
                            listBox2.Items.Remove(listBox2.SelectedItem);
                            break;
                        }
                    }

                    Program.GetUI().setSuccessTimer();
                }

            }
            else
            {
                Program.GetUI().setUnsuccessTimer();
                MessageBox.Show("Musíš mít vybraný dům a kontakt");
                return;
            }
        }
    }
}
