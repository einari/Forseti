namespace Forseti
{
    public interface IScriptEngineContext
    {
        void EvaluateString(string script, string source);
        void EvaluateFile(string file);
    }
}
