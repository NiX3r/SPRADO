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
    public partial class SubListBy : Form
    {

        private List<RevType> types;
        private List<RevMan> men;
        private List<House> houses;
        private List<RevisionForList> revisions;
        private List<RevisionForList> tempRevisions;
        private List<String> intoListBox = new List<String>();
        private int status = 0;

        public SubListBy()
        {
            InitializeComponent();

            types = Program.GetMySQL().GetRevType();
            men = Program.GetMySQL().GetRevMan();
            houses = Program.GetMySQL().GetHouses();
            revisions = Program.GetMySQL().GetRevisionForLists();
            tempRevisions = revisions;

            foreach (RevisionForList rfl in tempRevisions)
            {
                dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
            }

        }

        private void CleanUpUrTracks()
        {

            status = 0;
            pictureBox1.BackColor = Color.FromArgb(37, 42, 64);
            pictureBox2.BackColor = Color.FromArgb(37, 42, 64);
            pictureBox3.BackColor = Color.FromArgb(37, 42, 64);

            listBox3.Items.Clear();
            dataGridView1.Rows.Clear();
            foreach (RevisionForList rfl in revisions)
            {
                if (!checkBox1.Checked)
                {
                    if (checkBox2.Checked)
                    {
                        if (DateTime.Now > rfl.GetDate().AddYears(rfl.GetYearLoop()))
                        {
                            dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                        }
                    }
                    else
                    {
                        if (dateTimePicker1.Value < rfl.GetDate() && dateTimePicker2.Value > rfl.GetDate())
                        {
                            dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                        }
                    }
                }
                else
                {
                    dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                }
            }
            

        }

        private void pictureBox1_Click(object sender, EventArgs e) // by house
        {
            if(status != 1)
            {

                status = 1;
                pictureBox1.BackColor = Color.FromArgb(37, 42, 64);
                pictureBox2.BackColor = Color.FromArgb(37, 42, 64);
                pictureBox3.BackColor = Color.FromArgb(37, 42, 64);
                pictureBox1.BackColor = Color.FromArgb(46, 51, 73);
                listBox3.Items.Clear();
                intoListBox.Clear();
                dataGridView1.Rows.Clear();
                foreach (RevisionForList rfl in revisions)
                {
                    if (!checkBox1.Checked)
                    {
                        if (checkBox2.Checked)
                        {
                            if (DateTime.Now > rfl.GetDate().AddYears(rfl.GetYearLoop()))
                            {
                                dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                            }
                        }
                        else
                        {
                            if (dateTimePicker1.Value < rfl.GetDate() && dateTimePicker2.Value > rfl.GetDate())
                            {
                                dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                            }
                        }
                    }
                    else
                    {
                        dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                    }
                }
                foreach (House h in houses)
                {
                    listBox3.Items.Add(h.GetName());
                    intoListBox.Add(h.GetName());
                }

            }
            else
            {
                CleanUpUrTracks();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e) // by type
        {
            if (status != 2)
            {
                status = 2;
                pictureBox1.BackColor = Color.FromArgb(37, 42, 64);
                pictureBox2.BackColor = Color.FromArgb(37, 42, 64);
                pictureBox3.BackColor = Color.FromArgb(37, 42, 64);
                pictureBox2.BackColor = Color.FromArgb(46, 51, 73);
                listBox3.Items.Clear();
                intoListBox.Clear();
                dataGridView1.Rows.Clear();
                foreach (RevisionForList rfl in revisions)
                {
                    if (!checkBox1.Checked)
                    {
                        if (checkBox2.Checked)
                        {
                            if (DateTime.Now > rfl.GetDate().AddYears(rfl.GetYearLoop()))
                            {
                                dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                            }
                        }
                        else
                        {
                            if (dateTimePicker1.Value < rfl.GetDate() && dateTimePicker2.Value > rfl.GetDate())
                            {
                                dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                            }
                        }
                    }
                    else
                    {
                        dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                    }
                }
                foreach (RevType h in types)
                {
                    listBox3.Items.Add(h.GetName());
                    intoListBox.Add(h.GetName());
                }
            }
            else
            {
                CleanUpUrTracks();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e) // by man
        {
            if (status != 3)
            {
                status = 3;
                pictureBox1.BackColor = Color.FromArgb(37, 42, 64);
                pictureBox2.BackColor = Color.FromArgb(37, 42, 64);
                pictureBox3.BackColor = Color.FromArgb(37, 42, 64);
                pictureBox3.BackColor = Color.FromArgb(46, 51, 73);
                listBox3.Items.Clear();
                intoListBox.Clear();
                dataGridView1.Rows.Clear();
                foreach (RevisionForList rfl in revisions)
                {
                    if (!checkBox1.Checked)
                    {
                        if (checkBox2.Checked)
                        {
                            if (DateTime.Now > rfl.GetDate().AddYears(rfl.GetYearLoop()))
                            {
                                dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                            }
                        }
                        else
                        {
                            if (dateTimePicker1.Value < rfl.GetDate() && dateTimePicker2.Value > rfl.GetDate())
                            {
                                dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                            }
                        }
                    }
                    else
                    {
                        dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                    }
                }
                foreach (RevMan h in men)
                {
                    listBox3.Items.Add(h.GetName());
                    intoListBox.Add(h.GetName());
                }
            }
            else
            {
                CleanUpUrTracks();
            }
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

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox1.Text.Equals("Vyhledejte..."))
            {
                textBox1.Text = "";
            }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox3.SelectedItem != null)
            {

                dataGridView1.Rows.Clear();

                foreach (RevisionForList rfl in revisions)
                {
                    if(status == 1 && rfl.GetHouse().Equals(listBox3.SelectedItem))
                    {
                        if (!checkBox1.Checked)
                        {
                            if (checkBox2.Checked)
                            {
                                if (DateTime.Now > rfl.GetDate().AddYears(rfl.GetYearLoop()))
                                {
                                    dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                                }
                            }
                            else
                            {
                                if (dateTimePicker1.Value < rfl.GetDate() && dateTimePicker2.Value > rfl.GetDate())
                                {
                                    dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                                }
                            }
                        }
                        else
                        {
                            dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                        }
                    }
                    else if (status == 2 && rfl.GetRevType().Equals(listBox3.SelectedItem))
                    {
                        if (!checkBox1.Checked)
                        {
                            if (checkBox2.Checked)
                            {
                                if (DateTime.Now > rfl.GetDate().AddYears(rfl.GetYearLoop()))
                                {
                                    dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                                }
                            }
                            else
                            {
                                if (dateTimePicker1.Value < rfl.GetDate() && dateTimePicker2.Value > rfl.GetDate())
                                {
                                    dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                                }
                            }
                        }
                        else
                        {
                            dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                        }
                    }
                    else if (status == 3 && rfl.GetMan().Equals(listBox3.SelectedItem))
                    {
                        if (!checkBox1.Checked)
                        {
                            if (checkBox2.Checked)
                            {
                                if (DateTime.Now > rfl.GetDate().AddYears(rfl.GetYearLoop()))
                                {
                                    dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                                }
                            }
                            else
                            {
                                if (dateTimePicker1.Value < rfl.GetDate() && dateTimePicker2.Value > rfl.GetDate())
                                {
                                    dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                                }
                            }
                        }
                        else
                        {
                            dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                        }
                    }
                }

            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count > 0)
            {

                saveFileDialog1.Filter = ".PDF | *.pdf";

                if(saveFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    String filter = "";

                    if (checkBox1.Checked)
                        filter = "Všechny vytvořené revize";
                    else if (checkBox2.Checked)
                        filter = "Všechny propadlé revize ke dni vytvoření .pdf";
                    else
                        filter = "Všechny revize od " + dateTimePicker1.Value + " do " + dateTimePicker2.Value;

                    if (listBox3.SelectedItem != null)
                    {
                        if (status == 1)
                            filter += " k domu " + listBox3.SelectedItem;
                        else if (status == 2)
                            filter += " k typu " + listBox3.SelectedItem;
                        else if (status == 3)
                            filter += " k revizákovi " + listBox3.SelectedItem;
                    }

                    if (NixConverter.ConvertHtmlToPdf(dataGridView1, saveFileDialog1.FileName, filter))
                    {
                        System.Diagnostics.Process.Start(saveFileDialog1.FileName);
                        Program.GetUI().setSuccessTimer();
                    }
                    else
                    {
                        Program.GetUI().setUnsuccessTimer();
                        MessageBox.Show("A jéje! Něco se nepovedlo při ukládání!");
                    }
                }

            }
            else
            {
                MessageBox.Show("Pro export musíš mít v tabulce aspoň jeden záznam");
            }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            foreach (RevisionForList rfl in revisions)
            {
                if (!checkBox1.Checked)
                {
                    if (checkBox2.Checked)
                    {
                        if (DateTime.Now > rfl.GetDate().AddYears(rfl.GetYearLoop()))
                        {
                            dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                        }
                    }
                    else
                    {
                        if (dateTimePicker1.Value < rfl.GetDate() && dateTimePicker2.Value > rfl.GetDate())
                        {
                            dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                        }
                    }
                }
                else
                {
                    dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            foreach (RevisionForList rfl in revisions)
            {
                if (!checkBox1.Checked)
                {
                    if (checkBox2.Checked)
                    {
                        if (DateTime.Now > rfl.GetDate().AddYears(rfl.GetYearLoop()))
                        {
                            dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                        }
                    }
                    else
                    {
                        if (dateTimePicker1.Value < rfl.GetDate() && dateTimePicker2.Value > rfl.GetDate())
                        {
                            dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                        }
                    }
                }
                else
                {
                    dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            foreach (RevisionForList rfl in revisions)
            {
                if (!checkBox1.Checked)
                {
                    if (checkBox2.Checked)
                    {
                        if (DateTime.Now > rfl.GetDate().AddYears(rfl.GetYearLoop()))
                        {
                            dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                        }
                    }
                    else
                    {
                        if (dateTimePicker1.Value < rfl.GetDate() && dateTimePicker2.Value > rfl.GetDate())
                        {
                            dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                        }
                    }
                }
                else
                {
                    dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                }
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            foreach (RevisionForList rfl in revisions)
            {
                if (!checkBox1.Checked)
                {
                    if (checkBox2.Checked)
                    {
                        if (DateTime.Now > rfl.GetDate().AddYears(rfl.GetYearLoop()))
                        {
                            dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                        }
                    }
                    else
                    {
                        if (dateTimePicker1.Value < rfl.GetDate() && dateTimePicker2.Value > rfl.GetDate())
                        {
                            dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                        }
                    }
                }
                else
                {
                    dataGridView1.Rows.Add(rfl.GetDate().ToString("d"), rfl.GetDate().AddYears(rfl.GetYearLoop()).ToString("d"), rfl.GetHouse(), rfl.GetRevType(), rfl.GetMan(), rfl.GetInfo());
                }
            }
        }
    }
}
