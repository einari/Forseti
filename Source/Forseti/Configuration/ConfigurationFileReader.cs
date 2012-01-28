using YamlDotNet.RepresentationModel;
using Forseti.Files;
using System.IO;
using File = Forseti.Files.File;

namespace Forseti.Configuration
{
    public class ConfigurationFileReader : IConfigurationFileReader
    {
        IFileSystemWatcher _fileSystemWatcher;

        public ConfigurationFileReader(IFileSystemWatcher fileSystemWatcher)
        {
            _fileSystemWatcher = fileSystemWatcher;
        }

        public void Apply(IConfigure configure, File file)
        {
            var stream = new StringReader(file.ReadAllText());
            var yaml = new YamlStream();
            yaml.Load(stream);
        }
    }
}
