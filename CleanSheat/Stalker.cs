using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace CleanSheet
{
    class Stalker
    {
     
        private FileSystemWatcher Watcher;

        public void Start(string path, string moveTo, string filter)
        {
            var _FileSystemWatcher = new FileSystemWatcher(path);
            Watcher = _FileSystemWatcher;
            _FileSystemWatcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size;

            _FileSystemWatcher.Created += messageBoxHandler;
            _FileSystemWatcher.Error += OnError;

            _FileSystemWatcher.Filter = filter;
            _FileSystemWatcher.IncludeSubdirectories = false;
            _FileSystemWatcher.EnableRaisingEvents = true;

            MessageBox.Show("test");
        }

        public static void messageBoxHandler(object sender, FileSystemEventArgs e)
        {
            MessageBox.Show($"{e.ChangeType.ToString()}: {e.FullPath}");
        }
        public static void moveHandler(object sender, FileSystemEventArgs e)
        {
            MessageBox.Show($"{e.ChangeType.ToString()}: {e.FullPath}");
        }
        public static void moveRenameHandler(object sender, FileSystemEventArgs e)
        {

            MessageBox.Show($"{e.ChangeType.ToString()}: {e.FullPath}");
        }

        public static void OnError(object sender, ErrorEventArgs e) =>
            PrintException(e.GetException());

        public static void PrintException(Exception? ex)
        {
            if (ex != null)
            {
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine("Stacktrace:");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine();
                PrintException(ex.InnerException);
            }

        }
    }
}
