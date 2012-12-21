using System;
using System.IO;
using System.Threading;
using Forseti.Configuration;

namespace Forseti.Console
{
    public class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {
            //System.IO.Directory.SetCurrentDirectory(Path.GetDirectoryName("c:/PSS/ForsetiTesting/Source/"));
            System.IO.Directory.SetCurrentDirectory(Path.GetDirectoryName("C:/PSS/Bifrost/Source/"));
           

			System.Console.WriteLine("Keys : \n  R : Rerun\n  B : Run in browser\n  Any other key : Exit\n\n");

            var configuration = Configure
                .WithStandard()
                .FromConfigurationFile("forseti.yaml")
                .Initialize();

            configuration
                    .HarnessChangeManager.RegisterWatcher(typeof(Windows.Growl.GrowlHarnessWatcher));

            configuration.HarnessChangeManager.RegisterWatcher(typeof(ConsoleReporter.ConsoleHarnessWatcher));

            configuration
                    .HarnessManager.Run();

                    

			
			for( ;; ) 
			{
				var key = System.Console.ReadKey();
				if( key.KeyChar != 0x0 ) 
				{
                    switch (key.Key)
                    {
                        case ConsoleKey.R:
						    configuration.HarnessManager.Run ();
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
