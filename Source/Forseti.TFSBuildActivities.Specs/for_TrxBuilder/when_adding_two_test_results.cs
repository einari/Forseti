using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using System.Xml.Linq;
using Forseti.TFSBuildActivities.Trx;

namespace Forseti.TFSBuildActivities.Specs.for_TrxBuilder
{
    public class when_adding_two_test_results : given.a_builder
    {
        static Guid id, id2;
        static string name, computerName, testListName,name2;

        Establish context = () => 
                            {
                                name = "test name";
                                id =Guid.NewGuid();
                                computerName = "WTF";
                                testListName = "Qunit";

                                name2 = "another " + name;
                                id2 = Guid.NewGuid();
                            };

        Because of = () =>
                        {
                            builder.AddTestResult(name, id, computerName,UnitTestResult.ResultOutcome.Passed, testListName);
                            builder.AddTestResult(name2, id2, computerName, UnitTestResult.ResultOutcome.Failed, testListName);
                        };

        It should_have_two_tests_added = () => builder.Results.TestResults.Count().ShouldEqual(2);

        It should_contain_two_list_type_for_test_list = () => builder.TestLists.Lists.ContainsKey(testListName).ShouldBeTrue();
       
    }
} 
