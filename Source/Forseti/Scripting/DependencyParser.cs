using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Forseti.Scripting
{
	public class DependencyParser : IDependencyParser
	{
		static Regex	_regex = new Regex("/// <reference path=\"([\\d\\w.]*)\"\\s*/>");
		
		public IEnumerable<string> FindDependencies (string content)
		{
			var dependencies = new List<string>();
			var matches = _regex.Matches(content);
            foreach (Match match in matches)
            {
                if (match.Groups.Count > 1)
                    for (var groupIndex = 1; groupIndex < match.Groups.Count; groupIndex++)
                        dependencies.Add(match.Groups[groupIndex].Value);
            }

			return dependencies;
		}
	}
}

