using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Forseti.Suites;
using Forseti.Files;

namespace Forseti.Harnesses
{
    public class Harness
    {
		string _systemsSearchPath;
		string _descriptionsSearchPath;
        Regex _systemsSearchPathRegex;
        Regex _descriptionsSearchPathRegex;
        IEnumerable<string> _components;
		
		public string Name { get; set; }
		public string SystemsSearchPath 
		{ 
			get { return _systemsSearchPath; }
			set
			{
				_systemsSearchPath = value;
                _systemsSearchPathRegex = BuildSearchRegex(value);
			}
		}
		
		public string DescriptionsSearchPath 
		{ 
			get { return _descriptionsSearchPath; }
			set
			{
				_descriptionsSearchPath = value;
                _descriptionsSearchPathRegex = BuildSearchRegex(value);
			}
		}
		
		Regex BuildSearchRegex(string path) 
		{
            var replacePattern = "\\{[a-zA-Z]*\\}";
            var componentMatches = Regex.Match(path, replacePattern);
            var components = new List<string>();
            foreach (Group group in componentMatches.Groups)
                components.Add(group.Value);

            _components = components.ToArray();

            var pattern = Regex.Replace(path,replacePattern,"([\\w.]*)");
			return new Regex(pattern);
		}
		
		public IEnumerable<Suite> Suites { get; set; }
        public IEnumerable<Case> Cases { get; set; }
		
		public bool IsSystem(IFile file)
		{
			var path = file.FullPath;
			if( _systemsSearchPathRegex == null )
				return false;
			
			var isDescription = false;
			if( _descriptionsSearchPathRegex != null )
				isDescription = _descriptionsSearchPathRegex.IsMatch(path);
			
            return _systemsSearchPathRegex.IsMatch(path) && !isDescription;
		}
		
		public bool IsDescription(IFile file)
		{
			var path = file.FullPath;
			if( _descriptionsSearchPathRegex == null )
				return false;
			
			var isSystem = false;
			if( _systemsSearchPathRegex != null )
				isSystem = _systemsSearchPathRegex.IsMatch(path);
			
            return _descriptionsSearchPathRegex.IsMatch(path) && !isSystem; 
		}
    }
}
