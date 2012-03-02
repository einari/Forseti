using System.Collections.Generic;
using Forseti.Files;

namespace Forseti.Scripting
{
    public interface IScriptDependencyManager
    {
        IEnumerable<File>	GetDependencies(File file);
    }
}
