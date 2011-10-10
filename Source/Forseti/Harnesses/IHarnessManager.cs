using System.Collections.Generic;

namespace Forseti.Harnesses
{
    public interface IHarnessManager
    {
        Harness Execute(IEnumerable<Suite> suites);
        Harness Execute(string system, string @case);
    }
}
