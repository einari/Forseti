using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Forseti.Output.MSTest.Transformation
{
    public class TestSettings : ITransformToTrx
    {
        const string _elementName = "TestSettings";

        public static string DefaultDeploymentRoot = string.Empty;
        public static string DefaultExecutionAgentRule = "Execution Agents";

        public string Description { get; set; }
        public string ExecutionAgentRule { get; set; }
        public bool EnableDeployment { get; set; }
        public string RunDeploymentRoot { get; set; }


        public XElement TransformToTrx()
        {
            var testSettings = new XElement(TrxBuilder.XMLNS + _elementName);
            testSettings.SetAttributeValue("name", "Default Test Settings");
            testSettings.SetAttributeValue("id", Guid.Parse("8dfb34aa-91bc-45e3-8609-d0a4e732d982"));

            var description = new XElement(TrxBuilder.XMLNS + "Description", Description);

            var deploymentElement = new XElement(TrxBuilder.XMLNS + "Deployment");
            deploymentElement.SetAttributeValue("enabled", EnableDeployment);
            deploymentElement.SetAttributeValue("runDeploymentRoot", RunDeploymentRoot);

            var execution = new XElement(TrxBuilder.XMLNS + "Execution");
            execution.Add(new XElement(TrxBuilder.XMLNS + "TestTypeSpecific", ""));
            execution.Add(new XElement(TrxBuilder.XMLNS + "AgentRule", new XAttribute("name", ExecutionAgentRule)));

            
            testSettings.Add(description, deploymentElement, execution);

            return testSettings;
        }
    }
}
