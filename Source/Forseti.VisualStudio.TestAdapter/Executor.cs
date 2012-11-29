using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;

namespace Forseti.VisualStudio.TestAdapter
{
    public class Executor : ITestExecutor
    {
        public void Cancel()
        {
        }

        public void RunTests(IEnumerable<string> sources, IRunContext runContext, IFrameworkHandle frameworkHandle)
        {
        }

        public void RunTests(IEnumerable<Microsoft.VisualStudio.TestPlatform.ObjectModel.TestCase> tests, IRunContext runContext, IFrameworkHandle frameworkHandle)
        {
        }
    }
}
