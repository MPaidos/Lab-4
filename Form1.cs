using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Shared;

namespace Lab_3_1
{
    public partial class Form1 : Form, IView
    {
        public event Action<EventArgs> AddDataEvent;
        public event Action<int> DeleteDataEvent;
        public event Action<int, string, string, string> EditDataEvent;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
            {
                StudentEventArgs student = new StudentEventArgs
                {
                    Name = textBox1.Text,
                    Group = textBox2.Text,
                    Speciality = textBox3.Text
                };
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                AddDataEvent?.Invoke(student);
            }
            else
            {
                MessageBox.Show("Вы ничего не ввели!", "Ошибка!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                DeleteDataEvent?.Invoke(int.Parse(item.Text));
            }
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "")
                {
                    if (item.SubItems[0].Text == textBox1.Text && item.SubItems[1].Text == textBox2.Text && item.SubItems[2].Text == textBox3.Text)
                    {
                        MessageBox.Show("Вы пытаетесь добавить студента-клона, или же пытаетесь изменить студента на точно такого же", "Ошибочка вышла...");
                        return;
                    }
                    else
                    {
                        int ID = int.Parse(item.Text);
                        EditDataEvent?.Invoke(ID, textBox1.Text, textBox2.Text, textBox3.Text);
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Вы ничего не ввели!", "Ошибка!");
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                textBox1.Text = item.SubItems[1].Text;
                textBox2.Text = item.SubItems[2].Text;
                textBox3.Text = item.SubItems[3].Text;
            }
        }

        public void RedrawView(IEnumerable<StudentEventArgs> data)
        {
            Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
            listView1.Clear();
            chart1.Series.Clear();
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader0,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            columnHeader1.Text = "ФИО";
            columnHeader1.Width = 191;
            columnHeader2.Text = "ГРУППА";
            columnHeader2.Width = 141;
            columnHeader3.Text = "СПЕЦИАЛЬНОСТЬ";
            columnHeader3.Width = 167;
            columnHeader0.Text = "ID";
            foreach (var item in data)
            {
                ListViewItem new_item = new ListViewItem(new string[] { item.ID.ToString(), item.Name, item.Group, item.Speciality });
                listView1.Items.Add(new_item);
            }
            for(int i = 0; i < data.Count(); i++)
            {
                if (keyValuePairs.ContainsKey(data.ToList()[i].Speciality))
                {
                    keyValuePairs[data.ToList()[i].Speciality]++;
                }
                else
                {
                    keyValuePairs.Add(data.ToList()[i].Speciality, 1);
                }
            }
            for (int i = 0; i < keyValuePairs.Count; i++)
            {
                Series series = chart1.Series.Add(keyValuePairs.Keys.ToArray()[i]);
                series.Points.Add(keyValuePairs.Values.ToArray()[i]);
            }
        }
    }
}
