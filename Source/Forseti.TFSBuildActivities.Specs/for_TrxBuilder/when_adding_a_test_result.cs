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
        static string name, id, computerName, testListName;
        static IEnumerable<TestResult> results;
        static IDictionary<string, Guid> testLists;

        Establish context = () => 
                            {
                                name = "test name";
                                id ="System.suite.name";
                                computerName = "WTF";
                                testListName = "Qunit";

                            };

        Because of = () =>
                        {
                            builder.AddTestResult(name, id, computerName, TestResult.ResultOutcome.passed, testListName);
                            testLists = builder.TestLists.Lists;
                            results = builder.Results.TestResults;
                        };

        It should_have_one_test_added = () => results.Count().ShouldEqual(1);

        It should_contain_a_list_type_for_test_list = () => testLists.ContainsKey(testListName).ShouldBeTrue();
       
    }
} 
