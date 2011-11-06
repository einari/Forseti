using Forseti.Spark;
using Ninject.Modules;

namespace Forseti.Modules
{
    public class SparkModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPageGenerator>().To<PageGenerator>();
        }
    }
}
