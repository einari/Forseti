using System;
using Ninject;

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

        public static IConfigure FromConfigurationFile(this IConfigure configure, string filename)
        {
            var reader = configure.GetInstanceOf<IConfigurationFileReader>();
            reader.Apply(configure, filename);
            return configure;
        }
    }
}
