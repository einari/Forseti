using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Forseti.TRX.Transformation
{
    public class TrxBuilder 
    {
        //public const string XMLNS = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010";
        public static readonly XNamespace XMLNS = "http://microsoft.com/schemas/VisualStudio/TeamTest/2010";

        public readonly TestRun TestRun;
        public readonly TestSettings RunSettings;
        public readonly ResultSummary Summary;
        public readonly Times Timing;
        public readonly TestLists TestLists;
        public readonly TestDefinitions Definitions;
        public readonly TestEntries TestEntries;
        public readonly Results Results;

   

        public string TfsUsername { get; set; }
        public string LocalUserName { get; set; }
        public string ComputerName { get; set; }

        public TrxBuilder()
        {
            TestRun = new TestRun();
            RunSettings = new TestSettings(); //TestSettings.WithDefaults
            Summary = new ResultSummary();
            Timing = new Times();
            TestLists = new TestLists();

            Definitions = new TestDefinitions();
            TestEntries = new TestEntries();
            Results = new Results();
        }

        public TrxBuilder(string machineName, string userName, string tfsUsername) : this()
        {
            ComputerName = machineName;
            LocalUserName = userName;
            TfsUsername = tfsUsername;
        }

        public TrxBuilder SetRunInformation(Guid id, string runUser, string computerName, string name = "")
        {
            TestRun.Id = id;
            TestRun.Name = name;
            TestRun.RunUser = runUser;
            TestRun.ComputerName = computerName;

            return this;
        }

        public TrxBuilder SetDefaultTestSettingsWithDescription(string description) 
        {
            RunSettings.Description = description;
            RunSettings.ExecutionAgentRule = TestSettings.DefaultExecutionAgentRule;
            RunSettings.RunDeploymentRoot = TestSettings.DefaultDeploymentRoot;
            return this;
        }

        public TrxBuilder SetResultSummary(int passed, int failed, int inconclusive) 
        {
            Summary.Passed = passed;
            Summary.Failed = failed;
            Summary.Inconclusive = inconclusive;

            return this;
        
        }

        public TrxBuilder SetRunTimes(DateTime start, DateTime end)
        {
            Timing.StartTime = start;
            Timing.EndTime = end;

            return this;
        }



        public TrxBuilder AddTestResult(string name, Guid id, string computerName, UnitTestResult.ResultOutcome outcome ,string testFilePath = @"C:\TestFilePath\SomeTest", string testClassName = @"SomeTest", string stackTrace = "", string errorMessage = "")
        {
            var testId = Guid.NewGuid();
            var executionId = Guid.NewGuid();

            Definitions.AddDefinition(testId, name, executionId, testFilePath, testClassName);

            TestEntries.AddEntry(testId, executionId);

            Results.AddResult(name, testId, executionId, computerName, outcome, errorMessage);
            
            return this;
        }

        public XDocument AsTrxDocument() 
        {
            var testRun = TestRun.TransformToTrx();
            testRun.Add( RunSettings.TransformToTrx(),
                         Timing.TransformToTrx(),
                         Summary.TransformToTrx(),
                         Definitions.TransformToTrx(),
                         TestLists.TransformToTrx(),
                         TestEntries.TransformToTrx(),
                         Results.TransformToTrx());

            var trx = new XDocument(testRun);

            return trx;
        }

    }
}
