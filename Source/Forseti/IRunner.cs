using System.Collections.Generic;
using Forseti.Files;

namespace Forseti
{
    public interface IRunner
    {
        void Run(IEnumerable<File> files);
        void Run(string systemUnderTest, string test);
    }
}
