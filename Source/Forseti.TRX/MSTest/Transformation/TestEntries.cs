using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Forseti.Output.MSTest.Transformation
{
    public class TestEntries : ITransformToTrx
    {
        const string _elementName = "TestEntries";

        public IList<TestEntry> Entries { get; set; }

        public TestEntries() 
        {
            Entries = new List<TestEntry>();
        }

        public void AddEntry(Guid testId, Guid executionId) 
        {
            Entries.Add(new TestEntry 
                            {
                                TestId = testId,
                                ExecutionId = executionId
                            });
        }

        public XElement TransformToTrx()
        {
            var testEntries = new XElement(TrxBuilder.XMLNS + _elementName);
            foreach (var entry in Entries)
            {
                var result = new XElement(TrxBuilder.XMLNS + "TestEntry",
                                                        new XAttribute("testId", entry.TestId)
                                                      , new XAttribute("executionId", entry.ExecutionId)
                                                      , new XAttribute("testListId", entry.TestListId));


                testEntries.Add(result);
            }

            return testEntries;
        }
    }
}
