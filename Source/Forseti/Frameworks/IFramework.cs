namespace Forseti.Frameworks
{
    public interface IFramework
    {
		string Name { get; }
        string ScriptName { get; }
        string Script { get; }
        string ExecuteScriptName { get; }
        string ExecuteScript { get; }
        string ReportScriptName { get; }
        string ReportScript { get; }
    }
}
