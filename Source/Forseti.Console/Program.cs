using System;
using Forseti.Configuration;

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

            configuration.ScriptEngine.Execute(null);

            return 0;
        }
    }
}
