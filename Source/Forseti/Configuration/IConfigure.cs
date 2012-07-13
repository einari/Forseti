using System;
using Forseti.Harnesses;
using Forseti.Scripting;
using StructureMap;

namespace Forseti.Configuration
{
    public interface IConfigure
    {
        IContainer Container { get; }
        IHarnessManager HarnessManager { get; }
        IHarnessChangeManager HarnessChangeManager { get; }
        IScriptEngine ScriptEngine { get; }

        IConfigure Initialize();

        T GetInstanceOf<T>();
		object GetInstanceOf(Type type);
    }
}
