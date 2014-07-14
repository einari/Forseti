using Forseti.ConsoleReporter;
using Forseti.Execution;
using Forseti.Reporting;
using Forseti.Output.MSTest.Transformation;
using System;

namespace Forseti.Output
{
    class Program
    {
        static int Main(string[] args)
        {
            //args = new[] {  @"C:\PSS\Forseti-tfs\ForsetiTesting\forseti.yaml",
            //                @"C:\PSS\Forseti-tfs\ForsetiTesting\forseti-testresults-mstest.trx",
            //                "COMPUTERNAME",
            //                "USERNAME",
            //                "TFS\\USERNAME",
            //                "true"};


            var outputExecutor = CreateExecutor(args);

            var results = outputExecutor.Executor.ExecuteTests();

            outputExecutor.ReportOutput(results);

            return results.IsSuccessful() ? 0 : 1;
        }

        private static IOutputExecutor CreateExecutor(string[] args)
        {
            var executorType = GetExecutorTypeFromArguments(args);
            var settings = Settings.FromArguments(args);
            var supportsAppveyor = SupportsAppveyor();

            if (supportsAppveyor)
            {
                return new Forseti.Output.AppVeyor.OutputExecutor().Create(settings);
            }
            else
            {
               return new Forseti.Output.MSTest.OutputExecutor().Create(settings);
            }
        }

        private static bool SupportsAppveyor()
        {
            return Environment.GetEnvironmentVariable("APPVEYOR_API_URL") != null;
        }

        private static string GetExecutorTypeFromArguments(string[] args)
        {

            //parse the thing
            return "mstest";
        }

    }

}
