﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace Solitaire.App.Winforms
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
        }

        private void btnOndevice_Click(object sender, EventArgs e)
        {

            this.Hide();
            var f = new Ondevice();
            f.Closed += (s, args) => this.Close();
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var d = new IpPortDialog();
            d.ShowDialog();

            Program.ServerWithClient.Host(d.IP, d.Port);


            this.Hide();
            var f = new MultiPlayer();
            f.Closed += (s, args) => this.Close();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var d = new IpPortDialog();
            d.ShowDialog();
            Program.ServerWithClient.Join(d.IP, d.Port);

            this.Hide();
            var f = new MultiPlayer();
            f.Closed += (s, args) => this.Close();
            f.Show();
        }
    }
}