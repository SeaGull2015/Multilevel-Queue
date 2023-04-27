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
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
