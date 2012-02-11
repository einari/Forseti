using System.Linq;
using Forseti.Files;
using Forseti.Harnesses;
using File = Forseti.Files.File;
using System.Yaml;

namespace Forseti.Configuration
{
    public class ConfigurationFileReader : IConfigurationFileReader
    {
        IFileSystemWatcher _fileSystemWatcher;
		IYamlParser _yamlParser;
		IHarnessManager _harnessManager;
		IFile _appliedConfigFile;

        public ConfigurationFileReader(
			IConfigure configure, 
			IFileSystemWatcher fileSystemWatcher, 
			IYamlParser yamlParser,
			IHarnessManager harnessManager)
        {
            _fileSystemWatcher = fileSystemWatcher;
			_yamlParser = yamlParser;
			_harnessManager = harnessManager;
			
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
			
			var nodes = _yamlParser.Parse (fileContent);
			var root = nodes.First () as YamlMapping;
			
			if( root != null && root.ContainsKey("Harnesses") ) 
			{
				var harnesses = root["Harnesses"] as YamlSequence;
				if( harnesses != null )
				{
					foreach( YamlMapping harnessConfig in harnesses ) 
					{
						if( harnessConfig.ContainsKey("Harness") )
						{
							var values = harnessConfig["Harness"] as YamlMapping;
							
							var harness = new Harness();
							harness.Name = ((YamlScalar)values["Name"]).Value;
							harness.SystemsSearchPath = ((YamlScalar)values["SystemsSearchPath"]).Value;
							harness.DescriptionsSearchPath = ((YamlScalar)values["DescriptionsSearchPath"]).Value;
							_harnessManager.Add (harness);
						}
					}
				}
			}
        }
    }
}
