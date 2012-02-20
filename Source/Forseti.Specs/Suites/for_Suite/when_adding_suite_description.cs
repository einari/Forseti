using System.Linq;
using Machine.Specifications;
using Forseti.Suites;
using Forseti.Files;

namespace Forseti.Specs.Suites.for_Suite
{
    [Subject(typeof(Suite))]
    public class when_adding_suite_description
    {
        static Suite    suite;
        static Description description;

        Establish context = () =>
        {
            suite = new Suite((File)"Script/something.js");
            description = new Description((File)"Specs/for_something/when_doing_stuff.js");
        };

        Because of = () => suite.AddDescription(description);

        It should_have_one_description = () => suite.Descriptions.Count().ShouldEqual(1);
        It should_have_the_added_description = () => suite.Descriptions.ShouldContain(description);
        It should_set_suite_on_description_to_the_suite_its_added_to = () => description.Suite.ShouldEqual(suite);
    }
}
