namespace Forseti.Configuration
{
    public static class ConfigureExtensions
    {
        public static IConfigure UsingJasmin(this IConfigure configure)
        {
            configure.Kernel.Bind<IFramework>().To<Jasmine.Framework>();
            return configure;
        }
    }
}
