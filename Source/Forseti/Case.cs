using System.Collections.Generic;

namespace Forseti
{
    public class Case
    {
        List<Case> _children = new List<Case>();

        public Case Parent { get; set; }
        public string Name { get; set; }

        public IEnumerable<Case> Children { get { return _children; } }

        public void AddChildCase(Case @case)
        {
            _children.Add(@case);
            @case.Parent = this;
        }
    }
}
