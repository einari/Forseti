using Forseti.Resources;

namespace Forseti.Jasmine
{
    public class Framework : IFramework
    {
        public Framework(IResourceManager resourceManager)
        {
            ScriptName = "jasmine.js";
            Script = resourceManager.GetStringFromAssemblyOf<Framework>("Forseti.Jasmine.Scripts.jasmine.js");
            ExecuteScriptName = "jasmine-executor.js";
            ExecuteScript = resourceManager.GetStringFromAssemblyOf<Framework>("Forseti.Jasmine.Scripts.jasmine-executor.js");
            ReportScriptName = "jasmine-reporter.js";
            ReportScript = resourceManager.GetStringFromAssemblyOf<Framework>("Forseti.Jasmine.Scripts.jasmine-reporter.js");
        }


        public string ScriptName { get; private set; }
        public string Script { get; private set; }
        public string ExecuteScriptName { get; private set; }
        public string ExecuteScript { get; private set; }
        public string ReportScriptName { get; private set; }
        public string ReportScript { get; private set; }
    }
}
