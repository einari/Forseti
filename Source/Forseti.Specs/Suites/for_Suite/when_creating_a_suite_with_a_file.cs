using Forseti.Suites;
using Machine.Specifications;
using Forseti.Files;

namespace Forseti.Specs.Suites.for_Suite
{
    public class when_creating_a_suite_with_a_file
    {
        const string folder = "Scripts";
        const string system = "something";

        const string system_path = folder+"/"+system+".js";

        static Suite    suite;
        static IFile file;

        Because context = () =>
        {
            file = (File)system_path;
            suite = new Suite(file);
        };

        It should_extract_the_system_name = () => suite.System.ShouldEqual(system);
        It should_set_file = () => suite.SystemFile.ShouldEqual(file);
    }
}
