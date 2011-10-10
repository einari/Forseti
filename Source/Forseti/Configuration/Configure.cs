using Ninject;

namespace Forseti.Configuration
{
    public class Configure : IConfigure
    {
        static readonly object InstanceLock = new object();
        public static Configure Instance { get; private set; }
        public IRunner Runner { get; private set; }
        public IFramework Framework { get; private set; }
        public IKernel Kernel { get; private set; }


        Configure(IKernel kernel, IFramework framework)
        {
            Kernel = kernel;
            Framework = framework;
        }

        public static IConfigure With<T>(IKernel kernel) where T : IFramework
        {
            if (Instance == null)
            {
                lock (InstanceLock)
                {
                    var framework = kernel.Get<T>();
                    Instance = new Configure(kernel, framework);
                }
            }


            return Instance;
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
