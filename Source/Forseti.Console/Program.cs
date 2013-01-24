using System;
using System.IO;
using System.Threading;
using Forseti.ConsoleReporter;
using Forseti.Execution;
using Forseti.Reporting;

namespace Forseti.Console
{
    public class Program
    {

        static Executor _executor;
        static IReporter _reporter;

        [STAThread]
        public static int Main(string[] args)
        {
         

			System.Console.WriteLine("Keys : \n  R : Rerun\n  B : Run in browser\n  Any other key : Exit\n\n");

            var currentConfigurationFile = Path.Combine(Directory.GetCurrentDirectory(), "forseti.yaml");
            
            _executor = Executor.WithForsetiConfigurationFile(currentConfigurationFile);
            _executor.ReportWith<Reporter>();
            _executor.RegisterWatcher<Windows.Growl.GrowlHarnessWatcher>();
            _executor.RegisterWatcher<ConsoleHarnessWatcher>();

            _reporter = new Reporter(_executor.GetReportingOptions());
            
            _reporter.ReportSummary(_executor.ExecuteTests());

			for( ;; ) 
			{
				var key = System.Console.ReadKey();
				if( key.KeyChar != 0x0 ) 
				{
                    switch (key.Key)
                    {
                        case ConsoleKey.R:
						    
                            _reporter.ReportOn(_executor.ExecuteTests());
                            break;
                        case ConsoleKey.B:
                            var target = Path.GetTempPath() + @"Forseti/runner.html";
                            System.Diagnostics.Process.Start(target);
                            break;
                        default:
						    System.Diagnostics.Process.GetCurrentProcess().Kill ();
						    break;
                    }
				}
				
				Thread.Sleep(20);
			}
			

            return 0;
        }
    }
}
