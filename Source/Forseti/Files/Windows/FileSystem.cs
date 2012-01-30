using System;
using System.Text;
using System.Runtime.InteropServices;

namespace Forseti.Files.Windows
{
	public class FileSystem : Files.FileSystem
	{		
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetShortPathName(
            [MarshalAs(UnmanagedType.LPTStr)]string path,
            [MarshalAs(UnmanagedType.LPTStr)]StringBuilder shortPath,
            int shortPathLength);
		
		public override string GetActualPath (string path)
		{
            var shortPath = new StringBuilder(255);
            GetShortPathName(path, shortPath, shortPath.Capacity);
			return shortPath.ToString ();
		}
	}
}

