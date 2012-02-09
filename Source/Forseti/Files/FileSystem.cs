using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Forseti.Files
{
	public abstract class FileSystem : IFileSystem
	{
		string _currentDirectory;
		
		public FileSystem ()
		{
			_currentDirectory = Directory.GetCurrentDirectory();
		}
		
		public IEnumerable<File> GetAllFiles(string filter)
		{
			var files = Directory.GetFiles(_currentDirectory,filter,SearchOption.AllDirectories);
			return files.Select(f=>(File)f).ToArray ();
		}
		
		public virtual string GetActualPath (string path)
		{
			return path;
		}

		public string[] GetAll (string path)
		{
			throw new NotImplementedException ();
		}
	}
}

