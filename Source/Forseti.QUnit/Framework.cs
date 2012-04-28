using Forseti.Resources;
using Forseti.Frameworks;
using System.Collections.Generic;
using Forseti.Suites;
using Forseti.Files;

namespace Forseti.QUnit
{
    public class Framework : IFramework
    {
        public Framework(IResourceManager resourceManager)
        {
            ScriptName = "qunit.js";
            Script = resourceManager.GetStringFromAssemblyOf<Framework>("Forseti.QUnit.Scripts.qunit.js");
            ExecuteScriptName = "qunit-executor.js";
            ExecuteScript = resourceManager.GetStringFromAssemblyOf<Framework>("Forseti.QUnit.Scripts.qunit-executor.js");
            ReportScriptName = "qunit-reporter.js";
            ReportScript = resourceManager.GetStringFromAssemblyOf<Framework>("Forseti.QUnit.Scripts.qunit-reporter.js");
        }

		public string Name { get { return "QUnit"; } }
        public string ScriptName { get; private set; }
        public string Script { get; private set; }
        public string ExecuteScriptName { get; private set; }
        public string ExecuteScript { get; private set; }
        public string ReportScriptName { get; private set; }
        public string ReportScript { get; private set; }

		public IEnumerable<Case> DiscoverCasesFrom (File file)
		{
			throw new System.NotImplementedException ();
		}
	}
}
