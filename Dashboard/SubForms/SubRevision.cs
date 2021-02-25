using Dashboard.Instances;
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
    public partial class SubRevision : Form
    {

        private List<RevMan> revisionMen;
        private List<String> intoListBox4 = new List<String>();

        private List<RevType> revisionTypes;
        private List<String> intoListBox2 = new List<String>();

        private List<House> houses;
        private List<String> intoListBox1 = new List<String>();

        private List<Revision> revisions;
        private List<String> intoListBox3 = new List<String>();

        private int selectedHouse, selectedRevisionType, selectedRevisionMan;

        private void textBox1_TextChanged(object sender, EventArgs e)
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            listBox4.Items.Clear();

            if (textBox2.Text == "")
            {
                foreach (String s in intoListBox4)
                {
                    listBox4.Items.Add(s);
                }
            }
            else
            {
                foreach (String s in intoListBox4)
                {
                    if (s.Contains(textBox2.Text))
                    {
                        listBox4.Items.Add(s);
                    }
                }
            }
        }

        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            textBox3.Text = "";
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox2.Text = "";
        }

        private void textBox4_MouseClick(object sender, MouseEventArgs e)
        {
            listBox3.Items.Clear();

            if (textBox4.Text == "")
            {
                foreach (String s in intoListBox3)
                {
                    listBox3.Items.Add(s);
                }
            }
            else
            {
                foreach (String s in intoListBox3)
                {
                    if (s.Contains(textBox4.Text))
                    {
                        listBox3.Items.Add(s);
                    }
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox4.Text = "";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if(listBox1.SelectedItem != null)
            {

                listBox3.Items.Clear();
                intoListBox3.Clear();

                foreach (House h in houses)
                {

                    if (h.GetName().Equals(listBox1.SelectedItem))
                    {

                        revisions = Program.GetMySQL().GetRevisionsByHouseID(h.GetKey(), revisionTypes, revisionMen);

                        foreach (Revision r in revisions)
                        {
                            listBox3.Items.Add(r.GetRevisionType().GetName());
                            intoListBox3.Add(r.GetRevisionType().GetName());
                        }

                        selectedHouse = h.GetKey();

                        return;

                    }

                }

            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null && listBox2.SelectedItem != null && listBox4.SelectedItem != null)
            {
                
                foreach(RevType rtt in revisionTypes)
                {
                    if (rtt.GetName().Equals(listBox2.SelectedItem))
                    {
                        selectedRevisionType = rtt.GetKey();
                        break;
                    }
                }

                if (Program.GetMySQL().CheckRevisionExists(selectedHouse, selectedRevisionType))
                {
                    Program.GetUI().setUnsuccessTimer();
                    MessageBox.Show("Smíš vytvořit pouze jednu revizi tohoto typu\n\nTip: Smaž starý a následně jej vytvoř znovu");
                    return;
                }

                if (DialogResult.Yes == MessageBox.Show("Opravdu chcete pridat revizi: " + listBox2.SelectedItem + "?", "Potvrzeni interakce s databazi", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {

                    RevType rt = null;
                    RevMan rm = null;

                    foreach (RevType rtt in revisionTypes)
                    {
                        if (rtt.GetName().Equals(listBox2.SelectedItem.ToString()))
                        {
                            rt = rtt;
                        }
                    }
                    foreach (RevMan rmt in revisionMen)
                    {
                        if (rmt.GetName().Equals(listBox4.SelectedItem.ToString()))
                        {
                            rm = rmt;
                        }
                    }

                    Revision r = new Revision(richTextBox1.Text, dateTimePicker1.Value, rm, rt);

                    revisions.Add(r);
                    listBox3.Items.Add(r.GetRevisionType().GetName());
                    listBox3.SetSelected(listBox3.Items.Count - 1, true);

                    Program.GetMySQL().AddRevision(r, selectedHouse);
                    Program.GetUI().setSuccessTimer();

                }
                else
                    Program.GetUI().setUnsuccessTimer();
            }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null && listBox3.SelectedItem != null)
            {

                foreach (Revision r in revisions)
                {
                    if (r.GetRevisionType().GetName().Equals(listBox3.SelectedItem))
                    {

                        dateTimePicker1.Value = r.GetLastDone();
                        richTextBox1.Text = r.GetInfo();

                        listBox2.SetSelected(listBox2.Items.IndexOf(r.GetRevisionType().GetName()), true);
                        listBox4.SetSelected(listBox4.Items.IndexOf(r.GetRevisionMan().GetName()), true);

                    }
                }

            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null && listBox2.SelectedItem != null && listBox3.SelectedItem != null && listBox4.SelectedItem != null)
            {
                RevMan selectedRM = null;
                foreach (RevType rtt in revisionTypes)
                {
                    if (rtt.GetName().Equals(listBox2.SelectedItem))
                    {
                        selectedRevisionType = rtt.GetKey();
                        break;
                    }
                }
                foreach (RevMan rmt in revisionMen)
                {
                    if (rmt.GetName().Equals(listBox4.SelectedItem))
                    {
                        selectedRM = rmt;
                        break;
                    }
                }

                if (!Program.GetMySQL().CheckRevisionExists(selectedHouse, selectedRevisionType))
                {
                    Program.GetUI().setUnsuccessTimer();
                    MessageBox.Show("Snažíš se editovat neexistující revizi\n\nTip: Můžeš zmáčknout pouze Uložit pro vytvoření");
                    return;
                }

                if (DialogResult.Yes == MessageBox.Show("Opravdu chcete editovat revizi: " + listBox2.SelectedItem + "?", "Potvrzeni interakce s databazi", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    foreach (Revision r in revisions)
                    {

                        if (r.GetRevisionType().GetName().Equals(listBox2.SelectedItem))
                        {

                            r.SetDateTime(dateTimePicker1.Value);
                            r.SetInfo(richTextBox1.Text);
                            r.SetRevisionMan(selectedRM);

                            Program.GetMySQL().UpdateRevision(r, selectedHouse);

                            Program.GetUI().setSuccessTimer();

                            return;
                        }

                    }
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null && listBox3.SelectedItem != null)
            {
                if (DialogResult.Yes == MessageBox.Show("Opravdu chcete smazat revizi: " + listBox3.SelectedItem + "?", "Potvrzeni interakce s databazi", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    foreach (Revision r in revisions)
                    {
                        if (r.GetRevisionType().GetName().Equals(listBox3.SelectedItem))
                        {
                            Program.GetMySQL().DeleteRevision(selectedHouse, r.GetRevisionType().GetKey());
                            listBox3.Items.Remove(listBox3.SelectedItem);
                            revisions.Remove(r);
                            break;
                        }
                    }

                    Program.GetUI().setSuccessTimer();
                }
            }
            else
            {
                Program.GetUI().setUnsuccessTimer();
                MessageBox.Show("Musíš mít vybraný dům a revizi");
                return;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            if (textBox3.Text == "")
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
                    if (s.Contains(textBox3.Text))
                    {
                        listBox1.Items.Add(s);
                    }
                }
            }
        }

        public SubRevision()
        {
            InitializeComponent();

            houses = Program.GetMySQL().GetHouses();

            foreach (House h in houses)
            {
                listBox1.Items.Add(h.GetName());
                intoListBox1.Add(h.GetName());
            }

            revisionTypes = Program.GetMySQL().GetRevType();
            foreach (RevType rt in revisionTypes)
            {
                listBox2.Items.Add(rt.GetName());
                intoListBox2.Add(rt.GetName());
            }

            revisionMen = Program.GetMySQL().GetRevMan();
            foreach (RevMan rm in revisionMen)
            {
                listBox4.Items.Add(rm.GetName());
                intoListBox4.Add(rm.GetName());
            }

        }
    }
}
