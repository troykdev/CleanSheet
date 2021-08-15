using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CleanSheet
{
    public partial class Form1 : Form
    {
        Stalker _Stalker = new Stalker();


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            var _Rule = new WatcherRule();
           
            _Stalker.Start(_Rule);

            
        }

        private void TrayIcon_Click(object sender, EventArgs e)
        {
          
                ShowInTaskbar = true;
                TrayIcon.Visible = false;
                Show();


        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                ShowInTaskbar = false;
                TrayIcon.Visible = true;
            }

        }
    }
}
