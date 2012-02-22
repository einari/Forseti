using Forseti.Harnesses;

namespace Forseti.Scripting
{
    public interface IScriptDependencyManager
    {
        void SetCurrentHarness(Harness harness);
        void RequireDependency(string sourcePath, string path);
    }
}
