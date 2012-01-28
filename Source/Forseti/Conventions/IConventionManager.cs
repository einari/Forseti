
namespace Forseti.Conventions
{
    public interface IConventionManager
    {
        bool IsSystemFileSatisfied(string file);
        bool IsDescriptionFileSatisfied(string file, out string systemDefinition);
    }
}
