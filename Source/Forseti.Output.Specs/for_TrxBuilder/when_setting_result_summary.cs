using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using System.Xml.Linq;
using Forseti.Output.MSTest.Transformation;

namespace Forseti.Output.Specs.for_TrxBuilder
{
    public class when_setting_result_summary : given.a_builder
    {
        static int numberPassed, numberFailed, numberInconclusive;
        static ResultSummary summary;

        Establish context = () => 
                                {
                                    numberFailed = 2;
                                    numberPassed = 1;
                                    numberInconclusive = 3;
                                };

        Because of = () =>
                        {
                            builder.SetResultSummary(numberPassed, numberFailed, numberInconclusive); ;
                            summary = builder.Summary;
                        };

        It should_set_the_correct_number_of_passed_results = () => summary.Passed.ShouldEqual(numberPassed);
        It should_set_the_correct_number_of_failed_results = () => summary.Failed.ShouldEqual(numberFailed);
        It should_set_the_correct_number_of_inconclusive_results = () => summary.Inconclusive.ShouldEqual(numberInconclusive);
        It should_have_the_correct_total_of_results = () => summary.Total.ShouldEqual(6);
    }
} 
