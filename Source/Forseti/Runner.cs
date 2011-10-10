using System;
using System.Collections.Generic;

namespace Forseti
{
    public class Runner : IRunner
    {
        public void Run(IEnumerable<Files.File> files)
        {
            throw new NotImplementedException();
        }

        public void Run(string systemUnderTest, string test)
        {
            throw new NotImplementedException();
        }
    }
}
