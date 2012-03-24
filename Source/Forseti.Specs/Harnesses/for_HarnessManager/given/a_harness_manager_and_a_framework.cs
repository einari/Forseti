using Moq;
using Machine.Specifications;
using Forseti.Frameworks;

namespace Forseti.Specs.Harnesses.for_HarnessManager.given
{
	public class a_harness_manager_and_a_framework : a_harness_manager
	{
		protected static Mock<IFramework>	framework_mock;
		
		Establish context = () => framework_mock = new Mock<IFramework>();
	}
}

