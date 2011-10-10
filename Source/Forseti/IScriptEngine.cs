namespace Forseti
{
    public interface IScriptEngine
    {
        void Execute(IFramework framework, Execution execution);
    }
}
