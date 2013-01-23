using System.Collections.Generic;
using Forseti.Harnesses;

namespace Forseti.Reporting
{
    public interface IReporter
    {
        void ReportOn(HarnessResult harnesses);
        void ReportOn(IEnumerable<HarnessResult> harnesses);
        void ReportSummary(IEnumerable<HarnessResult> harnessResults);
    }
}
