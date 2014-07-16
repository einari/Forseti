using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using System.Xml.Linq;
using Forseti.Output.MSTest.Transformation;

namespace Forseti.Output.Specs.for_TrxBuilder
{
    public class when_setting_run_information : given.a_builder
    {
        static Guid runId;
        static string name, 
                      runUser,
                      computerName;

        Establish context = () =>
                        {
                            runId = Guid.NewGuid();
                            name = "run name";
                            runUser = "SomeUser";
                            computerName = "computer Name";

                            builder = new TrxBuilder();
                        };

        Because of = () => builder.SetRunInformation(runId, runUser, computerName, name);

        It should_have_a_test_run_element_with_correct_values = () =>
                                                {
                                                    var testRun = builder.TestRun;

                                                    testRun.Id.ShouldEqual(runId);
                                                    testRun.Name.ShouldEqual(name);
                                                    testRun.RunUser.ShouldEqual(runUser);
                                                };
        
    }
}
