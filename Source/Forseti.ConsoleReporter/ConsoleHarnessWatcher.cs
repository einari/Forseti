using System;
using System.Linq;
using Forseti.Harnesses;
using Forseti.Extensions;
using Forseti.Suites;
using Forseti.Reporting;
using System.Collections.Generic;

namespace Forseti.ConsoleReporter
{
    public enum CaseResult 
    {
        Passed = 0,
        Failed = 1,
        Inconclusive = 2
    }

    public class ConsoleHarnessWatcher : IHarnessWatcher
    {


        List<CaseResult> _resultsOverview;
        IReportingOptions _options;

        public ConsoleHarnessWatcher(IReportingOptions options) 
        {
            _options = options;
            _resultsOverview = new List<CaseResult>();
        }

        public void HarnessChanged(HarnessChangeType changeType, HarnessResult result)
        {
            _resultsOverview.Clear();
            result.AffectedSuites.ForEach(
                suite => suite.Descriptions.ForEach( description => {
                    PrintSuiteInformation(description);

                        if (description.HasExecutedCases())
                        {
                            description.Cases.Where(c => c.CanBeReportedOn()).ForEach(@case =>
                                                {
                                                    if (@case.Result.Success)
                                                        PrintPassedCase(@case);
                                                    else
                                                        PrintFailedCase(@case);
                                                });
                        }
                        else
                        {
                            PrintDescriptionWithoutExecutedCases(description);
                        }

                }));

            PrintResultsOverview();
            PrintResultSummary(result);
        
        }

        private void PrintResultsOverview()
        {
            Console.WriteLine("");
            for (int i = 0; i < _resultsOverview.Count; i++)
            {
                if (i == 0)
                    Console.Write("[");

                var result = _resultsOverview[i];
                PrintCaseResultForArray(result);

                if (i == _resultsOverview.Count - 1)
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
            _resultsOverview.Add(CaseResult.Inconclusive);

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
            Console.Write(" "+ result.SuccessfulCaseCount);
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

            //Console.WriteLine("{0} Passed, {1} Failed. Ran in {2}s on the {3} framework/n",
            //    result.SuccessfulCaseCount,
            //    result.FailedCaseCount,
            //    result.TotalTime.TotalSeconds,
            //    result.Harness.Framework.Name);
        }


        void PrintPassedCase(Case @case)
        {
            _resultsOverview.Add(CaseResult.Passed);
            if (_options.OnlyOutputFailed)
                return;

            Console.Write("  it( {0} ) ", @case.FriendlyName());
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("PASSED");
            Console.ResetColor();
        }
        void PrintFailedCase(Case @case)
        {
            _resultsOverview.Add(CaseResult.Failed);

            Console.Write("  it( {0} ) ", @case.FriendlyName());
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("FAILED");
            Console.ResetColor();
            Console.WriteLine(" with message : {0}", @case.Result.Message);
        }
    }
}
