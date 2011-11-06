using System;
using Spark;

namespace Forseti.Spark
{
    public class HarnessView : AbstractSparkView
    {
        public Harness Harness { get; set; }

        public override Guid GeneratedViewId { get { return Guid.NewGuid(); } }
        public override void Render()
        {
        }
    }
}
