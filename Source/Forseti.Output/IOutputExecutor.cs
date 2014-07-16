using Forseti.Execution;
using Forseti.Harnesses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forseti.Output
{
    public interface IOutputExecutor
    {
        Executor Executor { get; }
        IOutputExecutor Create(Settings settings);
        void ReportOutput(IEnumerable<HarnessResult> results);
    }

}
