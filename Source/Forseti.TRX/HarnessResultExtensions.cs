using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forseti.Harnesses;

namespace Forseti.TRX
{
    public static class HarnessResultExtensions
    {

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

        public static bool IsSuccessful (this IEnumerable<HarnessResult> harnessResults)
        {
            return harnessResults.SuccessfulCaseCount() > 0 && harnessResults.FailedCaseCount()  == 0;
        }


    }
}
