using System.Collections.Generic;
using Forseti.Files;

namespace Forseti
{
    public interface IExecutor
    {
        Execution Execute(IEnumerable<File> files);
        Execution Execute(string systemUnderTest, string test);
    }
}
