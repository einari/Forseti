using System.Collections.Generic;

namespace Forseti
{
    public class SuiteDescription
    {
        public string File { get; set; }
        public List<Case> Cases { get; set; }
    }
}
