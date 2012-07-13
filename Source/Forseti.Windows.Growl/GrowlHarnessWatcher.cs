using Forseti.Harnesses;

namespace Forseti.Windows.Growl
{
    public class GrowlHarnessWatcher : IHarnessWatcher
    {
        public void HarnessChanged(HarnessChangeType changeType, Harness harness)
        {
            GrowlHelper.simpleGrowl("Forseti", "Run complete");
        }
    }
}
