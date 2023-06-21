using SimpleTCP;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace TCPSocketProgrammingFormApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SimpleTcpServer server;
        private void Form1_Load(object sender, EventArgs e)
        {
            server = new SimpleTcpServer();
            server.Delimiter = 0x13;
            server.StringEncoder = Encoding.UTF8;
            server.DataReceived += Server_DataRecevied;
        }

        private void Server_DataRecevied(object? sender, SimpleTCP.Message e)
        {
            txtStatus.Invoke((MethodInvoker)delegate ()
            {
                txtStatus.Text += e.MessageString;
                e.ReplyLine(string.Format("you said: {0}", e.MessageString));
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ipAddress = txtHost.Text;
            IPAddress address = IPAddress.Parse(ipAddress);
            Byte[] bytes = address.GetAddressBytes();
            txtStatus.Text += "Server starting...";
            IPAddress ip = new IPAddress(bytes);
            server.Start(ip,Convert.ToInt32(txtPort.Text));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(server.IsStarted)
            {
                server.Stop();
            }
        }
    }
}