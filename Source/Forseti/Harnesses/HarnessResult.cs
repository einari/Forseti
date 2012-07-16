using System;
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
            StartTime = DateTime.Now;
            EndTime = DateTime.Now;
        }

        public Harness Harness { get; set; }
        public IEnumerable<Suite> AffectedSuites { get { return _affectedSuites; } }

        public int TotalCaseCount { get { return GetCount(); } }
        public int SuccessfulCaseCount { get { return GetCount(r => r.Success == true); } }
        public int FailedCaseCount { get { return GetCount(r => r.Success == false); } }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan TotalTime { get { return EndTime.Subtract(StartTime); } }

        public void AddAffectedSuite(Suite suite)
        {
            if (_affectedSuites.Contains(suite))
                return;

            _affectedSuites.Add(suite);
        }

        int GetCount(Func<CaseResult, bool> filter = null)
        {
            var count = 0;
            _affectedSuites.ForEach(s =>
            {
                s.Descriptions.ForEach(d =>
                {
                    d.Cases.ForEach(c =>
                    {
                        // Todo : fix so that there is no default case added just so the runner can run...  In fact, detect cases upfront for each framework
                        if (string.IsNullOrEmpty(c.Name))
                            return;

                        if (filter == null)
                            count++;
                        else if (filter(c.Result))
                            count++;
                    });
                });
            });
            return count;
        }
    }
}
