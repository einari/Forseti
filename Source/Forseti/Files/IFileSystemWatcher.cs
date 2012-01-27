namespace Forseti.Files
{
    public interface IFileSystemWatcher
    {
        FolderWatch WatchFolder(string path, string filter, bool recursive);
    }
}
