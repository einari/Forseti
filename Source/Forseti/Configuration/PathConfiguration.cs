using System.Collections.Generic;

namespace Forseti.Configuration
{
    public class PathConfiguration
    {
        public List<string> Paths { get; private set; }

        public PathConfiguration()
        {
            Paths = new List<string>();
        }

        public PathConfiguration Add(string path)
        {
            Paths.Add(path);
            return this;
        }
    }
}
