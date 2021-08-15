using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanSheet
{
    class WatcherRule
    {
        public bool MoveFile = true;
        public string FilePath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
        public string MoveFilePath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "SDFs");
        public string Filter = "*.sdf";
    }
}
