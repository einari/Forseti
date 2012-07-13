using System;
using Forseti.Frameworks;
using Forseti.Harnesses;
using Machine.Specifications;
using Moq;
using StructureMap;
using It = Machine.Specifications.It;

namespace Forseti.Specs.Harnesses.for_HarnessChangeManager
{
    public class when_notifying_change_with_one_registered_watcher
    {
        static Mock<IFramework> framework_mock;
        static Mock<IContainer>    container_mock;
        static HarnessChangeManager    manager;
        static Harness harness;
        static HarnessChangeType change_type;
        static Mock<IHarnessWatcher> watcher_mock;
        static Type watcher_type;

        Establish context = () => {
            framework_mock = new Mock<IFramework>();
            change_type = HarnessChangeType.RunComplete;
            harness = new Harness(framework_mock.Object);
            container_mock = new Mock<IContainer>();
            manager = new HarnessChangeManager(container_mock.Object);
            watcher_mock = new Mock<IHarnessWatcher>();
            watcher_type = watcher_mock.Object.GetType();
            container_mock.Setup(c => c.GetInstance(watcher_type)).Returns(watcher_mock.Object);
            manager.RegisterWatcher(watcher_type);
        };

        Because of = () => manager.NotifyChange(harness, change_type);

        It should_notify_the_watcher = () => watcher_mock.Verify(w => w.HarnessChanged(change_type, harness), Times.Once());
    }
}
