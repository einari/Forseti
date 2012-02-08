using Forseti.Configuration;
using Forseti.Files;
using Machine.Specifications;
using Moq;
using It=Machine.Specifications.It;
using YamlDotNet.RepresentationModel;

namespace Forseti.Specs.Configuration.for_ConfigurationFileReader
{
	public class when_applying_a_file_with_a_harness_in_it : given.a_configuration_file_reader
	{
		const string harness_name = "Something";
		const string systems_search_path = "Scripts";
		const string descriptions_search_path = "Specs/for_{system}";
		
		static YamlDocument	config_document;
		static Mock<IFile>	file_mock;
		
		static Harness result;
		
		Establish context = () => {
			file_mock = new Mock<IFile>();
			
			config_document = new YamlDocument(
"Harnesses:\n"+
"  - Harness:\n"+
"    - Name						: \""+harness_name+"\"\n"+
"      SystemsSearchPath 		: \""+systems_search_path+"\"\n"+
"      DescriptionsSearchPath	: \""+descriptions_search_path+"\"");
			
			yaml_parser_mock.Setup(y=>y.Parse(Moq.It.IsAny<string>())).Returns(new[] { config_document });
			harness_manager_mock.Setup(h=>h.Add(Moq.It.IsAny<Harness>())).Callback((Harness h)=>result = h);
		};
		
		Because of = () => reader.Apply(file_mock.Object);
		
		It should_add_a_harness = () => result.ShouldNotBeNull();
		It should_add_a_harness_with_expected_name = () => result.Name.ShouldEqual(harness_name);
	}
}

