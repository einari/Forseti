using System.IO;
using System.Linq;
using System.Text;
using Forseti.Resources;
using Spark;
using Spark.FileSystem;
using System;
using System.Collections.Generic;
using Forseti.Harnesses;
using System.Security.AccessControl;

namespace Forseti.Pages.Spark
{
    public class PageGenerator : IPageGenerator
    {
        const string TemplateName = "Harness";
        SparkViewDescriptor _descriptor;
        SparkViewEngine _engine;

        public PageGenerator(IResourceManager resourceManager)
        {
            var template = resourceManager.GetStringFromAssemblyOf<PageGenerator>("Forseti.Pages.Spark.Harness.spark");

            var settings = new SparkSettings().SetPageBaseType(typeof(HarnessView));
            var templates = new InMemoryViewFolder();
            _engine = new SparkViewEngine(settings)
            {
                ViewFolder = templates
            };
            templates.Add(TemplateName, template); 
            _descriptor = new SparkViewDescriptor().AddTemplate(TemplateName);
        }


        public Page GenerateFrom(Harness harness)
        {
            var page = new Page();

            var harnessView = (HarnessView)_engine.CreateInstance(_descriptor);
            harnessView.Harness = harness;
            harnessView.FrameworkScript = harness.Framework.ScriptName;
            harnessView.FrameworkExecutionScript = harness.Framework.ExecuteScriptName;
            harnessView.FrameworkReportingScript = harness.Framework.ReportScriptName;

            page.RootPath = Path.GetTempPath() + @"Forseti/";
            page.Filename = string.Format("{0}runner.html", page.RootPath);

            if (!Directory.Exists(page.RootPath))
                Directory.CreateDirectory(page.RootPath);

            if (harness.HasDependencies())
            {
                var actualDependencies = new List<string>();
                foreach (var dependency in harness.Dependencies)
                {
                    CopyScript(page.RootPath, dependency.RelativePath);
                    actualDependencies.Add(dependency.RelativePath);
                }
                harnessView.Dependencies = actualDependencies.ToArray();
            } else {
				harnessView.Dependencies = new string[0];
			}
							

            var writer = new StringWriter();
            harnessView.RenderView(writer);

            var result = writer.ToString();

            File.WriteAllText(page.RootPath + harness.Framework.ScriptName, harness.Framework.Script);
            File.WriteAllText(page.RootPath + harness.Framework.ExecuteScriptName, harness.Framework.ExecuteScript);
            File.WriteAllText(page.RootPath + harness.Framework.ReportScriptName, harness.Framework.ReportScript);

            foreach (var scriptFile in harnessView.SystemScripts)
                CopyScript(page.RootPath, scriptFile);

            foreach (var scriptFile in harnessView.CaseScripts)
                CopyScript(page.RootPath, scriptFile);
			
            if (harness.IncludeSubFoldersFromDescriptions)
                CopyDescriptionsRecursively(page, harnessView);
            
            File.WriteAllText(page.Filename, result);

            return page;
        }

        void CopyDescriptionsRecursively(Page page, HarnessView harnessView)
        {
            
            var folders = harnessView.CaseScripts.GroupBy(c => ((Files.File)c).Folder).Select(g => g.Key).ToArray();
            foreach (var folder in folders)
            {
                var sourcePath = Path.Combine(Directory.GetCurrentDirectory(), folder);
                var targetPath = Path.Combine(page.RootPath, folder);
                CopyFilesRecursively(page.RootPath, new DirectoryInfo(targetPath), new DirectoryInfo(sourcePath));
            }
        }

        void CopyFilesRecursively(string rootPath, DirectoryInfo target, DirectoryInfo source)
        {
            foreach (var dir in source.GetDirectories())
                CopyFilesRecursively(rootPath, target.CreateSubdirectory(dir.Name), dir);
            foreach (var file in source.GetFiles())
                CopyScript(rootPath, file.FullName);
        }


        void CopyScript(string rootPath, Files.File scriptFile)
        {
            var target = Path.Combine(rootPath,scriptFile.RelativePath);
            var script = scriptFile.ReadAllText();
            var dir = Path.GetDirectoryName(target);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.WriteAllText(target, script, Encoding.ASCII);
        }
    }
}
