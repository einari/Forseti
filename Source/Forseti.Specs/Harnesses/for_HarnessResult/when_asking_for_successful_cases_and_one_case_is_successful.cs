using Machine.Specifications;

namespace Forseti.Specs.Harnesses.for_HarnessResult
{
    public class when_asking_for_successful_cases_and_one_case_is_successful : given.a_harness_with_one_successful_case
    {
        It should_return_one_successful_case = () => result.SuccessfulCaseCount.ShouldEqual(1);
        It should_return_zero_failing_cases = () => result.FailedCaseCount.ShouldEqual(0);
    }
}
