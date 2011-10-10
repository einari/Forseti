using System;
using Forseti.Configuration;
using Ninject;

namespace Forseti.Console
{
    public class Program
    {
        [STAThread]
        public static int Main(string[] args)
        {
            Configure.
                WithStandardKernel().
                UsingJasmin().
                Initialize();


            var scriptEngine = Configure.Instance.Kernel.Get<IScriptEngine>();
            scriptEngine.Execute(null, null);

            return 0;
        }
    }
}
