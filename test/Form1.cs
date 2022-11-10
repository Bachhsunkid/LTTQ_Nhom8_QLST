using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = comboBox1.SelectedText;
            textBox2.Text = comboBox1.SelectedItem.ToString();
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            textBox3.Text = comboBox1.SelectedText.ToString();
        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            textBox4.Text = comboBox1.Text;
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            textBox4.Text = comboBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime a = dateTimePicker1.Value.Date;
            DateTime b = dateTimePicker2.Value.Date;
            while (a != b)
            {
                a = a.AddDays(1);
                textBox1.Text = a.ToString();
            }
        }
    }
}
