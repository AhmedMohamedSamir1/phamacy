using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace phamacy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Icon = Icon.ExtractAssociatedIcon(AppDomain.CurrentDomain.FriendlyName);

        }

        int x = 1;
        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (x == 1)
            {
                textBox1.Text = "";
                x++;
            }
        }

        int y = 1;
        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (y == 1)
            {
                textBox2.Text = "";
                y++;
                textBox2.PasswordChar = '*';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = "Ahmed";
            string password = "12345";
            if(textBox1.Text==name && textBox2.Text==password)
            {
                MessageBox.Show("Correct password and User Name", "correct", MessageBoxButtons.OK, MessageBoxIcon.Information);
                register.name = textBox1.Text;
                this.Hide();
                Form2 frm = new Form2();
                frm.Show();
            }
            else
                MessageBox.Show("Wrong password Or User Name", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

       
    }
}
