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

            var _RuleZip = new WatcherRule();
            _RuleZip.FilePath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            _RuleZip.MoveFilePath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "ZIPs");
            _RuleZip.Filter = "*.zip";
            Stalker _stalkerzip = new Stalker();
            _stalkerzip.Start(_RuleZip, Log);
            Stalkers.Add(_stalkerzip);
            

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
