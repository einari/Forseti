using Forseti.Resources;
using Forseti.Frameworks;

namespace Forseti.Buster
{
	public class Framework : IFramework
	{
		public Framework (IResourceManager resourceManager)
		{
            ScriptName = "buster-test.js";
            Script = resourceManager.GetStringFromAssemblyOf<Framework>("Forseti.Buster.Scripts.buster-test.js");
            ExecuteScriptName = "buster-executor.js";
            ExecuteScript = resourceManager.GetStringFromAssemblyOf<Framework>("Forseti.Buster.Scripts.buster-executor.js");
            ReportScriptName = "buster-reporter.js";
            ReportScript = resourceManager.GetStringFromAssemblyOf<Framework>("Forseti.Buster.Scripts.buster-reporter.js");
		}
		
		public string Name { get { return "Buster"; } }
		public string ScriptName { get; private set; }
		public string Script { get; private set; }
		public string ExecuteScriptName { get; private set; }
		public string ExecuteScript { get; private set; }
		public string ReportScriptName { get; private set; }
		public string ReportScript { get; private set; }
	}
}

