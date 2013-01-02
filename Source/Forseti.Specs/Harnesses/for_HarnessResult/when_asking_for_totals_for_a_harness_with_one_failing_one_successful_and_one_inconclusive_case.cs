using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forseti.Suites;
using Machine.Specifications;
using Forseti.Files;

namespace Forseti.Specs.Harnesses.for_HarnessResult
{
    public class when_asking_for_totals_for_a_harness_with_one_failing_one_successful_and_one_inconclusive_case : given.an_empty_harness_result
    {

        protected static Case @case;
        protected static Suite suite;
        protected static Description conclusive_description;
        protected static Description inconclusive_description;

        Establish context = () =>
        {
            suite = new Suite((File)"Something.js");
            conclusive_description = new Description((File)"Something_specs.js");

            inconclusive_description = new Description((File)"Something_specs_that_are_inconclusive.js");

            suite.AddDescription(conclusive_description);
            suite.AddDescription(inconclusive_description);

            inconclusive_description.AddCase(Case.DummyCase);

            conclusive_description.AddCase(new Case
            {
                Name = "it should pass",
                Result = new CaseResult
                {
                    Success = true
                }
            });

            conclusive_description.AddCase(new Case
            {
                Name = "it should fail",
                Result = new CaseResult
                {
                    Success = false
                }
            });

            result.AddAffectedSuite(suite);
        };

        It should_return_one_successful_case = () => result.SuccessfulCaseCount.ShouldEqual(1);
        It should_return_one_failing_cases = () => result.FailedCaseCount.ShouldEqual(1);
        It should_return_one_inconclusive_cases = () => result.InconclusiveCaseCount.ShouldEqual(1);
        It should_return_a_total_of_three_cases = () => result.TotalCaseCount.ShouldEqual(3);
    }
}
