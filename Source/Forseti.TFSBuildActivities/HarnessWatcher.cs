using System;
using Forseti.Extensions;
using Forseti.Harnesses;
using Forseti.Suites;

namespace Forseti.TFSBuildActivities
{
    public class HarnessWatcher : IHarnessWatcher
    {
        public void HarnessChanged(HarnessChangeType changeType, HarnessResult harness)
        {
            harness.AffectedSuites.ForEach(
                suite => suite.Descriptions.ForEach(description =>
                {
                    description.Cases.ForEach(@case =>
                    {
                        if (CanResultBeReported(@case))
                            return;

                        //if (@case.Result.Success)
                            //PrintPassedCase(@case);
                        //else
                            //PrintFailedCase(@case);
                    });
                }));

        }

        static bool CanResultBeReported(Case @case)
        {
            return String.IsNullOrEmpty(@case.Name);
        }

    }
}
