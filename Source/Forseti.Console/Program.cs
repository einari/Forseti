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
            if (args.Length != 1)
            {
                System.Console.WriteLine("\nUsage : forseti <config file>");
                return -1;
            }


            var json = File.ReadAllText("forseti.config");
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
            }

            var configuration = Configure.
                WithStandardKernel().
                UsingJasmin().
                Initialize();

            var harness = configuration.HarnessManager.Execute(suites);

            System.Console.ReadLine();


            return 0;
        }
    }
}
