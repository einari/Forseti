using Forseti.Harnesses;

namespace Forseti.Windows.Growl
{
    public class GrowlHarnessWatcher : IHarnessWatcher
    {
        public void HarnessChanged(HarnessChangeType changeType, HarnessResult result)
        {
            GrowlHelper.simpleGrowl("Forseti", string.Format(
                
                "Run complete with {0}\n"+
                "Took {1} seconds\n\n"+
                "Total tests : {2}\n"+
                "Successful tests : {3}\n"+
                "Failed tests : {4}"
            , 
            result.Harness.Framework.Name,
            result.TotalTime.TotalSeconds,
            result.TotalCaseCount, 
            result.SuccessfulCaseCount, 
            result.FailedCaseCount));
        }
    }
}
