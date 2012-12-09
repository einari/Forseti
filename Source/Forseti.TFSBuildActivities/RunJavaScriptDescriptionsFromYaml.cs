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
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using Microsoft.TeamFoundation.Framework.Client;
using System.Collections.Generic;
using System.Diagnostics;
using Forseti.Harnesses;
using Forseti.TFSBuildActivities.Trx;
using Forseti.Extensions;
using Forseti.Suites;


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
       
        public InArgument<string> ForsetiPath { get; set; }

        CodeActivityContext _context;

        void Log(string message, params object[] parameters) 
        {
            if (_context == null)
                return;
            _context.Log(message, parameters);
 
        }

        protected override void Execute(CodeActivityContext context)
        {
            _context = context;
            var origianlCurrentDir = Directory.GetCurrentDirectory();
            var buildDetail = context.GetExtension<IBuildDetail>();

            
            var buildDirectory  = System.Environment.ExpandEnvironmentVariables("%BuildDirectory%");

            Log("BuildDirectory : {0}", buildDirectory);

            


            //var currentDirectory = Directory.GetCurrentDirectory();
            var currentDirectory = @"C:\Builds\1\ForsetiTesting\Continuous\Sources\";
            Log("CurrentDirectory : {0}", currentDirectory);

            var file = context.GetValue(this.YamlFile);
            Log("YamlFile : {0}", file);
            
            var fileDirectory = Path.GetDirectoryName(file);
            Log("YamleFileDirectory : {0}",fileDirectory);
            Directory.SetCurrentDirectory(fileDirectory);


            string fullPath = currentDirectory + @"Tools\Forseti\Forseti.exe";
            Log("ForsetiPath : {0}", fullPath);            


            string workingDirectory = Path.GetDirectoryName(buildDirectory);
            Log("WorkingDirecrory : {0}", workingDirectory);


            Directory.SetCurrentDirectory(currentDirectory);
            var yamlPath = currentDirectory + "forseti.yaml";
            Log("CurrentDirectory : {0}", Directory.GetCurrentDirectory());
            
            IEnumerable<HarnessResult> testResults = null;
            try
            {

                var configuration = Configure.WithStandard().FromConfigurationFile(yamlPath).Initialize();

                Log("Forseti configuration initialized : {0}", configuration);

                configuration.HarnessChangeManager.RegisterWatcher(typeof(HarnessWatcher));


                //testResults = configuration.HarnessManager.Run();
                //Log("Forseti Results : {0}", testResults);

            }
            catch (Exception e)
            {
                Log("Something went wrong while running forseti tests! : {0}", e);
            }
            finally
            {
                Directory.SetCurrentDirectory(origianlCurrentDir);

            }

            GenerateTRXResultFileForPublishing(testResults);


            try
            {
                var builder = new TrxBuilder();

                var trx = builder.SetRunInformation(Guid.NewGuid(), "SomeName", "SomeUSer")
                       .SetDefaultTestSettingsWithDescription("This is a hardcoded test")
                       .SetResultSummary(1, 0)
                       .SetRunTimes(DateTime.Now, DateTime.Now)
                       .AddTestResult("should_be_a_test", Guid.NewGuid(), "BUILDSERVER" , UnitTestResult.ResultOutcome.Passed, "QUnit")
                       .Build();

                Log("XML {0} : ", trx);

                trx.Save(currentDirectory + "forseti.trx");

                var publisher = new TestResultPublisher(context);

                //publisher.PublishResultsFromPath(currentDirectory + "test.trx");
                publisher.PublishResultsFromPath(currentDirectory + "forseti.trx");

            }
            catch (Exception e)
            {
                Log("Something went wrong while publishing tests! : {0}", e);

            }
            context.Log("Done");
        }

        

        private void GenerateTRXResultFileForPublishing(IEnumerable<HarnessResult> testResults)
        {
            var successfullTests = testResults.Sum(suite => suite.SuccessfulCaseCount);
            var failingTests = testResults.Sum(suite => suite.FailedCaseCount);
            var startTime = testResults.Min(suite => suite.StartTime);
            var endTime = testResults.Max(suite => suite.EndTime);


            var builder = new TrxBuilder();
            builder.SetRunInformation(Guid.NewGuid(), "SomeName", "SomeUSer")
                      .SetDefaultTestSettingsWithDescription("This is a hardcoded test")
                      .SetResultSummary(successfullTests, failingTests)
                      .SetRunTimes(startTime, endTime);



             foreach (var harnessResult in testResults)
             {
                 harnessResult.AffectedSuites.ForEach(suite => 
                        suite.Descriptions.ForEach(description =>
                        {
                            description.Cases.ForEach(@case =>
                            {
                                if (CantResultBeReported(@case))
                                    return;

                                builder.AddTestResult( @case.Name, 
                                                        Guid.NewGuid(), 
                                                        "TO BE FIXED", 
                                                        @case.Result.Success ? UnitTestResult.ResultOutcome.Passed : UnitTestResult.ResultOutcome.Failed, 
                                                        description.File.FullPath, 
                                                        description.Name);
                            });

                        }));
             }


             var trx = builder.Build();

        }

        static bool CantResultBeReported(Case @case)
        {
            return String.IsNullOrEmpty(@case.Name);
        }

        private static TeamFoundationIdentity ReadCurrentUsersIdentity(IIdentityManagementService identityManagementService)
        {

                var currentUser = WindowsIdentity.GetCurrent();

                if (currentUser == null)
                    throw new InvalidOperationException("Could not find current Windows user.");

                var result = identityManagementService.ReadIdentities(Microsoft.TeamFoundation.Framework.Common.IdentitySearchFactor.AccountName, new[] { currentUser.Name }, 0, 0);
                if ((result == null) || (result.Length != 1) || (result[0].Length != 1))
                    throw new InvalidOperationException(string.Format("Could not find user {0} in TFS.", currentUser.Name));

                return result[0][0];

        }

    }
}
