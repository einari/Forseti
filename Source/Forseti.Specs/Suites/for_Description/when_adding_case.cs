using System.Linq;
using Forseti.Suites;
using Machine.Specifications;
using Forseti.Files;

namespace Forseti.Specs.Suites.for_Description
{
    [Subject(typeof(Description))]
    public class when_adding_case
    {
        static Description description;
        static Case @case;

        Establish context = () =>
        {
            description = new Description((File)"Specs/for_something/when_doing_stuff.js");
            @case = new Case();
        };

        Because of = () => description.AddCase(@case);

        It should_have_one_case = () => description.Cases.Count().ShouldEqual(1);
        It should_contain_the_added_case = () => description.Cases.ShouldContain(@case);
        It should_set_the_description_to_the_description_it_is_added_to = () => @case.Description.ShouldEqual(description);
    }
}
