using System.Collections.Generic;
using System.Linq;
using Forseti.Scripting;
using Machine.Specifications;

namespace Forseti.Specs
{
	public class when_finding_from_content_with_one_dependency
	{
		static string file = "some_script_file.js";
		static string content = string.Format ("/// <dependency path=\"{0}\"/>", file);
		static DependencyParser parser;
		static IEnumerable<string> result;
		
		Establish context = () => parser = new DependencyParser();
		
		Because of = () => result = parser.FindDependencies(content);
		
		It should_return_one_file = () => result.Count ().ShouldEqual(1);
		It should_return_dependency = () => result.First().ShouldEqual(file);
	}
}

