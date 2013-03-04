using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forseti.TRX.Transformation
{
    public class UnitTestResult
    {
        public enum ResultOutcome
        {
            Passed = 0,
            Failed = 1,
            Inconclusive = 2,
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ComputerName { get; set; }
        public Guid ExecutionId { get; set; }
        public ResultOutcome Outcome { get; set; }
        public string ErrorMessage { get; set; }

        public bool HasErrorMessage { get { return !string.IsNullOrEmpty(ErrorMessage); } }
    }
}
