using Forseti.Frameworks;
using Forseti.Harnesses;
using Machine.Specifications;
using Moq;

namespace Forseti.Specs.Harnesses.for_HarnessResult.given
{
    public class an_empty_harness_result
    {
        protected static Mock<IFramework> framework_mock;
        protected static Harness harness;
        protected static HarnessResult result;


        Establish context = () =>
        {
            framework_mock = new Mock<IFramework>();
            harness = new Harness(framework_mock.Object);
            result = new HarnessResult(harness);
        };
    }
}
