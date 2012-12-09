using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forseti.TFSBuildActivities.Trx;
using Forseti.Harnesses;

namespace Forseti.TFSBuildActivities
{
    public static class Extensions
    {
        //public static TrxBuilder ConvertForTfs(this IEnumerable<HarnessResult> forsetiResults) 
        //{
        ////public void HarnessChanged(HarnessChangeType changeType, HarnessResult result)
        ////{
        ////    result.AffectedSuites.ForEach(
        ////        suite => suite.Descriptions.ForEach( description => {
        ////                PrintSuiteInformation(description);
        ////                description.Cases.ForEach(@case =>
        ////                                    {
        ////                                        if (CanBeResultBeReported(@case))
        ////                                            return;

        ////                                        if (@case.Result.Success)
        ////                                            PrintPassedCase(@case);
        ////                                        else
        ////                                            PrintFailedCase(@case);
        ////                                    });
        ////                Console.WriteLine("");

        ////        }));

        ////    PrintResultSummary(result);
        
        ////}

        //    var builder = new TrxBuilder();



        //}

    }
}
