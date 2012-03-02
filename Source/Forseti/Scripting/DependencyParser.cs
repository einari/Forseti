using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Forseti.Scripting
{
	public class DependencyParser : IDependencyParser
	{
		static Regex	_regex = new Regex("/// <reference path=\"([\\w]*)\"\\s*/>");
		
		public IEnumerable<string> FindDependencies (string content)
		{
			var dependencies = new List<string>();
			
			var match = _regex.Match(content);
			
			
			foreach( Capture capture in match.Captures )
			{
				dependencies.Add (capture.Value);
			}
			
			return dependencies;
			
		}
	}
}

