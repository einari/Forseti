using Forseti.Resources;
namespace Forseti.Jasmine
{
    public class Framework : IFramework
    {
        public Framework(IResourceManager resourceManager)
        {
            Script = resourceManager.GetStringFromAssemblyOf<Framework>("Forseti.Jasmine.JS.jasmine.js");
            ExecuteScript = resourceManager.GetStringFromAssemblyOf<Framework>("Forseti.Jasmine.JS.jasmine-executor.js");
        }


        public string Script { get; private set; }
        public string ExecuteScript { get; private set; }
    }
}
