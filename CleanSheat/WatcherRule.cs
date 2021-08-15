using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanSheet
{
    class WatcherRule
    {
        public bool MoveFile = true;
        public string FilePath = @"C:\Users\troyk\Desktop";
        public string MoveFilePath = @"C:\Users\troyk\Desktop\txt";
        public string Filter = "*.txt";
    }
}
