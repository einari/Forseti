using Forseti.Harnesses;
using Ninject;
using Forseti.Scripting;

namespace Forseti.Configuration
{
    public interface IConfigure
    {
        IKernel Kernel { get; }
        IHarnessManager HarnessManager { get; }
        IFramework Framework { get; }
        IScriptEngine ScriptEngine { get; }

        IConfigure Initialize();

        T GetInstanceOf<T>();
    }
}
