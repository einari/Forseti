using Forseti.Harnesses;
using Ninject;
using Ninject.Extensions.Conventions;

namespace Forseti.Configuration
{
    public class Configure : IConfigure
    {
        public IHarnessManager HarnessManager { get; private set; }
        public IFramework Framework { get; private set; }
        public IKernel Kernel { get; private set; }
        public IScriptEngine ScriptEngine { get; private set; }
        public PathConfiguration SourcePaths { get; private set; }
        public PathConfiguration SpecificationPaths { get; private set; }


        Configure(IKernel kernel)
        {
            Kernel = kernel;
            SourcePaths = new PathConfiguration();
            SpecificationPaths = new PathConfiguration();
        }

        public static IConfigure WithStandardKernel()
        {
            var kernel = GetKernel();
            return With(kernel);
        }

        public static IConfigure With(IKernel kernel)
        {
            var instance = new Configure(kernel);
            return instance;
        }


        static IKernel GetKernel()
        {
            var kernel = new StandardKernel();

            var scanner = new AssemblyScanner();
            scanner.FromCallingAssembly();
            scanner.BindWithDefaultConventions();
            kernel.Scan(scanner);

            return kernel;
        }

        public IConfigure Initialize()
        {
            HarnessManager = Kernel.Get<IHarnessManager>();
            Framework = Kernel.Get<IFramework>();
            ScriptEngine = Kernel.Get<IScriptEngine>();

            return this;
        }
    }
}
