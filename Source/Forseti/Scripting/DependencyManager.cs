using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Forseti.Files;

namespace Forseti.Scripting
{
    public class DependencyManager : IDependencyManager
    {
		public IEnumerable<IFile> GetDependencies (IFile file)
		{
			throw new NotImplementedException ();
		}
    }
}
