using System.Linq;
using Forseti.Suites;
using Machine.Specifications;

namespace Forseti.Specs.Suites.for_Case
{
    [Subject(typeof(Case))]
    public class when_adding_child_case
    {
        static Case parent;
        static Case child;


        Establish context = () =>
        {
            parent = new Case();
            parent.Description = new SuiteDescription();
            child = new Case();
        };

        Because of = () => parent.AddChildCase(child);

        It should_have_one_child = () => parent.Children.Count().ShouldEqual(1);
        It should_contain_the_added_child = () => parent.Children.ShouldContain(child);
        It should_set_the_parent_of_the_child_to_the_parent_its_added_to = () => child.Parent.ShouldEqual(parent);
        It should_set_the_description_to_the_same_as_the_parent = () => child.Description.ShouldEqual(parent.Description);
    }
}
