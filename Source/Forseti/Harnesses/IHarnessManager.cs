using System.Collections.Generic;
using Forseti.Suites;

namespace Forseti.Harnesses
{
    public interface IHarnessManager
    {
		void Add(Harness harness);
		void Reset();
		
        Harness Execute(IEnumerable<Suite> suites);
    }
}
