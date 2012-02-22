using System;
using Forseti.Harnesses;

namespace Forseti.Scripting
{
    public class ScriptDependencyManager : IScriptDependencyManager
    {
        public void SetCurrentHarness(Harness harness)
        {
            
        }

        public void RequireDependency(string sourcePath, string path)
        {
            Console.WriteLine("Dependency : " + sourcePath);
        }
    }
}
