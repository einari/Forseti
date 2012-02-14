using Forseti.Harnesses;

namespace Forseti.Scripting
{
    public interface IScriptDependencyManager
    {
        void SetCurrentHarness(Harness harness);
        void Require(string sourcePath, string path);
    }
}
