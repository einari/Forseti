using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;
using Forseti.Files;

namespace Forseti.Specs.Configuration.for_ConfigurationFileReader
{
	public class when_applying_a_file : given.a_configuration_file_reader
	{
		static Mock<IFile>	file_mock;
		
		Establish context = () => file_mock = new Mock<IFile>();
		
		Because of = () => reader.Apply(file_mock.Object);
		
		It should_read_the_file = () => file_mock.Verify(f=>f.ReadAllText(), Times.Once ());
		It should_parse_the_yaml = () => yaml_parser_mock.Verify(y=>y.Parse(Moq.It.IsAny<string>()), Times.Once ());
	}
}

