using StructureMap;

namespace Forseti.Scripting
{
    public class ScriptEngineContextManager : IScriptEngineContextManager
    {
        IContainer _container;

        public ScriptEngineContextManager(IContainer container)
        {
            _container = container;
        }

        public IScriptEngineContext Create()
        {
            
            var context = _container.GetInstance<ScriptEngineContext>();
            return context;
        }
    }
}
