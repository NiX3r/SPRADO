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
    public partial class SubRevisionType : Form
    {

        private List<RevisionType> revisionTypes;
        private List<String> intoListBox;

        public SubRevisionType()
        {
            InitializeComponent();

            revisionTypes = Program.GetMySQL().GetRevisionTypes();
            intoListBox = new List<String>();
            foreach(RevisionType rt in revisionTypes)
            {
                listBox3.Items.Add(rt.GetName());
                intoListBox.Add(rt.GetName());
            }

        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox3.Items.Clear();

            if (textBox1.Text == "")
            {
                foreach (String s in intoListBox)
                {
                    listBox3.Items.Add(s);
                }
            }
            else
            {
                foreach (String s in intoListBox)
                {
                    if (s.Contains(textBox1.Text))
                    {
                        listBox3.Items.Add(s);
                    }
                }
            }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox3.SelectedItem != null)
            {

                foreach (RevisionType rt in revisionTypes)
                {

                    if (rt.GetName().Equals(listBox3.SelectedItem))
                    {

                        textBox2.Text = rt.GetName();
                        numericUpDown1.Value = rt.GetYearLoop();
                        richTextBox1.Text = rt.GetInfo();

                        return;

                    }

                }

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // check if man exists in database
            if (Program.GetMySQL().CheckRevisionTypeExists(textBox2.Text))
            {
                Program.GetUI().setUnsuccessTimer();
                MessageBox.Show("Smíš vytvořit pouze jednoho typ revize tohoto jména\n\nTip: Smaž starý a následně jej vytvoř znovu");
                return;
            }

            if (textBox2.Text != "" && numericUpDown1.Value > 0)
            {

                if (DialogResult.Yes == MessageBox.Show("Opravdu chcete pridat typ: " + textBox2.Text + "?", "Potvrzeni interakce s databazi", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    RevisionType rt = new RevisionType(textBox2.Text, Convert.ToInt16(numericUpDown1.Value), richTextBox1.Text);

                    // add to the list
                    revisionTypes.Add(rt);
                    // add to the listbox
                    listBox3.Items.Add(rt.GetName());
                    listBox3.SetSelected(listBox3.Items.Count - 1, true);
                    // add to the database
                    Program.GetMySQL().AddRevisionType(rt);

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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            //check if user change fullname
            if (!Program.GetMySQL().CheckRevisionTypeExists(textBox2.Text))
            {
                Program.GetUI().setUnsuccessTimer();
                MessageBox.Show("Zdá se že se snažíš editovat neexistující typ!\n\nTip: Pokud chceš vytvořit typ: " + textBox2.Text + " klikni na 'Vytvořit'");
                return;
            }

            if (listBox3.SelectedItem != null)
            {

                if (DialogResult.Yes == MessageBox.Show("Opravdu chcete editovat typ: " + listBox3.Text + "?", "Potvrzeni interakce s databazi", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {

                    foreach (RevisionType rt in revisionTypes)
                    {
                        if (rt.GetName().Equals(textBox2.Text))
                        {
                            rt.SetYearLoop(Convert.ToInt16(numericUpDown1.Value));
                            rt.SetInfo(richTextBox1.Text);

                            Program.GetMySQL().UpdateRevisionType(rt);
                            break;
                        }
                    }

                    Program.GetUI().setSuccessTimer();
                }

            }
            else
            {
                Program.GetUI().setUnsuccessTimer();
                MessageBox.Show("Musíš mít vybraný typ pro editaci");
                return;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (listBox3.SelectedItem != null)
            {

                if (DialogResult.Yes == MessageBox.Show("Opravdu chcete smazat typ: " + listBox3.Text + "?", "Potvrzeni interakce s databazi", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    foreach (RevisionType rt in revisionTypes)
                    {
                        if (rt.GetName().Equals(listBox3.SelectedItem.ToString()))
                        {
                            revisionTypes.Remove(rt);
                            Program.GetMySQL().DeleteRevisionType(listBox3.SelectedItem.ToString());
                            listBox3.Items.Remove(listBox3.SelectedItem);
                            break;
                        }
                    }

                    Program.GetUI().setSuccessTimer();
                }

            }
            else
            {
                Program.GetUI().setUnsuccessTimer();
                MessageBox.Show("Musíš mít vybraný typ pro smazání");
                return;
            }
        }
    }
}
