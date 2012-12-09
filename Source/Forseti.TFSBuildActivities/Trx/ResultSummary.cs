using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Forseti.TFSBuildActivities.Trx
{

    public class ResultSummary : IConvertToTrx
    {
        const string _elementName = "ResultSummary";
        public class Outcome 
        { 
            public const string COMPLETED = "Completed";
        }

        public int Failed { get; set; }
        public int Passed { get; set; }
        public int Total { get { return Passed + Failed; } }

        public XElement ConvertToTrxNode()
        {
            var resultSummary = new XElement(TrxBuilder.XMLNS + _elementName, new XAttribute("outcome", Outcome.COMPLETED));
            resultSummary.Add(new XElement(TrxBuilder.XMLNS + "Counters", 
                                                      new XAttribute("passed", Passed)
                                                    , new XAttribute("failed", Failed)
                                                    , new XAttribute("total", Total)));
            return resultSummary;
        }
    }
}
