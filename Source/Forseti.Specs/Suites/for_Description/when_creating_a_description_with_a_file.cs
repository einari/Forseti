using Forseti.Suites;
using Machine.Specifications;
using Forseti.Files;

namespace Forseti.Specs.Suites.for_Description
{
    public class when_creating_a_description_with_a_file
    {
        const string description_name = "when_doing_stuff";
        static Description  description;
        static IFile file;

        Because of = () => 
        {
            file = (File)("Specs/for_something/"+description_name+".js");
            description = new Description(file);
        };

        It should_extract_name = () => description.Name.ShouldEqual(description_name);
        It should_set_file = () => description.File.ShouldEqual(file);
    }
}
