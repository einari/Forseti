using System.Collections.Generic;
using System.IO;
using Forseti.Files;
using System;

namespace Forseti.Suites
{
    public class Suite
    {
        List<Description> _descriptions = new List<Description>();

        public string System { get; private set; }
        public IFile SystemFile { get; private set; }

        public DateTime LastRun { get; set; }

        public Suite(IFile systemFile)
        {
            System = Path.GetFileNameWithoutExtension(systemFile.FullPath);
            SystemFile = systemFile;

            LastRun = DateTime.MinValue;
        }

        public IEnumerable<Description> Descriptions { get { return _descriptions; } }

        public void AddDescription(Description description)
        {
            _descriptions.Add(description);
            description.Suite = this;
        }
    }
}
