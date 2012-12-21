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
    public class when_a_description_file_is_modified_for_an_existing_system : given.a_harness_manager_with_harness_and_fake_file_system_watcher
    {
        Because of = () => file_system_watcher.TriggerChange(Forseti.Files.FileChange.Modified, description_for_system);

        It should_mark_all_cases_in__something__suite_for_execution = () => 
                                                        {
                                                            harness_result_used_to_render_page.Cases.ShouldContain(c => c.Description.File == description_for_system);
                                                            harness_result_used_to_render_page.Cases.ShouldContain(c => c.Description.File == description2_for_system);
                                                        };
        It should_not_contain_cases_for_another_system = () =>
                                                        {
                                                            harness_result_used_to_render_page.Cases.ShouldEachConformTo(c =>
                                                                c.Description.Suite == harness_result_used_to_render_page.Suites.FirstOrDefault(s => s.SystemFile == system));

                                                            harness_result_used_to_render_page.Cases.Count().ShouldEqual(2);
                                                        };

            

    }

    
}
