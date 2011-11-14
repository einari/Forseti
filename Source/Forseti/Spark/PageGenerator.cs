using System.IO;
using Forseti.Resources;
using Spark;
using Spark.FileSystem;

namespace Forseti.Spark
{
    public class PageGenerator : IPageGenerator
    {
        const string TemplateName = "Harness";
        SparkViewDescriptor _descriptor;
        SparkViewEngine _engine;
        IFramework _framework;


        public PageGenerator(IResourceManager resourceManager, IFramework framework)
        {
            var template = resourceManager.GetStringFromAssemblyOf<PageGenerator>("Forseti.Spark.Harness.spark");

            _framework = framework;

            var settings = new SparkSettings().SetPageBaseType(typeof(HarnessView));
            var templates = new InMemoryViewFolder();
            _engine = new SparkViewEngine(settings)
            {
                ViewFolder = templates
            };
            templates.Add(TemplateName, template); //"<for each=\"var s in Stuff\"><p>${s}</p></for>");
            _descriptor = new SparkViewDescriptor().AddTemplate(TemplateName);
        }


        public Page GenerateFrom(Harness harness)
        {
            var page = new Page();

            var harnessView = (HarnessView)_engine.CreateInstance(_descriptor);
            harnessView.Harness = harness;
            var writer = new StringWriter();
            harnessView.RenderView(writer);

            var result = writer.ToString();
            File.WriteAllText("c:\\jasmine-runner.html", result);

            return page;
        }
    }
}
