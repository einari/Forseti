using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forseti.TRX.Transformation
{
    public class UnitTestDefinition
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TestFilePath { get; set; }
        public Guid ExecutionId { get; set; }
        public string TestClassName { get; set; }
    }
}
