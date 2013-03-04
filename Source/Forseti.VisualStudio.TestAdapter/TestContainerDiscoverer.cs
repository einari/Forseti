using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.TestWindow.Extensibility;
using Microsoft.VisualStudio.Shell;


namespace Forseti.VisualStudio.TestAdapter
{
    public class TestContainerDiscoverer : ITestContainerDiscoverer
    {
        public event EventHandler TestContainersUpdated;
        public Uri ExecutorUri { get; private set; }
        public IEnumerable<ITestContainer> TestContainers { get; private set; }

        [ImportingConstructor]
        public TestContainerDiscoverer(
            [Import(typeof(SVsServiceProvider))] IServiceProvider serviceProvider,
            ILogger logger)
        {
            ExecutorUri = new Uri("executor://forseti");
        }
    }
}
