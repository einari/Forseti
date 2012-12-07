using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Forseti.TFSBuildActivities.Trx
{
    public class TrxBuilder
    {

        public TestRun TestRun { get; private set; }
        public TestSettings RunSettings { get; private set; }
        public ResultSummary Summary { get; private set; }
        public Times Timing { get; private set; }
        public IDictionary<string, Guid> TestLists { get; private set; }
        public IList<TestResult> Results { get; private set; }


        public TrxBuilder()
        {
            TestRun = new TestRun();
            RunSettings = new TestSettings(); //TestSettings.WithDefaults
            Summary = new ResultSummary();
            Timing = new Times();
            TestLists = new Dictionary<string, Guid>();
            Results = new List<TestResult>();
        }


        public TrxBuilder SetRunInformation(Guid id, string name, string runUser)
        {
            TestRun.Id = id;
            TestRun.Name = name;
            TestRun.RunUser = runUser;

            return this;
        }

        public TrxBuilder SetDefaultTestSettingsWithDescription(string description) 
        {
            RunSettings.Description = description;
            RunSettings.ExecutionAgentRule = TestSettings.DefaultExecutionAgentRule;
            RunSettings.RunDeploymentRoot = TestSettings.DefaultDeploymentRoot;
            return this;
        }

        public TrxBuilder SetResultSummary(int passed, int failed) 
        {
            Summary.Passed = passed;
            Summary.Failed = failed;

            return this;
        
        }

        public TrxBuilder SetRunTimes(DateTime start, DateTime end)
        {
            Timing.StartTime = start;
            Timing.EndTime = end;

            return this;
        }



        public TrxBuilder AddTestResult(string name, string id, string computerName, string testListName = "Default")
        {
            var testResult = new TestResult 
                                    { 
                                        Name = name, 
                                        Id = id,
                                        ComputerName = computerName,
                                        Type = Guid.NewGuid(),
                                        ListId = GetOrCreateListTypeByName(testListName)
                                    };
            Results.Add(testResult);

            return this;
        }

        private Guid GetOrCreateListTypeByName(string testListName)
        {
            if (TestLists.ContainsKey(testListName))
                return TestLists[testListName];

            var testListId = Guid.NewGuid();

            TestLists.Add(testListName, testListId);
            return testListId;
        }
    }
}
