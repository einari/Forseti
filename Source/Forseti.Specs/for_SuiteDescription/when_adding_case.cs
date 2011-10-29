using System.Linq;
using Machine.Specifications;

namespace Forseti.Specs.for_SuiteDescription
{
    public class when_adding_case
    {
        static SuiteDescription description;
        static Case @case;

        Establish context = () =>
        {
            description = new SuiteDescription();
            @case = new Case();
        };

        Because of = () => description.AddCase(@case);

        It should_have_one_case = () => description.Cases.Count().ShouldEqual(1);
        It should_contain_the_added_case = () => description.Cases.ShouldContain(@case);
    }
}
