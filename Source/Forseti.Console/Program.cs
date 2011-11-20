using System;
using Forseti.Configuration;
using Forseti.Suites;

namespace Forseti.Console
{
    public class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {
            var configuration = Configure.
                WithStandardKernel().
                UsingJasmin().
                Initialize();

            var suite = new Suite();
            suite.System = "systemUnderTest";
            suite.SystemFile = "systemUnderTest.js";

            var description = new SuiteDescription();
            description.File = "systemUnderTest.Specs.js";

            var @case = new Case();
            description.AddCase(@case);

            suite.AddDescription(description);

            var harness = configuration.HarnessManager.Execute(new[] {suite, suite});

            return 0;
        }
    }
}
