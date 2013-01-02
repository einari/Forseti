using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forseti.Files;

namespace Forseti.Specs.Fakes
{
    public class FakeFileSystemWatcher : IFileSystemWatcher
    {

        List<FileChanged> _subcribers = new List<FileChanged>();

        public void SubscribeToChanges(FileChanged changed)
        {
            _subcribers.Add(changed);
        }

        public void TriggerChange(FileChange changeType, IFile file)
        {
            foreach (var subscriber in _subcribers)
                subscriber.Invoke(changeType, file);
        }
    }
}
