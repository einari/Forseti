using Forseti.Resources;
using System;

namespace Forseti
{
    public class ScriptEngine : IScriptEngine
    {
        IResourceManager _resourceManager;
        IScriptEngineContextManager _scriptEngineContextManager;
        IFramework _framework;

        public ScriptEngine(IResourceManager resourceManager, IScriptEngineContextManager scriptEngineContextManager, IFramework framework)
        {
            Console.WriteLine("ScriptEngine()");
            _resourceManager = resourceManager;
            _scriptEngineContextManager = scriptEngineContextManager;
            _framework = framework;
        }

        public void Execute(Page page)
        {
            var context = _scriptEngineContextManager.Create();
            var bootstrapper = _resourceManager.GetStringFromAssemblyOf<ScriptEngine>("Forseti.JS.env.bootstrapper.js");
            context.EvaluateString(bootstrapper, "bootstrapper.js");
            context.EvaluateString("executeSpecs()", "");
        }
    }
}
