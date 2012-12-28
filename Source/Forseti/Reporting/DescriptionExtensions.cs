using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forseti.Suites;

namespace Forseti.Reporting
{
    public static class DescriptionExtensions
    {
        public static bool HasExecutedCases(this Description d) 
        {
            return !(d.Cases.Count() == 0 || d.Cases.Count() == 1 && Case.IsDummyOrEmptyCase(d.Cases.First()));
        }
    }
}
