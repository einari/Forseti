namespace Forseti.Harnesses
{
    public interface IHarnessWatcher
    {
        void HarnessChanged(HarnessChangeType changeType, Harness harness);
    }
}
