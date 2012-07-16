using System.Collections.Generic;
using Forseti.Extensions;
using Forseti.Suites;

namespace Forseti.Harnesses
{
    public class HarnessResult
    {
        List<Suite> _affectedSuites = new List<Suite>();

        public HarnessResult(Harness harness)
        {
            Harness = harness;
        }


        public Harness Harness { get; set; }
        public IEnumerable<Suite> AffectedSuites { get { return _affectedSuites; } }

        public int TotalCaseCount { get; private set; }
        public int SuccessfulCaseCount { get; private set; }
        public int FailedCaseCount { get; private set; }

        public void AddAffectedSuite(Suite suite)
        {
            if (_affectedSuites.Contains(suite))
                return;

            _affectedSuites.Add(suite);

            HandleCaseCounts();
        }

        void HandleCaseCounts()
        {
            TotalCaseCount = 0;
            SuccessfulCaseCount = 0;
            FailedCaseCount = 0;

            _affectedSuites.ForEach(s => 
            {
                s.Descriptions.ForEach(d=> 
                {
                    d.Cases.ForEach(c =>
                    {
                        TotalCaseCount++;
                        if (c.Result.Success)
                            SuccessfulCaseCount++;
                        else
                            FailedCaseCount++;
                    });
                });
            });
        }
    }
}
