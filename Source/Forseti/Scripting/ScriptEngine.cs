using System;
using System.IO;
using System.Text;
using Forseti.Pages;
using Forseti.Resources;
using Forseti.Files;
using Forseti.Frameworks;

namespace Forseti.Scripting
{
    public class ScriptEngine : IScriptEngine
    {
        IResourceManager _resourceManager;
        IScriptEngineContextManager _scriptEngineContextManager;
		IFileSystem _fileSystem;
		
        public ScriptEngine(IResourceManager resourceManager, 
		                    IScriptEngineContextManager scriptEngineContextManager, 
		                    IFileSystem fileSystem)
        {
            _resourceManager = resourceManager;
            _scriptEngineContextManager = scriptEngineContextManager;
			_fileSystem = fileSystem;
        }

        public void Execute(Page page)
        {
            var context = _scriptEngineContextManager.Create();
            var bootstrapper = _resourceManager.GetStringFromAssemblyOf<ScriptEngine>("Forseti.Scripting.Scripts.env.bootstrapper.js");
            
            var path = string.Format("{0}{1}", Path.GetTempPath(), @"Forseti/");
			path = _fileSystem.GetActualPath(path);

            var htmlFile = string.Format("{0}{1}", path, @"runner.html").Replace(@"\", @"/");
            context.EvaluateString("window.pagePath = \"file:///" + htmlFile + "\"", "");
            context.EvaluateString(bootstrapper, "bootstrapper.js");
            context.EvaluateString("executeSpecs()", "");
        }
    }
}
