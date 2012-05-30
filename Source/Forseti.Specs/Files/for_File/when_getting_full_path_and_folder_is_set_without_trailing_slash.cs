using Forseti.Files;
using Machine.Specifications;

namespace Forseti.Specs.Files.for_File
{
    public class when_getting_full_path_and_folder_is_set_without_trailing_slash
    {
        const string filename = "something.txt";
        const string folder = @"c:/a_folder";
        const string expected = folder + @"/" + filename;

        static File file;

        Establish context = () => file = new File { Filename = filename, Folder = folder };

        It should_add_slash_between_folder_and_filename = () => file.FullPath.ShouldEqual(expected);
    }
}
