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
            var execution = new Harness
            {
                Suites = suites
            };
            return execution;
        }
    }
}
