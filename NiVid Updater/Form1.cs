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

namespace NiVid_Updater
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox2.Width = 0;
            pictureBox1.Width = 0;
            pictureBox3.Width = 0;
            label2.Text = "Connecting to Update server...";
            timer1.Start();
        }
        private void singleupdate()
        {
            //MessageBox.Show("Updating Program!");
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            Uri update = new Uri("http://nicoding.com/nivid/update/files/nivid.exe");
            client.DownloadFileAsync(update, "NiVid.exe");
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
        }
        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            pictureBox2.Width = 515;
            timer2.Start();
        }
        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;
            pictureBox2.Width = e.ProgressPercentage * 5;
            label2.Text = "Downloading.... " + e.ProgressPercentage.ToString() + "%";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pictureBox1.Width >= 515)
            {
                timer1.Stop();
                singleupdate();
            }
            else
            {
                pictureBox1.Width += 6;
                label2.Text = "Contacing download server...";
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (pictureBox3.Width >= 515)
            {
                timer2.Stop();
                Process.Start("NiVid.exe");
                label2.Text = "Successfully Installed...";
                Environment.Exit(0);
            }
            else
            {
                label2.Text = "Installing...";
                pictureBox3.Width += 6;
            }
        }
    }
}
