using Forseti.Files;

namespace Forseti.Configuration
{
    public interface IConfigurationFileReader
    {
        void Apply(IConfigure configure, IFile file);
    }
}
