using System.Collections.Generic;
using Forseti.Files;

namespace Forseti.Scripting
{
    public interface IDependencyManager
    {
        IEnumerable<IFile>	GetDependencies(IFile file);
    }
}
