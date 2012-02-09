using System;

namespace Forseti.Files
{
    public class File : IFile
    {
        public string Filename { get; set; }
        public string Folder { get; set; }
        public string FullPath
        {
            get
            {
                if (string.IsNullOrEmpty(Folder))
                    return Filename;

                return string.Format("{0}{1}{2}", Folder, Folder.EndsWith(@"\")?string.Empty:@"\", Filename);
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
		
		
		public override string ToString ()
		{
			return string.Format ("[File: Filename={0}, Folder={1}, FullPath={2}]", Filename, Folder, FullPath);
		}
    }
}
