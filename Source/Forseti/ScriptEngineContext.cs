using System.IO;
using Forseti.Resources;
using java.lang;
using java.lang.reflect;
using org.mozilla.javascript;

namespace Forseti
{
    public class ScriptEngineContext : IScriptEngineContext
    {
        Scriptable _scope;
        Context _context;

        public ScriptEngineContext(IResourceManager resourceManager)
        {
            string envJs = resourceManager.GetStringFromAssemblyOf<ScriptEngine>("Forseti.JS.env.js");

            _context = Context.enter();
            _context.setOptimizationLevel(-1);
            _scope = _context.initStandardObjects();

            Class myJClass = typeof(SystemConsole);
            Member method = myJClass.getMethod("Print", typeof(string));
            Scriptable function = new FunctionObject("print", method, _scope);
            _scope.put("print", _scope, function);
            _context.evaluateString(_scope, envJs, "env.js", 1, null);
        }

        public void EvaluateString(string script, string source)
        {
            _context.evaluateString(_scope, script, source, 1, null);
        }

        public void EvaluateFile(string file)
        {
            var script = File.ReadAllText(file);
            _context.evaluateString(_scope, script, file, 1, null);
        }
    }
}
