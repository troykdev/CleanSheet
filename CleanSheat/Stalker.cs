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
        private Collection _RunningWatchers = new Collection();

        public void Start()
        {
            var _FileSystemWatcher = new FileSystemWatcher(@"C:\Users\troyk\Desktop");
            _RunningWatchers.Add(_RunningWatchers);
            _FileSystemWatcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size;

            _FileSystemWatcher.Changed += OnChanged;
            _FileSystemWatcher.Created += OnCreated;
            _FileSystemWatcher.Deleted += OnDeleted;
            _FileSystemWatcher.Renamed += OnRenamed;
            _FileSystemWatcher.Error += OnError;

            _FileSystemWatcher.Filter = "*.txt";
            _FileSystemWatcher.IncludeSubdirectories = true;
            _FileSystemWatcher.EnableRaisingEvents = true;

            MessageBox.Show("test");
        }

        public static void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }
            MessageBox.Show($"Changed: {e.FullPath}");
        }

        public static void OnCreated(object sender, FileSystemEventArgs e)
        {
            string value = $"Created: {e.FullPath}";
            Console.WriteLine(value);
        }

        public static void OnDeleted(object sender, FileSystemEventArgs e) =>
            Console.WriteLine($"Deleted: {e.FullPath}");

        public static void OnRenamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine($"Renamed:");
            Console.WriteLine($"    Old: {e.OldFullPath}");
            Console.WriteLine($"    New: {e.FullPath}");
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
