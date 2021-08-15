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
        Stalker _StalkerSdf = new Stalker();
        Stalker _StalkerZip = new Stalker();
        Stalker _StalkerEvtx = new Stalker();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            var _RuleSdf = new WatcherRule();
            _StalkerSdf.Start(_RuleSdf);
            var _RuleZip = new WatcherRule();
            _RuleZip.FilePath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            _RuleZip.MoveFilePath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "ZIPs");
            _RuleZip.Filter = "*.zip";
            _StalkerZip.Start(_RuleZip);
            var _RuleEvtx = new WatcherRule();
            _RuleEvtx.FilePath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            _RuleEvtx.MoveFilePath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "EVTXs");
            _RuleEvtx.Filter = "*.evtx";
            _StalkerEvtx.Start(_RuleEvtx);

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
