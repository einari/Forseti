using System;
using Forseti.Files;

namespace Forseti.Configuration
{
    public static class ConfigureExtensions
    {
        public static IConfigure FromConfigurationFile(this IConfigure configure, string path)
        {
            var reader = configure.GetInstanceOf<IConfigurationFileReader>();
            reader.Apply(configure, (File)path);
            return configure;
        }
    }
}
