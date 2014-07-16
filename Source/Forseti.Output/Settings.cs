using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forseti.Output    
{
    public class Settings
    {

        public string ForsetiConfigurationFile { get; private set; }
        public string OutputFilePath { get; private set; }
        public string ComputerName { get; private set; }
        public string UserName { get; private set; }
        public string TfsUsername { get; private set; }
        public bool VerboseOutput { get; private set; }
        


        Settings() 
        {
            ForsetiConfigurationFile = "forseti.yaml";
            OutputFilePath = "forseti-testresults.trx";
        }

        public static Settings FromArguments(string[] args) 
        {
            var settings = new Settings();

            if (args != null) 
            {
                if (args.Length > 0 && !string.IsNullOrEmpty(args[0]))
                    settings.ForsetiConfigurationFile = args[0];

                if (args.Length > 1 && !string.IsNullOrEmpty(args[1]))
                    settings.OutputFilePath = args[1];


                if (args.Length > 2 && !string.IsNullOrEmpty(args[2]))
                    settings.ComputerName = args[2];

                if (args.Length > 3 && !string.IsNullOrEmpty(args[3]))
                    settings.UserName = args[3];

                if (args.Length > 4 && !string.IsNullOrEmpty(args[4]))
                    settings.TfsUsername = args[4];

                if (args.Length > 5 && !string.IsNullOrEmpty(args[5]))
                    settings.VerboseOutput = Convert.ToBoolean(args[5]);
            }
            return settings;
        }


    }
}
