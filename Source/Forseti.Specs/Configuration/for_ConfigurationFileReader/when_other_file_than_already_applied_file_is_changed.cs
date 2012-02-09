using Forseti.Files;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;

namespace Forseti.Specs.Configuration.for_ConfigurationFileReader
{
	public class when_other_file_than_already_applied_file_is_changed : given.a_configuration_file_reader
	{
		static Mock<IFile>	applied_file_mock;
		static Mock<IFile>	modified_file_mock;
		
		Establish context = () => {
			applied_file_mock = new Mock<IFile>();
			modified_file_mock = new Mock<IFile>();
			reader.Apply(applied_file_mock.Object);
		};
		
		Because of = () => FireFileChanged(FileChange.Modified, modified_file_mock.Object);
		
		It should_not_reload_file = () => modified_file_mock.Verify(f=>f.ReadAllText(), Times.Never());
	}
}

