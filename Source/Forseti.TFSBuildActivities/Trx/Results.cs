using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Forseti.TFSBuildActivities.Trx
{
    public class Results : IConvertToTrx
    {
        const string _elementName = "Results";
        readonly Guid _testType = Guid.Parse("82C80242-4F2C-4558-9678-0A1DBD440702");

        public IList<TestResult> TestResults { get; set; }

        public Results() 
        {
            TestResults = new List<TestResult>();
        }

        public void Add(TestResult result) 
        {
            TestResults.Add(result);
        }

        public XElement ConvertToTrxNode()
        {
            var results = new XElement(_elementName);
            foreach (var testResult in TestResults)
            {
                var result = new XElement("TestResult", new XAttribute("id", testResult.Id)
                                                      , new XAttribute("listId", testResult.ListId)
                                                      , new XAttribute("testType",_testType)
                                                      , new XAttribute("computerName",testResult.ComputerName)
                                                      , new XAttribute("name",testResult.Name)
                                                      , new XAttribute("outcome", testResult.Outcome));


                results.Add(result);
            }

            return results;
        }
    }

}
