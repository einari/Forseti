using Forseti.Harnesses;
using Forseti.Pages;
using Forseti.Suites;
using Machine.Specifications;
using It = Machine.Specifications.It;

namespace Forseti.Specs.for_HarnessManager
{
    [Subject(typeof(HarnessManager))]
    public class when_executing_one_suite_with_one_description_and_one_case : given.a_harness_manager
    {
        static Suite suite;
        static SuiteDescription description;
        static Case @case;
        static Harness harness_result;
        static Page expected_page;

        Establish context = () =>
        {
            suite = new Suite();
            description = new SuiteDescription();
            suite.AddDescription(description);
            
            @case = new Case();
            description.AddCase(@case);
            expected_page = new Page();
            page_generator_mock.Setup(p=>p.GenerateFrom(Moq.It.IsAny<Harness>())).Callback((Harness h) => harness_result = h).Returns(expected_page);
        };

        Because of = () => harness_manager.Execute(new[] {suite});

        It should_generate_a_page = () => page_generator_mock.Verify(p => p.GenerateFrom(Moq.It.IsAny<Harness>()));
        It should_generate_a_page_from_cases = () => harness_result.Cases.ShouldContainOnly(@case);
        It should_execute_script_for_page = () => script_engine_mock.Verify(s => s.Execute(expected_page));
    }
}
