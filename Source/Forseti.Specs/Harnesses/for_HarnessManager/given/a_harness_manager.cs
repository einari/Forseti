using Forseti.Files;
using Forseti.Harnesses;
using Forseti.Pages;
using Forseti.Scripting;
using Machine.Specifications;
using Moq;

namespace Forseti.Specs.for_HarnessManager.given
{
    public class a_harness_manager
    {
        protected static Mock<IScriptEngine> script_engine_mock;
        protected static Mock<IPageGenerator> page_generator_mock;
		protected static Mock<IFileSystem> file_system_mock;
		protected static Mock<IFileSystemWatcher> file_system_watcher_mock;
        protected static HarnessManager harness_manager;

        Establish context = () =>
        {
            script_engine_mock = new Mock<IScriptEngine>();
            page_generator_mock = new Mock<IPageGenerator>();
			file_system_mock = new Mock<IFileSystem>();
			file_system_watcher_mock = new Mock<IFileSystemWatcher>();
            harness_manager = 
				new HarnessManager(
					script_engine_mock.Object, 
					page_generator_mock.Object,
					file_system_mock.Object,
					file_system_watcher_mock.Object
				);
        };
    }
}
