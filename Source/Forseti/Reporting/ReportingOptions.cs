using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forseti.Reporting
{
    public class ReportingOptions : IReportingOptions
    {
        public bool OnlyOutputFailed { get; private set; }

        public ReportingOptions(bool onlyOutputFailed = true)
        {
            OnlyOutputFailed = onlyOutputFailed;
        }
    }
}
