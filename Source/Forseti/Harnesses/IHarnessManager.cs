using System.Collections.Generic;
using Forseti.Suites;

namespace Forseti.Harnesses
{
    public interface IHarnessManager
    {
        Harness Execute(IEnumerable<Suite> suites);
    }
}
