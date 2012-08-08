using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TeamVid
{
    public partial class splash : Form
    {
        public splash()
        {
            InitializeComponent();
        }

        private void splash_Load(object sender, EventArgs e)
        {
            pictureBox2.Width = 0;
            pictureBox4.Width = 0;
            pictureBox3.Width = 0;
            timer1.Start();
            timer2.Start();
            timer3.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pictureBox2.Width >= 515)
            {
                timer1.Stop();
            }
            else
            {
                pictureBox2.Width += 6;
            }
        }
        private void end()
        {
            Form1 main = new Form1();
            main.Show();
            this.Dispose(false);
            this.Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (pictureBox3.Width >= 515)
            {
                timer2.Stop();
            }
            else
            {
                pictureBox3.Width += 6;
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (pictureBox4.Width >= 515)
            {
                timer3.Stop();
                end();
            }
            else
            {
                pictureBox4.Width += 6;
            }
        }
    }
}
