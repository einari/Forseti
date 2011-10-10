namespace Forseti.Configuration
{
    public interface IConfigurationFileReader
    {
        void Apply(IConfigure configure, string filename);
    }
}
