using System;
namespace Forseti.AppVeyor
{
    public interface IBuildWorkerApi
    {
        void AddTest(string testName, string testFramework, string fileName, string outcome, long? durationMilliseconds, string errorMessage, string errorStackTrace, string stdOut, string stdErr);
        void UpdateTest(string testName, string testFramework, string fileName, string outcome, long? durationMilliseconds, string errorMessage, string errorStackTrace, string stdOut, string stdErr);
    }
}
