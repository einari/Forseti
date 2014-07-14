using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Forseti.Output.MSTest.Transformation
{
    public class TestLists : ITransformToTrx
    {
        const string _elementName = "TestLists";
        public static readonly Guid RESULTSNOTINLIST = Guid.Parse("8c84fa94-04c1-424b-9868-57a2d4851a1d");
        public static readonly Guid ALLLOADEDRESULTS = Guid.Parse("19431567-8539-422a-85d7-44ee4e166bda");

        public IDictionary<string, Guid> Lists { get; private set; }

        public TestLists() 
        {
            Lists = new Dictionary<string, Guid>();
            Lists.Add("Results Not in a List", RESULTSNOTINLIST);
            Lists.Add("All Loaded Results", ALLLOADEDRESULTS);
        }


        public XElement TransformToTrx()
        {
            var testLists = new XElement(TrxBuilder.XMLNS + _elementName);
            foreach (var list in Lists)
            {
                testLists.Add(new XElement(TrxBuilder.XMLNS + "TestList", 
                                                       new XAttribute("name", list.Key)
                                                     , new XAttribute("id", list.Value)));
            }

            return testLists;
        }
    }
}
