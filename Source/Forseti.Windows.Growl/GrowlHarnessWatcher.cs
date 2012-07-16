using Forseti.Harnesses;

namespace Forseti.Windows.Growl
{
    public class GrowlHarnessWatcher : IHarnessWatcher
    {
        public void HarnessChanged(HarnessChangeType changeType, HarnessResult harness)
        {
            GrowlHelper.simpleGrowl("Forseti", "Run complete");
        }
    }
}
