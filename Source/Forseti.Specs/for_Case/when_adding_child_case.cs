using System.Linq;
using Machine.Specifications;

namespace Forseti.Specs.for_Case
{
    [Subject(typeof(Case))]
    public class when_adding_child_case
    {
        static Case parent;
        static Case child;


        Establish context = () =>
        {
            parent = new Case();
            child = new Case();
        };

        Because of = () => parent.AddChildCase(child);

        It should_have_one_child = () => parent.Children.Count().ShouldEqual(1);
        It should_contain_the_added_child = () => parent.Children.ShouldContain(child);
        It should_set_the_parent_of_the_child_to_the_parent_its_added_to = () => child.Parent.ShouldEqual(parent);
    }
}
