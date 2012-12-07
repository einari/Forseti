using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forseti.TFSBuildActivities.Trx;
using Machine.Specifications;

namespace Forseti.TFSBuildActivities.Specs.for_TrxBuilder.given
{
    public class a_builder
    {
        protected static TrxBuilder builder;

        Establish context = () => builder = new TrxBuilder();
    }
}
