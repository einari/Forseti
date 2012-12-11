using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forseti.TRX.Transformation
{
    public class Settings
    {

        public string ForsetiConfigurationFile { get; private set; }
        public string OutputFilePath { get; private set; }


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
                if (args.Length >= 1 && !string.IsNullOrEmpty(args[0]))
                    settings.ForsetiConfigurationFile = args[0];

                if (args.Length >= 2 && !string.IsNullOrEmpty(args[1]))
                    settings.OutputFilePath = args[1];
            }
            return settings;
        }


    }
}
