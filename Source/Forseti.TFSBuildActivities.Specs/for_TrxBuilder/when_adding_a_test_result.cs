using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using System.Xml.Linq;
using Forseti.TFSBuildActivities.Trx;

namespace Forseti.TFSBuildActivities.Specs.for_TrxBuilder
{
    public class when_adding_a_test_result : given.a_builder
    {
        static Guid id;
        static string name, computerName, testListName;
        static IEnumerable<UnitTestResult> results;
        static IDictionary<string, Guid> testLists;

        Establish context = () => 
                            {
                                name = "test name";
                                id = Guid.NewGuid();
                                computerName = "WTF";
                                testListName = "Qunit";

                            };

        Because of = () =>
                        {
                            builder.AddTestResult(name, id, computerName, UnitTestResult.ResultOutcome.Passed, testListName);
                        };


        It should_create_a_test_definition = () => builder.Definitions.UnitTests.Count().ShouldEqual(1);

        It should_create_a_test_entry = () => builder.TestEntries.Entries.Count().ShouldEqual(1);

        It should_have_one_test_result_added = () => builder.Results.TestResults.Count().ShouldEqual(1);

        It should_have_correct_matching_values = () =>
                                                    {
                                                        var definition = builder.Definitions.UnitTests.First();
                                                        var testEntry = builder.TestEntries.Entries.First();
                                                        var testResult = builder.Results.TestResults.First();

                                                        testEntry.TestId.ShouldEqual(definition.Id);
                                                        testEntry.ExecutionId.ShouldEqual(definition.ExecutionId);

                                                        testResult.Id.ShouldEqual(definition.Id);
                                                        testResult.ExecutionId.ShouldEqual(definition.ExecutionId);
                                                        testResult.Name.ShouldEqual(definition.Name);

                                                    };
    }
} 
