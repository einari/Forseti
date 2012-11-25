using System.Activities;
using System.IO;
using Forseti.Configuration;
using Microsoft.TeamFoundation.Build.Client;
using System.ComponentModel;
using Microsoft.TeamFoundation.TestManagement.Client;
using Microsoft.TeamFoundation.Build.Workflow.Tracking;
using Microsoft.TeamFoundation.Build.Workflow.Activities;
using Microsoft.TeamFoundation.Client;
using System;

namespace Forseti.TFSBuildActivities
{
    public static class CodeActivityContextExtensions
    {
        public static void Log(this CodeActivityContext context, string format, params object[] parameters)
        {
            var message = string.Format(format, parameters);

            var record = new BuildInformationRecord<BuildMessage>
            {
                Value = new BuildMessage
                {
                    Importance = BuildMessageImportance.High,
                    Message = message
                }
            };

            context.Track(record);
        }
    }

    [BuildActivity(HostEnvironmentOption.All)]
    [Description("Activity to run Forseti for JavaScript tests / specs")]
    public sealed class RunJavaScriptDescriptionsFromYaml : CodeActivity
    {
        public InArgument<string> YamlFile { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var file = context.GetValue(this.YamlFile);
            var fileDirectory = Path.GetDirectoryName(file);
            Directory.SetCurrentDirectory(fileDirectory);

            

            /*
            var configuration = Configure
                .WithStandard()
                .FromConfigurationFile(file)
                .Initialize();

            configuration.HarnessChangeManager.RegisterWatcher(typeof(HarnessWatcher));

            configuration
                    .HarnessManager.Run();

            */


            
            var buildDetails = context.GetExtension<IBuildDetail>();
            context.Log("Build detail : {0}",buildDetails);



            var projectCollection = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(new Uri("http://tfs:8080/tfs/DefaultCollection"));
                //buildDetail.Uri);
            var testManagementService = projectCollection.GetService<ITestManagementService>();
                //context.GetExtension<ITestManagementService>();
            context.Log("Test management service : {0}", testManagementService);
            
            var project = testManagementService.GetTeamProject(buildDetails.TeamProject);
            context.Log("Project : {0}", project);

            var testRun = project.TestRuns.Create();
            testRun.Title = "Forseti TestRun";
            testRun.Save();


            context.Log("Set current dir");
            Directory.SetCurrentDirectory(currentDirectory);

            context.Log("Done");
        }
    }
}
