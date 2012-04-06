using System;
using System.IO;

namespace Forseti.Files
{
    public class File : IFile
    {
        static string _currentDirectory;
		
		string _fileName;
		string _folder;

        static File()
        {
            _currentDirectory = AdjustPath(Directory.GetCurrentDirectory());
            if (!EndsWithPathSeparator(_currentDirectory))
                _currentDirectory += "/";
        }
		
		

        public string Filename 
		{ 
			get { return _fileName; }
			set { _fileName = AdjustPath (value); }
		}
		
        public string Folder 
		{ 
			get { return _folder; }
			set { _folder = AdjustPath(value); }
		}
		
        public string FullPath
        {
            get
            {
                if (string.IsNullOrEmpty(Folder))
                    return Filename;

                return string.Format("{0}{1}{2}", Folder, EndsWithPathSeparator(Folder)?string.Empty:@"/", Filename);
            }
        }
		

        public string ReadAllText()
        {
            return System.IO.File.ReadAllText(FullPath);
        }

        public static implicit operator File(string path)
        {
            return new File
            {
                Filename = System.IO.Path.GetFileName(path),
                Folder = System.IO.Path.GetDirectoryName(path)
            };
        }
		
		public static string operator +(string a, File b)
		{
			return string.Format ("{0}{1}{2}",
			                      a,
			                      (a.EndsWith("\\") || a.EndsWith("/")) ? string.Empty : "/",
			                      b.FullPath
			                      );
		}
		
		
		
		public override string ToString ()
		{
			return string.Format ("[File: Filename={0}, Folder={1}, FullPath={2}]", Filename, Folder, FullPath);
		}
		
		static bool EndsWithPathSeparator(string path)
		{
			return path.EndsWith(@"\") || path.EndsWith(@"/");
		}

        public string RelativePath { get { return FullPath.Replace(_currentDirectory, string.Empty); } }

        public override bool Equals(object obj)
        {
            return ((IFile)obj).FullPath == FullPath;
        }
		
		public override int GetHashCode ()
		{
			return base.GetHashCode ();
		}
		
		static string AdjustPath(string path)
		{
			return path
				.Replace ("\\\\","/")
				.Replace("\\","/");
		}
    }
}
