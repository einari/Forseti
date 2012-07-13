using Forseti.Harnesses;
using Forseti.Suites;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Forseti.Specs.Harnesses.for_HarnessManager
{
    [Subject(typeof(HarnessManager))]
    public class when_executing : given.a_harness_manager_and_a_framework
    {
        static Harness harness;

        Establish context = () => harness = new Harness(framework_mock.Object);

        Because of = () => harness_manager.Execute(harness, new Suite[0]);

        It should_report_run_complete_to_change_manager = () => harness_change_manager_mock.Verify(h=>h.NotifyChange(harness, HarnessChangeType.RunComplete), Times.Once());
    }
}
