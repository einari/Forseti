using System;

namespace Forseti.Harnesses
{
    public interface IHarnessChangeManager
    {
        void RegisterWatcher(Type type);
        void NotifyChange(HarnessResult harnessResult, HarnessChangeType changeType);
    }
}
