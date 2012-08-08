using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace TeamVid
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //textBox1.Text = getIP();
            textBox2.Text = CreateRandomPassword(10);
            textBox1.Text = getid();
            Server();
        }
        private string getIP()
        {
            string url = "http://nicoding.com/api.php?getip=yes" + textBox3.Text;
            WebClient wc = new WebClient();
            string data = wc.DownloadString(url);
            return data;
        }
        private string getid()
        {
                string ip = getIP();
                string url = "http://nicoding.com/api.php?app=nivid&ip=" + ip + "&apppass=" + textBox2.Text;
                WebClient wc = new WebClient();
                string data = wc.DownloadString(url);
                if (data == "You have supplied an invalid id! Please try again.")
                {
                    string url2 = "http://nicoding.com/api.php?newnividid=yes";
                    WebClient wc2 = new WebClient();
                    string data2 = wc.DownloadString(url2);
                    return data2;
                }
                else
                {
                    return data;
                }
        }
        private static string CreateRandomPassword(int passwordLength)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
            char[] chars = new char[passwordLength];
            Random rd = new Random();

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = CreateRandomPassword(10);
            string ip = getIP();
            string url = "http://nicoding.com/api.php?app=nivid&ip=" + ip + "&apppass=" + textBox2.Text;
            WebClient wc = new WebClient();
            string data = wc.DownloadString(url);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about about = new about();
            about.Show();
        }
        private string comparepass(string pass)
        {
            string url = "http://nicoding.com/api.php?app=nivid&getdata=apppass&user=" + textBox3.Text;
            WebClient wc = new WebClient();
            string data = wc.DownloadString(url);
            if (pass != data)
            {
                return "Invalid Pass!";
            }
            else
            {
                return "Pass OK!";
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox3.Text) && !String.IsNullOrEmpty(textBox4.Text))
            {
                string url = "http://nicoding.com/api.php?app=nivid&id=" + textBox3.Text;
                WebClient wc = new WebClient();
                string data = wc.DownloadString(url);
                md5engine md5 = new md5engine();
                string pass = comparepass(md5.EncodePassword(textBox4.Text).ToLower());
                if (pass == "Invalid Pass!")
                {
                    MessageBox.Show("The password you have entered is incorrect!");
                }
                else 
                {
                if (data == "You have supplied an invalid id! Please try again.")
                {
                    if (MessageBox.Show("Are you a new user?", "Error: ID Not Found!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        MessageBox.Show("You are a new user! YAY!");
                    }
                    else
                    {
                        MessageBox.Show("You are not a new user! BOO!");
                    }

                }
                else
                {
                    ConnectToServer(data);
                }
              }
            }
            else
            {
                MessageBox.Show("You have not provided a NiVid id or password!");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
        #region udp_stuff
        Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private void Server()
        {
            // Lets Create an endpoint for the listener to bind to.
            IPEndPoint server = new IPEndPoint(IPAddress.Any, 8080);
            try
            {
                listener.Blocking = true; // set the listener into a blocking state.
                listener.Bind(server); // Bind the server's IP to the Listener.
                listener.Listen(1); // Listen for one connection.
                // If the Socket Gets connected to.
                while (listener.Connected)
                {
                    // Disconnect the Socket from the client.
                    listener.Disconnect(true);
                    // Close the Listener Socket.
                    listener.Close();
                    // Restart the Server, to wait for another connection.
                    Server();
                    MessageBox.Show("A Client Connected to your Server", "Connection - Succesfull");
                }
            }
            catch (SocketException se)
            {
                // The Socket could not start.
                MessageBox.Show(se.ToString());
            }
        }
        public void ConnectToServer(string ip)
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Blocking = true; // sets the socket into blocking mode.
            // Change the loopback address to the address of the server.
            IPEndPoint server = new IPEndPoint(IPAddress.Parse(ip), 8080);
            try
            {
                // Connect to the server.
                clientSocket.Connect(server);
                // Disconnect the Socket.
                clientSocket.Disconnect(true);
                // Close the Socket.
                clientSocket.Close();
                // Display Connected Message.
                MessageBox.Show("You connected to the Server... Now we just need to get streaming enabled!", "Information");
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.ToString());
            }
        }
        #endregion

        UdpClient send, receive; 
        IPEndPoint sendpt, receivept;
        private void button3_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog mydialog = new OpenFileDialog();
            if (mydialog.ShowDialog() == DialogResult.OK)
            {
                /*string path = mydialog.FileName;
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                byte[] data = br.ReadBytes(Convert.ToInt32(fs.Length));
                send = new UdpClient();
                sendpt = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3333);
                send.Send(data, data.Length, sendpt);
                send.Close();
                */

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            user userpanel = new user();
            userpanel.Show();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            preferences prefs = new preferences();
            prefs.Show();
        }

        private void codecPackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cccp cccpinstall = new cccp();
            cccpinstall.Show();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            progressbarcolors pbc = new progressbarcolors();
            pbc.Show();
        }
    }
}