namespace Forseti.Configuration
{
    public interface IConfigure
    {
        IRunner Runner { get; }
        IFramework Framework { get; }
    }
}
