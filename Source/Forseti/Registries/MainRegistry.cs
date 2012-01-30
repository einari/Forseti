using System;
using StructureMap.Configuration.DSL;
using Forseti.Scripting;
using Forseti.Harnesses;
using Forseti.Files;

namespace Forseti.Registries
{
    public class MainRegistry : Registry
    {
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
			
			if( Environment.OSVersion.Platform == PlatformID.Unix )
				For<IFileSystem>().Singleton().Use<Files.Unix.FileSystem>();
			else
				For<IFileSystem>().Singleton().Use<Files.Windows.FileSystem>();
			
			

            For<IScriptEngine>().Singleton().Use<ScriptEngine>();
            For<IHarnessManager>().Singleton().Use<HarnessManager>();
        }
    }
}
