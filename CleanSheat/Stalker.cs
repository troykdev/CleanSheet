using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Data;
using System.Formats.Asn1;

namespace CleanSheet
{
    class Stalker
    {
     
        private FileSystemWatcher Watcher;
        private WatcherRule Rules;
        public void Start(WatcherRule _rules)
        {
            Rules = _rules;
            MessageBox.Show(Rules.FilePath);
            var _FileSystemWatcher = new FileSystemWatcher(Rules.FilePath);
            Watcher = _FileSystemWatcher;
            _FileSystemWatcher.NotifyFilter = NotifyFilters.Attributes
                                 | NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastAccess
                                 | NotifyFilters.LastWrite
                                 | NotifyFilters.Security
                                 | NotifyFilters.Size;

            _FileSystemWatcher.Created +=  _rules.MoveFile ? moveFileHandler : messageBoxHandler;
            _FileSystemWatcher.Error += OnError;

            _FileSystemWatcher.Filter = Rules.Filter;
            _FileSystemWatcher.IncludeSubdirectories = false;
            _FileSystemWatcher.EnableRaisingEvents = true;
        }

        public static void messageBoxHandler(object sender, FileSystemEventArgs e)
        {
            MessageBox.Show($"{e.ChangeType.ToString()}: {e.FullPath}");
        }
        private void moveFileHandler(object sender, FileSystemEventArgs e)
        {
            
            var _movePath = Path.Join(Rules.MoveFilePath, DateTime.Now.ToString("MMddyyyyhhmm"),  e.Name );

            Directory.CreateDirectory(Path.Join(Rules.MoveFilePath, DateTime.Now.ToString("MMddyyyyhhmm")));
            try
            {
                File.Move(e.FullPath, _movePath);
            } 
            catch (Exception ex) 
            {
                MessageBox.Show(ex.ToString());
            }
            
           
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
