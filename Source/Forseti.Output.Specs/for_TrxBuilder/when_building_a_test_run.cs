﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Forseti.Output.MSTest.Transformation;
using Machine.Specifications;
using System.Xml.Linq;

namespace Forseti.Output.Specs.for_TrxBuilder
{
    public class when_building_a_test_run
    {
        static TrxBuilder builder;
        static XDocument trx;

        Establish context = () => builder = new TrxBuilder();

        Because of = () =>
                        {
                            trx = builder.SetRunInformation(Guid.NewGuid(), "SomeUSer","ComputerName" ,"SomeName")
                                       .SetDefaultTestSettingsWithDescription("This is a hardcoded test")
                                       .SetResultSummary(1, 0,0)
                                       .SetRunTimes(DateTime.Now, DateTime.Now)
                                       .AddTestResult("should_be_a_test", Guid.NewGuid(), "BUILDSERVER", UnitTestResult.ResultOutcome.Passed, "QUnit")
                                       .AsTrxDocument();

                            
                        };

        It should_return_an_xdocument = () => trx.ShouldNotBeNull();
    }
}
