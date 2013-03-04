using System;
using System.Linq;
using System.Text.RegularExpressions;
using Forseti.Extensions;
using Forseti.Scripting;
using Forseti.Suites;

namespace Forseti.Harnesses
{
    public class HarnessCaseReporter
    {
        public static HarnessResult HarnessResult;

        public static void ReportFailedCase(string description, string @case, string message, string filename)
        {
            Report(filename, description, @case, false, message:message);
        }

        public static void ReportPassedCase(string description, string @case, string filename)
        {
            Report(filename, description, @case);
        }


        static string ParseMessage(string message)
        {
            var regex = new Regex("\\(line ([0-9]*)\\)");
            var match = regex.Match(message);
            message = regex.Replace(message, m =>
            {
                return string.Format("(line {0})", Int32.Parse(m.Groups[1].Value)+1);
            });
            return message;
        }

        static void Report(string filename, string description, string @case, bool success = true, string message = "" )
        {
            message = ParseMessage(message);
            if (HarnessResult != null)
            {
                HarnessResult.AffectedSuites.ForEach(s =>
                {
                    s.Descriptions.ForEach(d =>
                    {
                        //var descriptionWithUnderscores = description.Replace(' ', '_');
                        if (d.File.RelativePath == filename)
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
