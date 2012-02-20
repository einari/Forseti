using System;
using System.Collections.Generic;

namespace Forseti.Files
{
    public class FileSystemWatcher : IFileSystemWatcher
    {
        System.IO.FileSystemWatcher _actualWatcher;
        List<FileChanged> _subscribers = new List<FileChanged>();


        public FileSystemWatcher()
        { 
            var currentDirectory = System.IO.Directory.GetCurrentDirectory();
            _actualWatcher = new System.IO.FileSystemWatcher(currentDirectory, "*.js");
            _actualWatcher.Changed += _actualWatcher_Changed;
            _actualWatcher.IncludeSubdirectories = true;
            _actualWatcher.NotifyFilter = System.IO.NotifyFilters.LastWrite;
            _actualWatcher.EnableRaisingEvents = true;
        }

        void _actualWatcher_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber(GetFileChangeStatus(e), (File)e.FullPath);
            }
        }

        FileChange GetFileChangeStatus(System.IO.FileSystemEventArgs e)
        {
            switch (e.ChangeType)
            {
                case System.IO.WatcherChangeTypes.Changed:
                    return FileChange.Modified;
                case System.IO.WatcherChangeTypes.Created:
                    return FileChange.Added;
                case System.IO.WatcherChangeTypes.Deleted:
                    return FileChange.Deleted;
                case System.IO.WatcherChangeTypes.Renamed:
                    return FileChange.Renamed;
            }
            return FileChange.Unknown;
        }


		public void SubscribeToChanges (FileChanged changed)
		{
            _subscribers.Add(changed);
		}
    }
}
