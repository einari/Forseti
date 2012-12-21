using Moq;
using Machine.Specifications;
using Forseti.Frameworks;
using Forseti.Specs.Fakes;
using Forseti.Harnesses;
using Forseti.Files;
using Forseti.Suites;
using Forseti.Pages;

namespace Forseti.Specs.Harnesses.for_HarnessManager.given
{
    public class a_harness_manager_with_harness_and_fake_file_system_watcher : a_harness_manager_and_a_framework
	{
		protected static FakeFileSystemWatcher file_system_watcher;
        protected static File system;
        protected static File another_system;
        protected static File description_for_system;
        protected static File description2_for_system;
        protected static File description_for_another_system;
        protected static Case @case;
        protected static Page expected_page;
        protected static Harness harness;
        protected static Harness harness_result;
        protected static string system_search_path;
        protected static string description_search_path;

        Establish context = () =>
                {
                    harness_result = null;
                    file_system_watcher = new FakeFileSystemWatcher();
                        
                    system_search_path = "Script/{system}.js";
                    description_search_path = "Specs/for_{system}/{description}.js";

                    system = (File)"Script/something.js";
                    description_for_system = (File)"Specs/for_something/when_doing_stuff.js";
                    description2_for_system = (File)"Specs/for_something/when_doing_more_stuff.js";
                    
                    another_system = (File)"Script/nothing.js";
                    description_for_another_system = (File)"Specs/for_nothing/when_doing_nothing.js";

                    expected_page = new Page();
                    page_generator_mock.Setup(p => p.GenerateFrom(Moq.It.IsAny<Harness>())).Callback((Harness h) => harness_result = h).Returns(expected_page);


                    harness = new Harness(framework_mock.Object);

                    harness.SystemsSearchPath = system_search_path;
                    harness.DescriptionsSearchPath = description_search_path;

                    harness.HandleFiles(new[] { system, another_system, 
                                                description_for_system, description2_for_system,
                                                description_for_another_system});


                    harness_manager =
                        new HarnessManager(
                            script_engine_mock.Object,
                            page_generator_mock.Object,
                            file_system_mock.Object,
                            file_system_watcher,
                            harness_change_manager_mock.Object

                            );
                  
                    harness_manager.Add(harness);


                };
	}
}

