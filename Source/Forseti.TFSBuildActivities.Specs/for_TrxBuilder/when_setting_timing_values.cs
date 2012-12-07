using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using System.Xml.Linq;
using Forseti.TFSBuildActivities.Trx;

namespace Forseti.TFSBuildActivities.Specs.for_TrxBuilder
{
    public class when_setting_default_test_settings : given.a_builder
    {
        static string description;
        static TestSettings settings;

        Establish context = () => description = "some description";

        Because of = () =>
                        {
                            builder.SetDefaultTestSettingsWithDescription(description);
                            settings = builder.RunSettings;
                        };
        It should_set_the_description = () =>       settings.Description.ShouldEqual(description);
        It should_disable_deployment = () => settings.EnableDeployment.ShouldBeFalse();
        It should_set_the_default_deployment_root = () => settings.RunDeploymentRoot.ShouldEqual(TestSettings.DefaultDeploymentRoot);
        It should_set_the_execution_agent_rule = () => settings.ExecutionAgentRule.ShouldEqual(TestSettings.DefaultExecutionAgentRule);
    }
} 
