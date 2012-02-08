using System.Linq;
using Forseti.Files;
using Forseti.Harnesses;
using File = Forseti.Files.File;

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
			var yamlDocument = _yamlParser.Parse (fileContent);
			foreach( var node in yamlDocument.First().AllNodes ) 
			{
				if( node.Tag == "Harnesses" ) 
				{
					foreach( var childNode in node.AllNodes )
					{
						if( childNode.Tag == "Harness") 
						{
							foreach( var parameter in childNode.AllNodes )
							{
								switch( parameter.Tag ) 
								{
								case "Name":
									{
										
										var i=0;
										i++;
										
									}break;
								}
								
							}
							
						}
					}
				}
				
			}
			
			var harness = new Harness();
			_harnessManager.Add (harness);
        }
    }
}
