using Machine.Specifications;
using Forseti.Harnesses;
using Moq;
using It = Machine.Specifications.It;

namespace Forseti.Specs.Harnesses.for_HarnessManager
{
    public class when_adding_a_harness : given.a_harness_manager
    {
        static Harness harness;

        Establish context = () => harness = new Harness();

        Because of = () => harness_manager.Add(harness);

        It should_get_all_files_from_file_system = () => file_system_mock.Verify(f => f.GetAllFiles(Moq.It.IsAny<string>()), Times.Once());
        It should_have_the_harness = () => harness_manager.Harnesses.ShouldContain(harness);
    }
}
