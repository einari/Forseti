using System.Linq;
using Forseti.Files;
using Forseti.Suites;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Forseti.Specs.Suites.for_Description
{
    public class when_adding_context_as_file
    {
        static Description description;
        static IFile file;

        Establish context = () =>
        {
            description = new Description((File)"Specs/for_something/when_doing_stuff.js");
            file = new Mock<IFile>().Object;
        };

        Because of = () => description.AddContext(file);

        It should_have_one_contextx = () => description.Contexts.Count().ShouldEqual(1);
        It should_contain_the_added_file = () => description.Contexts.ShouldContain(file);
    }
}
