
namespace Forseti.Files
{
    public interface IFileSystem
    {
		string GetActualPath(string path);
        string[] GetAll(string path);
    }
}
