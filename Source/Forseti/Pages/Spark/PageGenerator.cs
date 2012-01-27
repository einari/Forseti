using System.IO;
using System.Text;
using Forseti.Resources;
using Spark;
using Spark.FileSystem;
using System;

namespace Forseti.Pages.Spark
{
    public class PageGenerator : IPageGenerator
    {
        const string TemplateName = "Harness";
        SparkViewDescriptor _descriptor;
        SparkViewEngine _engine;
        IFramework _framework;


        public PageGenerator(IResourceManager resourceManager, IFramework framework)
        {
            var template = resourceManager.GetStringFromAssemblyOf<PageGenerator>("Forseti.Pages.Spark.Harness.spark");

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
            harnessView.FrameworkScript = _framework.ScriptName;
            harnessView.FrameworkExecutionScript = _framework.ExecuteScriptName;
            harnessView.FrameworkReportingScript = _framework.ReportScriptName;

            var writer = new StringWriter();
            harnessView.RenderView(writer);

            var result = writer.ToString();

            
            page.RootPath = Path.GetTempPath() + "\\Forseti\\";
            if (!Directory.Exists(page.RootPath))
                Directory.CreateDirectory(page.RootPath);

            File.WriteAllText(page.RootPath + _framework.ScriptName, _framework.Script);
            File.WriteAllText(page.RootPath + _framework.ExecuteScriptName, _framework.ExecuteScript);
            File.WriteAllText(page.RootPath + _framework.ReportScriptName, _framework.ReportScript);


            foreach (var scriptFile in harnessView.SystemScripts)
                CopyScript(page.RootPath, scriptFile);
                //File.Copy(scriptFile, page.RootPath + scriptFile, true);

            foreach (var scriptFile in harnessView.CaseScripts)
                CopyScript(page.RootPath, scriptFile);
                //File.Copy(scriptFile, page.RootPath + scriptFile, true);
            

            page.Filename = string.Format("{0}jasmine-runner.html", page.RootPath);

            File.WriteAllText(page.Filename, result);

            return page;
        }


        void CopyScript(string rootPath, string scriptFile)
        {
            var target = rootPath + scriptFile;
            var script = File.ReadAllText(scriptFile);

            var dir = Path.GetDirectoryName(target);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.WriteAllText(target, script, Encoding.ASCII);
        }
    }
}
