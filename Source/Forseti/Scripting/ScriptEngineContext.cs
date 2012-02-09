using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Forseti.Resources;
using Forseti.Scripting.Extensions;
using java.lang;
using java.lang.reflect;
using org.mozilla.javascript;

namespace Forseti.Scripting
{
    public class ScriptEngineContext : IScriptEngineContext
    {
        Scriptable _scope;
        Context _context;

        public ScriptEngineContext(IResourceManager resourceManager)
        {
            var envJs = resourceManager.GetStringFromAssemblyOf<ScriptEngine>("Forseti.Scripting.Scripts.env.js");

            _context = Context.enter();
            _context.setOptimizationLevel(-1);
            _scope = _context.initStandardObjects();

            AddConsoleFunctionsToScope();

            SystemConsole.LoggingEnabled = false;
            _context.evaluateString(_scope, envJs, "env.js", 1, null);
            SystemConsole.LoggingEnabled = true;
        }

        void AddConsoleFunctionsToScope()
        {
            var consoleLoggingMethods = typeof (SystemConsole).GetMethods(BindingFlags.Public | BindingFlags.Static);
            Class myJClass = typeof (SystemConsole);
            foreach (var method in consoleLoggingMethods)
            {
                Member methodMember = myJClass.getMethod(method.Name, GetParamatersForMethod(method));
                var functionName = method.Name.ToCamelCase();
                Scriptable methodFunction = new FunctionObject(functionName, methodMember, _scope);
                _scope.put(functionName,_scope, methodFunction);

            }
        }

        Class[] GetParamatersForMethod(MethodInfo method)
        {
            var listOfParamTypes = new List<Class>();
            var parameters = method.GetParameters();

            foreach (var parameterInfo in parameters)
            {

                listOfParamTypes.Add(parameterInfo.ParameterType);  
                
            }
            return listOfParamTypes.ToArray();
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
