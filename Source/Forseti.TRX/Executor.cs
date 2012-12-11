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
           _forsetiConfiguration.HarnessChangeManager.RegisterWatcher(typeof(ConsoleHarnessWatcher));
        }

        public static Executor WithForsetiConfigurationFile(File configurationFilePath) 
        {
            var configurationDirectory = configurationFilePath.Folder;
            if (System.IO.Directory.Exists(configurationDirectory))
            {
                //Console.WriteLine("SettingCurrentDirectory to " + configurationDirectory);
                System.IO.Directory.SetCurrentDirectory(configurationDirectory);
            }

            return new Executor(configurationFilePath.Filename);
        }

        public IEnumerable<HarnessResult> ExecuteTests()
        {
            return _forsetiConfiguration.HarnessManager.Run();
        }
    }
}
