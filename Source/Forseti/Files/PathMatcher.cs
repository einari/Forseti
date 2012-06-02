using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Forseti.Files
{
	public class PathMatcher : IPathMatcher
	{
		string _pathPattern;
		Regex _pathPatternRegex;
		Dictionary<string, int> _components = new Dictionary<string, int>();
		
		public PathMatcher(string pathPattern)
		{
			_pathPattern = pathPattern;
			BuildRegex();
		}
		
		public bool Matches(IFile file)
		{
			return _pathPatternRegex.IsMatch (file.FullPath);
		}
		
        void BuildRegex()
        {
            var replacePattern = @"\{[a-zA-Z]*\}";
            var componentMatches = Regex.Match(_pathPattern, replacePattern);
            
            for (var matchIndex = 0; matchIndex < componentMatches.Length; matchIndex++, componentMatches = componentMatches.NextMatch())
                _components[componentMatches.Value] = matchIndex;

            var pattern = Regex.Replace(_pathPattern, replacePattern, "([\\w.]*)");
            _pathPatternRegex = new Regex(pattern);
        }
	}
}

