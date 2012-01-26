using System.Collections.Generic;

namespace Forseti.Suites
{
    public class SuiteDescription
    {
        List<Case> _cases = new List<Case>();

        public Suite Suite { get; set; }
        public string File { get; set; }
        public IEnumerable<Case> Cases 
        { 
            get { return _cases; }
            set
            {
                _cases.Clear();
                _cases.AddRange(value);
            }
        }

        public void AddCase(Case @case)
        {
            @case.Description = this;
            _cases.Add(@case);
        }
    }
}
