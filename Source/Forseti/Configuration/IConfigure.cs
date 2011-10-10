using Forseti.Harnesses;
using Ninject;

namespace Forseti.Configuration
{
    public interface IConfigure
    {
        IKernel Kernel { get; }
        IHarnessManager HarnessManager { get; }
        IFramework Framework { get; }
        IScriptEngine ScriptEngine { get; }
        PathConfiguration SourcePaths { get; }
        PathConfiguration SpecificationPaths { get; }

        IConfigure Initialize();
    }
}
