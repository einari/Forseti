using Ninject;

namespace Forseti.Scripting
{
    public class ScriptEngineContextManager : IScriptEngineContextManager
    {
        IKernel _kernel;

        public ScriptEngineContextManager(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IScriptEngineContext Create()
        {
            var context = _kernel.Get<ScriptEngineContext>();
            return context;
        }
    }
}
