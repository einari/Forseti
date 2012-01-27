using System;

namespace Forseti.Files
{
    public class File
    {
        public string Filename { get; set; }
        public string Folder { get; set; }

        public DateTime LastModified { get; set; }
    }
}
