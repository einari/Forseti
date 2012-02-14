using Forseti.Harnesses;
using Machine.Specifications;

namespace Forseti.Specs.Harnesses.for_Harness
{
    public class when_checking_if_file_is_description_file_and_it_matches_as_system_and_description
    {
        static Harness harness;
        static bool result;

        Establish context = () =>
        {
            harness = new Harness
            {
                SystemsSearchPath = "Scripts/{system}.js",
                DescriptionsSearchPath = "Scripts/{system}.js"
            };
        };

        Because of = () => result = harness.IsDescription("Scripts/something.js");

        It should_not_acknowledge_file_as_a_system_file = () => result.ShouldBeFalse();
    }
}
