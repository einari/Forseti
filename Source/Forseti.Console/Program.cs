using System;
using System.Collections.Generic;
using System.IO;
using Forseti.Configuration;
using Forseti.Suites;
using Newtonsoft.Json;

namespace Forseti.Console
{
    public class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {
            /*
            var configFile = "forseti.config";
            if (!File.Exists(configFile))
            {
                System.Console.WriteLine("You need a config file!");
                return -1;
            }

            var json = File.ReadAllText(configFile);
            var suites = JsonConvert.DeserializeObject<IEnumerable<Suite>>(json);

            foreach (var suite in suites)
            {
                foreach (var description in suite.Descriptions)
                {
                    description.Suite = suite;

                    foreach( var @case in description.Cases )
                    {
                        @case.Description = description;
                    }                   
                }
            }*/
			
            var configuration = Configure
                .WithStandard()
                .FromConfigurationFile("forseti.yaml")
                .Initialize();

            configuration
                    .HarnessManager.Run();

            System.Console.ReadLine();


            //var harness = configuration.HarnessManager.Execute(suites);

            

            return 0;
        }
    }
}
