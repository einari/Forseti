using System.Collections.Generic;
using Forseti.Suites;

namespace Forseti
{
    public class Harness
    {
		public string Name { get; set; }
		public string SystemsSearchPath { get; set; }
		public string DescriptionsSearchPath { get; set; }
		
		public IEnumerable<Suite> Suites { get; set; }
        public IEnumerable<Case> Cases { get; set; }
		
		
		
    }
}
