
namespace Forseti.Files
{
    public class FolderWatch
    {
        public string Folder { get; set; }

        public event FileChanged FileChanged;
        public event FileAdded FileAdded;
    }
}
