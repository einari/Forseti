using Forseti.Configuration;
using Moq;
using Machine.Specifications;

namespace Forseti.Specs.Configuration.for_ConfigurationFileReader.given
{
	public class a_configuration_file_reader_and_a_configure_instance : a_configuration_file_reader
	{
		protected static Mock<IConfigure>	configure_mock;
		
		Establish context = () => configure_mock = new Mock<IConfigure>();
	}
}

