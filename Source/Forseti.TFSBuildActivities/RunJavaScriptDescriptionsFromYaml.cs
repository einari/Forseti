using System.Activities;
using System.IO;
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
using System.Xml.Linq;
using Microsoft.TeamFoundation.VersionControl.Client;


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

        [Description("Override default path to forseti configuration file. Default is automatically set to WorkspaceRoot\\forseti.yaml")]
        public InArgument<string> YamlFile { get; set; }

        [Description("Boolean indicating if failing javscript tests should cause the build to fail. Default false")]
        public InArgument<bool> ShouldFailingTestsBreakBuild { get; set; }

        [RequiredArgument]
        [Description("The configuration / flavor of the build (\"Debug\", \"Release\"etc. ). Needed for publishing test results. hint: platformConfiguration.Configuration")]
        public InArgument<string> BuildConfiguration { get; set; }

        [RequiredArgument]
        [Description("The platform of the build (\"x86\", \"Any CPU\" etc.) . Needed for publishing test results. hint: platformConfiguration.Platform")]
        public InArgument<string> BuildPlatform { get; set; }

        [RequiredArgument]
        public InArgument<Workspace> Workspace { get; set; }

        CodeActivityContext _context;
        string _workspaceDirectory;
        string _forsetiConfigurationPath;
        string _buildNumber;
        bool _shouldBreakBuild;
        string _buildFlavor;
        string _buildPlatform;
        IBuildDetail _buildDetail;

        void Log(string message, params object[] parameters) 
        {
            if (_context == null)
                return;
            _context.Log(message, parameters);
 
        }

        protected override void Execute(CodeActivityContext context)
        {
            try
            {

                SetActivityVariables(context);


                var trxPath = Path.Combine(_workspaceDirectory, string.Format("forseti_{0}.trx", _buildNumber));
                var computerName = Environment.MachineName;
                var userName = WindowsIdentity.GetCurrent().Name;
                var tfsUserName = _buildDetail.RequestedBy;

                var testRunner = new TestRunner(_forsetiConfigurationPath, trxPath, computerName, userName , tfsUserName);
                testRunner.LogTo((output) => Log(output));
                var allTestsPassed = testRunner.RunTests();


                if (!allTestsPassed && _shouldBreakBuild)
                    _buildDetail.Status = BuildStatus.PartiallySucceeded;
                

                var publisher = new TfsResultPublisher(_buildDetail.BuildServer.TeamProjectCollection.Uri.ToString(),
                                                   _buildNumber,
                                                   _buildDetail.TeamProject,
                                                   _buildPlatform,_buildFlavor);
                publisher.LogTo((output) => Log(output));
                publisher.PublishResultsFromPath(trxPath);

            }
            catch (Exception e)
            {
                Log("Something went wrong while exdcuting javascrpit tests tests! : {0}", e);
                _buildDetail.Status = BuildStatus.PartiallySucceeded;

            }

        }

        private void SetActivityVariables(CodeActivityContext context)
        {
            _context = context;
            SetWorkspaceDirectory(context);
            SetForsetiConfigurationPath(context, _workspaceDirectory);
            SetIfFailingTestsShouldBreakBuild(context);
            SetBuildPlatform(context);
            SetBuildFlavor(context);

            _buildDetail = context.GetExtension<IBuildDetail>();
            _buildNumber = _buildDetail.BuildNumber;
            //var tfs = buildDetail.BuildServer.TeamProjectCollection;
            //var identityManagementService = tfs.GetService<IIdentityManagementService>();
        }

        private void SetBuildFlavor(CodeActivityContext context)
        {
            var flavor = context.GetValue(BuildConfiguration);
            _buildFlavor = flavor;
        }

        private void SetBuildPlatform(CodeActivityContext context)
        {
            var platform = context.GetValue(BuildPlatform);
            _buildPlatform = platform;
        }

        private void SetIfFailingTestsShouldBreakBuild(CodeActivityContext context)
        {
            var shouldBreakBuild = context.GetValue(ShouldFailingTestsBreakBuild);
            _shouldBreakBuild = shouldBreakBuild;
        }

        private void SetWorkspaceDirectory(CodeActivityContext context)
        {

            var workspace = context.GetValue(Workspace);
            _workspaceDirectory = Path.GetFullPath(workspace.Folders[0].LocalItem);
        }

        private void SetForsetiConfigurationPath(CodeActivityContext context, string workspaceDirectory)
        {
            var yamlFile = context.GetValue(YamlFile);
            _context.Log("YAML file input : {0}", yamlFile);
            if (File.Exists(Path.GetFullPath(yamlFile)))
                _forsetiConfigurationPath = yamlFile;
            else
                _forsetiConfigurationPath = Path.Combine(workspaceDirectory, "forseti.yaml");

            if (!File.Exists(Path.GetFullPath(_forsetiConfigurationPath)))
                throw new FileNotFoundException("Could not locate the forseti configuration file at: " + _forsetiConfigurationPath);

        }

        //private TeamFoundationIdentity ReadCurrentUsersIdentity(IIdentityManagementService identityManagementService)
        //{
        //        var currentUser = WindowsIdentity.GetCurrent();

        //        if (currentUser == null)
        //            throw new InvalidOperationException("Could not find current Windows user.");

        //        var result = identityManagementService.ReadIdentities(Microsoft.TeamFoundation.Framework.Common.IdentitySearchFactor.AccountName, new[] { currentUser.Name }, 0, 0);
        //        if ((result == null) || (result.Length != 1) || (result[0].Length != 1))
        //            throw new InvalidOperationException(string.Format("Could not find user {0} in TFS.", currentUser.Name));

        //        return result[0][0];
        //}

    }
}
