using System.Collections.Generic;

namespace Forseti.Suites
{
    public class Case
    {
		public Case()
		{
			Result = new CaseResult();
		}
		
        List<Case> _children = new List<Case>();

        public Description Description { get; set; }
        public Case Parent { get; set; }
        public string Name { get; set; }
		
		public CaseResult Result { get; set; }

        public IEnumerable<Case> Children { get { return _children; } }

        public void AddChildCase(Case @case)
        {
            @case.Parent = this;
            @case.Description = this.Description;
            _children.Add(@case);
        }
    }
}
