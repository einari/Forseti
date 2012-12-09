using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forseti.TFSBuildActivities.Trx
{
    public class UnitTestResult
    {
        public enum ResultOutcome 
        {
            Passed = 0,
            Failed = 1
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ComputerName { get; set; }
        public Guid ExecutionId { get; set; }
        public ResultOutcome Outcome { get; set; }
    }
}
