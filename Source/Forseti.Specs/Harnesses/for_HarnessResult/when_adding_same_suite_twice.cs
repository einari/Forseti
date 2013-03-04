using System.Linq;
using Forseti.Files;
using Forseti.Suites;
using Machine.Specifications;

namespace Forseti.Specs.Harnesses.for_HarnessResult
{
    public class when_adding_same_suite_twice : given.an_empty_harness_result
    {
        static Suite suite = new Suite((File)"Something");

        Because of = () =>
        {
            result.AddAffectedSuite(suite);
            result.AddAffectedSuite(suite);
        };

        It should_have_one_suite = () => result.AffectedSuites.Count().ShouldEqual(1);
    }
}
