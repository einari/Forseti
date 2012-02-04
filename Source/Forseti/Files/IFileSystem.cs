using System.Collections.Generic;

namespace Forseti.Files
{
    public interface IFileSystem
    {
		IEnumerable<File> GetAllFiles(string filter); 
		
		string GetActualPath(string path);
        string[] GetAll(string path);
    }
}
