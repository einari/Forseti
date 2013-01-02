using Machine.Specifications;

namespace Forseti.Specs.Harnesses.for_HarnessResult
{
    public class when_asking_for_inconclusive_cases_and_one_case_is_inconclusie : given.a_harness_with_one_inconclusive_case
    {
        It should_return_one_inconclusive_case = () => result.InconclusiveCaseCount.ShouldEqual(1);
        It should_return_zero_successful_cases = () => result.SuccessfulCaseCount.ShouldEqual(0);
        It should_return_zero_failing_cases = () => result.FailedCaseCount.ShouldEqual(0);
    }
}
