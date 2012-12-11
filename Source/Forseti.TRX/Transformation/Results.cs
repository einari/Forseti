using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Forseti.TRX.Transformation
{
    public class Results : ITransformToTrx
    {
        const string _elementName = "Results";
        readonly Guid _testType = Guid.Parse("82C80242-4F2C-4558-9678-0A1DBD440702");

        public IList<UnitTestResult> TestResults { get; set; }

        public Results() 
        {
            TestResults = new List<UnitTestResult>();
        }

        public void AddResult(string name, Guid testId, Guid executionId, string computerName, UnitTestResult.ResultOutcome outcome)
        {
            TestResults.Add(new UnitTestResult
            {
                Name = name,
                Id = testId,
                ExecutionId = executionId,
                ComputerName = computerName,
                Outcome = outcome
            });
        }

        public XElement TransformToTrx()
        {
            var results = new XElement(TrxBuilder.XMLNS + _elementName);
            foreach (var testResult in TestResults)
            {
                var result = new XElement(TrxBuilder.XMLNS + "UnitTestResult", 
                                                        new XAttribute("testId", testResult.Id)
                                                      , new XAttribute("executionId", testResult.ExecutionId)
                                                      , new XAttribute("testListId", TestLists.RESULTSNOTINLIST)
                                                      , new XAttribute("testType",_testType)
                                                      , new XAttribute("computerName",testResult.ComputerName)
                                                      , new XAttribute("testName",testResult.Name)
                                                      , new XAttribute("outcome", testResult.Outcome));


                results.Add(result);
            }

            return results;
        }

    }

}
