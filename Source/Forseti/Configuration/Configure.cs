using Ninject;
using Ninject.Extensions.Conventions;

namespace Forseti.Configuration
{
    public class Configure : IConfigure
    {
        static readonly object InstanceLock = new object();
        public static Configure Instance { get; private set; }
        public IExecutor Executor { get; private set; }
        public IFramework Framework { get; private set; }
        public IKernel Kernel { get; private set; }
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
            if (Instance == null)
            {
                lock (InstanceLock)
                {
                    Instance = new Configure(kernel);
                }
            }


            return Instance;
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

        public void Initialize()
        {
            Executor = Kernel.Get<IExecutor>();
            Framework = Kernel.Get<IFramework>();
        }


        /// <summary>
        /// Reset configuration
        /// </summary>
        public static void Reset()
        {
            lock (InstanceLock)
            {
                Instance = null;
            }
        }

    }
}
