using System.Collections.Generic;
using Forseti.Suites;

namespace Forseti.Harnesses
{
    public interface IHarnessManager
    {
		void Add(Harness harness);
		void Reset();
        IEnumerable<HarnessResult> Run();

        IEnumerable<Harness> Harnesses { get; }
		
        HarnessResult Execute(Harness harness, IEnumerable<Suite> suites);
    }
}
