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
    public partial class cccp : Form
    {
        public cccp()
        {
            InitializeComponent();
        }

        private void cccp_Load(object sender, EventArgs e)
        {
            singleupdate();
            pictureBox1.Width = 0;
        }
        private void singleupdate()
        {
            //MessageBox.Show("Updating Program!");
            label3.Text = "Downloading...";
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            Uri update = new Uri("http://nicoding.com/nivid/addons/Combined-Community-Codec-Pack-2011-11-11.exe");
            client.DownloadFileAsync(update, "cccp.exe");
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
        }
        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            pictureBox3.Width = 515;
            label3.Text = "Installing...";
            label5.Visible = true;
            timer2.Start();
        }
        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;
            pictureBox3.Width = e.ProgressPercentage * 5;
            label2.Text = e.ProgressPercentage.ToString() + "%";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            FileInfo fInfo = new FileInfo("cccp.exe");
            ProcessStartInfo psi = new ProcessStartInfo("cccp.exe");
            psi.Arguments = "/SILENT /NORESTART";
            Process p = Process.Start(psi);
            //p.Close();
            p.EnableRaisingEvents = true;
            p.Exited += new EventHandler(p_Exited);
        }

        void p_Exited(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;
            timer3.Start();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (pictureBox1.Width >= 515)
            {
                timer3.Stop();
                label3.Text = "Successfully installed!";
                label5.Text = "Successfully Installed!";
                timer1.Start();
            }
            else
            {
                pictureBox1.Width += 6;
            }
        }
    }
}
