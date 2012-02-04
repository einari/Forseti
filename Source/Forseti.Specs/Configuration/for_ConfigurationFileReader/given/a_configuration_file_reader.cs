using System;
using Forseti.Configuration;
using Forseti.Files;
using Machine.Specifications;
using Moq;

namespace Forseti.Specs.Configuration.for_ConfigurationFileReader.given
{
	public class a_configuration_file_reader
	{
		protected static ConfigurationFileReader reader;
		protected static Mock<IFileSystemWatcher> file_system_watcher_mock;
		protected static Mock<IYamlParser> yaml_parser;
			
		
		Establish context = () => 
		{ 
			file_system_watcher_mock = new Mock<IFileSystemWatcher>();
			yaml_parser = new Mock<IYamlParser>();
			reader = new ConfigurationFileReader(
				file_system_watcher_mock.Object,
				yaml_parser.Object
			);
		};
		
	}
}

