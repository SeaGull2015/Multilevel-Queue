using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multilevel_Queue
{
    public partial class Form1 : Form
    {
        private Processor processor;
        public Form1()
        {
            InitializeComponent();
            processor = new Processor(4);
            comboBox1.SelectedIndex = 1;
            comboBox2.DataSource = processor.GetListPriorities();
            listBox1.DataSource = processor.GetListPriorities();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int t = listBox1.SelectedIndex;
            listBox2.DataSource = processor.GetListProcesses(t);
            if (comboBox2.Items.Count > 0) comboBox2.SelectedIndex = t;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            processor.step();
            richTextBox1.Text += processor.getLog();
            listBox1.DataSource = processor.GetListPriorities();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {
            if (processor.isStep())
            {
                textBox18.Text = Convert.ToString(processor.getQuant());
                MessageBox.Show("Failed to change quant, because the process is running", "Epic fail!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                processor.setQuant(Convert.ToInt32(textBox18.Text));
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                processor.AddProcess(comboBox2.SelectedIndex, Convert.ToInt32(textBox6.Text), Convert.ToInt32(textBox8.Text));
                this.listBox2.DataSource = processor.GetListProcesses(listBox1.SelectedIndex);
                textBox6.Text = Convert.ToString(Convert.ToInt32(textBox6.Text) + 1);
            }
            catch
            {
                MessageBox.Show("Bad inputs (probably)", "Epic fail!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            processor.AddPriority(comboBox1.SelectedItem.ToString());
            listBox1.DataSource = processor.GetListPriorities();
            comboBox2.DataSource = processor.GetListPriorities();                        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try {
                processor.RemoveLastPriority();
                comboBox2.DataSource = processor.GetListPriorities();
                listBox1.DataSource = processor.GetListPriorities();
            }
            catch
            {
                MessageBox.Show("Nothing to remove", "Epic fail!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            processor.run();
            richTextBox1.Text += processor.getLog();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
