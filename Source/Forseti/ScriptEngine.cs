using Forseti.Resources;

namespace Forseti
{
    public class ScriptEngine : IScriptEngine
    {
        IResourceManager _resourceManager;
        IScriptEngineContextManager _scriptEngineContextManager;

        public ScriptEngine(IResourceManager resourceManager, IScriptEngineContextManager scriptEngineContextManager)
        {
            _resourceManager = resourceManager;
            _scriptEngineContextManager = scriptEngineContextManager;
        }

        public void Execute(Harness harness)
        {
            var context = _scriptEngineContextManager.Create();
            foreach (var @case in harness.Cases)
            {
                context.EvaluateFile(@case.Description.Suite.SystemFile);
                context.EvaluateFile(@case.Description.File);
            }
        }
    }
}
