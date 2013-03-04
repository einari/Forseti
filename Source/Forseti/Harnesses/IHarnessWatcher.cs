namespace Forseti.Harnesses
{
    public interface IHarnessWatcher
    {
        void HarnessChanged(HarnessChangeType changeType, HarnessResult harness);
    }
}
