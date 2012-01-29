using Forseti.Harnesses;
using Forseti.Scripting;
using StructureMap;

namespace Forseti.Configuration
{
    public interface IConfigure
    {
        IContainer Container { get; }
        IHarnessManager HarnessManager { get; }
        IFramework Framework { get; }
        IScriptEngine ScriptEngine { get; }

        IConfigure Initialize();

        T GetInstanceOf<T>();
    }
}
