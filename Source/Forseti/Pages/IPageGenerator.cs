using Forseti.Harnesses;
namespace Forseti.Pages
{
    public interface IPageGenerator
    {
        Page GenerateFrom(Harness harness);
    }
}
