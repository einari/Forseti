using System.Collections.Generic;

namespace Forseti.Harnesses
{
    public class HarnessManager : IHarnessManager
    {
        IScriptEngine _scriptEngine;
        IPageGenerator _pageGenerator;

        public HarnessManager(IScriptEngine scriptEngine, IPageGenerator pageGenerator)
        {
            _scriptEngine = scriptEngine;
            _pageGenerator = pageGenerator;
        }


        public Harness Execute(IEnumerable<Suite> suites)
        {
            var harness = new Harness();
            var cases = new List<Case>();

            foreach (var suite in suites)
                foreach (var description in suite.Descriptions)
                    cases.AddRange(description.Cases);

            harness.Cases = cases;

            var page = _pageGenerator.GenerateFrom(harness);
            _scriptEngine.Execute(page);

            return harness;
        }
    }
}
