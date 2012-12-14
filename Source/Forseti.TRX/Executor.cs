using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forseti.Harnesses;
using Forseti.Configuration;
using Forseti.Files;
using Forseti.ConsoleReporter;

namespace Forseti.TRX
{
    public class Executor
    {
        IConfigure _forsetiConfiguration;

        Executor(string configurationFilePath) 
        {
            _forsetiConfiguration = Configure.WithStandard()
                                             .FromConfigurationFile(configurationFilePath)
                                             .Initialize();
            //_forsetiConfiguration.HarnessChangeManager.RegisterWatcher(typeof(ConsoleHarnessWatcher));
        }

        public static Executor WithForsetiConfigurationFile(string configurationFilePath) 
        {
            var configurationDirectory = System.IO.Path.GetDirectoryName(configurationFilePath);
            if (System.IO.Directory.Exists(configurationDirectory))
            {
                System.IO.Directory.SetCurrentDirectory(configurationDirectory);
            }

            return new Executor(configurationFilePath);
        }

        public IEnumerable<HarnessResult> ExecuteTests()
        {
            return _forsetiConfiguration.HarnessManager.Run();
        }
    }
}
