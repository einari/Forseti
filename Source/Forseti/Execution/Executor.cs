using System.Collections.Generic;
using Forseti.Harnesses;
using Forseti.Configuration;
using Forseti.Reporting;

namespace Forseti.Execution
{
    public class Executor
    {
        IConfigure _forsetiConfiguration;

        Executor(string configurationFilePath, bool verbose) 
        {
            _forsetiConfiguration = Configure.WithStandard()
                                             .WithReportingOptions(new ReportingOptions(onlyOutputFailed: !verbose))
                                             .FromConfigurationFile(configurationFilePath)
                                             .Initialize();
        }

        public void RegisterWatcher<T>() where T : IHarnessWatcher
        {
            _forsetiConfiguration.HarnessChangeManager.RegisterWatcher(typeof(T));
        }

        public void ReportWith<T>() where T : IReporter
        {
            _forsetiConfiguration.Container.Configure(c => c.For<IReporter>().Use<T>());
        }

        public static Executor WithForsetiConfigurationFile(string configurationFilePath, bool verbose = false) 
        {
            var configurationDirectory = System.IO.Path.GetDirectoryName(configurationFilePath);
            if (System.IO.Directory.Exists(configurationDirectory))
            {
                System.IO.Directory.SetCurrentDirectory(configurationDirectory);
            }

            return new Executor(configurationFilePath, verbose);
        }

        public IEnumerable<HarnessResult> ExecuteTests()
        {
            return _forsetiConfiguration.HarnessManager.Run();
        }

        public IReportingOptions GetReportingOptions()
        {
            return _forsetiConfiguration != null ? _forsetiConfiguration.ReportingOptions : null;
        }
    }
}
