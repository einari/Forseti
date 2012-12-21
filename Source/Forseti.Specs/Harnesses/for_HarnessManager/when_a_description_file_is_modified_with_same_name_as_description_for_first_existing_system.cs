using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using Forseti.Files;
using Forseti.Suites;
using Forseti.Pages;
using Forseti.Harnesses;

namespace Forseti.Specs.Harnesses.for_HarnessManager
{
    public class when_a_description_file_is_modified_with_same_name_as_description_for_first_existing_system : given.a_harness_manager_with_harness_and_fake_file_system_watcher
    {

        static File description_with_same_name_as_for_first_system;

        Establish context = () =>
            {
                description_with_same_name_as_for_first_system = (File)"Specs/for_nothing/when_doing_stuff.js";

                harness.RemoveSuites(harness.Suites.ToArray());

                harness.HandleFiles(new[] { system, another_system, 
                                                description_for_system, description2_for_system,
                                                description_for_another_system, description_with_same_name_as_for_first_system});
            };

        Because of = () => file_system_watcher.TriggerChange(Forseti.Files.FileChange.Modified, description_for_system);

        It should_mark_all_the_descriptions_in__nothing__suite_for_execution = () => 
                                                        {
                                                            harness_result_used_to_render_page.Cases.ShouldContain(c => c.Description.File == description_for_system);
                                                            harness_result_used_to_render_page.Cases.ShouldContain(c => c.Description.File == description2_for_system);
                                                        };
        It should_not_mark_any_other_suites_for_execution = () =>
                                                            {
                                                                harness_result_used_to_render_page.Cases.ShouldEachConformTo(c =>
                                                                    c.Description.Suite == harness_result_used_to_render_page.Suites.FirstOrDefault(s => s.SystemFile == system));

                                                                harness_result_used_to_render_page.Cases.Count().ShouldEqual(2);
                                                            };

    }

    
}
