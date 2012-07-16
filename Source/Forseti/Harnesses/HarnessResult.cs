using System.Collections.Generic;
using Forseti.Suites;

namespace Forseti.Harnesses
{
    public class HarnessResult
    {
        List<Suite> _affectedSuites = new List<Suite>();

        public IEnumerable<Suite> AffectedSuites { get { return _affectedSuites; } }

        public int TotalDescriptionCount { get; private set; }
        public int SuccessfulDescriptionCount { get; private set; }
        public int FailedDescriptionCount { get; private set; }

        public void AddAffectedSuite(Suite suite)
        {
            if (_affectedSuites.Contains(suite))
                return;

            _affectedSuites.Add(suite);
        }
    }
}
