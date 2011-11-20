using System.Linq;
using Forseti.Suites;
using Machine.Specifications;

namespace Forseti.Specs.Suites.for_SuiteDescription
{
    [Subject(typeof(SuiteDescription))]
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
        It should_set_the_description_to_the_description_it_is_added_to = () => @case.Description.ShouldEqual(description);
    }
}
