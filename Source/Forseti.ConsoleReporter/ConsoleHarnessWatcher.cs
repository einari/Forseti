using Forseti.Harnesses;
using Forseti.Reporting;

namespace Forseti.ConsoleReporter
{
    public class ConsoleHarnessWatcher : IHarnessWatcher
    {
        IReporter _reporter;

        public ConsoleHarnessWatcher(IReporter reporter) 
        {
            _reporter = reporter;
        }

        public void HarnessChanged(HarnessChangeType changeType, HarnessResult result)
        {
            _reporter.ReportOn(result);
        }

        
    }
}
