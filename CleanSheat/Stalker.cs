using System;
using Serilog;
using System.IO;

namespace CleanSheet
{
    class Stalker
    {

        private FileSystemWatcher Watcher;
        private WatcherRule Rules;
        public static ILogger Log = null;
        public void Start(WatcherRule _rules, ILogger _log)
        {
            Log = _log;
            Rules = _rules;
            Log.Information($"Stalker started for: {Rules.FilePath}");
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

            _FileSystemWatcher.Changed += this.moveFileHandler;
            _FileSystemWatcher.Created += this.moveFileHandler;
            _FileSystemWatcher.Error += OnError;

            _FileSystemWatcher.Filter = Rules.Filter;
            _FileSystemWatcher.IncludeSubdirectories = false;
            _FileSystemWatcher.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            Watcher.Dispose(); 
        }

        public void messageBoxHandler(object sender, FileSystemEventArgs e)
        {
            
            Log.Information($"{e.ChangeType.ToString()}: {e.FullPath}");
        }
        public void moveFileHandler(object sender, FileSystemEventArgs e)
        {
            

            Log.Information("File created: {name}", e.Name);
            var _movePath = Path.Join(Rules.MoveFilePath, DateTime.Now.ToString("MMddyyyyhhmm"), e.Name);

            Directory.CreateDirectory(Path.Join(Rules.MoveFilePath, DateTime.Now.ToString("MMddyyyyhhmm")));


            bool _FileMoved = false;
            while (_FileMoved == false)
            {
                try
                {
                    if (File.Exists(e.FullPath))
                    {
                        File.Move(e.FullPath, _movePath);
                        _FileMoved = true;
                        Log.Information("Moved!");
                    } else
                    {
                        Log.Warning("File no longer exists!");
                    }
                }
                catch (Exception ex)
                {
                    Log.Information("Not able to move file: {Ex}", ex.ToString());
                }
                System.Threading.Thread.Sleep(1000);
            }



        }


        public static void OnError(object sender, ErrorEventArgs e)
        {
            Log.Error("Error with FileSystemWatcher: {Ex}", e.GetException().Message);
        }



    }


}
