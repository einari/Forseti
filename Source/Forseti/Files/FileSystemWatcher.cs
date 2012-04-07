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
			_actualWatcher.Created += _actualWatcher_Created;
            _actualWatcher.Renamed += _actualWatcher_Renamed;
            _actualWatcher.IncludeSubdirectories = true;
            _actualWatcher.NotifyFilter = System.IO.NotifyFilters.LastWrite|System.IO.NotifyFilters.CreationTime;
            _actualWatcher.EnableRaisingEvents = true;
        }

		public void SubscribeToChanges (FileChanged changed)
		{
            _subscribers.Add(changed);
		}
	
        void _actualWatcher_Renamed(object sender, System.IO.RenamedEventArgs e)
        {
            var oldFile = (File)e.OldFullPath;
            NotifySubscribers(FileChange.Deleted, oldFile);
            var file = (File)e.FullPath;
            NotifySubscribers(FileChange.Added, file);
        }

		void _actualWatcher_Created(object sender, System.IO.FileSystemEventArgs e)
		{
			var file = (File)e.FullPath;
			NotifySubscribers(FileChange.Added, file);
		}

        void _actualWatcher_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
			var file = (File)e.FullPath;
			var fileChange = GetFileChangeStatus(e);
			NotifySubscribers(fileChange, file);
        }
		
		void NotifySubscribers(FileChange fileChange, IFile file)
		{
            foreach (var subscriber in _subscribers)
                subscriber(fileChange, file);
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


    }
}
