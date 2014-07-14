using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Forseti.Output.MSTest.Transformation;
using Machine.Specifications;

namespace Forseti.Output.Specs.for_TrxBuilder.given
{
    public class a_builder
    {
        protected static TrxBuilder builder;

        Establish context = () => builder = new TrxBuilder();
    }
}
