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
using Forseti.TFSBuildActivities.TestResultsService;
using System.Security.Principal;
using System.ServiceModel;

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


            var binding = new BasicHttpBinding();
            var address = new EndpointAddress("http://tfs:8080/tfs/_tfs_resources/TestManagement/v1.0/TestResults.asmx");
            

            var service = new TestResultsService.TestResultsServiceSoapClient();

            
            var userId = ReadCurrentUsersIdentity();

            
            /*
            var testRun = new TestRun
            {
                Title = "",
                Owner = userId,
                State = (byte)TestRunState.NotStarted,
                BuildUri = buildDetails.Uri,
                BuildNumber = Options.Build,
                BuildPlatform = Options.Platform,
                BuildFlavor = Options.Flavour,
                // TODO: Get correct times into <Times> element and use those instead.
                StartDate = GetStartDate(trx),
                CompleteDate = GetStartDate(trx).AddMinutes(1),
                PostProcessState = (byte)PostProcessState.Complete,
                Iteration = Options.TeamProject,
                Version = 1000,
                TeamProject = Options.TeamProject
            };
              
            var result = client.CreateTestRun(testRun, Options.TeamProject);
            */


            /*
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
            */

            

            context.Log("Set current dir");
            Directory.SetCurrentDirectory(currentDirectory);

            context.Log("Done");
        }

        private static Guid ReadCurrentUsersIdentity()
        {
            using (var client = new IdentityManagementService.IdentityManagementWebServiceSoapClient())
            {
                var currentUser = WindowsIdentity.GetCurrent();

                if (currentUser == null)
                    throw new InvalidOperationException("Could not find current Windows user.");

                var result = client.ReadIdentities(0, new[] { currentUser.Name }, 0, 0);
                if ((result == null) || (result.Length != 1) || (result[0].Length != 1))
                    throw new InvalidOperationException(string.Format("Could not find user {0} in TFS.", currentUser.Name));

                return result[0][0].TeamFoundationId;
            }
        }

    }
}
