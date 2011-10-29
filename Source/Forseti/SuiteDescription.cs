using System.Collections.Generic;

namespace Forseti
{
    public class SuiteDescription
    {
        List<Case> _cases = new List<Case>();

        public string File { get; set; }
        public IEnumerable<Case> Cases { get { return _cases; } }

        public void AddCase(Case @case)
        {
            _cases.Add(@case);
        }
    }
}
