using System;
using System.Collections.Generic;
using System.Linq;
using Forseti.Harnesses;
using Forseti.Suites;

namespace Forseti.Reporting
{
    public static class HarnessResultExtensions
    {
        public static string FriendlyName(this Case @case)
        {
            return ToFriendlyName(@case.Name);
        }

        static string ToFriendlyName(string name)
        {
            return string.IsNullOrEmpty(name) ? String.Empty : name.Replace("_", " ");
        }

        public static string FriendlyName(this Description description)
        {
            return ToFriendlyName(description.Name);
        }

        public static string FriendlyName(this Suite suite) 
        {
            return ToFriendlyName(suite.System);
        }

        public static int SuccessfulCaseCount(this IEnumerable<HarnessResult> harnessResults)
        {
            return harnessResults.Sum(result => result.SuccessfulCaseCount);
        }

        public static int FailedCaseCount(this IEnumerable<HarnessResult> harnessResults)
        {
            return harnessResults.Sum(result => result.FailedCaseCount);
        }

        public static int InconclusiveCaseCount(this IEnumerable<HarnessResult> harnessResults)
        {
            return harnessResults.Sum(result => result.InconclusiveCaseCount);
        }

        public static DateTime StartTime(this IEnumerable<HarnessResult> harnessResults)
        {
            return harnessResults.Min(result => result.StartTime);
        }

        public static DateTime EndTime(this IEnumerable<HarnessResult> harnessResults)
        {
            return harnessResults.Max(result => result.EndTime);
        }


        public static bool IsFailing(this IEnumerable<HarnessResult> harnessResults)
        {
            return harnessResults.FailedCaseCount() > 0;
        }

        public static bool IsSuccessful(this IEnumerable<HarnessResult> harnessResults)
        {
            return harnessResults.SuccessfulCaseCount() > 0 && harnessResults.FailedCaseCount() == 0;
        }
    }
}
