using System.Collections.Generic;
using Forseti.Configuration;
using Forseti.Files;
using Machine.Specifications;
using Moq;

namespace Forseti.Specs.Configuration.for_ConfigurationFileReader.given
{
	public class a_configuration_file_reader
	{
		static List<FileChanged>	file_changed_delegates = new List<FileChanged>();
		
		protected static ConfigurationFileReader reader;
		protected static Mock<IFileSystemWatcher> file_system_watcher_mock;
		protected static Mock<IYamlParser> yaml_parser;
		protected static Mock<IConfigure>	configure_mock;
		
		Establish context = () => 
		{ 
			file_system_watcher_mock = new Mock<IFileSystemWatcher>();
			
			file_system_watcher_mock.Setup (f=>f.SubscribeToChanges(Moq.It.IsAny<FileChanged>())).Callback ((FileChanged f)=>file_changed_delegates.Add(f));
			configure_mock = new Mock<IConfigure>();
			
			yaml_parser = new Mock<IYamlParser>();
			reader = new ConfigurationFileReader(
				configure_mock.Object,
				file_system_watcher_mock.Object,
				yaml_parser.Object
			);
		};
		
		protected static void FireFileChanged(FileChange change, IFile file)
		{
			foreach( var file_changed_delegate in file_changed_delegates ) 
				file_changed_delegate(change, file);
		}
	}
}

