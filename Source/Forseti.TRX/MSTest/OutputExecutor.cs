using Forseti.ConsoleReporter;
using Forseti.Execution;
using Forseti.Harnesses;
using Forseti.Output.MSTest.Transformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forseti.Output.MSTest
{
    public class OutputExecutor : IOutputExecutor
    {

        public Executor Executor { get; private set; }
        public Settings Settings { get; private set; }

        public IOutputExecutor Create(Settings settings)
        {
            Settings = settings;
            Executor = Executor.WithForsetiConfigurationFile(Settings.ForsetiConfigurationFile, verbose: Settings.VerboseOutput);
        
            Executor.ReportWith<Reporter>();
            Executor.RegisterWatcher<ConsoleHarnessWatcher>();
            return this;
        }


        public void ReportOutput(IEnumerable<HarnessResult> results)
        {
            var trxBuilder = new TrxBuilder(Settings.ComputerName, Settings.UserName, Settings.TfsUsername).BuildFrom(results);
            var trx = trxBuilder.AsTrxDocument();

            trx.Save(Settings.OutputFilePath);
        }
    }
}
