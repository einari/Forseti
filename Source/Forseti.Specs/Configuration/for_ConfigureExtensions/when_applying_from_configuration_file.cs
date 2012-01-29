using Forseti.Configuration;
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;
using Forseti.Files;

namespace Forseti.Specs.Configuration.for_ConfigureExtensions
{
    [Subject(typeof(ConfigureExtensions))]
    public class when_applying_from_configuration_file
    {
        const string Filename = "configFile.cfg";
        static Mock<IConfigure> configure_mock;
        static Mock<IConfigurationFileReader> reader_mock;

        Establish context = () =>
        {
            reader_mock = new Mock<IConfigurationFileReader>();
            configure_mock = new Mock<IConfigure>();
            configure_mock.Setup(c => c.GetInstanceOf<IConfigurationFileReader>()).Returns(reader_mock.Object);
        };

        Because of = () => configure_mock.Object.FromConfigurationFile(Filename);

        It should_apply_configuration_from_file = () => reader_mock.Verify(r=>r.Apply(configure_mock.Object, Moq.It.IsAny<File>()));
    }
}
