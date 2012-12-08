using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Forseti.TFSBuildActivities.Trx
{
    public class TestSettings : IConvertToTrx
    {
        const string _elementName = "TestSettings";

        public static string DefaultDeploymentRoot = string.Empty;
        public static string DefaultExecutionAgentRule = "Execution Agents";

        public string Description { get; set; }
        public string ExecutionAgentRule { get; set; }
        public bool EnableDeployment { get; set; }
        public string RunDeploymentRoot { get; set; }


        public XElement ConvertToTrxNode()
        {
            var testSettings = new XElement(XName.Get(_elementName, TrxBuilder.XMLNS));
            testSettings.SetAttributeValue("name","Build");
            testSettings.SetAttributeValue("id", Guid.NewGuid());

            var description = new XElement("Description",Description);

            var deploymentElement = new XElement("Deployment");
            deploymentElement.SetAttributeValue("enabled", EnableDeployment);
            deploymentElement.SetAttributeValue("runDeploymentRoot", RunDeploymentRoot);

            var execution = new XElement("Execution");
            execution.SetElementValue("TestTypeSpecific","");
            execution.Add(new XElement("AgentRule", new XAttribute("name", ExecutionAgentRule)));

            
            testSettings.Add(description, deploymentElement, execution);

            return testSettings;
        }
    }
}
