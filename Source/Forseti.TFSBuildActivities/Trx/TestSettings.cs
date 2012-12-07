using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forseti.TFSBuildActivities.Trx
{
    public class TestSettings
    {
        public static string DefaultDeploymentRoot = string.Empty;
        public static string DefaultExecutionAgentRule = "Execution Agents";

        public string Description { get; set; }
        public string ExecutionAgentRule { get; set; }
        public bool EnableDeployment { get; set; }
        public string RunDeploymentRoot { get; set; }

    }
}
