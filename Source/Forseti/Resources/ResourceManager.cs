using System.IO;
using System.Reflection;

namespace Forseti.Resources
{
    public class ResourceManager : IResourceManager
    {
        public string GetStringFromAssemblyOf<T>(string resourceName)
        {
            return GetString(typeof(T).Assembly, resourceName);
        }

        public string GetString(Assembly assembly, string resourceName)
        {
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
