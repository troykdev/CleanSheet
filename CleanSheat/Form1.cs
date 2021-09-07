using System;
using Serilog;
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
        List<Stalker> Stalkers = new List<Stalker>();
        ILogger Log;
        public Form1(ILogger _log)
        {
            Log = _log;
            _log.Information("CleanSheet started");
            Stalker.Log = _log;
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void StartButton_Click(object sender, EventArgs e)
        {


            foreach (WatcherRule rule in ConfigHelper.LoadConfigFiles())
            {
                var _rule = rule;
                Stalker _stalker = new Stalker();
                _stalker.Start(_rule, Log);
                Stalkers.Add(_stalker);
            }

            dirCountLabel.Text = Stalkers.Count().ToString();

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
