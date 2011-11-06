namespace Forseti
{
    public interface IFramework
    {
        string ScriptName { get; }
        string Script { get; }
        string ExecuteScriptName { get; }
        string ExecuteScript { get; }
        string ReportScriptName { get; }
        string ReportScript { get; }
    }
}
