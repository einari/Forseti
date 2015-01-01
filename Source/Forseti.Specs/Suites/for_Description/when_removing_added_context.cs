using System.Linq;
using Forseti.Files;
using Forseti.Suites;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Forseti.Specs.Suites.for_Description
{
    public class when_removing_added_context
    {
        static Description description;
        static IFile file;

        Establish context = () =>
        {
            description = new Description((File)"Specs/for_something/when_doing_stuff.js");
            file = new Mock<IFile>().Object;
            description.AddContext(file);
        };

        Because of = () => description.RemoveContext(file);

        It should_have_no_context = () => description.Contexts.Count().ShouldEqual(0);
        It should_not_contain_the_added_contexts = () => description.Contexts.ShouldNotContain(file);
    }
}
