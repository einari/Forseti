using System.Collections.Generic;
using System.Linq;
using Forseti.Scripting;
using Machine.Specifications;

namespace Forseti.Specs.Scripting.for_DependencyParser
{
    public class when_finding_from_content_with_multiple_dependencies
    {
        static string first_file = "first_script_file.js";
        static string second_file = "second_script_file.js";

        static string content = string.Format("/// <reference path=\"{0}\"/>\n/// <reference path=\"{1}\"/>", first_file, second_file);

        static DependencyParser parser;
        static IEnumerable<string> result;

        Establish context = () => parser = new DependencyParser();

        Because of = () => result = parser.FindDependencies(content);

        It should_return_two_dependencies = () => result.Count().ShouldEqual(2);
        It should_have_the_expected_dependencies = () => result.ShouldContainOnly(first_file, second_file);
    }
}
