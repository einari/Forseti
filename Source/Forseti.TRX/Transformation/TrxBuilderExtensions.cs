using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forseti.Harnesses;
using Forseti.Extensions;
using Forseti.Suites;

namespace Forseti.TRX.Transformation
{
    public static  class TrxBuilderExtensions
    {

        public static TrxBuilder BuildFrom(this TrxBuilder builder, IEnumerable<HarnessResult> results)
        {
            if (results.Count() == 0)
                throw new ArgumentOutOfRangeException("No test results found!");

            var successfullTests = results.Sum(suite => suite.SuccessfulCaseCount);
            var failingTests = results.Sum(suite => suite.FailedCaseCount);
            var startTime = results.Min(suite => suite.StartTime);
            var endTime = results.Max(suite => suite.EndTime);


            
            builder.SetRunInformation(Guid.NewGuid(), builder.TfsUsername, builder.ComputerName)
                      .SetDefaultTestSettingsWithDescription("Forseti Javascript  test Run")
                      .SetResultSummary(successfullTests, failingTests)
                      .SetRunTimes(startTime, endTime);



            foreach (var harnessResult in results)
            {
                harnessResult.AffectedSuites.ForEach(suite =>
                       suite.Descriptions.ForEach(description =>
                       {
                           description.Cases.ForEach(@case =>
                           {
                               if (CantResultBeReported(@case))
                                   return;

                               builder.AddTestResult(@case.Name,
                                                       Guid.NewGuid(),
                                                       builder.ComputerName,
                                                       @case.Result.Success ? UnitTestResult.ResultOutcome.Passed : UnitTestResult.ResultOutcome.Failed,
                                                       description.File.FullPath,
                                                       description.Name);
                           });

                       }));
            }

            return builder;
        }


        static bool CantResultBeReported(Case @case)
        {
            return String.IsNullOrEmpty(@case.Name);
        }
    }
}
