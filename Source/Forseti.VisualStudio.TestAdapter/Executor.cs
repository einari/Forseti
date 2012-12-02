using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;

namespace Forseti.VisualStudio.TestAdapter
{
    public class Executor : ITestExecutor
    {
        public void Cancel()
        {
            var i = 0;
            i++;
        }

        public void RunTests(IEnumerable<string> sources, IRunContext runContext, IFrameworkHandle frameworkHandle)
        {
            var i = 0;
            i++;
        }

        public void RunTests(IEnumerable<Microsoft.VisualStudio.TestPlatform.ObjectModel.TestCase> tests, IRunContext runContext, IFrameworkHandle frameworkHandle)
        {
            var i = 0;
            i++;
        }
    }
}
