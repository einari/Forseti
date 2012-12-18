using Forseti.Files;
using Forseti.Suites;
using Machine.Specifications;

namespace Forseti.Specs.Harnesses.for_HarnessResult.given
{
    public class a_harness_with_one_successful_case : an_empty_harness_result
    {
        protected static Case @case;
        protected static Suite suite;
        protected static Description description;

        Establish context = () =>
        {
            suite = new Suite((File)"Something.js");
            description = new Description((File)"Something_specs.js");
            suite.AddDescription(description);
            @case = new Case
            {
                Name = "it should pass", 
                Result = new CaseResult
                {
                    Success = true
                }
            };
            
            description.AddCase(@case);

            result.AddAffectedSuite(suite);
        };
    }
}
