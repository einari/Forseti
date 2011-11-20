using Moq;
using Machine.Specifications;
using Forseti.Harnesses;
using Forseti.Scripting;

namespace Forseti.Specs.for_HarnessManager.given
{
    public class a_harness_manager
    {
        protected static Mock<IScriptEngine> script_engine_mock;
        protected static Mock<IPageGenerator> page_generator_mock;
        protected static HarnessManager harness_manager;

        Establish context = () =>
        {
            script_engine_mock = new Mock<IScriptEngine>();
            page_generator_mock = new Mock<IPageGenerator>();
            harness_manager = new HarnessManager(script_engine_mock.Object, page_generator_mock.Object);
        };
    }
}
