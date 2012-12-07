using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forseti.TFSBuildActivities.Trx
{

    public class ResultSummary
    {    
        public class Outcome 
        { 
            public const string COMPLETED = "Completed";
        }

        public int Failed { get; set; }
        public int Passed { get; set; }
        public int Total { get; set; }
    }
}
