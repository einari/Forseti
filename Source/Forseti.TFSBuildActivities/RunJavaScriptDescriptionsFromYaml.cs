using System.Activities;
using System.IO;
using Forseti.Configuration;
using Microsoft.TeamFoundation.Build.Client;

namespace Forseti.TFSBuildActivities
{
    [BuildActivity(HostEnvironmentOption.All)]
    public sealed class RunJavaScriptDescriptionsFromYaml : CodeActivity
    {
        public InArgument<string> YamlFile { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var file = context.GetValue(this.YamlFile);
            var fileDirectory = Path.GetDirectoryName(file);
            Directory.SetCurrentDirectory(fileDirectory);


            var configuration = Configure
                .WithStandard()
                .FromConfigurationFile(file)
                .Initialize();

            configuration.HarnessChangeManager.RegisterWatcher(typeof(HarnessWatcher));

            configuration
                    .HarnessManager.Run();

            Directory.SetCurrentDirectory(currentDirectory);
        }
    }
}
