using System;
using Forseti.Suites;

namespace Forseti.Reporting
{
    public static class CaseExtensions
    {
        public static bool CanBeReportedOn(this Case @case)
        {
            return !Case.IsDummyOrEmptyCase(@case);
        }

    }
}
