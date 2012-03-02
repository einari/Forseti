using System.Collections.Generic;

namespace Forseti.Scripting
{
	public interface IDependencyParser
	{
		IEnumerable<string> FindDependencies(string content);
	}
}

