using Machine.Specifications;
using Forseti.Files;

namespace Forseti.Specs
{
	public class when_matching_a_file_with_two_fragments_and_pattern_with_matching_fragments
	{
		static PathMatcher matcher = new PathMatcher("{area}/specs/for_{system}/{file}");
		static bool result;
		
		Because of = () => result = matcher.Matches((File)"someArea/specs/for_mySystem/when_doing_something.js");
		
		It should_match = () => result.ShouldBeTrue ();
	}
}

