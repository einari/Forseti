using Forseti.Configuration;

namespace Forseti.Buster
{
    public static class ConfigureExtensions
    {
        public static IConfigure UsingBuster(this IConfigure configure)
        {
            configure.Container.Configure(
                c => 
                    c   .For<IFramework>()
                        .Singleton()
                        .Use<Buster.Framework>());
            return configure;
        }
    }
}

