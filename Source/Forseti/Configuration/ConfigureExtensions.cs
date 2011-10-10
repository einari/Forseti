using System;

namespace Forseti.Configuration
{
    public static class ConfigureExtensions
    {
        public static IConfigure Systems(this IConfigure configure, Action<PathConfiguration> pathConfiguration)
        {
            pathConfiguration(configure.SystemPaths);
            return configure;
        }

        public static IConfigure Cases(this IConfigure configure, Action<PathConfiguration> pathConfiguration)
        {
            pathConfiguration(configure.CasePaths);
            return configure;
        }
    }
}
