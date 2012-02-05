using Forseti.Files;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Forseti.Specs.Configuration.for_ConfigurationFileReader
{
	public class when_file_changed_but_apply_has_never_been_called_before : given.a_configuration_file_reader
	{
		static Mock<IFile>	file_mock;
		
		Establish context = () => file_mock = new Mock<IFile>();
		
		Because of = () => FireFileChanged(FileChange.Modified, file_mock.Object);
		
		It should_not_reload_file = () => file_mock.Verify(f=>f.ReadAllText(), Times.Never());
	}
}

