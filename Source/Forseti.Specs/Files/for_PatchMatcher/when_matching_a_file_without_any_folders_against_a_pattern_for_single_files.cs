using Machine.Specifications;
using Forseti.Files;

namespace Forseti.Specs
{
	public class when_matching_a_file_without_any_folders_against_a_pattern_for_single_files
	{
		static PathMatcher matcher = new PathMatcher("{file}");
		static bool result;
		
		Because of = () => result = matcher.Matches ((File)"myFile.js");
		
		It should_match = () => result.ShouldBeTrue();
	}
}

