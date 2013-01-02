using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Forseti.TRX.Transformation
{

    public class ResultSummary : ITransformToTrx
    {
        const string _elementName = "ResultSummary";
        public class Outcome 
        { 
            public const string COMPLETED = "Completed";
        }

        public int Failed { get; set; }
        public int Passed { get; set; }
        public int Inconclusive { get; set; }
        public int Total { get { return Passed + Failed + Inconclusive; } }

        public XElement TransformToTrx()
        {
            var resultSummary = new XElement(TrxBuilder.XMLNS + _elementName, new XAttribute("outcome", Outcome.COMPLETED));
            resultSummary.Add(new XElement(TrxBuilder.XMLNS + "Counters", 
                                                      new XAttribute("passed", Passed)
                                                    , new XAttribute("failed", Failed)
                                                    , new XAttribute("inconclusive", Inconclusive)
                                                    , new XAttribute("total", Total)));
            return resultSummary;
        }
    }
}
