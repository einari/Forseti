using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public ScriptEngineContext(IResourceManager resourceManager, IScriptDependencyManager scriptDependencies)
        {
            var envJs = resourceManager.GetStringFromAssemblyOf<ScriptEngine>("Forseti.Scripting.Scripts.env.js");

            _context = Context.enter();
            _context.setOptimizationLevel(-1);
            _scope = _context.initStandardObjects();

            AddClassAndMethods(typeof(SystemConsole));
            AddClassAndMethods(typeof(ScriptDependencies),"Require");

            SystemConsole.LoggingEnabled = false;
            _context.evaluateString(_scope, envJs, "env.js", 1, null);
            SystemConsole.LoggingEnabled = true;
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

        void AddClassAndMethods(System.Type type, params string[] methodNames)
        {
            MethodInfo[] methods;
            var query = type.GetMethods(BindingFlags.Public | BindingFlags.Static).Select(m => m);
            if( methodNames.Length > 0 ) 
                query = query.Where(m => methodNames.Contains(m.Name));

            methods = query.ToArray();

            Class javaClass = type;
            foreach (var method in methods)
            {
                Member methodMember = javaClass.getMethod(method.Name, GetParametersForMethod(method));
                var functionName = method.Name.ToCamelCase();
                Scriptable methodFunction = new FunctionObject(functionName, methodMember, _scope);
                _scope.put(functionName, _scope, methodFunction);
            }
        }

        void AddConsoleFunctionsToScope()
        {
            var consoleLoggingMethods = typeof(SystemConsole).GetMethods(BindingFlags.Public | BindingFlags.Static);
            Class myJClass = typeof(SystemConsole);
            foreach (var method in consoleLoggingMethods)
            {
                Member methodMember = myJClass.getMethod(method.Name, GetParametersForMethod(method));
                var functionName = method.Name.ToCamelCase();
                Scriptable methodFunction = new FunctionObject(functionName, methodMember, _scope);
                _scope.put(functionName, _scope, methodFunction);
            }
        }

        Class[] GetParametersForMethod(MethodInfo method)
        {
            var listOfParamTypes = new List<Class>();
            var parameters = method.GetParameters();

            foreach (var parameterInfo in parameters)
                listOfParamTypes.Add(parameterInfo.ParameterType);

            return listOfParamTypes.ToArray();
        }

    }
}
