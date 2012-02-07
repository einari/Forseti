using Forseti.Files;
using Forseti.Harnesses;
using File = Forseti.Files.File;

namespace Forseti.Configuration
{
    public class ConfigurationFileReader : IConfigurationFileReader
    {
        IFileSystemWatcher _fileSystemWatcher;
		IYamlParser _yamlParser;
		IFile _appliedConfigFile;

        public ConfigurationFileReader(
			IConfigure configure, 
			IFileSystemWatcher fileSystemWatcher, 
			IYamlParser yamlParser,
			IHarnessManager harnessManager)
        {
            _fileSystemWatcher = fileSystemWatcher;
			_yamlParser = yamlParser;
			
			fileSystemWatcher.SubscribeToChanges(FileChanged);
        }
		
		void FileChanged(FileChange change, IFile file)
		{
			if(_appliedConfigFile == null || 
			   _appliedConfigFile != file)
				return;
			
			Apply (file);
		}
		

        public void Apply(IFile file)
        {
			_appliedConfigFile = file;
			var fileContent = file.ReadAllText();
			var yamlDocument = _yamlParser.Parse (fileContent);
			
        }
    }
}
