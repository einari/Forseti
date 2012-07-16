using Machine.Specifications;

namespace Forseti.Specs.Harnesses.for_HarnessResult
{
    public class when_asking_for_failed_cases_and_one_case_is_failing : given.a_harness_with_one_failing_case
    {
        It should_return_one_failing_case = () => result.FailedCaseCount.ShouldEqual(1);
        It should_return_zero_successful_cases = () => result.SuccessfulCaseCount.ShouldEqual(0);
    }
}
