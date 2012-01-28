using Forseti.Harnesses;
using Forseti.Scripting;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.Syntax;

namespace Forseti.Configuration
{
    public class Configure : IConfigure
    {
        public IHarnessManager HarnessManager { get; private set; }
        public IFramework Framework { get; private set; }
        public IKernel Kernel { get; private set; }
        public IScriptEngine ScriptEngine { get; private set; }


        Configure(IKernel kernel)
        {
            Kernel = kernel;
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

            kernel.Bind(s =>
            {
                s.FromThisAssembly()
                    .SelectAllTypes()
                        .Excluding<IScriptEngine>()
                        .Excluding<ScriptEngine>()
                        .Excluding<IHarnessManager>()
                        .Excluding<HarnessManager>()
                    .BindToDefaultInterfaces();
            });

            kernel.Bind<IScriptEngine>().To<ScriptEngine>().InSingletonScope();
            kernel.Bind<IHarnessManager>().To<HarnessManager>().InSingletonScope();

            return kernel;
        }

        public IConfigure Initialize()
        {
            HarnessManager = Kernel.Get<IHarnessManager>();
            Framework = Kernel.Get<IFramework>();
            ScriptEngine = Kernel.Get<IScriptEngine>();

            return this;
        }


        public T GetInstanceOf<T>()
        {
            return Kernel.Get<T>();
        }
    }
}
