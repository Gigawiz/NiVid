using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;
using System.IO;

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
                if (File.Exists("updater.exe"))
                {
                    File.Delete("updater.exe");
                }
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
                if (!File.Exists("updater.exe"))
                {
                    System.IO.File.WriteAllBytes("updater.exe", TeamVid.Properties.Resources.NiVid_Updater);
                }
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
                getupdates(); 
            }
            else
            {
                pictureBox4.Width += 6;
            }
        }
        private void getupdates()
        {
            try
            {
                string updateurl = "http://nicoding.com/nivid/update/nivid_version.txt";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(updateurl);
                WebResponse response = request.GetResponse();
                System.IO.StreamReader sr = new System.IO.StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("windows-1252"));
                string update = sr.ReadToEnd();
                int build = Convert.ToInt32(update);
                int thisbuild = 1;
                if (build > thisbuild)
                {
                    var result = MessageBox.Show("There is an update available for TubeRip! Would you like to download it now?", "Update Available", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        Process.Start("updater.exe");
                        Environment.Exit(0);
                    }
                }
                else
                {
                    if (File.Exists("updater.exe"))
                    {
                        File.Delete("updater.exe");
                    }
                    end();
                }
            }
            catch
            {
                MessageBox.Show("Unable to connect to update server! TubeRip will check for updates at next launch!");
            }
        }
    }
}
