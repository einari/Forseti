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
        string _forsetiJs;
        string _requireJs;
        string _forsetiBootstrapperJs;

        public PageGenerator(IResourceManager resourceManager)
        {
            var template = resourceManager.GetStringFromAssemblyOf<PageGenerator>("Forseti.Pages.Spark.Harness.spark");
            _forsetiJs = resourceManager.GetStringFromAssemblyOf<Forseti.Scripting.ScriptEngine>("Forseti.Scripting.Scripts.forseti.js");
            _requireJs = resourceManager.GetStringFromAssemblyOf<Forseti.Scripting.ScriptEngine>("Forseti.Scripting.Scripts.require.js");
            _forsetiBootstrapperJs = resourceManager.GetStringFromAssemblyOf<Forseti.Scripting.ScriptEngine>("Forseti.Scripting.Scripts.forseti.bootstrapper.js");

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
            harnessView.RunnerScripts = new[] {"forseti.bootstrapper.js", "r.js", "forseti.js"} ;
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
                    CopyScript(page, dependency.RelativePath);
                    actualDependencies.Add(dependency.RelativePath);
                }
                harnessView.Dependencies = actualDependencies.ToArray();
            } else {
				harnessView.Dependencies = new string[0];
			}
							

            var writer = new StringWriter();

            foreach (var scriptFile in harnessView.SystemScripts)
                CopyScriptAndPossibleAdditionalReferences(harness, page, scriptFile);

            var actualCaseScripts = new List<CaseScriptDescriptor>();
            foreach (var caseDescriptor in harnessView.CaseScripts)
            {
                var additionalRefereces = CopyScriptAndPossibleAdditionalReferences(harness, page, caseDescriptor.Case);
                actualCaseScripts.Add(new CaseScriptDescriptor { CaseDependencies = additionalRefereces, Case = caseDescriptor.Case });
            }
            harnessView.CaseScripts = actualCaseScripts.ToArray();


            harnessView.RenderView(writer);

            var result = writer.ToString();

            File.WriteAllText(page.RootPath + "forseti.bootstrapper.js", _forsetiBootstrapperJs);
            File.WriteAllText(page.RootPath + "r.js", _requireJs);
            File.WriteAllText(page.RootPath + "forseti.js", _forsetiJs);
            File.WriteAllText(page.RootPath + harness.Framework.ScriptName, harness.Framework.Script);
            File.WriteAllText(page.RootPath + harness.Framework.ExecuteScriptName, harness.Framework.ExecuteScript);
            File.WriteAllText(page.RootPath + harness.Framework.ReportScriptName, harness.Framework.ReportScript);

			
            
            File.WriteAllText(page.Filename, result);

            return page;
        }

        void CopyFilesRecursively(Page page, string descriptionRelativePath, Files.File descriptionFile, DirectoryInfo target, DirectoryInfo source, List<string> additionalReferences)
        {
            foreach (var dir in source.GetDirectories())
                CopyFilesRecursively(page, descriptionRelativePath, descriptionFile, target.CreateSubdirectory(dir.Name), dir, additionalReferences);
            foreach (var file in source.GetFiles())
            {
                var relativeFileName = string.Format("{0}/{1}/{2}", descriptionRelativePath, file.Directory.Name, file.Name);
                additionalReferences.Add(((Files.File)relativeFileName).RelativePath);
                CopyScript(page, file.FullName);
            }
        }


        IEnumerable<string> CopyScriptAndPossibleAdditionalReferences(Harness harness, Page page, Files.File descriptionFile)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var additionalReferences = new List<string>();
            if (harness.IncludeSubFoldersFromDescriptions)
            {
                var descriptionSourcePath = Path.GetDirectoryName(Path.Combine(currentDirectory, descriptionFile.Folder));
                var descriptionRelativePath = descriptionFile.Folder;
                var folders = Directory.GetDirectories(Path.GetDirectoryName(descriptionFile.FullPath));
                foreach (var folder in folders)
                {
                    var sourcePath = Path.Combine(currentDirectory, folder);
                    var targetPath = Path.Combine(page.RootPath, folder);

                    CopyFilesRecursively(page, descriptionRelativePath, descriptionFile, new DirectoryInfo(targetPath), new DirectoryInfo(sourcePath), additionalReferences);
                }
            }
            CopyScript(page, descriptionFile);

            return additionalReferences;
        }

        private static void CopyScript(Page page, Files.File scriptFile)
        {
            var target = Path.Combine(page.RootPath, scriptFile.RelativePath);
            var script = scriptFile.ReadAllText();
            var dir = Path.GetDirectoryName(target);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.WriteAllText(target, script, Encoding.ASCII);
        }
    }
}
