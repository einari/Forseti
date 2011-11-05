using System.Collections.Generic;

namespace Forseti.Harnesses
{
    public class HarnessManager : IHarnessManager
    {
        IScriptEngine _scriptEngine;

        public HarnessManager(IScriptEngine scriptEngine)
        {
            _scriptEngine = scriptEngine;
        }


        public Harness Execute(IEnumerable<Suite> suites)
        {
            var harness = new Harness();
            var cases = new List<Case>();

            foreach (var suite in suites)
                foreach (var description in suite.Descriptions)
                    cases.AddRange(description.Cases);

            harness.Cases = cases;

            _scriptEngine.Execute(harness);

            return harness;
        }
    }
}
