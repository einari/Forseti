using System;
using System.Collections.Generic;
using StructureMap;

namespace Forseti.Harnesses
{
    public class HarnessChangeManager : IHarnessChangeManager
    {
        IContainer _container;
        List<IHarnessWatcher> _watchers = new List<IHarnessWatcher>();

        public HarnessChangeManager(IContainer container)
        {
            _container = container;
        }

        public void RegisterWatcher(Type type)
        {
            var watcher = _container.GetInstance(type) as IHarnessWatcher;
            _watchers.Add(watcher);
        }

        public void NotifyChange(HarnessResult harnessResult, HarnessChangeType changeType)
        {
            foreach (var watcher in _watchers)
                watcher.HarnessChanged(changeType, harnessResult);
        }
    }
}
