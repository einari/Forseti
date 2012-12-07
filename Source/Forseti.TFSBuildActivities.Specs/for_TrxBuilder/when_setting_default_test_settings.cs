using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using System.Xml.Linq;
using Forseti.TFSBuildActivities.Trx;

namespace Forseti.TFSBuildActivities.Specs.for_TrxBuilder
{
    public class when_setting_timing_values : given.a_builder
    {
        static Times timing;
        static DateTime start, end;

        Establish context = () => 
                            {
                                start = DateTime.UtcNow;
                                end = DateTime.UtcNow.Add(new TimeSpan(0,1,0));
                            };

        Because of = () =>
                        {
                            builder.SetRunTimes(start, end);
                            timing = builder.Timing;
                        };

        It should_set_the_start_time = () => timing.StartTime.ShouldEqual(start);
        It should_set_the_end_time = () => timing.EndTime.ShouldEqual(end);
       
    }
} 
