using StructureMap.Configuration.DSL;
using Forseti.Scripting;
using Forseti.Harnesses;

namespace Forseti.Registries
{
    public class MainRegistry : Registry
    {
        public MainRegistry()
        {
            Scan(
                x =>
                {
                    x.TheCallingAssembly();
                    x.ExcludeType<IScriptEngine>();
                    x.ExcludeType<ScriptEngine>();
                    x.ExcludeType<IHarnessManager>();
                    x.ExcludeType<HarnessManager>();
                    x.WithDefaultConventions();
                }
            );


            For<IScriptEngine>().Singleton().Use<ScriptEngine>();
            For<IHarnessManager>().Singleton().Use<HarnessManager>();
        }
    }
}
