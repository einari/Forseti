using Forseti.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forseti.Output.AppVeyor
{
    public class OutputExecutor : IOutputExecutor
    {
        public Executor Executor { get; private set; }

        public IOutputExecutor Create(Settings settings)
        {
            Executor = Executor.WithForsetiConfigurationFile(settings.ForsetiConfigurationFile, verbose: settings.VerboseOutput);
            Executor.ReportWith<Forseti.AppVeyor.Reporter>();
            Executor.RegisterWatcher<Forseti.ConsoleReporter.ConsoleHarnessWatcher>();
            return this;
        }

        public void ReportOutput(IEnumerable<Harnesses.HarnessResult> results)
        {
            //Reporting done through the AppVeyor reporter
        }
    }
}
