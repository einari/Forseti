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
				return (IFramework)_configure.GetInstanceOf(Type.GetType ("Forseti.Jasmine.Framework, Forseti.Jasmine"));
			if( name == "Buster" )
				return (IFramework)_configure.GetInstanceOf(Type.GetType ("Forseti.Buster.Framework, Forseti.Buster"));
			if( name == "QUnit" )
				return (IFramework)_configure.GetInstanceOf(Type.GetType ("Forseti.QUnit.Framework, Forseti.QUnit"));

			return null;
		}
	}
}

