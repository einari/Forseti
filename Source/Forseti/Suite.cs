using System.Collections.Generic;

namespace Forseti
{
    public class Suite
    {
        List<SuiteDescription> _descriptions = new List<SuiteDescription>();

        public string System { get; set; }
        public string SystemFile { get; set; }
        public IEnumerable<SuiteDescription> Descriptions { get { return _descriptions; } }

        public void AddDescription(SuiteDescription description)
        {
            _descriptions.Add(description);
        }
    }
}
