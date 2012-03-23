namespace Forseti.Configuration
{
    public static class ConfigureExtensions
    {
        public static IConfigure UsingQUnit(this IConfigure configure)
        {
            configure.Container.Configure(
                c => 
                    c   .For<IFramework>()
                        .Singleton()
                        .Use<QUnit.Framework>());
            return configure;
        }
    }
}
