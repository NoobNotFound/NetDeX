using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetDeX.App.Winforms
{
    public partial class IpPortDialog : Form
    {
        public IPAddress? IP { get; private set; }
        public int Port { get; private set; }
        public string Name { get; private set; }
        public IpPortDialog()
        {
            InitializeComponent();

            numericUpDown1.Value = new Random().Next(1024, 49151);

            comboBox1.Items.AddRange(NetworkInterface
             .GetAllNetworkInterfaces()
             .SelectMany(i => i.GetIPProperties().UnicastAddresses)
             .Where(a => a.Address.AddressFamily == AddressFamily.InterNetwork)
             .Select(a => a.Address)
             .ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text))
                return;

            if (IPAddress.TryParse(comboBox1.Text, out var ip))
            {
                IP = ip;
                Port = (int)numericUpDown1.Value;
                Name = textBox1.Text ?? "";
                this.Close();
            }
        }
    }
}
