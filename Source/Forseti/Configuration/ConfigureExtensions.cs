using System;

namespace Forseti.Configuration
{
    public static class ConfigureExtensions
    {
        public static IConfigure Sources(this IConfigure configure, Action<PathConfiguration> pathConfiguration)
        {
            pathConfiguration(configure.SourcePaths);
            return configure;
        }

        public static IConfigure Specifications(this IConfigure configure, Action<PathConfiguration> pathConfiguration)
        {
            pathConfiguration(configure.SpecificationPaths);
            return configure;
        }
    }
}
