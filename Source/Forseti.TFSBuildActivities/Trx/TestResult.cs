using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forseti.TFSBuildActivities.Trx
{
    public class TestResult
    {
        public class Outcome 
        {
            public const string PASSED = "Passed";
            public const string FAILED = "Failed";
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string ComputerName { get; set; }
        public Guid Type { get; set; }
        public Guid ListId { get; set;   }
    }
}
