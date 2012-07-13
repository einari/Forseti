using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Forseti.Files;
using Forseti.Harnesses;
using Forseti.Scripting;
using Forseti.Extensions;
using StructureMap.Configuration.DSL;

namespace Forseti.Registries
{
    public class MainRegistry : Registry
    {
		Type[] _platformTypes;
		
        public MainRegistry()
        {
            Scan(
                x =>
                {
                    x.TheCallingAssembly();
                    x.ExcludeType<IScriptEngine>();
                    x.ExcludeType<ScriptEngine>();
                    x.ExcludeType<IHarnessManager>();
                    x.ExcludeType<HarnessManager>();
                    x.WithDefaultConventions();
                }
            );
			
			
			/*
			if( Environment.OSVersion.Platform == PlatformID.MacOSX )
				For<IFileSystem>().Singleton().Use<Files.Unix.FileSystem>();
			else
				For<IFileSystem>().Singleton().Use<Files.Windows.FileSystem>();
			
			*/

            For<IScriptEngine>().Singleton().Use<ScriptEngine>();
            For<IHarnessManager>().Singleton().Use<HarnessManager>();
			
			InitializePlatformTypes();
			
			var fileSystemType = FindImplementorOf(typeof(IFileSystem));
			For(typeof(IFileSystem)).Singleton().Use(fileSystemType);
			For(typeof(IFileSystemWatcher)).Singleton().Use(FindImplementorOf(typeof(IFileSystemWatcher)));
            For(typeof(IHarnessChangeManager)).Singleton().Use(typeof(HarnessChangeManager));
        }
		
		void InitializePlatformTypes()
		{
			var assembly = GetPlatformAssembly(Environment.OSVersion.Platform);
			_platformTypes = assembly.GetTypes();
		}
		
		
		Assembly GetPlatformAssembly(PlatformID platformId)
		{
			var assemblyName = string.Empty;
			var currentAssembly = Assembly.GetExecutingAssembly();
			
			var uri = new Uri(currentAssembly.CodeBase);
			var directory = Path.GetDirectoryName(uri.AbsolutePath);
			
			
			switch( platformId )
			{
			case PlatformID.Unix: 
				assemblyName = "Forseti.OSX";
				break;
			case PlatformID.Win32NT:
				assemblyName = "Forseti.Windows";
				break;
			}
			assemblyName = string.Format ("{0}/{1}.dll",directory,assemblyName);
			
			if( System.IO.File.Exists(assemblyName) ) 
				return Assembly.LoadFile (assemblyName);
			
			return typeof(MainRegistry).Assembly;
		}
		
		
		
		Type FindImplementorOf(Type type)
		{
			var query = from t in _platformTypes
						where t.HasInterface(type) && !t.IsInterface && !t.IsAbstract
						select t;
			return query.SingleOrDefault();
		}
		
		
    }
}
