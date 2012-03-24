using Forseti.Configuration;
using System;

namespace Forseti.Frameworks
{
	public class FrameworkManager : IFrameworkManager
	{
		IConfigure _configure;
		
		public FrameworkManager(IConfigure configure)
		{
			_configure = configure;
		}
		
		
		public IFramework GetByName (string name)
		{
			// HACK
			if( name == "Jasmine" )
				return (IFramework)_configure.GetInstanceOf(Type.GetType ("Forseti.Jasmine.Framework"));
			if( name == "Buster" )
				return (IFramework)_configure.GetInstanceOf(Type.GetType ("Forseti.Buster.Framework"));
			if( name == "QUnit" )
				return (IFramework)_configure.GetInstanceOf(Type.GetType ("Forseti.QUnit.Framework"));

			return null;
		}
	}
}

