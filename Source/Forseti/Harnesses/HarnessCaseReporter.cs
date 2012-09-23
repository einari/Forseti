using System;
using System.Linq;
using Forseti.Extensions;
using Forseti.Scripting;
using Forseti.Suites;

namespace Forseti.Harnesses
{
    public class HarnessCaseReporter
    {
        public static HarnessResult HarnessResult;

        public static void ReportFailedCase(string description, string @case, string message)
        {
            if (!SystemConsole.LoggingEnabled)
                return;

            Report(description, @case, false, message);
        }

        public static void ReportPassedCase(string description, string @case)
        {
            if (!SystemConsole.LoggingEnabled)
                return;

            Report(description, @case);
        }


        static void Report(string description, string @case, bool success = true, string message = "")
        {
            if (HarnessResult != null)
            {
                HarnessResult.AffectedSuites.ForEach(s =>
                {
                    s.Descriptions.ForEach(d =>
                    {
                        var descriptionWithUnderscores = description.Replace(' ', '_');

                        if (d.Name == description || d.Name == descriptionWithUnderscores)
                        {
                            var actualCase = d.Cases.Where(c => c.Name == @case).FirstOrDefault();
                            if (actualCase == null)
                            {
                                actualCase = new Case
                                {
                                    Name = @case
                                };
                                d.AddCase(actualCase);
                            }
                            actualCase.Result = new CaseResult
                            {
                                Success = success,
                                Message = message
                            };
                        }
                    });
                });

            }
        }
    }
}
