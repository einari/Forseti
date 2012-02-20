
namespace Forseti.Files
{
    public interface IFile
    {
        string Filename { get; set; }
        string Folder { get; set; }
        string FullPath { get; }
        string RelativePath { get; }

        string ReadAllText();
    }
}
