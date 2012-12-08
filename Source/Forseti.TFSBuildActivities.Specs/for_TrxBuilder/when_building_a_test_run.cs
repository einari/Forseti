﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Forseti.TFSBuildActivities.Trx;
using Machine.Specifications;
using System.Xml.Linq;

namespace Forseti.TFSBuildActivities.Specs.for_TrxBuilder
{
    public class when_building_a_test_run
    {
        static TrxBuilder builder;
        static XDocument trx;

        Establish context = () => builder = new TrxBuilder();

        Because of = () =>
                        {
                            trx = builder.SetRunInformation(Guid.NewGuid(), "SomeName", "SomeUSer")
                                       .SetDefaultTestSettingsWithDescription("This is a hardcoded test")
                                       .SetResultSummary(1, 0)
                                       .SetRunTimes(DateTime.Now, DateTime.Now)
                                       .AddTestResult("should_be_a_test", "for_System_when_publishing_should_be_a_test", "BUILDSERVER", TestResult.ResultOutcome.passed, "QUnit")
                                       .Build();

                            //Debug.Write(trx.ToString());

                        };

        It should_return_an_xdocument = () => trx.ShouldNotBeNull();
    }
}
