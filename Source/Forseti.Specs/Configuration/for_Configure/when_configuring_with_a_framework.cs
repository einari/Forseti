using Forseti.Configuration;
using Machine.Specifications;
using Ninject;
using Moq;
using It = Machine.Specifications.It;

namespace Forseti.Specs.Configuration.for_Configure
{
    public class when_configuring_with_a_framework
    {
        static Mock<IKernel>    kernel_mock;
        static FakeFramework    expected;
        static IConfigure configure;

        Establish context = () => {
            expected = new FakeFramework();
            kernel_mock = new Mock<IKernel>();
            kernel_mock.Setup(k=>k.Get<FakeFramework>()).Returns(expected);
        };


        Because of = () => configure = Configure.With<FakeFramework>(kernel_mock.Object);

        It should_set_the_framework_property = () => configure.Framework.ShouldEqual(expected);
    }
}
