using Machine.Specifications;
using Forseti.Harnesses;
using Moq;
using It=Machine.Specifications.It;

namespace Forseti.Specs.for_HarnessManager
{
    [Subject(typeof(HarnessManager))]
    public class when_executing_one_suite_with_one_description_and_one_case
    {
        static Suite suite;
        static SuiteDescription description;
        static Case @case;
        static HarnessManager manager;
        static Mock<IScriptEngine>  script_engine_mock;
        static Harness harness_result;

        Establish context = () =>
        {
            suite = new Suite();
            description = new SuiteDescription();
            suite.AddDescription(description);
            
            @case = new Case();
            description.AddCase(@case);

            script_engine_mock = new Mock<IScriptEngine>();
            manager = new HarnessManager(script_engine_mock.Object);

            script_engine_mock.Setup(s=>s.Execute(Moq.It.IsAny<Harness>())).Callback((Harness h) => harness_result = h);
        };

        Because of = () => manager.Execute(new[] {suite});

        It should_forward_a_harness_to_the_script_engine = () => harness_result.ShouldNotBeNull();
        It should_forward_case = () => harness_result.Cases.ShouldContainOnly(@case);
    }
}
