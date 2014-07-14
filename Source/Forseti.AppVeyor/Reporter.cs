using System;
using System.Collections.Generic;
using System.Linq;
using Forseti.Extensions;
using Forseti.Harnesses;
using Forseti.Reporting;
using Forseti.Suites;


namespace Forseti.AppVeyor
{
    public static class AppVeyorCaseResult
    {
        public const string Passed = "Passed";
        public const string Failed = "Failed";
        public const string Inconclusive = "Skipped";
    }

    public class Reporter : IReporter
    {
        IReportingOptions _options;
        IBuildWorkerApi _workerApi;

        public Reporter(IReportingOptions options)//, IBuildWorkerApi workerApi) //have to set this up.
        {
            _options = options;
            _workerApi = new BuildWorkerApi();
        }


        public void ReportOn(IEnumerable<HarnessResult> harnesses)
        {
            foreach (var harness in harnesses)
            {
                ReportOn(harness);
            }
        }

        public void ReportSummary(IEnumerable<HarnessResult> harnessResults)
        {
            //var enumeratedResults = harnessResults as IList<HarnessResult> ?? harnessResults.ToList();
            //var totalPassed = enumeratedResults.SuccessfulCaseCount();
            //var totalFailed = enumeratedResults.FailedCaseCount();
            //var totalInconclusive = enumeratedResults.InconclusiveCaseCount();
            //var totalCaseCount = totalPassed + totalFailed + totalInconclusive;
            //var totalTime = enumeratedResults.EndTime().Subtract( enumeratedResults.StartTime() );

            //Console.WriteLine("");
            //Console.WriteLine("======= Total Harness' Summary =======");
            //Console.ForegroundColor = ConsoleColor.Green;
            //Console.Write(" " + totalPassed);
            //Console.ResetColor();
            //Console.Write(" Passed, ");

            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.Write(totalFailed);
            //Console.ResetColor();
            //Console.Write("  Failed, ");

            //Console.ForegroundColor = ConsoleColor.Yellow;
            //Console.Write(totalInconclusive);
            //Console.ResetColor();
            //Console.Write("  Inconclusive. Total {0} tests run.\r\n", totalCaseCount);
            //Console.WriteLine("<--- Took {0} seconds --->", totalTime.TotalSeconds);
        }

        public void ReportOn(HarnessResult result)
        {
            var frameworkName = result.Harness.Framework.Name;
            var harnessResultsOverview = new List<CaseResult>();
            result.AffectedSuites.ForEach(
                suite => suite.Descriptions.ForEach(description =>
                {
                    if (description.HasExecutedCases())
                    {
                        description.Cases.Where(c => c.CanBeReportedOn()).ForEach(@case =>
                        {

                            if (@case.Result.Success)
                            {
                                _workerApi.AddTest(
                                    string.Format("{0}-{1}-{2}", description.Suite.FriendlyName(), description.FriendlyName(), @case.FriendlyName()),
                                    frameworkName,
                                    description.File.Filename,
                                    AppVeyorCaseResult.Passed,
                                    null,
                                    null,
                                    null,
                                    @case.Result.Message,
                                    null);

                            }
                            else
                            {
                                _workerApi.AddTest(
                                    string.Format("{0}-{1}-{2}", description.Suite.FriendlyName(), description.FriendlyName(), @case.FriendlyName()),
                                    frameworkName,
                                    description.File.Filename,
                                    AppVeyorCaseResult.Failed,
                                    null,
                                    null,
                                    null,
                                    @case.Result.Message,
                                    @case.Result.Message);
                            }
                        });
                    }
                    else
                    {
                        _workerApi.AddTest(
                                    string.Format("{0}-{1}", description.Suite.FriendlyName(), description.FriendlyName()),
                                    frameworkName,
                                    description.File.Filename,
                                    AppVeyorCaseResult.Failed,
                                    null,
                                    null,
                                    null,
                                    null,
                                    null);
                    }

                }));
        }
    }
}
