using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Forseti.TRX.Transformation
{
    public class TestDefinitions : ITransformToTrx
    {
        const string _elementName = "TestDefinitions";
        public IList<UnitTestDefinition> UnitTests { get; set; }

        public TestDefinitions() 
        {
            UnitTests = new List<UnitTestDefinition>();
        }

        public void AddDefinition(Guid testId, string testName, Guid executionId, string testFilePath, string testClassName)
        {
            UnitTests.Add(new UnitTestDefinition 
                                {
                                    Id = testId,
                                    Name = testName,
                                    ExecutionId = executionId,
                                    TestFilePath = testFilePath,
                                    TestClassName = testClassName
                                });

        }


        public XElement TransformToTrx()
        {
            var definitions = new XElement(TrxBuilder.XMLNS + _elementName);
            foreach (var testDefinition in UnitTests)
            {
                var definition = new XElement(TrxBuilder.XMLNS + "UnitTest",
                                                        new XAttribute("id", testDefinition.Id)
                                                      , new XAttribute("name", testDefinition.Name)
                                                      , new XAttribute("storage", testDefinition.TestFilePath));

                definition.Add(new XElement(TrxBuilder.XMLNS + "Execution", new XAttribute("id",testDefinition.ExecutionId)));

                definition.Add(new XElement(TrxBuilder.XMLNS + "TestMethod", new XAttribute("codeBase", testDefinition.TestFilePath)
                                                                           , new XAttribute("className", testDefinition.TestClassName)
                                                                           , new XAttribute("name", testDefinition.Name)));


                definitions.Add(definition);
            }
            return definitions;
        }
    }
}
    