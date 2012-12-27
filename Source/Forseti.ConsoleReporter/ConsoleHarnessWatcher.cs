using System;
using Forseti.Harnesses;
using Forseti.Extensions;
using Forseti.Suites;
using Forseti.Reporting;

namespace Forseti.ConsoleReporter
{
    public class ConsoleHarnessWatcher : IHarnessWatcher
    {
        public void HarnessChanged(HarnessChangeType changeType, HarnessResult result)
        {
            result.AffectedSuites.ForEach(
                suite => suite.Descriptions.ForEach( description => {
                        PrintSuiteInformation(description);
                        description.Cases.ForEach(@case =>
                                            {
                                                if (@case.CanBeReportedOn())
                                                {
                                                    if (@case.Result.Success)
                                                        PrintPassedCase(@case);
                                                    else
                                                        PrintFailedCase(@case);
                                                }
                                            });
                        Console.WriteLine("");

                }));

            PrintResultSummary(result);
        
        }

        static void PrintSuiteInformation(Description description)
        {
            Console.WriteLine("for( {0} ) ", description.Suite.FriendlyName());
            Console.WriteLine(" describing( {0} ) ", description.FriendlyName());
        }

        static void PrintResultSummary(HarnessResult result)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" "+ result.SuccessfulCaseCount);
            Console.ResetColor();
            Console.Write(" Passed, ");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(result.FailedCaseCount);
            Console.ResetColor();
            Console.Write("  Failed. Total {0} tests run.\n", result.TotalCaseCount);

            //Console.WriteLine("{0} Passed, {1} Failed. Ran in {2}s on the {3} framework/n",
            //    result.SuccessfulCaseCount,
            //    result.FailedCaseCount,
            //    result.TotalTime.TotalSeconds,
            //    result.Harness.Framework.Name);
        }


        void PrintPassedCase(Case @case)
        {
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
