using Machine.Specifications;
using Forseti.Harnesses;

namespace Forseti.Specs.for_HarnessManager
{
    [Subject(typeof(HarnessManager))]
    public class when_executing_multiple_suites
    {
        static Suite[] files = new[] {
            new Suite { System = "FirstSystem", Cases = new[] { "FirstCase","SecondCase" } }
        };



    }
}
