using System.Collections.Generic;
using Forseti.Files;
using Forseti.Suites;

namespace Forseti.Frameworks
{
    public interface IFramework
    {
		string Name { get; }
        string ScriptName { get; }
        string Script { get; }
        string ExecuteScriptName { get; }
        string ExecuteScript { get; }
        string ReportScriptName { get; }
        string ReportScript { get; }
		
		IEnumerable<Case>	DiscoverCasesFrom(File file);
    }
}
