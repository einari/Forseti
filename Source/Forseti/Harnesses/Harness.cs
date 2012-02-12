using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Forseti.Suites;

namespace Forseti
{
    public class Harness
    {
		string _systemsSearchPath;
		string _descriptionsSearchPath;
		Regex _systemsSearchPathRegex;
		Regex _descriptionsSearchPathRegex;
		
		public string Name { get; set; }
		public string SystemsSearchPath 
		{ 
			get { return _systemsSearchPath; }
			set
			{
				_systemsSearchPath = value;
			}
		}
		
		public string DescriptionsSearchPath 
		{ 
			get { return _descriptionsSearchPath; }
			set
			{
				_descriptionsSearchPath = value;
			}
		}
		
		
		Regex BuildSearchRegex() 
		{
			var pattern = string.Empty;
			
			return new Regex(pattern);
		}
		
		
		public IEnumerable<Suite> Suites { get; set; }
        public IEnumerable<Case> Cases { get; set; }
		
		public bool IsSystemFile(string relativePath)
		{
			throw new NotImplementedException();
		}
		
		public bool IsDescriptionsFile(string relativePath)
		{
			throw new NotImplementedException();
		}
		
		
    }
}
