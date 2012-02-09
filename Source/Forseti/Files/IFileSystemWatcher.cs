namespace Forseti.Files
{
    public interface IFileSystemWatcher
    {
		void SubscribeToChanges(FileChanged changed);
    }
}
