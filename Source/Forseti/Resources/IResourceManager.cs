using System.Reflection;

namespace Forseti.Resources
{
    public interface IResourceManager
    {
        string GetStringFromAssemblyOf<T>(string resourceName);
        string GetString(Assembly assembly, string resourceName);
    }
}
