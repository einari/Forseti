using System;
using System.Collections.Generic;
using System.IO;
using Forseti.Configuration;
using Forseti.Suites;
using Newtonsoft.Json;
using System.Threading;

namespace Forseti.Console
{
    public class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {
			System.Console.WriteLine("Keys : \n  R : Rerun\n  Esc : Exit\n\n");
			
			
            var configuration = Configure
                .WithStandard()
                .FromConfigurationFile("forseti.yaml")
                .Initialize();

            configuration
                    .HarnessManager.Run();
			
			
			for( ;; ) 
			{
				var key = System.Console.ReadKey();
				if( key.Key == ConsoleKey.R )
					configuration.HarnessManager.Run ();
				
				if( key.Key == ConsoleKey.Escape )
					break;
				
				Thread.Sleep(20);
			}
			

            return 0;
        }
    }
}
