using System;
using Forseti.Pages;
using Forseti.Resources;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Forseti.Scripting
{
    public class ScriptEngine : IScriptEngine
    {
        IResourceManager _resourceManager;
        IScriptEngineContextManager _scriptEngineContextManager;
        IFramework _framework;

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetShortPathName(
            [MarshalAs(UnmanagedType.LPTStr)]string path,
            [MarshalAs(UnmanagedType.LPTStr)]StringBuilder shortPath,
            int shortPathLength);


        public ScriptEngine(IResourceManager resourceManager, IScriptEngineContextManager scriptEngineContextManager, IFramework framework)
        {
            _resourceManager = resourceManager;
            _scriptEngineContextManager = scriptEngineContextManager;
            _framework = framework;
        }

        public void Execute(Page page)
        {
            var context = _scriptEngineContextManager.Create();
            var bootstrapper = _resourceManager.GetStringFromAssemblyOf<ScriptEngine>("Forseti.Scripting.Scripts.env.bootstrapper.js");
            
            var path = string.Format("{0}{1}", Path.GetTempPath(), @"Forseti\");
            var shortPath = new StringBuilder(255);
            GetShortPathName(path, shortPath, shortPath.Capacity);

            var htmlFile = string.Format("{0}{1}", shortPath, @"jasmine-runner.html").Replace(@"\", @"/");
            context.EvaluateString("window.pagePath = \"file:///" + htmlFile + "\"", "");
            context.EvaluateString(bootstrapper, "bootstrapper.js");
            context.EvaluateString("executeSpecs()", "");
        }
    }
}
