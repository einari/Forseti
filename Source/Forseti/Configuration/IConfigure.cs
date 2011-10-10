using Ninject;

namespace Forseti.Configuration
{
    public interface IConfigure
    {
        IKernel Kernel { get; }
        IExecutor Executor { get; }
        IFramework Framework { get; }
        PathConfiguration SourcePaths { get; }
        PathConfiguration SpecificationPaths { get; }

        void Initialize();
    }
}
