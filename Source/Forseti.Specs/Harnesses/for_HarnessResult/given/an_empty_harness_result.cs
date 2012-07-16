using Forseti.Harnesses;
using Machine.Specifications;

namespace Forseti.Specs.Harnesses.for_HarnessResult.given
{
    public class an_empty_harness_result
    {
        protected static HarnessResult result;
        
        Establish context = () => result = new HarnessResult();
    }
}
