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
using System.Security.Principal;
using System.ServiceModel;
using Microsoft.TeamFoundation.Framework.Client;
using System.Collections.Generic;
using System.Diagnostics;

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
            
            try
            {
        
                var configuration = Configure.WithStandard().FromConfigurationFile(yamlPath).Initialize();

                Log("Forseti configuration initialized : {0}", configuration);

                configuration.HarnessChangeManager.RegisterWatcher(typeof( HarnessWatcher));


                var results = configuration.HarnessManager.Run();
                Log("Forseti Results : {0}", results);

            }
            catch (Exception e)
            {
                Log("Something went wrong! : {0}", e);
                throw;
            }
            context.Log("Done");
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
