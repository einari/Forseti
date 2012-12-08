using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forseti.TFSBuildActivities.Trx
{
    public class TestResult
    {
        public enum ResultOutcome 
        {
            passed = 0,
            failed = 1
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string ComputerName { get; set; }
        public Guid ListId { get; set;   }
        public ResultOutcome Outcome { get; set; }
    }
}
