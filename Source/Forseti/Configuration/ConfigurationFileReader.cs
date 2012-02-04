using YamlDotNet.RepresentationModel;
using Forseti.Files;
using System.IO;
using File = Forseti.Files.File;

namespace Forseti.Configuration
{
    public class ConfigurationFileReader : IConfigurationFileReader
    {
        IFileSystemWatcher _fileSystemWatcher;
		IYamlParser _yamlParser;

        public ConfigurationFileReader(IFileSystemWatcher fileSystemWatcher, IYamlParser yamlParser)
        {
            _fileSystemWatcher = fileSystemWatcher;
			_yamlParser = yamlParser;
        }

        public void Apply(IConfigure configure, IFile file)
        {
			var fileContent = file.ReadAllText();
			var yamlDocument = _yamlParser.Parse (fileContent);
        }
    }
}
