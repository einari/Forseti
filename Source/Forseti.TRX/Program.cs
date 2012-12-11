﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forseti.TRX.Transformation;

namespace Forseti.TRX
{
    class Program
    {
        static int Main(string[] args)
        {
            //args = new[] { @"C:\PSS\Forseti-tfs\ForsetiTesting\forseti.yaml", @"C:\PSS\Forseti-tfs\ForsetiTesting\forseti.trx" };

            var settings = Settings.FromArguments(args);
            
            var executor = Executor.WithForsetiConfigurationFile(settings.ForsetiConfigurationFile);
            var results = executor.ExecuteTests();

            var trxBuilder = new TrxBuilder().BuildFrom(results);
            var trx = trxBuilder.AsTrxDocument();

            trx.Save(settings.OutputFilePath);

            return 0;
        }

    }
}
