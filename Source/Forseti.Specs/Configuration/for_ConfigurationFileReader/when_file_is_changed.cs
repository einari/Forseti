using Forseti.Files;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Forseti.Specs.Configuration.for_ConfigurationFileReader
{
	public class when_file_is_changed : given.a_configuration_file_reader
	{
		static Mock<IFile>	file_mock;
		
		Establish context = () => {
			file_mock = new Mock<IFile>();
			reader.Apply(file_mock.Object);
		};
		
		Because of = () => FireFileChanged(FileChange.Modified, file_mock.Object);
		
		It should_reload_file = () => file_mock.Verify(f=>f.ReadAllText(), Times.Exactly(2));
	}
}

