using System.Linq;
using Forseti.Harnesses;
using Machine.Specifications;
using Forseti.Files;

namespace Forseti.Specs.Harnesses.for_Harness
{
    public class when_handling_one_system_and_one_related_description
    {
        const string script_path = "Scripts";
        const string system_name = "something";
        const string description_name = "when_the_moon_hits_the_sky";

        static Harness harness;

        static IFile[] files;

        Establish context = () =>
        {
            harness = new Harness
            {
                SystemsSearchPath = "Scripts/{system}.js",
                DescriptionsSearchPath = "Specs/for_{system}/{description}.js"
            };

            files = new[] {
                ((File)(script_path+"/"+system_name+".js")),
                ((File)("Specs/for_"+system_name+"/"+description_name+".js")),
            };
        };

        Because of = () => harness.HandleFiles(files);

        It should_add_a_suite_with_the_system = () => harness.Suites.First().System.ShouldEqual(system_name);
        It should_add_description_to_same_suite = () => harness.Suites.First().Descriptions.First().Name.ShouldEqual(description_name);
    }
}
