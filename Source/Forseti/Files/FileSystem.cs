using System;

namespace Forseti.Files
{
	public abstract class FileSystem : IFileSystem
	{
		public FileSystem ()
		{
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

