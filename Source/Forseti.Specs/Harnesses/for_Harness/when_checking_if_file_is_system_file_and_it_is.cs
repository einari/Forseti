using Forseti.Harnesses;
using Machine.Specifications;

namespace Forseti.Specs.Harnesses.for_Harness
{
	public class when_checking_if_file_is_system_file_and_it_is
	{
		static Harness	harness;
        static bool result;
		
		Establish context = () => 
		{
            harness = new Harness
            {
                SystemsSearchPath = "Scripts/{system}.js"
            };
		};

        Because of = () => result = harness.IsSystemFile("Scripts/something.js");

        It should_acknowledge_file_as_a_system_file = () => result.ShouldBeTrue();
	}
}

