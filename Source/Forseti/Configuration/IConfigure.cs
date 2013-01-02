using System;
using Forseti.Harnesses;
using Forseti.Scripting;
using StructureMap;
using Forseti.Reporting;

namespace Forseti.Configuration
{
    public interface IConfigure
    {
        IContainer Container { get; }
        IHarnessManager HarnessManager { get; }
        IHarnessChangeManager HarnessChangeManager { get; }
        IScriptEngine ScriptEngine { get; }
        IReportingOptions ReportingOptions { get; }

        IConfigure Initialize();
        IConfigure WithReportingOptions(IReportingOptions options);

        T GetInstanceOf<T>();
		object GetInstanceOf(Type type);
    }
}
