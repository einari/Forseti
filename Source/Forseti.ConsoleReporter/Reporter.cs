using System;
using System.Collections.Generic;
using System.Linq;
using Forseti.Extensions;
using Forseti.Harnesses;
using Forseti.Reporting;
using Forseti.Suites;


namespace Forseti.ConsoleReporter
{
    public enum CaseResult
    {
        Passed = 0,
        Failed = 1,
        Inconclusive = 2
    }

    public class Reporter : IReporter
    {
        IReportingOptions _options;

        public Reporter(IReportingOptions options) 
        {
            _options = options;
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
            var enumeratedResults = harnessResults as IList<HarnessResult> ?? harnessResults.ToList();
            var totalPassed = enumeratedResults.SuccessfulCaseCount();
            var totalFailed = enumeratedResults.FailedCaseCount();
            var totalInconclusive = enumeratedResults.InconclusiveCaseCount();
            var totalCaseCount = totalPassed + totalFailed + totalInconclusive;
            var totalTime = enumeratedResults.EndTime().Subtract( enumeratedResults.StartTime() );

            Console.WriteLine("");
            Console.WriteLine("======= Total Harness' Summary =======");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" " + totalPassed);
            Console.ResetColor();
            Console.Write(" Passed, ");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(totalFailed);
            Console.ResetColor();
            Console.Write("  Failed, ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(totalInconclusive);
            Console.ResetColor();
            Console.Write("  Inconclusive. Total {0} tests run.\r\n", totalCaseCount);
            Console.WriteLine("<--- Took {0} seconds --->", totalTime.TotalSeconds);
        }

        public void ReportOn(HarnessResult result)
        {
            var harnessResultsOverview = new List<CaseResult>();
            result.AffectedSuites.ForEach(
                suite => suite.Descriptions.ForEach(description =>
                {
                    PrintSuiteInformation(description);

                    if (description.HasExecutedCases())
                    {
                        description.Cases.Where(c => c.CanBeReportedOn()).ForEach(@case =>
                        {
                            if (@case.Result.Success)
                            {
                                PrintPassedCase(@case);
                                harnessResultsOverview.Add(CaseResult.Passed);
                            }
                            else
                            {
                                PrintFailedCase(@case);
                                harnessResultsOverview.Add(CaseResult.Failed);
                            }
                        });
                    }
                    else
                    {
                        PrintDescriptionWithoutExecutedCases(description);
                        harnessResultsOverview.Add(CaseResult.Inconclusive);
                    }

                }));

            PrintResultsOverview(harnessResultsOverview);
            PrintResultSummary(result);
        }

        private void PrintResultsOverview(IList<CaseResult> caseResults )
        {
            Console.WriteLine("");
            for (int i = 0; i < caseResults.Count(); i++)
            {
                if (i == 0)
                    Console.Write("[");

                var result = caseResults[i];
                PrintCaseResultForArray(result);

                if (i == caseResults.Count() - 1)
                {
                    Console.Write("]");
                }
            }
        }

        private static void PrintCaseResultForArray(CaseResult result)
        {
            var resultOutput = "-";
            switch (result)
            {
                case CaseResult.Passed:
                    Console.ForegroundColor = ConsoleColor.Green;
                    resultOutput = ".";
                    break;
                case CaseResult.Failed:
                    Console.ForegroundColor = ConsoleColor.Red;
                    resultOutput = "x";
                    break;
                case CaseResult.Inconclusive:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    resultOutput = "-";
                    break;
                default:
                    break;
            }
            Console.Write(resultOutput);
            Console.ResetColor();
        }

        private void PrintDescriptionWithoutExecutedCases(Description description)
        {
            if (_options.OnlyOutputFailed)
                return;

            Console.WriteLine(" no cases executed for description ");
        }

        void PrintSuiteInformation(Description description)
        {
            var descriptionHasFailingCases = description.Cases.Any(c => !Case.IsDummyOrEmptyCase(c) && c.Result.Success == false);

            if (_options.OnlyOutputFailed && !descriptionHasFailingCases)
                return;

            Console.WriteLine("");
            Console.WriteLine("for( {0} ) ", description.Suite.FriendlyName());
            Console.WriteLine(" describing( {0} ) ", description.FriendlyName());
        }

        void PrintResultSummary(HarnessResult result)
        {
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" " + result.SuccessfulCaseCount);
            Console.ResetColor();
            Console.Write(" Passed(.), ");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(result.FailedCaseCount);
            Console.ResetColor();
            Console.Write("  Failed(x), ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(result.InconclusiveCaseCount);
            Console.ResetColor();
            Console.Write("  Inconclusive(-). Total {0} tests run.\r\n", result.TotalCaseCount);
        }


        void PrintPassedCase(Case @case)
        {
            if (_options.OnlyOutputFailed)
                return;

            Console.Write("  it( {0} ) ", @case.FriendlyName());
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("PASSED");
            Console.ResetColor();
        }
        void PrintFailedCase(Case @case)
        {
            Console.Write("  it( {0} ) ", @case.FriendlyName());
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("FAILED");
            Console.ResetColor();
            Console.WriteLine(" with message : {0}", @case.Result.Message);
        }
    }
}
