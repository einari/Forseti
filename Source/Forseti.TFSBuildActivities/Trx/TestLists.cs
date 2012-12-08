using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Forseti.TFSBuildActivities.Trx
{
    public class TestLists : IConvertToTrx
    {
        const string _elementName = "TestLists";

        public IDictionary<string, Guid> Lists { get; set; }

        public TestLists() 
        {
            Lists = new Dictionary<string, Guid>();
        }


        public Guid GetOrCreateListTypeByName(string testListName)
        {
            if (Lists.ContainsKey(testListName))
                return Lists[testListName];

            var testListId = Guid.NewGuid();

            Lists.Add(testListName, testListId);
            return testListId;
        }

        public XElement ConvertToTrxNode()
        {
            var testLists = new XElement(_elementName);
            foreach (var list in Lists)
            {
                testLists.Add(new XElement("TestList", new XAttribute("name", list.Key)
                                                     , new XAttribute("id", list.Value)));
            }

            return testLists;
        }
    }
}
