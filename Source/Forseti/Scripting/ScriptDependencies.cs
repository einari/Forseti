using StructureMap;
using Microsoft.Practices.ServiceLocation;

namespace Forseti.Scripting
{
    public class ScriptDependencies
    {
        public static void Require(string sourcePath, string scriptPath)
        {
            var scriptDependencyManager = ServiceLocator.Current.GetInstance<IScriptDependencyManager>();
            scriptDependencyManager.Require(sourcePath, scriptPath);
        }
    }
}
