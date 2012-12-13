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
            //args = new[] {  @"C:\PSS\Forseti-tfs\ForsetiTesting\FirstLevel\firstlevel.yaml",
            //                @"C:\PSS\Forseti-tfs\ForsetiTesting\forseti.trx",
            //                "COMPUTERNAME",
            //                "USERNAME",
            //                "TFS\\USERNAME"};

            var settings = Settings.FromArguments(args);
            
            var executor = Executor.WithForsetiConfigurationFile(settings.ForsetiConfigurationFile);
            var results = executor.ExecuteTests();

            var trxBuilder = new TrxBuilder(settings.ComputerName,settings.UserName,settings.TfsUsername).BuildFrom(results);
            var trx = trxBuilder.AsTrxDocument();

            trx.Save(settings.OutputFilePath);

            return results.IsSuccessful() ? 0 : 1;
        }

    }
}
