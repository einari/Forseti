using Forseti.Files;
using Machine.Specifications;

namespace Forseti.Specs.Files.for_File
{
    public class when_getting_full_path_and_folder_is_not_set
    {
        static File    file;

        Establish context = () => file = new File { Filename = "something.txt" };

        It should_return_only_the_file_name = () => file.FullPath.ShouldEqual(file.Filename);
    }
}
