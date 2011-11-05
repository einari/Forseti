using Forseti.Resources;

namespace Forseti
{
    public class ScriptEngine : IScriptEngine
    {
        IResourceManager _resourceManager;
        IScriptEngineContextManager _scriptEngineContextManager;
        IFramework _framework;

        public ScriptEngine(IResourceManager resourceManager, IScriptEngineContextManager scriptEngineContextManager, IFramework framework)
        {
            _resourceManager = resourceManager;
            _scriptEngineContextManager = scriptEngineContextManager;
            _framework = framework;
        }

        public void Execute(Harness harness)
        {
            var context = _scriptEngineContextManager.Create();
            context.EvaluateString(_framework.Script, "framework");
            foreach (var @case in harness.Cases)
            {
                context.EvaluateFile(@case.Description.Suite.SystemFile);
                context.EvaluateFile(@case.Description.File);
            }

            
            context.EvaluateString(_framework.ExecuteScript, "framework");
        }
    }
}
