using System.Collections.Generic;
using Forseti.Suites;

namespace Forseti.Harnesses
{
    public interface IHarnessManager
    {
		void Add(Harness harness);
		void Reset();
        void Run();

        IEnumerable<Harness> Harnesses { get; }
		
        void Execute(Harness harness, IEnumerable<Suite> suites);
    }
}
