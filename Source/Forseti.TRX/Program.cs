using System;
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
            //args = new[] {  @"C:\PSS\Forseti-tfs\ForsetiTesting\forseti.yaml",
            //                @"C:\PSS\Forseti-tfs\ForsetiTesting\forseti.trx",
            //                "COMPUTERNAME",
            //                "USERNAME",
            //                "TFS\\USERNAME",
            //                "true"};

            var settings = Settings.FromArguments(args);
            
            var executor = Executor.WithForsetiConfigurationFile(settings.ForsetiConfigurationFile, verbose: settings.VerboseOutput);
            var results = executor.ExecuteTests();

            var trxBuilder = new TrxBuilder(settings.ComputerName,settings.UserName,settings.TfsUsername).BuildFrom(results);
            var trx = trxBuilder.AsTrxDocument();

            trx.Save(settings.OutputFilePath);

            return results.IsSuccessful() ? 0 : 1;
        }

    }
}
